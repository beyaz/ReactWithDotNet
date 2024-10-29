
var GlobalMetadata =
{
    MetadataScopes: [],
    Types: [],
    Fields: [],
    Methods: [],
    Properties: [],
    Events: []
};

function SelfBindMetadataTable(metadataTable) 
{
    for (var i = 0; i < metadataTable.Methods.length; i++)
    {
        if (metadataTable.Methods[i].IsDefinition === true)
        {
            metadataTable.Methods[i].MetadataTable = metadataTable;

        }
    }

    for (var i = 0; i < metadataTable.Types.length; i++)
    {
        if (metadataTable.Types[i].IsDefinition === true)
        {
            metadataTable.Types[i].MetadataTable = metadataTable;
        }
    }

    
}

var NativeJs =
{
    Sum: function(thread)
    {
        const stackFrame = thread.CallStack[thread.CallStack.length - 1];
        const evaluationStack = stackFrame.EvaluationStack;

        const b = evaluationStack.pop();
        const a = evaluationStack.pop();

        evaluationStack.push(a + b);
    }
}

function Interpret(thread)
{
    var currentStackFrame = thread.CallStack[thread.CallStack.length - 1];

    var instructions = currentStackFrame.Method.Body.Instructions;
    var operands = currentStackFrame.Method.Body.Operands;

    var evaluationStack = currentStackFrame.EvaluationStack;
    var localVariables  = currentStackFrame.LocalVariables;
    var methodArguments = currentStackFrame.MethodArguments;
    var methodArgumentsOfset= currentStackFrame.MethodArgumentsOfset;

    let previousStackFrame, v0, v1, v2;

    var methodDefinitionOrMaybeNumber;
    var methodDefinition;
    var fieldDefinition;
    var fieldDefinitionOrMaybeNumber;
   
       
    while(true)
    {
        switch (instructions[currentStackFrame.Line])
        {
            case 0: // Nop: No operation
                currentStackFrame.Line++;
                break;

            case 1: // Break: Inform debugger of a break point
                currentStackFrame.Line++;
                break;

            case 2: // Ldarg_0: Load argument 0 onto the stack
                evaluationStack.push(methodArguments[methodArgumentsOfset + 0]);
                currentStackFrame.Line++;
                break;

            case 3: // Ldarg_1: Load argument 1 onto the stack
                evaluationStack.push(methodArguments[methodArgumentsOfset + 1]);
                currentStackFrame.Line++;
                break;

            case 4: // Ldarg_2: Load argument 2 onto the stack
                evaluationStack.push(methodArguments[methodArgumentsOfset + 2]);
                currentStackFrame.Line++;
                break;

            case 5: // Ldarg_3: Load argument 3 onto the stack
                evaluationStack.push(methodArguments[methodArgumentsOfset + 3]);
                currentStackFrame.Line++;
                break;

            case 6: // Ldloc_0: Load local variable 0 onto the stack
                evaluationStack.push(localVariables[0]);
                currentStackFrame.Line++;
                break;

            case 7: // Ldloc_1: Load local variable 1 onto the stack
                evaluationStack.push(localVariables[1]);
                currentStackFrame.Line++;
                break;

            case 8: // Ldloc_2: Load local variable 2 onto the stack
                evaluationStack.push(localVariables[2]);
                currentStackFrame.Line++;
                break;

            case 9: // Ldloc_3: Load local variable 3 onto the stack
                evaluationStack.push(localVariables[3]);
                currentStackFrame.Line++;
                break;

            case 10: // Stloc_0: Store value from the stack in local variable 0
                localVariables[0] = evaluationStack.pop();
                currentStackFrame.Line++;
                break;

            case 11: // Stloc_1: Store value from the stack in local variable 1
                localVariables[1] = evaluationStack.pop();
                currentStackFrame.Line++;
                break;

            case 12: // Stloc_2: Store value from the stack in local variable 2
                localVariables[2] = evaluationStack.pop();
                currentStackFrame.Line++;
                break;

            case 13: // Stloc_3: Store value from the stack in local variable 3
                localVariables[3] = evaluationStack.pop();
                currentStackFrame.Line++;
                break;

            case 14: // Ldarg_S: Load argument at a specified index (short form)
                evaluationStack.push(methodArguments[methodArgumentsOfset + operands[currentStackFrame.Line]]);
                currentStackFrame.Line++;
                break;

            case 15: // Ldarga_S: Load address of argument at a specified index (short form)
                evaluationStack.push({ Array: methodArguments, Index: methodArgumentsOfset + operands[currentStackFrame.Line] } );
                currentStackFrame.Line++;
                break;

            case 16: // Starg_S: Store value from the stack into argument at specified index
                methodArguments[methodArgumentsOfset + operands[currentStackFrame.Line]] = evaluationStack.pop();
                currentStackFrame.Line++;
                break;

            case 17: // Ldloc_S: Load local variable at a specified index (short form)
                evaluationStack.push(localVariables[operands[currentStackFrame.Line]]);
                currentStackFrame.Line++;
                break;

            case 18: // Ldloca_S: Load address of local variable at a specified index (short)
                evaluationStack.push({ Array: localVariables, Index: operands[currentStackFrame.Line] });
                currentStackFrame.Line++;
                break;

            case 19: // Stloc_S: Store value from the stack into local variable at specified index
                localVariables[operands[currentStackFrame.Line]] = evaluationStack.pop();
                currentStackFrame.Line++;
                break;

            case 20: // Ldnull: Push a null reference onto the stack
                evaluationStack.push(null);
                currentStackFrame.Line++;
                break;

            case 21: // Ldc_I4_M1: Load integer constant -1 onto the stack
                evaluationStack.push(-1);
                currentStackFrame.Line++;
                break;

            case 22: // Ldc_I4_0: Load integer constant 0 onto the stack
                evaluationStack.push(0);
                currentStackFrame.Line++;
                break;

            case 23: // Ldc_I4_1: Load integer constant 1 onto the stack
                evaluationStack.push(1);
                currentStackFrame.Line++;
                break;

            case 24: // Ldc_I4_2: Load integer constant 2 onto the stack
                evaluationStack.push(2);
                currentStackFrame.Line++;
                break;

            case 25: // Ldc_I4_3: Load integer constant 3 onto the stack
                evaluationStack.push(3);
                currentStackFrame.Line++;
                break;

            case 26: // Ldc_I4_4: Load integer constant 4 onto the stack
                evaluationStack.push(4);
                currentStackFrame.Line++;
                break;

            case 27: // Ldc_I4_5: Load integer constant 5 onto the stack
                evaluationStack.push(5);
                currentStackFrame.Line++;
                break;

            case 28: // Ldc_I4_6: Load integer constant 6 onto the stack
                evaluationStack.push(6);
                currentStackFrame.Line++;
                break;

            case 29: // Ldc_I4_7: Load integer constant 7 onto the stack
                evaluationStack.push(7);
                currentStackFrame.Line++;
                break;

            case 30: // Ldc_I4_8: Load integer constant 8 onto the stack
                evaluationStack.push(8);
                currentStackFrame.Line++;
                break;

            case 31: // Ldc_I4_S: Load 4-byte integer constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                currentStackFrame.Line++;
                break;

            case 32: // Ldc_I4: Load 8-byte integer constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                currentStackFrame.Line++;
                break;

            case 33: // Ldc_I8: Load 4-byte floating-point constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                currentStackFrame.Line++;
                break;

            case 34: // Ldc_R4: Load 8-byte floating-point constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                currentStackFrame.Line++;
                break;

            case 35: // Ldc_R8: Load 8-byte floating-point constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                currentStackFrame.Line++;
                break;

            case 36: // Dup: Duplicate the value on top of the stack
                evaluationStack.push(evaluationStack[evaluationStack.length - 1]);
                currentStackFrame.Line++;
                break;

            case 37: // Pop: Remove the value from the top of the stack
                evaluationStack.pop();
                currentStackFrame.Line++;
                break;

            case 38: // Jmp
                currentStackFrame.Line++;
                break;
            case 39: // Call

          

                methodDefinitionOrMaybeNumber = operands[currentStackFrame.Line];

                if (typeof methodDefinitionOrMaybeNumber === 'number')
                {
                    methodDefinition = operands[currentStackFrame.Line] = currentStackFrame.Method.MetadataTable.Methods[operands[currentStackFrame.Line]];
                }

                if (typeof methodDefinition.DeclaringType === 'number')
                {
                    if (currentStackFrame.Method.MetadataTable.Types[methodDefinition.DeclaringType].Name === "NativeJs")
                    {
                        var fn = NativeJs[methodDefinition.Name];
                        if (fn === undefined)
                        {
                            throw 'NativeJs has no function like:' + methodDefinition.Name;
                        }

                        fn(thread);

                        currentStackFrame.Line++;
                        break;
                    }
                }

                if (methodDefinition.IsGenericInstance)
                {
                    if (typeof methodDefinition.ElementMethod === 'number')
                    {
                        methodDefinition.ElementMethod = currentStackFrame.Method.MetadataTable.Methods[methodDefinition.ElementMethod];

                        methodDefinition.Body = methodDefinition.ElementMethod.Body;
                        methodDefinition.Parameters = methodDefinition.ElementMethod.Parameters;
                        methodDefinition.IsStatic = methodDefinition.ElementMethod.IsStatic;
                    }
                }

                instructions = methodDefinition.Body.Instructions;
                operands     = methodDefinition.Body.Operands;


                evaluationStack = [];
                methodArguments = currentStackFrame.EvaluationStack;
                methodArgumentsOfset = methodArguments.length - methodDefinition.Parameters.length;
                localVariables  = [];

                if (methodDefinition.IsStatic === false)
                {
                      // 0: this
                    methodArgumentsOfset--;
                }
                
                currentStackFrame = 
                {
                    Method: methodDefinition,
                    Line: 0,

                    EvaluationStack: evaluationStack,
                    LocalVariables: localVariables,
                    MethodArguments: methodArguments,
                    MethodArgumentsOfset: methodArgumentsOfset
                };

                thread.CallStack.push(currentStackFrame);              
               
                break;
            case 40: // Calli
                currentStackFrame.Line++;
                break;
            case 41: // Ret
                if (thread.CallStack.length === 1)
                {
                    return;
                }

                previousStackFrame = thread.CallStack.pop();

                currentStackFrame = thread.CallStack[thread.CallStack.length -1];

                

                instructions = currentStackFrame.Method.Body.Instructions;
                operands     = currentStackFrame.Method.Body.Operands;

                evaluationStack = currentStackFrame.EvaluationStack;
                localVariables  = currentStackFrame.LocalVariables;
                methodArguments = currentStackFrame.MethodArguments;
                methodArgumentsOfset = currentStackFrame.MethodArgumentsOfset;
                
                for (var i = 0; i < previousStackFrame.Method.Parameters.length; i++)
                {
                    evaluationStack.pop();
                }

                if (previousStackFrame.Method.IsStatic === false)
                {
                    evaluationStack.pop();
                }

                if(previousStackFrame.EvaluationStack.length > 0)
                {
                    evaluationStack.push(previousStackFrame.EvaluationStack.pop());
                }

                currentStackFrame.Line++;
                
                break;
            case 42: // Br_S
                currentStackFrame.Line = operands[currentStackFrame.Line];
                break;

            case 43: // Brfalse_S
                if (evaluationStack[evaluationStack.length])
                {
                    currentStackFrame.Line++;
                }
                else
                {
                    currentStackFrame.Line = operands[currentStackFrame.Line];
                }
                evaluationStack.pop();          
                break;

            case 44: // Brtrue_S
                if (evaluationStack[evaluationStack.length - 1])
                {
                    currentStackFrame.Line = operands[currentStackFrame.Line];
                }
                else
                {
                    currentStackFrame.Line++;
                }
                evaluationStack.pop(); 
                break;
            case 45: // Beq_S
                currentStackFrame.Line++;
                break;
            case 46: // Bge_S
                currentStackFrame.Line++;
                break;

            case 47: // Bgt_S

                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                if (typeof v0 === 'number')
                {
                    if (v0 > v1)
                    {
                        currentStackFrame.Line = operands[currentStackFrame.Line];
                    }
                    else
                    {
                        currentStackFrame.Line++;
                    }
                }
                break;

            case 48: // Ble_S
                currentStackFrame.Line++;
                break;
            case 49: // Blt_S
                currentStackFrame.Line++;
                break;
            case 50: // Bne_Un_S
                currentStackFrame.Line++;
                break;
            case 51: // Bge_Un_S
                currentStackFrame.Line++;
                break;
            case 52: // Bgt_Un_S
                currentStackFrame.Line++;
                break;
            case 53: // Ble_Un_S
                currentStackFrame.Line++;
                break;
            case 54: // Blt_Un_S
                currentStackFrame.Line++;
                break;
            case 55: // Br
                currentStackFrame.Line++;
                break;
            case 56: // Brfalse
                currentStackFrame.Line++;
                break;
            case 57: // Brtrue
                currentStackFrame.Line++;
                break;
            case 58: // Beq
                currentStackFrame.Line++;
                break;
            case 59: // Bge
                currentStackFrame.Line++;
                break;
            case 60: // Bgt
                currentStackFrame.Line++;
                break;
            case 61: // Ble
                currentStackFrame.Line++;
                break;
            case 62: // Blt
                currentStackFrame.Line++;
                break;
            case 63: // Bne_Un
                currentStackFrame.Line++;
                break;
            case 64: // Bge_Un
                currentStackFrame.Line++;
                break;
            case 65: // Bgt_Un
                currentStackFrame.Line++;
                break;
            case 66: // Ble_Un
                currentStackFrame.Line++;
                break;
            case 67: // Blt_Un
                currentStackFrame.Line++;
                break;
            case 68: // Switch
                currentStackFrame.Line++;
                break;
            case 69: // Ldind_I1
                currentStackFrame.Line++;
                break;
            case 70: // Ldind_U1
                currentStackFrame.Line++;
                break;
            case 71: // Ldind_I2
                currentStackFrame.Line++;
                break;
            case 72: // Ldind_U2
                currentStackFrame.Line++;
                break;
            case 73: // Ldind_I4
                currentStackFrame.Line++;
                break;
            case 74: // Ldind_U4
                currentStackFrame.Line++;
                break;
            case 75: // Ldind_I8
                currentStackFrame.Line++;
                break;
            case 76: // Ldind_I
                currentStackFrame.Line++;
                break;
            case 77: // Ldind_R4
                currentStackFrame.Line++;
                break;
            case 78: // Ldind_R8
                currentStackFrame.Line++;
                break;
            case 79: // Ldind_Ref
                v0 = evaluationStack.pop();

                evaluationStack.push(v0.Array[v0.Index]);

                currentStackFrame.Line++;
                break;
            case 80: // Stind_Ref
                
                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                v0.Array[v0.Index] = v1;

                currentStackFrame.Line++;
                break;
            case 81: // Stind_I1
                currentStackFrame.Line++;
                break;
            case 82: // Stind_I2
                currentStackFrame.Line++;
                break;
            case 83: // Stind_I4
                currentStackFrame.Line++;
                break;
            case 84: // Stind_I8
                currentStackFrame.Line++;
                break;
            case 85: // Stind_R4
                currentStackFrame.Line++;
                break;
            case 86: // Stind_R8
                currentStackFrame.Line++;
                break;
            case 87: // Add
                currentStackFrame.Line++;
                break;
            case 88: // Sub
                currentStackFrame.Line++;
                break;
            case 89: // Mul
                currentStackFrame.Line++;
                break;
            case 90: // Div
                currentStackFrame.Line++;
                break;
            case 91: // Div_Un
                currentStackFrame.Line++;
                break;
            case 92: // Rem
                currentStackFrame.Line++;
                break;
            case 93: // Rem_Un
                currentStackFrame.Line++;
                break;
            case 94: // And
                currentStackFrame.Line++;
                break;

            case 95: // Or

                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                evaluationStack.push(v0|v1);

                currentStackFrame.Line++;
                break;

            case 96: // Xor
                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                evaluationStack.push(v0 ^ v1);

                currentStackFrame.Line++;
                break;
            case 97: // Shl
                currentStackFrame.Line++;
                break;
            case 98: // Shr
                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                if (typeof v0 === 'number')
                {
                    evaluationStack.push(v0 >> v1);
                }
                break;
            case 99: // Shr_Un
                currentStackFrame.Line++;
                break;
            case 100: // Neg
                currentStackFrame.Line++;
                break;
            case 101: // Not
                currentStackFrame.Line++;
                break;
            case 102: // Conv_I1
                currentStackFrame.Line++;
                break;
            case 103: // Conv_I2
                currentStackFrame.Line++;
                break;
            case 104: // Conv_I4
                currentStackFrame.Line++;
                break;
            case 105: // Conv_I8
                currentStackFrame.Line++;
                break;
            case 106: // Conv_R4
                currentStackFrame.Line++;
                break;
            case 107: // Conv_R8
                currentStackFrame.Line++;
                break;
            case 108: // Conv_U4
                currentStackFrame.Line++;
                break;
            case 109: // Conv_U8
                currentStackFrame.Line++;
                break;
            case 110: // Callvirt
                currentStackFrame.Line++;
                break;
            case 111: // Cpobj
                currentStackFrame.Line++;
                break;
            case 112: // Ldobj
                currentStackFrame.Line++;
                break;
            case 113: // Ldstr
                evaluationStack.push(operands[currentStackFrame.Line]);
                currentStackFrame.Line++;
                break;
            case 114: // Newobj
                
               var newObj = {};
            
                methodDefinitionOrMaybeNumber = operands[currentStackFrame.Line];

                if (typeof methodDefinitionOrMaybeNumber === 'number')
                {
                    methodDefinition = operands[currentStackFrame.Line] = currentStackFrame.Method.MetadataTable.Methods[operands[currentStackFrame.Line]];
                    if (typeof methodDefinition.DeclaringType === 'number')
                    {
                        methodDefinition.DeclaringType = currentStackFrame.Method.MetadataTable.Types[methodDefinition.DeclaringType];
                    }
                    newObj['$type'] = methodDefinition.DeclaringType;
                }

                if (methodDefinition.DeclaringType.IsGenericInstance)
                {
                    if (typeof methodDefinition.DeclaringType.ElementType === 'number')
                    {
                        methodDefinition.DeclaringType.ElementType = currentStackFrame.Method.MetadataTable.Types[methodDefinition.DeclaringType.ElementType];
                    }

                    for (var i = 0; i < methodDefinition.DeclaringType.ElementType.Methods.length; i++)
                    {
                        if (typeof methodDefinition.DeclaringType.ElementType.Methods[i] === 'number')
                        {
                            methodDefinition.DeclaringType.ElementType.Methods[i] = currentStackFrame.Method.MetadataTable.Methods[methodDefinition.DeclaringType.ElementType.Methods[i]];
                        }
                    }

                    for (var i = 0; i < methodDefinition.DeclaringType.ElementType.Methods.length; i++)
                    {
                        if (methodDefinition.DeclaringType.ElementType.Methods[i].IsDefinition)
                        {
                            if (methodDefinition.DeclaringType.ElementType.Methods[i].Name === methodDefinition.Name)
                            {
                                methodDefinition.Body = methodDefinition.DeclaringType.ElementType.Methods[i].Body;
                                methodDefinition.Parameters = methodDefinition.DeclaringType.ElementType.Methods[i].Parameters;
                                methodDefinition.IsStatic = methodDefinition.DeclaringType.ElementType.Methods[i].IsStatic;
                            } 
                        }
                    }
                }

                var tempArray = [];             

                for (var i = 0; i < methodDefinition.Parameters.length; i++)
                {
                    tempArray.push(evaluationStack.pop());
                }

                tempArray.push(newObj);
                tempArray.push(newObj);
                

                while(tempArray.length > 0)
                {
                    evaluationStack.push(tempArray.pop());
                }



                instructions = methodDefinition.Body.Instructions;
                operands     = methodDefinition.Body.Operands;

                evaluationStack = [];
                methodArguments = currentStackFrame.EvaluationStack;
                methodArgumentsOfset = methodArguments.length - methodDefinition.Parameters.length;
                methodArgumentsOfset--;
                localVariables  = [];
                
                currentStackFrame = 
                {
                    Method: methodDefinition,
                    Line: 0,

                    EvaluationStack: evaluationStack,
                    LocalVariables: localVariables,
                    MethodArguments: methodArguments,
                    MethodArgumentsOfset: methodArgumentsOfset
                };

                thread.CallStack.push(currentStackFrame); 

                break;
            case 115: // Castclass
                currentStackFrame.Line++;
                break;
            case 116: // Isinst
                currentStackFrame.Line++;
                break;
            case 117: // Conv_R_Un
                currentStackFrame.Line++;
                break;
            case 118: // Unbox
                currentStackFrame.Line++;
                break;
            case 119: // Throw
                currentStackFrame.Line++;
                break;
            case 120: // Ldfld
            
                fieldDefinitionOrMaybeNumber = operands[currentStackFrame.Line];
                if (typeof fieldDefinitionOrMaybeNumber === 'number')
                {
                    fieldDefinition = operands[currentStackFrame.Line] = currentStackFrame.Method.MetadataTable.Fields[fieldDefinitionOrMaybeNumber];
                }
                
                /*instance*/v0 = evaluationStack.pop();

                evaluationStack.push(/*instance*/v0[fieldDefinition.Name]);

                currentStackFrame.Line++;
                break;
            case 121: // Ldflda
                currentStackFrame.Line++;
                break;
            case 122: // Stfld
                
                fieldDefinitionOrMaybeNumber = operands[currentStackFrame.Line];
                if (typeof fieldDefinitionOrMaybeNumber === 'number')
                {
                    fieldDefinition = operands[currentStackFrame.Line] = currentStackFrame.Method.MetadataTable.Fields[fieldDefinitionOrMaybeNumber];
                }
                
                /*value*/v1    = evaluationStack.pop();
                /*instance*/v0 = evaluationStack.pop();

                /*instance*/v0[fieldDefinition.Name] = /*value*/v1;

                currentStackFrame.Line++;
                break;
            case 123: // Ldsfld
                currentStackFrame.Line++;
                break;
            case 124: // Ldsflda
                currentStackFrame.Line++;
                break;
            case 125: // Stsfld
                currentStackFrame.Line++;
                break;
            case 126: // Stobj
                currentStackFrame.Line++;
                break;
            case 127: // Conv_Ovf_I1_Un
                currentStackFrame.Line++;
                break;
            case 128: // Conv_Ovf_I2_Un
                currentStackFrame.Line++;
                break;
            case 129: // Conv_Ovf_I4_Un
                currentStackFrame.Line++;
                break;
            case 130: // Conv_Ovf_I8_Un
                currentStackFrame.Line++;
                break;
            case 131: // Conv_Ovf_U1_Un
                currentStackFrame.Line++;
                break;
            case 132: // Conv_Ovf_U2_Un
                currentStackFrame.Line++;
                break;
            case 133: // Conv_Ovf_U4_Un
                currentStackFrame.Line++;
                break;
            case 134: // Conv_Ovf_U8_Un
                currentStackFrame.Line++;
                break;
            case 135: // Conv_Ovf_I_Un
                currentStackFrame.Line++;
                break;
            case 136: // Conv_Ovf_U_Un
                currentStackFrame.Line++;
                break;
            case 137: // Box
                currentStackFrame.Line++;
                break;
            case 138: // Newarr
                currentStackFrame.Line++;
                break;
            case 139: // Ldlen
                currentStackFrame.Line++;
                break;
            case 140: // Ldelema
                currentStackFrame.Line++;
                break;
            case 141: // Ldelem_I1
                currentStackFrame.Line++;
                break;
            case 142: // Ldelem_U1
                currentStackFrame.Line++;
                break;
            case 143: // Ldelem_I2
                currentStackFrame.Line++;
                break;
            case 144: // Ldelem_U2
                currentStackFrame.Line++;
                break;
            case 145: // Ldelem_I4
                currentStackFrame.Line++;
                break;
            case 146: // Ldelem_U4
                currentStackFrame.Line++;
                break;
            case 147: // Ldelem_I8
                currentStackFrame.Line++;
                break;
            case 148: // Ldelem_I
                currentStackFrame.Line++;
                break;
            case 149: // Ldelem_R4
                currentStackFrame.Line++;
                break;
            case 150: // Ldelem_R8
                currentStackFrame.Line++;
                break;
            case 151: // Ldelem_Ref
                currentStackFrame.Line++;
                break;
            case 152: // Stelem_I
                currentStackFrame.Line++;
                break;
            case 153: // Stelem_I1
                currentStackFrame.Line++;
                break;
            case 154: // Stelem_I2
                currentStackFrame.Line++;
                break;
            case 155: // Stelem_I4
                currentStackFrame.Line++;
                break;
            case 156: // Stelem_I8
                currentStackFrame.Line++;
                break;
            case 157: // Stelem_R4
                currentStackFrame.Line++;
                break;
            case 158: // Stelem_R8
                currentStackFrame.Line++;
                break;
            case 159: // Stelem_Ref
                currentStackFrame.Line++;
                break;

            case 160: // Ldelem_Any
                currentStackFrame.Line++;
                break;

            case 161: // Stelem_Any
                currentStackFrame.Line++;
                break;

            case 162: // Unbox_Any
                currentStackFrame.Line++;
                break;

            case 163: // Conv_Ovf_I1
                currentStackFrame.Line++;
                break;

            case 164: // Conv_Ovf_U1
                currentStackFrame.Line++;
                break;

            case 165: // Conv_Ovf_I2
                currentStackFrame.Line++;
                break;

            case 166: // Conv_Ovf_U2
                currentStackFrame.Line++;
                break;

            case 167: // Conv_Ovf_I4
                currentStackFrame.Line++;
                break;

            case 168: // Conv_Ovf_U4
                currentStackFrame.Line++;
                break;

            case 169: // Conv_Ovf_I8
                currentStackFrame.Line++;
                break;

            case 170: // Conv_Ovf_U8
                currentStackFrame.Line++;
                break;

            case 171: // Refanyval
                currentStackFrame.Line++;
                break;

            case 172: // Ckfinite
                currentStackFrame.Line++;
                break;

            case 173: // Mkrefany
                currentStackFrame.Line++;
                break;

            case 174: // Ldtoken
                currentStackFrame.Line++;
                break;

            case 175: // Conv_U2
                currentStackFrame.Line++;
                break;

            case 176: // Conv_U1
                currentStackFrame.Line++;
                break;

            case 177: // Conv_I
                currentStackFrame.Line++;
                break;

            case 178: // Conv_Ovf_I
                currentStackFrame.Line++;
                break;

            case 179: // Conv_Ovf_U
                currentStackFrame.Line++;
                break;

            case 180: // Add_Ovf
                currentStackFrame.Line++;
                break;

            case 181: // Add_Ovf_Un
                currentStackFrame.Line++;
                break;

            case 182: // Mul_Ovf
                currentStackFrame.Line++;
                break;

            case 183: // Mul_Ovf_Un
                currentStackFrame.Line++;
                break;

            case 184: // Sub_Ovf
                currentStackFrame.Line++;
                break;

            case 185: // Sub_Ovf_Un
                currentStackFrame.Line++;
                break;

            case 186: // Endfinally
                currentStackFrame.Line++;
                break;

            case 187: // Leave
                currentStackFrame.Line++;
                break;

            case 188: // Leave_S
                currentStackFrame.Line++;
                break;

            case 189: // Stind_I
                currentStackFrame.Line++;
                break;

            case 190: // Conv_U
                currentStackFrame.Line++;
                break;

            case 191: // Arglist
                currentStackFrame.Line++;
                break;

            case 192: // Ceq
                currentStackFrame.Line++;
                break;

            case 193: // Cgt
                currentStackFrame.Line++;
                break;

            case 194: // Cgt_Un
                currentStackFrame.Line++;
                break;

            case 195: // Clt
                currentStackFrame.Line++;
                break;

            case 196: // Clt_Un
                currentStackFrame.Line++;
                break;

            case 197: // Ldftn
                currentStackFrame.Line++;
                break;

            case 198: // Ldvirtftn
                currentStackFrame.Line++;
                break;

            case 199: // Ldarg
                currentStackFrame.Line++;
                break;

            case 200: // Ldarga
                currentStackFrame.Line++;
                break;

            case 201: // Starg
                currentStackFrame.Line++;
                break;

            case 202: // Ldloc
                currentStackFrame.Line++;
                break;

            case 203: // Ldloca
                currentStackFrame.Line++;
                break;

            case 204: // Stloc
                currentStackFrame.Line++;
                break;

            case 205: // Localloc
                currentStackFrame.Line++;
                break;

            case 206: // Endfilter
                currentStackFrame.Line++;
                break;

            case 207: // Unaligned
                currentStackFrame.Line++;
                break;

            case 208: // Volatile
                currentStackFrame.Line++;
                break;

            case 209: // Tail
                currentStackFrame.Line++;
                break;

            case 210: // Initobj
                currentStackFrame.Line++;
                break;

            case 211: // Constrained
                currentStackFrame.Line++;
                break;

            case 212: // Cpblk
                currentStackFrame.Line++;
                break;

            case 213: // Initblk
                currentStackFrame.Line++;
                break;

            case 214: // No
                currentStackFrame.Line++;
                break;

            case 215: // Rethrow
                currentStackFrame.Line++;
                break;

            case 216: // Sizeof
                currentStackFrame.Line++;
                break;

            case 217: // Refanytype
                currentStackFrame.Line++;
                break;

            case 218: // Readonly
                currentStackFrame.Line++;
                break;




        }
    }
}


