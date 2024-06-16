using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class JsonSerializationOptionHelper
{
    sealed class JsonConverterFactoryForCommounUsage : JsonConverterFactory
    {
        internal static readonly JsonConverterFactoryForCommounUsage Instance = new();

        static readonly ConcurrentDictionary<Type, TypeSerializationInfo> typeMap = [];

        public override bool CanConvert(Type typeToConvert)
        {
            if (typeMap.TryGetValue(typeToConvert, out var info))
            {
                return info.CanConvert;
            }

            var canConvert = canConvertImpl(typeToConvert);
            if (canConvert is false)
            {
                typeMap.TryAdd(typeToConvert, new() { CanConvert = false });
                return false;
            }

            var properties = typeToConvert
                .GetProperties()
                .Where(p => p.GetIndexParameters().Length == 0)
                .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>() is null)
                .Select(toCalculatedProperty)
                .ToList();

            typeMap.TryAdd(typeToConvert, new()
            {
                CanConvert             = true,
                TypeKey                = $"{typeToConvert.FullName}, {typeToConvert.Assembly.GetName().Name}",
                PropertiesAsList       = properties,
                PropertiesAsDictionary = properties.ToDictionary(x => x.JsonPropertyName)
            });
            return true;

            static bool canConvertImpl(Type typeToConvert)
            {
                if (isEnumerable(typeToConvert))
                {
                    return false;
                }

                // ignore core types
                if (typeToConvert.Assembly.GetName().Name == nameof(ReactWithDotNet))
                {
                    return false;
                }

                // ignore core types
                if (typeToConvert.FullName?.StartsWith("System.", StringComparison.OrdinalIgnoreCase) is true)
                {
                    return false;
                }

                return typeToConvert.IsClass;

                static bool isEnumerable(Type type)
                {
                    return typeof(IEnumerable).IsAssignableFrom(type);
                }
            }

            static TypeSerializationInfo.PropertyInfoCalculated toCalculatedProperty(PropertyInfo propertyInfo)
            {
                var jsonPropertyName = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? propertyInfo.Name;

                return new()
                {
                    PropertyInfo     = propertyInfo,
                    GetFunction      = ReflectionHelper.CreateGetFunction(propertyInfo),
                    SetFunction      = ReflectionHelper.CreateSetFunction(propertyInfo),
                    DefaultValue     = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null,
                    JsonPropertyName = jsonPropertyName
                };
            }
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(PolymorphicJsonConverter<>).MakeGenericType(typeToConvert));
        }

        sealed class PolymorphicJsonConverter<T> : JsonConverter<T> where T : class
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var typeSerializationInfo = typeMap[typeToConvert];

                object instance = null;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return (T)instance;
                    }

                    var propertyName = reader.GetString();

                    if (propertyName == "$type")
                    {
                        reader.Read();

                        var typeKey = reader.GetString();
                        if (typeKey is null)
                        {
                            throw new JsonException("Missing type discriminator property.");
                        }

                        var type = Type.GetType(typeKey);
                        if (type is null)
                        {
                            throw new JsonException($"Missing type: {typeKey}");
                        }

                        instance = Activator.CreateInstance(type);

                        continue;
                    }

                    var propertyInfoCalculated = typeSerializationInfo.PropertiesAsDictionary[propertyName];

                    var propertyInfo = propertyInfoCalculated.PropertyInfo;

                    var propertyValue = JsonSerializer.Deserialize(ref reader, propertyInfo.PropertyType, options);

                    propertyInfoCalculated.SetFunction(instance, propertyValue);
                }

                return (T)instance;
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                var type = value.GetType();

                var typeSerializationInfo = typeMap[type];

                writer.WriteString("$type", typeSerializationInfo.TypeKey);

                var propertyList = CollectionsMarshal.AsSpan(typeSerializationInfo.PropertiesAsList);

                var count = propertyList.Length;

                for (var i = 0; i < count; i++)
                {
                    var item = propertyList[i];

                    var propertyValue = item.GetFunction(value);

                    if (propertyValue == item.DefaultValue)
                    {
                        continue;
                    }

                    writer.WritePropertyName(item.JsonPropertyName);

                    JsonSerializer.Serialize(writer, propertyValue, options);
                }

                writer.WriteEndObject();
            }
        }

        record TypeSerializationInfo
        {
            public bool CanConvert { get; init; }

            public List<PropertyInfoCalculated> PropertiesAsList { get; init; }

            public IReadOnlyDictionary<string, PropertyInfoCalculated> PropertiesAsDictionary { get; init; }

            public string TypeKey { get; init; }

            public sealed record PropertyInfoCalculated
            {
                public PropertyInfo PropertyInfo { get; init; }
                public Func<object, object> GetFunction { get; init; }
                public Action<object, object> SetFunction { get; init; }
                public string JsonPropertyName { get; init; }
                public object DefaultValue { get; init; }
            }
        }
    }
}