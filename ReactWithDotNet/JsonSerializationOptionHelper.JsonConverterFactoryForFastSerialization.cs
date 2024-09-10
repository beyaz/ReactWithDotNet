using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class JsonSerializationOptionHelper
{
    sealed class JsonConverterFactoryForFastSerialization : JsonConverterFactory
    {
        internal static readonly JsonConverterFactoryForFastSerialization Instance = new();

        static readonly ConcurrentDictionary<Type, JsonConverter> cache = [];

        public override bool CanConvert(Type typeToConvert)
        {
            return TryFindRelatedJsonConverterType(typeToConvert) is not null;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (cache.TryGetValue(typeToConvert, out var value))
            {
                return value;
            }

            return cache[typeToConvert] = (JsonConverter)Activator.CreateInstance(TryFindRelatedJsonConverterType(typeToConvert));
        }

        static Type TryFindRelatedJsonConverterType(Type typeToConvert)
        {
            return typeToConvert.Assembly.GetType(typeToConvert.FullName + "JsonConverter");
        }
    }
}