
const CLR = {

};
window.CLR = CLR;


function Interpret(thread)
{
    const instructions = thread.Method.Body.Instructions;

    const operands = thread.Method.Body.Operands;

    const evaluatinStack = thread.Method.Body.EvaluatinStack;

    const methodArguments = thread.Method.Body.MethodArguments;    

    const localVariables = thread.Method.Body.LocalVariables;  

    switch (instructions[thread.Line])
    {
        case 0: // nop: No operation
            thread.Line++;
            break;

        case 1: // break: Inform debugger of a break point
            thread.Line++;
            break;

        case 2: // ldarg.0: Load argument 0 onto the stack
            evaluatinStack.push(methodArguments[0]);
            thread.Line++;
            break;

        case 3: // ldarg.1: Load argument 1 onto the stack
            evaluatinStack.push(methodArguments[1]);
            thread.Line++;
            break;

        case 4: // ldarg.2: Load argument 2 onto the stack
            evaluatinStack.push(methodArguments[2]);
            thread.Line++;
            break;

        case 5: // ldarg.3: Load argument 3 onto the stack
            evaluatinStack.push(methodArguments[3]);
            thread.Line++;
            break;

        case 6: // ldloc.0: Load local variable 0 onto the stack
            evaluatinStack.push(localVariables[0]);
            thread.Line++;
            break;

        case 7: // ldloc.1: Load local variable 1 onto the stack
            evaluatinStack.push(localVariables[1]);
            thread.Line++;
            break;

        case 8: // ldloc.2: Load local variable 2 onto the stack
            evaluatinStack.push(localVariables[2]);
            thread.Line++;
            break;

        case 9: // ldloc.3: Load local variable 3 onto the stack
            evaluatinStack.push(localVariables[3]);
            thread.Line++;
            break;

        case 10: // stloc.0: Store value from the stack in local variable 0
            localVariables[0] = evaluatinStack.pop();
            thread.Line++;
            break;

        case 11: // stloc.1: Store value from the stack in local variable 1
            localVariables[1] = evaluatinStack.pop();
            thread.Line++;
            break;

        case 12: // stloc.2: Store value from the stack in local variable 2
            localVariables[2] = evaluatinStack.pop();
            thread.Line++;
            break;

        case 13: // stloc.3: Store value from the stack in local variable 3
            localVariables[3] = evaluatinStack.pop();
            thread.Line++;
            break;

        case 14: // ldarg.s: Load argument at a specified index (short form)
            evaluatinStack.push(methodArguments[operands[thread.Line]]);
            thread.Line++;
            break;

        case 15: // ldarga.s: Load address of argument at a specified index (short form)
            evaluatinStack.push({ Array: methodArguments, Index: operands[thread.Line] } );
            thread.Line++;
            break;

        case 15: // starg.s: Store value from the stack into argument at specified index
            methodArguments[operands[thread.Line]] = evaluatinStack.pop();
            thread.Line++;
            break;

        case 16: // ldloc.s: Load local variable at a specified index (short form)
            evaluatinStack.push(localVariables[operands[thread.Line]]);
            thread.Line++;
            break;

        case 17: // ldloca.s: Load address of local variable at a specified index (short)
            evaluatinStack.push({ Array: localVariables, Index: operands[thread.Line] });
            thread.Line++;
            break;

        case 18: // stloc.s: Store value from the stack into local variable at specified index
            localVariables[operands[thread.Line]] = evaluatinStack.pop();
            thread.Line++;
            break;

        case 19: // ldnull: Push a null reference onto the stack
            evaluatinStack.push(null);
            thread.Line++;
            break;

        case 20: // ldc.i4.m1: Load integer constant -1 onto the stack
            evaluatinStack.push(-1);
            thread.Line++;
            break;

        case 21: // ldc.i4.m1: Load integer constant -1 onto the stack
            evaluatinStack.push(-1);
            thread.Line++;
            break;

        case 22: // ldc.i4.0: Load integer constant 0 onto the stack
            evaluatinStack.push(0);
            thread.Line++;
            break;

        case 23: // ldc.i4.1: Load integer constant 1 onto the stack
            evaluatinStack.push(1);
            thread.Line++;
            break;

        case 24: // ldc.i4.2: Load integer constant 2 onto the stack
            evaluatinStack.push(2);
            thread.Line++;
            break;

        case 25: // ldc.i4.3: Load integer constant 3 onto the stack
            evaluatinStack.push(3);
            thread.Line++;
            break;

        case 26: // ldc.i4.4: Load integer constant 4 onto the stack
            evaluatinStack.push(4);
            thread.Line++;
            break;

        case 27: // ldc.i4.5: Load integer constant 5 onto the stack
            evaluatinStack.push(5);
            thread.Line++;
            break;

        case 28: // ldc.i4.6: Load integer constant 6 onto the stack
            evaluatinStack.push(6);
            thread.Line++;
            break;

        case 29: // ldc.i4.7: Load integer constant 7 onto the stack
            evaluatinStack.push(7);
            thread.Line++;
            break;

        case 30: // ldc.i4.8: Load integer constant 8 onto the stack
            evaluatinStack.push(8);
            thread.Line++;
            break;

        case 31: // ldc.i4: Load 4-byte integer constant onto the stack
            evaluatinStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 32: // ldc.i8: Load 8-byte integer constant onto the stack
            evaluatinStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 33: // ldc.r4: Load 4-byte floating-point constant onto the stack
            evaluatinStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 34: // ldc.r8: Load 8-byte floating-point constant onto the stack
            evaluatinStack.push(operands[thread.Line]);
            thread.Line++;
            break;

        case 35: // dup: Duplicate the value on top of the stack
            evaluatinStack.push(evaluatinStack[evaluatinStack.length - 1]);
            thread.Line++;
            break;

        case 36: // pop: Remove the value from the top of the stack
            evaluatinStack.pop();
            thread.Line++;
            break;


    }
}
