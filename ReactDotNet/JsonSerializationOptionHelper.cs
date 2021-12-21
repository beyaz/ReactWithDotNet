using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ReactDotNet.PrimeReact;

namespace ReactDotNet
{

    static class JsonSerializationOptionHelper
    {

        public class JsonConverterForElement : JsonConverterFactory
        {
            public override bool CanConvert(Type typeToConvert)
            {
                return typeToConvert.IsSubclassOf(typeof(Element)) ||
                       typeToConvert.FullName == typeof(Element).FullName ||
                       typeToConvert.IsSubclassOf(typeof(ReactComponent<>));
            }

            public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            {
                JsonConverter converter = (JsonConverter)Activator.CreateInstance(typeof(JsonConverterForElement<>)
                                                                                     .MakeGenericType(typeToConvert),
                                                                                  BindingFlags.Instance | BindingFlags.Public,
                                                                                  binder: null,
                                                                                  args: null,
                                                                                  culture: null)!;

                return converter;
            }
        }

        class EventInfo
        {
            [JsonPropertyName("$isRemoteMethod")]
            public bool IsRemoteMethod { get; set; }

            public string RemoteMethodName { get; set; }
        }

        public class JsonConverterForElement<T> : JsonConverter<T> where T:Element
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                var reactAttributes = new List<string>();

                foreach (var propertyInfo in value.GetType().GetProperties().Where(x=>x.GetCustomAttribute<JsonIgnoreAttribute>() == null))
                {
                    var propertyValue = propertyInfo.GetValue(value);
                    if (propertyValue == propertyInfo.PropertyType.GetDefaultValue())
                    {
                        continue;
                    }

                    if (propertyValue is CSSStyleDeclaration style)
                    {
                        var hasValue = typeof(CSSStyleDeclaration).GetProperties().Any(x => x.GetValue(style) != x.PropertyType.GetDefaultValue());
                        if (!hasValue)
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
                    
                    JsonSerializer.Serialize(writer, propertyValue, options);

                    if (value is ThirdPartyComponent || value is HtmlElement)
                    {
                        if (propertyInfo.GetCustomAttribute<ReactAttribute>()!=null)
                        {
                            reactAttributes.Add(propertyName);
                        }
                    }
                }

                if (reactAttributes.Count>0)
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

                    foreach (var item in value.children)
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }

                    writer.WriteEndArray();
                }
                
                
                writer.WriteEndObject();
            }
        }



        #region Public Methods
        public static JsonSerializerOptions Modify(JsonSerializerOptions options)
        {
            options.WriteIndented    = true;
            options.IgnoreNullValues = true;



            options.PropertyNamingPolicy = Mixin.JsonNamingPolicy;
            options.Converters.Add(new UnionConverter<AlignContent>());
            options.Converters.Add(new UnionConverter<Display>());
            options.Converters.Add(new JsonConverterForElement()); 

            options.Converters.Add(new EnumToStringConverter<TooltipPositionType>()); 



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

        class EnumToStringConverter<T>: JsonConverter<T>
        {
            #region Public Methods
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
            #endregion
        }

        

        class UnionConverter<B> : JsonConverter<Union<string, B>> where B : Enum
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