using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

[Serializable]
public sealed class UnionProp<A,B>
{
    internal readonly object value;
    
    internal UnionProp(object value)
    {
        this.value = value;
    }
    
    public override string ToString()
    {
        return value?.ToString();
    }

    public static implicit operator UnionProp<A,B>(A a)
    {
        return new UnionProp<A,B>(a);
    }
    public static implicit operator UnionProp<A,B>(B b)
    {
        return new UnionProp<A,B>(b);
    }

    public static implicit operator A(UnionProp<A,B> union)
    {
        return (A)Convert.ChangeType(union.value, typeof(A));
    }

    public static implicit operator B(UnionProp<A,B> union)
    {
        return (B)Convert.ChangeType(union.value, typeof(B));
    }
}
[Serializable]
public sealed class UnionProp<A,B,C>
{
    internal readonly object value;
    
    internal UnionProp(object value)
    {
        this.value = value;
    }
    
    public override string ToString()
    {
        return value?.ToString();
    }

    public static implicit operator UnionProp<A,B,C>(A a)
    {
        return new UnionProp<A,B,C>(a);
    }
    public static implicit operator UnionProp<A,B,C>(B b)
    {
        return new UnionProp<A,B,C>(b);
    }
    public static implicit operator UnionProp<A,B,C>(C c)
    {
        return new UnionProp<A,B,C>(c);
    }

    public static implicit operator A(UnionProp<A,B,C> union)
    {
        return (A)Convert.ChangeType(union.value, typeof(A));
    }

    public static implicit operator B(UnionProp<A,B,C> union)
    {
        return (B)Convert.ChangeType(union.value, typeof(B));
    }
    public static implicit operator C(UnionProp<A,B,C> union)
    {
        return (C)Convert.ChangeType(union.value, typeof(C));
    }
}
[Serializable]
public sealed class UnionProp<A,B,C,D>
{
    internal readonly object value;
    
    internal UnionProp(object value)
    {
        this.value = value;
    }
    
    public override string ToString()
    {
        return value?.ToString();
    }

    public static implicit operator UnionProp<A,B,C,D>(A a)
    {
        return new UnionProp<A,B,C,D>(a);
    }
    public static implicit operator UnionProp<A,B,C,D>(B b)
    {
        return new UnionProp<A,B,C,D>(b);
    }
    public static implicit operator UnionProp<A,B,C,D>(C c)
    {
        return new UnionProp<A,B,C,D>(c);
    }
    public static implicit operator UnionProp<A,B,C,D>(D d)
    {
        return new UnionProp<A,B,C,D>(d);
    }

    public static implicit operator A(UnionProp<A,B,C,D> union)
    {
        return (A)Convert.ChangeType(union.value, typeof(A));
    }

    public static implicit operator B(UnionProp<A,B,C,D> union)
    {
        return (B)Convert.ChangeType(union.value, typeof(B));
    }
    public static implicit operator C(UnionProp<A,B,C,D> union)
    {
        return (C)Convert.ChangeType(union.value, typeof(C));
    }
    public static implicit operator D(UnionProp<A,B,C,D> union)
    {
        return (D)Convert.ChangeType(union.value, typeof(D));
    }
}


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
    
    class UnionPropFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType is false)
            {
                return false;
            }

            var genericTypeDefinition = typeToConvert.GetGenericTypeDefinition();


            return genericTypeDefinition == typeof(UnionProp<,>) ||
                   genericTypeDefinition == typeof(UnionProp<,,>) ||
                   genericTypeDefinition == typeof(UnionProp<,,,>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var genericArguments = typeToConvert.GetGenericArguments();

            var converterType = genericArguments.Length switch
            {
                2 => typeof(UnionPropConverter<,>).MakeGenericType(genericArguments),
                3 => typeof(UnionPropConverter<,,>).MakeGenericType(genericArguments),
                4 => typeof(UnionPropConverter<,,,>).MakeGenericType(genericArguments),
                _ => throw new NotSupportedException()
            };
            return (JsonConverter)Activator.CreateInstance(converterType);
        }
    }
    
    class UnionPropConverter<A,B> : JsonConverter<UnionProp<A,B>>
    {
        public override UnionProp<A,B> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, UnionProp<A,B> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.value, options);
        }
    }
    class UnionPropConverter<A,B,C> : JsonConverter<UnionProp<A,B,C>>
    {
        public override UnionProp<A,B,C> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, UnionProp<A,B,C> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.value, options);
        }
    }
    class UnionPropConverter<A,B,C,D> : JsonConverter<UnionProp<A,B,C,D>>
    {
        public override UnionProp<A,B,C,D> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, UnionProp<A,B,C,D> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.value, options);
        }
    }
    
    
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