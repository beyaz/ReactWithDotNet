using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ReactWithDotNet.ILCodeGeneration;

static class MonoCecilToJsonModelMapper
{
    public static MethodDefinitionModel AsModel(this MethodDefinition value)
    {
        return new()
        {
            Body             = value.Body?.AsModel(),
            Name             = value.Name,
            Parameters       = value.Parameters.ToListOf(AsModel),
            ReturnType       = value.ReturnType.AsModel(),
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
                CatchType    = handler.CatchType.AsModel(),
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

    static TypeReferenceModel AsModel(this TypeReference typeReference)
    {
        if (typeReference is ArrayType arrayType)
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
            Name      = typeReference.Name,
            Namespace = typeReference.Namespace,
            Scope     = typeReference.Scope.AsModel()
        };
    }

    static MetadataScopeModel AsModel(this IMetadataScope metadataScope)
    {
        return new()
        {
            Name = metadataScope.Name
        };
    }

    static IReadOnlyList<B> ToListOf<A, B>(this IEnumerable<A> enumerable, Func<A, B> convertFunc)
    {
        return enumerable?.Select(convertFunc).ToList();
    }
}