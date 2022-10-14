namespace ReactWithDotNet.PrimeReact;

public class ElementBase : ThirdPartyReactComponent
{
    protected const string Prefix = "ReactWithDotNet.PrimeReact.";
    
    [React]
    public string tooltip { get; set; }

    [React]
    public TooltipOptions tooltipOptions { get; set; }

    [React]
    public string className { get; set; }

    protected ElementBase()
    {
        
    }

    protected ElementBase(params StyleModifier[] modifiers):base(modifiers)
    {
    }
}