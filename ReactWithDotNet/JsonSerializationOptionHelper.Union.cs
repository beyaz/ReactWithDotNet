using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

[Serializable]
public abstract class UnionPropBase
{
    internal readonly object value;
    
    internal UnionPropBase(object value)
    {
        this.value = value;
    }
    
    public override string ToString()
    {
        if (value is double d)
        {
            return d.AsString();
        }
        
        return value?.ToString();
    }
}

[Serializable]
public sealed class UnionProp<A, B> : UnionPropBase
{
    internal UnionProp(object value) : base(value) { }

    public static implicit operator UnionProp<A, B>(A a)
    {
        return new(a);
    }

    public static implicit operator UnionProp<A, B>(B b)
    {
        return new(b);
    }

    public static implicit operator A(UnionProp<A, B> union)
    {
        return (A)Convert.ChangeType(union.value, typeof(A));
    }

    public static implicit operator B(UnionProp<A, B> union)
    {
        return (B)Convert.ChangeType(union.value, typeof(B));
    }
}

[Serializable]
public sealed class UnionProp<A, B, C> : UnionPropBase
{
    internal UnionProp(object value): base(value) { }

    public static implicit operator UnionProp<A, B, C>(A a)
    {
        return new(a);
    }

    public static implicit operator UnionProp<A, B, C>(B b)
    {
        return new(b);
    }

    public static implicit operator UnionProp<A, B, C>(C c)
    {
        return new(c);
    }

    public static implicit operator A(UnionProp<A, B, C> union)
    {
        return (A)Convert.ChangeType(union.value, typeof(A));
    }

    public static implicit operator B(UnionProp<A, B, C> union)
    {
        return (B)Convert.ChangeType(union.value, typeof(B));
    }

    public static implicit operator C(UnionProp<A, B, C> union)
    {
        return (C)Convert.ChangeType(union.value, typeof(C));
    }
}

[Serializable]
public sealed class UnionProp<A, B, C, D>: UnionPropBase
{
    internal UnionProp(object value): base(value) { }

    public static implicit operator UnionProp<A, B, C, D>(A a)
    {
        return new(a);
    }

    public static implicit operator UnionProp<A, B, C, D>(B b)
    {
        return new(b);
    }

    public static implicit operator UnionProp<A, B, C, D>(C c)
    {
        return new(c);
    }

    public static implicit operator UnionProp<A, B, C, D>(D d)
    {
        return new(d);
    }

    public static implicit operator A(UnionProp<A, B, C, D> union)
    {
        return (A)Convert.ChangeType(union.value, typeof(A));
    }

    public static implicit operator B(UnionProp<A, B, C, D> union)
    {
        return (B)Convert.ChangeType(union.value, typeof(B));
    }

    public static implicit operator C(UnionProp<A, B, C, D> union)
    {
        return (C)Convert.ChangeType(union.value, typeof(C));
    }

    public static implicit operator D(UnionProp<A, B, C, D> union)
    {
        return (D)Convert.ChangeType(union.value, typeof(D));
    }
}

partial class JsonSerializationOptionHelper
{
    class UnionPropConverter<A, B> : JsonConverter<UnionProp<A, B>>
    {
        public override UnionProp<A, B> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, UnionProp<A, B> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.value, options);
        }
    }

    class UnionPropConverter<A, B, C> : JsonConverter<UnionProp<A, B, C>>
    {
        public override UnionProp<A, B, C> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, UnionProp<A, B, C> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.value, options);
        }
    }

    class UnionPropConverter<A, B, C, D> : JsonConverter<UnionProp<A, B, C, D>>
    {
        public override UnionProp<A, B, C, D> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, UnionProp<A, B, C, D> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.value, options);
        }
    }

    sealed class UnionPropFactory : JsonConverterFactory
    {
        static readonly Type TypeOfUnionTypeBase = typeof(UnionPropBase);
        
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.BaseType == TypeOfUnionTypeBase;
        }

        static readonly Type[] UnionPropConverterTypesCache =
        [
            null,
            null,
            typeof(UnionPropConverter<,>),
            typeof(UnionPropConverter<,,>),
            typeof(UnionPropConverter<,,,>)
        ];
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterTypes = UnionPropConverterTypesCache;
            
            var genericArguments = typeToConvert.GetGenericArguments();

            var genericArgumentsLength = genericArguments.Length;

            if (genericArgumentsLength > converterTypes.Length)
            {
                throw new NotSupportedException();
            }
            return (JsonConverter)Activator.CreateInstance(converterTypes[genericArgumentsLength].MakeGenericType(genericArguments));
        }
    }
}