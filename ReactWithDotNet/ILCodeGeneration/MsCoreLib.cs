using ReactWithDotNet;
using static ReactWithDotNet.NativeJsHelper;


namespace _System_
{
    class Object
    {
        public Object()
        {
        
        }
    }

    class Int64
    {
        public override string ToString()
        {
            return this.Call("toString").As<string>();
        }
    }
    
    class Exception
    {
        public Exception()
        {
            
        }

        public Exception(string message)
        {
            Message = message;
        }
        
        public Exception(string message, Exception innerException)
            : this()
        {
            Message        = message;
            InnerException = innerException;
        }

        public string Message { get; }
        
        public Exception InnerException { get; }
    }
    
    class String
    {
        public int Length => this.Get("length").As<int>();

        public static string Concat(string a, string b)
        {
            return Sum(a, b);
        }
        
    }
}