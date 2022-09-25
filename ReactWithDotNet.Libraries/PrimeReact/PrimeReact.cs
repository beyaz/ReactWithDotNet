namespace ReactWithDotNet.PrimeReact;

public class ElementBase : ThirdPartyReactComponent
{
    [React]
    public string tooltip { get; set; }

    [React]
    public TooltipOptions tooltipOptions { get; set; }

    [React]
    public string className { get; set; }

    protected ElementBase()
    {
        
    }

    protected ElementBase(params Modifier[] modifiers):base(modifiers)
    {
    }
}