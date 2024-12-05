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

        var index = metadataTable.Types.FindIndex(x => IsSame((TypeReferenceModel)x, model));
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

        var index = metadataTable.Methods.FindIndex(x => IsSame(x, model));
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
            IsDefinition  = value.IsDefinition,
            Name          = value.Name,
            Namespace     = value.Namespace,
            Scope         = value.Scope.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            IsValueType   = value.IsValueType ? 1: 0,

            CustomAttributes = value.CustomAttributes.Where(IsExportableAttribute).ToListOf(AsModel, metadataTable),
            BaseType         = value.BaseType.IndexAt(metadataTable),
            Methods          = metadataTable.Methods.ForceReplaceThenGetIndexes(value.Methods.ToListOf(AsModel, metadataTable)),
            Fields           = metadataTable.Fields.ForceReplaceThenGetIndexes(value.Fields.ToListOf(AsModel, metadataTable)),
            Properties       = metadataTable.Properties.ForceReplaceThenGetIndexes(value.Properties.ToListOf(AsModel, metadataTable)),
            NestedTypes      = metadataTable.Types.ForceReplaceThenGetIndexes(value.NestedTypes.ToListOf(AsModel, metadataTable)),
            Events           = metadataTable.Events.ForceReplaceThenGetIndexes(value.Events.ToListOf(AsModel, metadataTable)),
            Interfaces       = value.Interfaces.ToListOf(AsModel, metadataTable)
        };
    }

    static MethodDefinitionModel AsModel(this MethodDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            IsDefinition = true,
            

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
        return new()
        {
            Name          = value.Name,
            FieldType     = value.FieldType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable)
        };
    }

    static FieldDefinitionModel AsModel(this FieldDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            Name             = value.Name,
            FieldType        = value.FieldType.IndexAt(metadataTable),
            DeclaringType    = value.DeclaringType?.IndexAt(metadataTable),
            CustomAttributes = value.CustomAttributes.ToListOf(AsModel, metadataTable),
            IsDefinition     = true
        };
    }

    static EventReferenceModel AsModel(this EventReference value, MetadataTable metadataTable)
    {
        return new()
        {
            Name          = value.Name,
            EventType     = value.EventType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable)
        };
    }

    static EventDefinitionModel AsModel(this EventDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            IsDefinition  = true,
            Name          = value.Name,
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
            PropertyType  = value.PropertyType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            Parameters    = value.Parameters.ToListOf(AsModel, metadataTable)
        };
    }

    static PropertyDefinitionModel AsModel(this PropertyDefinition value, MetadataTable metadataTable)
    {
        return new()
        {
            IsDefinition = true,

            Name          = value.Name,
            PropertyType  = value.PropertyType.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
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

    static MethodReferenceModel AsModel(this MethodReference methodReference, MetadataTable metadataTable)
    {
        if (methodReference is GenericInstanceMethod genericInstanceMethod)
        {
            return new GenericInstanceMethodModel
            {
                IsGenericInstance = 1,
                
                ElementMethod    = genericInstanceMethod.ElementMethod.IndexAt(metadataTable),
                GenericArguments = genericInstanceMethod.GenericArguments.ToListOf(x => x.IndexAt(metadataTable)),

                Parameters    = default,
                Name          = default,
                DeclaringType = default,
                ReturnType    = default
            };
        }

        
        return new()
        {
            ReturnType    = methodReference.ReturnType.IndexAt(metadataTable),
            Name          = methodReference.Name,
            Parameters    = methodReference.Parameters.ToListOf(AsModel, metadataTable),
            DeclaringType = methodReference.DeclaringType.IndexAt(metadataTable)
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

                Name          = default,
                Namespace     = default,
                Scope         = default,
                DeclaringType = value.DeclaringType?.IndexAt(metadataTable)
            };
        }
        
        if (value is GenericInstanceType genericInstanceType)
        {
            
            return new()
            {
                IsGenericInstance = genericInstanceType.IsGenericInstance,
                GenericArguments  = genericInstanceType.GenericArguments.ToListOf(x => x.IndexAt(metadataTable)),
                ElementType       = genericInstanceType.ElementType.IndexAt(metadataTable),
                IsValueType       = value.IsValueType ? 1: 0
            };
        }
        
        if (value is GenericParameter genericParameter)
        {
            return new()
            {
                IsGenericParameter = genericParameter.IsGenericParameter,
                Position           = genericParameter.Position,
                Name               = genericParameter.Name,
                DeclaringType      = genericParameter.DeclaringType?.IndexAt(metadataTable),
                DeclaringMethod    = genericParameter.DeclaringMethod?.IndexAt(metadataTable),
            };
        }

        return new()
        {
            Name          = value.Name,
            Namespace     = value.Namespace,
            Scope         = value.Scope.IndexAt(metadataTable),
            DeclaringType = value.DeclaringType?.IndexAt(metadataTable),
            IsValueType = value.IsValueType ? 1: 0
        };
    }

    static IReadOnlyList<int> ForceReplaceThenGetIndexes<TDefinitionModel>(this List<MemberReferenceModel> targetList, IEnumerable<TDefinitionModel> enumerable) where TDefinitionModel : MemberReferenceModel
    {
        var list = new List<int>();

        foreach (var definitionModel in enumerable)
        {
            var index = targetList.FindIndex(x => IsSame(x, definitionModel));
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
        var index = metadataTable.Types.FindIndex(x => isSame((TypeReferenceModel)x, value,metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Types.AddAndGetIndex(AsModel(value, metadataTable));
        
        static bool isSame(TypeReferenceModel model, TypeReference typeReference,MetadataTable metadataTable)
        {
            if (model.Name != typeReference.Name)
            {
                return false;
            }

            if (typeReference.Namespace == model.Namespace)
            {
                if (typeReference is GenericInstanceType genericInstanceType)
                {
                    if (genericInstanceType.GenericArguments.Count != model.GenericArguments?.Count)
                    {
                        return false;
                    }

                    for (var i = 0; i < genericInstanceType.GenericArguments.Count; i++)
                    {
                        var genericArgument = genericInstanceType.GenericArguments[i];

                        if (!isSame((TypeReferenceModel)metadataTable.Types[model.GenericArguments[i]], genericArgument, metadataTable))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            
            if (typeReference.Namespace == nameof(_System_) && model.Namespace == nameof(System))
            {
                return true;
            }

            return false;
        }
    }

    static int IndexAt(this MethodReference value, MetadataTable metadataTable)
    {
        var index = metadataTable.Methods.FindIndex(x => isSame(x, value, metadataTable));
        if (index >= 0)
        {
            return index;
        }

        return metadataTable.Methods.AddAndGetIndex(AsModel(value, metadataTable));
        
        static bool isSame(MemberReferenceModel model, MethodReference value, MetadataTable metadataTable)
        {
            if (model is MethodReferenceModel methodReferenceModel)
            {
                if (value.Name != model.Name)
                {
                    return false;
                }

                if (value.DeclaringType.IndexAt(metadataTable) != methodReferenceModel.DeclaringType)
                {
                    return false;
                }

                if (!value.Parameters.IsFullMatchWith(methodReferenceModel.Parameters, metadataTable))
                {
                    return false;
                }
                
                return true;
            }

            return false;
            
        }
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
    
    static bool IsFullMatchWith(this IReadOnlyList<ParameterDefinitionModel> listA, IReadOnlyList<ParameterDefinitionModel> listB)
    {
        if (listA.Count != listB.Count)
        {
            return false;
        }

        for (var i = 0; i < listA.Count; i++)
        {
            if (listA[i].ParameterType != listB[i].ParameterType)
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

    static bool IsSame(MemberReferenceModel a, MethodDefinitionModel b)
    {
        return a is MethodReferenceModel a_as_methodReferenceModel && a.Name == b.Name && a.DeclaringType == b.DeclaringType && a_as_methodReferenceModel.Parameters.IsFullMatchWith(b.Parameters);
    }

    static bool IsSame(MemberReferenceModel a, MemberReferenceModel b)
    {
        if (a is FieldReferenceModel && b is FieldReferenceModel)
        {
            return a.Name == b.Name && a.DeclaringType == b.DeclaringType;
        }

        if (a is PropertyReferenceModel && b is PropertyReferenceModel)
        {
            return a.Name == b.Name && a.DeclaringType == b.DeclaringType;
        }

        if (a is EventReferenceModel && b is EventReferenceModel)
        {
            return a.Name == b.Name && a.DeclaringType == b.DeclaringType;
        }

        if (a is MethodReferenceModel methodReferenceModelA && b is MethodReferenceModel methodReferenceModelB)
        {
            if (methodReferenceModelA.Name != methodReferenceModelB.Name)
            {
                return false;
            }
            
            if (methodReferenceModelA.DeclaringType != methodReferenceModelB.DeclaringType)
            {
                return false;
            }
            
            if (methodReferenceModelA.Parameters.Count != methodReferenceModelB.Parameters.Count)
            {
                return false;
            }

            for (int i = 0; i < methodReferenceModelA.Parameters.Count; i++)
            {
                if (methodReferenceModelA.Parameters[i].ParameterType != methodReferenceModelB.Parameters[i].ParameterType)
                {
                    return false;
                }
            }

            return true;
        }

        if (a is TypeReferenceModel aAsTypeReferenceModel && b is TypeReferenceModel bAsMemberReferenceModel)
        {
            return a.Name == b.Name && aAsTypeReferenceModel.Namespace == bAsMemberReferenceModel.Namespace;
        }

        return false;
        
        
    }

    


    

    static bool IsSame(TypeReferenceModel a, TypeReferenceModel b)
    {
        return a.Name == b.Name && a.Namespace == b.Namespace;
    }

    static bool IsSame(PropertyReference value, MemberReferenceModel model, MetadataTable metadataTable)
    {
        return model is PropertyReferenceModel referenceModel &&
               value.Name == referenceModel.Name &&
               value.DeclaringType.IndexAt(metadataTable) == referenceModel.DeclaringType;
    }

    static bool IsSame(FieldReference value, MemberReferenceModel model, MetadataTable metadataTable)
    {
        return model is FieldReferenceModel referenceModel &&
               value.Name == referenceModel.Name &&
               value.DeclaringType.IndexAt(metadataTable) == referenceModel.DeclaringType;
    }

    static bool IsSame(EventReference value, MemberReferenceModel model, MetadataTable metadataTable)
    {
        return model is EventReferenceModel referenceModel &&
               value.Name == referenceModel.Name &&
               value.DeclaringType.IndexAt(metadataTable) == referenceModel.DeclaringType;
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