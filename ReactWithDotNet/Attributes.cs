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
public class ReactDefaultValueAttribute : Attribute
{
    public string DefaultValue { get; set; }
}


[Serializable]
public class ReactTemplateAttribute : Attribute
{
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