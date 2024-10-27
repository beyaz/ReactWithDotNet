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
            Scope     = value.Scope.IndexAt(metadataTable),

            CustomAttributes = value.CustomAttributes.Where(IsExportableAttribute).ToListOf(AsModel, metadataTable),
            BaseType         = value.BaseType.IndexAt(metadataTable),
            Methods          = value.Methods.ToListOf(AsModel, metadataTable),
            Fields           = value.Fields.ToListOf(AsModel, metadataTable),
            Properties       = value.Properties.ToListOf(AsModel, metadataTable),
            NestedTypes      = value.NestedTypes.ToListOf(AsModel, metadataTable),
            Events           = value.Events.ToListOf(AsModel, metadataTable),
            Interfaces       = value.Interfaces.ToListOf(AsModel, metadataTable)
        };
    }

    static int AddAndGetIndex<T>(this List<T> list, T newItem)
    {
        var index = list.Count;

        list.Add(newItem);

        return index;
    }

    static MethodDefinitionModel AsModel(this MethodDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            Body             = value.Body?.AsModel(metadataTable),
            Name             = value.Name,
            Parameters       = value.Parameters.ToListOf(AsModel, metadataTable),
            ReturnType       = value.ReturnType.IndexAt(metadataTable),
            CustomAttributes = value.CustomAttributes.Where(IsExportableAttribute).ToListOf(AsModel, metadataTable)
        };
    }

    static InterfaceImplementationModel AsModel(this InterfaceImplementation value, MetadataTable metadataTable)
    {
        return new()
        {
            InterfaceType    = value.InterfaceType.IndexAt(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel, metadataTable)
        };
    }

    static FieldReferenceModel AsModel(this FieldReference value, MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            FieldType     = value.FieldType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable)
        };
    }

    static FieldDefinitionModel AsModel(this FieldDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            Name             = value.Name,
            FullName         = value.FullName,
            FieldType        = value.FieldType.IndexAt(metadataTable),
            DeclaringType    = value.DeclaringType?.IndexAt(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel, metadataTable)
        };
    }

    static EventReferenceModel AsModel(this EventReference value, MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            EventType     = value.EventType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable)
        };
    }

    static EventDefinitionModel AsModel(this EventDefinition value, MetadataTable metadataTable)
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

    static PropertyReferenceModel AsModel(this PropertyReference value, MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            PropertyType  = value.PropertyType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            Parameters    = value.Parameters.ToListOf(AsModel, metadataTable)
        };
    }

    static PropertyDefinitionModel AsModel(this PropertyDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            FullName      = value.FullName,
            PropertyType  = value.PropertyType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            Parameters    = value.Parameters.ToListOf(AsModel, metadataTable),

            GetMethod        = value.GetMethod?.IndexAt(metadataTable),
            SetMethod        = value.SetMethod?.IndexAt(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel, metadataTable)
        };
    }

    static MethodBodyModel AsModel(this MethodBody body, MetadataTable metadataTable)
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
                operands.Add(i, methodReference.IndexAt(metadataTable));
                continue;
            }

            if (operand is TypeReference typeReference)
            {
                operands.Add(i, typeReference.IndexAt(metadataTable));
                continue;
            }

            if (operand is FieldReference fieldReference)
            {
                operands.Add(i, fieldReference.IndexAt(metadataTable));
                continue;
            }

            if (operand is EventReference eventReference)
            {
                operands.Add(i, eventReference.IndexAt(metadataTable));
                continue;
            }

            if (operand is PropertyReference propertyReference)
            {
                operands.Add(i, propertyReference.IndexAt(metadataTable));
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
                CatchType    = handler.CatchType?.IndexAt(metadataTable),
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

    static MethodReferenceModel AsModel(this MethodReference methodReference, MetadataTable metadataTable)
    {
        if (methodReference is GenericInstanceMethod genericInstanceMethod)
        {
            return new GenericInstanceMethodModel
            {
                ElementMethod    = genericInstanceMethod.ElementMethod.IndexAt(metadataTable),
                GenericArguments = genericInstanceMethod.GenericArguments.ToListOf(x => x.IndexAt(metadataTable)),

                Parameters = default,
                Name       = default,
                ReturnType = default
            };
        }

        return new()
        {
            ReturnType = methodReference.ReturnType.IndexAt(metadataTable),
            Name       = methodReference.Name,
            Parameters = methodReference.Parameters.ToListOf(AsModel, metadataTable)
        };
    }

    static CustomAttributeArgumentModel AsModel(this CustomAttributeArgument value, MetadataTable metadataTable)
    {
        return new()
        {
            Type  = value.Type.IndexAt(metadataTable),
            Value = value.Value
        };
    }

    static CustomAttributeNamedArgumentModel AsModel(this CustomAttributeNamedArgument value, MetadataTable metadataTable)
    {
        return new()
        {
            Name     = value.Name,
            Argument = value.Argument.AsModel(metadataTable)
        };
    }

    static CustomAttributeModel AsModel(this CustomAttribute value, MetadataTable metadataTable)
    {
        return new()
        {
            Constructor          = value.Constructor?.IndexAt(metadataTable),
            ConstructorArguments = value.ConstructorArguments.ToListOf(AsModel, metadataTable),
            Fields               = value.Fields.ToListOf(AsModel, metadataTable),
            Properties           = value.Properties.ToListOf(AsModel, metadataTable)
        };
    }

    static ParameterDefinitionModel AsModel(this ParameterDefinition parameterDefinition, MetadataTable metadataTable)
    {
        return new()
        {
            Index         = parameterDefinition.Index,
            ParameterType = parameterDefinition.ParameterType.IndexAt(metadataTable),
            Name          = parameterDefinition.Name
        };
    }

    static TypeReferenceModel AsModel(this TypeReference value, MetadataTable metadataTable)
    {
        if (value is ArrayType arrayType)
        {
            return new ArrayTypeModel
            {
                Rank        = arrayType.Rank,
                ElementType = arrayType.ElementType.IndexAt(metadataTable),

                Name      = default,
                Namespace = default,
                Scope     = default
            };
        }

        return new()
        {
            Name      = value.Name,
            Namespace = value.Namespace,
            Scope     = value.Scope.IndexAt(metadataTable)
        };
    }

    static int IndexAt(this IMetadataScope value, MetadataTable metadataTable)
    {
        var index = metadataTable.MetadataScopes.FindIndex(x => x.Name == value.Name);
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.MetadataScopes.AddAndGetIndex(new()
        {
            Name = value.Name
        });
    }

    static int IndexAt(this EventReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Events.FindIndex(x => IsSame(x, value, metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Events.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static int IndexAt(this FieldReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Fields.FindIndex(x => IsSame(x, value, metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Fields.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static int IndexAt(this PropertyReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Properties.FindIndex(x => IsSame(x, value, metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Properties.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static int IndexAt(this TypeReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Types.FindIndex(x => IsSame(x, value));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Types.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static int IndexAt(this MethodReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Methods.FindIndex(x => IsSame(x, value, metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Methods.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static bool IsExportableAttribute(CustomAttribute value)
    {
        if (NotExportableAttributes.Contains(value.AttributeType.FullName))
        {
            return false;
        }

        return true;
    }

    static bool IsSame(TypeReferenceModel model, TypeReference value)
    {
        return value.Name == model.Name && value.Namespace == model.Namespace;
    }

    static bool IsSame(MethodReferenceModel model, MethodReference value, MetadataTable metadataTable)
    {
        return value.Name == model.Name && value.ReturnType.IndexAt(metadataTable) == model.ReturnType;
    }

    static bool IsSame(PropertyReferenceModel model, PropertyReference value, MetadataTable metadataTable)
    {
        return value.Name == model.Name && value.DeclaringType.IndexAt(metadataTable) == model.DeclaringType;
    }

    static bool IsSame(FieldReferenceModel model, FieldReference value, MetadataTable metadataTable)
    {
        return value.Name == model.Name && value.DeclaringType.IndexAt(metadataTable) == model.DeclaringType;
    }

    static bool IsSame(EventReferenceModel model, EventReference value, MetadataTable metadataTable)
    {
        return value.Name == model.Name && value.DeclaringType.IndexAt(metadataTable) == model.DeclaringType;
    }

    static IReadOnlyList<B> ToListOf<A, B>(this IEnumerable<A> enumerable, Func<A, B> convertFunc)
    {
        return enumerable?.Select(convertFunc).ToList();
    }

    static IReadOnlyList<C> ToListOf<A, B, C>(this IEnumerable<A> enumerable, Func<A, B, C> convertFunc, B b)
    {
        return enumerable?.Select(a => convertFunc(a, b)).ToList();
    }
}