using System.Reflection;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public static class ElementSerializer
{
    public static IReadOnlyDictionary<string, object> ToMap(this Element element, StateTree stateTree)
    {
        // maybe root element is inherits from ReactElement
        if (element is ReactComponent reactComponent)
        {
            reactComponent.Context = stateTree.Context;
            return ToMap(GetElementTreeOfStatelessReactComponent(reactComponent), stateTree);
        }

        element.BeforeSerialize();

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

        if (element is ReactStatefulComponent reactStatefulComponent)
        {
            if (stateTree.BreadCrumpPath != "0")
            {
                if (true == stateTree.ChildStates?.TryGetValue(stateTree.BreadCrumpPath, out ClientStateInfo clientStateInfo))
                {
                    var statePropertyInfo = reactStatefulComponent.GetType().GetProperty("state");
                    if (statePropertyInfo == null)
                    {
                        throw new MissingMemberException(reactStatefulComponent.GetType().GetFullName(), "state");
                    }

                    if (statePropertyInfo.PropertyType.GetFullName() == clientStateInfo.FullTypeNameOfState)
                    {
                        var stateValue = Json.DeserializeJsonByNewtonsoft(clientStateInfo.StateAsJson, statePropertyInfo.PropertyType);
                        statePropertyInfo.SetValue(reactStatefulComponent, stateValue);
                    }
                }
            }

            reactStatefulComponent.Context = stateTree.Context;
            reactStatefulComponent.OnStateInitialized();

            map.Add("state", reactStatefulComponent.GetType().GetProperty("state")?.GetValue(reactStatefulComponent));

            map.Add(___RootNode___, ToMap(reactStatefulComponent.render(), stateTree));

            map.Add(___Type___, GetReactComponentTypeInfo(reactStatefulComponent));
            map.Add(___TypeOfState___, GetTypeFullNameOfState(reactStatefulComponent));
            if (HasComponentDidMountMethod(reactStatefulComponent))
            {
                map.Add(___HasComponentDidMountMethod___, true);
            }

            map.Add(nameof(reactStatefulComponent.key), reactStatefulComponent.key);

            return map;
        }

        foreach (var propertyInfo in element.GetType().GetProperties().Where(x => x.GetCustomAttribute<ReactAttribute>() != null))
        {
            var (propertyValue, noNeedToExport) = getPropertyValue(element, propertyInfo, stateTree);
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

            var breadCrumpPath = stateTree.BreadCrumpPath;

            var i = 0;
            foreach (var item in element.children)
            {
                stateTree.BreadCrumpPath = breadCrumpPath + "," + i;

                childElements.Add(ToMap(item, stateTree));
                i++;
            }

            stateTree.BreadCrumpPath = breadCrumpPath;

            map.Add("$children", childElements);
        }

        return map;
    }
    static Element GetElementTreeOfStatelessReactComponent(ReactComponent reactComponent)
    {
        var rootElement = reactComponent.render();

        rootElement.key = reactComponent.key;

        return rootElement;
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

    static (object value, bool noNeedToExport) getPropertyValue(object instance, PropertyInfo propertyInfo, StateTree stateTree)
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

        {
            var isDefaultValue = propertyValue == propertyInfo.PropertyType.GetDefaultValue();
            if (isDefaultValue || IsEmptyStyle(propertyValue))
            {
                return (null, true);
            }
        }

        if (propertyValue is Action action)
        {
            propertyValue = new EventInfo { IsRemoteMethod = true, remoteMethodName = action.Method.Name };
        }

        if (propertyInfo.PropertyType.IsGenericType)
        {
            if (propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Action<>))
            {
                propertyValue = new EventInfo { IsRemoteMethod = true, remoteMethodName = ((Delegate)propertyValue)?.Method.Name };
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
                Element   = element.ToMap(stateTree)
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
                (Func<object, IReadOnlyDictionary<string, object>>)(item => ((Element)func.DynamicInvoke(item)).ToMap(stateTree))
            });

            var template = new ItemTemplate
            {
                ___ItemTemplates___ = itemTemplates
            };

            if (propertyInfo.GetCustomAttribute<ReactTemplateForNullAttribute>() is not null)
            {
                template.___TemplateForNull___ = Try(() => ((Element)func.DynamicInvoke((object)null))?.ToMap(stateTree)).value;
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

    static bool IsEmptyStyle(object value)
    {
        if (value is Style style)
        {
            return style.GetValues().Count == 0;
        }

        return false;
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
    static bool HasComponentDidMountMethod(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetMethod("ComponentDidMount", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
    }

    static string GetReactComponentTypeInfo(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetFullName();
    }

    static string GetTypeFullNameOfState(object reactStatefulComponent)
    {
        return reactStatefulComponent.GetType().GetProperty("state")!.PropertyType.GetFullName();
    }

    const string ___Type___ = "$Type";
    const string ___TypeOfState___ = "$TypeOfState";
    const string ___RootNode___ = "$RootNode";
    const string ___HasComponentDidMountMethod___ = "$HasComponentDidMountMethod";
}

class ItemTemplate
{
    public List<KeyValuePair<object, object>> ___ItemTemplates___ { get; set; }
    public object ___TemplateForNull___ { get; set; }
}