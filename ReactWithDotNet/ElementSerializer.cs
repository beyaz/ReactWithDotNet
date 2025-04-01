using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ReactWithDotNet;

static partial class ElementSerializer
{
    const string ___ComponentDidMountMethod___ = "$ComponentDidMountMethod";
    const string ___ComponentWillUnmountMethod___ = "$ComponentWillUnmountMethod";
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

        if (style._hover?.headNode is not null)
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

        if (style._before?.headNode is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "before",
                BodyOfCss = style._before.ToCssWithImportant()
            });
        }

        if (style._after?.headNode is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "after",
                BodyOfCss = style._after.ToCssWithImportant()
            });
        }

        if (style._active?.headNode is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "active",
                BodyOfCss = style._active.ToCssWithImportant()
            });
        }

        if (style._focus?.headNode is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "focus",
                BodyOfCss = style._focus.ToCssWithImportant()
            });
        }

        if (style._focusVisible?.headNode is not null)
        {
            pseudos ??= [];

            pseudos.Add(new()
            {
                Name      = "focus-visible",
                BodyOfCss = style._focusVisible.ToCssWithImportant()
            });
        }

        return pseudos;
    }

    static void InitializeKeyIfNotExists(Node node)
    {
        node.IsChildrenKeysInitialized = true;
        
        var element = node.Element;
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
            var child = children[index];
            if (child is not null)
            {
                child.key = index.ToString();
            }
        }
    }

    internal static int? TryFindHandlerComponentUniqueIdentifier(ElementSerializerContext context, object handlerDelegateTarget)
    {
        if (handlerDelegateTarget is ReactComponentBase target)
        {
            return target.ComponentUniqueIdentifier;
        }

        if (context.FunctionalComponentStack.Count > 0)
        {
            foreach (var item in context.FunctionalComponentStack)
            {
                if (item.state.CompilerGeneratedType == handlerDelegateTarget.GetType())
                {
                    return item.ComponentUniqueIdentifier;
                }

                var scope = handlerDelegateTarget.GetType().GetFields().FirstOrDefault(f => f.FieldType.IsFunctionalComponent())?.GetValue(handlerDelegateTarget);
                if (scope is not null && scope == item)
                {
                    return item.ComponentUniqueIdentifier;
                }
            }
        }

        return null;
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
            if (style.headNode is null && pseudos is null && style._mediaQueries is null)
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

    static (bool success, int value, bool handlerIsReactComponent) GetHandlerComponentUniqueIdentifierFromBindingExpression(ElementSerializerContext context, LambdaExpression lambdaExpression)
    {
        var (success, targetValue) = GetTargetValueFromExpression(lambdaExpression);
        if (success)
        {
            var handlerComponentUniqueIdentifier = TryFindHandlerComponentUniqueIdentifier(context, targetValue);

            if (handlerComponentUniqueIdentifier.HasValue)
            {
                return (true, handlerComponentUniqueIdentifier.Value, targetValue is ReactComponentBase);
            }
        }

        return default;
    }

    static string GetPropertyName(PropertyInfoCalculated propertyInfoCalculated)
    {
        var jsonPropertyName = propertyInfoCalculated.JsonPropertyName;
        if (jsonPropertyName != null)
        {
            return jsonPropertyName.Name;
        }

        return propertyInfoCalculated.PropertyInfo.Name;
    }

    static async Task<ValueExportInfo<object>> GetPropertyValue(ElementSerializerContext context, Node node, TypeInfoCalculated typeInfoCalculated, object instance, PropertyInfoCalculated property)
    {
        var propertyInfo = property.PropertyInfo;

        var propertyValue = property.GetValueFunc(instance);

        var isDefaultValue = propertyValue == property.DefaultValue;
        if (isDefaultValue)
        {
            return NotExportableObject;
        }

        if (property.IsUnionProperty)
        {
            propertyValue = ((UnionPropBase)propertyValue).value;
        }

        if (typeInfoCalculated.GetPropertyValueForSerializeToClient is not null)
        {
            var output = typeInfoCalculated.GetPropertyValueForSerializeToClient(instance, propertyInfo.Name);
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
                var (needToExport, newValue) = property.TransformValueInServerSide(propertyValue, new(convertStyleToCssClass));
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

            var handlerComponentUniqueIdentifier = TryFindHandlerComponentUniqueIdentifier(context, handlerDelegateTarget);

            var handlerMethod = handlerDelegate.Method.GetCalculated();

            return new RemoteMethodInfo
            {
                IsRemoteMethod                   = true,
                remoteMethodName                 = handlerDelegate.Method.GetAccessKey(),
                HandlerComponentUniqueIdentifier = handlerComponentUniqueIdentifier,
                FunctionNameOfGrabEventArguments = property.FunctionNameOfGrabEventArguments,
                StopPropagation                  = handlerMethod.HasStopPropagation,
                PreventDefault                = handlerMethod.HasPreventDefault,
                
                KeyboardEventCallOnly            = handlerMethod.KeyboardEventCallOnly,
                DebounceTimeout                  = handlerMethod.DebounceTimeout,
                Cacheable = handlerMethod.Cacheable ? 1: null
            };
        }

        if (property.IsBindingExpression)
        {
            var propertyValueAsLambdaExpression = (LambdaExpression)propertyValue;

            var reactBindAttribute = property.ReactBindAttribute;
            if (reactBindAttribute == null)
            {
                return NotExportableObject;
            }

            var (path, isConnectedToState) = propertyValueAsLambdaExpression.AsBindingPath();
            var bindInfo = new BindInfo
            {
                targetProp        = reactBindAttribute.targetProp,
                eventName         = reactBindAttribute.eventName,
                sourcePath        = path,
                sourceIsState     = isConnectedToState,
                IsBinding         = true,
                jsValueAccess     = reactBindAttribute.jsValueAccess.Split('.', StringSplitOptions.RemoveEmptyEntries),
                transformFunction = property.NameOfTransformValueInClient
            };

            var (success, handlerComponentUniqueIdentifier, handlerIsReactComponent) = GetHandlerComponentUniqueIdentifierFromBindingExpression(context, propertyValueAsLambdaExpression);
            if (!success)
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyInfo, propertyValueAsLambdaExpression.ToString());
            }

            if (handlerIsReactComponent is false)
            {
                bindInfo.sourceIsState = true;
                bindInfo.sourcePath    = new[] { nameof(FunctionalComponent.State.Scope) }.Concat(bindInfo.sourcePath).ToList();
            }

            bindInfo.HandlerComponentUniqueIdentifier = handlerComponentUniqueIdentifier;

            var debounceTimeout = property.DebounceTimeoutGetFunc(instance) as int?;
            if (debounceTimeout > 0)
            {
                if (property.DebounceHandlerGetFunc(instance) is Func<Task> debounceHandler)
                {
                    bindInfo.DebounceTimeout = debounceTimeout;
                    bindInfo.DebounceHandler = debounceHandler.Method.GetAccessKey();
                }
            }

            return bindInfo;
        }

        if (propertyValue is HtmlTextNode htmlTextNode)
        {
            return htmlTextNode.innerText;
        }

        if (property.IsEnum)
        {
            propertyValue = propertyValue.ToString();
        }

        if (propertyValue is Element element)
        {
            element.key ??= propertyInfo.Name;

            return new InnerElementInfo
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
                method = instance.GetType().GetProperty(templateAttribute.MethodNameForGettingItemsSource, BindingFlags.Instance | BindingFlags.Public)?.GetMethod;
                if (method == null)
                {
                    throw new MissingMethodException(templateAttribute.MethodNameForGettingItemsSource);
                }
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

            var itemTemplates = (IEnumerable)method.Invoke(instance, []);

            var results = new List<ItemTemplateInfo>();

            if (itemTemplates is not null)
            {
                foreach (var item in itemTemplates)
                {
                    results.Add(new() { Item = item, ElementAsJson = await convertToReactNode(item) });
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

            var newTargetTypeInfo = target.GetType().Calculated();
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

            var handlerComponentUniqueIdentifier = TryFindHandlerComponentUniqueIdentifier(context, handlerDelegateTarget);
            if (handlerComponentUniqueIdentifier is null)
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyDefinition.name, handlerDelegateTarget);
            }

            var handlerMethod = handlerDelegate.Method.GetCalculated();

            return new RemoteMethodInfo
            {
                IsRemoteMethod                   = true,
                remoteMethodName                 = handlerMethod.NameWithToken,
                HandlerComponentUniqueIdentifier = handlerComponentUniqueIdentifier,
                FunctionNameOfGrabEventArguments = propertyDefinition.GrabEventArgumentsByUsingFunction,
                StopPropagation                  = handlerMethod.HasStopPropagation,
                PreventDefault                  = handlerMethod.HasPreventDefault,
                KeyboardEventCallOnly            = handlerMethod.KeyboardEventCallOnly,
                DebounceTimeout                  = handlerMethod.DebounceTimeout,
                Cacheable                        = handlerMethod.Cacheable ? 1 : null
            };
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

            var (success, handlerComponentUniqueIdentifier, handlerIsReactComponent) = GetHandlerComponentUniqueIdentifierFromBindingExpression(context, propertyValueAsLambdaExpression);
            if (!success)
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyDefinition.name, propertyValueAsLambdaExpression.ToString());
            }

            if (handlerIsReactComponent is false)
            {
                bindInfo.sourceIsState = true;
                bindInfo.sourcePath    = new[] { nameof(FunctionalComponent.State.Scope) }.Concat(bindInfo.sourcePath).ToList();
            }

            bindInfo.HandlerComponentUniqueIdentifier = handlerComponentUniqueIdentifier;

            // initialize binding debounce information
            {
                var debounceMethods = DebounceHelper.GetDebounceMethods(instance.GetType(), propertyDefinition.name);

                var debounceTimeout = debounceMethods.DebounceTimeoutGetFunc(instance) as int?;
                if (debounceTimeout > 0)
                {
                    if (debounceMethods.DebounceHandlerGetFunc(instance) is Func<Task> debounceHandler)
                    {
                        bindInfo.DebounceTimeout = debounceTimeout;
                        bindInfo.DebounceHandler = debounceHandler.Method.GetAccessKey();
                    }
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

    static ValueExportInfo<object> GetStylePropertyValueOfHtmlElementForSerialize(ElementSerializerContext context, Node node, object instance, Style style)
    {
        var response = ConvertStyleToCssClass(context, node, style, false, context.DynamicStyles.GetClassName);
        if (response.needToExport is false)
        {
            if (style.headNode is not null)
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

        if (style.headNode is null)
        {
            return NotExportableObject;
        }

        return style;
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

    static Task TryCallBeforeSerializeElementToClient(this ElementSerializerContext context, Element element, Element parent)
    {
        if (element is null || context.BeforeSerializeElementToClient is null)
        {
            return Task.CompletedTask;
        }

        return context.BeforeSerializeElementToClient(context.ReactContext, element, parent);
    }

    static int UpclimbForComponentUniqueIdentifier(ElementSerializerContext context, Node node)
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
            Value        = null;
            NeedToExport = false;
        }

        public static implicit operator ValueExportInfo<TValue>(TValue value)
        {
            return new(value);
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