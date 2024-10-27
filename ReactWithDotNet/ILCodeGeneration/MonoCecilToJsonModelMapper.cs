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
            Scope     = value.Scope.AsModel(),

            CustomAttributes = value.CustomAttributes.Where(IsExportableAttribute).ToListOf(AsModel),
            BaseType         = value.BaseType.AsModel(),
            Methods          = value.Methods.ToListOf(AsModel),
            Fields           = value.Fields.ToListOf(AsModel),
            Properties       = value.Properties.ToListOf(AsModel),
            NestedTypes      = value.NestedTypes.ToListOf(AsModel, metadataTable),
            Events           = value.Events.ToListOf(AsModel),
            Interfaces       = value.Interfaces.ToListOf(AsModel)
        };
    }

    static MethodDefinitionModel AsModel(this MethodDefinition value)
    {
        return new()
        {
            Body             = value.Body?.AsModel(),
            Name             = value.Name,
            Parameters       = value.Parameters.ToListOf(AsModel),
            ReturnType       = value.ReturnType.AsModel(),
            CustomAttributes = value.CustomAttributes.Where(IsExportableAttribute).ToListOf(AsModel)
        };
    }

    static InterfaceImplementationModel AsModel(this InterfaceImplementation value)
    {
        return new()
        {
            InterfaceType    = value.InterfaceType.AsModel(),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel)
        };
    }

    static FieldReferenceModel AsModel(this FieldReference value)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            FieldType     = value.FieldType.AsModel(),
            DeclaringType = value.DeclaringType?.AsModel()
        };
    }

    static FieldDefinitionModel AsModel(this FieldDefinition value)
    {
        return new()
        {
            Name             = value.Name,
            FullName         = value.FullName,
            FieldType        = value.FieldType.AsModel(),
            DeclaringType    = value.DeclaringType?.AsModel(),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel)
        };
    }

    static EventReferenceModel AsModel(this EventReference value)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            EventType     = value.EventType.AsModel(),
            DeclaringType = value.DeclaringType?.AsModel()
        };
    }

    static EventDefinitionModel AsModel(this EventDefinition value)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            EventType     = value.EventType.AsModel(),
            DeclaringType = value.DeclaringType?.AsModel(),

            AddMethod    = value.AddMethod.AsModel(),
            RemoveMethod = value.RemoveMethod.AsModel()
        };
    }

    static PropertyReferenceModel AsModel(this PropertyReference value)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            PropertyType  = value.PropertyType.AsModel(),
            DeclaringType = value.DeclaringType?.AsModel(),
            Parameters    = value.Parameters.ToListOf(AsModel)
        };
    }

    static PropertyDefinitionModel AsModel(this PropertyDefinition value)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            PropertyType  = value.PropertyType.AsModel(),
            DeclaringType = value.DeclaringType?.AsModel(),
            Parameters    = value.Parameters.ToListOf(AsModel),

            GetMethod        = value.GetMethod.AsModel(),
            SetMethod        = value.SetMethod.AsModel(),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel)
        };
    }

    static MethodBodyModel AsModel(this MethodBody body)
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
                operands.Add(i, methodReference.AsModel());
                continue;
            }

            if (operand is TypeReference typeReference)
            {
                operands.Add(i, typeReference.AsModel());
                continue;
            }

            if (operand is FieldReference fieldReference)
            {
                operands.Add(i, fieldReference.AsModel());
                continue;
            }

            if (operand is EventReference eventReference)
            {
                operands.Add(i, eventReference.AsModel());
                continue;
            }

            if (operand is PropertyReference propertyReference)
            {
                operands.Add(i, propertyReference.AsModel());
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
                CatchType    = handler.CatchType?.AsModel(),
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

    static MethodReferenceModel AsModel(this MethodReference methodReference)
    {
        if (methodReference is GenericInstanceMethod genericInstanceMethod)
        {
            return new GenericInstanceMethodModel
            {
                ElementMethod    = genericInstanceMethod.ElementMethod.AsModel(),
                GenericArguments = genericInstanceMethod.GenericArguments.Select(AsModel).ToList(),

                Parameters = default,
                Name       = default,
                ReturnType = default
            };
        }

        return new()
        {
            ReturnType = methodReference.ReturnType.AsModel(),
            Name       = methodReference.Name,
            Parameters = methodReference.Parameters.ToListOf(AsModel)
        };
    }

    static CustomAttributeArgumentModel AsModel(this CustomAttributeArgument value)
    {
        return new()
        {
            Type  = value.Type.AsModel(),
            Value = value.Value
        };
    }

    static CustomAttributeNamedArgumentModel AsModel(this CustomAttributeNamedArgument value)
    {
        return new()
        {
            Name     = value.Name,
            Argument = value.Argument.AsModel()
        };
    }

    static CustomAttributeModel AsModel(this CustomAttribute value)
    {
        return new()
        {
            Constructor          = value.Constructor?.AsModel(),
            ConstructorArguments = value.ConstructorArguments.ToListOf(AsModel),
            Fields               = value.Fields.ToListOf(AsModel),
            Properties           = value.Properties.ToListOf(AsModel)
        };
    }

    static ParameterDefinitionModel AsModel(this ParameterDefinition parameterDefinition)
    {
        return new()
        {
            Index         = parameterDefinition.Index,
            ParameterType = parameterDefinition.ParameterType.AsModel(),
            Name          = parameterDefinition.Name
        };
    }

    static TypeReferenceModel AsModel(this TypeReference value)
    {
        if (value is ArrayType arrayType)
        {
            return new ArrayTypeModel
            {
                Rank        = arrayType.Rank,
                ElementType = arrayType.ElementType.AsModel(),

                Name      = default,
                Namespace = default,
                Scope     = default
            };
        }

        return new()
        {
            Name      = value.Name,
            Namespace = value.Namespace,
            Scope     = value.Scope.AsModel()
        };
    }

    static MetadataScopeModel AsModel(this IMetadataScope metadataScope)
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