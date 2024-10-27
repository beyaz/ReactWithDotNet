﻿namespace ReactWithDotNet.ILCodeGeneration;

public record TypeReferenceModel
{
    public required string Name { get; init; }

    public required string Namespace { get; init; }
    
    public required MetadataScopeModel Scope { get; init; }
}

public sealed record ParameterDefinitionModel
{
    public required int Index { get; init; }

    public required string Name { get; init; }

    public required TypeReferenceModel ParameterType { get; init; }
}

public enum ExceptionHandlerType 
{
    Catch = 0,
    Filter = 1,
    Finally = 2,
    Fault = 4,
}

public sealed record ExceptionHandler
{
    public required int HandlerEnd { get; init; }
    
    public required int HandlerStart { get; init; }
    
    public required TypeReferenceModel CatchType { get; init; }

    public required ExceptionHandlerType  HandlerType { get; init; }
}


public record MemberReferenceModel
{
    public required string Name { get; init; }
    
    public required TypeReferenceModel DeclaringType { get; init; }
    
    public required string FullName { get; init; }
}

public record FieldReferenceModel : MemberReferenceModel
{
    public required TypeReferenceModel FieldType { get; init; }
}

public record FieldDefinitionModel : FieldReferenceModel
{
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}

public record PropertyReferenceModel : MemberReferenceModel
{
    public required TypeReferenceModel PropertyType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
}

public record PropertyDefinitionModel : PropertyReferenceModel
{
    public required MethodDefinitionModel GetMethod { get; init; }
    
    public required MethodDefinitionModel SetMethod { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}

public record EventReferenceModel : MemberReferenceModel
{
    public required TypeReferenceModel EventType { get; init; }
}

public record EventDefinitionModel : EventReferenceModel
{
    public required MethodDefinitionModel AddMethod { get; init; }
    
    public required MethodDefinitionModel RemoveMethod { get; init; }
}

public sealed record InterfaceImplementationModel
{
    public required TypeReferenceModel InterfaceType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}


public sealed record MethodBodyModel
{
    public required IReadOnlyList<int> Instructions { get; init; }

    public required IReadOnlyDictionary<int, object> Operands { get; init; }
      
    public required IReadOnlyList<ExceptionHandler> ExceptionHandlers { get; init; }
}

public record MethodReferenceModel
{
    public required string Name { get; init; }
    
    public required TypeReferenceModel ReturnType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
}

public sealed record CustomAttributeArgumentModel
{
    public required TypeReferenceModel Type { get; init; }
    
    public required object Value { get; init; }
}

public sealed record CustomAttributeNamedArgumentModel
{
    public required string Name { get; init; }
    
    public required CustomAttributeArgumentModel Argument { get; init; }
}

public sealed record CustomAttributeModel
{
    public required IReadOnlyList<CustomAttributeNamedArgumentModel> Fields { get; init; }
    
    public required IReadOnlyList<CustomAttributeNamedArgumentModel> Properties { get; init; }
    
    public required IReadOnlyList<CustomAttributeArgumentModel> ConstructorArguments { get; init; }
    
    public required MethodReferenceModel Constructor { get; init; }
}

public sealed record MethodDefinitionModel : MethodReferenceModel
{
    public required MethodBodyModel Body { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}

public sealed record TypeDefinitionModel : TypeReferenceModel
{
    public required TypeReferenceModel BaseType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
    
    public required IReadOnlyList<MethodDefinitionModel> Methods { get; init; }
    
    public required IReadOnlyList<FieldDefinitionModel> Fields { get; init; }
    
    public required IReadOnlyList<PropertyDefinitionModel> Properties { get; init; }
    
    public required IReadOnlyList<TypeDefinitionModel> NestedTypes { get; init; }
    
    public required IReadOnlyList<EventDefinitionModel> Events { get; init; }
    
    public required IReadOnlyList<InterfaceImplementationModel> Interfaces { get; init; }
}

public sealed record MetadataScopeModel
{
    public required string Name { get; init; }
}

public sealed record ArrayTypeModel : TypeReferenceModel
{
    public required int Rank { get; init; }
    
    public required TypeReferenceModel ElementType { get; init; }
}

public sealed record GenericInstanceMethodModel : MethodReferenceModel
{
    public required MethodReferenceModel ElementMethod { get; init; }
    
    public required IReadOnlyList<TypeReferenceModel> GenericArguments { get; init; }
}