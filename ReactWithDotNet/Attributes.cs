namespace ReactWithDotNet;

[Serializable]
public class ReactPropAttribute : Attribute
{
}

[Serializable]
public class ReactBindAttribute : Attribute
{
    public string eventName { get; set; }
    public string jsValueAccess { get; set; }
    public string targetProp { get; set; }
}

[Serializable]
public class ReactStopPropagationAttribute : Attribute
{
}

[Serializable]
public class ReactTemplateAttribute : Attribute
{
    public ReactTemplateAttribute(string methodNameForGettingItemsSource)
    {
        MethodNameForGettingItemsSource = methodNameForGettingItemsSource ?? throw new ArgumentNullException(nameof(methodNameForGettingItemsSource));
    }

    public string MethodNameForGettingItemsSource { get; }
}

[Serializable]
public class ReactTemplateForNullAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class)]
public class ReactRealTypeAttribute : Attribute
{
    public ReactRealTypeAttribute(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}

[AttributeUsage(AttributeTargets.Class)]
public class ReactHigherOrderComponentAttribute : Attribute
{
}



[AttributeUsage(AttributeTargets.Property)]
public class ReactTransformValueInClientAttribute : Attribute
{
    public ReactTransformValueInClientAttribute(string transformFunction)
    {
        TransformFunction = transformFunction;
    }

    public string TransformFunction { get; }
}

[AttributeUsage(AttributeTargets.Property)]
public class ReactGrabEventArgumentsByUsingFunctionAttribute : Attribute
{
    public ReactGrabEventArgumentsByUsingFunctionAttribute(string transformFunction)
    {
        TransformFunction = transformFunction;
    }

    public string TransformFunction { get; }
}

[AttributeUsage(AttributeTargets.Property)]
public class ReactCustomEventAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public class CacheThisMethodByTheseParametersAttribute : Attribute
{
    public CacheThisMethodByTheseParametersAttribute(string nameofMethodForGettingParameters)
    {
        NameofMethodForGettingParameters = nameofMethodForGettingParameters;
    }

    public string NameofMethodForGettingParameters { get; }
}

[AttributeUsage(AttributeTargets.Method)]
public class CacheThisMethodAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class ReactTransformValueInServerSideAttribute : Attribute
{
    public ReactTransformValueInServerSideAttribute(Type transformMethodDeclaringType)
    {
        TransformMethodDeclaringType = transformMethodDeclaringType ?? throw new ArgumentNullException(nameof(transformMethodDeclaringType));
    }

    public Type TransformMethodDeclaringType { get; }
}