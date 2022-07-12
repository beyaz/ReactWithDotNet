using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactDotNet;

static class JsonSerializationOptionHelper
{
    #region Public Methods
    public static JsonSerializerOptions Modify(JsonSerializerOptions options)
    {
        // options.WriteIndented    = true;
        options.IgnoreNullValues = true;
        

        options.PropertyNamingPolicy = Mixin.JsonNamingPolicy;
        options.Converters.Add(new Union_String_Enum_Converter());
        options.Converters.Add(new JsonConverterForElement());

        options.Converters.Add(new JsonConverterForEnum());

        options.Converters.Add(new ClientTaskConverter());
        

        return options;
    }
    #endregion

    #region Methods
    static object GetValueFromUnion<B>(Union<string, B> union) where B : Enum
    {
        if (union.a != null)
        {
            return union.a;
        }

        var b             = union.b;
        var field         = b.GetType().GetField(b.ToString());
        var nameAttribute = (NameAttribute) field?.GetCustomAttributes(typeof(NameAttribute)).FirstOrDefault();
        if (nameAttribute != null)
        {
            return nameAttribute.value;
        }

        return b;
    }
    #endregion

    public class Union_String_Enum_Converter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType)
            {
                var gtd = typeToConvert.GetGenericTypeDefinition();
                if (gtd == typeof(Union<,>))
                {
                    return true;
                }
            }

            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var genericArguments = typeToConvert.GetGenericArguments();

            var converter = (JsonConverter) Activator.CreateInstance(typeof(Union_String_Enum_Converter<>)
                                                                        .MakeGenericType(genericArguments[1]),
                                                                     BindingFlags.Instance | BindingFlags.Public,
                                                                     binder: null,
                                                                     args: null,
                                                                     culture: null)!;

            return converter;
        }
    }

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
            var converter = (JsonConverter) Activator.CreateInstance(typeof(EnumToStringConverter<>)
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
            var converter = (JsonConverter) Activator.CreateInstance(typeof(JsonConverterForElement<>)
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
            var elementSerializationExtraData = options.GetElementSerializationExtraData();

            value.BeforeSerialize();

            

            // maybe root element is inherits from ReactElement
            {
                if (value is ReactComponent reactComponent)
                {
                    JsonSerializer.Serialize(writer, renderStatelessReactComponent(reactComponent), options);
                    return;
                }
            }


            writer.WriteStartObject();

            tryInitStateProperty();

            var reactAttributes = new List<string>();

            foreach (var propertyInfo in value.GetType().GetProperties().Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null))
            {
                var propertyName = getPropertyName(propertyInfo);

                var (propertyValue, noNeedToExport) = getPropertyValue(propertyInfo, propertyName);
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
                        JsonSerializer.Serialize(writer, renderStatelessReactComponent(reactComponent), options);
                        i++;
                        continue;
                    }

                    if (item is  IReactStatefulComponent reactStatefulComponent)
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

            void tryInitStateProperty()
            {
                if (value is IReactStatefulComponent)
                {
                    if (elementSerializationExtraData.BreadCrumpPath != "0")
                    {
                        if (true == elementSerializationExtraData.ChildStates?.TryGetValue(elementSerializationExtraData.BreadCrumpPath, out ClientStateInfo clientStateInfo))
                        {
                            var statePropertyInfo = value.GetType().GetProperty("state");
                            if (statePropertyInfo == null)
                            {
                                throw new MissingMemberException(value.GetType().GetFullName(), "state");
                            }

                            if (statePropertyInfo.PropertyType.GetFullName() == clientStateInfo.FullTypeNameOfState)
                            {
                                var stateValue = JsonSerializer.Deserialize(clientStateInfo.StateAsJson, statePropertyInfo.PropertyType);
                                statePropertyInfo.SetValue(value, stateValue);
                            }
                        }
                    }
                }
            }

            string getPropertyName(PropertyInfo propertyInfo)
            {
                var propertyName = propertyInfo.Name;

                var jsonPropertyNameAttribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
                if (jsonPropertyNameAttribute != null)
                {
                    propertyName = jsonPropertyNameAttribute.Name;
                }

                if (options.PropertyNamingPolicy != null)
                {
                    propertyName = options.PropertyNamingPolicy.ConvertName(propertyName);
                }

                return propertyName;
            }

            (object value, bool noNeedToExport) getPropertyValue(PropertyInfo propertyInfo, string propertyName)
            {
                var propertyValue = propertyInfo.GetValue(value);
                
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
                    propertyValue = new EventInfo {IsRemoteMethod = true, remoteMethodName = action.Method.Name};
                }

                if (propertyInfo.PropertyType.IsGenericType)
                {
                    if (propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Action<>))
                    {
                        propertyValue = new EventInfo {IsRemoteMethod = true, remoteMethodName = ((Delegate) propertyValue)?.Method.Name};
                    }
                }

                if (propertyValue is Enum enumValue)
                {
                    propertyValue = enumValue.ToString();
                }

                {
                    if (propertyValue is BindibleProperty<string> bindibleProperty)
                    {
                        if (bindibleProperty.PathInState != null)
                        {
                            string[] calculateSourcePathFunc() => bindibleProperty.AsBindingSourcePathInState();

                            var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, calculateSourcePathFunc);
                            if (bindInfo == null)
                            {
                                return (null, true);
                            }

                            return (bindInfo, false);
                        }

                        var rawValue = bindibleProperty.RawValue;

                        if (rawValue is null  && reactDefaultValueAttribute is not null)
                        {
                            rawValue = reactDefaultValueAttribute.DefaultValue;
                        }

                        return (rawValue, false);
                    }

                    
                }

                {
                    if (propertyValue is BindibleProperty<int> bindibleProperty)
                    {
                        if (bindibleProperty.PathInState != null)
                        {
                            string[] calculateSourcePathFunc() => bindibleProperty.AsBindingSourcePathInState();

                            var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, calculateSourcePathFunc);
                            if (bindInfo == null)
                            {
                                return (null, true);
                            }

                            return (bindInfo, false);
                        }

                        var rawValue = bindibleProperty.RawValue;

                        if (rawValue is 0 && reactDefaultValueAttribute is not null)
                        {
                            rawValue = int.Parse(reactDefaultValueAttribute.DefaultValue);
                        }

                        return (rawValue, false);
                    }
                }

                {
                    if (propertyValue is Expression<Func<string>> expression)
                    {
                        string[] calculateSourcePathFunc() => expression.AsBindingSourcePathInState().Split('.', StringSplitOptions.RemoveEmptyEntries);

                        var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, calculateSourcePathFunc);
                        if (bindInfo == null)
                        {
                            return (null, true);
                        }

                        return (bindInfo, false);
                    }
                }

                {
                    if (propertyValue is Expression<Func<int>> expression)
                    {
                        string[] calculateSourcePathFunc() => expression.AsBindingSourcePathInState().Split('.', StringSplitOptions.RemoveEmptyEntries);

                        var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, calculateSourcePathFunc);
                        if (bindInfo == null)
                        {
                            return (null, true);
                        }

                        return (bindInfo, false);
                    }
                }
                {
                    if (propertyValue is Expression<Func<bool>> expression)
                    {
                        string[] calculateSourcePathFunc() => expression.AsBindingSourcePathInState().Split('.', StringSplitOptions.RemoveEmptyEntries);

                        var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, calculateSourcePathFunc);
                        if (bindInfo == null)
                        {
                            return (null, true);
                        }

                        return (bindInfo, false);
                    }
                }

                if (propertyName != nameof(IReactStatefulComponent.___RootNode___) && propertyValue is Element element)
                {
                    propertyValue = new InnerElementInfo
                    {
                        IsElement = true,
                        Element   = element
                    };
                }

                return (propertyValue, false);
            }

            static Element renderStatelessReactComponent(ReactComponent reactComponent)
            {
                var rootElement = reactComponent.render();

                rootElement.key = reactComponent.key;

                return rootElement;
            }
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

    class BindInfo
    {
        #region Public Properties
        public string eventName { get; set; }

        [JsonPropertyName("$isBinding")]
        public bool IsBinding { get; set; }

        public string[] jsValueAccess { get; set; }

        public string[] sourcePath { get; set; }

        public string targetProp { get; set; }
        public string defaultValue { get; internal set; }
        #endregion
    }

    class InnerElementInfo
    {
        public Element Element { get; set; }

        [JsonPropertyName("$isElement")]
        public bool IsElement { get; set; }
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

    class EventInfo
    {
        #region Public Properties
        [JsonPropertyName("$isRemoteMethod")]
        public bool IsRemoteMethod { get; set; }

        public string remoteMethodName { get; set; }
        #endregion
    }

    class Union_String_Enum_Converter<B> : JsonConverter<Union<string, B>> where B : Enum
    {
        #region Public Methods
        public override Union<string, B> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Union<string, B> value, JsonSerializerOptions options)
        {
            void writeStringValue(string v)
            {
                if (options.PropertyNamingPolicy != null)
                {
                    v = options.PropertyNamingPolicy.ConvertName(v);
                }

                writer.WriteStringValue(v);
            }

            var rawValue = GetValueFromUnion(value);

            if (rawValue is string str)
            {
                writeStringValue(str);
                return;
            }

            writeStringValue(rawValue.ToString());
        }
        #endregion
    }
}