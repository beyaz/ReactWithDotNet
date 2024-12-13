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
    
    
    
    class String
    {
        public int Length => this.Get("length").As<int>();

        public static bool op_Inequality(string a, string b)
        {
            if (a==b)
            {
                return false;
            }

            return true;
        }
        
    }
}