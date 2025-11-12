namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Button : ElementBase
{
    /// <summary>
    /// The color of the component.
    /// </summary>
    [ReactProp]
    public string color { get; set; }

    /// <summary>
    /// The size of the component. 'small' | 'medium' | 'large'
    /// </summary>
    [ReactProp]
    public string size { get; set; }

    /// <summary>
    /// The variant to use. 'text' | 'outlined' | 'contained'
    /// </summary>
    [ReactProp]
    public string variant { get; set; }

    /// <summary>
    /// If true, the component is disabled.
    /// </summary>
    [ReactProp]
    public bool disabled { get; set; }

    /// <summary>
    /// If true, the 3D shadow/elevation is disabled.
    /// </summary>
    [ReactProp]
    public bool disableElevation { get; set; }

    /// <summary>
    /// If true, the ripple effect on focus is disabled.
    /// </summary>
    [ReactProp]
    public bool disableFocusRipple { get; set; }

    /// <summary>
    /// If true, the button will take up the full width of its container.
    /// </summary>
    [ReactProp]
    public bool fullWidth { get; set; }

    /// <summary>
    /// The URL to link to when the button is clicked.
    /// </summary>
    [ReactProp]
    public string href { get; set; }

    /// <summary>
    /// Element placed before the children.
    /// </summary>
    [ReactProp]
    public object startIcon { get; set; }

    /// <summary>
    /// Element placed after the children.
    /// </summary>
    [ReactProp]
    public object endIcon { get; set; }

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

}
