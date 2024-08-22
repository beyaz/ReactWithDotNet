

namespace ReactWithDotNet;

[Serializable]
public class ReactPropAttribute : Attribute;

[Serializable]
public class ReactBindAttribute : Attribute
{
    public string eventName { get; set; }
    public string jsValueAccess { get; set; }
    public string targetProp { get; set; }
}



[Serializable]
public sealed class ReactTemplateAttribute : Attribute
{
    public ReactTemplateAttribute(string methodNameForGettingItemsSource)
    {
        MethodNameForGettingItemsSource = methodNameForGettingItemsSource ?? throw new ArgumentNullException(nameof(methodNameForGettingItemsSource));
    }

    public string MethodNameForGettingItemsSource { get; }
}

[Serializable]
public sealed class ReactTemplateForNullAttribute : Attribute;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ReactRealTypeAttribute : Attribute
{
    public ReactRealTypeAttribute(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}

[AttributeUsage(AttributeTargets.Property)]
public sealed class ReactTransformValueInClientAttribute : Attribute
{
    public ReactTransformValueInClientAttribute(string transformFunction)
    {
        TransformFunction = transformFunction;
    }

    public string TransformFunction { get; }
}

[AttributeUsage(AttributeTargets.Property)]
public sealed class ReactGrabEventArgumentsByUsingFunctionAttribute : Attribute
{
    public ReactGrabEventArgumentsByUsingFunctionAttribute(string transformFunction)
    {
        TransformFunction = transformFunction;
    }

    public string TransformFunction { get; }
}



[AttributeUsage(AttributeTargets.Method)]
public sealed class CacheThisMethodByTheseParametersAttribute : Attribute
{
    public CacheThisMethodByTheseParametersAttribute(string nameofMethodForGettingParameters)
    {
        NameofMethodForGettingParameters = nameofMethodForGettingParameters;
    }

    public string NameofMethodForGettingParameters { get; }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class CacheThisMethodAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ReactTransformValueInServerSideAttribute : Attribute
{
    public ReactTransformValueInServerSideAttribute(Type transformMethodDeclaringType)
    {
        TransformMethodDeclaringType = transformMethodDeclaringType ?? throw new ArgumentNullException(nameof(transformMethodDeclaringType));
    }

    public Type TransformMethodDeclaringType { get; }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class KeyboardEventCallOnlyAttribute : Attribute
{
    public KeyboardEventCallOnlyAttribute(params string[] keys)
    {
        Keys = keys ?? throw new ArgumentNullException(nameof(keys));
    }

    public IReadOnlyList<string> Keys { get; }
}

[Serializable]
[AttributeUsage(AttributeTargets.Method)]
public sealed class StopPropagationAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property)]
public sealed class CustomEventAttribute : Attribute;


[AttributeUsage(AttributeTargets.Method)]
public sealed class DebounceTimeoutAttribute: Attribute
{
    public DebounceTimeoutAttribute(int millisecond)
    {
        Millisecond = millisecond;
    }

    public int Millisecond { get; }
}


/// <summary>
///     Skips call c# render method of component.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class SkipRenderAttribute: Attribute;