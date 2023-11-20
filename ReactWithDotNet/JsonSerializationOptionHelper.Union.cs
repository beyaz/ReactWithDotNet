using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

[Serializable]
[DebuggerDisplay("{value}")]
public sealed class UnionStringOrInt32
{
    internal readonly object value;
    
    public override string ToString()
    {
        return value?.ToString();
    }


    //public override int GetHashCode()
    //{

    //    return value?.GetHashCode();
    //}

    //static bool Equals<T1,T2>(Nullable<T> n1, Nullable<T> n2) where T : struct
    //{
    //    if (n1.HasValue)
    //    {
    //        if (n2.HasValue) return EqualityComparer<T>.Default.Equals(n1.value, n2.value);
    //        return false;
    //    }
    //    if (n2.HasValue) return false;
    //    return true;
    //}

    //public override bool Equals(object obj)
    //{
    //    if (obj is null)
    //    {
    //        if (value is null)
    //        {
    //            return true;
    //        }

    //        return false;
    //    }

    //    if (value is null)
    //    {
    //        return false;
    //    }
    //    if (obj is Union<A, B> union)
    //    {
    //        return union.value?.Equals(value) == true;
    //    }

    //    return value?.Equals(obj) == true;
    //}

    internal UnionStringOrInt32(object value)
    {
        this.value = value;
    }

    public static implicit operator UnionStringOrInt32(string a)
    {
        return new UnionStringOrInt32(a);
    }
    public static implicit operator UnionStringOrInt32(int b)
    {
        return new UnionStringOrInt32(b);
    }

    public static implicit operator string(UnionStringOrInt32 union)
    {
        return union.value?.ToString();
    }

    public static implicit operator int(UnionStringOrInt32 union)
    {
        return Convert.ToInt32(union.value);
    }
}

partial class JsonSerializationOptionHelper
{
    class UnionStringOrInt32Converter : JsonConverter<UnionStringOrInt32>
    {
        public override UnionStringOrInt32 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }
                
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32();
            }
            
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, UnionStringOrInt32 union, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, union.value, options);
        }
    }
}