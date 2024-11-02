
var GlobalMetadata =
{
    MetadataScopes: [],
    Types: [],
    Fields: [],
    Methods: [],
    Properties: [],
    Events: []
};

const InterpreterBridge_NewArr = 0;
const InterpreterBridge_NullReferenceException = 1;
const InterpreterBridge_ArgumentNullException = 2;



const InterpreterBridge_Jump = 219;

var InterpreterBridge_Jump_MethodDefinition;
var InterpreterBridge_ImportMetadata_MethodDefinition;

function TryInitialize_InterpreterBridge(metadataTable) 
{
    for (var i = 0; i < metadataTable.Methods.length; i++)
    {
        if (metadataTable.Methods[i].IsDefinition === true)
        {
            if (metadataTable.Types[metadataTable.Methods[i].DeclaringType].Name === 'InterpreterBridge' &&
                metadataTable.Types[metadataTable.Methods[i].DeclaringType].Namespace === 'ReactWithDotNet')
            {
                if (metadataTable.Methods[i].Name === "Jump")
                {
                    InterpreterBridge_Jump_MethodDefinition = metadataTable.Methods[i];
                }

                if (metadataTable.Methods[i].Name === "ImportMetadata")
                {
                    InterpreterBridge_ImportMetadata_MethodDefinition = metadataTable.Methods[i];
                }
            }
        }
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

    let previousStackFrame, v0, v1, v2, v3, v4, evaluationStackIndex, item;

    var method;
    var elementMethod;
    var methodDefinitionOrMaybeNumber;
    var methodDefinition;
    var fieldDefinition;
    var fieldDefinitionOrMaybeNumber;

    var nextInstruction = instructions[currentStackFrame.Line];
   
       
    while(true)
    {
        switch (nextInstruction)
        {
            case 0: // Nop: No operation
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 1: // Break: Inform debugger of a break point
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 2: // Ldarg_0: Load argument 0 onto the stack
                evaluationStack.push(methodArguments[methodArgumentsOfset + 0]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 3: // Ldarg_1: Load argument 1 onto the stack
                evaluationStack.push(methodArguments[methodArgumentsOfset + 1]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 4: // Ldarg_2: Load argument 2 onto the stack
                evaluationStack.push(methodArguments[methodArgumentsOfset + 2]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 5: // Ldarg_3: Load argument 3 onto the stack
                evaluationStack.push(methodArguments[methodArgumentsOfset + 3]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 6: // Ldloc_0: Load local variable 0 onto the stack
                evaluationStack.push(localVariables[0]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 7: // Ldloc_1: Load local variable 1 onto the stack
                evaluationStack.push(localVariables[1]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 8: // Ldloc_2: Load local variable 2 onto the stack
                evaluationStack.push(localVariables[2]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 9: // Ldloc_3: Load local variable 3 onto the stack
                evaluationStack.push(localVariables[3]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 10: // Stloc_0: Store value from the stack in local variable 0
                localVariables[0] = evaluationStack.pop();
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 11: // Stloc_1: Store value from the stack in local variable 1
                localVariables[1] = evaluationStack.pop();
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 12: // Stloc_2: Store value from the stack in local variable 2
                localVariables[2] = evaluationStack.pop();
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 13: // Stloc_3: Store value from the stack in local variable 3
                localVariables[3] = evaluationStack.pop();
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 14: // Ldarg_S: Load argument at a specified index (short form)
                evaluationStack.push(methodArguments[methodArgumentsOfset + operands[currentStackFrame.Line]]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 15: // Ldarga_S: Load address of argument at a specified index (short form)
                evaluationStack.push({ Array: methodArguments, Index: methodArgumentsOfset + operands[currentStackFrame.Line] } );
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 16: // Starg_S: Store value from the stack into argument at specified index
                methodArguments[methodArgumentsOfset + operands[currentStackFrame.Line]] = evaluationStack.pop();
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 17: // Ldloc_S: Load local variable at a specified index (short form)
                evaluationStack.push(localVariables[operands[currentStackFrame.Line]]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 18: // Ldloca_S: Load address of local variable at a specified index (short)
                evaluationStack.push({ Array: localVariables, Index: operands[currentStackFrame.Line] });
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 19: // Stloc_S: Store value from the stack into local variable at specified index
                localVariables[operands[currentStackFrame.Line]] = evaluationStack.pop();
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 20: // Ldnull: Push a null reference onto the stack
                evaluationStack.push(null);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 21: // Ldc_I4_M1: Load integer constant -1 onto the stack
                evaluationStack.push(-1);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 22: // Ldc_I4_0: Load integer constant 0 onto the stack
                evaluationStack.push(0);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 23: // Ldc_I4_1: Load integer constant 1 onto the stack
                evaluationStack.push(1);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 24: // Ldc_I4_2: Load integer constant 2 onto the stack
                evaluationStack.push(2);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 25: // Ldc_I4_3: Load integer constant 3 onto the stack
                evaluationStack.push(3);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 26: // Ldc_I4_4: Load integer constant 4 onto the stack
                evaluationStack.push(4);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 27: // Ldc_I4_5: Load integer constant 5 onto the stack
                evaluationStack.push(5);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 28: // Ldc_I4_6: Load integer constant 6 onto the stack
                evaluationStack.push(6);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 29: // Ldc_I4_7: Load integer constant 7 onto the stack
                evaluationStack.push(7);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 30: // Ldc_I4_8: Load integer constant 8 onto the stack
                evaluationStack.push(8);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 31: // Ldc_I4_S: Load 4-byte integer constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 32: // Ldc_I4: Load 8-byte integer constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 33: // Ldc_I8: Load 4-byte floating-point constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 34: // Ldc_R4: Load 8-byte floating-point constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 35: // Ldc_R8: Load 8-byte floating-point constant onto the stack
                evaluationStack.push(operands[currentStackFrame.Line]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 36: // Dup: Duplicate the value on top of the stack
                evaluationStack.push(evaluationStack[evaluationStack.length - 1]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 37: // Pop: Remove the value from the top of the stack
                evaluationStack.pop();
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 38: // Jmp
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 39: // Call          

                method = GlobalMetadata.Methods[operands[currentStackFrame.Line]];               
                

                if (method.IsGenericInstance)
                {
                    elementMethod = GlobalMetadata.Methods[method.ElementMethod];

                    method.Body       = elementMethod.Body;
                    method.Parameters = elementMethod.Parameters;
                    method.IsStatic   = elementMethod.IsStatic;
                }

                instructions = method.Body.Instructions;
                operands     = method.Body.Operands;


                evaluationStack = [];
                methodArguments = currentStackFrame.EvaluationStack;
                methodArgumentsOfset = methodArguments.length - method.Parameters.length;
                localVariables  = [];

                if (method.IsStatic === false)
                {
                      // 0: this
                    methodArgumentsOfset--;
                }
                
                currentStackFrame =
                {
                    Method: method,
                    Line: 0,

                    EvaluationStack: evaluationStack,
                    LocalVariables: localVariables,
                    MethodArguments: methodArguments,
                    MethodArgumentsOfset: methodArgumentsOfset
                };

                thread.CallStack.push(currentStackFrame);

                nextInstruction = instructions[currentStackFrame.Line];
               
                break;
            case 40: // Calli
                nextInstruction = instructions[++currentStackFrame.Line];
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

                nextInstruction = instructions[++currentStackFrame.Line];
                
                break;
            case 42: // Br_S
                currentStackFrame.Line = operands[currentStackFrame.Line];
                nextInstruction = instructions[currentStackFrame.Line];
                break;

            case 43: // Brfalse_S
                if (evaluationStack[evaluationStack.length - 1])
                {
                    nextInstruction = instructions[++currentStackFrame.Line];
                }
                else
                {
                    currentStackFrame.Line = operands[currentStackFrame.Line];

                    nextInstruction = instructions[currentStackFrame.Line];
                }
                evaluationStack.pop();          
                break;

            case 44: // Brtrue_S
                if (evaluationStack[evaluationStack.length - 1])
                {
                    currentStackFrame.Line = operands[currentStackFrame.Line];

                    nextInstruction = instructions[currentStackFrame.Line];
                }
                else
                {
                    nextInstruction = instructions[++currentStackFrame.Line];
                }
                evaluationStack.pop(); 
                break;
            case 45: // Beq_S
                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                if (typeof v0 === 'number')
                {
                    if (v0 === v1)
                    {
                        currentStackFrame.Line = operands[currentStackFrame.Line];

                        nextInstruction = instructions[currentStackFrame.Line];
                    }
                    else
                    {
                        nextInstruction = instructions[++currentStackFrame.Line];
                    }
                }
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 46: // Bge_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 47: // Bgt_S

                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                if (typeof v0 === 'number')
                {
                    if (v0 > v1)
                    {
                        currentStackFrame.Line = operands[currentStackFrame.Line];

                        nextInstruction = instructions[currentStackFrame.Line];
                    }
                    else
                    {
                        nextInstruction = instructions[++currentStackFrame.Line];
                    }
                }
                break;

            case 48: // Ble_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 49: // Blt_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 50: // Bne_Un_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 51: // Bge_Un_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 52: // Bgt_Un_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 53: // Ble_Un_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 54: // Blt_Un_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 55: // Br
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 56: // Brfalse
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 57: // Brtrue
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 58: // Beq
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 59: // Bge
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 60: // Bgt
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 61: // Ble
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 62: // Blt
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 63: // Bne_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 64: // Bge_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 65: // Bgt_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 66: // Ble_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 67: // Blt_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 68: // Switch
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 69: // Ldind_I1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 70: // Ldind_U1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 71: // Ldind_I2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 72: // Ldind_U2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 73: // Ldind_I4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 74: // Ldind_U4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 75: // Ldind_I8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 76: // Ldind_I
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 77: // Ldind_R4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 78: // Ldind_R8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 79: // Ldind_Ref
                v0 = evaluationStack.pop();

                evaluationStack.push(v0.Array[v0.Index]);

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 80: // Stind_Ref
                
                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                v0.Array[v0.Index] = v1;

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 81: // Stind_I1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 82: // Stind_I2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 83: // Stind_I4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 84: // Stind_I8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 85: // Stind_R4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 86: // Stind_R8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 87: // Add
                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                evaluationStack.push(v0 + v1);

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 88: // Sub
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 89: // Mul
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 90: // Div
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 91: // Div_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 92: // Rem
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 93: // Rem_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 94: // And
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 95: // Or

                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                evaluationStack.push(v0|v1);

                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 96: // Xor
                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                evaluationStack.push(v0 ^ v1);

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 97: // Shl
                nextInstruction = instructions[++currentStackFrame.Line];
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
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 100: // Neg
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 101: // Not
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 102: // Conv_I1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 103: // Conv_I2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 104: // Conv_I4
                
                evaluationStackIndex = evaluationStack.length - 1;

                item = evaluationStack[evaluationStackIndex];

                if (typeof item === 'number')
                {
                    if ( item  >= 0 )
                    {
                        if ( item > /*Int32.MaxValue*/2147483647 )
                        {
                            evaluationStack[evaluationStackIndex] = /*Int32.MinValue*/-2147483648;
                        }
                        else
                        {
                            evaluationStack[evaluationStackIndex] = item << 0;
                        }
                    }
                    else
                    {
                        if( item < /*Int32.MinValue*/-2147483648 )
                        {
                            evaluationStack[evaluationStackIndex] = /*Int32.MinValue*/-2147483648;
                        }
                        else
                        {
                            evaluationStack[evaluationStackIndex] = Math.ceil(item);
                        }
                    }
                }

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 105: // Conv_I8
                
                evaluationStackIndex = evaluationStack.length - 1;

                evaluationStack[evaluationStackIndex] = Long.fromNumber(evaluationStack[evaluationStackIndex]);
                
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 106: // Conv_R4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 107: // Conv_R8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 108: // Conv_U4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 109: // Conv_U8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 110: // Callvirt
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 111: // Cpobj
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 112: // Ldobj
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 113: // Ldstr
                evaluationStack.push(operands[currentStackFrame.Line]);
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 114: // Newobj
                
                method = GlobalMetadata.Methods[operands[currentStackFrame.Line]];

                declaringType = GlobalMetadata.Types[method.DeclaringType];

                var newObj = {};
                newObj['$type'] = declaringType;

                if (declaringType.IsGenericInstance)
                {
                    elementType = GlobalMetadata.Types[declaringType.ElementType];

                    for (var i = 0; i < elementType.Methods.length; i++)
                    {
                        var methodTemp = GlobalMetadata.Methods[elementType.Methods[i]];

                        if (methodTemp.IsDefinition)
                        {
                            if (methodTemp.Name === method.Name)
                            {
                                method.Body       = methodTemp.Body;
                                method.Parameters = methodTemp.Parameters;
                                method.IsStatic   = methodTemp.IsStatic;
                            } 
                        }
                    }
                }

                var tempArray = [];

                for (var i = 0; i < method.Parameters.length; i++)
                {
                    tempArray.push(evaluationStack.pop());
                }

                tempArray.push(newObj);
                tempArray.push(newObj);                

                while(tempArray.length > 0)
                {
                    evaluationStack.push(tempArray.pop());
                }

                instructions = method.Body.Instructions;
                operands     = method.Body.Operands;

                evaluationStack = [];
                methodArguments = currentStackFrame.EvaluationStack;
                methodArgumentsOfset = methodArguments.length - method.Parameters.length;
                methodArgumentsOfset--;
                localVariables  = [];
                
                currentStackFrame = 
                {
                    Method: method,
                    Line: 0,

                    EvaluationStack: evaluationStack,
                    LocalVariables: localVariables,
                    MethodArguments: methodArguments,
                    MethodArgumentsOfset: methodArgumentsOfset
                };

                thread.CallStack.push(currentStackFrame);

                nextInstruction = instructions[0];

                break;
            case 115: // Castclass
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 116: // Isinst
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 117: // Conv_R_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 118: // Unbox
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 119: // Throw
                throw evaluationStack.pop();
                break;
            case 120: // Ldfld
            
                fieldDefinitionOrMaybeNumber = operands[currentStackFrame.Line];
                if (typeof fieldDefinitionOrMaybeNumber === 'number')
                {
                    fieldDefinition = operands[currentStackFrame.Line] = GlobalMetadata.Fields[fieldDefinitionOrMaybeNumber];
                }
                
                /*instance*/v0 = evaluationStack.pop();

                evaluationStack.push(/*instance*/v0[fieldDefinition.Name]);

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 121: // Ldflda
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 122: // Stfld
                
                fieldDefinitionOrMaybeNumber = operands[currentStackFrame.Line];
                if (typeof fieldDefinitionOrMaybeNumber === 'number')
                {
                    fieldDefinition = operands[currentStackFrame.Line] = GlobalMetadata.Fields[fieldDefinitionOrMaybeNumber];
                }
                
                /*value*/v1    = evaluationStack.pop();
                /*instance*/v0 = evaluationStack.pop();

                /*instance*/v0[fieldDefinition.Name] = /*value*/v1;

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 123: // Ldsfld
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 124: // Ldsflda
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 125: // Stsfld
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 126: // Stobj
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 127: // Conv_Ovf_I1_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 128: // Conv_Ovf_I2_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 129: // Conv_Ovf_I4_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 130: // Conv_Ovf_I8_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 131: // Conv_Ovf_U1_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 132: // Conv_Ovf_U2_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 133: // Conv_Ovf_U4_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 134: // Conv_Ovf_U8_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 135: // Conv_Ovf_I_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 136: // Conv_Ovf_U_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 137: // Box
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 138: // Newarr
                evaluationStack.push(InterpreterBridge_NewArr);
                nextInstruction = InterpreterBridge_Jump;
                break;
            case 139: // Ldlen
                
                /*array*/v0 = evaluationStack.pop();

                evaluationStack.push(/*array*/v0.length);

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 140: // Ldelema
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 141: // Ldelem_I1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 142: // Ldelem_U1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 143: // Ldelem_I2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 144: // Ldelem_U2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 145: // Ldelem_I4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 146: // Ldelem_U4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 147: // Ldelem_I8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 148: // Ldelem_I
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 149: // Ldelem_R4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 150: // Ldelem_R8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 151: // Ldelem_Ref
              
                /*index*/v1 = evaluationStack.pop();
                /*array*/v0 = evaluationStack.pop();

                if (/*array*/v0.length <= /*index*/v1 || /*index*/v1 < 0)
                {
                    IndexOutOfRangeException(/*index*/v1);
                }
                                
                // todo check index

                evaluationStack.push(/*array*/v0[/*index*/v1]);

                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 152: // Stelem_I
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 153: // Stelem_I1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 154: // Stelem_I2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 155: // Stelem_I4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 156: // Stelem_I8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 157: // Stelem_R4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 158: // Stelem_R8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;
            case 159: // Stelem_Ref
                /*value*/v2 = evaluationStack.pop();
                /*index*/v1 = evaluationStack.pop();
                /*array*/v0 = evaluationStack.pop();

                // todo: check array type
                // todo check index

                /*array*/v0[/*index*/v1] = /*value*/v2;

                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 160: // Ldelem_Any
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 161: // Stelem_Any
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 162: // Unbox_Any
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 163: // Conv_Ovf_I1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 164: // Conv_Ovf_U1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 165: // Conv_Ovf_I2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 166: // Conv_Ovf_U2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 167: // Conv_Ovf_I4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 168: // Conv_Ovf_U4
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 169: // Conv_Ovf_I8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 170: // Conv_Ovf_U8
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 171: // Refanyval
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 172: // Ckfinite
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 173: // Mkrefany
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 174: // Ldtoken
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 175: // Conv_U2
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 176: // Conv_U1
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 177: // Conv_I
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 178: // Conv_Ovf_I
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 179: // Conv_Ovf_U
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 180: // Add_Ovf
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 181: // Add_Ovf_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 182: // Mul_Ovf
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 183: // Mul_Ovf_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 184: // Sub_Ovf
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 185: // Sub_Ovf_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 186: // Endfinally
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 187: // Leave
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 188: // Leave_S
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 189: // Stind_I
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 190: // Conv_U
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 191: // Arglist
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 192: // Ceq

                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                evaluationStack.push(v0 === v1);

                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 193: // Cgt
                        
                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                if (typeof v1 === 'number')
                {
                    evaluationStack.push(v1 < v0 ? 1 : 0);
                }
                else
                {
                    evaluationStack.push(v1.lessThan(v0) ? 1 : 0);
                }

                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 194: // Cgt_Un

                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                if (typeof v1 === 'number')
                {
                    evaluationStack.push(v1 < v0 ? 1 : 0);
                }
                else
                {
                    evaluationStack.push(v1.lessThan(v0) ? 1 : 0);
                }

                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 195: // Clt

                v1 = evaluationStack.pop();
                v0 = evaluationStack.pop();

                if (typeof v1 === 'number')
                {
                    evaluationStack.push(v0 < v1 ? 1 : 0);
                }
                else
                {
                    evaluationStack.push(v0.lessThan(v1) ? 1 : 0);
                }

                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 196: // Clt_Un
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 197: // Ldftn
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 198: // Ldvirtftn
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 199: // Ldarg
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 200: // Ldarga
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 201: // Starg
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 202: // Ldloc
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 203: // Ldloca
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 204: // Stloc
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 205: // Localloc
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 206: // Endfilter
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 207: // Unaligned
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 208: // Volatile
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 209: // Tail
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 210: // Initobj
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 211: // Constrained
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 212: // Cpblk
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 213: // Initblk
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 214: // No
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 215: // Rethrow
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 216: // Sizeof
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 217: // Refanytype
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 218: // Readonly
                nextInstruction = instructions[++currentStackFrame.Line];
                break;

            case 219: // Jump

                instructions = InterpreterBridge_Jump_MethodDefinition.Body.Instructions;
                operands     = InterpreterBridge_Jump_MethodDefinition.Body.Operands;

                evaluationStack = [];
                localVariables  = [];
                                
                currentStackFrame = 
                {
                    Method: InterpreterBridge_Jump_MethodDefinition,
                    Line: 0,

                    EvaluationStack: evaluationStack,
                    LocalVariables: localVariables
                };

                thread.CallStack.push(currentStackFrame);
                
                nextInstruction = instructions[currentStackFrame.Line];

                break;

            case 220: // CallJsNative
                switch(operands[currentStackFrame.Line])
                {
                    // set
                    case 0:
                    /*value*/v2    = evaluationStack.pop();
                    /*key*/v1      = evaluationStack.pop();
                    /*instance*/v0 = evaluationStack.pop();
                    /*instance*/v0[/*key*/v1] = /*value*/v2;
                    break;

                    // get
                    case 1:
                    /*key*/v1      = evaluationStack.pop();
                    /*instance*/v0 = evaluationStack.pop();
                    evaluationStack.push(/*instance*/v0[/*key*/v1]);                     
                    break;

                    // CreateNewPlainObject
                    case 2:
                    evaluationStack.push({});
                    break;

                    // CreateNewArray
                    case 3:
                    evaluationStack.push([]);
                    break;

                    // Sum
                    case 4:
                    /*b*/v1 = evaluationStack.pop();
                    /*a*/v0 = evaluationStack.pop();
                    evaluationStack.push(/*a*/v0 + /*b*/v1);
                    break;

                    case 5: // PreviousStackFrame
                    evaluationStack.push(thread.CallStack[thread.CallStack.length - 2]);
                    break;

                    // instance.Call('functionName')
                    case 6:

                        /*functionName*/v1 = evaluationStack.pop();
                        /*instance*/v0     = evaluationStack.pop();

                        if ( /*functionName*/v1 == null)
                        {
                            evaluationStack.push('functionName');
                            evaluationStack.push(InterpreterBridge_ArgumentNullException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }

                        if ( /*instance*/v0 == null)
                        {
                            evaluationStack.push(/*functionName*/v1 + ' not found.');
                            evaluationStack.push(InterpreterBridge_NullReferenceException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }
                        evaluationStack.push( /*instance*/v0[/*functionName*/v1]());

                    break;

                    // instance.Call('functionName', parameter1)
                    case 7:

                        /*parameter1*/v2   = evaluationStack.pop();
                        /*functionName*/v1 = evaluationStack.pop();
                        /*instance*/v0     = evaluationStack.pop();

                        if ( /*functionName*/v1 == null)
                        {
                            evaluationStack.push('functionName');
                            evaluationStack.push(InterpreterBridge_ArgumentNullException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }

                        if ( /*instance*/v0 == null)
                        {
                            evaluationStack.push(/*functionName*/v1 + ' not found.');
                            evaluationStack.push(InterpreterBridge_NullReferenceException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }
                        evaluationStack.push( /*instance*/v0[/*functionName*/v1](/*parameter1*/v2));

                    break;

                    // instance.Call('functionName', parameter1, parameter2)
                    case 8:

                        /*parameter2*/v3   = evaluationStack.pop();
                        /*parameter1*/v2   = evaluationStack.pop();
                        /*functionName*/v1 = evaluationStack.pop();
                        /*instance*/v0     = evaluationStack.pop();

                        if ( /*functionName*/v1 == null)
                        {
                            evaluationStack.push('functionName');
                            evaluationStack.push(InterpreterBridge_ArgumentNullException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }

                        if ( /*instance*/v0 == null)
                        {
                            evaluationStack.push(/*functionName*/v1 + ' not found.');
                            evaluationStack.push(InterpreterBridge_NullReferenceException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }
                        evaluationStack.push( /*instance*/v0[/*functionName*/v1](/*parameter1*/v2, /*parameter1*/v3));

                    break;

                    // instance.Call('functionName', parameter1, parameter2, parameter3)
                    case 9:

                        /*parameter3*/v4   = evaluationStack.pop();
                        /*parameter2*/v3   = evaluationStack.pop();
                        /*parameter1*/v2   = evaluationStack.pop();
                        /*functionName*/v1 = evaluationStack.pop();
                        /*instance*/v0     = evaluationStack.pop();

                        if ( /*functionName*/v1 == null)
                        {
                            evaluationStack.push('functionName');
                            evaluationStack.push(InterpreterBridge_ArgumentNullException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }

                        if ( /*instance*/v0 == null)
                        {
                            evaluationStack.push(/*functionName*/v1 + ' not found.');
                            evaluationStack.push(InterpreterBridge_NullReferenceException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }
                        evaluationStack.push( /*instance*/v0[/*functionName*/v1](/*parameter1*/v2, /*parameter1*/v3, /*parameter3*/v4));

                    break;

                    // instance.Call('functionName', parameters)
                    case 10:

                        /*parameters*/v2   = evaluationStack.pop();
                        /*functionName*/v1 = evaluationStack.pop();
                        /*instance*/v0     = evaluationStack.pop();

                        if ( /*functionName*/v1 == null)
                        {
                            evaluationStack.push('functionName');
                            evaluationStack.push(InterpreterBridge_ArgumentNullException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }

                        if ( /*instance*/v0 == null)
                        {
                            evaluationStack.push(/*functionName*/v1 + ' not found.');
                            evaluationStack.push(InterpreterBridge_NullReferenceException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }
                        evaluationStack.push( /*instance*/v0[/*functionName*/v1].apply(/*instance*/v0, /*parameters*/v2));

                    break;
                }
            
                nextInstruction = instructions[++currentStackFrame.Line];
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
        
        TryInitialize_InterpreterBridge(metadataTable);

        CallManagedStaticMethod(InterpreterBridge_ImportMetadata_MethodDefinition, [GlobalMetadata, metadataTable], console.log, console.log);


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

