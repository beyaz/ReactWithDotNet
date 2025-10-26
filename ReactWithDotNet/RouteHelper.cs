using System.Reflection;

namespace ReactWithDotNet;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class RouteAttribute : Attribute
{
    public string Url { get; }

    public RouteAttribute(string url)
    {
        Url = url;
    }
}

public sealed class RouteInfo
{
    public string Url { get; init; }

    public Type Page { get; init; }
}

public static class RouteHelper
{
    public static IReadOnlyDictionary<string, RouteInfo> GetRoutesFrom(params IReadOnlyList<Assembly> assemblies)
    {
        var routes =
        (
            from assembly in assemblies
            from type in assembly.GetTypes()
            from route in type.GetCustomAttributes<RouteAttribute>()
            select new { Template = route.Url, type }
        )
        .ToDictionary(x => x.Template, x => new RouteInfo
        {
            Url = x.Template, 
            Page = x.type
        }, StringComparer.OrdinalIgnoreCase);
        
        return routes;
    }
}