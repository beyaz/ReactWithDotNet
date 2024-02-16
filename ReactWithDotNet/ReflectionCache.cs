using System.Collections.Concurrent;
using System.Reflection;

namespace ReactWithDotNet;

sealed class MethodInfoCalculated
{
    // todo: cache more accessed values

    public required bool HasStopPropagationAttribute { get; init; }

    public required IReadOnlyList<string> KeyboardEventCallOnlyAttribute { get; init; }

    public required MethodInfo MethodInfo { get; init; }

    public required string NameWithToken { get; init; }

    public static MethodInfoCalculated From(MethodInfo methodInfo)
    {
        return new()
        {
            MethodInfo                     = methodInfo,
            HasStopPropagationAttribute    = methodInfo.GetCustomAttributes<ReactStopPropagationAttribute>().Any(),
            KeyboardEventCallOnlyAttribute = methodInfo.GetCustomAttributes<ReactKeyboardEventCallOnlyAttribute>().FirstOrDefault()?.Keys,
            NameWithToken                  = methodInfo.GetNameWithToken()
        };
    }
}

partial class Mixin
{
    static readonly ConcurrentDictionary<Assembly, ConcurrentDictionary<MethodInfo, MethodInfoCalculated>> methodInfoCalculatedCache = new();

    internal static MethodInfoCalculated GetCalculated(this MethodInfo methodInfo)
    {
        var assembly = methodInfo.Module.Assembly;

        if (!methodInfoCalculatedCache.TryGetValue(assembly, out var methodCache))
        {
            methodInfoCalculatedCache[assembly] = methodCache = new();
        }

        if (!methodCache.TryGetValue(methodInfo, out var methodInfoCalculated))
        {
            methodCache[methodInfo] = methodInfoCalculated = MethodInfoCalculated.From(methodInfo);
        }

        return methodInfoCalculated;
    }
}