using Microsoft.AspNetCore.Components;

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Tabs : ElementBase
{
    /// <summary>
    /// The value of the currently selected Tab. 
    /// If you don’t want any selected Tab, set this to false.
    /// </summary>
    [ReactProp]
    public string value { get; set; }

    /// <summary>
    /// Callback fired when the value changes.
    /// </summary>
    [ReactProp]
    public EventHandler<ChangeEventArgs> onChange { get; set; }

    /// <summary>
    /// Determines the orientation of the Tabs.
    /// 'horizontal' | 'vertical'
    /// </summary>
    [ReactProp]
    public string orientation { get; set; }

    /// <summary>
    /// Determines the order of the indicator.
    /// 'primary' | 'secondary'
    /// </summary>
    [ReactProp]
    public string textColor { get; set; }

    /// <summary>
    /// The variant to use.
    /// 'standard' | 'scrollable' | 'fullWidth'
    /// </summary>
    [ReactProp]
    public string variant { get; set; }

    /// <summary>
    /// Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new();

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
}