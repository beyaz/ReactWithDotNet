using System.Reflection;
using System.Runtime.CompilerServices;
using static ReactWithDotNet.NativeJsHelper;

namespace ReactWithDotNet;

static class DotNetJsOverrides
{
    public static Type GetTypeFromHandle(RuntimeTypeHandle value)
    {
        return value.AsObject().As<Type>();
    }

    public static string get_Name(MemberInfo memberInfo)
    {
        return memberInfo.Get("Name").As<string>();
    }


    #region DefaultInterpolatedStringHandler
    
    public static DefaultInterpolatedStringHandler ctor(DefaultInterpolatedStringHandler instance, int literalLength, int formattedCount)
    {
        instance.RefStructToObject().Set("parts", CreateNewArray());

        return instance;
    }

    static string[] GetParts(this DefaultInterpolatedStringHandler instance)
    {
        return instance.RefStructToObject().Get("parts").As<string[]>();
    }
    
    public static void AppendLiteral(DefaultInterpolatedStringHandler instance, string value)
    {
        instance.GetParts().push(value);
    }
    
    public static void AppendFormatted<T>(DefaultInterpolatedStringHandler instance, T value) 
    {
        instance.GetParts().push(value.ToString());
    }
    
    public static void AppendFormatted(DefaultInterpolatedStringHandler instance, string formattedString) 
    {
        instance.GetParts().push(formattedString);
    }
    
    #endregion
}