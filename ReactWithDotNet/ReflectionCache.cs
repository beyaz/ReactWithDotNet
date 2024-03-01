using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;



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
    
    internal sealed class MethodInfoCalculated
    {
        public required bool HasStopPropagation { get; init; }

        public required IReadOnlyList<string> KeyboardEventCallOnly { get; init; }

        public required MethodInfo MethodInfo { get; init; }

        public required string NameWithToken { get; init; }

        public static MethodInfoCalculated From(MethodInfo methodInfo)
        {
            return new()
            {
                MethodInfo            = methodInfo,
                HasStopPropagation    = methodInfo.GetCustomAttributes<ReactStopPropagationAttribute>().Any(),
                KeyboardEventCallOnly = methodInfo.GetCustomAttributes<ReactKeyboardEventCallOnlyAttribute>().FirstOrDefault()?.Keys,
                NameWithToken         = methodInfo.GetNameWithToken()
            };
        }
    }
    
    
    
}


internal sealed class PropertyInfoCalculated
{
    public object DefaultValue { get; init; }
    public Func<object, object> GetValueFunc { get; init; }
    public bool HasReactAttribute { get; init; }
    public JsonPropertyNameAttribute JsonPropertyName { get; init; }
    public PropertyInfo PropertyInfo { get; init; }
    public bool PropertyTypeIsIsVoidTaskDelegate { get; init; }
    public Action<object, object> SetValueFunc { get; init; }
    public ReactTemplateAttribute TemplateAttribute { get; init; }
    public string TransformValueInClientFunction { get; init; }
    public Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse> TransformValueInServerSide { get; init; }
    public string FunctionNameOfGrabEventArguments { get; init; }
    public ReactBindAttribute ReactBindAttribute { get; init; }
    public string NameOfTransformValueInClient { get; init; }
}

internal sealed class TypeInfoCalculated
{
    public IReadOnlyList<MethodInfo> CacheableMethodInfoList { get; init; }
    public string ComponentDidMountMethod { get; init; }
    public IReadOnlyList<PropertyInfoCalculated> CustomEventPropertiesOfType { get; init; }
    public IReadOnlyList<PropertyInfoCalculated> DotNetPropertiesOfType { get; init; }
    public Func<object, string, (bool needToExport, object value)> GetPropertyValueForSerializeToClient { get; init; }
    public IReadOnlyList<MethodInfo> ParameterizedCacheableMethodInfoList { get; init; }
    public IReadOnlyList<PropertyInfoCalculated> ReactAttributedPropertiesOfType { get; init; }
    public PropertyInfoCalculated StateProperty { get; init; }
}