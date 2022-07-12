using ReactDotNet.Html5;

namespace ReactDotNet.PrimeReact;

public class ElementBase : ThirdPartyComponent
{
    [React]
    public string tooltip { get; set; }

    [React]
    public TooltipOptions tooltipOptions { get; set; } 
}