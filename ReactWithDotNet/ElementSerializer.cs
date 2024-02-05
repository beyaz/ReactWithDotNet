using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ReactWithDotNet;

sealed class Tracer
{
    internal readonly LinkedList<string> Messages = new();

    readonly Stopwatch stopwatch = new();

    public Tracer()
    {
        stopwatch.Start();
    }

    public long ElapsedMilliseconds => stopwatch.ElapsedMilliseconds;

    public void Trace(string message)
    {
        Messages.AddLast(message);
    }
}

sealed class ElementSerializerContext
{
    internal readonly DynamicStyleContentForEmbedInClient DynamicStyles = new();

    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public bool CalculateSuspenseFallbackForThirdPartyReactComponents { get; set; }

    public int ComponentUniqueIdentifierNextValue { get; set; }

    public ReactContext ReactContext { get; init; }

    public bool SkipHandleCacheableMethods { get; set; }

    public StateTree StateTree { get; init; }
    public Tracer Tracer { get; init; }
    public bool IsCapturingPreview { get; set; }
    public Stack<FunctionalComponent> FunctionalComponentStack { get; set; }
}

static partial class ElementSerializer
{
    const string ___ComponentDidMountMethod___ = "$ComponentDidMountMethod";
    const string ___RootNode___ = "$RootNode";
    const string ___Type___ = "$Type";
    

    static readonly ValueExportInfo<object> NotExportableObject = ValueExportInfo<object>.NotExportable;

