// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public class SwitchBase : ButtonBase
{
    [ReactProp]
    public bool? autoFocus { get; set; }
    
    public static Modifier AutoFocus(bool? value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.autoFocus = value);
    
    /// <summary>
    ///     If `true`, the component is checked.
    /// </summary>
    [ReactProp]
    public bool? @checked { get; set; }
    
    /// <summary>
    ///     If `true`, the component is checked.
    /// </summary>
    public static Modifier Checked(bool? value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.@checked = value);
    
    [ReactProp]
    public Element checkedIcon { get; set; }
    
    public static Modifier CheckedIcon(Element value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.checkedIcon = value);
    
    /// <summary>
    ///     The default checked state. Use when the component is not controlled.
    /// </summary>
    [ReactProp]
    public bool? defaultChecked { get; set; }
    
    /// <summary>
    ///     The default checked state. Use when the component is not controlled.
    /// </summary>
    public static Modifier DefaultChecked(bool? value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.defaultChecked = value);
    
    /// <summary>
    ///     If `true`, the  keyboard focus ripple is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disableFocusRipple { get; set; }
    
    /// <summary>
    ///     If `true`, the  keyboard focus ripple is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier DisableFocusRipple(bool? value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.disableFocusRipple = value);
    
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
    public object edge { get; set; }
    
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
    public static Modifier Edge(object value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.edge = value);
    
    [ReactProp]
    public Element icon { get; set; }
    
    public static Modifier Icon(Element value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.icon = value);
    
    /// <summary>
    ///     The id of the `input` element.
    /// </summary>
    [ReactProp]
    public string id { get; set; }
    
    /// <summary>
    ///     The id of the `input` element.
    /// </summary>
    public static Modifier Id(string value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.id = value);
    
    /// <summary>
    ///     [Attributes](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Attributes) applied to the `input` element.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic inputProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     Pass a ref to the `input` element.
    /// </summary>
    
    /// <summary>
    ///     Name attribute of the `input` element.
    /// </summary>
    [ReactProp]
    public string name { get; set; }
    
    /// <summary>
    ///     Name attribute of the `input` element.
    /// </summary>
    public static Modifier Name(string value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.name = value);
    
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
    public Func<ChangeEvent, bool?, Task> onChange {get;set;}
    
    [ReactProp]
    public bool? readOnly { get; set; }
    
    public static Modifier ReadOnly(bool? value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.readOnly = value);
    
    /// <summary>
    ///     If `true`, the `input` element is required.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? required { get; set; }
    
    /// <summary>
    ///     If `true`, the `input` element is required.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier Required(bool? value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.required = value);
    
    [ReactProp]
    public double? tabIndex { get; set; }
    
    public static Modifier TabIndex(double? value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.tabIndex = value);
    
    [ReactProp]
    public string type { get; set; }
    
    public new static Modifier Type(string value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.type = value);
    
    /// <summary>
    ///     The value of the component. The DOM API casts this to a string.
    /// </summary>
    [ReactProp]
    public string value { get; set; }
    
    /// <summary>
    ///     The value of the component. The DOM API casts this to a string.
    /// </summary>
    public static Modifier Value(string value) => CreateThirdPartyReactComponentModifier<SwitchBase>(x => x.value = value);
}
