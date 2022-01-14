using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ReactDotNet.PrimeReact;

namespace ReactDotNet
{
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
            var nameAttribute = (NameAttribute)field?.GetCustomAttributes(typeof(NameAttribute)).FirstOrDefault();
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

                var converter = (JsonConverter)Activator.CreateInstance(typeof(Union_String_Enum_Converter<>)
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
                return typeToConvert.IsSubclassOf(typeof(Element)) ||
                       typeToConvert.FullName == typeof(Element).FullName ||
                       typeToConvert.IsSubclassOf(typeof(ReactComponent<>));
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
                value.BeforeSerialize();

                writer.WriteStartObject();

                var reactAttributes = new List<string>();

                foreach (var propertyInfo in value.GetType().GetProperties().Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null))
                {
                    var propertyValue = propertyInfo.GetValue(value);
                    if (propertyValue == propertyInfo.PropertyType.GetDefaultValue())
                    {
                        continue;
                    }

                    if (propertyValue is CSSStyleDeclaration style)
                    {
                        bool hasValue(PropertyInfo x)
                        {
                            var value = x.GetValue(style);

                            var defaultValue = x.PropertyType.GetDefaultValue();

                            if (value == null)
                            {
                                if (defaultValue == null)
                                {
                                    return false;
                                }

                                return true;
                            }

                            return !value.Equals(defaultValue);
                        }
                        if (!typeof(CSSStyleDeclaration).GetProperties().Any(hasValue))
                        {
                            continue;
                        }
                    }

                    var propertyName = propertyInfo.Name;

                    var jsonPropertyNameAttribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
                    if (jsonPropertyNameAttribute != null)
                    {
                        propertyName = jsonPropertyNameAttribute.Name;
                    }

                    propertyName = options.PropertyNamingPolicy.ConvertName(propertyName);

                    writer.WritePropertyName(propertyName);

                    if (propertyValue is Action action)
                    {
                        propertyValue = new EventInfo { IsRemoteMethod = true, RemoteMethodName = action.Method.Name };
                    }

                    if (propertyInfo.PropertyType.IsGenericType)
                    {
                        if (propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Action<>))
                        {
                            propertyValue = new EventInfo { IsRemoteMethod = true, RemoteMethodName = ((Delegate)propertyValue).Method.Name };
                        }
                        
                    }

                    if (propertyValue is Enum enumValue)
                    {
                        propertyValue = enumValue.ToString();
                    }

                    if (propertyValue is Expression<Func<string>> expression)
                    {
                        var reactBindAttribute = propertyInfo.GetCustomAttribute<ReactBindAttribute>();
                        if (reactBindAttribute == null)
                        {
                            continue;
                        }

                        propertyValue = new BindInfo
                        {
                            TargetProp    = reactBindAttribute.TargetProp,
                            EventName     = reactBindAttribute.EventName,
                            SourcePath    = Extensions.Bind(expression).Split('.',StringSplitOptions.RemoveEmptyEntries),
                            IsBinding     = true,
                            JsValueAccess = reactBindAttribute.JsValueAccess.Split('.', StringSplitOptions.RemoveEmptyEntries)
                        };
                    }

                    if (propertyName != "rootElement" && propertyValue is Element element)
                    {
                        propertyValue = new InnerElementInfo
                        {
                            IsElement = true,
                            Element = element
                        };
                    }

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

                if (value.Children.Count > 0)
                {
                    writer.WritePropertyName("children");

                    writer.WriteStartArray();

                    foreach (var item in value.Children)
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }

                    writer.WriteEndArray();
                }

                writer.WriteEndObject();
            }
            #endregion
        }

        class BindInfo
        {
            #region Public Properties
            public string EventName { get; set; }

            [JsonPropertyName("$isBinding")]
            public bool IsBinding { get; set; }

            public string[] JsValueAccess { get; set; }

            public string[] SourcePath { get; set; }
            public string TargetProp { get; set; }
            #endregion
        }

        class InnerElementInfo
        {
            public Element Element{ get; set; }

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
                writer.WriteStringValue(value.ToString().ToLower());
            }
            #endregion
        }

        class EventInfo
        {
            #region Public Properties
            [JsonPropertyName("$isRemoteMethod")]
            public bool IsRemoteMethod { get; set; }

            public string RemoteMethodName { get; set; }
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
                var rawValue = GetValueFromUnion(value);

                if (rawValue is string str)
                {
                    writer.WriteStringValue(options.PropertyNamingPolicy.ConvertName(str));
                    return;
                }

                writer.WriteStringValue(options.PropertyNamingPolicy.ConvertName(rawValue.ToString()));
            }
            #endregion
        }
    }
}