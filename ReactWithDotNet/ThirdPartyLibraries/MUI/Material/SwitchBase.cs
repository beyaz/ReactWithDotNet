// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public class SwitchBase : ButtonBase
{
    [ReactProp]
    public bool? autoFocus { get; set; }
    
    /// <summary>
    ///     If `true`, the component is checked.
    /// </summary>
    [ReactProp]
    public bool? @checked { get; set; }
    
    [ReactProp]
    public Element checkedIcon { get; set; }
    
    /// <summary>
    ///     The default checked state. Use when the component is not controlled.
    /// </summary>
    [ReactProp]
    public bool? defaultChecked { get; set; }
    
    /// <summary>
    ///     If `true`, the  keyboard focus ripple is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
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
    [ReactProp]
    public string edge { get; set; }
    
    [ReactProp]
    public Element icon { get; set; }
    
    /// <summary>
    ///     The id of the `input` element.
    /// </summary>
    [ReactProp]
    public string id { get; set; }
    
    /// <summary>
    ///     [Attributes](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Attributes) applied to the `input` element.
    /// </summary>
    [ReactProp]
    public string inputProps { get; set; }
    
    /// <summary>
    ///     Pass a ref to the `input` element.
    /// </summary>
    
    /// <summary>
    ///     Name attribute of the `input` element.
    /// </summary>
    [ReactProp]
    public string name { get; set; }
    
    /// <summary>
    ///     Callback fired when the state is changed.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {React.ChangeEvent&lt;HTMLInputElement&gt;} event The event source of the callback.
    ///     <br/>
    ///     You can pull out the new value by accessing `event.target.value` (string).
    ///     <br/>
    ///     You can pull out the new checked state by accessing `event.target.checked` (boolean).
    /// </summary>
    
    [ReactProp]
    public bool? readOnly { get; set; }
    
    /// <summary>
    ///     If `true`, the `input` element is required.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? required { get; set; }
    
    [ReactProp]
    public double? tabIndex { get; set; }
    
    [ReactProp]
    public string type { get; set; }
    
    /// <summary>
    ///     The value of the component. The DOM API casts this to a string.
    /// </summary>
    [ReactProp]
    public string value { get; set; }
}
