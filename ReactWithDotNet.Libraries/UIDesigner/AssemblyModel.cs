namespace ReactWithDotNet.UIDesigner;

[Serializable]
public sealed class AssemblyReference
{
    public string Name { get; set; }
    
    public override string ToString()
    {
        return Name;
    }
}

[Serializable]
public sealed class TypeReference
{
    public AssemblyReference Assembly { get; set; }

    public string FullName { get; set; }

    public string Name { get; set; }

    public string NamespaceName { get; set; }

    public override string ToString()
    {
        return $"{FullName},{Assembly}";
    }
}

[Serializable]
public sealed class MethodReference
{
    public TypeReference DeclaringType { get; set; }

    public string FullNameWithoutReturnType { get; set; }

    public bool IsStatic { get; set; }

    public int MetadataToken { get; set; }

    public string Name { get; set; }

    public IReadOnlyList<ParameterReference> Parameters { get; set; }

    public override string ToString()
    {
        return $"{DeclaringType}::{Name}({string.Join(", ", Parameters)})";
    }
}

[Serializable]
public sealed class ParameterReference
{
    public string Name { get; set; }

    public TypeReference ParameterType { get; set; }

    public override string ToString()
    {
        return $"{ParameterType} {Name}";
    }
}

