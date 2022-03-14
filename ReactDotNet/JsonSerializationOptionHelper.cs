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
        options.WriteIndented    = true;
        options.IgnoreNullValues = true;

        options.PropertyNamingPolicy = Mixin.JsonNamingPolicy;
        options.Converters.Add(new Union_String_Enum_Converter());
        options.Converters.Add(new JsonConverterForElement());

        options.Converters.Add(new JsonConverterForEnum());

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
            return typeToConvert.IsSubclassOf(typeof(Enum));
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
            return typeToConvert.IsSubclassOf(typeof(Element)) ||
                   typeToConvert.FullName == typeof(Element).FullName ||
                   typeToConvert.IsSubclassOf(typeof(ReactComponent<>));
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

                if (value is ThirdPartyComponent || value is HtmlElement)
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
                    if (item is IReactStatelessComponent statelessComponent)
                    {
                        var rootElement = statelessComponent.render();
                        rootElement.key = statelessComponent.key;
                        JsonSerializer.Serialize(writer, rootElement, options);
                        i++;
                        continue;
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
                if (propertyValue == propertyInfo.PropertyType.GetDefaultValue())
                {
                    if (reactDefaultValueAttribute != null)
                    {
                        propertyValue = reactDefaultValueAttribute.DefaultValue;
                    }
                }

                var isDefaultValue = propertyValue == propertyInfo.PropertyType.GetDefaultValue();
                if (isDefaultValue || IsEmptyStyle(propertyValue))
                {
                    return (null, true);
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
                    if (propertyValue is Expression<Func<string>> expression)
                    {
                        var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, expression);
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
                        var bindInfo = GetExpressionAsBindingInfo(propertyInfo, reactDefaultValueAttribute, expression);
                        if (bindInfo == null)
                        {
                            return (null, true);
                        }

                        return (bindInfo, false);
                    }
                }

                if (propertyName != nameof(IReactStatefulComponent.RootElement) && propertyValue is Element element)
                {
                    propertyValue = new InnerElementInfo
                    {
                        IsElement = true,
                        Element   = element
                    };
                }

                return (propertyValue, false);
            }
        }

        static BindInfo GetExpressionAsBindingInfo<TBindValuePropertyType>(PropertyInfo propertyInfo, ReactDefaultValueAttribute reactDefaultValueAttribute, Expression<Func<TBindValuePropertyType>> expression)
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
                sourcePath    = Extensions.Bind(expression).Split('.', StringSplitOptions.RemoveEmptyEntries),
                IsBinding     = true,
                jsValueAccess = reactBindAttribute.jsValueAccess.Split('.', StringSplitOptions.RemoveEmptyEntries),
                defaultValue  = defaultValue
            };
        }

        static bool IsEmptyStyle(object value)
        {
            if (value is Style style)
            {
                bool hasValue(PropertyInfo x)
                {
                    var styleProperty = x.GetValue(style);

                    var defaultValue = x.PropertyType.GetDefaultValue();

                    if (styleProperty == null)
                    {
                        if (defaultValue == null)
                        {
                            return false;
                        }

                        return true;
                    }

                    return !styleProperty.Equals(defaultValue);
                }

                if (!typeof(Style).GetProperties().Any(hasValue))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
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