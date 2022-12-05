namespace ReactWithDotNet.UIDesigner.AssemblyModel;

[Serializable]
public sealed class AssemblyReference
{
    public string Name { get; set; }
}

[Serializable]
public sealed class TypeReference
{
    public AssemblyReference Assembly { get; set; }

    public string FullName { get; set; }

    public string Name { get; set; }

    public string NamespaceName { get; set; }
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
}

[Serializable]
public sealed class ParameterReference
{
    public string Name { get; set; }

    public TypeReference ParameterType { get; set; }
}

