using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ReactWithDotNet;

partial class ElementSerializer
{

    public static async Task<IReadOnlyJsonMap> ToJsonMap(this Element element, ElementSerializerContext context)
    {
        var tracer = context.Tracer;

        var node = ConvertToNode(element);

        while (true)
        {
            if (node.IsCompleted)
            {
                if (node.NextSibling is not null)
                {
                    node = node.NextSibling;
                    continue;
                }

                if (node.Parent is not null)
                {
                    node.Parent.IsAllChildrenCompleted = true;

                    node = node.Parent;

                    continue;
                }

                // root
                break;
            }

            if (node.ElementIsTask)
            {
                var realElement = await node.ElementAsTask.Value;

                if (node.ElementAsTask.Modifiers is not null)
                {
                    foreach (var modifier in node.ElementAsTask.Modifiers)
                    {
                        ModifyHelper.ProcessModifier(realElement, modifier);
                    }
                }

                node = ReplaceNode(node, ConvertToNode(realElement));

                continue;
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

            if (node.FirstChild is not null)
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
            
            if (!node.IsChildrenKeysInitialized)
            {
                InitializeKeyIfNotExists(node);    
            }
            

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

                if (node.FirstChild is not null)
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

                await context.TryCallBeforeSerializeElementToClient(node.Element, node.Parent?.Element);

                node.ElementAsJsonMap = await LeafToMap_Node(node, context);

                node.IsCompleted = true;

                continue;
            }

            if (node.ElementIsDotNetReactPureComponent)
            {
                node.Begin ??= tracer.ElapsedMilliseconds;

                var reactPureComponent = node.ElementAsDotNetReactPureComponent;

                if (reactPureComponent.ComponentUniqueIdentifier == 0)
                {
                    reactPureComponent.ComponentUniqueIdentifier = context.ComponentUniqueIdentifierNextValue++;
                }

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

                    node.DotNetComponentRootElement = await reactPureComponent.InvokeRender();
                    
                    if (node.DotNetComponentRootElement is not null)
                    {
                        node.DotNetComponentRootElement.key = "0";

                        if (reactPureComponent.StyleForRootElement is not null)
                        {
                            ModifyHelper.ProcessModifier(node.DotNetComponentRootElement, new StyleModifier(style => style.Import(reactPureComponent.StyleForRootElement)));
                        }

                        if (reactPureComponent.classNameList is not null)
                        {
                            foreach (var style in reactPureComponent.classNameList)
                            {
                                var response = ConvertStyleToCssClass(context, node, style, true, context.DynamicStyles.GetClassName);
                                if (response.needToExport)
                                {
                                    reactPureComponent.Add(ClassName(response.cssClassName));
                                }
                            }
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

                var typeInfo = reactPureComponent.GetType().Calculated();
                
                var map = new JsonMap();
                map.Add("$isPureComponent", 1);
                map.Add("$DotNetComponentUniqueIdentifier", reactPureComponent.ComponentUniqueIdentifier);
                map.Add(___RootNode___, node.DotNetComponentRootNode.ElementAsJsonMap);
                map.Add(___Type___, typeInfo.FullNameWithAssembly);
                map.Add(nameof(Element.key), reactPureComponent.key);

                await TransferProps(context,node,map,typeInfo,reactPureComponent);
                
                node.ElementAsJsonMap = map;

                node.IsCompleted = true;

                node.End ??= tracer.ElapsedMilliseconds;

                var duration = node.End - node.Begin;
                if ( duration >= 5)
                {
                    tracer.Trace(reactPureComponent.GetType().Name, duration.Value);
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

                node.Begin ??= tracer.ElapsedMilliseconds;

                if (node.IsFunctionalComponent is null)
                {
                    node.FunctionalComponent = reactStatefulComponent as FunctionalComponent;
                    if (node.FunctionalComponent is not null)
                    {
                        context.FunctionalComponentStack.Push(node.FunctionalComponent);

                        node.IsFunctionalComponent = true;
                    }
                    else
                    {
                        node.IsFunctionalComponent = false;
                    }
                }

                var stateTree = context.StateTree;

                var dotNetTypeOfReactComponent = reactStatefulComponent.GetType();

                var typeInfo = dotNetTypeOfReactComponent.Calculated();

                var stateProperty = typeInfo.StateProperty;

                if (stateProperty is null)
                {
                    throw new MissingMemberException(dotNetTypeOfReactComponent.SerializeToString(), "state");
                }

                static void InitializeNodeLocationToRoot(Node node)
                {
                    if (node.Parent is null)
                    {
                        node.location = node.Element.key;

                        return;
                    }

                    if (node.Parent.location is null)
                    {
                        InitializeNodeLocationToRoot(node.Parent);
                    }

                    Debug.Assert(node.Parent.location is not null);
                    Debug.Assert(node.location is null);

                    node.location = node.Parent.location + "," + node.Element.key;
                }

                if (node.location is null)
                {
                    InitializeNodeLocationToRoot(node);
                }

                if (stateProperty.GetValueFunc(reactStatefulComponent) is null)
                {
                    if (true == stateTree.ChildStates?.TryGetValue(node.location, out var clientStateInfo))
                    {
                        if (reactStatefulComponent.GetType().SerializeToString() == clientStateInfo.FullTypeNameOfComponent)
                        {
                            var stateValue = DeserializeJsonBySystemTextJson(clientStateInfo.StateAsJson, stateProperty.PropertyInfo.PropertyType);
                            stateProperty.SetValueFunc(reactStatefulComponent, stateValue);
                        }
                    }
                }

                reactStatefulComponent.Context = context.ReactContext;

                if (reactStatefulComponent.ComponentUniqueIdentifier == 0)
                {
                    reactStatefulComponent.ComponentUniqueIdentifier = context.ComponentUniqueIdentifierNextValue++;
                }

                var state = stateProperty.GetValueFunc(reactStatefulComponent);
                if (state == null)
                {

                    // invoke constructor
                    {
                        var construnctorTask = reactStatefulComponent.InvokeConstructor();
                        if (construnctorTask is null)
                        {
                            throw new DeveloperException($"{reactStatefulComponent.GetType().FullName} constructor should return task but null value returned.");
                        }

                        await construnctorTask;
                    }

                    if (reactStatefulComponent.IsStateNull)
                    {
                        // maybe developer forget init state
                        stateProperty.SetValueFunc(reactStatefulComponent, ReflectionHelper.CreateNewInstance(stateProperty.PropertyInfo.PropertyType));
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
                        var begin = tracer.ElapsedMilliseconds;

                        await reactStatefulComponent.OverrideStateFromPropsBeforeRender();

                        var end = tracer.ElapsedMilliseconds;
                        
                        var duration = end - begin;

                        if (duration > 10)
                        {
                            tracer.Trace($"{dotNetTypeOfReactComponent.Name}::OverrideStateFromPropsBeforeRender", duration);
                        }
                    }

                    node.DotNetComponentRootElement = await reactStatefulComponent.InvokeRender();
                    
                    if (node.IsFunctionalComponent == true)
                    {
                        if (node.FunctionalComponent.Constructor is not null &&
                            node.FunctionalComponent.state.Scope is null)
                        {
                            await node.FunctionalComponent.Constructor.Invoke();
                        }

                        node.FunctionalComponent.CalculateScopeFromTarget(context);
                    }

                    if (node.DotNetComponentRootElement is not null)
                    {
                        node.DotNetComponentRootElement.key = "0";

                        if (reactStatefulComponent.StyleForRootElement is not null)
                        {
                            ModifyHelper.ProcessModifier(node.DotNetComponentRootElement, new StyleModifier(style => style.Import(reactStatefulComponent.StyleForRootElement)));
                        }

                        if (reactStatefulComponent.classNameList is not null)
                        {
                            foreach (var style in reactStatefulComponent.classNameList)
                            {
                                var response = ConvertStyleToCssClass(context, node, style, true, context.DynamicStyles.GetClassName);
                                if (response.needToExport)
                                {
                                    reactStatefulComponent.Add(ClassName(response.cssClassName));
                                }
                            }
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

                    reactStatefulComponent.ConvertReactEventsToTaskForEventBus(context);

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
                        reactAttributeNames ??= [];
                        reactAttributeNames.Add(item.PropertyInfo.Name);
                    }
                }

                var map = new JsonMap();
                map.Add("$DotNetComponentUniqueIdentifier", reactStatefulComponent.ComponentUniqueIdentifier);
                map.Add(___RootNode___, node.DotNetComponentRootNode.ElementAsJsonMap);
                map.Add(dotNetState, state);
                map.Add(___Type___, typeInfo.FullNameWithAssembly);
                map.Add(nameof(Element.key), reactStatefulComponent.key);
                map.Add("DotNetProperties", dotNetProperties);

                if (reactAttributeNames is not null)
                {
                    map.Add("$ReactAttributeNames", reactAttributeNames);
                }

                if (typeInfo.ComponentDidMountMethod is not null)
                {
                    map.Add(___ComponentDidMountMethod___, typeInfo.ComponentDidMountMethod);
                }
                
                if (typeInfo.ComponentWillUnmountMethod is not null)
                {
                    map.Add(___ComponentWillUnmountMethod___, typeInfo.ComponentWillUnmountMethod);
                }

                if (node.IsFunctionalComponent == true)
                {
                    if (node.FunctionalComponent.ComponentDidMount is not null)
                    {
                        map.Add(___ComponentDidMountMethod___, node.FunctionalComponent.ComponentDidMount.Method.GetAccessKey());
                    }
                    
                    if (node.FunctionalComponent.ComponentWillUnmount is not null)
                    {
                        map.Add(___ComponentWillUnmountMethod___, node.FunctionalComponent.ComponentWillUnmount.Method.GetAccessKey());
                    }
                }

                if (reactStatefulComponent._client is not null && reactStatefulComponent._client.TaskList.Count > 0)
                {
                    map.Add("$ClientTasks", reactStatefulComponent._client.TaskList);
                }

                if (node.IsHighOrderComponent)
                {
                    map.Add("$LogicalChildrenCount", reactStatefulComponent._children?.Count ?? 0);
                }
                
                await TransferProps(context,node,map,typeInfo,reactStatefulComponent);

                node.ElementAsJsonMap = map;

                node.IsCompleted = true;

                if (node.IsFunctionalComponent is true)
                {
                    context.FunctionalComponentStack.Pop();
                }

                if (node.Begin is not null)
                {
                    node.End = tracer.ElapsedMilliseconds;

                    var duration = node.End - node.Begin;

                    if (duration >= 5)
                    {
                        tracer.Trace(node.IsFunctionalComponent is true ? node.FunctionalComponent.Name: dotNetTypeOfReactComponent.Name, duration.Value);
                    }
                }
            }
        }

        return node.ElementAsJsonMap;
    }
    
    static async Task TransferProps(ElementSerializerContext context, Node node, JsonMap map, TypeInfoCalculated calculatedType, object component)
    {
        var reactAttributes = calculatedType.ReactAttributedPropertiesOfType;
        if (reactAttributes.Count > 0)
        {
            var reactAttributesMap = new Dictionary<string, object>();
                    
            foreach (var calculatedPropertyInfo in reactAttributes)
            {
                var propertyValue = await GetPropertyValue(context, node, calculatedType, component, calculatedPropertyInfo);
                if (propertyValue.NeedToExport)
                {
                    reactAttributesMap.Add(calculatedPropertyInfo.PropertyInfo.Name, propertyValue.Value);
                }
            }
                    
            map.Add("$props",reactAttributesMap);
        }
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
                var childrenCount = (int)ArrangeValueForTargetType(value, typeof(int));
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

            property.SetValue(instance, ArrangeValueForTargetType(value, property.PropertyType));
        }

        return null;
    }

    static async Task AddReactAttributes(ElementSerializerContext context, Node node, JsonMap jsonMap, Element element)
    {
        var typeInfo = element.GetType().Calculated();

        var reactProperties = typeInfo.ReactAttributedPropertiesOfType;

        foreach (var item in reactProperties)
        {
            var valueExportInfo = await GetPropertyValue(context, node, typeInfo, element, item);
            if (valueExportInfo.NeedToExport)
            {
                jsonMap.Add(GetPropertyName(item), valueExportInfo.Value);
            }
        }
    }

    static async Task AddReactAttributes(ElementSerializerContext context, JsonMap jsonMap, HtmlElement element)
    {
        var current = element._head;
        while (current is not null)
        {
            if (current.propertyDefinition.jsonIgnore)
            {
                current = current.next;
                continue;
            }
            
            var value = await GetPropertyValueOfHtmlElement(context, element, current);

            jsonMap.Add(current.propertyDefinition.name, value);

            current = current.next;
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
                await context.TryCallBeforeSerializeElementToClient(child.Element, node.Element);

                (childElements ??= []).Add((object)child.ElementAsHtmlTextElement.innerText ?? child.ElementAsHtmlTextElement.stringBuilder);
            }
            else
            {
                (childElements ??= []).Add(child.ElementAsJsonMap);
            }

            child = child.NextSibling;
        }

        var map = await LeafToMap_Node(node, context);

        if (childElements is not null && !node.SkipProcessChildren)
        {
            map.Add("$children", childElements);
        }

        node.ElementAsJsonMap = map;

        node.IsCompleted = true;
    }

    static void ConvertReactEventsToTaskForEventBus(this ReactComponentBase reactComponent, ElementSerializerContext context)
    {
        var type = reactComponent.GetType();

        var reactCustomEventProperties = type.Calculated().CustomEventPropertiesOfType;

        foreach (var fastPropertyInfo in reactCustomEventProperties)
        {
            convertToTask(fastPropertyInfo);
        }

        return;

        void convertToTask(PropertyInfoCalculated fastPropertyInfo)
        {
            var @delegate = (Delegate)fastPropertyInfo.GetValueFunc(reactComponent);
            if (@delegate is null)
            {
                return;
            }

            var handlerDelegateTarget = @delegate.Target;
            if (handlerDelegateTarget is null)
            {
                throw DeveloperException(string.Join(Environment.NewLine,
                [
                    "Action handler method should belong to React component.",
                    "@delegate.Target: null",
                    $"@delegate.Method: {@delegate.Method}"
                ]));
            }

            if (handlerDelegateTarget is ReactComponentBase target)
            {
                var handlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier;

                if (handlerComponentUniqueIdentifier == 0)
                {
                    throw DeveloperException("ComponentUniqueIdentifier not initialized yet. @" + target.GetType().FullName);
                }

                var propertyInfo = fastPropertyInfo.PropertyInfo;

                propertyInfo.SetValue(reactComponent, null);

                var eventSenderInfo = GetEventSenderInfo(reactComponent, propertyInfo.Name);

                var handlerMethod = @delegate.Method.GetAccessKey();

                reactComponent.Client.InitializeDotnetComponentEventListener(eventSenderInfo, handlerMethod, handlerComponentUniqueIdentifier);

                return;
            }

            if (context.FunctionalComponentStack.Count > 0 && handlerDelegateTarget.GetType().IsCompilerGenerated())
            {
                var handlerComponent = context.FunctionalComponentStack.Peek();

                var handlerComponentUniqueIdentifier = handlerComponent.ComponentUniqueIdentifier;

                var propertyInfo = fastPropertyInfo.PropertyInfo;

                propertyInfo.SetValue(reactComponent, null);

                var eventSenderInfo = GetEventSenderInfo(reactComponent, propertyInfo.Name);

                var handlerMethod = @delegate.Method.GetAccessKey();

                reactComponent.Client.InitializeDotnetComponentEventListener(eventSenderInfo, handlerMethod, handlerComponentUniqueIdentifier);

                return;
            }

            throw DeveloperException(string.Join(Environment.NewLine,
            [
                "Action handler method should belong to React component.",
                $"@delegate.Target: {handlerDelegateTarget.GetType().FullName}",
                $"@delegate.Method: {@delegate.Method}"
            ]));
        }
    }

    static Node ConvertToNode(Element element)
    {
        if (element is null)
        {
            return new()
            {
                ElementIsNull = true
            };
        }

        if (element is HtmlTextNode htmlTextNode)
        {
            return new()
            {
                Element                  = element,
                ElementIsHtmlTextElement = true,
                ElementAsHtmlTextElement = htmlTextNode
            };
        }

        if (element is HtmlElement htmlElement)
        {
            return new()
            {
                Element              = element,
                ElementIsHtmlElement = true,
                ElementAsHtmlElement = htmlElement
            };
        }

        if (element is FakeChild fakeChild)
        {
            return new()
            {
                Element            = element,
                ElementIsFakeChild = true,
                ElementAsFakeChild = fakeChild
            };
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            return new()
            {
                Element                           = element,
                ElementIsThirdPartyReactComponent = true,
                ElementAsThirdPartyReactComponent = thirdPartyReactComponent
            };
        }

        if (element is ReactComponentBase dotNetComponent)
        {
            return new()
            {
                Element                       = element,
                ElementIsDotNetReactComponent = true,
                ElementAsDotNetReactComponent = dotNetComponent
            };
        }

        if (element is Fragment fragment)
        {
            return new()
            {
                Element           = element,
                ElementIsFragment = true,
                ElementAsFragment = fragment
            };
        }

        if (element is PureComponent pureComponent)
        {
            return new()
            {
                Element                           = element,
                ElementIsDotNetReactPureComponent = true,
                ElementAsDotNetReactPureComponent = pureComponent
            };
        }

        if (element is ElementAsTask elementAsTask)
        {
            return new()
            {
                Element       = element,
                ElementIsTask = true,
                ElementAsTask = elementAsTask
            };
        }
        
        throw FatalError("Node type not recognized");
    }

    static Exception FatalError(string message)
    {
        return new(message);
    }

    static JsonMap LeafToMap_Fragment(Fragment fragment)
    {
        var map = new JsonMap();

        map.Add("$tag", "React.Fragment");
        map.Add("key", fragment.key);

        return map;
    }

    static async Task<JsonMap> LeafToMap_HtmlElement(Node node, ElementSerializerContext context)
    {
        var htmlElement = node.ElementAsHtmlElement;

        var tag = htmlElement.__type__;
        
        var map = new JsonMap();
        
        map.Add("$tag", tag);
        map.Add("key", htmlElement.key);

        // export style
        {
            var style = htmlElement.style;

            var hasStyle = style.headNode is not null ||

                           // hasAnyPseudo
                           style._hover?.headNode is not null ||
                           style._active?.headNode is not null ||
                           style._after?.headNode is not null ||
                           style._before?.headNode is not null ||
                           style._focus?.headNode is not null ||
                           style._focusVisible?.headNode is not null ||

                           // hasMediaQueries
                           style._mediaQueries?.Count > 0;

            if (hasStyle)
            {
                var valueExportInfo = GetStylePropertyValueOfHtmlElementForSerialize(context, node, htmlElement, style);
                if (valueExportInfo.NeedToExport)
                {
                    map.Add("style", valueExportInfo.Value);
                }
            }
        }


        if (htmlElement.classNameList is not null)
        {
            foreach (var style in htmlElement.classNameList)
            {
                var response = ConvertStyleToCssClass(context, node, style, true, context.DynamicStyles.GetClassName);
                if (response.needToExport)
                {
                    htmlElement.AddClass(response.cssClassName);
                }
            }
        }

        await AddReactAttributes(context, map, htmlElement);

        if (htmlElement.innerText is not null)
        {
            map.Add("$text", htmlElement.innerText);
        }

        if (htmlElement._aria is not null)
        {
            foreach (var (key, value) in htmlElement._aria)
            {
                map.Add($"aria-{key.ToLower()}", value);
            }
        }

        if (htmlElement._data is not null)
        {
            foreach (var (key, value) in htmlElement._data)
            {
                map.Add($"data-{key.ToLower()}", value);
            }
        }

        if (tag == "style" && htmlElement._children?.Count > 1 && htmlElement.innerText is null)
        {
            var text = new StringBuilder();
            
            foreach (var child in htmlElement._children)
            {
                if (child is null)
                {
                    continue;
                }
                
                if (child is HtmlTextNode htmlTextNode)
                {
                    if (htmlTextNode.stringBuilder is not null)
                    {
                        text.Append(htmlTextNode.stringBuilder);
                    }
                    else
                    {
                        text.Append(htmlTextNode.innerText);
                    }
                    
                    continue;
                }

                throw DeveloperException("Invalid child in style tag");
            }
            
            map.Add("$text", text.ToString());

            node.SkipProcessChildren = true;
        }
        
        return map;
    }

    static async Task<JsonMap> LeafToMap_Node(Node node, ElementSerializerContext context)
    {
        if (node.ElementIsHtmlElement)
        {
            return await LeafToMap_HtmlElement(node, context);
        }

        if (node.ElementIsThirdPartyReactComponent)
        {
            return await LeafToMap_ThirdPartyReactComponent(node, node.ElementAsThirdPartyReactComponent, context);
        }

        if (node.ElementIsFragment)
        {
            return LeafToMap_Fragment(node.ElementAsFragment);
        }

        throw FatalError("Wrong Leaf");
    }

    static async Task<JsonMap> LeafToMap_ThirdPartyReactComponent(Node node, ThirdPartyReactComponent thirdPartyReactComponent, ElementSerializerContext context)
    {
        var map = new JsonMap();
        map.Add("$tag", thirdPartyReactComponent.Type);
        map.Add("key", thirdPartyReactComponent.key);

        if (thirdPartyReactComponent.HasStyle)
        {
            var valueExportInfo = GetStylePropertyValueOfHtmlElementForSerialize(context, node, thirdPartyReactComponent, thirdPartyReactComponent.style);
            if (valueExportInfo.NeedToExport)
            {
                map.Add("style", valueExportInfo.Value);
            }
        }

        await AddReactAttributes(context, node, map, thirdPartyReactComponent);

        if (context.CalculateSuspenseFallbackForThirdPartyReactComponents)
        {
            map.Add("$SuspenseFallback", node.SuspenseFallbackNode.ElementAsJsonMap);
        }

        return map;
    }

    static void OpenChildren(Node node)
    {
        node.IsChildrenOpened = true;

        var children = node.Element._children;
        if (children == null)
        {
            return;
        }

        var length = children.Count;
        if (length == 0)
        {
            return;
        }

        var child = node.FirstChild;
        if (child is not null)
        {
            while (child.NextSibling is not null)
            {
                child = child.NextSibling;
            }
        }

        var childrenAsSpan = CollectionsMarshal.AsSpan(children);
        for (var i = 0; i < length; i++)
        {
            var item = childrenAsSpan[i];
            
            
            var childNode = ConvertToNode(item);

            childNode.Parent = node;

            if (child == null)
            {
                node.FirstChild = childNode;

                child = childNode;

                continue;
            }

            child.NextSibling = childNode;

            childNode.PreviousSibling = child;

            child = childNode;
        }
    }

    static Node ReplaceNode(Node node, Node newNode)
    {
        newNode.Parent = node.Parent;

        if (node.Parent is not null)
        {
            if (node.Parent.FirstChild == node)
            {
                node.Parent.FirstChild = newNode;
            }

            if (node.Parent.DotNetComponentRootNode == node)
            {
                node.Parent.DotNetComponentRootNode = newNode;
            }
        }

        if (node.PreviousSibling is not null)
        {
            node.PreviousSibling.NextSibling = newNode;
        }

        newNode.NextSibling = node.NextSibling;

        if (newNode.NextSibling is not null)
        {
            newNode.NextSibling.PreviousSibling = newNode;
        }

        if (newNode.Element != null)
        {
            newNode.Element.key = node.Element.key;
        }

        return newNode;
    }







    sealed class Node
    {
        public PureComponent ElementAsDotNetReactPureComponent;
        public bool ElementIsDotNetReactPureComponent;
        public FunctionalComponent FunctionalComponent;

        public bool? IsFunctionalComponent;
        public bool IsSuspenseFallbackElementCalculated;
        public string location;
        public Node PreviousSibling;
        public Element SuspenseFallbackElement;
        public Node SuspenseFallbackNode;
        public long? Begin;
        public bool DotNetComponentRenderMethodInvoked;
        public Element DotNetComponentRootElement;
        public Node DotNetComponentRootNode;

        public Element Element;

        public ReactComponentBase ElementAsDotNetReactComponent;

        public FakeChild ElementAsFakeChild;
        public Fragment ElementAsFragment;

        public HtmlElement ElementAsHtmlElement;
        public HtmlTextNode ElementAsHtmlTextElement;

        public IReadOnlyJsonMap ElementAsJsonMap;
        public ElementAsTask ElementAsTask;

        public ThirdPartyReactComponent ElementAsThirdPartyReactComponent;

        public bool ElementIsDotNetReactComponent;

        public bool ElementIsFakeChild;
        public bool ElementIsFragment;
        public bool ElementIsHtmlElement;
        public bool ElementIsHtmlTextElement;

        public bool ElementIsNull;
        public bool ElementIsTask;

        public bool ElementIsThirdPartyReactComponent;
        public long? End;

        public Node FirstChild;

        public Node FirstChildTemp;

        

        
        public bool IsAllChildrenCompleted;

        public bool IsChildrenOpened;

        public bool IsCompleted;
        public bool IsHighOrderComponent;

        public Node NextSibling;

        public Node Parent;
        public bool SkipProcessChildren;
        public bool IsChildrenKeysInitialized;
    }
}

record TransformValueInServerSideContext(Func<Style, string> ConvertStyleToCssClass);

record TransformValueInServerSideResponse(bool needToExport, object newValue = null);

static class DoNotSendToClientWhenStyleEmpty
{
    public static TransformValueInServerSideResponse Transform(object value, TransformValueInServerSideContext transformContext)
    {
        var style = value as Style;

        if (style == null || style.headNode is null)
        {
            return new(false);
        }

        return new(true, value);
    }
}