using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ReactWithDotNet.ILCodeGeneration;

static class MonoCecilToJsonModelMapper
{
    public static MethodDefinitionModel Map(this MethodDefinition value)
    {
        return new()
        {
            Body             = value.Body?.Map(),
            Name             = value.Name,
            Parameters       = value.Parameters.ToListOf(Map),
            ReturnType       = value.ReturnType.Map(),
            CustomAttributes = value.CustomAttributes.ToListOf(Map)
        };
    }

    static MethodBodyModel Map(this MethodBody body)
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
                operands.Add(i, methodReference.Map());
                continue;
            }

            if (operand is TypeReference typeReference)
            {
                operands.Add(i, typeReference.Map());
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
                CatchType    = handler.CatchType.Map(),
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

    static MethodReferenceModel Map(this MethodReference methodReference)
    {
        if (methodReference is GenericInstanceMethod genericInstanceMethod)
        {
            return new GenericInstanceMethodModel
            {
                ElementMethod    = genericInstanceMethod.ElementMethod.Map(),
                GenericArguments = genericInstanceMethod.GenericArguments.Select(Map).ToList(),

                Parameters = default,
                Name       = default,
                ReturnType = default
            };
        }

        return new()
        {
            ReturnType = methodReference.ReturnType.Map(),
            Name       = methodReference.Name,
            Parameters = methodReference.Parameters.ToListOf(Map)
        };
    }

    static CustomAttributeArgumentModel Map(this CustomAttributeArgument value)
    {
        return new()
        {
            Type  = value.Type.Map(),
            Value = value.Value
        };
    }

    static CustomAttributeNamedArgumentModel Map(this CustomAttributeNamedArgument value)
    {
        return new()
        {
            Name     = value.Name,
            Argument = value.Argument.Map()
        };
    }

    static CustomAttributeModel Map(this CustomAttribute value)
    {
        return new()
        {
            Constructor          = value.Constructor?.Map(),
            ConstructorArguments = value.ConstructorArguments.ToListOf(Map),
            Fields               = value.Fields.ToListOf(Map),
            Properties           = value.Properties.ToListOf(Map)
        };
    }

    static ParameterDefinitionModel Map(this ParameterDefinition parameterDefinition)
    {
        return new()
        {
            Index         = parameterDefinition.Index,
            ParameterType = parameterDefinition.ParameterType.Map(),
            Name          = parameterDefinition.Name
        };
    }

    static TypeReferenceModel Map(this TypeReference typeReference)
    {
        if (typeReference is ArrayType arrayType)
        {
            return new ArrayTypeModel
            {
                Rank        = arrayType.Rank,
                ElementType = arrayType.ElementType.Map(),

                Name      = default,
                Namespace = default,
                Scope     = default
            };
        }

        return new()
        {
            Name      = typeReference.Name,
            Namespace = typeReference.Namespace,
            Scope     = typeReference.Scope.Map()
        };
    }

    static MetadataScopeModel Map(this IMetadataScope metadataScope)
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