namespace ReactWithDotNet.Libraries.mui.material;

public sealed class FormGroup : ElementBase
{

    
}


public sealed class FormControlLabel : ElementBase
{
    [React]
    public string? label { get; set; }

    [React]
    public Element control { get; set; }

}