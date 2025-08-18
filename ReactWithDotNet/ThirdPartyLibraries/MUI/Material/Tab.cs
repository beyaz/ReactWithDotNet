namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Tab : ElementBase
{
    /// <summary>
    /// The label element of the tab.
    /// </summary>
    [ReactProp]
    public string label { get; set; }

    /// <summary>
    /// The value of the Tab. 
    /// It will be used to match against the value passed to the Tabs component.
    /// </summary>
    [ReactProp]
    public string value { get; set; }

    /// <summary>
    /// If true, the tab will be disabled.
    /// </summary>
    [ReactProp]
    public bool disabled { get; set; }

    /// <summary>
    /// Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new();

    /// <summary>
    /// The icon to display.
    /// </summary>
    [ReactProp]
    public Element icon { get; set; }

    /// <summary>
    /// The position of the icon.
    /// 'top' | 'bottom' | 'start' | 'end'
    /// </summary>
    [ReactProp]
    public string iconPosition { get; set; }

    /// <summary>
    /// The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();

    /// <summary>
    /// The id of the component.
    /// </summary>
    [ReactProp]
    public string id { get; set; }

    [ReactProp]
    public MouseEventHandler onClick { get; set; }
}