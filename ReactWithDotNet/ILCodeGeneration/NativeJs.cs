using System.Runtime.CompilerServices;
using static ReactWithDotNet.NativeJs;

namespace ReactWithDotNet;


sealed class StackFrame
{
    public MethodDefinitionModel Method;
    public Array EvaluationStack;
    public Array LocalVariables;
    public Array MethodArguments;
    public int MethodArgumentsOfset;
    public int Line;
}

sealed class Address
{
    public Array Array;
    public int Index;
}

sealed class ThreadModel
{
    public Array CallStack;
    public int Line;
}

static class NativeJs
{
    public static extern object Get<TInstance>(TInstance instance, string key);
    
    public static extern void Set<TValue>(this object instance, string key, TValue value);

    public static extern string Sum(string a, string b);
    public static extern object CreateNewPlainObject();
    
    public static extern StackFrame CurrentStackFrame { get; }

    public static extern object pop(this Array array);
    
    public static extern void push(this Array array, object value);
    
    public static extern object AsObject<T>(this T value);
    
    public static extern T As<T>(this object value);
    
    public static extern Array CreateNewArray();
}







static class InterpreterBridge
{
    public static void Jump()
    {
        
        
        var length = CurrentStackFrame.EvaluationStack.pop().As<int>();

        var array = CreateNewArray();
        
        array.Set("length", length.AsObject());

        CurrentStackFrame.EvaluationStack.push(array);
    }
}

