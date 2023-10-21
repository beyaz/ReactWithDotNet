using System.Reflection;
using System.Reflection.Emit;
using Newtonsoft.Json;

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
        var json = JsonConvert.SerializeObject(value);

        return (T)JsonConvert.DeserializeObject(json, value.GetType());
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
}