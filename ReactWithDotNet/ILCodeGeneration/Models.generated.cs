namespace ReactWithDotNet;

record TypeReferenceModel : MemberReferenceModel
{
    public string Namespace { get; init; }
    
    public int Scope { get; init; }
    
    public int IsValueType { get; set; }
    
    public bool IsGenericParameter { get; set; }
    public int Position { get; set; }
    public int? DeclaringMethod { get; set; }

    public bool IsGenericInstance;

    public int? ElementType;
    
    public IReadOnlyList<int> GenericArguments;
}

sealed record ParameterDefinitionModel
{
    public required int Index { get; init; }

    public required string Name { get; init; }

    public required int ParameterType { get; init; }
}

enum ExceptionHandlerType 
{
    Catch = 0,
    Filter = 1,
    Finally = 2,
    Fault = 4,
}

sealed record ExceptionHandler
{
    public int TryStart { get; set; }
    
    public int TryEnd { get; set; }
    
    public required int HandlerStart { get; init; }
    
    public required int HandlerEnd { get; init; }
    
    public required int? CatchType { get; init; }

    public required ExceptionHandlerType  HandlerType { get; init; }
   
}


record MemberReferenceModel
{
    public string Name { get; init; }
    
    public  int? DeclaringType { get; init; }

    public bool IsDefinition { get; init; }
}

sealed record FieldReferenceModel
{
    public required int IsFieldReference;
    
    public required string Name { get; init; }
    
    public  required int DeclaringType { get; init; }
    
    public required int FieldType { get; init; }
}

sealed record FieldDefinitionModel
{
    public required int IsFieldDefinition;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int FieldType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}

sealed record PropertyReferenceModel
{
    public required int IsPropertyReference;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int PropertyType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
}

sealed record PropertyDefinitionModel
{
    public required int IsPropertyDefinition;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int PropertyType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
    
    public required int? GetMethod { get; init; }
    
    public required int? SetMethod { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}

sealed record EventReferenceModel
{
    public required int IsEventReference;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int EventType { get; init; }
}

sealed record EventDefinitionModel
{
    public required int IsEventDefinition;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int EventType { get; init; }
    
    public required int? AddMethod { get; init; }
    
    public required int? RemoveMethod { get; init; }
}

sealed record InterfaceImplementationModel
{
    public required int InterfaceType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}


sealed record MethodBodyModel
{
    public required IReadOnlyList<int> Instructions { get; init; }

    public required IReadOnlyDictionary<int, object> Operands { get; init; }
      
    public required IReadOnlyList<ExceptionHandler> ExceptionHandlers { get; init; }
}

sealed record MethodReferenceModel
{
    public required int IsMethodReference;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int ReturnType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
}

sealed record CustomAttributeArgumentModel
{
    public required int Type { get; init; }
    
    public required object Value { get; init; }
}

sealed record CustomAttributeNamedArgumentModel
{
    public required string Name { get; init; }
    
    public required CustomAttributeArgumentModel Argument { get; init; }
}

sealed record CustomAttributeModel
{
    public required IReadOnlyList<CustomAttributeNamedArgumentModel> Fields { get; init; }
    
    public required IReadOnlyList<CustomAttributeNamedArgumentModel> Properties { get; init; }
    
    public required IReadOnlyList<CustomAttributeArgumentModel> ConstructorArguments { get; init; }
    
    public required int? Constructor { get; init; }
}

sealed record MethodDefinitionModel
{
    public required int IsMethodDefinition;
    
    public required string Name { get; init; }
    
    
    
    public required int DeclaringType { get; init; }

    public required int ReturnType;
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
    
    
    public required MethodBodyModel Body { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
    
    public bool IsStatic;

    public bool IsConstructor;
}

sealed record TypeDefinitionModel : TypeReferenceModel
{
    public required int BaseType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
    
    public required IReadOnlyList<int> Methods { get; init; }
    
    public required IReadOnlyList<int> Fields { get; init; }
    
    public required IReadOnlyList<int> Properties { get; init; }
    
    public required IReadOnlyList<int> NestedTypes { get; init; }
    
    public required IReadOnlyList<int> Events { get; init; }
    
    public required IReadOnlyList<InterfaceImplementationModel> Interfaces { get; init; }
}

sealed record MetadataScopeModel
{
    public required string Name { get; init; }
}

sealed class MetadataTable
{
    public readonly List<MetadataScopeModel> MetadataScopes = [];
    public readonly List<object> Types = [];
    public readonly List<object> Fields = [];
    public readonly List<object> Methods = [];
    public readonly List<object> Properties = [];
    public readonly List<object> Events = [];
}

sealed record ArrayTypeModel : TypeReferenceModel
{
    public required int Rank { get; init; }
}

sealed record GenericInstanceMethodModel
{
    public required int ElementMethod;

    public required IReadOnlyList<int> GenericArguments;
    
    public required int IsGenericInstanceMethod;
}