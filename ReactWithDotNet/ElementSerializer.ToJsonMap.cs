using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;

namespace ReactWithDotNet;

partial class ElementSerializer
{
    static readonly ConcurrentDictionary<Type, TypeInfo> TypeInfoMap = new();

    public static IReadOnlyJsonMap ToJsonMap(this Element element, ElementSerializerContext context)
    {
        var node = ConvertToNode(element);

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

            if (node.IsAllChildrenCompleted && node.ElementIsDotNetReactComponent is false && node.ElementIsDotNetReactPureComponent is false)
            {
                // Try Calculate ThirdParty Component Suspense Fallback
                if (context.CalculateSuspenseFallbackForThirdPartyReactComponents &&
                    node.ElementIsThirdPartyReactComponent &&
                    !node.IsSuspenseFallbackElementCalculated)
                {
                    node.IsSuspenseFallbackElementCalculated = true;

                    node.ElementAsThirdPartyReactComponent.Context = context.ReactContext;

                    node.SuspenseFallbackElement = node.ElementAsThirdPartyReactComponent.InvokeSuspenseFallback();
                    if (node.SuspenseFallbackElement is not null)
                    {
                        node.SuspenseFallbackElement.key = "0";
                    }

                    node.SuspenseFallbackNode = ConvertToNode(node.SuspenseFallbackElement);

                    node.SuspenseFallbackNode.Parent = node;

                    node.FirstChild = node.SuspenseFallbackNode;

                    node = node.SuspenseFallbackNode;

                    continue;
                }

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
                    OpenChildren(node);
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

                // Try Calculate ThirdParty Component Suspense Fallback
                if (context.CalculateSuspenseFallbackForThirdPartyReactComponents &&
                    node.ElementIsThirdPartyReactComponent &&
                    !node.IsSuspenseFallbackElementCalculated)
                {
                    node.IsSuspenseFallbackElementCalculated = true;

                    node.ElementAsThirdPartyReactComponent.Context = context.ReactContext;

                    node.SuspenseFallbackElement = node.ElementAsThirdPartyReactComponent.InvokeSuspenseFallback();
                    if (node.SuspenseFallbackElement is not null)
                    {
                        node.SuspenseFallbackElement.key = "0";
                    }

                    node.SuspenseFallbackNode = ConvertToNode(node.SuspenseFallbackElement);

                    node.SuspenseFallbackNode.Parent = node;

                    node.FirstChild = node.SuspenseFallbackNode;

                    node = node.SuspenseFallbackNode;

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

            if (node.ElementIsDotNetReactPureComponent)
            {
                var reactPureComponent = node.ElementAsDotNetReactPureComponent;

                if (node.DotNetComponentRenderMethodInvoked is false)
                {
                    reactPureComponent.Context = context.ReactContext;

                    node.DotNetComponentRenderMethodInvoked = true;

                    if (reactPureComponent.Modifiers is not null)
                    {
                        foreach (var modifier in reactPureComponent.Modifiers)
                        {
                            if (modifier is ReactPureComponentModifier pureComponentModifier)
                            {
                                pureComponentModifier.Modify(reactPureComponent);
                            }
                        }
                    }

                    node.DotNetComponentRootElement = reactPureComponent.InvokeRender();

                    if (node.DotNetComponentRootElement is not null)
                    {
                        node.DotNetComponentRootElement.key = "0";

                        if (reactPureComponent.StyleForRootElement is not null)
                        {
                            ModifyHelper.ProcessModifier(node.DotNetComponentRootElement, new StyleModifier(style => style.Import(reactPureComponent.StyleForRootElement)));
                        }

                        if (reactPureComponent.Modifiers is not null)
                        {
                            foreach (var modifier in reactPureComponent.Modifiers)
                            {
                                if (modifier is ReactPureComponentModifier)
                                {
                                    continue;
                                }

                                ModifyHelper.ProcessModifier(node.DotNetComponentRootElement, modifier);
                            }
                        }
                    }

                    node.DotNetComponentRootNode = ConvertToNode(node.DotNetComponentRootElement);

                    node.DotNetComponentRootNode.Parent = node;

                    node = node.DotNetComponentRootNode;

                    continue;
                }

                var map = new JsonMap();
                map.Add("$isPureComponent", 1);
                map.Add(___RootNode___, node.DotNetComponentRootNode.ElementAsJsonMap);
                map.Add(___Type___, GetReactComponentTypeInfo(reactPureComponent));
                map.Add(nameof(Element.key), reactPureComponent.key);

                node.ElementAsJsonMap = map;

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

                context.ComponentStack.Push(reactStatefulComponent);

                var stateTree = context.StateTree;

                var dotNetTypeOfReactComponent = reactStatefulComponent.GetType();

                var statePropertyInfo = dotNetTypeOfReactComponent.GetProperty("state");
                if (statePropertyInfo == null)
                {
                    throw new MissingMemberException(dotNetTypeOfReactComponent.GetFullName(), "state");
                }

                if (node.CurrentOrder is null)
                {
                    node.BreadCrumbPath = stateTree.BreadCrumbPath;
                    node.CurrentOrder   = stateTree.CurrentOrder;

                    if (statePropertyInfo.GetValue(reactStatefulComponent) is null)
                    {
                        stateTree.BreadCrumbPath = node.BreadCrumbPath + "," + stateTree.CurrentOrder;
                        stateTree.CurrentOrder   = 0;

                        if (true == stateTree.ChildStates?.TryGetValue(stateTree.BreadCrumbPath, out var clientStateInfo))
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

                        if (stateTree.BreadCrumbPath != "0")
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
                            if (modifier is ReactComponentModifier componentModifier)
                            {
                                componentModifier.Modify(node.ElementAsDotNetReactComponent);
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
                                if (modifier is ReactComponentModifier)
                                {
                                    continue;
                                }

                                ModifyHelper.ProcessModifier(node.DotNetComponentRootElement, modifier);
                            }
                        }
                    }

                    reactStatefulComponent.ConvertReactEventsToTaskForEventBus();

                    node.DotNetComponentRootNode = ConvertToNode(node.DotNetComponentRootElement);

                    node.DotNetComponentRootNode.Parent = node;

                    node = node.DotNetComponentRootNode;

                    continue;
                }

                state = statePropertyInfo.GetValue(reactStatefulComponent);

                const string dotNetState = "$State";

                var dotNetProperties = new JsonMap();

                var typeInfo = GetTypeInfo(dotNetTypeOfReactComponent);

                var propertyAccessors = typeInfo.DotNetPropertiesOfType;

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
                map.Add(dotNetState, state);
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

                if (typeInfo.IsReactHigherOrderComponent)
                {
                    map.Add("$LogicalChildrenCount", reactStatefulComponent._children?.Count ?? 0);
                }

                stateTree.BreadCrumbPath = node.BreadCrumbPath;
                stateTree.CurrentOrder   = node.CurrentOrder.Value;

                node.ElementAsJsonMap = map;

                node.IsCompleted = true;

                if (context.SkipHandleCacheableMethods is false)
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
                            SkipHandleCacheableMethods          = true,
                            StateTree = new StateTree
                            {
                                BreadCrumbPath = context.StateTree.BreadCrumbPath,
                                CurrentOrder   = context.StateTree.CurrentOrder,
                                ChildStates    = context.StateTree.ChildStates
                            }
                        };
                        elementSerializerContext.ComponentStack.Push(context.ComponentStack.Peek());
                        elementSerializerContext.DynamicStyles.ListOfClasses.AddRange(context.DynamicStyles.ListOfClasses);

                        return elementSerializerContext;
                    }

                    List<CacheableMethodInfo> cachedMethods = null;

                    foreach (var cacheableMethod in typeInfo.CacheableMethodInfoList)
                    {
                        CacheableMethodInfo cacheableMethodInfo;
                        {
                            var component = cloneComponent();

                            cacheableMethod.Invoke(component, new object[cacheableMethod.GetParameters().Length]);

                            var newElementSerializerContext = createNewElementSerializerContext();

                            var cachedVersion = ToJsonMap(component, newElementSerializerContext);

                            context.Tracer.Trace(newElementSerializerContext.Tracer.Messages);

                            // take back dynamic styles
                            context.DynamicStyles.ListOfClasses.Clear();
                            context.DynamicStyles.ListOfClasses.AddRange(newElementSerializerContext.DynamicStyles.ListOfClasses);

                            cacheableMethodInfo = new CacheableMethodInfo
                            {
                                MethodName       = cacheableMethod.GetNameWithToken(),
                                IgnoreParameters = true,
                                ElementAsJson    = cachedVersion
                            };
                        }

                        cachedMethods ??= new List<CacheableMethodInfo>();

                        cachedMethods.Add(cacheableMethodInfo);
                    }

                    ReactComponentBase cloneComponent()
                    {
                        var component = (ReactComponentBase)reactStatefulComponent.Clone();

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

                    foreach (var cacheableMethod in typeInfo.ParameterizedCacheableMethodInfoList)
                    {
                        IEnumerable parameters;
                        {
                            var nameofMethodForGettingParameters = cacheableMethod.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>()?.NameofMethodForGettingParameters;

                            var methodInfoForGettingParameters = dotNetTypeOfReactComponent.FindMethodOrGetProperty(nameofMethodForGettingParameters, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                            if (methodInfoForGettingParameters is null)
                            {
                                throw new InvalidOperationException($"Method not found method name is {nameofMethodForGettingParameters}");
                            }

                            parameters = (IEnumerable)methodInfoForGettingParameters.Invoke(reactStatefulComponent, Array.Empty<object>());
                            if (parameters == null)
                            {
                                throw new InvalidOperationException($"Method should return IEnumerable<{cacheableMethod.GetParameters().FirstOrDefault()}>");
                            }
                        }

                        foreach (var parameter in parameters)
                        {
                            CacheableMethodInfo cacheableMethodInfo;
                            {
                                var component = cloneComponent();

                                // invoke method
                                {
                                    try
                                    {
                                        cacheableMethod.Invoke(component, new[] { parameter });
                                    }
                                    catch (Exception exception)
                                    {
                                        throw new InvalidOperationException("Error occurred when calculating cache method", exception);
                                    }
                                }

                                var newElementSerializerContext = createNewElementSerializerContext();

                                var cachedVersion = ToJsonMap(component, newElementSerializerContext);

                                // take back dynamic styles
                                context.DynamicStyles.ListOfClasses.Clear();
                                context.DynamicStyles.ListOfClasses.AddRange(newElementSerializerContext.DynamicStyles.ListOfClasses);

                                cacheableMethodInfo = new CacheableMethodInfo
                                {
                                    MethodName    = cacheableMethod.GetNameWithToken(),
                                    Parameter     = parameter,
                                    ElementAsJson = cachedVersion
                                };
                            }

                            cachedMethods ??= new List<CacheableMethodInfo>();

                            cachedMethods.Add(cacheableMethodInfo);
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

                var componentAtTopOfStack = context.ComponentStack.Pop();
                if (!ReferenceEquals(componentAtTopOfStack, reactStatefulComponent))
                {
                    throw FatalError("component stack problem");
                }
            }
        }

        return node.ElementAsJsonMap;
    }

    static void AddReactAttributes(Action<string, object> add, Element element, ElementSerializerContext context)
    {
        var reactProperties = GetTypeInfo(element.GetType()).ReactAttributedPropertiesOfType;

        foreach (var item in reactProperties)
        {
            var propertyInfo = item.PropertyInfo;

            var valueExportInfo = GetPropertyValue(element, item, context);
            if (valueExportInfo.needToExport)
            {
                add(GetPropertyName(propertyInfo), valueExportInfo.value);
            }
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

                childElements.Add(child.ElementAsHtmlTextElement.innerText);
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

    static void ConvertReactEventsToTaskForEventBus(this ReactComponentBase reactComponent)
    {
        var type = reactComponent.GetType();

        var reactCustomEventProperties = GetTypeInfo(type).CustomEventPropertiesOfType;

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

            if (@delegate.Target is ReactComponentBase target)
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

    static Node ConvertToNode(Element element)
    {
        var node = new Node
        {
            Element = element
        };

        if (element is null)
        {
            node.ElementIsNull = true;
            return node;
        }

        if (element is HtmlTextNode htmlTextNode)
        {
            node.ElementIsHtmlTextElement = true;
            node.ElementAsHtmlTextElement = htmlTextNode;
            return node;
        }

        if (element is HtmlElement htmlElement)
        {
            node.ElementIsHtmlElement = true;
            node.ElementAsHtmlElement = htmlElement;
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

        if (element is ReactComponentBase dotNetComponent)
        {
            node.ElementIsDotNetReactComponent = true;
            node.ElementAsDotNetReactComponent = dotNetComponent;
            return node;
        }

        if (element is Fragment fragment)
        {
            node.ElementIsFragment = true;
            node.ElementAsFragment = fragment;
            return node;
        }

        if (element is ReactPureComponent pureComponent)
        {
            node.ElementIsDotNetReactPureComponent = true;
            node.ElementAsDotNetReactPureComponent = pureComponent;
            return node;
        }

        throw FatalError("Node type not recognized");
    }

    static Exception FatalError(string message)
    {
        return new Exception(message);
    }

    static TypeInfo GetTypeInfo(Type type)
    {
        if (!TypeInfoMap.TryGetValue(type, out var typeInfo))
        {
            var reactCustomEventProperties = new List<PropertyAccessInfo>();
            {
                foreach (var propertyInfo in type.GetSerializableProperties().Where(x => x.GetCustomAttribute<ReactCustomEventAttribute>() is not null))
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
            }

            var propertyAccessors = new List<PropertyAccessInfo>();
            {
                foreach (var propertyInfo in type.GetSerializableProperties())
                {
                    if (propertyInfo.Name == nameof(ReactComponentBase.Context)
                        || propertyInfo.Name == nameof(Element.children)
                        || propertyInfo.Name == nameof(ReactComponentBase.key)
                        || propertyInfo.Name == nameof(ReactComponentBase.Client)
                        || propertyInfo.Name == "state"
                        || propertyInfo.PropertyType.IsSubclassOf(typeof(Delegate))
                        || propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>() is not null
                        || propertyInfo.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>() is not null
                       )
                    {
                        continue;
                    }

                    if (propertyInfo.PropertyType == typeof(Element) || propertyInfo.PropertyType.IsSubclassOf(typeof(Element)))
                    {
                        continue;
                    }

                    if (propertyInfo.CanWrite == false && !propertyInfo.DeclaringType?.IsSubclassOf(typeof(ThirdPartyReactComponent)) == true)
                    {
                        continue;
                    }

                    propertyAccessors.Add(propertyInfo.ToFastAccess());
                }
            }

            var reactProperties = new List<PropertyAccessInfo>();
            {
                foreach (var propertyInfo in type.GetSerializableProperties().Where(x => x.GetCustomAttribute<ReactPropAttribute>() != null))
                {
                    reactProperties.Add(propertyInfo.ToFastAccess());
                }
            }

            typeInfo = new TypeInfo
            {
                CustomEventPropertiesOfType         = reactCustomEventProperties,
                DotNetPropertiesOfType              = propertyAccessors,
                ReactAttributedPropertiesOfType     = reactProperties,
                IsReactHigherOrderComponent         = type.GetCustomAttribute<ReactHigherOrderComponentAttribute>() is not null,
                CacheableMethodInfoList              = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodAttribute>() != null).ToArray(),
                ParameterizedCacheableMethodInfoList = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>() != null).ToArray()
            };

            TypeInfoMap.TryAdd(type, typeInfo);
        }

        return typeInfo;
    }

    static JsonMap LeafToMap(HtmlElement htmlElement, ElementSerializerContext context)
    {
        var map = new JsonMap();
        map.Add("$tag", htmlElement.Type);
        map.Add("key", htmlElement.key);

        if (htmlElement._style is not null)
        {
            var valueExportInfo = GetStylePropertyValueOfHtmlElementForSerialize(htmlElement, htmlElement._style, context);
            if (valueExportInfo.needToExport)
            {
                map.Add("style", valueExportInfo.value);
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

    static JsonMap LeafToMap(Node node, ThirdPartyReactComponent thirdPartyReactComponent, ElementSerializerContext context)
    {
        var map = new JsonMap();
        map.Add("$tag", thirdPartyReactComponent.Type);
        map.Add("key", thirdPartyReactComponent.key);

        if (thirdPartyReactComponent._style is not null)
        {
            var valueExportInfo = GetStylePropertyValueOfHtmlElementForSerialize(thirdPartyReactComponent, thirdPartyReactComponent._style, context);
            if (valueExportInfo.needToExport)
            {
                map.Add("style", valueExportInfo.value);
            }
        }

        AddReactAttributes(map.Add, thirdPartyReactComponent, context);

        if (context.CalculateSuspenseFallbackForThirdPartyReactComponents)
        {
            map.Add("SuspenseFallback", node.FirstChild.ElementAsJsonMap);
        }

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
            return LeafToMap(node, node.ElementAsThirdPartyReactComponent, context);
        }

        if (node.ElementIsFragment)
        {
            return LeafToMap(node.ElementAsFragment);
        }

        throw FatalError("Wrong Leaf");
    }

    static void OpenChildren(Node node)
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
            var childNode = ConvertToNode(item);

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
            GetValueFunc               = ReflectionHelper.CreateGetFunction(propertyInfo),
            PropertyInfo               = propertyInfo,
            DefaultValue               = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null,
            HasReactAttribute          = propertyInfo.GetCustomAttribute<ReactPropAttribute>() is not null,
            TransformValueInServerSide = getTransformValueInServerSideTransformFunction()
        };

        Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse> getTransformValueInServerSideTransformFunction()
        {
            var attribute = propertyInfo.GetCustomAttribute<ReactTransformValueInServerSideAttribute>();
            if (attribute == null)
            {
                return null;
            }

            var methodInfo = attribute.TransformMethodDeclaringType.GetMethod("Transform", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (methodInfo == null)
            {
                throw DeveloperException($"Type should have a static method named 'Transform'. @type:{attribute.TransformMethodDeclaringType}");
            }

            return (Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse>)methodInfo.CreateDelegate(typeof(Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse>));
        }
    }

    internal class PropertyAccessInfo
    {
        public object DefaultValue { get; init; }
        public Func<object, object> GetValueFunc { get; init; }
        public bool HasReactAttribute { get; init; }
        public PropertyInfo PropertyInfo { get; init; }
        public Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse> TransformValueInServerSide { get; init; }
    }

    class Node
    {
        public ReactPureComponent ElementAsDotNetReactPureComponent;
        public bool ElementIsDotNetReactPureComponent;
        public bool IsSuspenseFallbackElementCalculated;
        public Element SuspenseFallbackElement;
        public Node SuspenseFallbackNode;
        public string BreadCrumbPath { get; set; }
        public int? CurrentOrder { get; set; }
        public bool DotNetComponentRenderMethodInvoked { get; set; }
        public Element DotNetComponentRootElement { get; set; }
        public Node DotNetComponentRootNode { get; set; }

        public Element Element { get; init; }

        public ReactComponentBase ElementAsDotNetReactComponent { get; set; }

        public FakeChild ElementAsFakeChild { get; set; }
        public Fragment ElementAsFragment { get; set; }

        public HtmlElement ElementAsHtmlElement { get; set; }
        public HtmlTextNode ElementAsHtmlTextElement { get; set; }

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
    }

    sealed class TypeInfo
    {
        public IReadOnlyList<MethodInfo> CacheableMethodInfoList { get; init; }
        public IReadOnlyList<PropertyAccessInfo> CustomEventPropertiesOfType { get; init; }
        public IReadOnlyList<PropertyAccessInfo> DotNetPropertiesOfType { get; init; }
        public bool IsReactHigherOrderComponent { get; init; }
        public IReadOnlyList<MethodInfo> ParameterizedCacheableMethodInfoList { get; init; }
        public IReadOnlyList<PropertyAccessInfo> ReactAttributedPropertiesOfType { get; init; }
    }
}

internal record TransformValueInServerSideContext(Func<Style, string> ConvertStyleToCssClass);

internal record TransformValueInServerSideResponse(bool needToExport, object newValue = null);