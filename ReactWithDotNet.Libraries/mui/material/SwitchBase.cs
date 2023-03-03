// auto generated code (do not edit manually)

namespace ReactWithDotNet.Libraries.mui.material;

public class SwitchBase : ButtonBase
{
    [React]
    public bool? autoFocus { get; set; }
    
    /// <summary>
    ///     If `true`, the component is checked.
    /// </summary>
    [React]
    public bool? @checked { get; set; }
    
    [React]
    public Element checkedIcon { get; set; }
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [React]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     The default checked state. Use when the component is not controlled.
    /// </summary>
    [React]
    public bool? defaultChecked { get; set; }
    
    /// <summary>
    ///     If `true`, the component is disabled.
    /// </summary>
    [React]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     If `true`, the ripple effect is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public bool? disableRipple { get; set; }
    
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
    
    [React]
    public Element icon { get; set; }
    
    /// <summary>
    ///     The id of the `input` element.
    /// </summary>
    [React]
    public string id { get; set; }
    
    /// <summary>
    ///     [Attributes](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Attributes) applied to the `input` element.
    /// </summary>
    [React]
    public string inputProps { get; set; }
    
    /// <summary>
    ///     Pass a ref to the `input` element.
    /// </summary>
    
    /// <summary>
    ///     Name attribute of the `input` element.
    /// </summary>
    [React]
    public string name { get; set; }
    
    /// <summary>
    ///     Callback fired when the state is changed.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {React.ChangeEvent<HTMLInputElement>} event The event source of the callback.
    ///     <br/>
    ///     You can pull out the new value by accessing `event.target.value` (string).
    ///     <br/>
    ///     You can pull out the new checked state by accessing `event.target.checked` (boolean).
    /// </summary>
    
    [React]
    public bool? readOnly { get; set; }
    
    /// <summary>
    ///     If `true`, the `input` element is required.
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public bool? required { get; set; }
    
    [React]
    public double? tabIndex { get; set; }
    
    [React]
    public string type { get; set; }
    
    /// <summary>
    ///     The value of the component. The DOM API casts this to a string.
    /// </summary>
    [React]
    public string value { get; set; }
}
