using System.Reflection;

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
}