// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

partial class TextField
{
    /// <summary>
    ///     This prop helps users to fill forms faster, especially on mobile devices.
    ///     <br/>
    ///     The name can be confusing, as it's more like an autofill.
    ///     <br/>
    ///     You can learn more about it [following the specification](https://html.spec.whatwg.org/multipage/form-control-infrastructure.html#autofill).
    /// </summary>
    [ReactProp]
    public string autoComplete { get; set; }
    
    /// <summary>
    ///     If `true`, the `input` element is focused during the first mount.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? autoFocus { get; set; }
    
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
    ///     The default value. Use when the component is not controlled.
    /// </summary>
    [ReactProp]
    public string defaultValue { get; set; }
    
    /// <summary>
    ///     If `true`, the component is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     If `true`, the label is displayed in an error state.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? error { get; set; }
    
    /// <summary>
    ///     Props applied to the [`FormHelperText`](/material-ui/api/form-helper-text/) element.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic FormHelperTextProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     If `true`, the input will take up the full width of its container.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? fullWidth { get; set; }
    
    /// <summary>
    ///     The helper text content.
    /// </summary>
    [ReactProp]
    public Element helperText { get; set; }
    
    /// <summary>
    ///     The id of the `input` element.
    ///     <br/>
    ///     Use this prop to make `label` and `helperText` accessible for screen readers.
    /// </summary>
    [ReactProp]
    public string id { get; set; }
    
    /// <summary>
    ///     Props applied to the [`InputLabel`](/material-ui/api/input-label/) element.
    ///     <br/>
    ///     Pointer events like `onClick` are enabled if and only if `shrink` is `true`.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic InputLabelProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     [Attributes](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Attributes) applied to the `input` element.
    /// </summary>
    [ReactProp]
    public string inputProps { get; set; }
    
    /// <summary>
    ///     The label content.
    /// </summary>
    [ReactProp]
    public Element label { get; set; }
    
    /// <summary>
    ///     If `true`, a `textarea` element is rendered instead of an input.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? multiline { get; set; }
    
    /// <summary>
    ///     Name attribute of the `input` element.
    /// </summary>
    [ReactProp]
    public string name { get; set; }
    
    [ReactProp]
    public string onBlur { get; set; }
    
    [ReactProp]
    public string onFocus { get; set; }
    
    /// <summary>
    ///     The short hint displayed in the `input` before the user enters a value.
    /// </summary>
    [ReactProp]
    public string placeholder { get; set; }
    
    /// <summary>
    ///     If `true`, the label is displayed as required and the `input` element is required.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? required { get; set; }
    
    /// <summary>
    ///     Number of rows to display when multiline option is set to true.
    /// </summary>
    [ReactProp]
    public string rows { get; set; }
    
    /// <summary>
    ///     Maximum number of rows to display when multiline option is set to true.
    /// </summary>
    [ReactProp]
    public string maxRows { get; set; }
    
    /// <summary>
    ///     Minimum number of rows to display when multiline option is set to true.
    /// </summary>
    [ReactProp]
    public string minRows { get; set; }
    
    /// <summary>
    ///     Render a [`Select`](/material-ui/api/select/) element while passing the Input element to `Select` as `input` parameter.
    ///     <br/>
    ///     If this option is set you must pass the options of the select as children.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? select { get; set; }
    
    /// <summary>
    ///     Props applied to the [`Select`](/material-ui/api/select/) element.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic SelectProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The size of the component.
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
    ///     Type of the `input` element. It should be [a valid HTML5 input type](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Form_%3Cinput%3E_types).
    /// </summary>
    [ReactProp]
    public string type { get; set; }
    
    /// <summary>
    ///     The value of the `input` element, required for a controlled component.
    /// </summary>
    [ReactProp]
    public string value { get; set; }
}
