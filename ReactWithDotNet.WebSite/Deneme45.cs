namespace ReactWithDotNet.WebSite;

class Deneme45
{
    //public string FF1;

    //public Deneme45(string a)
    //{
    //    FF1 = a+"u";
    //}

    //void CallNonStaticMethod(string abc)
    //{
    //    FF1 = "a" + "b";
    //}
    
    //public string GetA()
    //{
    //    return FF1;
    //}

    //class GenericClass1<A,B>
    //{
    //    public A a;
    //    public B b;

    //    public string P;
        
    //    public GenericClass1(string p1)
    //    {
    //        P = p1;
    //    }
    //}

    public static string Compare(int a, ref long b)
    {
        return a == b ? "Equal" : "Not Equal";
    }

    static void TestCase_Try_Catch_In_Current_Method()
    {
        var result = "0";

        var e = new Exception("success");
        
        try
        {
            throw e;
        }
        catch (Exception exception)
        {
            result = exception.Message;
        }
        
        // console.log(result);
        
        
    }

   
    
    static class ExternalCallTest
    {
        public static void Static_Void_Call()
        {
            console.log("success");
        }
        
        public static void Static_NonVoid_Call()
        {
            var result = Math.max(1, 2);
            if (result ==2)
            {
                console.log("success");    
            }
            else
            {
                console.log("fail");
            }
            
        }
    }

    static void LdInd()
    {
        sbyte v0 = 1;
        byte v1 = 1;
        short v2 = 1;
        int v3 = 1;
        float v4 = 1;
        long v5 = 1;

        var response = refSByte(ref v0);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }
        response = refByte(ref v1);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }

        response = refShort(ref v2);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }

        response = refInt32(ref v3);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }

        response = refFloat(ref v4);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }
        response = refLong(ref v5);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }

        console.log("success");
        
        return;
        
        static string refSByte(ref sbyte value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        static string refByte(ref byte value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        
        static string refShort(ref short value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        
        
        static string refInt32(ref int value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        
        static string refFloat(ref float value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        
        static string refLong(ref long value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
    }







    static void TryCatchFinaly_0()
    {
        string value = "0";
        try
        {
            value += "1";
        }
        catch (Exception)
        {
            value += "2";
        }
        finally
        {
            value += "3";
        }

        if (value == "013")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
    }
    
    static void TryCatchFinaly_1()
    {
        string value = "0";

        try
        {
            try
            {
                value += "1";
            }
            catch (Exception)
            {
                value +="2";
            }
            finally
            {
                value += "3";
            }
        }
        catch (Exception)
        {
            value += "4";
        }
        finally
        {
            value += "5";
        }

        if (value == "0135")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
    }
    
    static void TryCatch_0()
    {
        var trace = "0";
        
        try
        {
            trace += "1";

            throw new Exception("-abc-");
        }
        catch (Exception exception)
        {
            trace +=  "2";

            trace += exception.Message;
        }

        
        if (trace == "012-abc-")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
    }
    
    static void TryCatch_HandlerType()
    {
        Exception instance = null;
        
        var trace = "0";
        
        try
        {
            trace += "1";

            instance.Message.ToString();
            
            trace += "2";
        }
        catch (ArgumentOutOfRangeException)
        {
            trace +=  "3";
        }
        catch (ArgumentException)
        {
            trace +=  "4";
        }
        catch (NullReferenceException e)
        {
            trace +=  "5";
        }

        
        if (trace == "015")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
    }
    
    static void TryCatch_1()
    {
        var trace = "0";
        try
        {
            trace += "1";
            
            trace += Call1455("t");
        }
        catch (Exception exception)
        {
            trace +=  "2";

            trace += exception.Message;
        }

        
        if (trace == "012-abc-")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
        
        return;

        static string Call1455(string a)
        {
            throw new Exception("-abc-");
        }
    }

    static void BoxTest1()
    {
        int a = 5;

        object obj = a;

        if (obj is 5)
        {
            console.log("success");
            return;
        }
        
        console.log("fail");
    }
    
    static void AutomaticallyLoadType()
    {
        var instance = new InvalidProgramException("abc");

        if (instance.Message == "abc")
        {
            console.log("success");
            return;
        }
        
        console.log("fail");
    }

    static void NullableIntTest()
    {
        int? nullableInt = 5;

        if (nullableInt.HasValue)
        {
            console.log("success");
            return;
        }
        
        console.log("fail");
    }

    struct MyStruct
    {
        public int F0;
        public string F1;  
    }
    
    static void StructCreationTest()
    {
        var myStruct = new MyStruct
        {
            F0 = 4,
            F1 = "abc"
        };
        
        if (myStruct.F1 != "abc")
        {
            console.log("fail");    
            return;
        }
        
        if (myStruct.F0 != 4)
        {
            console.log("fail");    
            return;
        }

        console.log("success");
        
    }
    
    public static string Abc5()
    {
        StructCreationTest();
        
        //BoxTest1();
        //ExternalCallTest.Static_Void_Call();
        //ExternalCallTest.Static_NonVoid_Call();
        //LdInd();
        // NullableIntTest();
        //TryCatch_0();
        //TryCatch_1();
        //TryCatch_HandlerType();
        //TryCatchFinaly_0();
        //TryCatchFinaly_1();
        //AutomaticallyLoadType();

        return "E N D";
    }
}