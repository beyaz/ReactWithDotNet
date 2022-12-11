namespace ReactWithDotNet.Libraries.PrimeReact;

public class ElementBase : ThirdPartyReactComponent
{
    protected const string Prefix = "ReactWithDotNet.Libraries.PrimeReact.";
    protected const string GrabOnlyValueParameterFromCommonPrimeReactEvent = "GrabOnlyValueParameterFromCommonPrimeReactEvent";
    protected const string GrabWithoutOriginalEvent = "GrabWithoutOriginalEvent";
    


    [React]
    public string tooltip { get; set; }

    [React]
    public string id { get; set; }
    

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