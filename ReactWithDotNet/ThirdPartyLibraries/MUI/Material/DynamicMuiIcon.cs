namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class DynamicMuiIcon : ElementBase
{
    [ReactProp] 
    public string name { get; set; }

    [ReactProp] 
    public string color { get; set; }

    [ReactProp] 
    public string fontSize { get; set; }
}