// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public partial class Switch
{
    /// <summary>
    ///     The icon to display when the component is checked.
    /// </summary>
    [ReactProp]
    public Element checkedIcon { get; set; }
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     The color of the component.
    ///     <br/>
    ///     It supports both default and custom theme colors, which can be added as shown in the
    ///     <br/>
    ///     [palette customization guide](https://mui.com/material-ui/customization/palette/#adding-new-colors).
    ///     <br/>
    ///     @default 'primary'
    /// </summary>
    [ReactProp]
    public string color { get; set; }
    
    /// <summary>
    ///     If `true`, the component is disabled.
    /// </summary>
    [ReactProp]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     The icon to display when the component is unchecked.
    /// </summary>
    [ReactProp]
    public Element icon { get; set; }
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     `small` is equivalent to the dense switch styling.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    [ReactProp]
    public string size { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The value of the component. The DOM API casts this to a string.
    ///     <br/>
    ///     The browser uses "on" as the default value.
    /// </summary>
    [ReactProp]
    public string value { get; set; }
}
