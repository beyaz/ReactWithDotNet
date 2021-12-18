using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactDotNet
{
    public static class JsonSerializationOptionHelper
    {
        public static void Modify(JsonSerializerOptions options)
        {
            options.WriteIndented        = true;
            options.IgnoreNullValues     = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new CustomJsonStringEnumConverter());
            options.Converters.Add(new UnionConverter<AlignContent>());
            options.Converters.Add(new UnionConverter<Display>());
            


        }

        static object GetValueFromUnion<B>(Union<string, B> union) where  B:Enum
        {
            if (union.a != null)
            {
                return union.a;
            }

            var b     = union.b;
            var field         = b.GetType().GetField(b.ToString());
            var nameAttribute = (NameAttribute)field?.GetCustomAttributes(typeof(NameAttribute)).FirstOrDefault();
            if (nameAttribute != null)
            {

                return nameAttribute.value;
            }

            return b;
        }
        public class UnionConverter<B> : JsonConverter<Union<string, B>> where B : Enum
        {
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
        }


        public class CustomJsonStringEnumConverter : JsonConverterFactory
        {
            private readonly JsonNamingPolicy namingPolicy;
            private readonly bool allowIntegerValues;
            private readonly JsonStringEnumConverter baseConverter;

            public CustomJsonStringEnumConverter() : this(null, true) { }

            public CustomJsonStringEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
            {
                this.namingPolicy       = namingPolicy;
                this.allowIntegerValues = allowIntegerValues;
                this.baseConverter      = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);
            }

            public override bool CanConvert(Type typeToConvert) => baseConverter.CanConvert(typeToConvert);

            public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            {
                var query = from field in typeToConvert.GetFields(BindingFlags.Public | BindingFlags.Static)
                            let attr = field.GetCustomAttribute<NameAttribute>()
                            where attr != null
                            select (field.Name, ((NameAttribute)attr).value);
                var dictionary = query.ToDictionary(p => p.Item1, p => p.Item2);
                if (dictionary.Count > 0)
                {
                    return new JsonStringEnumConverter(new DictionaryLookupNamingPolicy(dictionary, namingPolicy), allowIntegerValues).CreateConverter(typeToConvert, options);
                }
                else
                {
                    return baseConverter.CreateConverter(typeToConvert, options);
                }
            }
        }

        public class JsonNamingPolicyDecorator : JsonNamingPolicy
        {
            readonly JsonNamingPolicy underlyingNamingPolicy;

            public JsonNamingPolicyDecorator(JsonNamingPolicy underlyingNamingPolicy) => this.underlyingNamingPolicy = underlyingNamingPolicy;

            public override string ConvertName(string name) => underlyingNamingPolicy == null ? name : underlyingNamingPolicy.ConvertName(name);
        }

        internal class DictionaryLookupNamingPolicy : JsonNamingPolicyDecorator
        {
            readonly Dictionary<string, string> dictionary;

            public DictionaryLookupNamingPolicy(Dictionary<string, string> dictionary, JsonNamingPolicy underlyingNamingPolicy) : base(underlyingNamingPolicy) => this.dictionary = dictionary ?? throw new ArgumentNullException();

            public override string ConvertName(string name) => dictionary.TryGetValue(name, out var value) ? value : base.ConvertName(name);
        }
    }
}