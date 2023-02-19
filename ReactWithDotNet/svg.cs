namespace ReactWithDotNet;

public class svg : HtmlElement
{
    public svg()
    {
        
    }
    
    public svg(params IModifier[] modifiers) : base(modifiers) { }

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

    [React]
    public string preserveAspectRatio { get; set; }


    /// <summary>
    /// svg.fill = <paramref name="color"/>
    /// </summary>
    public static HtmlElementModifier Fill(string color) => CreateHtmlElementModifier<svg>(el => el.fill = color);

}

public class path : HtmlElement
{
    [React]
    public string d { get; set; }

    [React]
    public string fill { get; set; }

    [React]
    public string stroke { get; set; }
    
}

public class g : HtmlElement
{
    [React]
    public string transform { get; set; }

    [React]
    public string opacity { get; set; }
}

public class defs : HtmlElement
{
}

public class clipPath : HtmlElement
{
}

public class rect : HtmlElement
{
    [React]
    public string x { get; set; }
    
    [React]
    public string y { get; set; }

    [React]
    public string width { get; set; }

    [React]
    public string height { get; set; }

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
