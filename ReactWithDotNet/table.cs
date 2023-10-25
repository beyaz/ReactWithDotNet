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

