
const CLR = {

};
window.CLR = CLR;


function Interpret(thread)
{
    const instructions = thread.Method.Body.Instructions;

    const operands = thread.Method.Body.Operands;

    const evaluationStack = thread.Method.Body.EvaluationStack;

    const methodArguments = thread.Method.Body.MethodArguments;    

    const localVariables = thread.Method.Body.LocalVariables;

    let v0, v1, v2;

    switch (instructions[thread.Line])
    {
        case 0: // Nop: No operation
            thread.Line++;
            break;

        case 1: // Break: Inform debugger of a break point
            thread.Line++;
            break;

        case 2: // Ldarg_0: Load argument 0 onto the stack
            evaluationStack.push(methodArguments[0]);
            thread.Line++;
            break;

        case 3: // Ldarg_1: Load argument 1 onto the stack
            evaluationStack.push(methodArguments[1]);
            thread.Line++;
            break;

        case 4: // Ldarg_2: Load argument 2 onto the stack
            evaluationStack.push(methodArguments[2]);
            thread.Line++;
            break;

        case 5: // Ldarg_3: Load argument 3 onto the stack
            evaluationStack.push(methodArguments[3]);
            thread.Line++;
            break;

        case 6: // Ldloc_0: Load local variable 0 onto the stack
            evaluationStack.push(localVariables[0]);
            thread.Line++;
            break;

        case 7: // Ldloc_1: Load local variable 1 onto the stack
            evaluationStack.push(localVariables[1]);
            thread.Line++;
            break;

        case 8: // Ldloc_2: Load local variable 2 onto the stack
            evaluationStack.push(localVariables[2]);
            thread.Line++;
            break;

        case 9: // Ldloc_3: Load local variable 3 onto the stack
            evaluationStack.push(localVariables[3]);
            thread.Line++;
            break;

        case 10: // Stloc_0: Store value from the stack in local variable 0
            localVariables[0] = evaluationStack.pop();
            thread.Line++;
            break;

        case 11: // Stloc_1: Store value from the stack in local variable 1
            localVariables[1] = evaluationStack.pop();
            thread.Line++;
            break;

        case 12: // Stloc_2: Store value from the stack in local variable 2
            localVariables[2] = evaluationStack.pop();
            thread.Line++;
            break;

        case 13: // Stloc_3: Store value from the stack in local variable 3
            localVariables[3] = evaluationStack.pop();
            thread.Line++;
            break;

        case 14: // Ldarg_S: Load argument at a specified index (short form)
            evaluationStack.push(methodArguments[operands[thread.Line]]);
            thread.Line++;
            break;

        case 15: // Ldarga_S: Load address of argument at a specified index (short form)
            evaluationStack.push({ Array: methodArguments, Index: operands[thread.Line] } );
            thread.Line++;
            break;

        case 16: // Starg_S: Store value from the stack into argument at specified index
            methodArguments[operands[thread.Line]] = evaluationStack.pop();
            thread.Line++;
            break;

        case 17: // Ldloc_S: Load local variable at a specified index (short form)
            evaluationStack.push(localVariables[operands[thread.Line]]);
            thread.Line++;
            break;

        case 18: // Ldloca_S: Load address of local variable at a specified index (short)
            evaluationStack.push({ Array: localVariables, Index: operands[thread.Line] });
            thread.Line++;
            break;

        case 19: // Stloc_S: Store value from the stack into local variable at specified index
            localVariables[operands[thread.Line]] = evaluationStack.pop();
            thread.Line++;
            break;

        case 20: // Ldnull: Push a null reference onto the stack
            evaluationStack.push(null);
            thread.Line++;
            break;

        case 21: // Ldc_I4_M1: Load integer constant -1 onto the stack
            evaluationStack.push(-1);
            thread.Line++;
            break;

        case 22: // Ldc_I4_0: Load integer constant 0 onto the stack
            evaluationStack.push(0);
            thread.Line++;
            break;

        case 23: // Ldc_I4_1: Load integer constant 1 onto the stack
            evaluationStack.push(1);
            thread.Line++;
            break;

        case 24: // Ldc_I4_2: Load integer constant 2 onto the stack
            evaluationStack.push(2);
            thread.Line++;
            break;

        case 25: // Ldc_I4_3: Load integer constant 3 onto the stack
            evaluationStack.push(3);
            thread.Line++;
            break;

        case 26: // Ldc_I4_4: Load integer constant 4 onto the stack
            evaluationStack.push(4);
            thread.Line++;
            break;

        case 27: // Ldc_I4_5: Load integer constant 5 onto the stack
            evaluationStack.push(5);
            thread.Line++;
            break;

        case 28: // Ldc_I4_6: Load integer constant 6 onto the stack
            evaluationStack.push(6);
            thread.Line++;
            break;

        case 29: // Ldc_I4_7: Load integer constant 7 onto the stack
            evaluationStack.push(7);
            thread.Line++;
            break;

        case 30: // Ldc_I4_8: Load integer constant 8 onto the stack
            evaluationStack.push(8);
            thread.Line++;
            break;

        case 31: // Ldc_I4_S: Load 4-byte integer constant onto the stack
            evaluationStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 32: // Ldc_I4: Load 8-byte integer constant onto the stack
            evaluationStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 33: // Ldc_I8: Load 4-byte floating-point constant onto the stack
            evaluationStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 34: // Ldc_R4: Load 8-byte floating-point constant onto the stack
            evaluationStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 35: // Ldc_R8: Load 8-byte floating-point constant onto the stack
            evaluationStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 36: // Dup: Duplicate the value on top of the stack
            evaluationStack.push(evaluationStack[evaluationStack.length - 1]);
            thread.Line++;
            break;

        case 37: // Pop: Remove the value from the top of the stack
            evaluationStack.pop();
            thread.Line++;
            break;

        case 38: // Jmp
            thread.Line++;
            break;
        case 39: // Call
            thread.Line++;
            break;
        case 40: // Calli
            thread.Line++;
            break;
        case 41: // Ret
            thread.Line++;
            break;
        case 42: // Br_S
            thread.Line = operands[thread.Line];
            break;

        case 43: // Brfalse_S
            if (evaluationStack[evaluationStack.length])
            {
                thread.Line++;
            }
            else
            {
                thread.Line = operands[thread.Line];
            }
            evaluationStack.pop();          
            break;

        case 44: // Brtrue_S
            if (evaluationStack[evaluationStack.length - 1])
            {
                thread.Line = operands[thread.Line];
            }
            else
            {
                thread.Line++;
            }
            evaluationStack.pop(); 
            break;
        case 45: // Beq_S
            thread.Line++;
            break;
        case 46: // Bge_S
            thread.Line++;
            break;

        case 47: // Bgt_S

            v1 = evaluationStack.pop();
            v0 = evaluationStack.pop();

            if (typeof v0 === 'number')
            {
                if (v0 > v1)
                {
                    thread.Line = operands[thread.Line];
                }
                else
                {
                    thread.Line++;
                }
            }
            break;

        case 48: // Ble_S
            thread.Line++;
            break;
        case 49: // Blt_S
            thread.Line++;
            break;
        case 50: // Bne_Un_S
            thread.Line++;
            break;
        case 51: // Bge_Un_S
            thread.Line++;
            break;
        case 52: // Bgt_Un_S
            thread.Line++;
            break;
        case 53: // Ble_Un_S
            thread.Line++;
            break;
        case 54: // Blt_Un_S
            thread.Line++;
            break;
        case 55: // Br
            thread.Line++;
            break;
        case 56: // Brfalse
            thread.Line++;
            break;
        case 57: // Brtrue
            thread.Line++;
            break;
        case 58: // Beq
            thread.Line++;
            break;
        case 59: // Bge
            thread.Line++;
            break;
        case 60: // Bgt
            thread.Line++;
            break;
        case 61: // Ble
            thread.Line++;
            break;
        case 62: // Blt
            thread.Line++;
            break;
        case 63: // Bne_Un
            thread.Line++;
            break;
        case 64: // Bge_Un
            thread.Line++;
            break;
        case 65: // Bgt_Un
            thread.Line++;
            break;
        case 66: // Ble_Un
            thread.Line++;
            break;
        case 67: // Blt_Un
            thread.Line++;
            break;
        case 68: // Switch
            thread.Line++;
            break;
        case 69: // Ldind_I1
            thread.Line++;
            break;
        case 70: // Ldind_U1
            thread.Line++;
            break;
        case 71: // Ldind_I2
            thread.Line++;
            break;
        case 72: // Ldind_U2
            thread.Line++;
            break;
        case 73: // Ldind_I4
            thread.Line++;
            break;
        case 74: // Ldind_U4
            thread.Line++;
            break;
        case 75: // Ldind_I8
            thread.Line++;
            break;
        case 76: // Ldind_I
            thread.Line++;
            break;
        case 77: // Ldind_R4
            thread.Line++;
            break;
        case 78: // Ldind_R8
            thread.Line++;
            break;
        case 79: // Ldind_Ref
            thread.Line++;
            break;
        case 80: // Stind_Ref
            thread.Line++;
            break;
        case 81: // Stind_I1
            thread.Line++;
            break;
        case 82: // Stind_I2
            thread.Line++;
            break;
        case 83: // Stind_I4
            thread.Line++;
            break;
        case 84: // Stind_I8
            thread.Line++;
            break;
        case 85: // Stind_R4
            thread.Line++;
            break;
        case 86: // Stind_R8
            thread.Line++;
            break;
        case 87: // Add
            thread.Line++;
            break;
        case 88: // Sub
            thread.Line++;
            break;
        case 89: // Mul
            thread.Line++;
            break;
        case 90: // Div
            thread.Line++;
            break;
        case 91: // Div_Un
            thread.Line++;
            break;
        case 92: // Rem
            thread.Line++;
            break;
        case 93: // Rem_Un
            thread.Line++;
            break;
        case 94: // And
            thread.Line++;
            break;
        case 95: // Or
            thread.Line++;
            break;
        case 96: // Xor
            thread.Line++;
            break;
        case 97: // Shl
            thread.Line++;
            break;
        case 98: // Shr
            thread.Line++;
            break;
        case 99: // Shr_Un
            thread.Line++;
            break;
        case 100: // Neg
            thread.Line++;
            break;
        case 101: // Not
            thread.Line++;
            break;
        case 102: // Conv_I1
            thread.Line++;
            break;
        case 103: // Conv_I2
            thread.Line++;
            break;
        case 104: // Conv_I4
            thread.Line++;
            break;
        case 105: // Conv_I8
            thread.Line++;
            break;
        case 106: // Conv_R4
            thread.Line++;
            break;
        case 107: // Conv_R8
            thread.Line++;
            break;
        case 108: // Conv_U4
            thread.Line++;
            break;
        case 109: // Conv_U8
            thread.Line++;
            break;
        case 110: // Callvirt
            thread.Line++;
            break;
        case 111: // Cpobj
            thread.Line++;
            break;
        case 112: // Ldobj
            thread.Line++;
            break;
        case 113: // Ldstr
            thread.Line++;
            break;
        case 114: // Newobj
            thread.Line++;
            break;
        case 115: // Castclass
            thread.Line++;
            break;
        case 116: // Isinst
            thread.Line++;
            break;
        case 117: // Conv_R_Un
            thread.Line++;
            break;
        case 118: // Unbox
            thread.Line++;
            break;
        case 119: // Throw
            thread.Line++;
            break;
        case 120: // Ldfld
            thread.Line++;
            break;
        case 121: // Ldflda
            thread.Line++;
            break;
        case 122: // Stfld
            thread.Line++;
            break;
        case 123: // Ldsfld
            thread.Line++;
            break;
        case 124: // Ldsflda
            thread.Line++;
            break;
        case 125: // Stsfld
            thread.Line++;
            break;
        case 126: // Stobj
            thread.Line++;
            break;
        case 127: // Conv_Ovf_I1_Un
            thread.Line++;
            break;
        case 128: // Conv_Ovf_I2_Un
            thread.Line++;
            break;
        case 129: // Conv_Ovf_I4_Un
            thread.Line++;
            break;
        case 130: // Conv_Ovf_I8_Un
            thread.Line++;
            break;
        case 131: // Conv_Ovf_U1_Un
            thread.Line++;
            break;
        case 132: // Conv_Ovf_U2_Un
            thread.Line++;
            break;
        case 133: // Conv_Ovf_U4_Un
            thread.Line++;
            break;
        case 134: // Conv_Ovf_U8_Un
            thread.Line++;
            break;
        case 135: // Conv_Ovf_I_Un
            thread.Line++;
            break;
        case 136: // Conv_Ovf_U_Un
            thread.Line++;
            break;
        case 137: // Box
            thread.Line++;
            break;
        case 138: // Newarr
            thread.Line++;
            break;
        case 139: // Ldlen
            thread.Line++;
            break;
        case 140: // Ldelema
            thread.Line++;
            break;
        case 141: // Ldelem_I1
            thread.Line++;
            break;
        case 142: // Ldelem_U1
            thread.Line++;
            break;
        case 143: // Ldelem_I2
            thread.Line++;
            break;
        case 144: // Ldelem_U2
            thread.Line++;
            break;
        case 145: // Ldelem_I4
            thread.Line++;
            break;
        case 146: // Ldelem_U4
            thread.Line++;
            break;
        case 147: // Ldelem_I8
            thread.Line++;
            break;
        case 148: // Ldelem_I
            thread.Line++;
            break;
        case 149: // Ldelem_R4
            thread.Line++;
            break;
        case 150: // Ldelem_R8
            thread.Line++;
            break;
        case 151: // Ldelem_Ref
            thread.Line++;
            break;
        case 152: // Stelem_I
            thread.Line++;
            break;
        case 153: // Stelem_I1
            thread.Line++;
            break;
        case 154: // Stelem_I2
            thread.Line++;
            break;
        case 155: // Stelem_I4
            thread.Line++;
            break;
        case 156: // Stelem_I8
            thread.Line++;
            break;
        case 157: // Stelem_R4
            thread.Line++;
            break;
        case 158: // Stelem_R8
            thread.Line++;
            break;
        case 159: // Stelem_Ref
            thread.Line++;
            break;

        case 160: // Ldelem_Any
            thread.Line++;
            break;

        case 161: // Stelem_Any
            thread.Line++;
            break;

        case 162: // Unbox_Any
            thread.Line++;
            break;

        case 163: // Conv_Ovf_I1
            thread.Line++;
            break;

        case 164: // Conv_Ovf_U1
            thread.Line++;
            break;

        case 165: // Conv_Ovf_I2
            thread.Line++;
            break;

        case 166: // Conv_Ovf_U2
            thread.Line++;
            break;

        case 167: // Conv_Ovf_I4
            thread.Line++;
            break;

        case 168: // Conv_Ovf_U4
            thread.Line++;
            break;

        case 169: // Conv_Ovf_I8
            thread.Line++;
            break;

        case 170: // Conv_Ovf_U8
            thread.Line++;
            break;

        case 171: // Refanyval
            thread.Line++;
            break;

        case 172: // Ckfinite
            thread.Line++;
            break;

        case 173: // Mkrefany
            thread.Line++;
            break;

        case 174: // Ldtoken
            thread.Line++;
            break;

        case 175: // Conv_U2
            thread.Line++;
            break;

        case 176: // Conv_U1
            thread.Line++;
            break;

        case 177: // Conv_I
            thread.Line++;
            break;

        case 178: // Conv_Ovf_I
            thread.Line++;
            break;

        case 179: // Conv_Ovf_U
            thread.Line++;
            break;

        case 180: // Add_Ovf
            thread.Line++;
            break;

        case 181: // Add_Ovf_Un
            thread.Line++;
            break;

        case 182: // Mul_Ovf
            thread.Line++;
            break;

        case 183: // Mul_Ovf_Un
            thread.Line++;
            break;

        case 184: // Sub_Ovf
            thread.Line++;
            break;

        case 185: // Sub_Ovf_Un
            thread.Line++;
            break;

        case 186: // Endfinally
            thread.Line++;
            break;

        case 187: // Leave
            thread.Line++;
            break;

        case 188: // Leave_S
            thread.Line++;
            break;

        case 189: // Stind_I
            thread.Line++;
            break;

        case 190: // Conv_U
            thread.Line++;
            break;

        case 191: // Arglist
            thread.Line++;
            break;

        case 192: // Ceq
            thread.Line++;
            break;

        case 193: // Cgt
            thread.Line++;
            break;

        case 194: // Cgt_Un
            thread.Line++;
            break;

        case 195: // Clt
            thread.Line++;
            break;

        case 196: // Clt_Un
            thread.Line++;
            break;

        case 197: // Ldftn
            thread.Line++;
            break;

        case 198: // Ldvirtftn
            thread.Line++;
            break;

        case 199: // Ldarg
            thread.Line++;
            break;

        case 200: // Ldarga
            thread.Line++;
            break;

        case 201: // Starg
            thread.Line++;
            break;

        case 202: // Ldloc
            thread.Line++;
            break;

        case 203: // Ldloca
            thread.Line++;
            break;

        case 204: // Stloc
            thread.Line++;
            break;

        case 205: // Localloc
            thread.Line++;
            break;

        case 206: // Endfilter
            thread.Line++;
            break;

        case 207: // Unaligned
            thread.Line++;
            break;

        case 208: // Volatile
            thread.Line++;
            break;

        case 209: // Tail
            thread.Line++;
            break;

        case 210: // Initobj
            thread.Line++;
            break;

        case 211: // Constrained
            thread.Line++;
            break;

        case 212: // Cpblk
            thread.Line++;
            break;

        case 213: // Initblk
            thread.Line++;
            break;

        case 214: // No
            thread.Line++;
            break;

        case 215: // Rethrow
            thread.Line++;
            break;

        case 216: // Sizeof
            thread.Line++;
            break;

        case 217: // Refanytype
            thread.Line++;
            break;

        case 218: // Readonly
            thread.Line++;
            break;




    }
}







