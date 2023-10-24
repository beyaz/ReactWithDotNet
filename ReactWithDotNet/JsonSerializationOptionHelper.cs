using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static ReactWithDotNet.JsonSerializationOptionHelper;

namespace ReactWithDotNet;

partial class Mixin
{
    internal static object ArrangeValueForTargetType(object value, Type targetType)
    {
        if (value is null)
        {
            if (targetType.IsClass)
            {
                return null;
            }

            return Activator.CreateInstance(targetType);
        }



        if (value is JsonElement jsonElement)
        {
            if (targetType == typeof(string))
            {
                return jsonElement.GetString();
            }

            // BOOL
            if (targetType == typeof(bool) || targetType == typeof(bool?))
            {
                return jsonElement.GetBoolean();
            }


            // DATE
            if (targetType == typeof(DateTime))
            {
                return jsonElement.GetDateTime();
            }

            if (targetType == typeof(DateTime?))
            {
                if (jsonElement.TryGetDateTime(out var dateTimeValue))
                {
                    return dateTimeValue;
                }
            }

            // DATETIMEOFFSET
            if (targetType == typeof(DateTimeOffset))
            {
                return jsonElement.GetDateTimeOffset();
            }

            if (targetType == typeof(DateTimeOffset?))
            {
                if (jsonElement.TryGetDateTimeOffset(out var dateTimeOffsetValue))
                {
                    return dateTimeOffsetValue;
                }
            }

            // GUID
            if (targetType == typeof(Guid))
            {
                return jsonElement.GetGuid();
            }

            if (targetType == typeof(Guid?))
            {
                if (jsonElement.TryGetGuid(out var guidValue))
                {
                    return guidValue;
                }
            }


            // NUMBER TYPES
            if (targetType == typeof(sbyte))
            {
                return jsonElement.GetSByte();
            }

            if (targetType == typeof(byte))
            {
                return jsonElement.GetByte();
            }


            if (targetType == typeof(short))
            {
                return jsonElement.GetInt16();
            }

            if (targetType == typeof(int))
            {
                return jsonElement.GetInt32();
            }

            if (targetType == typeof(long))
            {
                return jsonElement.GetInt64();
            }

            if (targetType == typeof(double))
            {
                return jsonElement.GetDouble();
            }

            if (targetType == typeof(float))
            {
                return jsonElement.GetSingle();
            }

            if (targetType == typeof(decimal))
            {
                return jsonElement.GetDecimal();
            }


            // UNSIGNED NUMBER TYPES
            if (targetType == typeof(ushort))
            {
                return jsonElement.GetUInt16();
            }

            if (targetType == typeof(uint))
            {
                return jsonElement.GetUInt32();
            }

            if (targetType == typeof(ulong))
            {
                return jsonElement.GetUInt64();
            }

            // NULLABLE NUMBER TYPES
            if (targetType == typeof(sbyte?))
            {
                if (jsonElement.TryGetSByte(out var sbyteValue))
                {
                    return sbyteValue;
                }
            }

            if (targetType == typeof(byte?))
            {
                if (jsonElement.TryGetByte(out var byteValue))
                {
                    return byteValue;
                }
            }


            if (targetType == typeof(short?))
            {
                if (jsonElement.TryGetInt16(out var int16Value))
                {
                    return int16Value;
                }
            }

            if (targetType == typeof(int?))
            {
                if (jsonElement.TryGetInt32(out var int32Value))
                {
                    return int32Value;
                }
            }

            if (targetType == typeof(long?))
            {
                if (jsonElement.TryGetInt64(out var int64Value))
                {
                    return int64Value;
                }
            }

            if (targetType == typeof(double?))
            {
                if (jsonElement.TryGetDouble(out var doubleValue))
                {
                    return doubleValue;
                }
            }

            if (targetType == typeof(float?))
            {
                if (jsonElement.TryGetSingle(out var floatValue))
                {
                    return floatValue;
                }
            }

            if (targetType == typeof(decimal?))
            {
                if (jsonElement.TryGetDecimal(out var decimalValue))
                {
                    return decimalValue;
                }
            }



            if (jsonElement.ValueKind == JsonValueKind.Object)
            {
                if (targetType == typeof(Style))
                {
                    var style = new Style();
                    style.Import(jsonElement.Deserialize<Dictionary<string, string>>());
                    return style;
                }

                return jsonElement.Deserialize(targetType);
            }

            if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                return jsonElement.Deserialize(targetType);
            }

