using Mono.Cecil;

namespace ReactWithDotNet;

static class ValueTypeId
{
    public const int TypeDefinition = 0;
    public const int TypeReference = 1;
    public const int ArrayType = 2;
    public const int GenericInstanceType = 3;
    public const int GenericParameter = 4;
}

sealed class TypeReferenceModel 
{
    public required int ValueTypeId  { get; init; }
    
    public required string Name { get; init; }
    
    public required string Namespace { get; init; }
    
    public required int Scope { get; init; }
    
    public required int? DeclaringType { get; init; }
    
    public required int IsValueType { get; init; }
    
    public uint Token { get; init; }

    public override string ToString()
    {
        return Namespace + "." + Name;
    }
}

sealed class GenericParameterModel
{
    public required int ValueTypeId  { get; init; }
    
    public required int Position { get; init; }
    
    public required string Name { get; init; }
    
    public required int? DeclaringType { get; init; }
    
    public required int? DeclaringMethod { get; init; }
}

sealed class GenericInstanceTypeModel
{
    public required int ValueTypeId { get; init; }
    
    public required int ElementType { get; init; }
    
    public required IReadOnlyList<int> GenericArguments { get; init; }
    
    public required int IsValueType { get; init; }
}

sealed class ParameterDefinitionModel
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

sealed class ExceptionHandler
{
    public int TryStart { get; init; }
    
    public int TryEnd { get; init; }
    
    public required int HandlerStart { get; init; }
    
    public required int HandlerEnd { get; init; }
    
    public required int? CatchType { get; init; }

    public required ExceptionHandlerType  HandlerType { get; init; }
   
}

sealed class FieldReferenceModel
{
    public required int IsFieldReference { get; init; }
    
    public required string Name { get; init; }
    
    public required int FieldType { get; init; }
    
    public  required int DeclaringType { get; init; }
}

sealed class FieldDefinitionModel
{
    public required int IsFieldDefinition { get; init; }
    
    public required string Name { get; init; }
    
    public required int FieldType { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}

sealed class PropertyReferenceModel
{
    public required int IsPropertyReference { get; init; }
    
    public required string Name { get; init; }
    
    public required int PropertyType { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
}

sealed class PropertyDefinitionModel
{
    public required int IsPropertyDefinition { get; init; }
    
    public required string Name { get; init; }
    
    public required int PropertyType { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int? GetMethod { get; init; }
    
    public required int? SetMethod { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
}

sealed class EventReferenceModel
{
    public required int IsEventReference { get; init; }
    
    public required string Name { get; init; }
    
    public required int EventType { get; init; }
    
    public required int DeclaringType { get; init; }
}

sealed class EventDefinitionModel
{
    public required int IsEventDefinition { get; init; }
    
    public required string Name { get; init; }
    
    public required int EventType { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int? AddMethod { get; init; }
    
    public required int? RemoveMethod { get; init; }
}

sealed class InterfaceImplementationModel
{
    public required int InterfaceType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}

sealed class CustomAttributeArgumentModel
{
    public required int Type { get; init; }
    
    public required object Value { get; init; }
}

sealed class CustomAttributeNamedArgumentModel
{
    public required string Name { get; init; }
    
    public required CustomAttributeArgumentModel Argument { get; init; }
}

sealed class CustomAttributeModel
{
    public required IReadOnlyList<CustomAttributeNamedArgumentModel> Fields { get; init; }
    
    public required IReadOnlyList<CustomAttributeNamedArgumentModel> Properties { get; init; }
    
    public required IReadOnlyList<CustomAttributeArgumentModel> ConstructorArguments { get; init; }
    
    public required int? Constructor { get; init; }
}

sealed class MethodReferenceModel
{
    public required int IsMethodReference { get; init; }
    
    public required string Name { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int ReturnType { get; init; }
    
    public required uint RID { get; init; }
    
    public required int IsStatic { get; init; }
}

sealed class MethodBodyModel
{
    public required IReadOnlyList<int> Instructions { get; init; }

    public required IReadOnlyDictionary<int, object> Operands { get; init; }
      
    public required IReadOnlyList<ExceptionHandler> ExceptionHandlers { get; init; }
}

sealed class MethodDefinitionModel
{
    public required int IsMethodDefinition { get; init; }
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }

    public required int ReturnType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
    
    
    public required MethodBodyModel Body { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
    
    public bool IsStatic { get; init; }

    public bool IsConstructor { get; init; }
}

sealed record TypeDefinitionModel
{
    internal TypeDefinition _rawValue;
    
    public int BaseType { get; init; }
    
    public int ValueTypeId { get; init; }
    
    public  string Name{ get; init; }
    
    public string Namespace{ get; init; }
    
    public int Scope{ get; init; }
    
    public int? DeclaringType{ get; init; }
    
    public int IsValueType{ get; init; }
    
    public IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
    
    public IReadOnlyList<int> Methods { get; init; }
    
    public IReadOnlyList<int> Fields { get; init; }
    
    public IReadOnlyList<int> Properties { get; init; }
    
    public IReadOnlyList<int> NestedTypes { get; init; }
    
    public IReadOnlyList<int> Events { get; init; }
    
    public IReadOnlyList<InterfaceImplementationModel> Interfaces { get; init; }
    
    public override string ToString()
    {
        return Namespace + "." + Name;
    }
    
}

sealed class MetadataScopeModel
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

sealed class ArrayTypeModel
{
    public required int ValueTypeId;
    public required int Rank { get; init; }
    public required int ElementType { get; init; }
}

sealed class GenericInstanceMethodModel
{
    public required int ElementMethod { get; init; }

    public required IReadOnlyList<int> GenericArguments { get; init; }
    
    public required int IsGenericInstanceMethod { get; init; }
    
    public required uint RID { get; init; }
}