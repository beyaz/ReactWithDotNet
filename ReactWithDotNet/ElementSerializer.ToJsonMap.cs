using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;

namespace ReactWithDotNet;

partial class ElementSerializer
{
    static readonly ConcurrentDictionary<Type, List<PropertyAccessInfo>> CustomEventPropertiesOfType = new();
    static readonly ConcurrentDictionary<Type, List<PropertyAccessInfo>> DotNetPropertiesOfType = new();
    static readonly ConcurrentDictionary<Type, List<PropertyAccessInfo>> ReactAttributedPropertiesOfType = new();

    public static IReadOnlyJsonMap ToJsonMap(this Element element, ElementSerializerContext context)
    {
        var node = ConvertToNode(element, context);

        while (true)
        {
            if (node.IsCompleted)
            {
                if (node.HasNextSibling)
                {
                    node = node.NextSibling;
                    continue;
                }

                if (node.HasParent)
                {
                    node.Parent.IsAllChildrenCompleted = true;

                    node = node.Parent;

                    continue;
                }

                // root
                break;
            }

            if (node.IsAllChildrenCompleted && node.ElementIsDotNetReactComponent is false)
            {
                CompleteWithChildren(node, context);

                continue;
            }

            if (node.HasFirstChild)
            {
                node = node.FirstChild;
                continue;
            }

            if (node.ElementIsNull)
            {
                node.IsCompleted = true;
                continue;
            }

            if (node.ElementIsFakeChild)
            {
                node.IsCompleted = true;
                var jsMap = new JsonMap();
                jsMap.Add("$FakeChild", node.ElementAsFakeChild.Index);
                node.ElementAsJsonMap = jsMap;
                continue;
            }

            InitializeKeyIfNotExists(node.Element);

            if (node.ElementIsHtmlTextElement)
            {
                node.IsCompleted = true;
                continue;
            }

            if (node.ElementIsHtmlElement || node.ElementIsThirdPartyReactComponent || node.ElementIsFragment)
            {
                if (node.IsChildrenOpened is false)
                {
                    OpenChildren(node, context);
                }

                if (node.ElementIsFragment)
                {
                    // Apply modifiers to children
                    node.ElementAsFragment.ArrangeChildren();
                }

                if (node.HasFirstChild)
                {
                    continue;
                }

                if (node.ElementIsHtmlElement)
                {
                    node.ElementAsHtmlElement.BeforeSerialize(node.Parent?.ElementAsHtmlElement);
                }

                context.TryCallBeforeSerializeElementToClient(node.Element);

                node.ElementAsJsonMap = LeafToMap(node, context);

                node.IsCompleted = true;

                continue;
            }

            if (node.ElementIsDotNetReactComponent is false)
            {
                throw FatalError("traverse problem");
            }

            // process React dot net component
            {
                var reactStatefulComponent = node.ElementAsDotNetReactComponent;

                context.componentStack.Push(reactStatefulComponent);

                var stateTree = context.StateTree;

                var dotNetTypeOfReactComponent = reactStatefulComponent.GetType();

                var statePropertyInfo = dotNetTypeOfReactComponent.GetProperty("state");
                if (statePropertyInfo == null)
                {
                    throw new MissingMemberException(dotNetTypeOfReactComponent.GetFullName(), "state");
                }

                if (node.CurrentOrder is null)
                {
                    node.BreadCrumpPath = stateTree.BreadCrumpPath;
                    node.CurrentOrder   = stateTree.CurrentOrder;

                    if (statePropertyInfo.GetValue(reactStatefulComponent) is null)
                    {
                        stateTree.BreadCrumpPath = node.BreadCrumpPath + "," + stateTree.CurrentOrder;
                        stateTree.CurrentOrder   = 0;

                        if (true == stateTree.ChildStates?.TryGetValue(stateTree.BreadCrumpPath, out var clientStateInfo))
                        {
                            if (statePropertyInfo.PropertyType.GetFullName() == clientStateInfo.FullTypeNameOfState)
                            {
                                if (reactStatefulComponent.GetType().GetFullName() == clientStateInfo.FullTypeNameOfComponent)
                                {
                                    var stateValue = DeserializeJson(clientStateInfo.StateAsJson, statePropertyInfo.PropertyType);
                                    statePropertyInfo.SetValue(reactStatefulComponent, stateValue);
                                }
                            }
                        }

                        if (stateTree.BreadCrumpPath != "0")
                        {
                            node.CurrentOrder++;
                        }
                    }
                }

                reactStatefulComponent.Context = context.ReactContext;

                reactStatefulComponent.ComponentUniqueIdentifier ??= context.ComponentUniqueIdentifierNextValue++;

                var state = statePropertyInfo.GetValue(reactStatefulComponent);
                if (state == null)
                {
                    reactStatefulComponent.InvokeConstructor();

                    if (reactStatefulComponent.IsStateNull)
                    {
                        // maybe developer forget init state
                        if (reactStatefulComponent is ReactComponent<EmptyState> reactComponent)
                        {
                            reactComponent.state = new EmptyState();
                        }
                        else
                        {
                            throw DeveloperException($"'state' property must be initialized in constructor. @component: {reactStatefulComponent.GetType().FullName}");
                        }
                    }
                }

                if (node.DotNetComponentRenderMethodInvoked is false)
                {
                    node.DotNetComponentRenderMethodInvoked = true;

                    if (reactStatefulComponent.modifiers is not null)
                    {
                        foreach (var modifier in reactStatefulComponent.modifiers)
                        {
                            if (modifier is ComponentModifier componentModifier)
                            {
                                componentModifier.modify(node.ElementAsDotNetReactComponent);
                            }
                        }
                    }

                    node.DotNetComponentRootElement = reactStatefulComponent.InvokeRender();

                    if (node.DotNetComponentRootElement is not null)
                    {
                        node.DotNetComponentRootElement.key = "0";

                        if (reactStatefulComponent._styleForRootElement is not null)
                        {
                            ModifyHelper.ProcessModifier(node.DotNetComponentRootElement, new StyleModifier(style => style.Import(reactStatefulComponent._styleForRootElement)));
                        }

                        if (reactStatefulComponent.modifiers is not null)
                        {
                            foreach (var modifier in reactStatefulComponent.modifiers)
                            {
                                if (modifier is ComponentModifier)
                                {
                                    continue;
                                }

                                ModifyHelper.ProcessModifier(node.DotNetComponentRootElement, modifier);
                            }
                        }
                    }

                    reactStatefulComponent.ConvertReactEventsToTaskForEventBus();

                    node.DotNetComponentRootNode = ConvertToNode(node.DotNetComponentRootElement, context);

                    node.DotNetComponentRootNode.Parent = node;

                    node = node.DotNetComponentRootNode;

                    continue;
                }

                state = statePropertyInfo.GetValue(reactStatefulComponent);

                const string DotNetState = "$State";

                var dotNetProperties = new JsonMap();

                if (!DotNetPropertiesOfType.TryGetValue(dotNetTypeOfReactComponent, out var propertyAccessors))
                {
                    propertyAccessors = new List<PropertyAccessInfo>();

                    foreach (var propertyInfo in dotNetTypeOfReactComponent.GetSerializableProperties())
                    {
                        if (propertyInfo.Name == nameof(reactStatefulComponent.Context)
                            || propertyInfo.Name == nameof(Element.children)
                            || propertyInfo.Name == nameof(reactStatefulComponent.key)
                            || propertyInfo.Name == nameof(reactStatefulComponent.Client)
                            || propertyInfo.Name == "state"
                            || propertyInfo.PropertyType.IsSubclassOf(typeof(Delegate))
                            || propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>() is not null
                            || propertyInfo.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>() is not null
                           )
                        {
                            continue;
                        }

                        propertyAccessors.Add(propertyInfo.ToFastAccess());
                    }

                    DotNetPropertiesOfType.TryAdd(dotNetTypeOfReactComponent, propertyAccessors);
                }

                List<string> reactAttributeNames = null;

                foreach (var item in propertyAccessors)
                {
                    var propertyValue = item.GetValueFunc(reactStatefulComponent);

                    if (item.DefaultValue == propertyValue)
                    {
                        continue;
                    }

                    dotNetProperties.Add(item.PropertyInfo.Name, propertyValue);

                    if (item.HasReactAttribute)
                    {
                        reactAttributeNames ??= new List<string>();
                        reactAttributeNames.Add(item.PropertyInfo.Name);
                    }
                }

                var map = new JsonMap();
                map.Add("$DotNetComponentUniqueIdentifier", reactStatefulComponent.ComponentUniqueIdentifier);
                map.Add(___RootNode___, node.DotNetComponentRootNode.ElementAsJsonMap);
                map.Add(DotNetState, state);
                map.Add(___Type___, GetReactComponentTypeInfo(reactStatefulComponent));
                map.Add(___TypeOfState___, GetTypeFullNameOfState(reactStatefulComponent));
                map.Add(nameof(Element.key), reactStatefulComponent.key);
                map.Add("DotNetProperties", dotNetProperties);

                if (reactAttributeNames is not null)
                {
                    map.Add("$ReactAttributeNames", reactAttributeNames);
                }

                if (HasComponentDidMountMethod(reactStatefulComponent))
                {
                    map.Add(___HasComponentDidMountMethod___, true);
                }

                if (reactStatefulComponent.Client.taskList.Count > 0)
                {
                    map.Add("$ClientTasks", reactStatefulComponent.Client.taskList);
                }

                stateTree.BreadCrumpPath = node.BreadCrumpPath;
                stateTree.CurrentOrder   = node.CurrentOrder.Value;

                node.ElementAsJsonMap = map;

                node.IsCompleted = true;

                if (context.SkipHandleCachableMethods is false)
                {
                    var stopwatch = new Stopwatch();

                    stopwatch.Start();

                    ElementSerializerContext createNewElementSerializerContext()
                    {
                        var elementSerializerContext = new ElementSerializerContext
                        {
                            BeforeSerializeElementToClient     = context.BeforeSerializeElementToClient,
                            ComponentUniqueIdentifierNextValue = context.ComponentUniqueIdentifierNextValue + 1,
                            ReactContext                       = context.ReactContext,
                            SkipHandleCachableMethods          = true,
                            StateTree = new StateTree
                            {
                                BreadCrumpPath = context.StateTree.BreadCrumpPath,
                                CurrentOrder   = context.StateTree.CurrentOrder,
                                ChildStates    = context.StateTree.ChildStates
                            }
                        };
                        elementSerializerContext.componentStack.Push(context.componentStack.Peek());
                        elementSerializerContext.DynamicStyles.listOfClasses.AddRange(context.DynamicStyles.listOfClasses);

                        return elementSerializerContext;
                    }

                    List<CachableMethodInfo> cachedMethods = null;

                    var cachableMethodInfoList = dotNetTypeOfReactComponent.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodAttribute>() != null).ToArray();

                    foreach (var cachableMethod in cachableMethodInfoList)
                    {
                        var component = cloneComponent();

                        cachableMethod.Invoke(component, new object[cachableMethod.GetParameters().Length]);

                        var newElementSerializerContext = createNewElementSerializerContext();

                        var cachedVersion = ToJsonMap(component, newElementSerializerContext);

                        context.Tracer.Trace(newElementSerializerContext.Tracer.traceMessages);

                        // take back dynamic styles
                        context.DynamicStyles.listOfClasses.Clear();
                        context.DynamicStyles.listOfClasses.AddRange(newElementSerializerContext.DynamicStyles.listOfClasses);

                        var cachableMethodInfo = new CachableMethodInfo
                        {
                            MethodName       = cachableMethod.GetNameWithToken(),
                            IgnoreParameters = true,
                            ElementAsJson    = cachedVersion
                        };

                        cachedMethods ??= new List<CachableMethodInfo>();

                        cachedMethods.Add(cachableMethodInfo);
                    }

                    ReactStatefulComponent cloneComponent()
                    {
                        var component = (ReactStatefulComponent)reactStatefulComponent.Clone();

                        dotNetProperties.Foreach(copyPropertyValueDeeply);

                        void copyPropertyValueDeeply(string dotNetPropertyName, object dotNetPropertyValue)
                        {
                            var noNeedToDeeplyAssign = dotNetPropertyValue is null || dotNetPropertyValue is string || dotNetPropertyValue.GetType().IsValueType;
                            if (noNeedToDeeplyAssign)
                            {
                                return;
                            }

                            var dotNetPropertyInfo = dotNetTypeOfReactComponent.GetProperty(dotNetPropertyName);
                            if (dotNetPropertyInfo == null)
                            {
                                throw new MissingMemberException(dotNetTypeOfReactComponent.FullName + "::" + dotNetPropertyName);
                            }

                            dotNetPropertyInfo.SetValue(component, ReflectionHelper.DeepCopy(dotNetPropertyInfo.GetValue(component)));
                        }

                        if (statePropertyInfo == null)
                        {
                            throw new Exception();
                        }

                        statePropertyInfo.SetValue(component, ReflectionHelper.DeepCopy(state));

                        component.Client = ReflectionHelper.DeepCopy(component.Client);

                        return component;
                    }

                    var parameterizedCachableMethodInfoList = dotNetTypeOfReactComponent.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>() != null);

                    foreach (var cachableMethod in parameterizedCachableMethodInfoList)
                    {
                        var nameofMethodForGettingParameters = cachableMethod.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>()?.NameofMethodForGettingParameters;

                        var methodInfoForGettingParameters = dotNetTypeOfReactComponent.FindMethodOrGetProperty(nameofMethodForGettingParameters, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        if (methodInfoForGettingParameters is null)
                        {
                            throw new InvalidOperationException($"Method not found method name is {nameofMethodForGettingParameters}");
                        }

                        var parameters = (IEnumerable)methodInfoForGettingParameters.Invoke(reactStatefulComponent, Array.Empty<object>());
                        if (parameters == null)
                        {
                            throw new InvalidOperationException($"Method should return IEnumerable<{cachableMethod.GetParameters().FirstOrDefault()}>");
                        }

                        foreach (var parameter in parameters)
                        {
                            var component = cloneComponent();

                            try
                            {
                                cachableMethod.Invoke(component, new[] { parameter });
                            }
                            catch (Exception exception)
                            {
                                throw new InvalidOperationException("Error occured when calculating cache method", exception);
                            }

                            var newElementSerializerContext = createNewElementSerializerContext();

                            var cachedVersion = ToJsonMap(component, newElementSerializerContext);

                            context.Tracer.Trace(newElementSerializerContext.Tracer.traceMessages);

                            // take back dynamic styles
                            context.DynamicStyles.listOfClasses.Clear();
                            context.DynamicStyles.listOfClasses.AddRange(newElementSerializerContext.DynamicStyles.listOfClasses);

                            var cachableMethodInfo = new CachableMethodInfo
                            {
                                MethodName    = cachableMethod.GetNameWithToken(),
                                Parameter     = parameter,
                                ElementAsJson = cachedVersion
                            };

                            cachedMethods ??= new List<CachableMethodInfo>();

                            cachedMethods.Add(cachableMethodInfo);
                        }
                    }

                    if (cachedMethods?.Count > 0)
                    {
                        map.Add("$CachedMethods", cachedMethods);
                    }

                    stopwatch.Stop();

                    if (stopwatch.ElapsedMilliseconds > 10)
                    {
                        context.Tracer.Trace($"{dotNetTypeOfReactComponent.FullName} cached methods calculation duration is {stopwatch.ElapsedMilliseconds} milliseconds");
                    }
                }

                var popudComponent = context.componentStack.Pop();
                if (!ReferenceEquals(popudComponent, reactStatefulComponent))
                {
                    throw FatalError("component stack problem");
                }
            }
        }

