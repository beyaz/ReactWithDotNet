namespace ReactWithDotNet;

public class table : HtmlElement
{
    public table(params IModifier[] modifiers) : base(modifiers) { }
}
public class tbody : HtmlElement
{
    public tbody()
    {
        
    }

    public tbody(params IModifier[] modifiers) : base(modifiers) { }
}
public class tr : HtmlElement
{
    [React]
    public int? rowSpan { get; set; }

    [React]
    public int? colSpan { get; set; }

    public tr(params IModifier[] modifiers) : base(modifiers) { }
}

public class th : HtmlElement
{
    [React]
    public int? rowSpan { get; set; }

    [React]
    public int? colSpan { get; set; }

    public th(params IModifier[] modifiers) : base(modifiers) { }
}

public class td : HtmlElement
{
    [React]
    public int? rowSpan { get; set; }

    [React]
    public int? colSpan { get; set; }

    public td()
    {
        
    }
    public td(params IModifier[] modifiers) : base(modifiers) { }
}