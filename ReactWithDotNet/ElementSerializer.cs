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

    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }

    public int ComponentUniqueIdentifierNextValue { get; set; }

    public ReactContext ReactContext { get; init; }

    public bool SkipHandleCachableMethods { get; set; }

    public StateTree StateTree { get; init; }
}

static partial class ElementSerializer
{
    const string ___HasComponentDidMountMethod___ = "$HasComponentDidMountMethod";
    const string ___RootNode___ = "$RootNode";
    const string ___Type___ = "$Type";
    const string ___TypeOfState___ = "$TypeOfState";

    static BindInfo GetExpressionAsBindingInfo(PropertyInfo propertyInfo, Func<string[]> calculateSourcePathFunc)
    {
        var reactBindAttribute = propertyInfo.GetCustomAttribute<ReactBindAttribute>();
        if (reactBindAttribute == null)
        {
            return null;
        }

        var transformValueInClientAttribute = propertyInfo.GetCustomAttribute<ReactTransformValueInClientAttribute>();

        return new BindInfo
        {
            targetProp    = reactBindAttribute.targetProp,
            eventName     = reactBindAttribute.eventName,
            sourcePath    = calculateSourcePathFunc(),
            IsBinding     = true,
            jsValueAccess = reactBindAttribute.jsValueAccess.Split('.', StringSplitOptions.RemoveEmptyEntries),
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

    static (object value, bool noNeedToExport) getPropertyValue(object instance, PropertyAccessInfo property, ElementSerializerContext context)
    {
        var propertyInfo = property.PropertyInfo;
            
        var propertyValue = property.GetValueFunc(instance);

        var isDefaultValue = propertyValue == property.DefaultValue;
        if (isDefaultValue)
        {
            return (null, true);
        }

        // check inline
        {
            if (propertyValue is Style style)
            {
                return GetStylePropertyValueOfHtmlElementForSerialize(instance, style, context);
            }
        }

        if (propertyValue is Action action)
        {
            if (action.Target is ReactStatefulComponent target)
            {
                propertyValue = new RemoteMethodInfo { IsRemoteMethod = true, remoteMethodName = action.Method.Name, HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier };
            }
            else
            {
                throw HandlerMethodShouldBelongToReactComponent(propertyInfo);
            }
        }

        if (propertyInfo.PropertyType.IsGenericType)
        {
            if (propertyInfo.PropertyType.IsGenericAction1or2or3())
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
                            HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier,
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
            static object getTargetValueFromExpression(PropertyInfo pi, LambdaExpression expression)
            {
                if (expression.Body is MemberExpression memberExpression)
                {
                    while (true)
                    {
                        if (memberExpression.Expression is MemberExpression me)
                        {
                            memberExpression = me;
                            continue;
                        }

                        if (memberExpression.Expression is ConstantExpression constantExpression)
                        {
                            return constantExpression.Value;
                        }

                        throw HandlerMethodShouldBelongToReactComponent(pi, expression.ToString());
                    }
                }

                throw HandlerMethodShouldBelongToReactComponent(pi, expression.ToString());
            }

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

            var bindInfo = GetExpressionAsBindingInfo(propertyInfo,  calculateSourcePathFunc);
            if (bindInfo == null)
            {
                return (null, true);
            }

            if (getTargetValueFromExpression(propertyInfo, propertyValue as LambdaExpression) is ReactStatefulComponent target)
            {
                bindInfo.HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier;
            }

            return (bindInfo, false);
        }

        if (propertyValue is Element element)
        {
            element.key ??= "0";

            propertyValue = new InnerElementInfo
            {
                IsElement = true,
                Element   = element.ToJsonMap(context)
            };
        }

        var templateAttribute = propertyInfo.GetCustomAttribute<ReactTemplateAttribute>();
        if (templateAttribute is not null)
        {
            var func = (Delegate)propertyInfo.GetValue(instance);
            if (func == null)
            {
                return (null, true);
            }

            var method = instance.GetType().GetMethod(templateAttribute.MethodNameForGettingItemsSource, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method == null)
            {
                throw new MissingMethodException(templateAttribute.MethodNameForGettingItemsSource);
            }

            IReadOnlyJsonMap convertToReactNode(object item)
            {
                var reactNode = (Element)func.DynamicInvoke(item);
                if (reactNode is not null && item is not null)
                {
                    reactNode.key ??= item.GetType().GetProperty("key")?.GetValue(item)?.ToString();
                }

                return reactNode.ToJsonMap(context);
            }

            var itemTemplates = (IEnumerable)method.Invoke(instance, new object[] { });

            var results = new List<ItemTemplateInfo>();

            if (itemTemplates is not null)
            {
                foreach (var item in itemTemplates)
                {
                    results.Add(new ItemTemplateInfo { Item = item, ElementAsJson = convertToReactNode(item) });
                }
            }

            var template = new ItemTemplate
            {
                ___ItemTemplates___ = results
            };

            if (propertyInfo.GetCustomAttribute<ReactTemplateForNullAttribute>() is not null)
            {
                template.___TemplateForNull___ = convertToReactNode(null);
            }

            return (template, false);
        }

        var reactTransformValueInClient = propertyInfo.GetCustomAttribute<ReactTransformValueInClientAttribute>();
        if (reactTransformValueInClient is not null)
        {
            var jsonMap = new JsonMap();

            jsonMap.Add("$transformValueFunction", reactTransformValueInClient.TransformFunction);
            jsonMap.Add("RawValue", propertyValue);

            return (jsonMap, false);
        }

        return (propertyValue, false);
    }

    static string GetReactComponentTypeInfo(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetFullName();
    }

    static (Style style, bool noNeedToExport) GetStylePropertyValueOfHtmlElementForSerialize(object instance, Style style, ElementSerializerContext context)
    {
        List<CssPseudoCodeInfo> pseudos = null;

        if (style._hover is not null)
        {
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
        
        if (pseudos is not null || style._mediaQueries is not null)
        {
            var cmp = context.componentStack.Peek();
            
            var cssClassName = context.DynamicStyles.GetClassName(new CssClassInfo
            {
                ComponentUniqueIdentifier= cmp.ComponentUniqueIdentifier,
                Name    = "_"+cmp.ComponentUniqueIdentifier + "_" + cmp.GetType().FullName?.Replace(".", "_").Replace("+", "_").Replace("/", "_"),
                Pseudos = pseudos,
                MediaQueries = style._mediaQueries?.Select(pair => (pair.query, pair.style.ToCssWithImportant())).ToList()
            });

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
                throw new NotImplementedException("Style attribute problem occured.");
            }
        }

        if (IsEmptyStyle(style))
        {
            return (null, true);
        }

        return (style, false);
    }

    static string GetTypeFullNameOfState(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetProperty("state")!.PropertyType.GetFullName();
    }

    static Exception HandlerMethodShouldBelongToReactComponent(PropertyInfo propertyInfo)
    {
        throw new InvalidOperationException("Delegate method should belong to ReactComponent. Please give named method to " + propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name);
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
            if (didMountMethodInfo.DeclaringType != typeof(ReactStatefulComponent))
            {
                return true;
            }
        }

        return false;
    }

    static void InitializeKeyIfNotExists(Element element)
    {
        if (element.key == null)
        {
            throw new DeveloperException("key of react component cannot be null");
        }

        var orderOfChild = 0;

        foreach (var sibling in element.children)
        {
            if (sibling is not null)
            {
                sibling.key ??= orderOfChild.ToString();
            }

            orderOfChild++;
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
    public IEnumerable<ItemTemplateInfo> ___ItemTemplates___ { get; set; }
    public IReadOnlyJsonMap ___TemplateForNull___ { get; set; }
}

sealed class ItemTemplateInfo
{
    public IReadOnlyJsonMap ElementAsJson { get; set; }
    public object Item { get; set; }
}