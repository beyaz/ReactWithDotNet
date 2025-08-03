namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Alert : ElementBase
{
    /// <summary>
    /// The severity of the alert. This defines the color and icon.
    /// 'error' | 'info' | 'success' | 'warning'
    /// </summary>
    [ReactProp]
    public string severity { get; set; }

    /// <summary>
    /// The variant to use. 'standard' | 'outlined' | 'filled'
    /// </summary>
    [ReactProp]
    public string variant { get; set; }

    /// <summary>
    /// Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();

    /// <summary>
    /// The ARIA role attribute of the element.
    /// </summary>
    [ReactProp]
    public string role { get; set; }
    

    /// <summary>
    /// The component maps the `color` prop to the theme palette.
    /// </summary>
    [ReactProp]
    public string color { get; set; }

    /// <summary>
    /// Callback fired when the close icon is clicked.
    /// </summary>
    [ReactProp]
    public MouseEventHandler onClose { get; set; }

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
    public MouseEventHandler onClick{ get; set; }
}
