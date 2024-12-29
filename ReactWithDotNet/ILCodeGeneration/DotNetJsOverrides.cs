using System.Reflection;
using System;
using static ReactWithDotNet.NativeJsHelper;

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

class DefaultInterpolatedStringHandler
{
    readonly Array parts;
        
    public DefaultInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        parts = CreateNewArray();
    }
        
    public void AppendLiteral(string value) 
    {
        parts.push(value);
    }

    public void AppendFormatted(string formattedString) 
    {
        parts.push(formattedString);
    }

    public override string ToString()
    {
        return parts.join("");
    }

    public string ToStringAndClear()
    {
        return ToString();
    }
}