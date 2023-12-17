using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

[Serializable]
public sealed class UnionProp<A, B>
{
    internal readonly object value;

    internal UnionProp(object value)
    {
        this.value = value;
    }

    public static implicit operator UnionProp<A, B>(A a)
    {
        return new UnionProp<A, B>(a);
    }

    public static implicit operator UnionProp<A, B>(B b)
    {
        return new UnionProp<A, B>(b);
    }

    public static implicit operator A(UnionProp<A, B> union)
    {
        return (A)Convert.ChangeType(union.value, typeof(A));
    }

    public static implicit operator B(UnionProp<A, B> union)
    {
        return (B)Convert.ChangeType(union.value, typeof(B));
    }

    public override string ToString()
    {
        return value?.ToString();
    }
}

[Serializable]
public sealed class UnionProp<A, B, C>
{
    internal readonly object value;

    internal UnionProp(object value)
    {
        this.value = value;
    }

    public static implicit operator UnionProp<A, B, C>(A a)
    {
        return new UnionProp<A, B, C>(a);
    }

    public static implicit operator UnionProp<A, B, C>(B b)
    {
        return new UnionProp<A, B, C>(b);
    }

    public static implicit operator UnionProp<A, B, C>(C c)
    {
        return new UnionProp<A, B, C>(c);
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

    public override string ToString()
    {
        return value?.ToString();
    }
}

[Serializable]
public sealed class UnionProp<A, B, C, D>
{
    internal readonly object value;

    internal UnionProp(object value)
    {
        this.value = value;
    }

    public static implicit operator UnionProp<A, B, C, D>(A a)
    {
        return new UnionProp<A, B, C, D>(a);
    }

    public static implicit operator UnionProp<A, B, C, D>(B b)
    {
        return new UnionProp<A, B, C, D>(b);
    }

    public static implicit operator UnionProp<A, B, C, D>(C c)
    {
        return new UnionProp<A, B, C, D>(c);
    }

    public static implicit operator UnionProp<A, B, C, D>(D d)
    {
        return new UnionProp<A, B, C, D>(d);
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

    public override string ToString()
    {
        return value?.ToString();
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

    class UnionPropFactory : JsonConverterFactory
    {
        static readonly Type[] UnionPropTypesCache =
        [
            typeof(UnionProp<,>),
            typeof(UnionProp<,,>),
            typeof(UnionProp<,,,>)
        ];
        
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType is false)
            {
                return false;
            }

            var genericTypeDefinition = typeToConvert.GetGenericTypeDefinition();

            return Array.IndexOf(UnionPropTypesCache, genericTypeDefinition) >= 0;
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