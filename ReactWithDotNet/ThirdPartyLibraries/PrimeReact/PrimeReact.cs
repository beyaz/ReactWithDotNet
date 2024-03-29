﻿namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public class ElementBase : ThirdPartyReactComponent
{
    protected const string Prefix = "ReactWithDotNet.ThirdPartyLibraries.PrimeReact.";
    protected const string GrabOnlyValueParameterFromCommonPrimeReactEvent = "GrabOnlyValueParameterFromCommonPrimeReactEvent";
    protected const string GrabWithoutOriginalEvent = "GrabWithoutOriginalEvent";
    


    [ReactProp]
    public string tooltip { get; set; }

    [ReactProp]
    public string id { get; set; }

    [ReactProp]
    public TooltipOptions tooltipOptions { get; set; }

    

    protected ElementBase()
    {
        
    }

    protected ElementBase(params StyleModifier[] modifiers):base(modifiers)
    {
    }
}