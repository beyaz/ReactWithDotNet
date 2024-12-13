using System.Reflection;

namespace ReactWithDotNet;

static class DotNetJsOverrides
{
    public static System.Type GetTypeFromHandle(System.RuntimeTypeHandle value)
    {
        return value.AsObject().As<Type>();
    }

    public static string get_Name(MemberInfo memberInfo)
    {
        return memberInfo.Get("Name").As<string>();
    }
}