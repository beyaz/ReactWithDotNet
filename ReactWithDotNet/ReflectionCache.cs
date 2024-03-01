using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class Mixin
{
    static readonly ConcurrentDictionary<Type, TypeInfoCalculated> CacheForTypeInfoCalculated = new();
    static readonly ConcurrentDictionary<Assembly, ConcurrentDictionary<MethodInfo, MethodInfoCalculated>> methodInfoCalculatedCache = new();

    internal static TypeInfoCalculated Calculated(this Type type)
    {
        if (CacheForTypeInfoCalculated.TryGetValue(type, out var typeInfo))
        {
            return typeInfo;
        }

        var isAnonymousType = type.IsAnonymousType();
        

        var serializableProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty);

        var reactCustomEventProperties = new List<PropertyInfoCalculated>();
        {
            foreach (var propertyInfo in serializableProperties.Where(x => x.GetCustomAttribute<ReactCustomEventAttribute>() is not null))
            {
                if (!propertyInfo.IsVoidTaskDelegate())
                {
                    throw DeveloperException($"Delegate should return 'Task'. @PropertyName is '{propertyInfo.Name}'");
                }

                reactCustomEventProperties.Add(propertyInfo.Calculate());
            }
        }

        var dotNetPropertiesOfType = new List<PropertyInfoCalculated>();
        {
            foreach (var propertyInfo in serializableProperties)
            {
                if (isAnonymousType)
                {
                    dotNetPropertiesOfType.Add(propertyInfo.Calculate());
                    continue;
                }
                
                if (propertyInfo.Name == nameof(ReactComponentBase.Context)
                    || propertyInfo.Name == nameof(Element.children)
                    || propertyInfo.Name == nameof(ReactComponentBase.key)
                    || propertyInfo.Name == nameof(ReactComponentBase.Client)
                    || propertyInfo.Name == "state"
                    || propertyInfo.PropertyType.IsSubclassOf(typeof(Delegate))
                    || propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>() is not null
                    || (propertyInfo.Name == "Item" && propertyInfo.GetIndexParameters().Length > 0)
                   )
                {
                    continue;
                }

                if (propertyInfo.PropertyType == typeof(Element) || propertyInfo.PropertyType.IsSubclassOf(typeof(Element)))
                {
                    continue;
                }

                if (propertyInfo.CanWrite == false && !propertyInfo.DeclaringType?.IsSubclassOf(typeof(ThirdPartyReactComponent)) == true)
                {
                    continue;
                }

                dotNetPropertiesOfType.Add(propertyInfo.Calculate());
            }
        }

        var reactProperties = new List<PropertyInfoCalculated>();
        {
            foreach (var propertyInfo in serializableProperties.Where(x => x.GetCustomAttribute<ReactPropAttribute>() != null))
            {
                reactProperties.Add(propertyInfo.Calculate());
            }
        }

        var getPropertyValueForSerializeToClientFunc =
            (Func<object, string, (bool needToExport, object value)>)
            type.GetMethod("GetPropertyValueForSerializeToClient", BindingFlags.NonPublic | BindingFlags.Static)
                ?.CreateDelegate(typeof(Func<object, string, (bool needToExport, object value)>));

        typeInfo = new()
        {
            CustomEventPropertiesOfType          = reactCustomEventProperties,
            DotNetPropertiesOfType               = dotNetPropertiesOfType,
            ReactAttributedPropertiesOfType      = reactProperties,
            CacheableMethodInfoList              = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodAttribute>() != null).ToArray(),
            ParameterizedCacheableMethodInfoList = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(m => m.GetCustomAttribute<CacheThisMethodByTheseParametersAttribute>() != null).ToArray(),
            StateProperty                        = type.GetProperty("state", BindingFlags.NonPublic | BindingFlags.Instance)?.Calculate(),

            GetPropertyValueForSerializeToClient = getPropertyValueForSerializeToClientFunc,

            ComponentDidMountMethod = GetComponentDidMountMethod(type),
            
            IsAnonymousType = isAnonymousType
        };

        CacheForTypeInfoCalculated.TryAdd(type, typeInfo);

        return typeInfo;

        static string GetComponentDidMountMethod(Type componentType)
        {
            var didMountMethodInfo = componentType.FindMethod("componentDidMount", BindingFlags.NonPublic | BindingFlags.Instance);
            if (didMountMethodInfo != null)
            {
                if (didMountMethodInfo.DeclaringType != typeof(ReactComponentBase))
                {
                    return didMountMethodInfo.GetNameWithToken();
                }
            }

            return null;
        }
    }

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

    static PropertyInfoCalculated Calculate(this PropertyInfo propertyInfo)
    {
        return new()
        {
            SetValueFunc                   = ReflectionHelper.CreateSetFunction(propertyInfo),
            GetValueFunc                   = ReflectionHelper.CreateGetFunction(propertyInfo),
            PropertyInfo                   = propertyInfo,
            DefaultValue                   = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null,
            HasReactAttribute              = propertyInfo.GetCustomAttribute<ReactPropAttribute>() is not null,
            TransformValueInServerSide     = getTransformValueInServerSideTransformFunction(propertyInfo),
            TemplateAttribute              = propertyInfo.GetCustomAttribute<ReactTemplateAttribute>(),
            JsonPropertyName               = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>(),
            TransformValueInClientFunction = TryGetTransformValueInClientFunctionName(propertyInfo),

            PropertyTypeIsIsVoidTaskDelegate = propertyInfo.IsVoidTaskDelegate(),

            ReactBindAttribute = propertyInfo.GetCustomAttribute<ReactBindAttribute>(),

            FunctionNameOfGrabEventArguments = propertyInfo.GetCustomAttribute<ReactGrabEventArgumentsByUsingFunctionAttribute>()?.TransformFunction,

            NameOfTransformValueInClient = propertyInfo.GetCustomAttribute<ReactTransformValueInClientAttribute>()?.TransformFunction
        };

        static Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse> getTransformValueInServerSideTransformFunction(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<ReactTransformValueInServerSideAttribute>();
            if (attribute == null)
            {
                return null;
            }

            var methodInfo = attribute.TransformMethodDeclaringType.GetMethod("Transform", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (methodInfo == null)
            {
                throw DeveloperException($"Type should have a static method named 'Transform'. @type:{attribute.TransformMethodDeclaringType}");
            }

            return (Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse>)methodInfo.CreateDelegate(typeof(Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse>));
        }

        static string TryGetTransformValueInClientFunctionName(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<ReactTransformValueInClientAttribute>()?.TransformFunction;
        }
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

sealed class PropertyInfoCalculated
{
    public object DefaultValue { get; init; }
    public string FunctionNameOfGrabEventArguments { get; init; }
    public Func<object, object> GetValueFunc { get; init; }
    public bool HasReactAttribute { get; init; }
    public JsonPropertyNameAttribute JsonPropertyName { get; init; }
    public string NameOfTransformValueInClient { get; init; }
    public PropertyInfo PropertyInfo { get; init; }
    public bool PropertyTypeIsIsVoidTaskDelegate { get; init; }
    public ReactBindAttribute ReactBindAttribute { get; init; }
    public Action<object, object> SetValueFunc { get; init; }
    public ReactTemplateAttribute TemplateAttribute { get; init; }
    public string TransformValueInClientFunction { get; init; }
    public Func<object, TransformValueInServerSideContext, TransformValueInServerSideResponse> TransformValueInServerSide { get; init; }
}

sealed class TypeInfoCalculated
{
    public IReadOnlyList<MethodInfo> CacheableMethodInfoList { get; init; }
    public string ComponentDidMountMethod { get; init; }
    public IReadOnlyList<PropertyInfoCalculated> CustomEventPropertiesOfType { get; init; }
    public IReadOnlyList<PropertyInfoCalculated> DotNetPropertiesOfType { get; init; }
    public Func<object, string, (bool needToExport, object value)> GetPropertyValueForSerializeToClient { get; init; }
    public IReadOnlyList<MethodInfo> ParameterizedCacheableMethodInfoList { get; init; }
    public IReadOnlyList<PropertyInfoCalculated> ReactAttributedPropertiesOfType { get; init; }
    public PropertyInfoCalculated StateProperty { get; init; }
    public bool IsAnonymousType { get; init; }
}