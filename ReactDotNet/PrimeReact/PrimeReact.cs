using System.Collections.Generic;

namespace ReactDotNet.Html5.PrimeReact;

public class ElementBase : ThirdPartyComponent
{
    public override IReadOnlyList<string> jsLocation => new[] { "primereact", GetType().Name };


    public  string FullName => $"primereact.{GetType().Name}";

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