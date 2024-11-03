
const GlobalMetadata =
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
const InterpreterBridge_Call = 3;


function AssertNumber(value)
{
    if (typeof value === 'number')
    {
        return;
    }

    throw 'AssertNumber: ' + value;
}

function AssertBoolean(value)
{
    if (typeof value === 'boolean')
    {
        return;
    }

    throw 'AssertBoolean: ' + value;
}

function AssertNotNull(value)
{
    if (value == null)
    {
        throw 'AssertNotNull';
    }
}


const InterpreterBridge_Jump = 219;

let InterpreterBridge_Jump_MethodDefinition;
let InterpreterBridge_ImportMetadata_MethodDefinition;

function TryInitialize_InterpreterBridge(metadataTable) 
{
    for (let i = 0; i < metadataTable.Methods.length; i++)
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

function NotImplementedOpCode()
{
    throw 'NotImplementedOpCode';
}
function Interpret(thread)
{
    let currentStackFrame = thread.LastFrame;

    let instructions = currentStackFrame.Method.Body.Instructions;
    let operands     = currentStackFrame.Method.Body.Operands;

    let evaluationStack = currentStackFrame.EvaluationStack;
    let localVariables  = currentStackFrame.LocalVariables;
    
    let methodArguments       = currentStackFrame.MethodArguments;
    let methodArgumentsOffset = currentStackFrame.MethodArgumentsOffset;

    let previousStackFrame, v0, v1, v2, v3, v4;

    let method;

    let nextInstruction = instructions[currentStackFrame.Line];

    while(true)
    {
        try
        {
            switch (nextInstruction)
            {
                case 0: // Nop: No operation
                {
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 1: // Break: Inform debugger of a break point
                {
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 2: // Ldarg_0: Load argument 0 onto the stack
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + 0]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 3: // Ldarg_1: Load argument 1 onto the stack
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + 1]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 4: // Ldarg_2: Load argument 2 onto the stack
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + 2]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 5: // Ldarg_3: Load argument 3 onto the stack
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + 3]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 6: // Ldloc_0: Load local variable 0 onto the stack
                {
                    evaluationStack.push(localVariables[0]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 7: // Ldloc_1: Load local variable 1 onto the stack
                {
                    evaluationStack.push(localVariables[1]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 8: // Ldloc_2: Load local variable 2 onto the stack
                {
                    evaluationStack.push(localVariables[2]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 9: // Ldloc_3: Load local variable 3 onto the stack
                {
                    evaluationStack.push(localVariables[3]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 10: // Stloc_0: Store value from the stack in local variable 0
                {
                    localVariables[0] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 11: // Stloc_1: Store value from the stack in local variable 1
                {
                    localVariables[1] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 12: // Stloc_2: Store value from the stack in local variable 2
                {
                    localVariables[2] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 13: // Stloc_3: Store value from the stack in local variable 3
                {
                    localVariables[3] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 14: // Ldarg_S: Load argument at a specified index (short form)
                {
                    evaluationStack.push(methodArguments[methodArgumentsOffset + operands[currentStackFrame.Line]]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 15: // Ldarga_S: Load address of argument at a specified index (short form)
                {
                    evaluationStack.push({
                        Array: methodArguments,
                        Index: methodArgumentsOffset + operands[currentStackFrame.Line]
                    });
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 16: // Starg_S: Store value from the stack into argument at specified index
                {
                    methodArguments[methodArgumentsOffset + operands[currentStackFrame.Line]] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 17: // Ldloc_S: Load local variable at a specified index (short form)
                {
                    evaluationStack.push(localVariables[operands[currentStackFrame.Line]]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 18: // Ldloca_S: Load address of local variable at a specified index (short)
                {
                    evaluationStack.push({
                        Array: localVariables,
                        Index: operands[currentStackFrame.Line]
                    });
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 19: // Stloc_S: Store value from the stack into local variable at specified index
                {
                    localVariables[operands[currentStackFrame.Line]] = evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 20: // Ldnull: Push a null reference onto the stack
                {
                    evaluationStack.push(null);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 21: // Ldc_I4_M1: Load integer constant -1 onto the stack
                {
                    evaluationStack.push(-1);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 22: // Ldc_I4_0: Load integer constant 0 onto the stack
                {
                    evaluationStack.push(0);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 23: // Ldc_I4_1: Load integer constant 1 onto the stack
                {
                    evaluationStack.push(1);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 24: // Ldc_I4_2: Load integer constant 2 onto the stack
                {
                    evaluationStack.push(2);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 25: // Ldc_I4_3: Load integer constant 3 onto the stack
                {
                    evaluationStack.push(3);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 26: // Ldc_I4_4: Load integer constant 4 onto the stack
                {
                    evaluationStack.push(4);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 27: // Ldc_I4_5: Load integer constant 5 onto the stack
                {
                    evaluationStack.push(5);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 28: // Ldc_I4_6: Load integer constant 6 onto the stack
                {
                    evaluationStack.push(6);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 29: // Ldc_I4_7: Load integer constant 7 onto the stack
                {
                    evaluationStack.push(7);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 30: // Ldc_I4_8: Load integer constant 8 onto the stack
                {
                    evaluationStack.push(8);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 31: // Ldc_I4_S: Load 4-byte integer constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 32: // Ldc_I4: Load 8-byte integer constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 33: // Ldc_I8: Load 4-byte floating-point constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 34: // Ldc_R4: Load 8-byte floating-point constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 35: // Ldc_R8: Load 8-byte floating-point constant onto the stack
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 36: // Dup: Duplicate the value on top of the stack
                {
                    evaluationStack.push(evaluationStack[evaluationStack.length - 1]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }    

                case 37: // Pop: Remove the value from the top of the stack
                {
                    evaluationStack.pop();
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 38: // Jmp
                {
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 39: // Call
                {
                    let method = GlobalMetadata.Methods[operands[currentStackFrame.Line]];

                    if (method.IsGenericInstance)
                    {
                        let elementMethod = GlobalMetadata.Methods[method.ElementMethod];

                        method.Body       = elementMethod.Body;
                        method.Parameters = elementMethod.Parameters;
                        method.IsStatic   = elementMethod.IsStatic;
                    }

                    // arrange opcodes
                    instructions = method.Body.Instructions;
                    operands = method.Body.Operands;

                    // arrange arguments
                    methodArguments = evaluationStack;
                    methodArgumentsOffset = evaluationStack.length - method.Parameters.length;
                    if (method.IsStatic === false)
                    {
                        // 0: this
                        methodArgumentsOffset--;
                    }

                    // arrange calculation stacks
                    evaluationStack = [];
                    localVariables = [];

                    // connect frame
                    currentStackFrame = thread.LastFrame =
                    {
                        Prev: thread.LastFrame,

                        Method: method,
                        Line: 0,

                        MethodArguments: methodArguments,
                        MethodArgumentsOffset: methodArgumentsOffset,

                        EvaluationStack: evaluationStack,
                        LocalVariables: localVariables
                    };

                    nextInstruction = instructions[0];                    
                    break;
                }                  

                case 40: // Calli
                {
                    NotImplementedOpCode(); break;
                }

                case 41: // Ret
                {
                    if (currentStackFrame.Prev === null)
                    {
                        return;
                    }

                    previousStackFrame = currentStackFrame;

                    thread.LastFrame = currentStackFrame = currentStackFrame.Prev;

                    // arrange fast access variables
                    instructions = currentStackFrame.Method.Body.Instructions;
                    operands = currentStackFrame.Method.Body.Operands;

                    // arrange fast access variables
                    evaluationStack = currentStackFrame.EvaluationStack;
                    localVariables = currentStackFrame.LocalVariables;
                    methodArguments = currentStackFrame.MethodArguments;
                    methodArgumentsOffset = currentStackFrame.MethodArgumentsOffset;

                    // remove parameters
                    let length = previousStackFrame.Method.Parameters.length;
                    while (length-- > 0)
                    {
                        evaluationStack.pop();
                    }

                    // remove instance
                    if (previousStackFrame.Method.IsStatic === false)
                    {
                        evaluationStack.pop();
                    }

                    // check has any return value
                    if (previousStackFrame.EvaluationStack.length === 1)
                    {
                        evaluationStack.push(previousStackFrame.EvaluationStack.pop());
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 42: // Br_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 55;
                    break;
                }
                
                case 43: // Brfalse_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 56;
                    break;
                }
                
                case 44: // Brtrue_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 57;
                    break;
                }
                
                case 45: // Beq_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 58;
                    break;
                }
                
                case 46: // Bge_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 59;
                    break;
                }

                case 47: // Bgt_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 60;
                    break;
                }                    

                case 48: // Ble_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 61;
                    break;
                }
                
                case 49: // Blt_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 62;
                    break;
                }
                
                case 50: // Bne_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 63;
                    break;
                }
                case 51: // Bge_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 64;
                    break;
                }
                case 52: // Bgt_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 65;
                    break;
                }
                case 53: // Ble_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 66;
                    break;
                }
                case 54: // Blt_Un_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 67;
                    break;
                }
                    
                case 55: // Br: Unconditionally transfers control to a target instruction.
                {
                    currentStackFrame.Line = operands[currentStackFrame.Line];
                    nextInstruction = instructions[currentStackFrame.Line];
                    break;
                }    
                
                case 56: // Brfalse: Transfers control to a target instruction if value is false, a null reference, or zero.
                {
                    let value = evaluationStack.pop();
                    if (value)
                    {
                        nextInstruction = instructions[++currentStackFrame.Line];
                    }
                    else
                    {
                        currentStackFrame.Line = operands[currentStackFrame.Line];

                        nextInstruction = instructions[currentStackFrame.Line];
                    }

                    break;
                }
                
                case 57: // Brtrue: Transfers control to a target instruction (short form) if value is true, not null, or non-zero.
                {
                    let value = evaluationStack.pop();
                    if (value)
                    {
                        currentStackFrame.Line = operands[currentStackFrame.Line];

                        nextInstruction = instructions[currentStackFrame.Line];
                    }
                    else
                    {
                        nextInstruction = instructions[++currentStackFrame.Line];
                    }

                    break;
                }
                
                case 58: // Beq: Transfers control to a target instruction if two values are equal.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 === value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 59: // Bge: Transfers control to a target instruction if the first value is greater than or equal to the second value.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 >= value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                    
                case 60: // Bgt: Transfers control to a target instruction if the first value is greater than the second value.
                {   
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 > value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 61: // Ble: Transfers control to a target instruction if the first value is less than or equal to the second value.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 <= value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 62: // Blt: Transfers control to a target instruction if the first value is less than the second value.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value0 < value1)
                        {
                            currentStackFrame.Line = operands[currentStackFrame.Line];

                            nextInstruction = instructions[currentStackFrame.Line];
                        }
                        else
                        {
                            nextInstruction = instructions[++currentStackFrame.Line];
                        }

                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 63: // Bne_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 64: // Bge_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 65: // Bgt_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 66: // Ble_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 67: // Blt_Un
                {
                    NotImplementedOpCode(); break;
                }
                
                case 68: // Switch: Implements a jump table.
                {
                    let jumpTable = operands[currentStackFrame.Line];
                    
                    let caseIndex = evaluationStack.pop();
                    
                    if ( caseIndex > 0 && jumpTable.length > caseIndex)
                    {
                        currentStackFrame.Line = jumpTable[caseIndex];

                        nextInstruction = instructions[currentStackFrame.Line];
                    }
                    else
                    {
                        nextInstruction = instructions[++currentStackFrame.Line];
                    }
                    
                    break;
                }
                    
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
                    v1 = evaluationStack.pop();
                    v0 = evaluationStack.pop();

                    evaluationStack.push(v0 - v1);

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
                {
                    let value = evaluationStack.pop();
                    
                    if (typeof value === 'number')
                    {
                        if (value >= 0)
                        {
                            if (value > /*Int32.MaxValue*/2147483647)
                            {
                                value = /*Int32.MinValue*/ -2147483648;
                            }
                            else
                            {
                                value = value << 0;
                            }
                        }
                        else
                        {
                            if (value < /*Int32.MinValue*/ -2147483648)
                            {
                                value = /*Int32.MinValue*/ -2147483648;
                            }
                            else
                            {
                                value = Math.ceil(value);
                            }
                        }
                    }

                    evaluationStack.push(value);
                    
                    nextInstruction = instructions[++currentStackFrame.Line];
                    
                    break;
                }
                
                case 105: // Conv_I8
                {
                    let value = evaluationStack.pop();

                    evaluationStack.push(Long.fromNumber(value));

                    nextInstruction = instructions[++currentStackFrame.Line];
                    
                    break;
                }
                
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
                {
                    let method = GlobalMetadata.Methods[operands[currentStackFrame.Line]];
                
                    let methodParameterCount = method.Parameters.length;

                    // arrange arguments
                    methodArguments      = evaluationStack;
                    methodArgumentsOffset = evaluationStack.length - methodParameterCount - 1;

                    let instance = methodArguments[methodArgumentsOffset];

                    AssertNotNull(instance.$type);

                    // find target method

                    let targetMethod = null;
                    {                    
                        let instanceMethods = instance.$type.Methods;
                        let instanceMethodsLength = instanceMethods.length;

                        for (let i = 0; i < instanceMethodsLength; i++)
                        {
                            targetMethod = GlobalMetadata.Methods[instanceMethods[i]];

                            if (targetMethod.Name !== method.Name)
                            {
                                continue;
                            }
                    
                            if (IsTwoParameterListFullMatch(method, targetMethod) === false)
                            {
                                continue;
                            }

                            method = targetMethod;
                            break;
                        }
                    }

                    if (method !== targetMethod)
                    {
                        throw 'MissingMethodException' + method.Name;
                    }


                    // arrange opcodes
                    instructions = method.Body.Instructions;
                    operands     = method.Body.Operands;
                                
                    // arrange calculation stacks
                    evaluationStack = [];
                    localVariables  = [];
                              
                    // connect frame
                    currentStackFrame = thread.LastFrame =
                    {
                        Prev: thread.LastFrame,

                        Method: method,
                        Line: 0,

                        MethodArguments: methodArguments,
                        MethodArgumentsOffset: methodArgumentsOffset,

                        EvaluationStack: evaluationStack,
                        LocalVariables: localVariables                    
                    };
                    nextInstruction = instructions[0];
                    break;
                }   
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

                        for (let i = 0; i < elementType.Methods.length; i++)
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
                    methodArgumentsOffset = methodArguments.length - method.Parameters.length;
                    methodArgumentsOffset--;
                    localVariables  = [];
                
                    currentStackFrame = thread.LastFrame =
                    {
                        Prev: thread.LastFrame,

                        Method: method,
                        Line: 0,

                        EvaluationStack: evaluationStack,
                        LocalVariables: localVariables,
                        MethodArguments: methodArguments,
                        MethodArgumentsOffset: methodArgumentsOffset
                    };

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
                {
                    ++currentStackFrame.Line;

                    throw evaluationStack.pop();
                }

                case 120: // Ldfld
                {
                    let fieldReference = GlobalMetadata.Fields[operands[currentStackFrame.Line]];
                                
                    let instance = evaluationStack.pop();

                    if (instance == null)
                    {
                        // NullReferenceException
                        evaluationStack.push(fieldReference.Name);
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }


                    let value = instance[fieldReference.Name];

                    if (typeof value === 'boolean')
                    {
                        value = value ? 1 : 0;
                    }

                    evaluationStack.push(value);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                case 121: // Ldflda
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                case 122: // Stfld
                {    
                    let fieldReference = GlobalMetadata.Fields[operands[currentStackFrame.Line]];
                                    
                    let value    = evaluationStack.pop();
                    let instance = evaluationStack.pop();

                    if (instance == null)
                    {
                        // NullReferenceException
                        evaluationStack.push(fieldReference.Name);
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    instance[fieldReference.Name] = value;

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
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
                {
                    // branch
                    currentStackFrame.Line = operands[currentStackFrame.Line];
                    nextInstruction = instructions[currentStackFrame.Line];

                    break;
                }  

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
                {                
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    AssertNumber(value1);
                    AssertNumber(value0);

                    evaluationStack.push(value0 === value1);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

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
                                
                    currentStackFrame = thread.LastFrame = 
                    {
                        Prev: thread.LastFrame,

                        Method: InterpreterBridge_Jump_MethodDefinition,
                        Line: 0,

                        EvaluationStack: evaluationStack,
                        LocalVariables: localVariables
                    };
                                
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
                        evaluationStack.push(currentStackFrame.Prev);
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

                        case 11: // GlobalMetadata
                            evaluationStack.push(GlobalMetadata);
                            break;

                        case 12: // array.push
                            /*item*/v1 = evaluationStack.pop();
                            /*arrayInstance*/v0 = evaluationStack.pop();

                            if ( /*arrayInstance*/v0 == null)
                            {
                                evaluationStack.push('array is null. when call push');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            /*array*/v0.push(/*item*/v1);
                            break;

                        case 13: // array.pop
                        {
                            let arrayInstance = evaluationStack.pop();

                            if ( arrayInstance == null)
                            {
                                evaluationStack.push('array is null. when call push');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            evaluationStack.push(arrayInstance.pop());
                            break;
                        }    
                        case 14: // Dump
                            console.log(evaluationStack.pop());
                            break;

                        case 15: // CurrentStackFrame
                            evaluationStack.push(currentStackFrame);
                            break;

                    }
            
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;

            }   
        } 
        catch (exception)
        {
            let isExceptionHandled = false;

            let exceptionStack = [];

            // buble up exception
            while(true)
            {
                exceptionStack.push(currentStackFrame);

                let isInHandlerRange = false;

                let exceptionHandlers = currentStackFrame.Method.Body.ExceptionHandlers;
                let exceptionHandlersLength = exceptionHandlers.length;

                for (let i = 0; i < exceptionHandlersLength; i++)
                {
                    let exceptionHandler = exceptionHandlers[i];

                    // is in range
                    if (exceptionHandler.HandlerStart <= currentStackFrame.Line && 
                        exceptionHandler.HandlerEnd >= currentStackFrame.Line)
                    {
                        isInHandlerRange = true;

                        // no need to go previous method
                        if (exceptionStack.length === 1)
                        {
                            evaluationStack.push(exception);

                            currentStackFrame.Line = exceptionHandler.HandlerStart;
                            nextInstruction = instructions[currentStackFrame.Line];
                            break;
                        }

                        // arrange stack frame
                        previousStackFrame = currentStackFrame;

                        thread.LastFrame = currentStackFrame = currentStackFrame.Prev;

                        // arrange fast access variables
                        instructions = currentStackFrame.Method.Body.Instructions;
                        operands     = currentStackFrame.Method.Body.Operands;

                        // arrange fast access variables
                        evaluationStack      = currentStackFrame.EvaluationStack;
                        localVariables       = currentStackFrame.LocalVariables;
                        methodArguments      = currentStackFrame.MethodArguments;
                        methodArgumentsOffset = currentStackFrame.MethodArgumentsOffset;
                
                        // remove parameters
                        length = previousStackFrame.Method.Parameters.length;
                        while(length-- > 0)
                        {
                            evaluationStack.pop();
                        }

                        // remove instance
                        if (previousStackFrame.Method.IsStatic === false)
                        {
                            evaluationStack.pop();
                        }

                        // check has any return value
                        if(previousStackFrame.EvaluationStack.length === 1)
                        {
                            evaluationStack.push(previousStackFrame.EvaluationStack.pop());
                        }

                        nextInstruction = instructions[++currentStackFrame.Line];

                        break;
                    }
                }

                if (isInHandlerRange)
                {
                    isExceptionHandled = true;
                    break;
                }
            }

            if (!isExceptionHandled)
            {
                throw exception;
            }
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
        LastFrame: 
        {
            Method: methodDefinition,
            EvaluationStack:[],
            LocalVariables:[],
            MethodArguments: args,
            MethodArgumentsOffset: 0,
            Line: 0,
            Prev: null
        }
    };

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
        if (thread.LastFrame.EvaluationStack.length > 0) 
        {
            const returnValue = thread.LastFrame.EvaluationStack.pop();
            
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
    
}, 2000);


const CLR =
{

};
window.CLR = CLR;

function IsTwoParameterListFullMatch(parametersA, parametersB)
{
    let lengthA = parametersA.length;
    let lengthB = parametersB.length;

    if (lengthA !== lengthB)
    {
        return false;
    }

    for (let i = 0; i < lengthA; i++)
    {
        if (parametersA[i].ParameterType !== parametersB[i].ParameterType)
        {
            return false;
        }
    }

    return true;
}

