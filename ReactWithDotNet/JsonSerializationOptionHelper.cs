using System.Collections.Immutable;
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
            if (jsonElement.ValueKind == JsonValueKind.String)
            {
                var stringValue = jsonElement.GetString();

                if (targetType == typeof(string))
                {
                    return stringValue;
                }

                if (targetType == typeof(Type))
                {
                    return JsonConverterFactoryForType.DeserializeType(stringValue);
                }

                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    if (targetType.IsClass)
                    {
                        return null;
                    }

                    return Activator.CreateInstance(targetType);
                }

                if (targetType.IsEnum)
                {
                    return Enum.Parse(targetType, stringValue);
                }

                return ReflectionHelper.ConvertNonEmptyOrNullStringValueToPrimitiveType(stringValue, targetType);
            }

            if (jsonElement.ValueKind == JsonValueKind.Number )
            {
                if (targetType.IsEnum)
                {
                    return Enum.ToObject(targetType, long.Parse(jsonElement.GetRawText()));
                }
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
                
                if (jsonElement.TryGetProperty("$type", out var typeProperty))
                {
                    var typeName = typeProperty.GetString();
                    if (!string.IsNullOrWhiteSpace(typeName))
                    {
                        var type = Type.GetType(typeName);
                        if (type is not null)
                        {
                            return jsonElement.Deserialize(type, JsonSerializerOptionsInstance);
                        }
                    }
                }

                return jsonElement.Deserialize(targetType, JsonSerializerOptionsInstance);
            }

            if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                var genericArguments = targetType.GetGenericArguments();

                if (targetType.IsGenericType && genericArguments[0] == typeof(object))
                {
                    var objectArray = jsonElement.Deserialize<object[]>(JsonSerializerOptionsInstance);

                    for (var i = 0; i < objectArray.Length; i++)
                    {
                        if (objectArray[i] is JsonElement itemAsJsonElement)
                        {
                            if (itemAsJsonElement.TryGetProperty("$type", out var typeProperty))
                            {
                                var typeName = typeProperty.GetString();
                                if (!string.IsNullOrWhiteSpace(typeName))
                                {
                                    var type = Type.GetType(typeName);
                                    if (type is not null)
                                    {
                                        objectArray[i]  = itemAsJsonElement.Deserialize(type, JsonSerializerOptionsInstance);
                                    }
                                }
                            }
                        }
                    }

                    if (targetType == typeof(IReadOnlyList<object>)||
                        targetType == typeof(IList<object>)||
                        targetType == typeof(ICollection<object>)||
                        targetType == typeof(IEnumerable<object>)||
                        targetType == typeof(object[]))
                    {
                        return objectArray;
                    }

                    if (targetType == typeof(List<object>))
                    {
                        return objectArray.ToList();
                    }
                    
                    if (targetType == typeof(ImmutableList<object>))
                    {
                        return objectArray.ToImmutableList();
                    }
                    if (targetType == typeof(ImmutableArray<object>))
                    {
                        return objectArray.ToImmutableArray();
                    }
                }
                
                return jsonElement.Deserialize(targetType, JsonSerializerOptionsInstance);
            }

            throw new();
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
        options.WriteIndented  = false;
        options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        options.PropertyNamingPolicy = null;

        options.Converters.Add(new StyleConverter());

        options.Converters.Add(new JsMapConverter());

        options.Converters.Add(new ReadOnlyJsonMapConverter());

        options.Converters.Add(new JsonStringEnumConverter());

        options.Converters.Add(new ValueTupleFactory());

        options.Converters.Add(new JsonConverterFactoryForType());

        options.Converters.Add(new UnionPropFactory());

        options.Converters.Add(AnonymousTypeJsonConverter.Instance);
        
        options.Converters.Add(new JsonConverterFactoryForCompilerGeneratedClass());

        options.Converters.Add(JsonConverterFactoryForDelegate.Instance);
        
        options.Converters.Add(JsonConverterFactoryForCommounUsage.Instance);
        
        options.Converters.Add(new JsonConverterFactoryForNullableNumbers());

        return options;
    }
    
    // todo: check performance
    sealed class JsonConverterFactoryForNullableNumbers : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var genericArgument = typeToConvert.GetGenericArguments()[0];
                
                switch (Type.GetTypeCode(genericArgument))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Single:
                        return true;
                    default:
                        return false;
                }
            }
            
            return false;
            
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new Converter();
        }

        class Converter : JsonConverter<object>
        {
            public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if(reader.TokenType == JsonTokenType.String) 
                {
                    var str = reader.GetString();
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        return null;
                    }
                }
                
                return JsonSerializer.Deserialize(ref reader, typeToConvert.GetGenericArguments()[0], options);
            }

            public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
            {
                if (value is null)
                {
                    writer.WriteNullValue();
                    return;
                }

                JsonSerializer.Serialize(writer, value, options);
                
            }
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
                return DeserializeType(reader.GetString());
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

    class JsMapConverter : JsonConverter<JsonMap>
    {
        public override JsonMap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, JsonMap jsonMap, JsonSerializerOptions options)
        {
            jsonMap.Write(writer, options);
        }
    }

    sealed class AnonymousTypeJsonConverter : JsonConverter<dynamic>
    {
        public static readonly AnonymousTypeJsonConverter Instance = new ();
    
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAnonymousType();
        }

        public override dynamic Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, dynamic value, JsonSerializerOptions options)
        {
            Type type = value.GetType();
        
            writer.WriteStartObject();

            foreach (var property in type.Calculated().DotNetPropertiesOfType)
            {
                writer.WritePropertyName(property.PropertyInfo.Name);
                
                JsonSerializer.Serialize(writer, property.GetValueFunc(value), options);
            }
        
            writer.WriteEndObject();
        }
    }
    
    sealed class JsonConverterFactoryForCompilerGeneratedClass : JsonConverterFactory
    {
        static readonly ConverterForCompilerGeneratedClass ConverterForCompilerGeneratedClassInstance = new();

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsCompilerGenerated();
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return ConverterForCompilerGeneratedClassInstance;
        }

        class ConverterForCompilerGeneratedClass : JsonConverter<object>
        {
            public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);

                if (dictionary is null)
                {
                    return null;
                }

                return SerializationHelperForCompilerGeneratedClasss.Deserialize(typeToConvert, dictionary);
            }

            public override void Write(Utf8JsonWriter writer, object obj, JsonSerializerOptions options)
            {
                if (obj is null)
                {
                    writer.WriteNullValue();
                    return;
                }

                var dictionary = SerializationHelperForCompilerGeneratedClasss.Serialize(obj);

                JsonSerializer.Serialize(writer, dictionary, options);
            }
        }
    }

    sealed class JsonConverterFactoryForDelegate : JsonConverterFactory
    {
        internal static readonly JsonConverterFactoryForDelegate Instance = new();

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.BaseType == typeof(MulticastDelegate);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return ConverterForDelegate.ConverterInstance;
        }

        class ConverterForDelegate : JsonConverter<object>
        {
            internal static readonly ConverterForDelegate ConverterInstance = new();

            public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                object obj = null;

                MethodInfo methodInfo = null;

                Type targeType = null;

                Type delegateType = null;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return obj;
                    }

                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new JsonException();
                    }

                    var name = reader.GetString();
                    if (name == null)
                    {
                        throw new JsonException();
                    }

                    if (name == "Type")
                    {
                        reader.Read();

                        var typeName = reader.GetString();
                        if (typeName == null)
                        {
                            throw new JsonException($"DelegateTypeName should have value for deserializing {typeToConvert.FullName}");
                        }

                        delegateType = Type.GetType(typeName);
                        if (delegateType == null)
                        {
                            throw new JsonException($"{typeName} not found when  deserializing {typeToConvert.FullName}");
                        }

                        continue;
                    }

                    if (name == "Method")
                    {
                        reader.Read();

                        var methodName = reader.GetString();

                        MethodAccess.TryResolveMethodInfo(methodName, ref methodInfo);

                        if (methodInfo == null)
                        {
                            throw new JsonException($"{methodName} not resolved when  deserializing {typeToConvert.FullName}");
                        }

                        continue;
                    }

                    if (name == "TargetType")
                    {
                        reader.Read();

                        var typeName = reader.GetString();

                        if (typeName == null)
                        {
                            throw new JsonException($"DelegateTypeName should have value for deserializing {typeToConvert.FullName}");
                        }

                        targeType = Type.GetType(typeName);

                        if (targeType == null)
                        {
                            throw new JsonException($"{typeName} not found when  deserializing {typeToConvert.FullName}");
                        }

                        continue;
                    }

                    if (name == "Target")
                    {
                        reader.Read();

                        if (targeType == null)
                        {
                            throw new JsonException($"targeType should have value. when  deserializing {typeToConvert.FullName}");
                        }

                        if (delegateType == null)
                        {
                            throw new JsonException($"delegateType should have value. when  deserializing {typeToConvert.FullName}");
                        }

                        if (methodInfo == null)
                        {
                            throw new JsonException($"methodInfo should have value. when  deserializing {typeToConvert.FullName}");
                        }

                        var target = JsonSerializer.Deserialize(ref reader, targeType, options);

                        obj = Delegate.CreateDelegate(delegateType, target, methodInfo);
                    }
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, object obj, JsonSerializerOptions options)
            {
                if (obj is null)
                {
                    writer.WriteNullValue();
                    return;
                }

                writer.WriteStartObject();

                var @delegate = (Delegate)obj;

                writer.WritePropertyName("Type");
                JsonSerializer.Serialize(writer, obj.GetType().FullName, options);

                writer.WritePropertyName("Method");
                JsonSerializer.Serialize(writer, @delegate.Method.GetAccessKey(), options);

                var target = @delegate.Target;
                if (target is not null)
                {
                    writer.WritePropertyName("TargetType");
                    JsonSerializer.Serialize(writer, target.GetType().SerializeToString(), options);

                    writer.WritePropertyName("Target");
                    JsonSerializer.Serialize(writer, target, options);
                }

                writer.WriteEndObject();
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
            jsonMap.Write(writer, options);
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
            value.Write(writer);
        }
    }
}

[Serializable]
sealed class RemoteMethodInfo
{
    public string FunctionNameOfGrabEventArguments { get; set; }

    public int? HandlerComponentUniqueIdentifier { get; set; }
    public int? HtmlElementScrollDebounceTimeout { get; set; }

    [JsonPropertyName("$isRemoteMethod")]
    public bool IsRemoteMethod { get; set; }

    public IReadOnlyList<string> KeyboardEventCallOnly { get; set; }

    public string remoteMethodName { get; set; }

    public bool? StopPropagation { get; set; }
    
    public int? DebounceTimeout { get; init; }
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