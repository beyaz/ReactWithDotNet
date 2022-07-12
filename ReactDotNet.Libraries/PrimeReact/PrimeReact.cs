namespace ReactDotNet.PrimeReact;

public class ElementBase : ThirdPartyReactComponent
{
    [React]
    public string tooltip { get; set; }

    [React]
    public TooltipOptions tooltipOptions { get; set; }

    [React]
    public string className { get; set; }
}