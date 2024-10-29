using static ReactWithDotNet.NativeJs;

namespace _System_
{
    class Object
    {
        public Object()
        {
        
        }
    }
    
    class String
    {
        public int Length => Get(this,"length").As<int>();

        public static string Concat(string a, string b)
        {
            return Sum(a, b);
        }
    }
}