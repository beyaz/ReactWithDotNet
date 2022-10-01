using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace ReactWithDotNet;

partial class ElementSerializer
{
    public static IReadOnlyDictionary<string, object> ToMap2(this Element element, ElementSerializerContext context)
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
                node.IsCompleted      = true;
                node.ElementAsJsonMap = new Dictionary<string, object> { { "$FakeChild", node.ElementAsFakeChild.Index } };
                continue;
            }

            InitializeKeyIfNotExists(node.Element, context);

            if (node.ElementIsHtmlTextElement)
            {
                node.IsCompleted = true;
                continue;
            }

            if (node.ElementIsHtmlElement || node.ElementIsThirdPartyReactComponent)
            {
                if (node.IsChildrenOpened is false)
                {
                    OpenChildren(node, context);
                }

                if (node.HasFirstChild)
                {
                    continue;
                }

                context.TryCallBeforeSerializeElementToClient(node.Element);

                node.ElementAsJsonMap = LeafToMap(node.Element, context);

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

                        if (true == stateTree.ChildStates?.TryGetValue(stateTree.BreadCrumpPath, out ClientStateInfo clientStateInfo))
                        {
                            if (statePropertyInfo.PropertyType.GetFullName() == clientStateInfo.FullTypeNameOfState)
                            {
                                var stateValue = Json.DeserializeJsonByNewtonsoft(clientStateInfo.StateAsJson, statePropertyInfo.PropertyType);
                                statePropertyInfo.SetValue(reactStatefulComponent, stateValue);
                            }
                        }

                        if (stateTree.BreadCrumpPath != "0")
                        {
                            node.CurrentOrder++;
                        }
                    }
                }

                reactStatefulComponent.Context = context.ReactContext;

                var state = statePropertyInfo.GetValue(reactStatefulComponent);
                if (state == null)
                {
                    reactStatefulComponent.InvokeConstructor();

                    // maybe developer forget init state
                    if (reactStatefulComponent is ReactComponent<EmptyState> reactComponent && reactComponent.state == null)
                    {
                        reactComponent.state = new EmptyState();
                    }
                }

                if (node.DotNetComponentRenderMethodInvoked is false)
                {
                    node.DotNetComponentRenderMethodInvoked = true;

                    node.DotNetComponentRootElement = reactStatefulComponent.InvokeRender();

                    node.DotNetComponentRootNode = ConvertToNode(node.DotNetComponentRootElement, context);

                    node.DotNetComponentRootNode.Parent = node;

                    node = node.DotNetComponentRootNode;

                    continue;
                }

                state = statePropertyInfo.GetValue(reactStatefulComponent);

                const string DotNetState = "$State";

                var dotNetProperties = new Dictionary<string, object>();

                foreach (var propertyInfo in dotNetTypeOfReactComponent.GetProperties())
                {
                    if (propertyInfo.Name == nameof(reactStatefulComponent.Context) ||
                        propertyInfo.Name == nameof(reactStatefulComponent.Children) ||
                        propertyInfo.Name == nameof(Element.children) ||
                        propertyInfo.Name == nameof(reactStatefulComponent.key) ||
                        propertyInfo.Name == nameof(reactStatefulComponent.ClientTask) ||
                        propertyInfo.Name == "state" ||
                        propertyInfo.PropertyType.IsSubclassOf(typeof(Delegate))
                       )
                    {
                        continue;
                    }

                    dotNetProperties.Add(propertyInfo.Name, propertyInfo.GetValue(reactStatefulComponent));
                }

                var map = new Dictionary<string, object>
                {
                    { ___RootNode___, node.DotNetComponentRootNode.ElementAsJsonMap },
                    { DotNetState, state },
                    { ___Type___, GetReactComponentTypeInfo(reactStatefulComponent) },
                    { ___TypeOfState___, GetTypeFullNameOfState(reactStatefulComponent) },
                    { nameof(Element.key), reactStatefulComponent.key },
                    { "DotNetProperties", dotNetProperties }
                };

                if (HasComponentDidMountMethod(reactStatefulComponent))
                {
                    map.Add(___HasComponentDidMountMethod___, true);
                }

                if (reactStatefulComponent.ClientTask.taskList.Count > 0)
                {
                    map.Add("$ClientTasks", reactStatefulComponent.ClientTask.taskList);
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
                            BeforeSerializeElementToClient = context.BeforeSerializeElementToClient,
                            ComponentRefId                 = context.ComponentRefId + 1,
                            ReactContext                   = context.ReactContext,
                            SkipHandleCachableMethods      = true,
                            StateTree = new StateTree
                            {
                                BreadCrumpPath = context.StateTree.BreadCrumpPath,
                                CurrentOrder   = context.StateTree.CurrentOrder,
                                ChildStates    = context.StateTree.ChildStates
                            }
                        };
                        elementSerializerContext.componentStack.Push(context.componentStack.Peek());

                        return elementSerializerContext;
                    }

                    List<CachableMethodInfo> cachedMethods = null;

                    var cachableMethodInfoList = dotNetTypeOfReactComponent.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodAttribute>() != null).ToArray();

                    foreach (var cachableMethod in cachableMethodInfoList)
                    {
                        var component = cloneComponent();

                        cachableMethod.Invoke(component, new object[cachableMethod.GetParameters().Length]);

                        var newElementSerializerContext = createNewElementSerializerContext();
                        
                        var cachedVersion = ToMap2(component, newElementSerializerContext);

                        context.Tracer.Trace(newElementSerializerContext.Tracer.traceMessages);

                        var cachableMethodInfo = new CachableMethodInfo
                        {
                            MethodName       = cachableMethod.Name,
                            IgnoreParameters = true,
                            ElementAsJson    = cachedVersion
                        };

                        cachedMethods ??= new List<CachableMethodInfo>();

                        cachedMethods.Add(cachableMethodInfo);
                    }

                    ReactStatefulComponent cloneComponent()
                    {
                        var component = (ReactStatefulComponent)reactStatefulComponent.Clone();

                        foreach (var (dotNetPropertyName, _) in dotNetProperties)
                        {
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

                        component.ClientTask = ReflectionHelper.DeepCopy(component.ClientTask);

                        return component;
                    }

                    var parameterizedCachableMethodInfoList = dotNetTypeOfReactComponent.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>() != null);

                    foreach (var cachableMethod in parameterizedCachableMethodInfoList)
                    {
                        var nameofMethodForGettingParameters = cachableMethod.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>()?.NameofMethodForGettingParameters;

                        var methodInfoForGettingParameters = dotNetTypeOfReactComponent.FindMethod(nameofMethodForGettingParameters, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

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
                            
                            var cachedVersion = ToMap2(component, newElementSerializerContext);

                            context.Tracer.Trace(newElementSerializerContext.Tracer.traceMessages);

                            var cachableMethodInfo = new CachableMethodInfo
                            {
                                MethodName    = cachableMethod.Name,
                                Parameter     = parameter,
                                ElementAsJson = cachedVersion
                            };

                            cachedMethods ??= new List<CachableMethodInfo>();

                            cachedMethods.Add(cachableMethodInfo);
                        }
                    }

                    if (cachedMethods?.Count  > 0)
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

    static void AddReactAttributes(Dictionary<string, object> map, Element htmlElement, ElementSerializerContext context)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        foreach (var propertyInfo in htmlElement.GetType().GetProperties().Where(x => x.GetCustomAttribute<ReactAttribute>() != null))
        {
            var (propertyValue, noNeedToExport) = getPropertyValue(htmlElement, propertyInfo, context);
            if (noNeedToExport)
            {
                continue;
            }

            map.Add(GetPropertyName(propertyInfo), propertyValue);
        }

        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds > 10)
        {
            context.Tracer.Trace($"{htmlElement.GetType().FullName} > attribute cacculation duration is {stopwatch.ElapsedMilliseconds} milliseconds");
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

        var map = LeafToMap(node.Element, context);

        map.Add("$children", childElements);

        node.ElementAsJsonMap = map;

        node.IsCompleted = true;
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

    static Dictionary<string, object> LeafToMap(HtmlElement htmlElement, ElementSerializerContext context)
    {
        var map = new Dictionary<string, object>
        {
            { "$tag", htmlElement.Type }
        };

        AddReactAttributes(map, htmlElement, context);

        if (htmlElement.innerText is not null)
        {
            map.Add("$text", htmlElement.innerText);
        }

        return map;
    }

    static Dictionary<string, object> LeafToMap(ThirdPartyReactComponent thirdPartyReactComponent, ElementSerializerContext context)
    {
        var map = new Dictionary<string, object>
        {
            { "$tag", thirdPartyReactComponent.Type }
        };

        AddReactAttributes(map, thirdPartyReactComponent, context);

        return map;
    }

    static Dictionary<string, object> LeafToMap(Element element, ElementSerializerContext context)
    {
        if (element is HtmlElement htmlElement)
        {
            return LeafToMap(htmlElement, context);
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            return LeafToMap(thirdPartyReactComponent, context);
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

        Node child = null;

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

    class Node
    {
        public string BreadCrumpPath { get; set; }
        public int? CurrentOrder { get; set; }
        public bool DotNetComponentRenderMethodInvoked { get; set; }
        public Element DotNetComponentRootElement { get; set; }
        public Node DotNetComponentRootNode { get; set; }

        public Element Element { get; set; }

        public ReactStatefulComponent ElementAsDotNetReactComponent { get; set; }

        public FakeChild ElementAsFakeChild { get; set; }

        public HtmlElement ElementAsHtmlElement { get; set; }
        public HtmlTextNode ElementasHtmlTextElement { get; set; }

        public IReadOnlyDictionary<string, object> ElementAsJsonMap { get; set; }

        public ThirdPartyReactComponent ElementAsThirdPartyReactComponent { get; set; }

        public bool ElementIsDotNetReactComponent { get; set; }

        public bool ElementIsFakeChild { get; set; }
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