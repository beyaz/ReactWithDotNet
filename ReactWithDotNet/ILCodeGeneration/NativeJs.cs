using System.Diagnostics.CodeAnalysis;
using ReactWithDotNet;
using System.Runtime.CompilerServices;
using Mono.Cecil.Cil;
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
}

sealed class Address
{
    public Array Array;
    public int Index;
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

        throw new Exception();
    }

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    public static void ImportMetadata(MetadataTable globalMetadata, MetadataTable metadata)
    {
        var typesGlobal = globalMetadata.Get(nameof(MetadataTable.Types)).As<TypeReferenceModel[]>();
        
        var types = metadata.Get(nameof(MetadataTable.Types)).As<TypeReferenceModel[]>();
        var fields = metadata.Get(nameof(MetadataTable.Fields)).As<MethodReferenceModel[]>();
        var methods = metadata.Get(nameof(MetadataTable.Methods)).As<MethodReferenceModel[]>();
        var properties = metadata.Get(nameof(MetadataTable.Properties)).As<MethodReferenceModel[]>();
        var events = metadata.Get(nameof(MetadataTable.Events)).As<MethodReferenceModel[]>();
        var metadataScopes = metadata.Get(nameof(MetadataTable.MetadataScopes)).As<MethodReferenceModel[]>();
        
        // is first load
        if (typesGlobal.Length == 0)
        {
            globalMetadata.Set(nameof(MetadataTable.MetadataScopes), metadataScopes);
            globalMetadata.Set(nameof(MetadataTable.Types), types);
            globalMetadata.Set(nameof(MetadataTable.Fields), fields);
            globalMetadata.Set(nameof(MetadataTable.Methods), methods);
            globalMetadata.Set(nameof(MetadataTable.Properties), properties);
            globalMetadata.Set(nameof(MetadataTable.Events), events);
            
            return;
        }
        
        for (var i = 0; i < methods.Length; i++)
        {
            var methodReferenceModel = methods[i];
            if (methodReferenceModel.IsDefinition)
            {
                var methodDefinitionModel = methodReferenceModel.As<MethodDefinitionModel>();

                var instructions = methodDefinitionModel.Body.Instructions.As<int[]>();
                
                var operands = methodDefinitionModel.Body.Operands.As<object>();
                
                for (var j = 0; j < instructions.Length; j++)
                {
                    var instruction = instructions[j];
                    if (instruction == (int)OpCodes.Call.Code)
                    {
                        var targetMethodReference = operands.Get(j);
                        
                    }
                }
            }
        }


        static int indexAt(MetadataTable globalMetadata,  MetadataTable metadata, MethodReferenceModel targetMethodReference, int index)
        {
            var types = metadata.Get(nameof(MetadataTable.Types)).As<TypeReferenceModel[]>();
            var fields = metadata.Get(nameof(MetadataTable.Fields)).As<MethodReferenceModel[]>();
            var methods = metadata.Get(nameof(MetadataTable.Methods)).As<MethodReferenceModel[]>();
            var properties = metadata.Get(nameof(MetadataTable.Properties)).As<MethodReferenceModel[]>();
            var events = metadata.Get(nameof(MetadataTable.Events)).As<MethodReferenceModel[]>();
            var metadataScopes = metadata.Get(nameof(MetadataTable.MetadataScopes)).As<MethodReferenceModel[]>();
            
            
            var global_types = globalMetadata.Get(nameof(MetadataTable.Types)).As<TypeReferenceModel[]>();
            var global_fields = globalMetadata.Get(nameof(MetadataTable.Fields)).As<MethodReferenceModel[]>();
            var global_methods = globalMetadata.Get(nameof(MetadataTable.Methods)).As<MethodReferenceModel[]>();
            var global_properties = globalMetadata.Get(nameof(MetadataTable.Properties)).As<MethodReferenceModel[]>();
            var global_events = globalMetadata.Get(nameof(MetadataTable.Events)).As<MethodReferenceModel[]>();
            var global_metadataScopes = globalMetadata.Get(nameof(MetadataTable.MetadataScopes)).As<MethodReferenceModel[]>();
            
            for (var i = 0; i < global_methods.Length; i++)
            {
                var methodReference = global_methods[i];
                
                if (targetMethodReference.Name != methodReference.Name)
                {
                    continue;
                }

                if (methodReference.DeclaringType == null)
                {
                    continue;
                }
                
                
                


            }

            return index;

        }
        
        
        
        
    }
}

