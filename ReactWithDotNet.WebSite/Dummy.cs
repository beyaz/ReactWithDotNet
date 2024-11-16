using ReactWithDotNet.WebSite.HeaderComponents;
using System;

namespace ReactWithDotNet.WebSite;

static class Dummy
{
    public static Menu Menu => MenuAccess.MenuList.First();

    public static MenuItem MenuItem => Menu.Children.First();

    public static string VideoUrl => "https://uploads.codesandbox.io/uploads/user/fb7bd72f-ef17-4810-9e14-ca854fb0f56e/9GBo-mountain-video.mp4";
}



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
        string value = "";
        try
        {
            value += "0";
        }
        catch (Exception)
        {
            value += "1";
        }
        finally
        {
            value += "2";
        }

        if (value == "02")
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
        string value = "";

        try
        {
            try
            {
                value += "0";
            }
            catch (Exception)
            {
                value +="1";
            }
            finally
            {
                value += "2";
            }
        }
        catch (Exception)
        {
            value += "3";
        }
        finally
        {
            value += "4";
        }

        if (value == "024")
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
        var trace = "";
        try
        {
            trace += "0";
            
            trace += Call1455("t");
        }
        catch (Exception exception)
        {
            trace +=  "1";

            trace += exception.Message;
        }

        
        if (trace == "01-abc-")
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
    public static string Abc5()
    {
        //ExternalCallTest.Static_Void_Call();
        //ExternalCallTest.Static_NonVoid_Call();
        //LdInd();
        //TryCatchFinaly_0();
        //TryCatchFinaly_1();
        TryCatch_1();






        var result = "0";

        
        
        //try
        //{
        //    //try
        //    //{
        //       console.log("Call_1");
        //    //    result = Call1("A");
        //    //    result = "1";
        //    //    console.log("Call_2");
        //    //}
        //    //catch (ArgumentException exception)
        //    //{
        //    //    result = exception.Message;
        //    //}
        //    //finally
        //    //{
        //    //    console.log("Call_3");
        //    //}
        //}
        //catch (ArgumentException exception)
        //{
        //    result = exception.Message;
        //}
        //finally
        //{
        //    console.log("Call_4");
        //    result = "ok";
        //}

        return result;



        //return e.Message;

        //console.log("started");

        //var l = 't';
        //var i = 56;

        //if (l==i)
        //{
        //    i = 78;
        //}

        // return "b";

        //string a = "abo";
        //string b = "ag";



        //if (a == b)
        //{
        //    return "X";
        //}

        //return "Y";

        //var e = new Exception("abc");

        //return e.Message;

        //try
        //{
        //    return Call1455("t");
        //}
        //catch (Exception exception)
        //{
        //    return "catched";
        //}
    }

    

    //static string Call1455(string a)
    //{
    //    throw new Exception("abc");
    //}
    
    //static string Ref1(ref string a)
    //{
    //    return Call1(a);
    //}

    static string Call1(char a)
    {
        if (a == 'y')
        {
            return "r";
        }
        return "ş";
    }
    
    static string Call1(string a)
    {
        return a+"ş";
    }

    
    
    //static string GenericCall1<A, B>(string p1)
    //{
    //    return p1;
    //}

    //public static string static_method_1()
    //{
      
    //    return static_method_2("a","b",7);
    //}
    
    //public static string static_method_2(string str1, string str2, int number1)
    //{
        
        
    //    var instance = new Deneme45(str2);
        
    //    instance.CallNonStaticMethod("a");

    //    return instance.FF1;
    //}
}