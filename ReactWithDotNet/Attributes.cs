namespace ReactWithDotNet;

[Serializable]
public class ReactAttribute : Attribute
{
}

[Serializable]
public class ReactBindAttribute : Attribute
{
    public string targetProp { get; set; }
    public string jsValueAccess { get; set; }
    public string eventName { get; set; }
}

[Serializable]
public class ReactStopPropagationAttribute : Attribute
{
}

[Serializable]
public class ReactTemplateAttribute : Attribute
{
    public string MethodNameForGettingItemsSource { get; }

    public ReactTemplateAttribute(string methodNameForGettingItemsSource)
    {
        MethodNameForGettingItemsSource = methodNameForGettingItemsSource ?? throw new ArgumentNullException(nameof(methodNameForGettingItemsSource));
    }
    
}

[Serializable]
public class ReactTemplateForNullAttribute : Attribute
{
}



[AttributeUsage(AttributeTargets.Class)]
public class ReactRealTypeAttribute : Attribute
{
    public Type Type { get; }

    public ReactRealTypeAttribute(Type type)
    {
        Type = type;
    }
}


[AttributeUsage(AttributeTargets.Property)]
public class ReactTransformValueInClientAttribute : Attribute
{
    public string TransformFunction { get; }

    public ReactTransformValueInClientAttribute(string transformFunction)
    {
        this.TransformFunction = transformFunction;
    }
}


[AttributeUsage(AttributeTargets.Property)]
public class ReactGrabEventArgumentsByUsingFunctionAttribute : Attribute
{
    public string TransformFunction { get; }

    public ReactGrabEventArgumentsByUsingFunctionAttribute(string transformFunction)
    {
        this.TransformFunction = transformFunction;
    }
}

[AttributeUsage(AttributeTargets.Property)]
public class ReactCustomEventAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public class CacheThisMethodByTheseParametersAttribute : Attribute
{
    public string NameofMethodForGettingParameters { get; }

    public CacheThisMethodByTheseParametersAttribute(string nameofMethodForGettingParameters)
    {
        NameofMethodForGettingParameters = nameofMethodForGettingParameters;
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class CacheThisMethodAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class ReactTransformValueInServerSideAttribute : Attribute
{
    public Type TransformMethodDeclaringType { get; }

    public ReactTransformValueInServerSideAttribute(Type transformMethodDeclaringType)
    {
        this.TransformMethodDeclaringType = transformMethodDeclaringType ?? throw new ArgumentNullException(nameof(transformMethodDeclaringType));
    }
}