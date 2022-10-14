using System.Reflection;
using Newtonsoft.Json;

namespace ReactWithDotNet;

static class ReflectionHelper
{
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

    public static T DeepCopy<T>(T value)
    {
        var json = JsonConvert.SerializeObject(value);

        return (T)JsonConvert.DeserializeObject(json,value.GetType());
    }

    public static bool IsGenericAction1or2or3(this Type type)
    {
        var typeDefinition = type.GetGenericTypeDefinition();
        
        return typeDefinition == typeof(Action<>) || typeDefinition == typeof(Action<,>)|| typeDefinition == typeof(Action<,,>);
    }
}