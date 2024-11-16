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
        int _HResult;
        public int HResult
        {
            get => _HResult; set=>_HResult = value; }
        
        readonly string _message;
        public Exception()
        {
            
        }

        public Exception(string message)
        {
            _message = message;
        }
        
        public Exception(string message, Exception innerException)
            : this()
        {
            _message       = message;
            InnerException = innerException;
        }

        // ReSharper disable once ConvertToAutoProperty
        public string Message => _message;
        
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