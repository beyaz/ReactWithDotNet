namespace ReactWithDotNet;

[External]
public sealed class DomEventArg
{
    public string key;
}

[External]
public sealed class DomElementStyle
{
    public string display;
    public string width;
    public string height;
    public string border;
    public string backgroundColor;
}

[External]
public sealed class DomElement
{
    public string innerHTML;
    
    public string textContent;
    
    public DomElementStyle style;

    public extern void appendChild(DomElement child);
}


[External]
public sealed class DomDocument
{
    public extern DomElement getElementById(string id);

    public extern DomElement createElement(string tag);

    public extern void addEventListener(string keydown, Action<DomEventArg> action);
}



[External]
public sealed  class Console
{
    public extern void log(object data);
    public extern void time(string label);
    public extern void timeEnd(string label);
}

[External]
public  class Math
{
    public  extern double random();

    public  extern double floor(double value);

    public  extern int max(int a, int b);
    
    public  extern double max(double a, double b);
}


[External]
public static class window
{
    public static extern Console console { get; }
    
    public static extern DomDocument document { get; }
    
    public static extern Math Math { get; }
    
    public static extern void setInterval(Action action, int timeout);
}



