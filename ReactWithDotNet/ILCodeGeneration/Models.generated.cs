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
    public required int ValueTypeId;
    public required IReadOnlyList<int> GenericArguments;
    public required int ElementType;
    public required int IsValueType;
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
    public required int IsFieldReference;
    
    public required string Name { get; init; }
    
    public  required int DeclaringType { get; init; }
    
    public required int FieldType { get; init; }
}

sealed class FieldDefinitionModel
{
    public required int IsFieldDefinition;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int FieldType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}

sealed class PropertyReferenceModel
{
    public required int IsPropertyReference;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int PropertyType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
}

sealed class PropertyDefinitionModel
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

sealed class EventReferenceModel
{
    public required int IsEventReference;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int EventType { get; init; }
}

sealed class EventDefinitionModel
{
    public required int IsEventDefinition;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int EventType { get; init; }
    
    public required int? AddMethod { get; init; }
    
    public required int? RemoveMethod { get; init; }
}

sealed class InterfaceImplementationModel
{
    public required int InterfaceType { get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
}


sealed class MethodBodyModel
{
    public required IReadOnlyList<int> Instructions { get; init; }

    public required IReadOnlyDictionary<int, object> Operands { get; init; }
      
    public required IReadOnlyList<ExceptionHandler> ExceptionHandlers { get; init; }
}

sealed class MethodReferenceModel
{
    public required int IsMethodReference;
    
    public required string Name { get; init; }
    
    public required int DeclaringType { get; init; }
    
    public required int ReturnType { get; init; }
    
    public required IReadOnlyList<ParameterDefinitionModel> Parameters { get; init; }
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

sealed class MethodDefinitionModel
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

sealed class TypeDefinitionModel
{
    public required int BaseType { get; init; }
    
    public required int ValueTypeId { get; init; }
    
    public required  string Name{ get; init; }
    
    public required string Namespace{ get; init; }
    
    public required int Scope{ get; init; }
    
    public required int? DeclaringType{ get; init; }
    
    public required int IsValueType{ get; init; }
    
    public required IReadOnlyList<CustomAttributeModel> CustomAttributes { get; init; }
    
    public required IReadOnlyList<int> Methods { get; init; }
    
    public required IReadOnlyList<int> Fields { get; init; }
    
    public required IReadOnlyList<int> Properties { get; init; }
    
    public required IReadOnlyList<int> NestedTypes { get; init; }
    
    public required IReadOnlyList<int> Events { get; init; }
    
    public required IReadOnlyList<InterfaceImplementationModel> Interfaces { get; init; }
    
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
    public required int ElementMethod;

    public required IReadOnlyList<int> GenericArguments;
    
    public required int IsGenericInstanceMethod;
}