using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactDotNet;

public static class ElementSerializer
{
    #region Public Methods
    public static IReadOnlyDictionary<string, object> ToMap(this Element element, StateTree stateTree)
    {
        // maybe root element is inherits from ReactElement
        if (element is ReactComponent reactComponent)
        {
            return ToMap(GetElementTreeOfStatelessReactComponent(reactComponent), stateTree);
        }

        element.BeforeSerialize();

        var map = new Dictionary<string, object>();

        if (element is HtmlElement htmlElement)
        {
            map.Add("$type", htmlElement.Type);
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            map.Add("$type", thirdPartyReactComponent.Type);
        }

        if (element is IReactStatefulComponent reactStatefulComponent)
        {
            if (stateTree.BreadCrumpPath != "0")
            {
                if (true == stateTree.ChildStates?.TryGetValue(stateTree.BreadCrumpPath, out ClientStateInfo clientStateInfo))
                {
                    var statePropertyInfo = element.GetType().GetProperty("state");
                    if (statePropertyInfo == null)
                    {
                        throw new MissingMemberException(element.GetType().GetFullName(), "state");
                    }

                    if (statePropertyInfo.PropertyType.GetFullName() == clientStateInfo.FullTypeNameOfState)
                    {
                        var stateValue = Json.DeserializeJson(clientStateInfo.StateAsJson, statePropertyInfo.PropertyType);
                        statePropertyInfo.SetValue(element, stateValue);
                    }
                }
            }

            reactStatefulComponent.Context = stateTree.Context;
            ((ReactStatefulComponent)reactStatefulComponent).OnStateInitialized();

            map.Add("state", reactStatefulComponent.GetType().GetProperty("state")?.GetValue(reactStatefulComponent));

            map.Add(nameof(reactStatefulComponent.___RootNode___), ToMap(reactStatefulComponent.render(), stateTree));

            map.Add(nameof(reactStatefulComponent.___Type___), reactStatefulComponent.___Type___);
            map.Add(nameof(reactStatefulComponent.___TypeOfState___), reactStatefulComponent.___TypeOfState___);
            if (reactStatefulComponent.___HasComponentDidMountMethod___)
            {
                map.Add(nameof(reactStatefulComponent.___HasComponentDidMountMethod___), reactStatefulComponent.___HasComponentDidMountMethod___);
            }

            map.Add(nameof(element.key), element.key);

            return map;
        }

        var reactAttributes = new List<string>();

        foreach (var propertyInfo in element.GetType().GetProperties().Where(x => x.GetCustomAttribute<ReactAttribute>() != null))
        {
            var (propertyValue, noNeedToExport) = getPropertyValue(element, propertyInfo, GetPropertyName(propertyInfo), stateTree);
            if (noNeedToExport)
            {
                continue;
            }

            reactAttributes.Add(GetPropertyName(propertyInfo));

            map.Add(GetPropertyName(propertyInfo), propertyValue);
        }

        if (element is HtmlElement htmlElement2)
        {
            if (htmlElement2.innerText != null)
            {
                map.Add("innerText", htmlElement2.innerText);
            }
        }

        if (reactAttributes.Count > 0)
        {
            map.Add(nameof(reactAttributes), reactAttributes);
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

            map.Add(nameof(element.children), childElements);
        }

        return map;
    }
    #endregion

    #region Methods
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

    static (object value, bool noNeedToExport) getPropertyValue(object instance, PropertyInfo propertyInfo, string propertyName, StateTree stateTree)
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
            propertyValue is Expression<Func<string>>)
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

                throw new NotImplementedException();
            }

            var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, calculateSourcePathFunc);
            if (bindInfo == null)
            {
                return (null, true);
            }

            return (bindInfo, false);
        }

        if (propertyName != nameof(IReactStatefulComponent.___RootNode___) && propertyValue is Element element)
        {
            propertyValue = new InnerElementInfo
            {
                IsElement = true,
                Element   = element.ToMap(stateTree)
            };
        }

        if (propertyValue is ItemTemplateInfo itemTemplateInfo)
        {
            var map = new List<KeyValuePair<object, object>>();
            
            foreach (var item in itemTemplateInfo._items)
            {
                map.Add(new KeyValuePair<object, object>(item, itemTemplateInfo._template(item).ToMap(stateTree)));
            }

            return (new ItemTemplate { ___ItemTemplates___ = map, ___TemplateForNull___ = itemTemplateInfo.TemplateForNull?.ToMap(stateTree) }, false);
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

    
    #endregion
}

class ItemTemplate
{
    public List<KeyValuePair<object, object>>  ___ItemTemplates___ { get; set; }
    public object ___TemplateForNull___ { get; set; }
}

public class ItemTemplateInfo
{
    internal IEnumerable _items;
    internal Func<object, Element> _template;
    public Element TemplateForNull { get; set; }
}
public class ItemTemplates<T>: ItemTemplateInfo
{
    public IEnumerable<T> Items
    {
        set => _items = value;
    }
    public  Func<T,Element> Template
    {
        set => _template = item => value((T)item);
    }

    
}