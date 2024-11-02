using System.Diagnostics.CodeAnalysis;
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
    public static extern void Set(this object instance, string key, object value);
    
    public static extern object Get(this object instance, string key);
    
    public static extern object CreateNewPlainObject();

    public static extern Array CreateNewArray();
    
    public static extern string Sum(string a, string b);
    
    public static extern StackFrame PreviousStackFrame { get; }
    
    public static extern object Call(this object instance, string functionName);
    public static extern object Call(this object instance, string functionName, object parameter1);
    public static extern object Call(this object instance, string functionName, object parameter1, object parameter2);
    public static extern object Call(this object instance, string functionName, object parameter1, object parameter2, object parameter3);
    public static extern object Call(this object instance, string functionName, object[] parameters);
}





static class InterpreterBridge
{
    public static void Jump()
    {
        const int InterpreterBridge_NewArr = 0;
        const int InterpreterBridge_NullReferenceException = 1;
        const int InterpreterBridge_ArgumentNullException = 2;
        
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

        throw new Exception();
    }

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    public static void ImportMetadata(MetadataTable globalMetadata, MetadataTable metadata)
    {
        var methods = metadata.Get(nameof(MetadataTable.Methods)).As<MethodReferenceModel[]>();
        
        for (var i = 0; i < methods.Length; i++)
        {
            if (methods[i].Get(nameof(MemberReferenceModel.IsDefinition)).As<bool>())
            {
                methods[i].Set("MetadataTable", metadata);
            }
        }
        
        var types = metadata.Get(nameof(MetadataTable.Types)).As<TypeReferenceModel[]>();
        
        for (var i = 0; i < types.Length; i++)
        {
            if (types[i].Get(nameof(MemberReferenceModel.IsDefinition)).As<bool>())
            {
                types[i].Set("MetadataTable", metadata);
            }
        }
    }
}

