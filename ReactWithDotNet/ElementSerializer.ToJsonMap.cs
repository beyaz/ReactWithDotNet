using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static ReactWithDotNet.JsonSerializationOptionHelper;

namespace ReactWithDotNet;

partial class ElementSerializer
{
    static readonly ConcurrentDictionary<Type, TypeInfo> TypeInfoMap = new();

    public static async Task<IReadOnlyJsonMap> ToJsonMap(this Element element, ElementSerializerContext context)
    {
        var tracer = context.Tracer;
        
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
                    node.ElementIsThirdPartyReactComponent)
                {
                    if (!node.IsSuspenseFallbackElementCalculated)
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

                        node.FirstChildTemp = node.FirstChild;
                    
                        node.FirstChild = node.SuspenseFallbackNode;

                        node = node.SuspenseFallbackNode;

                        continue;
                    }

                    node.FirstChild = node.FirstChildTemp;
                }

                await CompleteWithChildren(node, context);

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

                context.TryCallBeforeSerializeElementToClient(node.Element, node.Parent?.Element);

                node.ElementAsJsonMap = await LeafToMap(node, context);

                node.IsCompleted = true;

                continue;
            }

            if (node.ElementIsDotNetReactPureComponent)
            {

                node.Begin ??= tracer.ElapsedMilliseconds;
                
                
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
                
                node.End ??= tracer.ElapsedMilliseconds;

                if (node.End - node.Begin >= 3)
                {
                    tracer.Trace($"{reactPureComponent.GetType().FullName} rendered in {node.End - node.Begin} milliseconds");
                }

                continue;
            }

            if (node.ElementIsDotNetReactComponent is false)
            {
                throw FatalError("traverse problem");
            }

            // process React dot net component
            {
                var reactStatefulComponent = node.ElementAsDotNetReactComponent;

                if (node.Stopwatch is null)
                {
                    node.Stopwatch = new Stopwatch();
                    node.Stopwatch.Start();
                }

                if (context.ComponentStack.Count == 0)
                {
                    context.ComponentStack.Push(reactStatefulComponent);
                }
                else if (context.ComponentStack.TryPeek(out var top))
                {
                    if (!ReferenceEquals(top, reactStatefulComponent))
                    {
                        context.ComponentStack.Push(reactStatefulComponent);
                    }
                }

                var stateTree = context.StateTree;

                var dotNetTypeOfReactComponent = reactStatefulComponent.GetType();

                var typeInfo = GetTypeInfo(dotNetTypeOfReactComponent);

                var stateProperty = typeInfo.StateProperty;

                if (stateProperty is null)
                {
                    throw new MissingMemberException(dotNetTypeOfReactComponent.GetFullName(), "state");
                }

                if (node.CurrentOrder is null)
                {
                    node.BreadCrumbPath = stateTree.BreadCrumbPath;
                    node.CurrentOrder   = stateTree.CurrentOrder;

                    if (stateProperty.GetValueFunc(reactStatefulComponent) is null)
                    {
                        stateTree.BreadCrumbPath = node.BreadCrumbPath + "," + stateTree.CurrentOrder;
                        stateTree.CurrentOrder   = 0;

                        if (true == stateTree.ChildStates?.TryGetValue(stateTree.BreadCrumbPath, out var clientStateInfo))
                        {
                            if (reactStatefulComponent.GetType().GetFullName() == clientStateInfo.FullTypeNameOfComponent)
                            {
                                var stateValue = DeserializeJson(clientStateInfo.StateAsJson, stateProperty.PropertyInfo.PropertyType);
                                stateProperty.SetValueFunc(reactStatefulComponent, stateValue);
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

               

                var state = stateProperty.GetValueFunc(reactStatefulComponent);
                if (state == null)
                {
                    await reactStatefulComponent.InvokeConstructor();

                    if (reactStatefulComponent.IsStateNull)
                    {
                        // maybe developer forget init state
                        if (reactStatefulComponent is Component<EmptyState> reactComponent)
                        {
                            reactComponent.InitState(new EmptyState());
                        }
                        else
                        {
                            throw DeveloperException($"'state' property must be initialized in constructor. @component: {reactStatefulComponent.GetType().FullName}");
                        }
                    }
                }

                if (node.DotNetComponentRenderMethodInvoked is false)
                {
                    if (reactStatefulComponent._children?.Count > 0)
                    {
                        node.IsHighOrderComponent = true;
                    }
                    
                    node.DotNetComponentRenderMethodInvoked = true;

                    if (reactStatefulComponent.Modifiers is not null)
                    {
                        foreach (var modifier in reactStatefulComponent.Modifiers)
                        {
                            if (modifier is ReactComponentModifier componentModifier)
                            {
                                componentModifier.Modify(node.ElementAsDotNetReactComponent);
                            }
                        }
                    }

                    var getDerivedStateFromPropsMethodShouldInvoke = true;
                    if (getDerivedStateFromPropsMethodShouldInvoke)
                    {
                        var stopwatch = new Stopwatch();

                        stopwatch.Start();

                        await reactStatefulComponent.OverrideStateFromPropsBeforeRender();

                        stopwatch.Stop();
                        if (stopwatch.ElapsedMilliseconds > 10)
                        {
                            tracer.Trace($"{dotNetTypeOfReactComponent.FullName}::OverrideStateFromPropsBeforeRender  invoked in {stopwatch.ElapsedMilliseconds} milliseconds");
                        }
                    }

                    node.DotNetComponentRootElement = reactStatefulComponent.InvokeRender();

                    if (node.DotNetComponentRootElement is not null)
                    {
                        node.DotNetComponentRootElement.key = "0";

                        if (reactStatefulComponent.StyleForRootElement is not null)
                        {
                            ModifyHelper.ProcessModifier(node.DotNetComponentRootElement, new StyleModifier(style => style.Import(reactStatefulComponent.StyleForRootElement)));
                        }

                        if (reactStatefulComponent.Modifiers is not null)
                        {
                            foreach (var modifier in reactStatefulComponent.Modifiers)
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

                state = stateProperty.GetValueFunc(reactStatefulComponent);

                const string dotNetState = "$State";

                var dotNetProperties = new JsonMap();

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

                if (reactStatefulComponent.Client.TaskList.Count > 0)
                {
                    map.Add("$ClientTasks", reactStatefulComponent.Client.TaskList);
                }

                if (node.IsHighOrderComponent)
                {
                    map.Add("$LogicalChildrenCount", reactStatefulComponent._children?.Count ?? 0);
                }

                stateTree.BreadCrumbPath = node.BreadCrumbPath;
                stateTree.CurrentOrder   = node.CurrentOrder.Value;

                node.ElementAsJsonMap = map;

                node.IsCompleted = true;

                if (context.SkipHandleCacheableMethods is false)
                {
                    ElementSerializerContext createNewElementSerializerContext()
                    {
                        var elementSerializerContext = new ElementSerializerContext
                        {
                            Tracer                             = tracer,
                            BeforeSerializeElementToClient     = context.BeforeSerializeElementToClient,
                            ComponentUniqueIdentifierNextValue = context.ComponentUniqueIdentifierNextValue + 1,
                            ReactContext                       = context.ReactContext,
                            SkipHandleCacheableMethods         = true,
                            StateTree = new StateTree
                            {
                                BreadCrumbPath = context.StateTree.BreadCrumbPath,
                                CurrentOrder   = context.StateTree.CurrentOrder,
                                ChildStates    = context.StateTree.ChildStates
                            }
                        };

                        if (context.ComponentStack.TryPeek(out var top2))
                        {
                            elementSerializerContext.ComponentStack.Push(top2);
                        }

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

                            var cachedVersion = await ToJsonMap(component, newElementSerializerContext);

                           

                            // take back dynamic styles
                            context.DynamicStyles.ListOfClasses.Clear();
                            context.DynamicStyles.ListOfClasses.AddRange(newElementSerializerContext.DynamicStyles.ListOfClasses);
                            context.ComponentUniqueIdentifierNextValue = newElementSerializerContext.ComponentUniqueIdentifierNextValue;

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

                        dotNetProperties.Foreach((name,value)=>copyPropertyValueDeeply(dotNetTypeOfReactComponent,component, name, value));

                        stateProperty.SetValueFunc(component, ReflectionHelper.DeepCopy(state));

                        component.Client = ReflectionHelper.DeepCopy(component.Client);

                        return component;
                        
                        static void copyPropertyValueDeeply(Type dotNetTypeOfReactComponent, object component, string dotNetPropertyName, object dotNetPropertyValue)
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

                                var cachedVersion = await ToJsonMap(component, newElementSerializerContext);

                                // take back dynamic styles
                                context.DynamicStyles.ListOfClasses.Clear();
                                context.DynamicStyles.ListOfClasses.AddRange(newElementSerializerContext.DynamicStyles.ListOfClasses);
                                context.ComponentUniqueIdentifierNextValue = newElementSerializerContext.ComponentUniqueIdentifierNextValue;

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
                }

                if (context.ComponentStack.Count > 0)
                {
                    var componentAtTopOfStack = context.ComponentStack.Pop();
                    if (!ReferenceEquals(componentAtTopOfStack, reactStatefulComponent))
                    {
                        throw FatalError("component stack problem");
                    }
                }

                if (node.Stopwatch is not null)
                {
                    node.Stopwatch.Stop();

                    if (node.Stopwatch.ElapsedMilliseconds >= 3)
                    {
                        tracer.Trace($"{dotNetTypeOfReactComponent.FullName} rendered in {node.Stopwatch.ElapsedMilliseconds} milliseconds");
                    }
                }
            }
        }

        return node.ElementAsJsonMap;
    }

    static async Task AddReactAttributes(Action<string, object> add, Element element, ElementSerializerContext context)
    {
        var typeInfo = GetTypeInfo(element.GetType());

        var reactProperties = typeInfo.ReactAttributedPropertiesOfType;

        foreach (var item in reactProperties)
        {
            var propertyInfo = item.PropertyInfo;

            var valueExportInfo = await GetPropertyValue(typeInfo, element, item, context);
            if (valueExportInfo.NeedToExport)
            {
                add(GetPropertyName(propertyInfo), valueExportInfo.Value);
            }
        }
    }

    static async Task CompleteWithChildren(Node node, ElementSerializerContext context)
    {
        List<object> childElements = null;

        var child = node.FirstChild;

        while (child is not null)
        {
            if (child.ElementIsHtmlTextElement)
            {
                context.TryCallBeforeSerializeElementToClient(child.Element, node.Element);

                (childElements ??= new List<object>()).Add(child.ElementAsHtmlTextElement.innerText);
            }
            else
            {
                (childElements ??= new List<object>()).Add(child.ElementAsJsonMap);
            }

            child = child.NextSibling;
        }

        var map = await LeafToMap(node, context);

        if (childElements is not null)
        {
            map.Add("$children", childElements);
        }

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
        if (element is null)
        {
            return new Node
            {
                ElementIsNull = true
            };
        }

        if (element is HtmlTextNode htmlTextNode)
        {
            return new Node
            {
                Element                  = element,
                ElementIsHtmlTextElement = true,
                ElementAsHtmlTextElement = htmlTextNode
            };
        }

        if (element is HtmlElement htmlElement)
        {
            return new Node
            {
                Element              = element,
                ElementIsHtmlElement = true,
                ElementAsHtmlElement = htmlElement
            };
        }

        if (element is FakeChild fakeChild)
        {
            return new Node
            {
                Element            = element,
                ElementIsFakeChild = true,
                ElementAsFakeChild = fakeChild
            };
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            return new Node
            {
                Element                           = element,
                ElementIsThirdPartyReactComponent = true,
                ElementAsThirdPartyReactComponent = thirdPartyReactComponent
            };
        }

        if (element is ReactComponentBase dotNetComponent)
        {
            return new Node
            {
                Element                       = element,
                ElementIsDotNetReactComponent = true,
                ElementAsDotNetReactComponent = dotNetComponent
            };
        }

        if (element is Fragment fragment)
        {
            return new Node
            {
                Element           = element,
                ElementIsFragment = true,
                ElementAsFragment = fragment
            };
        }

        if (element is PureComponent pureComponent)
        {
            return new Node
            {
                Element                           = element,
                ElementIsDotNetReactPureComponent = true,
                ElementAsDotNetReactPureComponent = pureComponent
            };
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
            var serializableProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty);
            
            var reactCustomEventProperties = new List<PropertyAccessInfo>();
            {
                foreach (var propertyInfo in serializableProperties.Where(x => x.GetCustomAttribute<ReactCustomEventAttribute>() is not null))
                {
                    var isVoidTask = propertyInfo.PropertyType.FullName == typeof(Func<Task>).FullName;
                    var isVoidTaskWithParameter = propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.IsVoidTaskFunc1Or2Or3();

                    if (isVoidTask || isVoidTaskWithParameter)
                    {
                        reactCustomEventProperties.Add(propertyInfo.ToFastAccess());
                        continue;
                    }

                    throw DeveloperException($"{nameof(ReactCustomEventAttribute)} can only use with Func<Task> or Func<A,Task> or Func<A,B,Task> or Func<A,B,C,Task>");
                }
            }

            var propertyAccessors = new List<PropertyAccessInfo>();
            {
                foreach (var propertyInfo in serializableProperties)
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
                foreach (var propertyInfo in serializableProperties.Where(x => x.GetCustomAttribute<ReactPropAttribute>() != null))
                {
                    reactProperties.Add(propertyInfo.ToFastAccess());
                }
            }

            var getPropertyValueForSerializeToClientFunc =
                (Func<object, string, (bool needToExport, object value)>)
                type.GetMethod("GetPropertyValueForSerializeToClient", BindingFlags.NonPublic | BindingFlags.Static)
                    ?.CreateDelegate(typeof(Func<object, string, (bool needToExport, object value)>));

            typeInfo = new TypeInfo
            {
                CustomEventPropertiesOfType          = reactCustomEventProperties,
                DotNetPropertiesOfType               = propertyAccessors,
                ReactAttributedPropertiesOfType      = reactProperties,
                CacheableMethodInfoList              = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodAttribute>() != null).ToArray(),
                ParameterizedCacheableMethodInfoList = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>() != null).ToArray(),
                StateProperty                        = type.GetProperty("state", BindingFlags.NonPublic | BindingFlags.Instance)?.ToFastAccess(),

                GetPropertyValueForSerializeToClient = getPropertyValueForSerializeToClientFunc
            };

            TypeInfoMap.TryAdd(type, typeInfo);
        }

        return typeInfo;
    }

    static async Task<JsonMap> LeafToMap(HtmlElement htmlElement, ElementSerializerContext context)
    {
        var map = new JsonMap();
        map.Add("$tag", htmlElement.__type__);
        map.Add("key", htmlElement.key);

        if (htmlElement._style is not null)
        {
            var valueExportInfo = GetStylePropertyValueOfHtmlElementForSerialize(htmlElement, htmlElement._style, context);
            if (valueExportInfo.NeedToExport)
            {
                map.Add("style", valueExportInfo.Value);
            }
        }
        
        if (htmlElement.classNameList is not null)
        {
            foreach (var style in htmlElement.classNameList)
            {
                var response = ConvertStyleToCssClass(style, true, context.ComponentStack.PeekForComponentUniqueIdentifier(), context.DynamicStyles.GetClassName);
                if (response.needToExport)
                {
                    htmlElement.AddClass(response.cssClassName);
                }
            }
        }

        await AddReactAttributes(map.Add, htmlElement, context);

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

    static async Task<JsonMap> LeafToMap(Node node, ThirdPartyReactComponent thirdPartyReactComponent, ElementSerializerContext context)
    {
        var map = new JsonMap();
        map.Add("$tag", thirdPartyReactComponent.Type);
        map.Add("key", thirdPartyReactComponent.key);

        if (thirdPartyReactComponent.HasStyle)
        {
            var valueExportInfo = GetStylePropertyValueOfHtmlElementForSerialize(thirdPartyReactComponent, thirdPartyReactComponent.style, context);
            if (valueExportInfo.NeedToExport)
            {
                map.Add("style", valueExportInfo.Value);
            }
        }

        await AddReactAttributes(map.Add, thirdPartyReactComponent, context);

        if (context.CalculateSuspenseFallbackForThirdPartyReactComponents)
        {
            map.Add("$SuspenseFallback", node.SuspenseFallbackNode.ElementAsJsonMap);
        }

        return map;
    }

    static async Task<JsonMap> LeafToMap(Node node, ElementSerializerContext context)
    {
        if (node.ElementIsHtmlElement)
        {
            return await LeafToMap(node.ElementAsHtmlElement, context);
        }

        if (node.ElementIsThirdPartyReactComponent)
        {
            return await LeafToMap(node, node.ElementAsThirdPartyReactComponent, context);
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
            SetValueFunc               = ReflectionHelper.CreateSetFunction(propertyInfo),
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
        public Action<object, object> SetValueFunc { get; init; }
        public Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse> TransformValueInServerSide { get; init; }
    }

    sealed class Node
    {
        public PureComponent ElementAsDotNetReactPureComponent;
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

        public ReactComponentBase ElementAsDotNetReactComponent { get; init; }

        public FakeChild ElementAsFakeChild { get; init; }
        public Fragment ElementAsFragment { get; init; }

        public HtmlElement ElementAsHtmlElement { get; init; }
        public HtmlTextNode ElementAsHtmlTextElement { get; init; }

        public IReadOnlyJsonMap ElementAsJsonMap { get; set; }

        public ThirdPartyReactComponent ElementAsThirdPartyReactComponent { get; init; }

        public bool ElementIsDotNetReactComponent { get; init; }

        public bool ElementIsFakeChild { get; init; }
        public bool ElementIsFragment { get; init; }
        public bool ElementIsHtmlElement { get; init; }
        public bool ElementIsHtmlTextElement { get; init; }

        public bool ElementIsNull { get; init; }

        public bool ElementIsThirdPartyReactComponent { get; init; }

        public Node FirstChild { get; set; }
        
        public Node FirstChildTemp { get; set; }

        public bool HasFirstChild => FirstChild is not null;

        public bool HasNextSibling => NextSibling is not null;

        public bool HasParent => Parent is not null;
        public bool IsAllChildrenCompleted { get; set; }

        public bool IsChildrenOpened { get; set; }

        public bool IsCompleted { get; set; }

        public Node NextSibling { get; set; }

        public Node Parent { get; set; }
        public Stopwatch Stopwatch { get; set; }
        public long? Begin { get; set; }
        public long? End { get; set; }
        public bool IsHighOrderComponent { get; set; }
    }

    sealed class TypeInfo
    {
        public IReadOnlyList<MethodInfo> CacheableMethodInfoList { get; init; }
        public IReadOnlyList<PropertyAccessInfo> CustomEventPropertiesOfType { get; init; }
        public IReadOnlyList<PropertyAccessInfo> DotNetPropertiesOfType { get; init; }
        public Func<object, string, (bool needToExport, object value)> GetPropertyValueForSerializeToClient { get; init; }
        public IReadOnlyList<MethodInfo> ParameterizedCacheableMethodInfoList { get; init; }
        public IReadOnlyList<PropertyAccessInfo> ReactAttributedPropertiesOfType { get; init; }
        public PropertyAccessInfo StateProperty { get; init; }
    }


    internal static string TransferPropertiesToDotNetComponent(ReactComponentBase instance, Type type, IReadOnlyDictionary<string, object> props)
    {
        if (props == null)
        {
            return null;
        }

        foreach (var (key, value) in props)
        {
            if (key == "$LogicalChildrenCount")
            {
                var childrenCount = Convert.ToInt32(value);
                for (var i = 0; i < childrenCount; i++)
                {
                    instance.children.Add(new FakeChild { Index = i });
                }

                continue;
            }

            var property = type.GetProperty(key);
            if (property == null)
            {
                return $"Property not found.{type.FullName}::{key}";
            }

            property.SetValue(instance, RecalculatePropertyValue(property, value));
        }

        return null;

        static object RecalculatePropertyValue(PropertyInfo property, object value)
        {
            if (value is JToken jToken)
            {
                if (property.PropertyType == typeof(Style))
                {
                    var style = new Style();
                    style.Import(jToken.ToObject<Dictionary<string, string>>());
                    return style;
                }

                return jToken.ToObject(property.PropertyType);
            }

            if (value is string valueAsString && property.PropertyType == typeof(Type))
            {
                return JsonConverterForType.DeserializeType(valueAsString);
            }

            var changeResponse = ChangeType(value, property.PropertyType);
            if (changeResponse.exception is not null)
            {
                throw DeveloperException(changeResponse.exception.Message);
            }

            return  changeResponse.value;
        }
    }
}

internal record TransformValueInServerSideContext(Func<Style, string> ConvertStyleToCssClass);

internal record TransformValueInServerSideResponse(bool needToExport, object newValue = null);

static class DoNotSendToClientWhenStyleEmpty
{
    public static TransformValueInServerSideResponse Transform(object value, TransformValueInServerSideContext transformContext)
    {
        var style = value as Style;

        if (style == null || style.IsEmpty)
        {
            return new TransformValueInServerSideResponse(false);
        }

        return new TransformValueInServerSideResponse(true, value);
    }
}