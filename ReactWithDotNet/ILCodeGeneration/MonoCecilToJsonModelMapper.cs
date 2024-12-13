using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ReactWithDotNet;



static class MonoCecilToJsonModelMapper
{
    static readonly string[] NotExportableAttributes =
    [
        "System.Runtime.CompilerServices.AsyncStateMachineAttribute",
        "System.Diagnostics.DebuggerStepThroughAttribute",
        "System.Diagnostics.DebuggerBrowsableState",
        "System.Runtime.CompilerServices.ExtensionAttribute"
    ];

    public static void Import(this MetadataTable metadataTable, TypeDefinition value)
    {
        var model = AsModel(value, metadataTable);

        var index = metadataTable.Types.FindIndex(x => IsSame(x, model, metadataTable));
        if (index >= 0)
        {
            metadataTable.Types[index] = model;
            return;
        }

        metadataTable.Types.Add(model);
    }

    public static void Import(this MetadataTable metadataTable, MethodDefinition value)
    {
        var model = AsModel(value, metadataTable);

        var index = metadataTable.Methods.FindIndex(x => IsSame(x, model,metadataTable));
        if (index >= 0)
        {
            metadataTable.Methods[index] = model;
            return;
        }

        metadataTable.Methods.Add(model);
    }

    static int AddAndGetIndex<T>(this List<T> list, T newItem)
    {
        var index = list.Count;

        list.Add(newItem);

        return index;
    }