        return node.ElementAsJsonMap;
    }

    internal static IReadOnlyList<PropertyAccessInfo> GetReactAttributedPropertiesOfType(Type elementType)
    {
        if (!ReactAttributedPropertiesOfType.TryGetValue(elementType, out var reactProperties))
        {
            reactProperties = new List<PropertyAccessInfo>();

            foreach (var propertyInfo in elementType.GetSerializableProperties().Where(x => x.GetCustomAttribute<ReactAttribute>() != null))
            {
                reactProperties.Add(propertyInfo.ToFastAccess());
            }

            ReactAttributedPropertiesOfType.TryAdd(elementType, reactProperties);
        }

        return reactProperties;
    }

    static void AddReactAttributes(Action<string, object> add, Element element, ElementSerializerContext context)
    {
        var reactProperties = GetReactAttributedPropertiesOfType(element.GetType());

        foreach (var item in reactProperties)
        {
            var propertyInfo = item.PropertyInfo;

            var (propertyValue, noNeedToExport) = getPropertyValue(element, item, context);
            if (noNeedToExport)
            {
                continue;
            }

            add(GetPropertyName(propertyInfo), propertyValue);
        }
    }

    static void CompleteWithChildren(Node node, ElementSerializerContext context)
    {
        var childElements = new List<object>();

        var child = node.FirstChild;

        while (child is not null)
        {
            if (child.ElementIsHtmlTextElement)
            {
                context.TryCallBeforeSerializeElementToClient(child.Element);

                childElements.Add(child.ElementasHtmlTextElement.innerText);
            }
            else
            {
                childElements.Add(child.ElementAsJsonMap);
            }

            child = child.NextSibling;
        }

        var map = LeafToMap(node, context);

        map.Add("$children", childElements);

        node.ElementAsJsonMap = map;

        node.IsCompleted = true;
    }

    static void ConvertReactEventsToTaskForEventBus(this ReactStatefulComponent reactComponent)
    {
        var type = reactComponent.GetType();

        if (!CustomEventPropertiesOfType.TryGetValue(type, out var reactCustomEventProperties))
        {
            reactCustomEventProperties = new List<PropertyAccessInfo>();

            foreach (var propertyInfo in reactComponent.GetType().GetSerializableProperties().Where(x => x.GetCustomAttribute<ReactCustomEventAttribute>() is not null))
            {
                var isAction        = propertyInfo.PropertyType.FullName == typeof(Action).FullName;
                var isGenericAction = propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.IsGenericAction1or2or3();

                if (isAction || isGenericAction)
                {
                    reactCustomEventProperties.Add(propertyInfo.ToFastAccess());
                    continue;
                }

                throw DeveloperException("ReactCustomEventAttribute can only use with Action or Action<A> or Action<A,B> or Action<A,B,C>");
            }

            CustomEventPropertiesOfType.TryAdd(type, reactCustomEventProperties);
        }

        foreach (var fastPropertyInfo in reactCustomEventProperties)
        {
            convertToTask(fastPropertyInfo);
        }

        void convertToTask(PropertyAccessInfo fastPropertyInfo)
        {
            var @delegate = (Delegate)fastPropertyInfo.GetValueFunc(reactComponent);
            if (@delegate is null)
            {
                return;
            }

            if (@delegate.Target is ReactStatefulComponent target)
            {
                if (target.ComponentUniqueIdentifier is null)
                {
                    throw DeveloperException("ComponentUniqueIdentifier not initialized yet. @" + target.GetType().FullName);
                }

                var propertyInfo = fastPropertyInfo.PropertyInfo;

                propertyInfo.SetValue(reactComponent, null);

                reactComponent.Client.InitializeDotnetComponentEventListener(GetEventSenderInfo(reactComponent, propertyInfo.Name), @delegate.Method.GetNameWithToken(), target.ComponentUniqueIdentifier.GetValueOrDefault());
            }
            else
            {
                throw DeveloperException("Action handler method should belong to React component");
            }
        }
    }

    static Node ConvertToNode(Element element, ElementSerializerContext elementSerializerContext)
    {
        var node = new Node
        {
            Element           = element,
            SerializerContext = elementSerializerContext
        };

        if (element is null)
        {
            node.ElementIsNull = true;
            return node;
        }

        if (element is FakeChild fakeChild)
        {
            node.ElementIsFakeChild = true;
            node.ElementAsFakeChild = fakeChild;
            return node;
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            node.ElementIsThirdPartyReactComponent = true;
            node.ElementAsThirdPartyReactComponent = thirdPartyReactComponent;
            return node;
        }

        if (element is ReactStatefulComponent dotNetComponent)
        {
            node.ElementIsDotNetReactComponent = true;
            node.ElementAsDotNetReactComponent = dotNetComponent;
            return node;
        }

        if (element is HtmlTextNode htmlTextNode)
        {
            node.ElementIsHtmlTextElement = true;
            node.ElementasHtmlTextElement = htmlTextNode;
            return node;
        }

        if (element is Fragment fragment)
        {
            node.ElementIsFragment = true;
            node.ElementAsFragment = fragment;
            return node;
        }

        if (element is HtmlElement htmlElement)
        {
            node.ElementIsHtmlElement = true;
            node.ElementAsHtmlElement = htmlElement;
            return node;
        }

        throw FatalError("Node type not recognized");
    }

    static Exception FatalError(string message)
    {
        return new Exception(message);
    }

    static JsonMap LeafToMap(HtmlElement htmlElement, ElementSerializerContext context)
    {
        var map = new JsonMap();
        map.Add("$tag", htmlElement.Type);

        if (htmlElement._style is not null)
        {
            var (style, noNeedToExport) = GetStylePropertyValueOfHtmlElementForSerialize(htmlElement, htmlElement._style, context);
            if (noNeedToExport is false)
            {
                map.Add("style", style);
            }
        }

        AddReactAttributes(map.Add, htmlElement, context);

        if (htmlElement.innerText is not null)
        {
            map.Add("$text", htmlElement.innerText);
        }

        if (htmlElement._aria is not null)
        {
            foreach (var (key, value) in htmlElement._aria)
            {
                map.Add($"aria-{key}", value);
            }
        }

        if (htmlElement._data is not null)
        {
            foreach (var (key, value) in htmlElement._data)
            {
                map.Add($"data-{key}", value);
            }
        }

        return map;
    }

    static JsonMap LeafToMap(Fragment fragment)
    {
        var map = new JsonMap();

        map.Add("$tag", "React.Fragment");
        map.Add("key", fragment.key);

        return map;
    }

    static JsonMap LeafToMap(ThirdPartyReactComponent thirdPartyReactComponent, ElementSerializerContext context)
    {
        var map = new JsonMap();
        map.Add("$tag", thirdPartyReactComponent.Type);

        if (thirdPartyReactComponent._style is not null)
        {
            var (style, noNeedToExport) = GetStylePropertyValueOfHtmlElementForSerialize(thirdPartyReactComponent, thirdPartyReactComponent._style, context);
            if (noNeedToExport is false)
            {
                map.Add("style", style);
            }
        }

        AddReactAttributes(map.Add, thirdPartyReactComponent, context);

        return map;
    }

    static JsonMap LeafToMap(Node node, ElementSerializerContext context)
    {
        if (node.ElementIsHtmlElement)
        {
            return LeafToMap(node.ElementAsHtmlElement, context);
        }

        if (node.ElementIsThirdPartyReactComponent)
        {
            return LeafToMap(node.ElementAsThirdPartyReactComponent, context);
        }

        if (node.ElementIsFragment)
        {
            return LeafToMap(node.ElementAsFragment);
        }

        throw FatalError("Wrong Leaf");
    }

    static void OpenChildren(Node node, ElementSerializerContext context)
    {
        node.IsChildrenOpened = true;

        var children = node.Element._children;

        if (children == null || children.Count == 0)
        {
            return;
        }

        var child = node.FirstChild;
        if (child is not null)
        {
            while (child.HasNextSibling)
            {
                child = child.NextSibling;
            }
        }

        foreach (var item in children)
        {
            var childNode = ConvertToNode(item, context);

            childNode.Parent = node;

            if (child == null)
            {
                node.FirstChild = childNode;

                child = childNode;

                continue;
            }

            child.NextSibling = childNode;

            child = childNode;
        }
    }

    static PropertyAccessInfo ToFastAccess(this PropertyInfo propertyInfo)
    {
        return new PropertyAccessInfo
        {
            GetValueFunc      = ReflectionHelper.CreateGetFunction(propertyInfo),
            PropertyInfo      = propertyInfo,
            DefaultValue      = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null,
            HasReactAttribute = propertyInfo.GetCustomAttribute<ReactAttribute>() is not null
        };
    }

    internal class PropertyAccessInfo
    {
        public object DefaultValue { get; init; }
        public Func<object, object> GetValueFunc { get; init; }
        public bool HasReactAttribute { get; init; }
        public PropertyInfo PropertyInfo { get; init; }
    }

    class Node
    {
        public string BreadCrumpPath { get; set; }
        public int? CurrentOrder { get; set; }
        public bool DotNetComponentRenderMethodInvoked { get; set; }
        public Element DotNetComponentRootElement { get; set; }
        public Node DotNetComponentRootNode { get; set; }

        public Element Element { get; init; }

        public ReactStatefulComponent ElementAsDotNetReactComponent { get; set; }

        public FakeChild ElementAsFakeChild { get; set; }
        public Fragment ElementAsFragment { get; set; }

        public HtmlElement ElementAsHtmlElement { get; set; }
        public HtmlTextNode ElementasHtmlTextElement { get; set; }

        public IReadOnlyJsonMap ElementAsJsonMap { get; set; }

        public ThirdPartyReactComponent ElementAsThirdPartyReactComponent { get; set; }

        public bool ElementIsDotNetReactComponent { get; set; }

        public bool ElementIsFakeChild { get; set; }
        public bool ElementIsFragment { get; set; }
        public bool ElementIsHtmlElement { get; set; }
        public bool ElementIsHtmlTextElement { get; set; }

        public bool ElementIsNull { get; set; }

        public bool ElementIsThirdPartyReactComponent { get; set; }

        public Node FirstChild { get; set; }

        public bool HasFirstChild => FirstChild is not null;

        public bool HasNextSibling => NextSibling is not null;

        public bool HasParent => Parent is not null;
        public bool IsAllChildrenCompleted { get; set; }

        public bool IsChildrenOpened { get; set; }

        public bool IsCompleted { get; set; }

        public Node NextSibling { get; set; }

        public Node Parent { get; set; }

        public ElementSerializerContext SerializerContext { get; set; }
    }
}