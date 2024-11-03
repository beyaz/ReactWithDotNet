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

    static string Ref2(ref sbyte value)
    {
        if (value == 0)
        {
            return "0";
        }

        return "1";
    }
    static string Ref2(ref byte value)
    {
        if (value == 0)
        {
            return "0";
        }

        return "1";
    }
    static string Ref2(ref short value)
    {
        if (value == 0)
        {
            return "0";
        }

        return "1";
    }
    static string Ref2(ref int value)
    {
        if (value == 0)
        {
            return "0";
        }

        return "1";
    }
    static string Ref2(ref float value)
    {
        if (value == 0)
        {
            return "0";
        }

        return "1";
    }
    static string Ref2(ref long value)
    {
        if (value == 0)
        {
            return "0";
        }

        return "1";
    }
    
    public static string Abc5()
    {

        DivideByZeroException
        sbyte v0 = 1;
        byte v1 = 1;
        short v2 = 1;
        int v3 = 1;
        float v4 = 1;
        long v5 = 1;

        var response = Ref2(ref v0);
        
        response = Ref2(ref v1);
        
        response = Ref2(ref v2);
        
        response = Ref2(ref v3);
        
        response = Ref2(ref v4);
        
        response = Ref2(ref v5);
        

        return response;





        //var result = "0";

        //try
        //{
        //    result = Call1("A");
        //}
        //catch (ArgumentException exception)
        //{
        //    result = exception.Message;
        //}
        //finally
        //{
        //    result = "ok";
        //}




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