using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using static ReactDotNet.ElementSerializer;

namespace ReactDotNet;

static class JsonSerializationOptionHelper
{
    #region Public Methods
    public static JsonSerializerOptions Modify(JsonSerializerOptions options)
    {
        // options.WriteIndented    = true;
        options.IgnoreNullValues = true;

        options.PropertyNamingPolicy = Mixin.JsonNamingPolicy;
        options.Converters.Add(new JsonConverterForElement());

        options.Converters.Add(new JsonConverterForEnum());

        options.Converters.Add(new ClientTaskConverter());

        return options;
    }
    #endregion

    public class JsonConverterForEnum : JsonConverterFactory
    {
        #region Public Methods
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.Assembly == typeof(JsonConverterForEnum).Assembly)
            {
                return typeToConvert.IsSubclassOf(typeof(Enum));
            }

            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converter = (JsonConverter)Activator.CreateInstance(typeof(EnumToStringConverter<>)
                                                                       .MakeGenericType(typeToConvert),
                                                                    BindingFlags.Instance | BindingFlags.Public,
                                                                    binder: null,
                                                                    args: null,
                                                                    culture: null)!;

            return converter;
        }
        #endregion
    }

    public class JsonConverterForElement : JsonConverterFactory
    {
        #region Public Methods
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsSubclassOf(typeof(Element))
                   || typeToConvert.FullName == typeof(Element).FullName
                   || typeToConvert.IsSubclassOf(typeof(ReactComponent<>))
                   || typeToConvert.IsSubclassOf(typeof(ReactComponent))
                ;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converter = (JsonConverter)Activator.CreateInstance(typeof(JsonConverterForElement<>)
                                                                       .MakeGenericType(typeToConvert),
                                                                    BindingFlags.Instance | BindingFlags.Public,
                                                                    binder: null,
                                                                    args: null,
                                                                    culture: null)!;

            return converter;
        }
        #endregion
    }

    public class JsonConverterForElement<T> : JsonConverter<T> where T : Element
    {
        #region Public Methods
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            string fixPropertyName(string propertyName)
            {
                if (options.PropertyNamingPolicy != null)
                {
                    propertyName = options.PropertyNamingPolicy.ConvertName(propertyName);
                }

                return propertyName;
            }
            
            
            var elementSerializationExtraData = options.GetElementSerializationExtraData();

            value.BeforeSerialize();

            // maybe root element is inherits from ReactElement
            {
                if (value is ReactComponent reactComponent)
                {
                    JsonSerializer.Serialize(writer, GetElementTreeOfStatelessReactComponent(reactComponent), options);
                    return;
                }
            }

            writer.WriteStartObject();

            TryInitStateProperty(value,elementSerializationExtraData);

            var reactAttributes = new List<string>();

            foreach (var propertyInfo in value.GetType().GetProperties().Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null))
            {
                var propertyName = fixPropertyName(GetPropertyName(propertyInfo));

                var (propertyValue, noNeedToExport) = getPropertyValue(value, propertyInfo, propertyName);
                if (noNeedToExport)
                {
                    continue;
                }

                writer.WritePropertyName(propertyName);

                JsonSerializer.Serialize(writer, propertyValue, options);

                if (value is ThirdPartyReactComponent || value is HtmlElement)
                {
                    if (propertyInfo.GetCustomAttribute<ReactAttribute>() != null)
                    {
                        reactAttributes.Add(propertyName);
                    }
                }
            }

            if (reactAttributes.Count > 0)
            {
                writer.WritePropertyName("reactAttributes");

                writer.WriteStartArray();
                foreach (var item in reactAttributes)
                {
                    writer.WriteStringValue(item);
                }

                writer.WriteEndArray();
            }

            if (value.children.Count > 0)
            {
                writer.WritePropertyName("children");

                writer.WriteStartArray();

                var breadCrumpPath = elementSerializationExtraData.BreadCrumpPath;

                var i = 0;
                foreach (var item in value.children)
                {
                    elementSerializationExtraData.BreadCrumpPath = breadCrumpPath + "," + i;

                    if (item is ReactComponent reactComponent)
                    {
                        JsonSerializer.Serialize(writer, GetElementTreeOfStatelessReactComponent(reactComponent), options);
                        i++;
                        continue;
                    }

                    if (item is IReactStatefulComponent reactStatefulComponent)
                    {
                        if (elementSerializationExtraData.RootElement is IReactStatefulComponent rootElementAsReactStatefulComponent)
                        {
                            reactStatefulComponent.Context ??= rootElementAsReactStatefulComponent.Context;
                        }
                    }

                    JsonSerializer.Serialize(writer, item, options);
                    i++;
                }

                elementSerializationExtraData.BreadCrumpPath = breadCrumpPath;

                writer.WriteEndArray();
            }

            writer.WriteEndObject();

            

           

            

            
        }

        

        
        #endregion
    }

    public class ClientTaskConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(ClientTask));
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(PolymorphicJsonConverter<>).MakeGenericType(typeToConvert));
        }
    }

    class PolymorphicJsonConverter<T> : JsonConverter<T>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            foreach (var property in value.GetType().GetProperties())
            {
                if (!property.CanRead)
                    continue;
                var propertyValue = property.GetValue(value);
                writer.WritePropertyName(property.Name);
                JsonSerializer.Serialize(writer, propertyValue, options);
            }

            writer.WriteEndObject();
        }
    }



    

    class EnumToStringConverter<T> : JsonConverter<T>
    {
        #region Public Methods
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString()?.ToLower());
        }
        #endregion
    }

    
}

