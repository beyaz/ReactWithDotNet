namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class MenuItem : ElementBase
{
    [ReactProp] 
    public bool? autoFocus { get; set; }

    [ReactProp] 
    public Element component { get; set; }

    [ReactProp] 
    public bool? dense { get; set; }

    [ReactProp] 
    public bool? disableGutters { get; set; }

    [ReactProp] 
    public bool? disabled { get; set; }

    [ReactProp] 
    public bool? divider { get; set; }

    [ReactProp] 
    public string alignItems { get; set; } // 'center' | 'flex-start'

    [ReactProp] 
    public string selected { get; set; }

    [ReactProp] 
    public MouseEventHandler onClick { get; set; }

    [ReactProp] 
    public object classes { get; set; }

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
        
}