    static TypeDefinitionModel AsModel(this TypeDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            ValueTypeId = ValueTypeId.TypeDefinition,
            
            Name          = value.Name,
            Namespace     = value.Namespace,
            Scope         = value.Scope.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            IsValueType   = value.IsValueType ? 1: 0,

            CustomAttributes = value.CustomAttributes.Where(IsExportableAttribute).ToListOf(AsModel, metadataTable),
            BaseType         = value.BaseType.IndexAt(metadataTable),
            Methods          = metadataTable.Methods.ForceReplaceThenGetIndexes(value.Methods.ToListOf(AsModel, metadataTable), metadataTable),
            Fields           = metadataTable.Fields.ForceReplaceThenGetIndexes(value.Fields.ToListOf(AsModel, metadataTable),metadataTable),
            Properties       = metadataTable.Properties.ForceReplaceThenGetIndexes(value.Properties.ToListOf(AsModel, metadataTable),metadataTable),
            NestedTypes      = metadataTable.Types.ForceReplaceThenGetIndexes(value.NestedTypes.ToListOf(AsModel, metadataTable),metadataTable),
            Events           = metadataTable.Events.ForceReplaceThenGetIndexes(value.Events.ToListOf(AsModel, metadataTable),metadataTable),
            Interfaces       = value.Interfaces.ToListOf(AsModel, metadataTable)
        };
    }

    static MethodDefinitionModel AsModel(this MethodDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            IsMethodDefinition = 1,
            

            Name              = value.Name,
            DeclaringType     = value.DeclaringType.IndexAt(metadataTable),
            Body              = value.Body?.AsModel(metadataTable),
            IsConstructor     = value.IsConstructor,
            IsStatic          =value.IsStatic,

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
        return new ()
        {
            IsFieldReference = 1,
            Name          = value.Name,
            FieldType     = value.FieldType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType.IndexAt(metadataTable)
        };
    }

    static FieldDefinitionModel AsModel(this FieldDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            IsFieldDefinition = 1,
            Name             = value.Name,
            FieldType        = value.FieldType.IndexAt(metadataTable),
            DeclaringType    = value.DeclaringType.IndexAt(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel, metadataTable)
        };
    }

    static EventReferenceModel AsModel(this EventReference value, MetadataTable metadataTable)
    {
        return new()
        {
            IsEventReference = 1,
            Name          = value.Name,
            EventType     = value.EventType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType.IndexAt(metadataTable)
        };
    }

    static EventDefinitionModel AsModel(this EventDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            IsEventDefinition  = 1,
            Name          = value.Name,
            EventType     = value.EventType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType.IndexAt(metadataTable),

            AddMethod    = value.AddMethod?.IndexAt(metadataTable),
            RemoveMethod = value.RemoveMethod?.IndexAt(metadataTable)
        };
    }

    static PropertyReferenceModel AsModel(this PropertyReference value, MetadataTable metadataTable)
    {
        return new()
        {
            IsPropertyReference = 1,
            Name          = value.Name,
            PropertyType  = value.PropertyType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType.IndexAt(metadataTable),
            Parameters    = value.Parameters.ToListOf(AsModel, metadataTable)
        };
    }

    static PropertyDefinitionModel AsModel(this PropertyDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            IsPropertyDefinition = 1,

            Name          = value.Name,
            PropertyType  = value.PropertyType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType.IndexAt(metadataTable),
            Parameters    = value.Parameters.ToListOf(AsModel, metadataTable),

            GetMethod        = value.GetMethod?.IndexAt(metadataTable),
            SetMethod        = value.SetMethod?.IndexAt(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel, metadataTable)
        };
    }

    static bool IsFullMatchWith(this Mono.Collections.Generic.Collection<ParameterDefinition> listA, Mono.Collections.Generic.Collection<ParameterDefinition> listB)
    {
        if (listA.Count != listB.Count)
        {
            return false;
        }

        for (var i = 0; i < listA.Count; i++)
        {
            if (listA[i].ParameterType.FullName != listB[i].ParameterType.FullName)
            {
                return false;
            }
        }

        return true;
    }
    
    static bool IsNameAndParametersMatched(this MethodDefinition methodDefinition, MethodReference methodReference)
    {
        if (methodDefinition.Name != methodReference.Name)
        {
            return false;
        }
        
        return methodDefinition.Parameters.IsFullMatchWith(methodReference.Parameters);
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

            if (operand is null)
            {
                continue;
            }
            
            if (operand is Instruction operandAsInstruction)
            {
                operands.Add(i, body.Instructions.IndexOf(operandAsInstruction));
                continue;
            }

            if (operand is MethodReference methodReference)
            {
                if (methodReference.FullName == "System.Void System.Object::.ctor()")
                {
                    instructions[^1] = (int)OpCodes.Pop.Code;
                    continue;
                }
                
                if (methodReference.DeclaringType?.FullName == typeof(NativeJsHelper).FullName)
                {
                    instructions[^1] = 220;

                    if (methodReference.Name == nameof(NativeJsHelper.Set))
                    {
                        operands.Add(i,0);
                        continue;
                    }
                    if (methodReference.Name == nameof(NativeJsHelper.Get))
                    {
                        operands.Add(i,1);
                        continue;
                    }
                    if (methodReference.Name == nameof(NativeJsHelper.CreateNewPlainObject))
                    {
                        operands.Add(i,2);
                        continue;
                    }
                    if (methodReference.Name == nameof(NativeJsHelper.CreateNewArray))
                    {
                        operands.Add(i,3);
                        continue;
                    }
                    if (methodReference.Name == nameof(NativeJsHelper.Sum))
                    {
                        operands.Add(i,4);
                        continue;
                    }
                    if (methodReference.Name == "get_"+nameof(NativeJsHelper.PreviousStackFrame))
                    {
                        operands.Add(i,5);
                        continue;
                    }
                    
                    if (methodReference.Name == nameof(NativeJsHelper.Call))
                    {
                        if (methodReference.Parameters.Count == 2)
                        {
                            operands.Add(i,6);
                            continue;
                        }
                        
                        if (methodReference.Parameters.Count == 3 && methodReference.Parameters.Last().ParameterType.Name == "Object")
                        {
                            operands.Add(i,7);
                            continue;
                        }
                        
                        if (methodReference.Parameters.Count == 4)
                        {
                            operands.Add(i,8);
                            continue;
                        }
                        
                        if (methodReference.Parameters.Count == 5)
                        {
                            operands.Add(i,9);
                            continue;
                        }
                        
                        if (methodReference.Parameters.Count == 3)
                        {
                            operands.Add(i,10);
                            continue;
                        }

                        throw new NotImplementedException();
                    }
                    
                    if (methodReference.Name == "get_"+nameof(NativeJsHelper.GlobalMetadata))
                    {
                        operands.Add(i,11);
                        continue;
                    }
                    
                    if (methodReference.Name == nameof(NativeJsHelper.push))
                    {
                        operands.Add(i,12);
                        continue;
                    }
                    if (methodReference.Name == nameof(NativeJsHelper.pop))
                    {
                        operands.Add(i,13);
                        continue;
                    }
                    if (methodReference.Name == nameof(NativeJsHelper.Dump))
                    {
                        operands.Add(i,14);
                        continue;
                    }
                    
                    if (methodReference.Name == "get_"+nameof(NativeJsHelper.CurrentStackFrame))
                    {
                        operands.Add(i,15);
                        continue;
                    }
                    
                }
                
                if (methodReference.DeclaringType?.FullName == typeof(AsExtensions).FullName)
                {
                    instructions[^1] = (int)OpCodes.Nop.Code;
                    continue;
                }
                
                if (methodReference.FullName == "System.Boolean System.String::op_Equality(System.String,System.String)"||
                    methodReference.FullName == "System.Boolean System.String::Equals(System.String,System.String)")
                {
                    instructions[^1] = (int)OpCodes.Ceq.Code;
                    continue;
                }
                
                if (methodReference.FullName == "System.Int32 System.String::get_Length()")
                {
                    instructions[^1] = (int)OpCodes.Ldlen.Code;
                    continue;
                }
                
                if (methodReference.FullName == "System.String System.String::Concat(System.String,System.String)")
                {
                    instructions[^1] = 224;
                    continue;
                }
                if (methodReference.FullName == "System.String System.String::Concat(System.String,System.String,System.String)")
                {
                    instructions[^1] = 225;
                    continue;
                }
                if (methodReference.FullName == "System.String System.String::Concat(System.String,System.String,System.String,System.String)")
                {
                    instructions[^1] = 226;
                    continue;
                }
                
                if (methodReference.DeclaringType?.FullName == "System.String")
                {
                    var methodDefinition = MetadataHelper.AssemblyDefinitionOfCore.FindType(typeof(_System_.String))
                        .Methods.FirstOrDefault(x=>x.IsNameAndParametersMatched(methodReference));

                    if (methodDefinition is not null)
                    {
                        methodReference = methodDefinition;
                    }
                }
                
                if (methodReference.DeclaringType?.FullName == "System.Int64")
                {
                    var methodDefinition = MetadataHelper.AssemblyDefinitionOfCore.FindType(typeof(_System_.Int64))
                        .Methods.FirstOrDefault(x=>x.IsNameAndParametersMatched(methodReference));

                    if (methodDefinition is not null)
                    {
                        methodReference = methodDefinition;
                    }
                }

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

            if (operand is VariableDefinition  variableDefinition)
            {
                operands.Add(i, body.Variables.IndexOf(variableDefinition));
                continue;
            }
            if (operand is Instruction[] operandAsInstructionList)
            {
                operands.Add(i, operandAsInstructionList.Select(body.Instructions.IndexOf));
                continue;
            }
            
            if (operand is sbyte || operand is byte || operand is short || operand is  int || operand is  float)
            {
                operands.Add(i, operand);
                continue;
            }
            
            if (operand is ParameterDefinition  parameterDefinition)
            {
                operands.Add(i, parameterDefinition.Index);
                continue;
            }

            throw new NotImplementedException(code.ToString());
        }

        return new()
        {
            Instructions = instructions,
            Operands     = operands,
            ExceptionHandlers = body.ExceptionHandlers.ToListOf(handler => new ExceptionHandler
            {
                TryStart = body.Instructions.IndexOf(handler.TryStart),
                TryEnd   = body.Instructions.IndexOf(handler.TryEnd),
                
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

    static object AsModel(this MethodReference methodReference, MetadataTable metadataTable)
    {
        if (methodReference is GenericInstanceMethod genericInstanceMethod)
        {
            return new GenericInstanceMethodModel
            {
                IsGenericInstanceMethod = 1,
                ElementMethod    = genericInstanceMethod.ElementMethod.IndexAt(metadataTable),
                GenericArguments = genericInstanceMethod.GenericArguments.ToListOf(x => x.IndexAt(metadataTable))
            };
        }

        
        return new MethodReferenceModel
        {
            IsMethodReference = 1,
            ReturnType        = methodReference.ReturnType.IndexAt(metadataTable),
            Name              = methodReference.Name,
            Parameters        = methodReference.Parameters.ToListOf(AsModel, metadataTable),
            DeclaringType     = methodReference.DeclaringType.IndexAt(metadataTable)
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

    static object AsModel(this TypeReference value, MetadataTable metadataTable)
    {
        if (value is ArrayType arrayType)
        {
            return new ArrayTypeModel
            {
                ValueTypeId = ValueTypeId.ArrayType,
                Rank        = arrayType.Rank,
                ElementType = arrayType.ElementType.IndexAt(metadataTable)
            };
        }
        
        if (value is GenericInstanceType genericInstanceType)
        {
            return new GenericInstanceTypeModel
            {
                ValueTypeId = ValueTypeId.GenericInstanceType,
                GenericArguments  = genericInstanceType.GenericArguments.ToListOf(x => x.IndexAt(metadataTable)),
                ElementType       = genericInstanceType.ElementType.IndexAt(metadataTable),
                IsValueType       = genericInstanceType.IsValueType ? 1: 0
            };
        }
        
        if (value is GenericParameter genericParameter)
        {
            return new GenericParameterModel
            {
                ValueTypeId = ValueTypeId.GenericParameter,
                Position           = genericParameter.Position,
                Name               = genericParameter.Name,
                DeclaringType      = genericParameter.DeclaringType?.IndexAt(metadataTable),
                DeclaringMethod    = genericParameter.DeclaringMethod?.IndexAt(metadataTable),
            };
        }

        return new TypeReferenceModel
        {
            ValueTypeId = ValueTypeId.TypeReference,
            Name          = value.Name,
            Namespace     = value.Namespace,
            Scope         = value.Scope.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            IsValueType = value.IsValueType ? 1: 0
        };
    }

    static IReadOnlyList<int> ForceReplaceThenGetIndexes<TModel>(this List<object> targetList, IEnumerable<TModel> enumerable, MetadataTable metadataTable)
    {
        var list = new List<int>();

        foreach (var definitionModel in enumerable)
        {
            var index = targetList.FindIndex(x => IsSame(x, definitionModel,metadataTable));
            if (index >= 0)
            {
                targetList[index] = definitionModel;
                list.Add(index);
                continue;
            }

            index = targetList.AddAndGetIndex(definitionModel);
            list.Add(index);
        }

        return list;
    }

    static int IndexAt(this IMetadataScope value, MetadataTable metadataTable)
    {
        var scope = value.Name;
        if (scope == "System.Runtime"||scope=="System.Runtime.InteropServices")
        {
            scope = typeof(string).Assembly.Modules.First().Name;
        }
        
        var index = metadataTable.MetadataScopes.FindIndex(x => x.Name == scope);
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.MetadataScopes.AddAndGetIndex(new()
        {
            Name = scope
        });
    }

    static int IndexAt(this EventReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Events.FindIndex(x => IsSame(value, x, metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Events.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static int IndexAt(this FieldReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Fields.FindIndex(x => IsSame(value, x, metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Fields.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static int IndexAt(this PropertyReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Properties.FindIndex(x => IsSame(value, x, metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Properties.AddAndGetIndex(AsModel(value, metadataTable));
    }

    static int IndexAt(this TypeReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Types.FindIndex(x => IsSame(x, value,metadataTable));
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
    
    static bool IsFullMatchWith(this Mono.Collections.Generic.Collection<ParameterDefinition> listA, IReadOnlyList<ParameterDefinitionModel> listB, MetadataTable metadataTable)
    {
        if (listA.Count != listB.Count)
        {
            return false;
        }

        for (var i = 0; i < listA.Count; i++)
        {
            if (listA[i].ParameterType.IndexAt(metadataTable) != listB[i].ParameterType)
            {
                return false;
            }
        }

        return true;
    }
    
    static bool IsExportableAttribute(CustomAttribute value)
    {
        if (NotExportableAttributes.Contains(value.AttributeType.FullName))
        {
            return false;
        }

        return true;
    }

    #region IsSame
    

    
    
    
    
    
    
    
   
    
   

    
    
    static bool IsSame(object a, object b, MetadataTable metadataTable)
    {
        List<Func<bool?>> funcs = 
        [
            
            // f i e l d
            isSame<FieldReference, FieldReferenceModel>((reference, model)=>
            {
                if (reference.Name != model.Name)
                {
                    return false;
                }

                if (reference.DeclaringType.IndexAt(metadataTable) != model.DeclaringType)
                {
                    return false;
                }
                
                return true;
            }),
            isSame<FieldReference, FieldDefinitionModel>((reference, model)=>
            {
                if (reference.Name != model.Name)
                {
                    return false;
                }

                if (reference.DeclaringType.IndexAt(metadataTable) != model.DeclaringType)
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<FieldReferenceModel, FieldReferenceModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }

                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }

                return true;
            }),
            isSame<FieldReferenceModel, FieldDefinitionModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }

                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }

                return true;
            }),
            isSame<FieldDefinitionModel, FieldDefinitionModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }

                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }

                return true;
            }),
            
            // p r o p e r t y
            isSame<PropertyReference, PropertyReferenceModel>((reference, model)=>
            {
                if (reference.Name != model.Name)
                {
                    return false;
                }

                if (reference.DeclaringType.IndexAt(metadataTable) != model.DeclaringType)
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<PropertyReferenceModel, PropertyReferenceModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }

                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }

                return true;
            }),
            
            isSame<PropertyDefinitionModel, PropertyDefinitionModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }

                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }

                return true;
            }),
            isSame<PropertyReferenceModel, PropertyDefinitionModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }

                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }

                return true;
            }),

            
            // e v e n t
            isSame<EventReference, EventReferenceModel>((reference, model)=>
            {
                if (reference.Name != model.Name)
                {
                    return false;
                }

                if (reference.DeclaringType.IndexAt(metadataTable) != model.DeclaringType)
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<EventReferenceModel, EventReferenceModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }

                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }

                return true;
            }),
            
            // t y p e
            isSame<TypeReference, TypeDefinitionModel>(( reference,  model)=>
            {
                if (model.Name != reference.Name)
                {
                    return false;
                }

                if (namespacesAreNotSame(reference.Namespace,model.Namespace ))
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<TypeReferenceModel, TypeDefinitionModel>(( referenceModel,  definitionModel)=>
            {
                if (definitionModel.Name != referenceModel.Name)
                {
                    return false;
                }

                if (namespacesAreNotSame(referenceModel.Namespace , definitionModel.Namespace ))
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<TypeReference, TypeReferenceModel>(( reference,  model)=>
            {
                if (model.Name != reference.Name)
                {
                    return false;
                }

                if (namespacesAreNotSame(reference.Namespace, model.Namespace))
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<TypeDefinitionModel, TypeDefinitionModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }
                    
                if (namespacesAreNotSame(modelA.Namespace, modelB.Namespace))
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<TypeReferenceModel, TypeReferenceModel>(( modelA,  modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }
                    
                if (namespacesAreNotSame(modelA.Namespace, modelB.Namespace))
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<ArrayTypeModel, object>(( arrayModel,  item)=>
            {
                if (item is ArrayTypeModel arrayTypeModel)
                {
                    if (arrayModel.Rank != arrayTypeModel.Rank)
                    {
                        return false;
                    }
                    
                    if (arrayModel.ElementType != arrayTypeModel.ElementType)
                    {
                        return false;
                    }
                    
                    return true;
                }
                
                if (item is ArrayType arrayType)
                {
                    if (arrayModel.Rank != arrayType.Rank)
                    {
                        return false;
                    }
                    
                    if (arrayModel.ElementType != arrayType.ElementType.IndexAt(metadataTable))
                    {
                        return false;
                    }
                    
                    return true;
                }
                
                return false;
            }),
            
            isSame<GenericInstanceTypeModel, object>((model, item)=>
            {
                if (item is GenericInstanceType reference)
                {
                    if (model.ElementType != reference.ElementType.IndexAt(metadataTable))
                    {
                        return false;
                    }
                    
                    if (reference.GenericArguments.Count != model.GenericArguments.Count)
                    {
                        return false;
                    }

                    for (var i = 0; i < reference.GenericArguments.Count; i++)
                    {
                        var genericArgument = reference.GenericArguments[i];

                        if (!IsSame(genericArgument, metadataTable.Types[model.GenericArguments[i]], metadataTable))
                        {
                            return false;
                        }
                    }

                
                    return true;
                }
                
                if (item is GenericInstanceTypeModel model2)
                {
                    if (model.ElementType != model2.ElementType)
                    {
                        return false;
                    }
                    
                    if (model.GenericArguments.Count != model2.GenericArguments.Count)
                    {
                        return false;
                    }

                    for (var i = 0; i < model.GenericArguments.Count; i++)
                    {
                        if (model.GenericArguments[i] != model2.GenericArguments[i])
                        {
                            return false;
                        }
                    }

                
                    return true;
                }

                return false;
            }),
            
            isSame<GenericParameterModel, object>((model, item)=>
            {
                if (item is GenericParameter genericParameter)
                {
                    if (model.Position != genericParameter.Position)
                    {
                        return false;
                    }
                    
                    if (model.Name != genericParameter.Name)
                    {
                        return false;
                    }
                    
                    if (model.DeclaringType.HasValue && model.DeclaringType != genericParameter.DeclaringType.IndexAt(metadataTable))
                    {
                        return false;
                    }
                    
                    if (model.DeclaringMethod.HasValue && model.DeclaringMethod != genericParameter.DeclaringMethod.IndexAt(metadataTable))
                    {
                        return false;
                    }
                    
                    return true;
                }
                
                if (item is GenericParameterModel model2)
                {
                    if (model.Position != model2.Position)
                    {
                        return false;
                    }
                    
                    if (model.Name != model2.Name)
                    {
                        return false;
                    }
                    
                    if (model.DeclaringType.HasValue && model.DeclaringType != model2.DeclaringType)
                    {
                        return false;
                    }
                    
                    if (model.DeclaringMethod.HasValue && model.DeclaringMethod != model2.DeclaringMethod)
                    {
                        return false;
                    }
                    
                    return true;
                }

                return false;
            }),
            
            
            // m e t h o d
            isSame<MethodReferenceModel,MethodDefinitionModel>(( modelA, modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }
            
                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }
            
                if (modelA.Parameters.Count != modelB.Parameters.Count)
                {
                    return false;
                }

                for (var i = 0; i < modelA.Parameters.Count; i++)
                {
                    if (modelA.Parameters[i].ParameterType != modelB.Parameters[i].ParameterType)
                    {
                        return false;
                    }
                }

                
                return true;
            }),
            
            isSame<MethodReferenceModel,MethodReferenceModel>(( modelA, modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }
            
                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }
            
                if (modelA.Parameters.Count != modelB.Parameters.Count)
                {
                    return false;
                }

                for (var i = 0; i < modelA.Parameters.Count; i++)
                {
                    if (modelA.Parameters[i].ParameterType != modelB.Parameters[i].ParameterType)
                    {
                        return false;
                    }
                }
                
                return true;
            }),
            
            isSame<MethodDefinitionModel,MethodDefinitionModel>(( modelA, modelB)=>
            {
                if (modelA.Name != modelB.Name)
                {
                    return false;
                }
            
                if (modelA.DeclaringType != modelB.DeclaringType)
                {
                    return false;
                }
            
                if (modelA.Parameters.Count != modelB.Parameters.Count)
                {
                    return false;
                }

                for (var i = 0; i < modelA.Parameters.Count; i++)
                {
                    if (modelA.Parameters[i].ParameterType != modelB.Parameters[i].ParameterType)
                    {
                        return false;
                    }
                }
                
                return true;
            }),
             
            isSame<MethodReferenceModel,MethodReference>(( model, reference)=>
            {
                if (reference.Name != model.Name)
                {
                    return false;
                }

                if (reference.DeclaringType.IndexAt(metadataTable) != model.DeclaringType)
                {
                    return false;
                }

                if (!reference.Parameters.IsFullMatchWith(model.Parameters, metadataTable))
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<MethodDefinitionModel,MethodReference>(( model, reference)=>
            {
                if (reference.Name != model.Name)
                {
                    return false;
                }

                if (reference.DeclaringType.IndexAt(metadataTable) != model.DeclaringType)
                {
                    return false;
                }

                if (!reference.Parameters.IsFullMatchWith(model.Parameters, metadataTable))
                {
                    return false;
                }
                
                return true;
            }),
            
            isSame<GenericInstanceMethodModel,GenericInstanceMethodModel>(( modelA, modelB)=>
            {
                if (modelA.ElementMethod != modelB.ElementMethod)
                {
                    return false;
                }

                return isSameValues(modelA.GenericArguments, modelB.GenericArguments);
            }),
            isSame<GenericInstanceMethodModel,GenericInstanceMethod>(( modelA, reference)=>
            {
                if (modelA.ElementMethod != reference.ElementMethod.IndexAt(metadataTable))
                {
                    return false;
                }

                if (modelA.GenericArguments.Count != reference.GenericArguments.Count)
                {
                    return false;
                }
                
                for (var i = 0; i < modelA.GenericArguments.Count; i++)
                {
                    if (modelA.GenericArguments[i] != reference.GenericArguments[i].IndexAt(metadataTable))
                    {
                        return false;
                    }
                }

                return true;
            }),
            
            isSame<GenericInstanceMethodModel,object>(( _, _)=> false)
            
        ];

        

      
       
        
        bool? result = run(funcs);
        if (result is not null)
        {
            return result.Value;
        }

        throw new NotImplementedException();

        Func<bool?> isSame<A,B>(Func<A,B, bool> compareFunc)
        {
            {
                if (a is A _a && b is B _b)
                {
                    return ()=>compareFunc(_a, _b);
                }
            }

            {
                if (a is B _b && b is A _a)
                {
                    return ()=>compareFunc(_a, _b);
                }
            }

            return null;
        }

        static bool? run(IEnumerable<Func<bool?>> funcs)
        {
            foreach (var func in funcs.Where(x=>x is not null))
            {
                var result = func();
                if (result is not null)
                {
                    return result.Value;
                }
            }

            return null;
        }

        static bool namespacesAreNotSame(string namespaceA, string namespaceB)
        {
            if (namespaceA == namespaceB)
            {
                return false;
            }
                        
            if (namespaceA == nameof(_System_) && namespaceB == nameof(System))
            {
                return false;
            }
                        
            if (namespaceA == nameof(System) && namespaceB == nameof(_System_))
            {
                return false;
            }
                        
            return true;
        }

        static bool isSameValues(IReadOnlyList<int> listA, IReadOnlyList<int> listB)
        {
            if (listA.Count != listB.Count)
            {
                return false;
            }

            var count = listA.Count;
            
            for (int i = 0; i < count; i++)
            {
                if (listA[i] != listB[i])
                {
                    return false;
                }
            }
            
            return true;
        }
    }
    
  
            
    #endregion

    static IReadOnlyList<B> ToListOf<A, B>(this IEnumerable<A> enumerable, Func<A, B> convertFunc)
    {
        return enumerable?.Select(convertFunc).ToList();
    }

    static IReadOnlyList<C> ToListOf<A, B, C>(this IEnumerable<A> enumerable, Func<A, B, C> convertFunc, B b)
    {
        return enumerable?.Select(a => convertFunc(a, b)).ToList();
    }
}