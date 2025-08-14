namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class MenuList : ElementBase
{
    [ReactProp]
    public bool? autoFocus { get; set; }

    [ReactProp]
    public bool? disableAutoFocusItem { get; set; }
   
    [ReactProp]
    public string variant { get; set; }
}