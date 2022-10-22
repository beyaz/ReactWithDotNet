namespace ReactWithDotNet;

public class table : HtmlElement
{
    public table(params IModifier[] modifiers) : base(modifiers) { }
}
public class tbody : HtmlElement
{

}
public class tr : HtmlElement
{
    [React]
    public int? rowSpan { get; set; }

    [React]
    public int? colSpan { get; set; }
}

public class th : HtmlElement
{
    [React]
    public int? rowSpan { get; set; }

    [React]
    public int? colSpan { get; set; }
}

public class td : HtmlElement
{
    [React]
    public int? rowSpan { get; set; }

    [React]
    public int? colSpan { get; set; }
}