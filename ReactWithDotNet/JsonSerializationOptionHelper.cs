using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

static partial class JsonSerializationOptionHelper
{
    #region Public Methods
    public static JsonSerializerOptions Modify(JsonSerializerOptions options)
    {
        options.WriteIndented    = true;
        options.IgnoreNullValues = true;

        options.PropertyNamingPolicy = null;

        options.Converters.Add(new JsonConverterForEnum());

        options.Converters.Add(new StyleConverter());

        options.Converters.Add(new JsMapConverter());

        options.Converters.Add(new IReadOnlyJsonMapConverter());

        options.Converters.Add(new ValueTupleFactory());

        return options;
    }
    #endregion

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

    class IReadOnlyJsonMapConverter : JsonConverter<IReadOnlyJsonMap>
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
    #region Public Properties
    [JsonPropertyName("$isRemoteMethod")]
    public bool IsRemoteMethod { get; set; }

    public string remoteMethodName { get; set; }

    public int? HandlerComponentUniqueIdentifier { get; set; }

    public string FunctionNameOfGrabEventArguments { get; set; }

    public bool? StopPropagation { get; set; }
    #endregion
}

[Serializable]
public sealed class BindInfo
{
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