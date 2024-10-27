using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ReactWithDotNet.ILCodeGeneration;



static class MonoCecilToJsonModelMapper
{
    static readonly string[] NotExportableAttributes =
    [
        "System.Runtime.CompilerServices.AsyncStateMachineAttribute",
        "System.Diagnostics.DebuggerStepThroughAttribute",
        "System.Runtime.CompilerServices.ExtensionAttribute"
    ];

    public static TypeDefinitionModel AsModel(this TypeDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            Name      = value.Name,
            Namespace = value.Namespace,
            Scope     = value.Scope.AsModel(metadataTable),

            CustomAttributes = value.CustomAttributes.Where(IsExportableAttribute).ToListOf(AsModel,metadataTable),
            BaseType         = value.BaseType.AsModel(metadataTable),
            Methods          = value.Methods.ToListOf(AsModel,metadataTable),
            Fields           = value.Fields.ToListOf(AsModel,metadataTable),
            Properties       = value.Properties.ToListOf(AsModel,metadataTable),
            NestedTypes      = value.NestedTypes.ToListOf(AsModel, metadataTable),
            Events           = value.Events.ToListOf(AsModel,metadataTable),
            Interfaces       = value.Interfaces.ToListOf(AsModel,metadataTable)
        };
    }
    
    static int IndexAt(TypeDefinition value, MetadataTable metadataTable)
    {
        var index = metadataTable.Types.FindIndex(x=>IsSame(x,value));
        if (index >= 0)
        {
            return index;
        }
        
        return metadataTable.Types.AddAndGetIndex(AsModel(value, metadataTable));
    }
    
    static int IndexAt(this TypeReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Types.FindIndex(x=>IsSame(x,value));
        if (index >= 0)
        {
            return index;
        }
        
        return metadataTable.Types.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static int AddAndGetIndex<T>(this List<T> list, T newItem)
    {
        var index = list.Count;
        
        list.Add(newItem);

        return index;
    }
    
    static bool IsSame(TypeReferenceModel model, TypeReference value)
    {
        return value.Name == model.Name && value.Namespace == model.Namespace;
    }

    static MethodDefinitionModel AsModel(this MethodDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            Body             = value.Body?.AsModel(metadataTable),
            Name             = value.Name,
            Parameters       = value.Parameters.ToListOf(AsModel,metadataTable),
            ReturnType       = value.ReturnType.AsModel(metadataTable),
            CustomAttributes = value.CustomAttributes.Where(IsExportableAttribute).ToListOf(AsModel,metadataTable)
        };
    }

    static InterfaceImplementationModel AsModel(this InterfaceImplementation value,MetadataTable metadataTable)
    {
        return new()
        {
            InterfaceType    = value.InterfaceType.AsModel(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel,metadataTable)
        };
    }

    static FieldReferenceModel AsModel(this FieldReference value,MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            FieldType     = value.FieldType.AsModel(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable)
        };
    }

    static FieldDefinitionModel AsModel(this FieldDefinition value,MetadataTable metadataTable)
    {
        return new()
        {
            Name             = value.Name,
            FullName         = value.FullName,
            FieldType        = value.FieldType.AsModel(metadataTable),
            DeclaringType    = value.DeclaringType?.IndexAt(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel,metadataTable)
        };
    }

    static EventReferenceModel AsModel(this EventReference value,MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            EventType     = value.EventType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable)
        };
    }

    static EventDefinitionModel AsModel(this EventDefinition value,MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            EventType     = value.EventType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),

            AddMethod    = value.AddMethod.AsModel(metadataTable),
            RemoveMethod = value.RemoveMethod.AsModel(metadataTable)
        };
    }

    static PropertyReferenceModel AsModel(this PropertyReference value,MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            PropertyType  = value.PropertyType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            Parameters    = value.Parameters.ToListOf(AsModel,metadataTable)
        };
    }

    static PropertyDefinitionModel AsModel(this PropertyDefinition value,MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            PropertyType  = value.PropertyType.IndexAt( metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            Parameters    = value.Parameters.ToListOf(AsModel,metadataTable),

            GetMethod        = value.GetMethod.AsModel(metadataTable),
            SetMethod        = value.SetMethod.AsModel(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel,metadataTable)
        };
    }

    static MethodBodyModel AsModel(this MethodBody body,MetadataTable metadataTable)
    {
        var instructions = new List<int>();

        var operands = new Dictionary<int, object>();

        for (var i = 0; i < body.Instructions.Count; i++)
        {
            var instruction = body.Instructions[i];

            var code = instruction.OpCode.Code;

            var operand = instruction.Operand;

            instructions.Add((int)code);

            if (code == Code.Ldstr)
            {
                operands.Add(i, (string)instruction.Operand);
                continue;
            }

            if (operand is Instruction operandAsInstruction)
            {
                operands.Add(i, body.Instructions.IndexOf(operandAsInstruction));
                continue;
            }

            if (operand is MethodReference methodReference)
            {
                operands.Add(i, methodReference.AsModel(metadataTable));
                continue;
            }

            if (operand is TypeReference typeReference)
            {
                operands.Add(i, typeReference.AsModel(metadataTable));
                continue;
            }

            if (operand is FieldReference fieldReference)
            {
                operands.Add(i, fieldReference.AsModel(metadataTable));
                continue;
            }

            if (operand is EventReference eventReference)
            {
                operands.Add(i, eventReference.AsModel(metadataTable));
                continue;
            }

            if (operand is PropertyReference propertyReference)
            {
                operands.Add(i, propertyReference.AsModel(metadataTable));
                continue;
            }

            if (operand is Instruction[] operandAsInstructionList)
            {
                operands.Add(i, operandAsInstructionList.Select(body.Instructions.IndexOf));
            }
        }

        return new()
        {
            Instructions = instructions,
            Operands     = operands,
            ExceptionHandlers = body.ExceptionHandlers.ToListOf(handler => new ExceptionHandler
            {
                HandlerStart = body.Instructions.IndexOf(handler.HandlerStart),
                HandlerEnd   = body.Instructions.IndexOf(handler.HandlerEnd),
                CatchType    = handler.CatchType?.AsModel(metadataTable),
                HandlerType = handler.HandlerType switch
                {
                    Mono.Cecil.Cil.ExceptionHandlerType.Catch   => ExceptionHandlerType.Catch,
                    Mono.Cecil.Cil.ExceptionHandlerType.Filter  => ExceptionHandlerType.Filter,
                    Mono.Cecil.Cil.ExceptionHandlerType.Finally => ExceptionHandlerType.Finally,
                    Mono.Cecil.Cil.ExceptionHandlerType.Fault   => ExceptionHandlerType.Fault,
                    _                                           => throw new ArgumentOutOfRangeException()
                }
            })
        };
    }

    static MethodReferenceModel AsModel(this MethodReference methodReference,MetadataTable metadataTable)
    {
        if (methodReference is GenericInstanceMethod genericInstanceMethod)
        {
            return new GenericInstanceMethodModel
            {
                ElementMethod    = genericInstanceMethod.ElementMethod.AsModel(metadataTable),
                GenericArguments = genericInstanceMethod.GenericArguments.ToListOf(AsModel,metadataTable),

                Parameters = default,
                Name       = default,
                ReturnType = default
            };
        }

        return new()
        {
            ReturnType = methodReference.ReturnType.AsModel(metadataTable),
            Name       = methodReference.Name,
            Parameters = methodReference.Parameters.ToListOf(AsModel,metadataTable)
        };
    }

    static CustomAttributeArgumentModel AsModel(this CustomAttributeArgument value,MetadataTable metadataTable)
    {
        return new()
        {
            Type  = value.Type.AsModel(metadataTable),
            Value = value.Value
        };
    }

    static CustomAttributeNamedArgumentModel AsModel(this CustomAttributeNamedArgument value,MetadataTable metadataTable)
    {
        return new()
        {
            Name     = value.Name,
            Argument = value.Argument.AsModel(metadataTable)
        };
    }

    static CustomAttributeModel AsModel(this CustomAttribute value,MetadataTable metadataTable)
    {
        return new()
        {
            Constructor          = value.Constructor?.AsModel(metadataTable),
            ConstructorArguments = value.ConstructorArguments.ToListOf(AsModel,metadataTable),
            Fields               = value.Fields.ToListOf(AsModel,metadataTable),
            Properties           = value.Properties.ToListOf(AsModel,metadataTable)
        };
    }

    static ParameterDefinitionModel AsModel(this ParameterDefinition parameterDefinition,MetadataTable metadataTable)
    {
        return new()
        {
            Index         = parameterDefinition.Index,
            ParameterType = parameterDefinition.ParameterType.AsModel(metadataTable),
            Name          = parameterDefinition.Name
        };
    }

    static TypeReferenceModel AsModel(this TypeReference value,MetadataTable metadataTable)
    {
        if (value is ArrayType arrayType)
        {
            return new ArrayTypeModel
            {
                Rank        = arrayType.Rank,
                ElementType = arrayType.ElementType.AsModel(metadataTable),

                Name      = default,
                Namespace = default,
                Scope     = default
            };
        }

        return new()
        {
            Name      = value.Name,
            Namespace = value.Namespace,
            Scope     = value.Scope.AsModel(metadataTable)
        };
    }

    static MetadataScopeModel AsModel(this IMetadataScope metadataScope,MetadataTable metadataTable)
    {
        return new()
        {
            Name = metadataScope.Name
        };
    }

    static bool IsExportableAttribute(CustomAttribute value)
    {
        if (NotExportableAttributes.Contains(value.AttributeType.FullName))
        {
            return false;
        }

        return true;
    }

    static IReadOnlyList<B> ToListOf<A, B>(this IEnumerable<A> enumerable, Func<A, B> convertFunc)
    {
        return enumerable?.Select(convertFunc).ToList();
    }
    
    static IReadOnlyList<C> ToListOf<A, B, C>(this IEnumerable<A> enumerable, Func<A, B, C> convertFunc, B b)
    {
        return enumerable?.Select(a=>convertFunc(a,b)).ToList();
    }
}