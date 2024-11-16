
const GlobalMetadata =
{
    MetadataScopes: [],
    Types: [ { IsValueType: 0 }],
    Fields: [],
    Methods: [],
    Properties: [],
    Events: []
};

GlobalMetadata.Types.pop();

const InterpreterBridge_NewArr = 0;
const InterpreterBridge_NullReferenceException = 1;
const InterpreterBridge_ArgumentNullException = 2;
const InterpreterBridge_DivideByZeroException = 3;
const InterpreterBridge_IndexOutOfRangeException = 4;


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

                    // arrange arguments
                    methodArguments = evaluationStack;
                    methodArgumentsOffset = evaluationStack.length - method.Parameters.length;
                    if (method.IsStatic === false)
                    {
                        // 0: this
                        methodArgumentsOffset--;
                    }

                    // arrange opcodes
                    instructions = method.Body.Instructions;
                    operands = method.Body.Operands;
                    
                    // maybe external method
                    if (instructions.length === 0)
                    {
                        let declaringType = GlobalMetadata.Types[method.DeclaringType];
                        
                        let isDeclaringTypeExternal = false;
                        {
                            let customAttributes = declaringType.CustomAttributes;

                            let length = customAttributes.length;

                            for (let i= 0; i < length; i++)
                            {
                                let attribute = customAttributes[i];

                                let constructor = GlobalMetadata.Methods[attribute.Constructor];

                                let declaringTypeOfConstructor = GlobalMetadata.Types[constructor.DeclaringType];
                                if (declaringTypeOfConstructor.Name === 'ExternalAttribute' &&
                                    declaringTypeOfConstructor.Namespace === 'ReactWithDotNet')
                                {
                                    isDeclaringTypeExternal = true;
                                    break;
                                }
                            }
                        }
                        
                        if (isDeclaringTypeExternal === true)
                        {
                            let instance = null;
                            
                            let fnArguments = [];
                            
                            // move arguments to fnArguments
                            {
                                let length = method.Parameters.length;
                                while (length-- > 0)
                                {
                                    fnArguments.push(evaluationStack.pop());
                                }

                                fnArguments.reverse();
                            }

                            let isVoid = false;
                            {
                                let returnType = GlobalMetadata.Types[method.ReturnType];
                                
                                isVoid = returnType.Namespace === 'System' && returnType.Name === 'Void';
                            }

                            if (method.IsStatic === false)
                            {
                                instance = evaluationStack.pop();
                            }
                            
                            let fn = null;
                            
                            if (method.IsStatic === true)
                            {
                                let externalType = window[declaringType.Name];

                                fn = externalType[method.Name];                                
                            }
                            
                            if (fn == null)
                            {
                                throw new Error(`Missing function:'${method.Name}'.`);
                            }

                            let fnResult = fn.apply(null, fnArguments);

                            if (isVoid === false)
                            {
                                evaluationStack.push(fnResult);
                            }
                            
                            // arrange fast access variables
                            instructions = currentStackFrame.Method.Body.Instructions;
                            operands = currentStackFrame.Method.Body.Operands;

                            // arrange fast access variables
                            methodArguments = currentStackFrame.MethodArguments;
                            methodArgumentsOffset = currentStackFrame.MethodArgumentsOffset;

                            nextInstruction = instructions[++currentStackFrame.Line];
                            break;
                        }                                          
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

                    let previousStackFrame = currentStackFrame;

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
                case 63: // Bne_Un: Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        // use >>> for support UInt32
                        // NaN >>> 0 is 0 and NaN === NaN is false
                        // Infinity >>> 0 and Infinity === Infinity is true
                        
                        if (value0 === value1 || value0 >>> 0 === value1 >>> 0)
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
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 70: // Ldind_U1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 71: // Ldind_I2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 72: // Ldind_U2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 73: // Ldind_I4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 74: // Ldind_U4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 75: // Ldind_I8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 76: // Ldind_I
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 77: // Ldind_R4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 78: // Ldind_R8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 79;
                    break;
                }
                case 79: // Ldind_Ref: Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.
                {
                    let address = evaluationStack.pop();
                    
                    evaluationStack.push(address.Array[address.Index]);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 80: // Stind_Ref: Stores a object reference value at a supplied address.
                {
                    let value   = evaluationStack.pop();
                    let address = evaluationStack.pop();

                    address.Array[address.Index] = value;

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                    
                case 81: // Stind_I1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 82: // Stind_I2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 83: // Stind_I4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 84: // Stind_I8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 85: // Stind_R4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                case 86: // Stind_R8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 80;
                    break;
                }
                
                case 87: // Add
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 + value1);
                        
                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 88: // Sub
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 - value1);
                        
                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 89: // Mul
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 * value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 90: // Div
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        if (value1 === 0)
                        {
                            evaluationStack.push(value0 + ' cannot divide by zero');
                            evaluationStack.push(InterpreterBridge_DivideByZeroException);
                            nextInstruction = InterpreterBridge_Jump;
                            break;
                        }
                        
                        evaluationStack.push(value0 / value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 91: // Div_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 92: // Rem
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 % value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 93: // Rem_Un
                {
                    NotImplementedOpCode(); break;
                }
                
                case 94: // And
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 & value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }

                case 95: // Or
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 | value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }

                case 96: // Xor
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 ^ value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 97: // Shl
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 << value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 98: // Shr
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 >> value1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 99: // Shr_Un
                {
                    NotImplementedOpCode(); break;
                }
                
                case 100: // Neg
                {
                    let value = evaluationStack.pop();

                    if (typeof value === 'number')
                    {
                        evaluationStack.push(value * -1);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 101: // Not
                {
                    let value = evaluationStack.pop();

                    if (typeof value === 'number')
                    {
                        evaluationStack.push(~value);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                
                case 102: // Conv_I1
                {
                    let value = evaluationStack.pop();
    
                    if (typeof value === 'number')
                    {
                        if (value > /*Int32.MaxValue*/2147483647 || value < /*Int32.MinValue*/ -2147483648)
                        {
                            value = 0;
                        }
                        else
                        {
                            value = /*Number_To_Int8*/~~(value << 24) >> 24;
                        }
                        
                        evaluationStack.push(value);
                        
                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }
                case 103: // Conv_I2
                {
                    let value = evaluationStack.pop();

                    if (typeof value === 'number')
                    {
                        if (value > /*Int32.MaxValue*/2147483647 || value < /*Int32.MinValue*/ -2147483648)
                        {
                            value = 0;
                        }
                        else
                        {
                            value = /*Number_To_Int16*/~~(value << 16) >> 16;
                        }

                        evaluationStack.push(value);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    NotImplementedOpCode(); break;
                }

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

                        evaluationStack.push(value);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }

                    NotImplementedOpCode(); break;                   
                }
                
                case 105: // Conv_I8
                {
                    let value = evaluationStack.pop();

                    evaluationStack.push(Long.fromNumber(value));

                    nextInstruction = instructions[++currentStackFrame.Line];                    
                    break;
                }
                
                case 106: // Conv_R4
                {
                    NotImplementedOpCode(); break;
                }
                case 107: // Conv_R8
                {
                    NotImplementedOpCode(); break;
                }
                case 108: // Conv_U4
                {
                    NotImplementedOpCode(); break;
                }
                case 109: // Conv_U8
                {
                    NotImplementedOpCode(); break;
                }
                case 110: // Callvirt
                {
                    let method = GlobalMetadata.Methods[operands[currentStackFrame.Line]];
                
                    let methodParameterCount = method.Parameters.length;

                    // arrange arguments
                    methodArguments      = evaluationStack;
                    methodArgumentsOffset = evaluationStack.length - methodParameterCount - 1;

                    let instance = methodArguments[methodArgumentsOffset];

                    if (instance == null)
                    {
                        // NullReferenceException
                        evaluationStack.push(method.Name);
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }
                    
                    AssertNotNull(instance.$typeIndex);

                    // find target method

                    let targetMethod = null;
                    {                    
                        let instanceMethods = GlobalMetadata.Types[instance.$typeIndex].Methods;
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
                {
                    NotImplementedOpCode(); break;
                }
                case 112: // Ldobj
                {
                    NotImplementedOpCode(); break;
                }
                case 113: // Ldstr
                {
                    evaluationStack.push(operands[currentStackFrame.Line]);
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 114: // Newobj
                {
                    let method = GlobalMetadata.Methods[operands[currentStackFrame.Line]];

                    let declaringType = GlobalMetadata.Types[method.DeclaringType];

                    if (declaringType.IsGenericInstance)
                    {
                        let elementType = GlobalMetadata.Types[declaringType.ElementType];

                        for (let i = 0; i < elementType.Methods.length; i++)
                        {
                            let methodTemp = GlobalMetadata.Methods[elementType.Methods[i]];

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

                    if (!method.IsDefinition)
                    {
                        // Load method at runtime
                        evaluationStack.push(method);
                        nextInstruction = 222;
                        break;
                    }
                    
                    let newObj = {};
                    newObj.$typeIndex = method.DeclaringType;
                    
                    let tempArray = [];

                    for (let i = 0; i < method.Parameters.length; i++)
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
                }
                    
                case 115: // Castclass
                {
                    NotImplementedOpCode(); break;
                }
                case 116: // Isinst
                {
                    let value = evaluationStack.pop();
                    if ( value == null)
                    {
                        evaluationStack.push(null);
                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }

                    if (value.$isBox)
                    {
                        if (operands[currentStackFrame.Line] === value.$typeIndex)
                        {
                            evaluationStack.push(value.rawValue);
                            nextInstruction = instructions[++currentStackFrame.Line];
                            break;
                        }
                    }
                    
                    NotImplementedOpCode(); break;
                }
                case 117: // Conv_R_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 118: // Unbox
                {
                    NotImplementedOpCode(); break;
                }
                case 119: // Throw: Throws the exception object currently on the evaluation stack.
                {
                    ++currentStackFrame.Line;

                    nextInstruction = 221;
                    
                    break;
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
                {
                    let fieldReference = GlobalMetadata.Fields[operands[currentStackFrame.Line]];
                    
                    let instance = evaluationStack.pop();

                    evaluationStack.push({
                        Array: instance,
                        Index: fieldReference.Name
                    });
                    
                    break;
                }
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
                {
                    NotImplementedOpCode(); break;
                }
                case 124: // Ldsflda
                {
                    NotImplementedOpCode(); break;
                }
                case 125: // Stsfld
                {
                    NotImplementedOpCode(); break;
                }
                case 126: // Stobj
                {
                    NotImplementedOpCode(); break;
                }
                case 127: // Conv_Ovf_I1_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 128: // Conv_Ovf_I2_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 129: // Conv_Ovf_I4_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 130: // Conv_Ovf_I8_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 131: // Conv_Ovf_U1_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 132: // Conv_Ovf_U2_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 133: // Conv_Ovf_U4_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 134: // Conv_Ovf_U8_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 135: // Conv_Ovf_I_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 136: // Conv_Ovf_U_Un
                {
                    NotImplementedOpCode(); break;
                }
                case 137: // Box
                {
                    let value = evaluationStack.pop();
                    
                    evaluationStack.push({
                        $isBox: 1,
                        $typeIndex: operands[currentStackFrame.Line],
                        rawValue: value                        
                    });

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                case 138: // Newarr
                {
                    evaluationStack.push(InterpreterBridge_NewArr);
                    nextInstruction = InterpreterBridge_Jump;
                    break;
                }
                case 139: // Ldlen
                {
                    let array = evaluationStack.pop();

                    evaluationStack.push(array.length);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                    
                case 140: // Ldelema
                {
                    NotImplementedOpCode(); break;
                }
                case 141: // Ldelem_I1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 142: // Ldelem_U1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 143: // Ldelem_I2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 144: // Ldelem_U2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 145: // Ldelem_I4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 146: // Ldelem_U4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 147: // Ldelem_I8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 148: // Ldelem_I
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 149: // Ldelem_R4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 150: // Ldelem_R8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 151;
                    break;
                }
                case 151: // Ldelem_Ref
                {
                    let index = evaluationStack.pop();
                    let array = evaluationStack.pop();

                    if ( array == null)
                    {
                        evaluationStack.push('@array is null when load index');
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }
                    
                    if (array.length <= index || index < 0)
                    {
                        evaluationStack.push('@array.length:' + array.length + ' @index: ' + index);
                        evaluationStack.push(InterpreterBridge_IndexOutOfRangeException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    evaluationStack.push(array[index]);

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                    
                case 152: // Stelem_I
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 153: // Stelem_I1
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 154: // Stelem_I2
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 155: // Stelem_I4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 156: // Stelem_I8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 157: // Stelem_R4
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 158: // Stelem_R8
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 159;
                    break;
                }
                case 159: // Stelem_Ref
                {
                    let value = evaluationStack.pop();
                    let index = evaluationStack.pop();
                    let array = evaluationStack.pop();

                    if ( array == null)
                    {
                        evaluationStack.push('@array is null when set index');
                        evaluationStack.push(InterpreterBridge_NullReferenceException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    if (array.length <= index || index < 0)
                    {
                        evaluationStack.push('@array.length:' + array.length + ' @index: ' + index);
                        evaluationStack.push(InterpreterBridge_IndexOutOfRangeException);
                        nextInstruction = InterpreterBridge_Jump;
                        break;
                    }

                    array[index] = value;

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 160: // Ldelem_Any
                {
                    NotImplementedOpCode(); break;
                }

                case 161: // Stelem_Any
                {
                    NotImplementedOpCode(); break;
                }

                case 162: // Unbox_Any
                {
                    let box = evaluationStack.pop();
                    if ( box == null)
                    {
                        NotImplementedOpCode(); break;
                    }

                    if (box.$isBox)
                    {
                        if (operands[currentStackFrame.Line] === box.$typeIndex)
                        {
                            evaluationStack.push(box.rawValue);
                            nextInstruction = instructions[++currentStackFrame.Line];
                            break;
                        }
                    }
                    
                    NotImplementedOpCode(); break;
                }

                case 163: // Conv_Ovf_I1
                {
                    NotImplementedOpCode(); break;
                }

                case 164: // Conv_Ovf_U1
                {
                    NotImplementedOpCode(); break;
                }

                case 165: // Conv_Ovf_I2
                {
                    NotImplementedOpCode(); break;
                }

                case 166: // Conv_Ovf_U2
                {
                    NotImplementedOpCode(); break;
                }

                case 167: // Conv_Ovf_I4
                {
                    NotImplementedOpCode(); break;
                }

                case 168: // Conv_Ovf_U4
                {
                    NotImplementedOpCode(); break;
                }

                case 169: // Conv_Ovf_I8
                {
                    NotImplementedOpCode(); break;
                }

                case 170: // Conv_Ovf_U8
                {
                    NotImplementedOpCode(); break;
                }

                case 171: // Refanyval
                {
                    NotImplementedOpCode(); break;
                }

                case 172: // Ckfinite
                {
                    NotImplementedOpCode(); break;
                }

                case 173: // Mkrefany
                {
                    NotImplementedOpCode(); break;
                }

                case 174: // Ldtoken
                {
                    NotImplementedOpCode(); break;
                }

                case 175: // Conv_U2
                {
                    NotImplementedOpCode(); break;
                }

                case 176: // Conv_U1
                {
                    NotImplementedOpCode(); break;
                }

                case 177: // Conv_I
                {
                    NotImplementedOpCode(); break;
                }

                case 178: // Conv_Ovf_I
                {
                    NotImplementedOpCode(); break;
                }

                case 179: // Conv_Ovf_U
                {
                    NotImplementedOpCode(); break;
                }

                case 180: // Add_Ovf
                {
                    NotImplementedOpCode(); break;
                }

                case 181: // Add_Ovf_Un
                {
                    NotImplementedOpCode(); break;
                }

                case 182: // Mul_Ovf
                {
                    NotImplementedOpCode(); break;
                }

                case 183: // Mul_Ovf_Un
                {
                    NotImplementedOpCode(); break;
                }

                case 184: // Sub_Ovf
                {
                    NotImplementedOpCode(); break;
                }

                case 185: // Sub_Ovf_Un
                {
                    NotImplementedOpCode(); break;
                }

                case 186: // Endfinally
                {
                    if( thread.ExceptionObjectThatMustThrownWhenExitFinallyBlock !== null )
                    {
                        let exception = thread.ExceptionObjectThatMustThrownWhenExitFinallyBlock;
                        thread.ExceptionObjectThatMustThrownWhenExitFinallyBlock = null;
                        
                        throw exception;
                    }
                    
                    let line = currentStackFrame.Line;
                    
                    if (thread.LeaveJumpIndex !== null)
                    {
                        line = thread.LeaveJumpIndex;
                        thread.LeaveJumpIndex = null;
                    }
                    else
                    {
                        line++;
                    }

                    // branch
                    currentStackFrame.Line = line;
                    nextInstruction = instructions[line];
                    
                    break;
                }

                case 187: // Leave: Exits a protected region of code, unconditionally transferring control to a specific target instruction.
                {   
                    const Catch = 0;
                    const Finally = 2;
                    
                    let exceptionHandlers = currentStackFrame.Method.Body.ExceptionHandlers;
                    let exceptionHandlersLength = exceptionHandlers.length;
                    
                    let line = currentStackFrame.Line;
                    
                    let finallyFound = false;

                    for (let i = 0; i < exceptionHandlersLength; i++)
                    {
                        let exceptionHandler = exceptionHandlers[i];
                        
                        if (exceptionHandler.HandlerType === Catch)
                        {
                            // is successfully passed protected region code
                            if( line + 1 === exceptionHandler.HandlerStart  )
                            {
                                break;
                            }
                        }
                        else if (exceptionHandler.HandlerType === Finally)
                        {
                            if( line <= exceptionHandler.HandlerEnd)
                            {
                                if( line + 1 >= exceptionHandler.HandlerStart)
                                {
                                    thread.LeaveJumpIndex = operands[line];
                                    finallyFound = true;
                                    line = exceptionHandler.HandlerStart;
                                    break;
                                }
                            }
                        }
                    }

                    if( finallyFound === true )
                    {
                        currentStackFrame.Line = line;
                        nextInstruction = instructions[currentStackFrame.Line];
                    }
                    else
                    {
                        // nextInstruction = instructions[++currentStackFrame.Line];

                        // branch
                        currentStackFrame.Line = operands[line];
                        nextInstruction = instructions[currentStackFrame.Line];
                    }

                    break;
                }

                case 188: // Leave_S
                {
                    instructions[currentStackFrame.Line] = nextInstruction = 187;
                    break;                   
                }  

                case 189: // Stind_I
                {
                    NotImplementedOpCode(); break;
                }

                case 190: // Conv_U
                {
                    NotImplementedOpCode(); break;
                }

                case 191: // Arglist
                {
                    NotImplementedOpCode(); break;
                }

                case 192: // Ceq: Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number' ||
                        typeof value0 === 'string' && typeof value1 === 'string')
                    {
                        evaluationStack.push(value0 === value1 ? 1 : 0);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    
                    if (Long.isLong(value0) && Long.isLong(value1))
                    {
                        evaluationStack.push(value0.compare( value1 ) === 0 ? 1 : 0);

                        nextInstruction = instructions[++currentStackFrame.Line];
                        break;
                    }
                    
                    NotImplementedOpCode(); break;
                }

                case 193: // Cgt: If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value1 < value0 ? 1 : 0);
                    }
                    else
                    {
                        evaluationStack.push(value1.lessThan(value0) ? 1 : 0);
                    }
                    
                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 194: // Cgt_Un
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value1 < value0 ? 1 : 0);
                    }
                    else
                    {
                        evaluationStack.push(value1.lessThan(value0) ? 1 : 0);
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 195: // Clt
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 < value1 ? 1 : 0);
                    }
                    else
                    {
                        evaluationStack.push(value0.lessThan(value1) ? 1 : 0);
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 196: // Clt_Un
                {
                    let value1 = evaluationStack.pop();
                    let value0 = evaluationStack.pop();

                    if (typeof value0 === 'number' && typeof value1 === 'number')
                    {
                        evaluationStack.push(value0 < value1 ? 1 : 0);
                    }
                    else
                    {
                        evaluationStack.push(value0.lessThan(value1) ? 1 : 0);
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 197: // Ldftn
                {
                    NotImplementedOpCode(); break;
                }

                case 198: // Ldvirtftn
                {
                    NotImplementedOpCode(); break;
                }

                case 199: // Ldarg
                {
                    NotImplementedOpCode(); break;
                }

                case 200: // Ldarga
                {
                    NotImplementedOpCode(); break;
                }

                case 201: // Starg
                {
                    NotImplementedOpCode(); break;
                }

                case 202: // Ldloc
                {
                    NotImplementedOpCode(); break;
                }

                case 203: // Ldloca
                {
                    NotImplementedOpCode(); break;
                }

                case 204: // Stloc
                {
                    NotImplementedOpCode(); break;
                }

                case 205: // Localloc
                {
                    NotImplementedOpCode(); break;
                }

                case 206: // Endfilter
                {
                    NotImplementedOpCode(); break;
                }

                case 207: // Unaligned
                {
                    NotImplementedOpCode(); break;
                }

                case 208: // Volatile
                {
                    NotImplementedOpCode(); break;
                }

                case 209: // Tail
                {
                    NotImplementedOpCode(); break;
                }

                case 210: // Initobj: Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.
                {
                    //The address is popped from the stack; the value type object at the specified address is initialized as type
                    
                    let typeIndex = operands[currentStackFrame.Line];
                    
                    let typeReference = GlobalMetadata.Types[typeIndex];

                    let address = evaluationStack.pop();
                    
                    if (!typeReference.IsValueType)
                    {                
                        address.Array[address.Index] = null;
                    }
                    else
                    {
                        let newObj =
                        {
                            $typeIndex: typeIndex
                        };

                        address.Array[address.Index] = newObj;
                    }                    

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }

                case 211: // Constrained
                {
                    NotImplementedOpCode(); break;
                }

                case 212: // Cpblk
                {
                    NotImplementedOpCode(); break;
                }

                case 213: // Initblk
                {
                    NotImplementedOpCode(); break;
                }

                case 214: // No
                {
                    NotImplementedOpCode(); break;
                }

                case 215: // Rethrow
                {
                    NotImplementedOpCode(); break;
                }

                case 216: // Sizeof
                {
                    NotImplementedOpCode(); break;
                }

                case 217: // Refanytype
                {
                    NotImplementedOpCode(); break;
                }

                case 218: // Readonly
                {
                    NotImplementedOpCode(); break;
                }

                case 219: // Jump
                {
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
                }                    

                case 220: // CallJsNative
                {
                    switch(operands[currentStackFrame.Line])
                    {
                        // set
                        case 0:
                        {
                            let value    = evaluationStack.pop();
                            let key      = evaluationStack.pop();
                            let instance = evaluationStack.pop();

                            instance[key] = value;

                            break;
                        }

                        // get
                        case 1:
                        {
                            let key      = evaluationStack.pop();
                            let instance = evaluationStack.pop();

                            evaluationStack.push( instance[key] );

                            break;
                        }

                        // CreateNewPlainObject
                        case 2:
                        {
                            evaluationStack.push({});
                            break;
                        }

                        // CreateNewArray
                        case 3:
                        {
                            evaluationStack.push([]);
                            break;
                        }

                        // Sum
                        case 4:
                        {
                            let value1 = evaluationStack.pop();
                            let value0 = evaluationStack.pop();

                            evaluationStack.push( value0 + value1 );

                            break;
                        }

                        case 5: // PreviousStackFrame
                        {
                            evaluationStack.push(currentStackFrame.Prev);
                            break;
                        }

                        // instance.Call('functionName')
                        case 6:
                        {
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName]());

                            break;
                        }


                        // instance.Call('functionName', parameter1)
                        case 7:
                        {
                            let parameter1   = evaluationStack.pop();
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName]( parameter1 ));

                            break;
                        }


                        // instance.Call('functionName', parameter1, parameter2)
                        case 8:
                        {
                            let parameter2   = evaluationStack.pop();
                            let parameter1   = evaluationStack.pop();
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName]( parameter1, parameter2));

                            break;
                        }


                        // instance.Call('functionName', parameter1, parameter2, parameter3)
                        case 9:
                        {
                            let parameter3   = evaluationStack.pop();
                            let parameter2   = evaluationStack.pop();
                            let parameter1   = evaluationStack.pop();
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName]( parameter1, parameter2, parameter3));

                            break;
                        }


                        // instance.Call('functionName', parameters)
                        case 10:
                        {
                            let parameters   = evaluationStack.pop();
                            let functionName = evaluationStack.pop();
                            let instance     = evaluationStack.pop();

                            if ( functionName == null)
                            {
                                evaluationStack.push('functionName');
                                evaluationStack.push(InterpreterBridge_ArgumentNullException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            if ( instance == null)
                            {
                                evaluationStack.push(functionName + ' not found.');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }
                            evaluationStack.push( instance[functionName].apply(instance, parameters));

                            break;
                        }


                        case 11: // GlobalMetadata
                        {
                            evaluationStack.push(GlobalMetadata);
                            break;
                        }

                        case 12: // array.push
                        {
                            let item = evaluationStack.pop();
                            let arrayInstance = evaluationStack.pop();

                            if ( arrayInstance == null)
                            {
                                evaluationStack.push('array is null. when call push');
                                evaluationStack.push(InterpreterBridge_NullReferenceException);
                                nextInstruction = InterpreterBridge_Jump;
                                break;
                            }

                            arrayInstance.push(item);
                            break;
                        }

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
                        {
                            console.log(evaluationStack.pop());
                            break;
                        }

                        case 15: // CurrentStackFrame
                        {
                            evaluationStack.push(currentStackFrame);
                            break;
                        }
                    }

                    nextInstruction = instructions[++currentStackFrame.Line];
                    break;
                }
                
                case 221: // handle exception
                {
                    let exception = evaluationStack.pop();

                    if(typeof exception === 'string')
                    {
                        exception = new Error(exception);
                    }

                    let handlerFound = 0;
                    while( currentStackFrame && !handlerFound )
                    {
                        let exceptionHandlers = currentStackFrame.Method.Body.ExceptionHandlers;
                        let exceptionHandlersLength = exceptionHandlers.length;

                        for (let i = 0; i < exceptionHandlersLength; i++)
                        {
                            let exceptionHandler = exceptionHandlers[i];

                            if ( exceptionHandler.CatchType >= 0 )
                            {
                                let catchTypeIsDotNetRootException =  
                                    GlobalMetadata.Types[exceptionHandler.CatchType].Namespace === 'System' &&
                                    GlobalMetadata.Types[exceptionHandler.CatchType].Name === 'Exception';
                                
                                if ( exception.$typeIndex ===  exceptionHandler.CatchType || catchTypeIsDotNetRootException )
                                {
                                    if (exceptionHandler.TryStart <= currentStackFrame.Line &&
                                        exceptionHandler.TryEnd >= currentStackFrame.Line)
                                    {
                                        handlerFound = 1;
                                        
                                        evaluationStack.push(exception);

                                        currentStackFrame.Line = exceptionHandler.HandlerStart;
                                        nextInstruction = instructions[currentStackFrame.Line];
                                        break;
                                    }
                                }
                            }
                        }

                        const Finally = 2;
                        
                        let finallyFound = 0;
                        
                        // try to find finally block
                        if (!handlerFound)
                        {
                            for (let i = 0; i < exceptionHandlersLength; i++)
                            {
                                let exceptionHandler = exceptionHandlers[i];

                                if ( exceptionHandler.HandlerType === Finally )
                                {
                                    if (exceptionHandler.HandlerStart <= currentStackFrame.Line &&
                                        exceptionHandler.HandlerEnd >= currentStackFrame.Line)
                                    {
                                        finallyFound = 1;
                                        currentStackFrame.Line = exceptionHandler.HandlerStart;
                                        nextInstruction = instructions[currentStackFrame.Line];
                                        break;
                                    }
                                }
                            }
                            
                            if( finallyFound )
                            {
                                thread.ExceptionObjectThatMustThrownWhenExitFinallyBlock = exception;
                            }
                        }

                        if ( !handlerFound )
                        {
                            if(!exception._stackTrace)
                            {
                                exception._stackTrace =[];
                            }
                            exception._stackTrace.push(currentStackFrame.Method);

                            if( currentStackFrame.Prev === null)
                            {
                                // exit thread
                                throw exception;
                            }

                            // go previous stack frame
                            {
                                // arrange stack frame
                                let previousStackFrame = currentStackFrame;

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
                            }
                        }
                    }
                    
                    if ( handlerFound )
                    {
                        break;
                    }

                    // exit thread
                    throw exception;
                }

                case 222: // Load method at runtime
                {
                    let methodReference = evaluationStack.pop();

                    let declaringType = GlobalMetadata.Types[methodReference.DeclaringType];
                    
                    let assemblyName = GlobalMetadata.MetadataScopes[declaringType.Scope].Name;
                                        
                    function onSuccess(response)
                    {
                        if (response.Success === false)
                        {
                            throw response.ErrorMessage;
                        }

                        let metadataTable = response.Metadata;

                        CallManagedStaticMethod(InterpreterBridge_ImportMetadata_MethodDefinition, [GlobalMetadata, metadataTable], console.log, console.log);
                        
                        Interpret(thread);
                    }

                    let request =
                    {
                        IsInitialRequest: false,
                        RequestedTypes:
                        [
                            {
                                AssemblyName: assemblyName,
                                NamespaceName: declaringType.Namespace,
                                TypeName: declaringType.Name,
                                MethodName: methodReference.Name
                            }
                        ]
                    };
                    
                    GetMetadata(request, onSuccess);
                    return;
                }
            }
        }
        catch (exception)
        {
            // exit thread
            if (nextInstruction === 221)
            {
                throw exception;
            }

            // handle exception
            evaluationStack.push(exception);
            nextInstruction = 221;
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
        },

        LeaveJumpIndex: null,
        ExceptionObjectThatMustThrownWhenExitFinallyBlock: null
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
        
        if (response.Success === false)
        {
            throw response.ErrorMessage;
        }
        
        let metadataTable = response.Metadata;
        
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
    
    let request =
    {
        IsInitialRequest: true,
        RequestedTypes:
        [
            {
                AssemblyName: "ReactWithDotNet.WebSite.dll",
                NamespaceName: "ReactWithDotNet.WebSite",
                TypeName: "Deneme45"
            }
        ]
    };

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


