// auto generated code (do not edit manually)

namespace ReactWithDotNet.Libraries.mui.material;

public sealed class IconButton : ButtonBase
{
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [React]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     The color of the component.
    ///     <br/>
    ///     It supports both default and custom theme colors, which can be added as shown in the
    ///     <br/>
    ///     [palette customization guide](https://mui.com/material-ui/customization/palette/#adding-new-colors).
    ///     <br/>
    ///     @default 'default'
    /// </summary>
    [React]
    public string color { get; set; }
    
    /// <summary>
    ///     If `true`, the component is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     If `true`, the  keyboard focus ripple is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public bool? disableFocusRipple { get; set; }
    
    /// <summary>
    ///     If given, uses a negative margin to counteract the padding on one
    ///     <br/>
    ///     side (this is often helpful for aligning the left or right
    ///     <br/>
    ///     side of the icon with content above or below, without ruining the border
    ///     <br/>
    ///     size and shape).
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public string edge { get; set; }
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     `small` is equivalent to the dense button styling.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    [React]
    public string size { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
    
    [React]
    public string type { get; set; }
}