            throw new Exception();
        }

        if (value is string valueAsString && targetType == typeof(Type))
        {
            return JsonConverterFactoryForType.DeserializeType(valueAsString);
        }

        var changeResponse = ChangeType(value, targetType);
        if (changeResponse.exception is not null)
        {
            throw DeveloperException(changeResponse.exception.Message);
        }

        return changeResponse.value;
    }
}
static partial class JsonSerializationOptionHelper
{

   


    public static JsonSerializerOptions Modify(JsonSerializerOptions options)
    {
        options.WriteIndented  = true;
        options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        options.PropertyNamingPolicy = null;

        options.Converters.Add(new JsonConverterForEnum());

        options.Converters.Add(new StyleConverter());

        options.Converters.Add(new JsMapConverter());

        options.Converters.Add(new ReadOnlyJsonMapConverter());

        options.Converters.Add(new ValueTupleFactory());

        options.Converters.Add(new JsonConverterFactoryForType());

        return options;
    }

    public class JsonConverterForEnum : JsonConverterFactory
    {
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
                                                                    null,
                                                                    null,
                                                                    null)!;

            return converter;
        }
    }

    public class JsonConverterFactoryForType : JsonConverterFactory
    {
        static readonly TypeConverter ConverterInstance = new();

        public static Type DeserializeType(string type)
        {
            return Type.GetType(type);
        }

        public static string SerializeType(Type type)
        {
            return $"{type.FullName},{type.Assembly.GetName().Name}";
        }

        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.FullName == "System.RuntimeType" || typeToConvert.FullName == "System.Type")
            {
                return true;
            }

            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return ConverterInstance;
        }

        class TypeConverter : JsonConverter<Type>
        {
            public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.Read())
                {
                    if (reader.ValueSpan.Length > 0)
                    {
                        return DeserializeType(reader.ValueSpan.ToString());
                    }
                }

                return null;
            }

            public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
            {
                if (value is null)
                {
                    writer.WriteNullValue();
                    return;
                }

                writer.WriteStringValue(SerializeType(value));
            }
        }
    }

    class EnumToStringConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString()?.ToLower());
        }
    }

    class JsMapConverter : JsonConverter<JsonMap>
    {
        public override JsonMap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, JsonMap jsonMap, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            jsonMap.Foreach(add);

            writer.WriteEndObject();

            void add(string key, object value)
            {
                writer.WritePropertyName(key);

                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }

    class ReadOnlyJsonMapConverter : JsonConverter<IReadOnlyJsonMap>
    {
        public override IReadOnlyJsonMap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyJsonMap jsonMap, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            jsonMap.Foreach(add);

            writer.WriteEndObject();

            void add(string key, object value)
            {
                writer.WritePropertyName(key);

                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }

    class StyleConverter : JsonConverter<Style>
    {
        public override Style Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Style value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            value.VisitNotNullValues(add);

            writer.WriteEndObject();

            void add(string propertyName, string propertyValue)
            {
                writer.WritePropertyName(propertyName);

                writer.WriteStringValue(propertyValue);
            }
        }
    }
}

[Serializable]
sealed class RemoteMethodInfo
{
    public string FunctionNameOfGrabEventArguments { get; set; }

    public int? HandlerComponentUniqueIdentifier { get; set; }

    [JsonPropertyName("$isRemoteMethod")]
    public bool IsRemoteMethod { get; set; }

    public string remoteMethodName { get; set; }

    public bool? StopPropagation { get; set; }
}

[Serializable]
public sealed class BindInfo
{
    public string DebounceHandler { get; set; }
    public int? DebounceTimeout { get; set; }
    public string eventName { get; set; }
    public int? HandlerComponentUniqueIdentifier { get; set; }

    [JsonPropertyName("$isBinding")]
    public bool IsBinding { get; set; }

    public string[] jsValueAccess { get; set; }
    public bool sourceIsState { get; set; }

    public IReadOnlyList<string> sourcePath { get; set; }

    public string targetProp { get; set; }
    public string transformFunction { get; set; }
}

public class InnerElementInfo
{
    public object Element { get; set; }

    [JsonPropertyName("$isElement")]
    public bool IsElement { get; set; }
}