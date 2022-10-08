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