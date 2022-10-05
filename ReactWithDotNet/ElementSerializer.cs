using System.Collections;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;


class Tracer
{
    public int traceIndentLevel;
    
    internal readonly LinkedList<string> traceMessages = new();

    public void Trace(string message)
    {
        traceMessages.AddLast("".PadRight(traceIndentLevel) + message);
    }
    public void Trace(LinkedList<string> messageList)
    {
        foreach (var message in messageList)
        {
            Trace(message);
        }
    }
}

sealed class ElementSerializerContext
{
    public readonly Tracer Tracer = new();

    internal readonly Stack<ReactStatefulComponent> componentStack = new();

    internal readonly DynamicStyleContentForEmbeddInClient DynamicStyles = new();

    readonly Stack<(int componentRefId, string breadCrumpPathInStateTree, int currentOrderInStateTree)> CapturedValuesForCachedMethods = new();

    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }

    public int ComponentRefId { get; set; }

    public ReactContext ReactContext { get; init; }

    public StateTree StateTree { get; init; }
    public bool SkipHandleCachableMethods { get; set; }

    public void EnterToModeWorkingForCachedMethods()
    {
        CapturedValuesForCachedMethods.Push((ComponentRefId, StateTree.BreadCrumpPath, StateTree.CurrentOrder));
    }

    public void ExitFromModeWorkingForCachedMethods()
    {
        var (componentRefId, breadCrumpPathInStateTree, currentOrderInStateTree) = CapturedValuesForCachedMethods.Pop();

        // restore previous values
        ComponentRefId           = componentRefId;
        StateTree.BreadCrumpPath = breadCrumpPathInStateTree;
        StateTree.CurrentOrder   = currentOrderInStateTree;
    }

    public string GetNextUniqueValue()
    {
        var nextUniqueValue = ComponentRefId.ToString();

        ComponentRefId++;

        return nextUniqueValue;
    }
}

static partial class ElementSerializer
{
    const string ___HasComponentDidMountMethod___ = "$HasComponentDidMountMethod";
    const string ___RootNode___ = "$RootNode";
    const string ___Type___ = "$Type";
    const string ___TypeOfState___ = "$TypeOfState";

    public static IReadOnlyDictionary<string, object> ToMap(this Element element, ElementSerializerContext context)
    {
        if (element == null)
        {
            return null;
        }

        if (element is FakeChild fakeChild)
        {
            return new Dictionary<string, object> { { "$FakeChild", fakeChild.Index } };
        }

        InitializeKeyIfNotExists(element, context);

        if (element is ReactStatefulComponent reactStatefulComponent)
        {
            // push
            {
                context.componentStack.Push(reactStatefulComponent);
            }

            // process
            var returnMap = ToMap(reactStatefulComponent, context);

            // pop
            {
                var popudComponent = context.componentStack.Pop();
                if (!ReferenceEquals(popudComponent, reactStatefulComponent))
                {
                    throw new Exception("Abdullah todo");
                }
            }

            return returnMap;
        }

        context.TryCallBeforeSerializeElementToClient(element);

        var map = new Dictionary<string, object>();

        var htmlElement = element as HtmlElement;

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            map.Add("$tag", thirdPartyReactComponent.Type);
        }
        else if (htmlElement is not null)
        {
            map.Add("$tag", htmlElement.Type);
        }

        foreach (var propertyInfo in element.GetType().GetProperties().Where(x => x.GetCustomAttribute<ReactAttribute>() != null))
        {
            var (propertyValue, noNeedToExport) = getPropertyValue(element, propertyInfo, context);
            if (noNeedToExport)
            {
                continue;
            }

            map.Add(GetPropertyName(propertyInfo), propertyValue);
        }

        if (htmlElement?.innerText is not null)
        {
            map.Add("$text", htmlElement.innerText);
        }

        if (element.children.Count > 0)
        {
            var childElements = new List<object>();

            foreach (var child in element.children)
            {
                if (child is HtmlTextNode textNode)
                {
                    context.TryCallBeforeSerializeElementToClient(child);
                    childElements.Add(textNode.innerText);
                    continue;
                }

                childElements.Add(ToMap(child, context));
            }

            map.Add("$children", childElements);
        }

