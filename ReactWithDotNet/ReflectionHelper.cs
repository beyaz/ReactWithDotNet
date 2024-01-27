using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static ReactWithDotNet.ReflectionHelper.PrimitiveTypeCache;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReactWithDotNet;

static class ReflectionHelper
{
    public static object ConvertNonEmptyOrNullStringValueToPrimitiveType(string value, Type primitiveType)
    {
        if (primitiveType.Name == "Nullable`1" && primitiveType.Namespace == "System")
        {
            primitiveType = primitiveType.GenericTypeArguments[0];
        }

        if (ReferenceEquals(primitiveType, Type_bool))
        {
            return Convert.ToBoolean(value);
        }

        if (ReferenceEquals(primitiveType, Type_char))
        {
            return Convert.ToChar(value);
        }

        if (ReferenceEquals(primitiveType, Type_sbyte))
        {
            return Convert.ToSByte(value);
        }

        if (ReferenceEquals(primitiveType, Type_byte))
        {
            return Convert.ToByte(value);
        }

        if (ReferenceEquals(primitiveType, Type_short))
        {
            return Convert.ToInt16(value);
        }

        if (ReferenceEquals(primitiveType, Type_ushort))
        {
            return Convert.ToUInt16(value);
        }

        if (ReferenceEquals(primitiveType, Type_int))
        {
            return Convert.ToInt32(value);
        }

        if (ReferenceEquals(primitiveType, Type_uint))
        {
            return Convert.ToUInt32(value);
        }

        if (ReferenceEquals(primitiveType, Type_long))
        {
            return Convert.ToInt64(value);
        }

        if (ReferenceEquals(primitiveType, Type_ulong))
        {
            return Convert.ToUInt16(value);
        }

        if (ReferenceEquals(primitiveType, Type_float))
        {
            return Convert.ToSingle(value);
        }

        if (ReferenceEquals(primitiveType, Type_double))
        {
            return Convert.ToDouble(value);
        }

        if (ReferenceEquals(primitiveType, Type_decimal))
        {
            return Convert.ToDecimal(value);
        }

        if (ReferenceEquals(primitiveType, Type_DateTime))
        {
            return Convert.ToDateTime(value);
        }

        throw new InvalidCastException($"{value} not casted to {primitiveType}");
    }

    public static Func<object, object> CreateGetFunction(PropertyInfo propertyInfo)
    {
        var getMethod = propertyInfo.GetMethod;
        if (getMethod == null)
        {
            return null;
        }

        var declaringType = propertyInfo.DeclaringType;
        if (declaringType == null)
        {
            return null;
        }

        var propertyName = propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name;

        var dmGet = new DynamicMethod("Get_" + propertyName, typeof(object), new[] { typeof(object) });

        var ilGenerator = dmGet.GetILGenerator();

        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Castclass, declaringType);
        ilGenerator.Emit(OpCodes.Callvirt, getMethod);

        if (propertyInfo.PropertyType.IsValueType)
        {
            ilGenerator.Emit(OpCodes.Box, propertyInfo.PropertyType);
        }

        ilGenerator.Emit(OpCodes.Ret);

        return (Func<object, object>)dmGet.CreateDelegate(typeof(Func<object, object>));
    }

    public static Action<object, object> CreateSetFunction(PropertyInfo propertyInfo)
    {
        var setMethod = propertyInfo.SetMethod;
        if (setMethod == null)
        {
            return null;
        }

        var declaringType = propertyInfo.DeclaringType;
        if (declaringType == null)
        {
            return null;
        }

        var propertyName = propertyInfo.DeclaringType?.FullName + "::" + propertyInfo.Name;

        var dmGet = new DynamicMethod("Set_" + propertyName, typeof(void), new[] { typeof(object), typeof(object) });

        var ilGenerator = dmGet.GetILGenerator();

        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Castclass, declaringType);
        ilGenerator.Emit(OpCodes.Ldarg_1);
        ilGenerator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
        ilGenerator.Emit(OpCodes.Callvirt, setMethod);
        ilGenerator.Emit(OpCodes.Ret);

        return (Action<object, object>)dmGet.CreateDelegate(typeof(Action<object, object>));
    }

    public static T DeepCopy<T>(T value)
    {
        var json = JsonSerializer.Serialize(value, JsonSerializerOptionsInstance);

        return (T)JsonSerializer.Deserialize(json, value.GetType(), JsonSerializerOptionsInstance);
    }

    public static MethodInfo FindMethod(this Type type, string methodName, BindingFlags bindingFlags)
    {
        while (type != null)
        {
            var methodInfo = type.GetMethod(methodName, bindingFlags);
            if (methodInfo != null)
            {
                return methodInfo;
            }

            type = type.BaseType;
        }

        return null;
    }

    public static MethodInfo FindMethodOrGetProperty(this Type type, string propertyNameOrMethodName, BindingFlags bindingFlags)
    {
        while (type != null)
        {
            var propertyInfo = type.GetProperty(propertyNameOrMethodName, bindingFlags);
            if (propertyInfo != null)
            {
                return propertyInfo.GetMethod;
            }

            var methodInfo = type.GetMethod(propertyNameOrMethodName, bindingFlags);
            if (methodInfo != null)
            {
                return methodInfo;
            }

            type = type.BaseType;
        }

        return null;
    }

    public static PropertyInfo FindProperty(this Type type, string propertyName, BindingFlags bindingFlags)
    {
        while (type != null)
        {
            var propertyInfo = type.GetProperty(propertyName, bindingFlags);
            if (propertyInfo != null)
            {
                return propertyInfo;
            }

            type = type.BaseType;
        }

        return null;
    }

    public static bool IsCompilerGenerated(this Type type)
    {
        return Attribute.GetCustomAttribute(type, typeof(CompilerGeneratedAttribute)) != null;
    }

    public static bool IsVoidTaskDelegate(this PropertyInfo propertyInfo)
    {
        return propertyInfo.PropertyType.GetMethod("Invoke")?.ReturnType.FullName == "System.Threading.Tasks.Task";
    }

    internal static class PrimitiveTypeCache
    {
        public static readonly Type Type_bool = typeof(bool);
        public static readonly Type Type_byte = typeof(byte);
        public static readonly Type Type_char = typeof(char);
        public static readonly Type Type_DateTime = typeof(DateTime);
        public static readonly Type Type_decimal = typeof(decimal);
        public static readonly Type Type_double = typeof(double);
        public static readonly Type Type_float = typeof(float);
        public static readonly Type Type_int = typeof(int);
        public static readonly Type Type_long = typeof(long);
        public static readonly Type Type_sbyte = typeof(sbyte);
        public static readonly Type Type_short = typeof(short);
        public static readonly Type Type_uint = typeof(uint);
        public static readonly Type Type_ulong = typeof(ulong);
        public static readonly Type Type_ushort = typeof(ushort);
    }
}