[Serializable]
public sealed class EventInfo
{
    #region Public Properties
    [JsonPropertyName("$isRemoteMethod")]
    public bool IsRemoteMethod { get; set; }

    public string remoteMethodName { get; set; }
    #endregion
}


[Serializable]
public sealed class BindInfo
{
    public string eventName { get; set; }

    [JsonPropertyName("$isBinding")]
    public bool IsBinding { get; set; }

    public string[] jsValueAccess { get; set; }

    public string[] sourcePath { get; set; }

    public string targetProp { get; set; }
    public string defaultValue { get;  set; }
}



public class JsonWriterContext
{

}
public static class ElementSerializer
{

    public static bool IsEmptyStyle(object value)
    {
        if (value is Style style)
        {
            return style.GetValues().Count == 0;
        }

        return false;
    }

    public static string GetPropertyName(PropertyInfo propertyInfo)
    {
        var propertyName = propertyInfo.Name;

        var jsonPropertyNameAttribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
        if (jsonPropertyNameAttribute != null)
        {
            propertyName = jsonPropertyNameAttribute.Name;
        }

        return propertyName;
    }

    

    public static IReadOnlyDictionary<string, object> ToMap(this Element element, StateTree stateTree = null)
    {
        if (stateTree == null)
        {
            stateTree = new StateTree();
            
        }
        // maybe root element is inherits from ReactElement
        if (element is ReactComponent reactComponent)
        {
            return ToMap(GetElementTreeOfStatelessReactComponent(reactComponent), stateTree);
        }

        var map = new Dictionary<string, object>();

        if (element is HtmlElement htmlElement)
        {
            map.Add("$type", htmlElement.Type);
        }
        
        if (element is IReactStatefulComponent reactStatefulComponent)
        {
            map.Add(nameof(reactStatefulComponent.___Type___), reactStatefulComponent.___Type___);
            map.Add(nameof(reactStatefulComponent.___TypeOfState___), reactStatefulComponent.___TypeOfState___);
            if (reactStatefulComponent.___HasComponentDidMountMethod___)
            {
                map.Add(nameof(reactStatefulComponent.___HasComponentDidMountMethod___), reactStatefulComponent.___HasComponentDidMountMethod___);
            }

            reactStatefulComponent.Context = stateTree.Context;
            
            TryInitStateProperty(element, stateTree);

            

            map.Add(nameof(reactStatefulComponent.___RootNode___), ToMap(reactStatefulComponent.render(), stateTree));

            return map;

        }
        

        var reactAttributes = new List<string>();
        
        foreach (var propertyInfo in element.GetType().GetProperties().Where(x => x.GetCustomAttribute<ReactAttribute>() != null))
        {
            var (propertyValue, noNeedToExport) = getPropertyValue(element, propertyInfo, GetPropertyName(propertyInfo));
            if (noNeedToExport)
            {
                continue;
            }

            reactAttributes.Add(GetPropertyName(propertyInfo));

            map.Add(GetPropertyName(propertyInfo), propertyValue);
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

    public static (object value, bool noNeedToExport) getPropertyValue(object instance, PropertyInfo propertyInfo, string propertyName)
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
                Element = element
            };
        }

        return (propertyValue, false);
    }

    public static BindInfo GetExpressionAsBindingInfo(PropertyInfo propertyInfo, ReactDefaultValueAttribute reactDefaultValueAttribute, Func<string[]> calculateSourcePathFunc)
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


    public static Element GetElementTreeOfStatelessReactComponent(ReactComponent reactComponent)
    {
        var rootElement = reactComponent.render();

        rootElement.key = reactComponent.key;

        return rootElement;
    }

    public static void TryInitStateProperty(Element element, StateTree stateTree)
    {
        if (stateTree == null)
        {
            return;
        }
        
        if (element is IReactStatefulComponent)
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
                        var stateValue = JsonSerializer.Deserialize(clientStateInfo.StateAsJson, statePropertyInfo.PropertyType);
                        statePropertyInfo.SetValue(element, stateValue);
                    }
                }
            }
        }
    }
}


public class InnerElementInfo
{
    public Element Element { get; set; }

    [JsonPropertyName("$isElement")]
    public bool IsElement { get; set; }
}