    public static IReadOnlyList<CssPseudoCodeInfo> CalculatePseudos(Style style)
    {
        if (style is null)
        {
            return null;
        }

        List<CssPseudoCodeInfo> pseudos = null;

        if (style._hover is not null)
        {
            pseudos =
            [
                new()
                {
                    Name      = "hover",
                    BodyOfCss = style._hover.ToCssWithImportant()
                }
            ];
        }

        if (style._before is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "before",
                BodyOfCss = style._before.ToCssWithImportant()
            });
        }

        if (style._after is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "after",
                BodyOfCss = style._after.ToCssWithImportant()
            });
        }

        if (style._active is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "active",
                BodyOfCss = style._active.ToCssWithImportant()
            });
        }

        if (style._focus is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "focus",
                BodyOfCss = style._focus.ToCssWithImportant()
            });
        }

        return pseudos;
    }

    internal static void InitializeKeyIfNotExists(Element element)
    {
        if (element.key == null)
        {
            throw new DeveloperException("key of react component cannot be null");
        }

        var children = element._children;
        if (children == null)
        {
            return;
        }

        var childrenCount = children.Count;

        for (var index = 0; index < childrenCount; index++)
        {
            var sibling = children[index];
            if (sibling is not null)
            {
                sibling.key = index.ToString();
            }
        }
    }

    static int UpclimbForComponentUniqueIdentifier(ElementSerializerContext context,Node node)
    {
        while (node is not null)
        {
            if (node.ElementIsDotNetReactPureComponent)
            {
                return CheckComponentUniqueIdentifierHasValue(node.ElementAsDotNetReactPureComponent.ComponentUniqueIdentifier);
            }
            if (node.ElementIsDotNetReactComponent)
            {
                return CheckComponentUniqueIdentifierHasValue(node.ElementAsDotNetReactComponent.ComponentUniqueIdentifier);
            }
            
            node = node.Parent;
        }

        return context.ComponentUniqueIdentifierNextValue++;

        static int CheckComponentUniqueIdentifierHasValue(int componentUniqueIdentifier)
        {
            if (componentUniqueIdentifier == 0)
            {
                throw FatalError("componentUniqueIdentifier should be initialize before usage");
            }
                
            return componentUniqueIdentifier;
        }
    }
    
    static (bool needToExport, string cssClassName) ConvertStyleToCssClass(ElementSerializerContext context, Node node, Style style, bool fullExport, Func<CssClassInfo, string> getCssClassName)
    {
        if (style is null)
        {
            return (false, null);
        }

        var pseudos = CalculatePseudos(style);

        if (fullExport)
        {
            if (style.IsEmpty && pseudos is null && style._mediaQueries is null)
            {
                return (false, null);
            }
        }
        else
        {
            if (pseudos is null && style._mediaQueries is null)
            {
                return (false, null);
            }
        }

        var componentUniqueIdentifier = UpclimbForComponentUniqueIdentifier(context, node);
        
        var cssClassInfo = new CssClassInfo
        {
            ComponentUniqueIdentifier = componentUniqueIdentifier,
            Name                      = "_rwd_" + componentUniqueIdentifier + "_",
            Pseudos                   = pseudos,
            MediaQueries              = calculateMediaQueries(style._mediaQueries),
            Body                      = fullExport ? style.ToCssWithImportant() : null
        };

        return (true, getCssClassName(cssClassInfo));

        static IReadOnlyList<(string mediaRule, string cssBody)> calculateMediaQueries(IReadOnlyList<MediaQuery> mediaQueries)
        {
            if (mediaQueries == null || mediaQueries.Count == 0)
            {
                return null;
            }

            var uniqueMediaQueries = new List<MediaQuery>();

            foreach (var mediaQuery in mediaQueries)
            {
                var existingValue = uniqueMediaQueries.FirstOrDefault(x => hasValueAndEqual(x.Query, mediaQuery.Query));
                if (existingValue is null)
                {
                    uniqueMediaQueries.Add(mediaQuery);
                    continue;
                }

                existingValue.Style.Import(mediaQuery.Style);
            }

            return uniqueMediaQueries.ConvertAll(x => (x.Query, x.Style.ToCssWithImportant()));

            static bool hasValueAndEqual(string a, string b)
            {
                if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b))
                {
                    return false;
                }

                a = Regex.Replace(a, @"\s", "");
                b = Regex.Replace(b, @"\s", "");

                return a.Equals(b, StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    

    static string GetPropertyName(PropertyAccessInfo propertyAccessInfo)
    {
        var jsonPropertyName = propertyAccessInfo.JsonPropertyName;
        if (jsonPropertyName != null)
        {
            return jsonPropertyName.Name;
        }

        return propertyAccessInfo.PropertyInfo.Name;
    }

    static async Task<ValueExportInfo<object>> GetPropertyValue(ElementSerializerContext context, Node node, TypeInfo typeInfo, object instance, PropertyAccessInfo property)
    {
        var propertyInfo = property.PropertyInfo;

        var propertyValue = property.GetValueFunc(instance);

        var isDefaultValue = propertyValue == property.DefaultValue;
        if (isDefaultValue)
        {
            return NotExportableObject;
        }

        if (typeInfo.GetPropertyValueForSerializeToClient is not null)
        {
            var output = typeInfo.GetPropertyValueForSerializeToClient(instance, propertyInfo.Name);
            if (output.needToExport)
            {
                return output.value;
            }
        }

        if (property.TransformValueInServerSide != null)
        {
            string convertStyleToCssClass(Style style)
            {
                var (needToExport, cssClassName) = ConvertStyleToCssClass(context, node, style, true, context.DynamicStyles.GetClassName);
                if (needToExport)
                {
                    return cssClassName;
                }

                return string.Empty;
            }

            {
                var (needToExport, newValue) = property.TransformValueInServerSide(propertyValue, new TransformValueInServerSideContext(convertStyleToCssClass));
                if (needToExport == false)
                {
                    return NotExportableObject;
                }

                return newValue;
            }
        }

        // check inline
        {
            if (propertyValue is Style style)
            {
                return GetStylePropertyValueOfHtmlElementForSerialize(context, node, instance, style);
            }
        }

        if (property.PropertyTypeIsIsVoidTaskDelegate)
        {
            var handlerDelegate = (Delegate)propertyValue;
            
            var handlerDelegateTarget = handlerDelegate.Target;

            if (handlerDelegateTarget is null)
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyInfo, null);
            }
            
            int? handlerComponentUniqueIdentifier;
            
            if (handlerDelegateTarget is ReactComponentBase target)
            {
                handlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier;
            }
            else
            {
                if (context.FunctionalComponentStack?.Count > 0)
                {
                    var handlerComponent = context.FunctionalComponentStack.Peek();
                    if (handlerComponent.state.CompilerGeneratedType == handlerDelegateTarget.GetType())
                    {
                        handlerComponentUniqueIdentifier = handlerComponent.ComponentUniqueIdentifier; 
                    }
                    else
                    {
                        throw HandlerMethodShouldBelongToReactComponent(propertyInfo, handlerDelegateTarget);
                    }
                }
                else
                {
                    throw HandlerMethodShouldBelongToReactComponent(propertyInfo, handlerDelegateTarget);
                }
            }
                
            int? htmlElementScrollDebounceTimeout = null;
            if (propertyInfo.Name == nameof(HtmlElement.onScroll) && propertyInfo.DeclaringType == typeof(HtmlElement))
            {
                htmlElementScrollDebounceTimeout = ((HtmlElement)instance).onScrollDebounceTimeout;
            }
                    
            return new RemoteMethodInfo
            {
                IsRemoteMethod                   = true,
                remoteMethodName                 = handlerDelegate.Method.GetNameWithToken(),
                HandlerComponentUniqueIdentifier = handlerComponentUniqueIdentifier,
                FunctionNameOfGrabEventArguments = propertyInfo.GetCustomAttribute<ReactGrabEventArgumentsByUsingFunctionAttribute>()?.TransformFunction,
                StopPropagation                  = handlerDelegate.Method.GetCustomAttribute<ReactStopPropagationAttribute>() is not null,
                HtmlElementScrollDebounceTimeout = htmlElementScrollDebounceTimeout,
                KeyboardEventCallOnly            = handlerDelegate.Method.GetCustomAttribute<ReactKeyboardEventCallOnlyAttribute>()?.Keys
            };
        }

        if (propertyValue is Enum enumValue)
        {
            propertyValue = enumValue.ToString();
        }

        if (propertyValue is Expression<Func<int>> ||
            propertyValue is Expression<Func<double>> ||
            propertyValue is Expression<Func<string>> ||
            propertyValue is Expression<Func<bool>>||
            propertyValue is Expression<Func<InputValueBinder>>)
        {
            var propertyValueAsLambdaExpression = (LambdaExpression)propertyValue;
            
            var reactBindAttribute = propertyInfo.GetCustomAttribute<ReactBindAttribute>();
            if (reactBindAttribute == null)
            {
                return NotExportableObject;
            }

            var transformValueInClientAttribute = propertyInfo.GetCustomAttribute<ReactTransformValueInClientAttribute>();

            var (path, isConnectedToState) = propertyValueAsLambdaExpression.AsBindingPath();
            var bindInfo = new BindInfo
            {
                targetProp        = reactBindAttribute.targetProp,
                eventName         = reactBindAttribute.eventName,
                sourcePath        = path,
                sourceIsState     = isConnectedToState,
                IsBinding         = true,
                jsValueAccess     = reactBindAttribute.jsValueAccess.Split('.', StringSplitOptions.RemoveEmptyEntries),
                transformFunction = transformValueInClientAttribute?.TransformFunction
            };

            var (success, handlerComponentUniqueIdentifier, handlerIsReactComponent) = GetHandlerComponentUniqueIdentifierFromBindingExpression(context,propertyValueAsLambdaExpression);
            if (!success)
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyInfo, propertyValueAsLambdaExpression.ToString());
            }

            if (handlerIsReactComponent is false)
            {
                bindInfo.sourceIsState = true;
                bindInfo.sourcePath    = new[]{nameof(FunctionalComponent.State.Scope)}.Concat(bindInfo.sourcePath).ToList();
            }

            bindInfo.HandlerComponentUniqueIdentifier = handlerComponentUniqueIdentifier;

            var debounceTimeout = instance.GetType().GetProperty(propertyInfo.Name + "DebounceTimeout")?.GetValue(instance) as int?;
            if (debounceTimeout > 0)
            {
                if (instance.GetType().GetProperty(propertyInfo.Name + "DebounceHandler")?.GetValue(instance) is Func<Task> debounceHandler)
                {
                    bindInfo.DebounceTimeout = debounceTimeout;
                    bindInfo.DebounceHandler = debounceHandler.Method.GetNameWithToken();
                }
            }

            return bindInfo;
        }

        if (propertyValue is HtmlTextNode htmlTextNode)
        {
            return htmlTextNode.innerText;
        }

        if (propertyValue is Element element)
        {
            element.key ??= propertyInfo.Name;

            propertyValue = new InnerElementInfo
            {
                IsElement = true,
                Element   = await element.ToJsonMap(context)
            };
        }

        var templateAttribute = property.TemplateAttribute;
        if (templateAttribute is not null)
        {
            var func = (Delegate)propertyInfo.GetValue(instance);
            if (func == null)
            {
                return NotExportableObject;
            }

            var method = instance.GetType().GetMethod(templateAttribute.MethodNameForGettingItemsSource, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method == null)
            {
                throw new MissingMethodException(templateAttribute.MethodNameForGettingItemsSource);
            }

            Task<IReadOnlyJsonMap> convertToReactNode(object item)
            {
                var reactNode = (Element)func.DynamicInvoke(item);
                if (reactNode is not null && item is not null)
                {
                    reactNode.key ??= item.GetType().GetProperty("key")?.GetValue(item)?.ToString() ?? "0";
                }

                return reactNode.ToJsonMap(context);
            }

            var itemTemplates = (IEnumerable)method.Invoke(instance, new object[] { });

            var results = new List<ItemTemplateInfo>();

            if (itemTemplates is not null)
            {
                foreach (var item in itemTemplates)
                {
                    results.Add(new ItemTemplateInfo { Item = item, ElementAsJson = await convertToReactNode(item) });
                }
            }

            var template = new ItemTemplate
            {
                ___ItemTemplates___ = results
            };

            if (propertyInfo.GetCustomAttribute<ReactTemplateForNullAttribute>() is not null)
            {
                template.___TemplateForNull___ = await convertToReactNode(null);
            }

            return template;
        }

        if (property.TransformValueInClientFunction is not null)
        {
            var jsonMap = new JsonMap();

            jsonMap.Add("$transformValueFunction", property.TransformValueInClientFunction);
            jsonMap.Add("RawValue", propertyValue);

            return jsonMap;
        }

        return propertyValue;
    }
    static int? TryFindHandlerComponentUniqueIdentifier(ElementSerializerContext context, object handlerDelegateTarget)
    {
        if (handlerDelegateTarget is ReactComponentBase target)
        {
            return target.ComponentUniqueIdentifier;
        }

        if (context.FunctionalComponentStack?.Count > 0)
        {
            foreach (var item in context.FunctionalComponentStack)
            {
                if (item.state.CompilerGeneratedType == handlerDelegateTarget.GetType())
                {
                    return item.ComponentUniqueIdentifier;
                }

                var scope = handlerDelegateTarget.GetType().GetFields().FirstOrDefault(f => f.FieldType == typeof(Scope))?.GetValue(handlerDelegateTarget);
                if (scope is not null && scope == item)
                {
                    return item.ComponentUniqueIdentifier;
                }
            }
        }

        return null;
    }
    static async Task<object> GetPropertyValueOfHtmlElement(ElementSerializerContext context, HtmlElement instance, HtmlElement.PropertyValueNode propertyValueNode)
    {
        var propertyDefinition = propertyValueNode.propertyDefinition;
        
        var propertyValue = propertyValueNode._value;

        if (propertyDefinition.isOnClickPreview)
        {
            if (context.IsCapturingPreview)
            {
                return NotExportableObject;
            }

            var action = (Action)propertyValue;

            if (!(action.Target is ReactComponentBase target))
            {
                throw HandlerMethodShouldBelongToReactComponent("onClickPreview", action.Target);
            }
                    
            var newTarget = (ReactComponentBase)target.Clone();

            var newTargetTypeInfo = GetTypeInfo(target.GetType());
            if (newTargetTypeInfo.StateProperty is not null)
            {
                var targetState = newTargetTypeInfo.StateProperty.GetValueFunc(target);
                if (targetState is EmptyState == false)
                {
                    newTargetTypeInfo.StateProperty.SetValueFunc(newTarget, ReflectionHelper.DeepCopy(targetState));
                }
            }
                    
            action.Method.Invoke(newTarget, null);
                        
            await newTarget.InvokeRender();

            context.IsCapturingPreview = true;
            var newMap = await ToJsonMap(newTarget, context);
            context.IsCapturingPreview = false;

            return (JsonMap)newMap;
        }
        
        if (propertyDefinition.isIsVoidTaskDelegate)
        {
            var handlerDelegate = (Delegate)propertyValue;

            var handlerDelegateTarget = handlerDelegate.Target;

            if (handlerDelegateTarget is null)
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyDefinition.name, null);
            }
            
            var handlerComponentUniqueIdentifier =  TryFindHandlerComponentUniqueIdentifier(context, handlerDelegateTarget);
            if (handlerComponentUniqueIdentifier is null)
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyDefinition.name, handlerDelegateTarget);
            }

            var remoteMethodInfo = new RemoteMethodInfo
            {
                IsRemoteMethod                   = true,
                remoteMethodName                 = handlerDelegate.Method.GetNameWithToken(),
                HandlerComponentUniqueIdentifier = handlerComponentUniqueIdentifier,
                FunctionNameOfGrabEventArguments = propertyDefinition.GrabEventArgumentsByUsingFunction,
                StopPropagation                  = handlerDelegate.Method.GetCustomAttribute<ReactStopPropagationAttribute>() is not null,
                KeyboardEventCallOnly            = handlerDelegate.Method.GetCustomAttribute<ReactKeyboardEventCallOnlyAttribute>()?.Keys
            };
            if (propertyDefinition.isScrollEvent)
            {
                remoteMethodInfo.HtmlElementScrollDebounceTimeout = instance.onScrollDebounceTimeout;
            }
                
            return remoteMethodInfo;
        }
        
        if (propertyDefinition.isBindingExpression)
        {
            var propertyValueAsLambdaExpression = (LambdaExpression)propertyValue;
            
            var reactBindAttribute = propertyDefinition.bind;

            var (path, isConnectedToState) = propertyValueAsLambdaExpression.AsBindingPath();
            var bindInfo = new BindInfo
            {
                targetProp        = reactBindAttribute.targetProp,
                eventName         = reactBindAttribute.eventName,
                sourcePath        = path,
                sourceIsState     = isConnectedToState,
                IsBinding         = true,
                jsValueAccess     = reactBindAttribute.jsValueAccess.Split('.', StringSplitOptions.RemoveEmptyEntries),
                transformFunction = propertyDefinition.transformValueInClient
            };

            var (success, handlerComponentUniqueIdentifier, handlerIsReactComponent) = GetHandlerComponentUniqueIdentifierFromBindingExpression(context,propertyValueAsLambdaExpression);
            if (!success)
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyDefinition.name, propertyValueAsLambdaExpression.ToString());
            }
            
            if (handlerIsReactComponent is false)
            {
                bindInfo.sourceIsState = true;
                bindInfo.sourcePath    = new[]{nameof(FunctionalComponent.State.Scope)}.Concat(bindInfo.sourcePath).ToList();
            }

            bindInfo.HandlerComponentUniqueIdentifier = handlerComponentUniqueIdentifier;

            var debounceTimeout = instance.GetType().GetProperty(propertyDefinition.name+ "DebounceTimeout")?.GetValue(instance) as int?;
            if (debounceTimeout > 0)
            {
                if (instance.GetType().GetProperty(propertyDefinition.name+ "DebounceHandler")?.GetValue(instance) is Func<Task> debounceHandler)
                {
                    bindInfo.DebounceTimeout = debounceTimeout;
                    bindInfo.DebounceHandler = debounceHandler.Method.GetNameWithToken();
                }
            }

            return bindInfo;
        }
        
        if (propertyDefinition.transformValueInClient is not null)
        {
            var jsonMap = new JsonMap();

            jsonMap.Add("$transformValueFunction", propertyDefinition.transformValueInClient);
            jsonMap.Add("RawValue", propertyValue);

            return jsonMap;
        }

        return propertyValue;
    }

    static (bool success, int value, bool handlerIsReactComponent) GetHandlerComponentUniqueIdentifierFromBindingExpression(ElementSerializerContext context, LambdaExpression lambdaExpression)
    {
        var (success, targetValue) = GetTargetValueFromExpression(lambdaExpression);
        if (success)
        {
            var handlerComponentUniqueIdentifier = TryFindHandlerComponentUniqueIdentifier(context, targetValue);

            if (handlerComponentUniqueIdentifier.HasValue)
            {
                return  (true, handlerComponentUniqueIdentifier.Value, targetValue is ReactComponentBase);
            }
        }

        return default;
    }
    
    static (bool success, object value) GetTargetValueFromExpression(LambdaExpression lambdaExpression)
    {
        var expression = lambdaExpression.Body;
        while (true)
        {
            if (expression is UnaryExpression unaryExpression)
            {
                expression = unaryExpression.Operand;

                continue;
            }
                    
            if (expression is MemberExpression memberExpression)
            {
                expression = memberExpression.Expression;
                continue;
            }

            if (expression is ConstantExpression constantExpression)
            {
                return (true, constantExpression.Value);
            }

            if (expression is MethodCallExpression methodCallExpression)
            {
                expression = methodCallExpression.Object;
                continue;
            }

            return default;
        }
    }
    
    
    static string GetReactComponentTypeInfo(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetFullName();
    }

    static ValueExportInfo<object> GetStylePropertyValueOfHtmlElementForSerialize(ElementSerializerContext context, Node node, object instance, Style style)
    {
        var response = ConvertStyleToCssClass(context, node, style, false, context.DynamicStyles.GetClassName);
        if (response.needToExport is false)
        {
            if (style.IsEmpty == false)
            {
                return style;
            }

            return NotExportableObject;
        }

        var pseudos = CalculatePseudos(style);

        if (pseudos is not null || style._mediaQueries is not null)
        {
            var cssClassName = response.cssClassName;

            if (instance is HtmlElement htmlElement)
            {
                htmlElement.AddClass(cssClassName);
            }
            else if (instance is ThirdPartyReactComponent thirdPartyReactComponent)
            {
                thirdPartyReactComponent.AddClass(cssClassName);
            }
            else
            {
                throw new NotImplementedException("Style attribute problem occurred.");
            }
        }

        if (style.IsEmpty)
        {
            return NotExportableObject;
        }

        return style;
    }

    static Exception HandlerMethodShouldBelongToReactComponent(PropertyInfo propertyInfo, object handlerTarget)
    {
        throw DeveloperException(string.Join(Environment.NewLine,
                                             "Delegate method should belong to ReactComponent. ",
                                             "Please give named method to " + propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name,
                                             $"How to fix: inherit {handlerTarget?.GetType().FullName} class from ReactComponent."));
    }
    
    static Exception HandlerMethodShouldBelongToReactComponent(string fullNameOfProperty, object handlerTarget)
    {
        throw DeveloperException(string.Join(Environment.NewLine,
                                             "Delegate method should belong to ReactComponent. ",
                                             "Please give named method to " + fullNameOfProperty,
                                             $"How to fix: inherit {handlerTarget?.GetType().FullName} class from ReactComponent."));
    }

    static Exception HandlerMethodShouldBelongToReactComponent(PropertyInfo propertyInfo, string bindingPath)
    {
        throw new InvalidOperationException("Delegate method should belong to ReactComponent. Please give named method to " + propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name + $" Given bindingPath:{bindingPath} is invalid.");
    }
    
    static void TryCallBeforeSerializeElementToClient(this ElementSerializerContext context, Element element, Element parent)
    {
        if (element is null || context.BeforeSerializeElementToClient is null)
        {
            return;
        }

        context.BeforeSerializeElementToClient(context.ReactContext, element, parent);
    }

    class CacheableMethodInfo
    {
        public IReadOnlyJsonMap ElementAsJson { get; set; }
        public bool IgnoreParameters { get; set; }
        public string MethodName { get; set; }
        public object Parameter { get; set; }
    }

    sealed class ValueExportInfo<TValue> where TValue : class
    {
        public static readonly ValueExportInfo<TValue> NotExportable = new();
        public readonly bool NeedToExport;
        public readonly TValue Value;

        ValueExportInfo(TValue value)
        {
            Value        = value;
            NeedToExport = true;
        }

        ValueExportInfo()
        {
            Value        = default;
            NeedToExport = false;
        }

        public static implicit operator ValueExportInfo<TValue>(TValue value)
        {
            return new ValueExportInfo<TValue>(value);
        }
    }
}

class ItemTemplate
{
    public IEnumerable<ItemTemplateInfo> ___ItemTemplates___ { get; set; }
    public IReadOnlyJsonMap ___TemplateForNull___ { get; set; }
}

sealed class ItemTemplateInfo
{
    public IReadOnlyJsonMap ElementAsJson { get; set; }
    public object Item { get; set; }
}