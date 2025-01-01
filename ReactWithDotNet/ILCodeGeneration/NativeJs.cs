using ReactWithDotNet;
using static ReactWithDotNet.NativeJsHelper;
using static ReactWithDotNet.OpCodeManaged;

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
    
    public static extern object RefStructToObject<T>(this T value) where T :  allows ref struct;
    
    public static extern void Set(this object instance, string key, object value);
    public static extern void Set(this object instance, int key, object value);
    
    public static extern object Get(this object instance, string key);
    public static extern object Get(this object instance, int key);
    
    public static extern string TypeOf(this object instance);
    
    public static extern bool TypeOfIsNumber(this object instance);
    
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
    
    public static extern string join(this Array array, string separator);
    
    public static extern void Dump(object value);
    
    public static extern StackFrame CurrentStackFrame { get; }
}



static class OpCodeManaged
{
    public const int MultiDimensionalArray_Set = 0;
    
    public const int InterpreterBridge_NewArr = 0; // todo: free
    public const int InterpreterBridge_NullReferenceException = 1;
    public const int InterpreterBridge_ArgumentNullException = 2;
    public const int InterpreterBridge_DivideByZeroException = 3;
    public const int InterpreterBridge_IndexOutOfRangeException = 4;
    public const int InterpreterBridge_MissingMethodException = 5;
    public const int InterpreterBridge_OverflowException = 6;
}

static class InterpreterBridge
{
   
    public static void Jump()
    {
        
        
        
        var previousStackFrame = PreviousStackFrame;
        var previousStackFrameLine = previousStackFrame.Get(nameof(StackFrame.Line)).As<int>();
        
        var evaluationStack = previousStackFrame.EvaluationStack;
        
        var instruction = evaluationStack.pop().As<int>();

        switch (instruction)
        {
            case MultiDimensionalArray_Set:
            {
                var rank = 0;
                
                var value = evaluationStack.pop();

                object[] array;
                var indexList = CreateNewArray().As<int[]>();
                {
                    while (true)
                    {
                        var item = evaluationStack.pop();
                        if (item.TypeOfIsNumber())
                        {
                            indexList.push(item);
                            rank++;
                        }
                        else
                        {
                            array = item.As<object[]>();
                            break;
                        }
                    }
                    indexList.Call("reverse");
                }
                
                

                var dimensions = array.Get("$dimensions").As<int[]>();
                var flatIndex = 0;
                {
                    for (var i = 0; i < rank; i++)
                    {
                        var multiplier = 1;
                        for (var j = i + 1; j < rank; j++)
                        {
                            multiplier *= dimensions[j];
                        }

                        flatIndex += indexList[i] * multiplier;
                    }
                }
                
                array[flatIndex] = value;
                
                return;
            }

            case InterpreterBridge_NullReferenceException:
            {
                var message = evaluationStack.pop().As<string>();

                throw new NullReferenceException(message);
            }
            
            case InterpreterBridge_ArgumentNullException:
            {
                var message = evaluationStack.pop().As<string>();

                throw new ArgumentNullException(message);
            }
            
            case InterpreterBridge_DivideByZeroException:
            {
                var message = evaluationStack.pop().As<string>();

                throw new DivideByZeroException(message);
            }
            
            case InterpreterBridge_IndexOutOfRangeException:
            {
                var message = evaluationStack.pop().As<string>();

                throw new IndexOutOfRangeException(message);
            }
            
            case InterpreterBridge_MissingMethodException:
            {
                
                var methodReferenceModel = evaluationStack.pop().As<MethodReferenceModel>();

                throw new MissingMethodException(methodReferenceModel.Name);
            }
            
            case InterpreterBridge_OverflowException:
            {
                var message = evaluationStack.pop().As<string>();

                throw new OverflowException(message);
            }
        }

        throw new Exception();
    }

}

