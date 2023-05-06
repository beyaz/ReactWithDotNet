namespace ReactWithDotNet.Libraries.mui.material;

public sealed class FormGroup : ElementBase
{

    
}


public sealed class FormControlLabel : ElementBase
{
    [ReactProp]
    public string? label { get; set; }

    [ReactProp]
    public Element control { get; set; }

}