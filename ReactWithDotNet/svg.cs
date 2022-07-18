namespace ReactWithDotNet;

public class svg : HtmlElement
{
    [React]
    public string xmlns { get; set; } = "http://www.w3.org/2000/svg";

    [React]
    public string viewBox { get; set; }
}

public class path : HtmlElement
{
    [React]
    public string d { get; set; }

    [React]
    public string fill { get; set; }
}

public class rect : HtmlElement
{
    [React]
    public int y { get; set; }
}