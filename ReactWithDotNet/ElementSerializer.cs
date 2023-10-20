using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
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
    internal readonly Stack<ReactComponentBase> ComponentStack = new();

    internal readonly DynamicStyleContentForEmbedInClient DynamicStyles = new();

    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public bool CalculateSuspenseFallbackForThirdPartyReactComponents { get; set; }

    public int ComponentUniqueIdentifierNextValue { get; set; }

    public ReactContext ReactContext { get; init; }

    public bool SkipHandleCacheableMethods { get; set; }

    public StateTree StateTree { get; init; }
    public Tracer Tracer { get; init; }
    public bool IsCapturingPreview { get; set; }
}

static partial class ElementSerializer
{
    const string ___HasComponentDidMountMethod___ = "$HasComponentDidMountMethod";
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
            // ReSharper disable once UseObjectOrCollectionInitializer
            pseudos = new List<CssPseudoCodeInfo>();

            pseudos.Add(new CssPseudoCodeInfo
            {
                Name      = "hover",
                BodyOfCss = style._hover.ToCssWithImportant()
            });
        }

        if (style._before is not null)
        {
            pseudos ??= new List<CssPseudoCodeInfo>();

            pseudos.Add(new CssPseudoCodeInfo
            {
                Name      = "before",
                BodyOfCss = style._before.ToCssWithImportant()
            });
        }

        if (style._after is not null)
        {
            pseudos ??= new List<CssPseudoCodeInfo>();

            pseudos.Add(new CssPseudoCodeInfo
            {
                Name      = "after",
                BodyOfCss = style._after.ToCssWithImportant()
            });
        }

        if (style._active is not null)
        {
            pseudos ??= new List<CssPseudoCodeInfo>();

            pseudos.Add(new CssPseudoCodeInfo
            {
                Name      = "active",
                BodyOfCss = style._active.ToCssWithImportant()
            });
        }

        if (style._focus is not null)
        {
            pseudos ??= new List<CssPseudoCodeInfo>();

            pseudos.Add(new CssPseudoCodeInfo
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

    static (bool needToExport, string cssClassName) ConvertStyleToCssClass(Style style,
        bool fullExport,
        int? componentUniqueIdentifier,
        Func<CssClassInfo, string> getCssClassName)
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

        componentUniqueIdentifier ??= 1;

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

    static BindInfo GetExpressionAsBindingInfo(PropertyInfo propertyInfo, Func<(IReadOnlyList<string> path, bool isConnectedToState)> calculateSourcePathFunc)
    {
        var reactBindAttribute = propertyInfo.GetCustomAttribute<ReactBindAttribute>();
        if (reactBindAttribute == null)
        {
            return null;
        }

        var transformValueInClientAttribute = propertyInfo.GetCustomAttribute<ReactTransformValueInClientAttribute>();

        var (path, isConnectedToState) = calculateSourcePathFunc();
        return new BindInfo
        {
            targetProp        = reactBindAttribute.targetProp,
            eventName         = reactBindAttribute.eventName,
            sourcePath        = path,
            sourceIsState     = isConnectedToState,
            IsBinding         = true,
            jsValueAccess     = reactBindAttribute.jsValueAccess.Split('.', StringSplitOptions.RemoveEmptyEntries),
            transformFunction = transformValueInClientAttribute?.TransformFunction
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

    static async Task<ValueExportInfo<object>> GetPropertyValue(TypeInfo typeInfo, object instance, PropertyAccessInfo property, ElementSerializerContext context)
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
                var (needToExport, cssClassName) = ConvertStyleToCssClass(style, true, context.ComponentStack.PeekForComponentUniqueIdentifier(), context.DynamicStyles.GetClassName);
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
                return GetStylePropertyValueOfHtmlElementForSerialize(instance, style, context);
            }
        }

        if (propertyValue is Delegate @delegate)
        {
            if (propertyValue is Action)
            {
                if (@delegate.Target is ReactComponentBase target)
                {
                    // special case
                    if (propertyInfo.Name == "onClickPreview" && propertyInfo.DeclaringType== typeof(HtmlElement))
                    {
                        if (context.IsCapturingPreview)
                        {
                            return NotExportableObject;
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
                    
                        @delegate.Method.Invoke(newTarget, null);
                        newTarget.InvokeRender();

                        context.IsCapturingPreview = true;
                        var newMap = await ToJsonMap(newTarget, context);
                        context.IsCapturingPreview = false;

                        return (JsonMap)newMap;
                    }
                
                    propertyValue = new RemoteMethodInfo
                    {
                        IsRemoteMethod                   = true,
                        remoteMethodName                 = @delegate.Method.GetNameWithToken(),
                        HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier
                    };
                }
                else
                {
                    throw HandlerMethodShouldBelongToReactComponent(propertyInfo, @delegate.Target);
                }
            }
            
            
            if (propertyInfo.PropertyType.IsGenericAction1Or2Or3() || propertyInfo.PropertyType.IsVoidTaskFunc1Or2Or3())
            {
                if (@delegate.Target is ReactComponentBase target)
                {
                    propertyValue = new RemoteMethodInfo
                    {
                        IsRemoteMethod                   = true,
                        remoteMethodName                 = @delegate.Method.GetNameWithToken(),
                        HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier,
                        FunctionNameOfGrabEventArguments = propertyInfo.GetCustomAttribute<ReactGrabEventArgumentsByUsingFunctionAttribute>()?.TransformFunction,
                        StopPropagation                  = @delegate.Method.GetCustomAttribute<ReactStopPropagationAttribute>() is not null
                    };
                }
                else
                {
                    throw HandlerMethodShouldBelongToReactComponent(propertyInfo, @delegate.Target);
                }
            }
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
            static object getTargetValueFromExpression(PropertyInfo pi, LambdaExpression lambdaExpression)
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
                        return constantExpression.Value;
                    }

                    if (expression is MethodCallExpression methodCallExpression)
                    {
                        expression = methodCallExpression.Object;
                        continue;
                    }

                    throw HandlerMethodShouldBelongToReactComponent(pi, lambdaExpression.ToString());
                }
            }

            (IReadOnlyList<string> path, bool isConnectedToState) calculateSourcePathFunc()
            {
                if (propertyValue is Expression<Func<string>> bindingExpressionAsString)
                {
                    return bindingExpressionAsString.AsBindingPath();
                }

                if (propertyValue is Expression<Func<int>> bindingExpressionAsInt32)
                {
                    return bindingExpressionAsInt32.AsBindingPath();
                }

                if (propertyValue is Expression<Func<bool>> bindingExpressionAsBoolean)
                {
                    return bindingExpressionAsBoolean.AsBindingPath();
                }

                if (propertyValue is Expression<Func<double>> bindingExpressionAsDouble)
                {
                    return bindingExpressionAsDouble.AsBindingPath();
                }
                
                if (propertyValue is Expression<Func<InputValueBinder>> bindingExpressionAsInputValueBinder)
                {
                    return bindingExpressionAsInputValueBinder.AsBindingPath();
                }

                throw new NotImplementedException();
            }

            var bindInfo = GetExpressionAsBindingInfo(propertyInfo, calculateSourcePathFunc);
            if (bindInfo == null)
            {
                return NotExportableObject;
            }

            if (getTargetValueFromExpression(propertyInfo, propertyValue as LambdaExpression) is ReactComponentBase target)
            {
                bindInfo.HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier;
            }

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

        var templateAttribute = propertyInfo.GetCustomAttribute<ReactTemplateAttribute>();
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

        var reactTransformValueInClient = propertyInfo.GetCustomAttribute<ReactTransformValueInClientAttribute>();
        if (reactTransformValueInClient is not null)
        {
            var jsonMap = new JsonMap();

            jsonMap.Add("$transformValueFunction", reactTransformValueInClient.TransformFunction);
            jsonMap.Add("RawValue", propertyValue);

            return jsonMap;
        }

        return propertyValue;
    }

    static string GetReactComponentTypeInfo(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetFullName();
    }

    static ValueExportInfo<object> GetStylePropertyValueOfHtmlElementForSerialize(object instance, Style style, ElementSerializerContext context)
    {
        var response = ConvertStyleToCssClass(style, false, context.ComponentStack.PeekForComponentUniqueIdentifier(), context.DynamicStyles.GetClassName);
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

    static Exception HandlerMethodShouldBelongToReactComponent(PropertyInfo propertyInfo, string bindingPath)
    {
        throw new InvalidOperationException("Delegate method should belong to ReactComponent. Please give named method to " + propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name + $" Given bindingPath:{bindingPath} is invalid.");
    }

    static bool HasComponentDidMountMethod(object reactStatefulComponent)
    {
        var componentType = reactStatefulComponent.GetType();

        var didMountMethodInfo = componentType.FindMethod("componentDidMount", BindingFlags.NonPublic | BindingFlags.Instance);
        if (didMountMethodInfo != null)
        {
            if (didMountMethodInfo.DeclaringType != typeof(ReactComponentBase))
            {
                return true;
            }
        }

        return false;
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