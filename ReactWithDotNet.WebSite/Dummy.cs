using ReactWithDotNet.WebSite.HeaderComponents;

namespace ReactWithDotNet.WebSite;

static class Dummy
{
    public static Menu Menu => MenuAccess.MenuList.First();

    public static MenuItem MenuItem => Menu.Children.First();

    public static string VideoUrl => "https://uploads.codesandbox.io/uploads/user/fb7bd72f-ef17-4810-9e14-ca854fb0f56e/9GBo-mountain-video.mp4";
}


class Deneme45
{
    public string FF1;

    public Deneme45(string a)
    {
        FF1 = a+"u";
    }

    void CallNonStaticMethod(string abc)
    {
        FF1 = "a" + "b";
    }
    
    public string GetA()
    {
        return FF1;
    }



    public static string Abc5()
    {
        string a = "o";

        return Ref1(ref a);

    }

    static string Ref1(ref string a)
    {
        return Call1(a);
    }

    static string Call1(string a)
    {
        return a+"ş";
    }

    static string GenericCall1<A, B>(string p1)
    {
        return p1;
    }

    public static string static_method_1()
    {
      
        return static_method_2("a","b",7);
    }
    
    public static string static_method_2(string str1, string str2, int number1)
    {
        
        
        var instance = new Deneme45(str2);
        
        instance.CallNonStaticMethod("a");

        return instance.FF1;
    }
}