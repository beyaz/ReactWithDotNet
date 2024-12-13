using ReactWithDotNet;
using static ReactWithDotNet.NativeJsHelper;



namespace ReactWithDotNet;

sealed class StackFrame
{
    public MethodDefinitionModel Method;
    public Array EvaluationStack;
    public Array LocalVariables;
    public Array MethodArguments;
    public int MethodArgumentsOffset;
    public int Line;
    public StackFrame Prev;
    
    public MethodDefinitionModel GenericInstanceMethod;
}

sealed class ThreadModel
{
    public StackFrame LastFrame;
}

static class AsExtensions
{
    public static extern object AsObject<T>(this T value);
    
    public static extern T As<T>(this object value);
}


static class NativeJsHelper
{
    public static extern void Set(this object instance, string key, object value);
    public static extern void Set(this object instance, int key, object value);
    
    public static extern object Get(this object instance, string key);
    public static extern object Get(this object instance, int key);
    
    public static extern object CreateNewPlainObject();

    public static extern Array CreateNewArray();
    
    public static extern string Sum(string a, string b);
    
    public static extern StackFrame PreviousStackFrame { get; }
    
    
    
    public static extern object Call(this object instance, string functionName);
    public static extern object Call(this object instance, string functionName, object parameter1);
    public static extern object Call(this object instance, string functionName, object parameter1, object parameter2);
    public static extern object Call(this object instance, string functionName, object parameter1, object parameter2, object parameter3);
    public static extern object Call(this object instance, string functionName, object[] parameters);
    
    public static extern MetadataTable GlobalMetadata { get; }
    
    public static extern object pop(this Array array);
    
    public static extern void push(this Array array, object value);
    
    public static extern void Dump(object value);
    
    public static extern StackFrame CurrentStackFrame { get; }
}





static class InterpreterBridge
{
   
    public static void Jump()
    {
        const int InterpreterBridge_NewArr = 0;
        const int InterpreterBridge_NullReferenceException = 1;
        const int InterpreterBridge_ArgumentNullException = 2;
        const int InterpreterBridge_DivideByZeroException = 3;
        const int InterpreterBridge_IndexOutOfRangeException = 4;
        const int InterpreterBridge_MissingMethodException = 5;
        
        
        var previousStackFrame = PreviousStackFrame;
        var previousStackFrameLine = previousStackFrame.Get(nameof(StackFrame.Line)).As<int>();
        
        var evaluationStack = previousStackFrame.EvaluationStack;
        
        var instruction = evaluationStack.pop().As<int>();

        


        if (InterpreterBridge_NewArr == instruction)
        {
            var length = evaluationStack.pop().As<int>();

            var array = CreateNewArray();

            array.Set("length", length.AsObject());

            evaluationStack.push(array);

            return;
        }

        if (InterpreterBridge_NullReferenceException == instruction)
        {
            var message = evaluationStack.pop().As<string>();

            throw new NullReferenceException(message);
        }

        if (InterpreterBridge_ArgumentNullException == instruction)
        {
            var message = evaluationStack.pop().As<string>();

            throw new ArgumentNullException(message);
        }
        
        if (InterpreterBridge_DivideByZeroException == instruction)
        {
            var message = evaluationStack.pop().As<string>();

            throw new DivideByZeroException(message);
        }
        
        if (InterpreterBridge_IndexOutOfRangeException == instruction)
        {
            var message = evaluationStack.pop().As<string>();

            throw new IndexOutOfRangeException(message);
        }
        
        if (InterpreterBridge_MissingMethodException == instruction)
        {
            var methodReferenceModel = evaluationStack.pop().As<MethodReferenceModel>();

            throw new MissingMethodException(methodReferenceModel.Name);
        }

        throw new Exception();
    }

}

