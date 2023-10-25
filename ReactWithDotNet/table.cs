namespace ReactWithDotNet;

public sealed class table : HtmlElement
{
    [ReactProp]
    public double? cellSpacing  { get; set; }
    
    [ReactProp]
    public double? cellPadding  { get; set; }
    
    /// <summary>
    ///     table.cellSpacing = <paramref name="value" />
    /// </summary>
    public static HtmlElementModifier CellSpacing(double? value) =>
        CreateHtmlElementModifier<table>(element => element.cellSpacing = value);
    
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
