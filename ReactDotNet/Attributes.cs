using System;

namespace ReactDotNet;


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

