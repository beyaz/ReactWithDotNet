namespace ReactWithDotNet;

public sealed record TypeReferenceModel
{
    public string Rank { get; init; }

    public string Name { get; set; }

    public TypeReferenceModel ElementType { get; init; }

    public string Namespace { get; set; }
}

public sealed record MethodBodyModel
{
    public IReadOnlyList<int> Instructions { get; set; }

    public IReadOnlyDictionary<int, object> Operands { get; set; }

    public IReadOnlyList<ExceptionHandler> ExceptionHandlers { get; set; }
}

public sealed record ExceptionHandler
{
    public int HandlerStart { get; set; }
    public int HandlerEnd { get; set; }
}

public sealed record MethodReferenceModel
{
    public TypeReferenceModel ReturnType { get; set; }
    public string Name { get; set; }
    public IReadOnlyList<ParameterDefinitionModel> Parameters { get; set; }
}

public sealed class ParameterDefinitionModel
{
    public int Index { get; set; }
    public string Name { get; set; }
    public TypeReferenceModel ParameterType { get; set; }
}

public sealed record ArrayTypeModel
{
    public int Rank { get; init; }
    public TypeReferenceModel ElementType { get; init; }
}


public sealed record GenericInstanceMethodModel
{
    public IReadOnlyList<TypeReferenceModel> GenericArguments { get; init; }
        
    public MethodReferenceModel ElementMethod { get; init; }
}