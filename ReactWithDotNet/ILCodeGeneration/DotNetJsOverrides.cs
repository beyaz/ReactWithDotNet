namespace ReactWithDotNet;

static class DotNetJsOverrides
{
    public static System.Type GetTypeFromHandle(System.RuntimeTypeHandle value)
    {
        return value.As<Type>();
    }
}