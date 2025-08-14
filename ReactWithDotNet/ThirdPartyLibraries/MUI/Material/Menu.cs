namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Menu : ElementBase
{
    [ReactProp]
    public bool open { get; set; }

    [ReactProp]
    public Element anchorEl { get; set; } 

    [ReactProp]
    public bool? autoFocus { get; set; }

    [ReactProp]
    public bool? disableAutoFocusItem { get; set; }

    [ReactProp]
    public Func<object, string, Task> onClose { get; set; } // (event, reason)
       

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();

    [ReactProp]
    public string variant { get; set; }  // e.g. 'menu' | 'selectedMenu'
}