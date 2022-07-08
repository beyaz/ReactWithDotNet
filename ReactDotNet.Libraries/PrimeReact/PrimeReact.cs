using ReactDotNet.Html5;

namespace ReactDotNet.PrimeReact;

public class ElementBase : ThirdPartyComponent
{

    public ElementBase(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    public ElementBase()
    {
            
    }


    [React]
    public string tooltip { get; set; }

    [React]
    public TooltipOptions tooltipOptions { get; set; } 
}