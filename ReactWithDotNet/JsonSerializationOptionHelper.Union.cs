using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

[Serializable]
public sealed class Union<A,B>
{
    internal readonly object value;
    
    public override string ToString()
    {
        return value?.ToString();
    }

    internal Union(object value)
    {
        this.value = value;
    }

    public static implicit operator Union<A,B>(A a)
    {
        return new Union<A, B>(a);
    }
    public static implicit operator Union<A,B>(B b)
    {
        return new Union<A, B>(b);
    }

    public static implicit operator A(Union<A, B> union)
    {
        return (A)Convert.ChangeType(union.value, typeof(A));
    }

    public static implicit operator B(Union<A, B> union)
    {
        return (B)Convert.ChangeType(union.value, typeof(B));
    }
}

partial class JsonSerializationOptionHelper
{
    class UnionConverter<T1, T2> : JsonConverter<Union<T1, T2>>
    {
        public override Union<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!reader.Read())
            {
                throw new JsonException();
            }

            return new Union<T1, T2>(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Union<T1, T2> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.value, options);
        }
    }

   
    class UnionFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert .IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Union<,>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var genericArguments = typeToConvert.GetGenericArguments();

            var converterType = genericArguments.Length switch
            {
                2 => typeof(UnionConverter<,>).MakeGenericType(genericArguments),

                // And add other cases as needed
                _ => throw new NotSupportedException()
            };
            return (JsonConverter)Activator.CreateInstance(converterType);
        }
    }
}