function GetMetadata(request, onSuccess, onFail)
{
    const url = "/GetMetadata";
    
    let options =
    {
        method: "POST",
        headers:
        {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        body: JSON.stringify(request)
    };
    
    window.fetch(url, options).then(response => response.json()).then(json => onSuccess(json)).catch(onFail ?? console.log);
}

function CallManagedStaticMethod(methodDefinition, args, success, fail) 
{
    fail = fail ?? console.log;
    
    const thread =
    {
        CallStack: [],
        Line: 0
    };

    const stackFrame =
    {
        Method: methodDefinition,
        EvaluationStack:[],
        LocalVariables:[],
        MethodArguments: args,
        MethodArgumentsOfset: 0,
        Line: 0
    };

    thread.CallStack.push(stackFrame);

    try 
    {
        Interpret(thread);
    }
    catch (e) 
    {
        fail(e);
    } 

    if (success) 
    {
        if (thread.CallStack[0].EvaluationStack.length > 0) 
        {
            const returnValue = thread.CallStack[0].EvaluationStack.pop();
            
            success(returnValue);

            return returnValue;
        }
    }
 
    // ReSharper disable once NotAllPathsReturnValue
}

setTimeout(function () 
{
    function onSuccess(response)
    {
        var metadataTable = response.Metadata;
        
        SelfBindMetadataTable(metadataTable);

        for (var i = 0; i < metadataTable.Types.length; i++) 
        {
            var table = metadataTable.Types[i];

            if (!table.IsDefinition)
            {
                continue;
            }
            
            for (var j = 0; j < table.Methods.length; j++) 
            {
            
                var method = metadataTable.Methods[table.Methods[j]];
            
                if (method.IsDefinition) 
                {
                    if (method.Name === "Abc5")
                    {
                        CallManagedStaticMethod(method,[],console.log,console.log);
                    }
                }  
            } 
        }       

    }
    
    var request =
    [
        {
            AssemblyName: "ReactWithDotNet.WebSite.dll",
            NamespaceName: "ReactWithDotNet.WebSite",
            TypeName: "Deneme45"
        }
    ];

    GetMetadata(request, onSuccess);    
    
}, 3000);


const CLR =
{

};
window.CLR = CLR;




