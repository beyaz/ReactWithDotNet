namespace ReactWithDotNet.ILCodeGeneration;

public sealed record TypeReferenceModel
{
    // @formatter:off

    public required string Name { get; init; }

    public required string Namespace { get; init; }
    
    // @formatter:on
}

public sealed record ParameterDefinitionModel
{
     // @formatter:off
     
    public required int Index { get; init; }

    public required string Name { get; init; }

    public required TypeReferenceModel ParameterType { get; init; }
    
    // @formatter:on
}

public sealed record ExceptionHandler
{
    // @formatter:off
    
    public required int HandlerEnd { get; init; }
    
    public required int HandlerStart { get; init; }
    
    // @formatter:on
}

public sealed record MethodBodyModel
{
    // @formatter:off
    
    public required IReadOnlyList<int> Instructions { get; init; }

    public required IReadOnlyDictionary<int, object> Operands { get; init; }
      
    public required IReadOnlyList<ExceptionHandler> ExceptionHandlers { get; init; }
    
    // @formatter:on
}

public sealed record MethodReferenceModel
{
    // @formatter:off
    
    public required string Name { get; init; }
    
    public required TypeReferenceModel ReturnType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
    
    // @formatter:on
}

public sealed record ArrayTypeModel
{
    // @formatter:off
    
    public required int Rank { get; init; }
    
    public required TypeReferenceModel ElementType { get; init; }
    
    // @formatter:on
}

public sealed record GenericInstanceMethodModel
{
    // @formatter:off
    
    public required MethodReferenceModel ElementMethod { get; init; }
    
    public required IReadOnlyList<TypeReferenceModel> GenericArguments { get; init; }
    
    // @formatter:on
}