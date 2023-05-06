namespace ReactWithDotNet;

public sealed class table : HtmlElement
{
    public table(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public sealed class thead : HtmlElement
{
    public thead()
    {
    }

    public thead(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public sealed class tbody : HtmlElement
{
    public tbody()
    {
    }

    public tbody(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public sealed class tfoot : HtmlElement
{
    public tfoot()
    {
    }

    public tfoot(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public sealed class tr : HtmlElement
{
    public tr(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    public int? colSpan { get; set; }

    [ReactProp]
    public int? rowSpan { get; set; }
}

public sealed class th : HtmlElement
{
    public th(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    public int? colSpan { get; set; }

    [ReactProp]
    public int? rowSpan { get; set; }
}

public sealed class td : HtmlElement
{
    public td()
    {
    }

    public td(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    public int? colSpan { get; set; }

    [ReactProp]
    public int? rowSpan { get; set; }
}