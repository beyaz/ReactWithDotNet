using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactDotNet
{
    public static class JsonSerializationOptionHelper
    {
        #region Public Methods
        public static JsonSerializerOptions Modify(JsonSerializerOptions options)
        {
            options.WriteIndented        = true;
            options.IgnoreNullValues     = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new UnionConverter<AlignContent>());
            options.Converters.Add(new UnionConverter<Display>());

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