        return map;
    }

    static BindInfo GetExpressionAsBindingInfo(PropertyInfo propertyInfo, ReactDefaultValueAttribute reactDefaultValueAttribute, Func<string[]> calculateSourcePathFunc)
    {
        var reactBindAttribute = propertyInfo.GetCustomAttribute<ReactBindAttribute>();
        if (reactBindAttribute == null)
        {
            return null;
        }

        string defaultValue = null;

        if (reactDefaultValueAttribute != null)
        {
            defaultValue = reactDefaultValueAttribute.DefaultValue;
        }

        return new BindInfo
        {
            targetProp    = reactBindAttribute.targetProp,
            eventName     = reactBindAttribute.eventName,
            sourcePath    = calculateSourcePathFunc(),
            IsBinding     = true,
            jsValueAccess = reactBindAttribute.jsValueAccess.Split('.', StringSplitOptions.RemoveEmptyEntries),
            defaultValue  = defaultValue
        };
    }

    static string GetPropertyName(PropertyInfo propertyInfo)
    {
        var propertyName = propertyInfo.Name;

        var jsonPropertyNameAttribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
        if (jsonPropertyNameAttribute != null)
        {
            propertyName = jsonPropertyNameAttribute.Name;
        }

        return propertyName;
    }

    static (object value, bool noNeedToExport) getPropertyValue(object instance, PropertyInfo propertyInfo, ElementSerializerContext context)
    {
        var propertyValue = propertyInfo.GetValue(instance);

        var reactDefaultValueAttribute = propertyInfo.GetCustomAttribute<ReactDefaultValueAttribute>();

        {
            var isDefaultValue = propertyValue == propertyInfo.PropertyType.GetDefaultValue();

            if (isDefaultValue)
            {
                if (reactDefaultValueAttribute != null)
                {
                    propertyValue = reactDefaultValueAttribute.DefaultValue;
                }
            }
        }

        // check inline
        {
            if (propertyValue is Style style)
            {
                var pseudos = new List<CssPseudoCodeInfo>();

                if (style._hover is not null)
                {
                    pseudos.Add(new CssPseudoCodeInfo
                    {
                        Name      = "hover",
                        BodyOfCss = style._hover.ToCss().Replace(";", " !important;")
                    });
                }

                if (style._before is not null)
                {
                    pseudos.Add(new CssPseudoCodeInfo
                    {
                        Name      = "before",
                        BodyOfCss = style._before.ToCss().Replace(";", " !important;")
                    });
                }

                if (style._after is not null)
                {
                    pseudos.Add(new CssPseudoCodeInfo
                    {
                        Name      = "after",
                        BodyOfCss = style._after.ToCss().Replace(";", " !important;")
                    });
                }

                if (style._active is not null)
                {
                    pseudos.Add(new CssPseudoCodeInfo
                    {
                        Name      = "active",
                        BodyOfCss = style._active.ToCss().Replace(";", " !important;")
                    });
                }

                if (pseudos.Count > 0)
                {
                    ((HtmlElement)instance).AddClass(context.DynamicStyles.GetClassName(new CssClassInfo
                    {
                        Name    = context.componentStack.Peek().GetType().FullName?.Replace(".", "_").Replace("+", "_").Replace("/", "_"),
                        Pseudos = pseudos
                    }));
                }

                if (IsEmptyStyle(style))
                {
                    return (null, true);
                }

                return (style, false);
            }
        }

        {
            var isDefaultValue = propertyValue == propertyInfo.PropertyType.GetDefaultValue();
            if (isDefaultValue)
            {
                return (null, true);
            }
        }

        if (propertyValue is Action action)
        {
            if (action.Target is ReactStatefulComponent target)
            {
                propertyValue = new RemoteMethodInfo { IsRemoteMethod = true, remoteMethodName = action.Method.Name, TargetKey = target.key };
            }
            else
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyInfo);
            }
        }

        if (propertyInfo.PropertyType.IsGenericType)
        {
            if (propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Action<>))
            {
                var @delegate = (Delegate)propertyValue;
                if (@delegate is not null)
                {
                    if (@delegate.Target is ReactStatefulComponent target)
                    {
                        propertyValue = new RemoteMethodInfo
                        {
                            IsRemoteMethod                   = true,
                            remoteMethodName                 = @delegate.Method.Name,
                            TargetKey                        = target.key,
                            FunctionNameOfGrabEventArguments = propertyInfo.GetCustomAttribute<ReactGrabEventArgumentsByUsingFunctionAttribute>()?.TransformFunction,
                            StopPropagation                  = @delegate.Method.GetCustomAttribute<ReactStopPropagationAttribute>() is not null
                        };
                    }
                    else
                    {
                        throw HandlerMethodShouldBelongToReactComponent(propertyInfo);
                    }
                }
            }
        }

        if (propertyValue is Enum enumValue)
        {
            propertyValue = enumValue.ToString();
        }

        if (propertyValue is Expression<Func<int>> ||
            propertyValue is Expression<Func<string>> ||
            propertyValue is Expression<Func<bool>>)
        {
            string[] calculateSourcePathFunc()
            {
                if (propertyValue is Expression<Func<string>> bindingExpressionAsString)
                {
                    return bindingExpressionAsString.AsBindingSourcePathInState().Split(".".ToCharArray());
                }

                if (propertyValue is Expression<Func<int>> bindingExpressionAsInt32)
                {
                    return bindingExpressionAsInt32.AsBindingSourcePathInState().Split(".".ToCharArray());
                }

                if (propertyValue is Expression<Func<bool>> bindingExpressionAsBoolean)
                {
                    return bindingExpressionAsBoolean.AsBindingSourcePathInState().Split(".".ToCharArray());
                }

                throw new NotImplementedException();
            }

            var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, calculateSourcePathFunc);
            if (bindInfo == null)
            {
                return (null, true);
            }

            return (bindInfo, false);
        }

        if (propertyValue is Element element)
        {
            propertyValue = new InnerElementInfo
            {
                IsElement = true,
                Element   = element.ToMap(context)
            };
        }

        if (propertyInfo.GetCustomAttribute<ReactTemplateAttribute>() is not null)
        {
            var method = instance.GetType().GetMethod("GetItemTemplates", BindingFlags.Instance | BindingFlags.NonPublic);
            if (method == null)
            {
                throw new MissingMethodException("GetItemTemplates");
            }

            var func = (Delegate)propertyInfo.GetValue(instance);

            if (func == null)
            {
                return (null, true);
            }

            var itemTemplates = (List<KeyValuePair<object, object>>)method.Invoke(instance, new object[]
            {
                (Func<object, IReadOnlyDictionary<string, object>>)(item => ((Element)func.DynamicInvoke(item)).ToMap(context))
            });

            var template = new ItemTemplate
            {
                ___ItemTemplates___ = itemTemplates
            };

            if (propertyInfo.GetCustomAttribute<ReactTemplateForNullAttribute>() is not null)
            {
                template.___TemplateForNull___ = Try(() => ((Element)func.DynamicInvoke((object)null))?.ToMap(context)).value;
            }

            return (template, false);
        }

        var reactTransformValueInClient = propertyInfo.GetCustomAttribute<ReactTransformValueInClientAttribute>();
        if (reactTransformValueInClient is not null)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "$transformValueFunction", reactTransformValueInClient.TransformFunction },
                { "RawValue", propertyValue }
            };

            return (dictionary, false);
        }

        return (propertyValue, false);
    }

    static string GetReactComponentTypeInfo(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetFullName();
    }

    static string GetTypeFullNameOfState(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetProperty("state")!.PropertyType.GetFullName();
    }

    static Exception HandlerMethodShouldBelongToReactComponent(PropertyInfo propertyInfo)
    {
        throw new InvalidOperationException("Delegate method should belong to ReactComponent. Please give named method to " + propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name);
    }

    static bool HasComponentDidMountMethod(object reactStatefulComponent)
    {
        var componentType = reactStatefulComponent.GetType();

        var didMountMethodInfo = componentType.FindMethod("componentDidMount", BindingFlags.NonPublic | BindingFlags.Instance);
        if (didMountMethodInfo != null)
        {
            if (didMountMethodInfo.DeclaringType != typeof(ReactStatefulComponent))
            {
                return true;
            }
        }

        return false;
    }

    static void InitializeKeyIfNotExists(Element element, ElementSerializerContext context)
    {
        element.key ??= context.GetNextUniqueValue();

        foreach (var sibling in element.children.Where(sibling => sibling != null))
        {
            sibling.key ??= context.GetNextUniqueValue();
        }
    }

    static bool IsEmptyStyle(object value)
    {
        if (value is Style style)
        {
            return style.IsEmpty;
        }

        return false;
    }

    static IReadOnlyDictionary<string, object> ToMap(ReactStatefulComponent reactStatefulComponent, ElementSerializerContext context, bool handleCachableMethods = true)
    {
        var statePropertyInfo = reactStatefulComponent.GetType().GetProperty("state");
        if (statePropertyInfo == null)
        {
            throw new MissingMemberException(reactStatefulComponent.GetType().GetFullName(), "state");
        }

        var map = new Dictionary<string, object>();

        var stateTree      = context.StateTree;
        var breadCrumpPath = stateTree.BreadCrumpPath;
        var stateOrder     = stateTree.CurrentOrder;

        if (statePropertyInfo.GetValue(reactStatefulComponent) is null)
        {
            stateTree.BreadCrumpPath = breadCrumpPath + "," + stateTree.CurrentOrder;
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
                stateOrder++;
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

        map.Add(___RootNode___, ToMap(reactStatefulComponent.InvokeRender(), context));

        state = statePropertyInfo.GetValue(reactStatefulComponent);

        const string DotNetState = "$State";

        map.Add(DotNetState, state);

        map.Add(___Type___, GetReactComponentTypeInfo(reactStatefulComponent));
        map.Add(___TypeOfState___, GetTypeFullNameOfState(reactStatefulComponent));
        if (HasComponentDidMountMethod(reactStatefulComponent))
        {
            map.Add(___HasComponentDidMountMethod___, true);
        }

        map.Add(nameof(reactStatefulComponent.key), reactStatefulComponent.key);

        if (reactStatefulComponent.ClientTask.taskList.Count > 0)
        {
            map.Add("$ClientTasks", reactStatefulComponent.ClientTask.taskList);
        }

        var dotNetProperties = new Dictionary<string, object>();

        foreach (var propertyInfo in reactStatefulComponent.GetType().GetProperties())
        {
            if (propertyInfo.Name == nameof(reactStatefulComponent.Context) ||
                propertyInfo.Name == nameof(reactStatefulComponent.Children) ||
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

        map.Add("DotNetProperties", dotNetProperties);

        stateTree.BreadCrumpPath = breadCrumpPath;
        stateTree.CurrentOrder   = stateOrder;

        if (handleCachableMethods)
        {
            List<CachableMethodInfo> cachedMethods = null;

            foreach (var cachableMethod in reactStatefulComponent.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodAttribute>() != null))
            {
                context.EnterToModeWorkingForCachedMethods();

                var component = cloneComponent();

                cachableMethod.Invoke(component, new object[cachableMethod.GetParameters().Length]);

                var cachedVersion = ToMap(component, context, false);

                var cachableMethodInfo = new CachableMethodInfo
                {
                    MethodName       = cachableMethod.Name,
                    IgnoreParameters = true,
                    ElementAsJson    = cachedVersion
                };

                cachedMethods ??= new List<CachableMethodInfo>();

                cachedMethods.Add(cachableMethodInfo);

                context.ExitFromModeWorkingForCachedMethods();
            }

            ReactStatefulComponent cloneComponent()
            {
                var component = (ReactStatefulComponent)reactStatefulComponent.Clone();

                foreach (var (key, _) in dotNetProperties)
                {
                    var dotNetPropertyInfo = component.GetType().GetProperty(key);
                    if (dotNetPropertyInfo == null)
                    {
                        throw new Exception();
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

            foreach (var cachableMethod in reactStatefulComponent.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>() != null))
            {
                var nameofMethodForGettingParameters = cachableMethod.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>()?.NameofMethodForGettingParameters;

                var methodInfoForGettingParameters = reactStatefulComponent.GetType().FindMethod(nameofMethodForGettingParameters, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                var parameters = (IEnumerable)methodInfoForGettingParameters.Invoke(reactStatefulComponent, Array.Empty<object>());
                if (parameters == null)
                {
                    throw new InvalidOperationException($"Method should return IEnumerable<{cachableMethod.GetParameters().FirstOrDefault()}>");
                }

                foreach (var parameter in parameters)
                {
                    context.EnterToModeWorkingForCachedMethods();

                    var component = cloneComponent();

                    try
                    {
                        cachableMethod.Invoke(component, new[] { parameter });
                    }
                    catch (Exception exception)
                    {
                        throw new InvalidOperationException("Error occured when calculating cache method", exception);
                    }

                    var cachedVersion = ToMap(component, context, false);

                    var cachableMethodInfo = new CachableMethodInfo
                    {
                        MethodName    = cachableMethod.Name,
                        Parameter     = parameter,
                        ElementAsJson = cachedVersion
                    };

                    cachedMethods ??= new List<CachableMethodInfo>();

                    cachedMethods.Add(cachableMethodInfo);

                    context.ExitFromModeWorkingForCachedMethods();
                }
            }

            if (cachedMethods?.Any() == true)
            {
                map.Add("$CachedMethods", cachedMethods);
            }
        }

        return map;
    }

    static (T value, Exception exception) Try<T>(Func<T> func)
    {
        try
        {
            return (func(), null);
        }
        catch (Exception exception)
        {
            return (default, exception);
        }
    }

    static void TryCallBeforeSerializeElementToClient(this ElementSerializerContext context, Element element)
    {
        if (element is null || context.BeforeSerializeElementToClient is null)
        {
            return;
        }

        context.BeforeSerializeElementToClient(element, context.ReactContext);
    }

    class CachableMethodInfo
    {
        public object ElementAsJson { get; set; }
        public bool IgnoreParameters { get; set; }
        public string MethodName { get; set; }
        public object Parameter { get; set; }
    }
}

class ItemTemplate
{
    public List<KeyValuePair<object, object>> ___ItemTemplates___ { get; set; }
    public object ___TemplateForNull___ { get; set; }
}