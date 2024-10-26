namespace ReactWithDotNet.ILCodeGeneration;

public record TypeReferenceModel
{
    // @formatter:off

    public required string Name { get; init; }

    public required string Namespace { get; init; }
    
    public required MetadataScopeModel Scope { get; init; }

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

public enum ExceptionHandlerType {
    Catch = 0,
    Filter = 1,
    Finally = 2,
    Fault = 4,
}

public sealed record ExceptionHandler
{
    // @formatter:off
    
    public required int HandlerEnd { get; init; }
    
    public required int HandlerStart { get; init; }
    
    public required TypeReferenceModel CatchType { get; init; }

    public required ExceptionHandlerType  HandlerType { get; init; }

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

public record MethodReferenceModel
{
    // @formatter:off
    
    public required string Name { get; init; }
    
    public required TypeReferenceModel ReturnType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
    
    // @formatter:on
}

public sealed record CustomAttributeArgumentModel
{
    // @formatter:off
    
    public required TypeReferenceModel Type { get; init; }
    
    public required object Value { get; init; }
    
    // @formatter:on
}

public sealed record CustomAttributeNamedArgumentModel
{
    // @formatter:off
    
    public required string Name { get; init; }
    
    public required CustomAttributeArgumentModel Argument { get; init; }
    
    // @formatter:on
}


public sealed record CustomAttributeModel
{
    // @formatter:off
    
    public required IReadOnlyList<CustomAttributeNamedArgumentModel> Fields { get; init; }
    
    public required IReadOnlyList<CustomAttributeNamedArgumentModel> Properties { get; init; }
    
    public required IReadOnlyList<CustomAttributeArgumentModel> ConstructorArguments { get; init; }
    
     public required MethodReferenceModel Constructor { get; init; }
    
    // @formatter:on
}


public sealed record MethodDefinitionModel : MethodReferenceModel
{
    // @formatter:off
    
    public required MethodBodyModel Body { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
    
    // @formatter:on
}


public sealed record MetadataScopeModel
{
    // @formatter:off

    public required string Name { get; init; }
    
    // @formatter:on
}

public sealed record ArrayTypeModel : TypeReferenceModel
{
    // @formatter:off
    
    public required int Rank { get; init; }
    
    public required TypeReferenceModel ElementType { get; init; }
    
    // @formatter:on
}

public sealed record GenericInstanceMethodModel : MethodReferenceModel
{
    // @formatter:off
    
    public required MethodReferenceModel ElementMethod { get; init; }
    
    public required IReadOnlyList<TypeReferenceModel> GenericArguments { get; init; }
    
    // @formatter:on
}