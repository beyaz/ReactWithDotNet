using ReactWithDotNet;
using System.Runtime.CompilerServices;
using static ReactWithDotNet.NativeJs;
using static ReactWithDotNet.NativeJsHelper;


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
    public static extern object pop(this Array array);
    
    public static extern void push(this Array array, object value);
}

static class AsExtensions
{
    public static extern object AsObject<T>(this T value);
    
    public static extern T As<T>(this object value);
}

static class NativeJsHelper
{
    public static extern object Get(object instance, string key);
    
    public static extern void Set(this object instance, string key, object value);

    public static extern string Sum(string a, string b);
    public static extern object CreateNewPlainObject();
    
    public static extern StackFrame PreviousStackFrame { get; }


    
    public static extern Array CreateNewArray();
}





static class InterpreterBridge
{
    public static void Jump()
    {
        const int InterpreterBridge_NewArr = 0;
        
        var evaluationStack = PreviousStackFrame.EvaluationStack;
        
        var instruction = evaluationStack.pop().As<int>();
        
        if (InterpreterBridge_NewArr == instruction)
        {
            var length = evaluationStack.pop().As<int>();

            var array = CreateNewArray();
        
            array.Set("length", length.AsObject());

            evaluationStack.push(array);
            
            return;
        }

        throw new Exception();

    }
}

