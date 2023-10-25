namespace ReactWithDotNet;

public sealed class table : HtmlElement
{
    [ReactProp]
    public double? cellSpacing  { get; set; }
    
    [ReactProp]
    public double? cellPadding  { get; set; }
    
 
    
    public table(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

