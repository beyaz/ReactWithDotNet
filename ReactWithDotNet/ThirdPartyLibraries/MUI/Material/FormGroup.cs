﻿namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class FormGroup : ElementBase
{

    
}


public sealed class FormControlLabel : ElementBase
{
    [ReactProp]
    public UnionProp<Element, string> label { get; set; }

    [ReactProp]
    public Element control { get; set; }

}