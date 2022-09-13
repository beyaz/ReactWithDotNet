namespace ReactWithDotNet;

public class svg : HtmlElement
{
    [React]
    public string xmlns { get; set; } = "http://www.w3.org/2000/svg";

    [React]
    public string viewBox { get; set; }

    [React]
    public string fill { get; set; }

    [React]
    public string width { get; set; }

    [React]
    public string height { get; set; }
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

public class polygon : HtmlElement
{
    [React]
    public string points { get; set; }

    [React]
    public string fill { get; set; }

    [React]
    public string stroke { get; set; }

    [React]
    public string strokeWidth { get; set; }


    [React]
    public string transform { get; set; }
    
}
