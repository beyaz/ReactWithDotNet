using System.Reflection;
using System.Reflection.Emit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReactWithDotNet;

static class ReflectionHelper
{
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
    
    
    public static bool IsVoidTaskFunc1Or2Or3(this Type type)
    {
        // todo: rename with more meaningfull

        if (type.BaseType == typeof(MulticastDelegate))
        {
            var invokeMethodInfo = type.GetMethod("Invoke");
            if (invokeMethodInfo is not null && 
                invokeMethodInfo.ReturnType == typeof(Task) &&
                invokeMethodInfo.GetParameters().Length <= 3)
            {
                return true;
            }
        }

        return false;
    }

    public static object ConvertNonEmptyOrNullStringValueToNumberType(string value, Type targetNumberType)
    {
        if (ReferenceEquals(targetNumberType,Type_bool))
        {
            return Convert.ToBoolean(value);
        }
        if (ReferenceEquals(targetNumberType,Type_char))
        {
            return Convert.ToChar(value);
        }
        if (ReferenceEquals(targetNumberType,Type_sbyte))
        {
            return Convert.ToSByte(value);
        }
        if (ReferenceEquals(targetNumberType,Type_byte))
        {
            return Convert.ToByte(value);
        }
        if (ReferenceEquals(targetNumberType,Type_short))
        {
            return Convert.ToInt16(value);
        }
        if (ReferenceEquals(targetNumberType,Type_ushort))
        {
            return Convert.ToUInt16(value);
        }
        if (ReferenceEquals(targetNumberType,Type_int))
        {
            return Convert.ToInt32(value);
        }
        if (ReferenceEquals(targetNumberType,Type_uint))
        {
            return Convert.ToUInt32(value);
        }
        if (ReferenceEquals(targetNumberType,Type_long))
        {
            return Convert.ToInt64(value);
        }
        if (ReferenceEquals(targetNumberType,Type_ulong))
        {
            return Convert.ToUInt16(value);
        }
        if (ReferenceEquals(targetNumberType,Type_float))
        {
            return Convert.ToSingle(value);
        }
        if (ReferenceEquals(targetNumberType,Type_double))
        {
            return Convert.ToDouble(value);
        }
        if (ReferenceEquals(targetNumberType,Type_decimal))
        {
            return Convert.ToDecimal(value);
        }
        if (ReferenceEquals(targetNumberType,Type_DateTime))
        {
            return Convert.ToDateTime(value);
        }

        throw new InvalidCastException($"{value} not casted to {targetNumberType}");
    }

    static readonly Type Type_bool = typeof(bool);
    static readonly Type Type_char = typeof(char);
    static readonly Type Type_sbyte = typeof(sbyte);
    static readonly Type Type_byte =typeof(byte);
    static readonly Type Type_short =typeof(short);
    static readonly Type Type_ushort =typeof(ushort);
    static readonly Type Type_int =typeof(int);
    static readonly Type Type_uint =typeof(uint);
    static readonly Type Type_long =typeof(long);
    static readonly Type Type_ulong =typeof(ulong);
    static readonly Type Type_float =typeof(float);
    static readonly Type Type_double =typeof(double);
    static readonly Type Type_decimal =typeof(decimal);
    static readonly Type Type_DateTime =typeof(DateTime);
}