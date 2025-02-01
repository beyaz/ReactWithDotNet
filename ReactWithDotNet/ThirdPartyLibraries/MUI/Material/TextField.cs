// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public partial class TextField
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
    ///     This prop helps users to fill forms faster, especially on mobile devices.
    ///     <br/>
    ///     The name can be confusing, as it's more like an autofill.
    ///     <br/>
    ///     You can learn more about it [following the specification](https://html.spec.whatwg.org/multipage/form-control-infrastructure.html#autofill).
    /// </summary>
    public static Modifier AutoComplete(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.autoComplete = value);
    
    /// <summary>
    ///     If `true`, the `input` element is focused during the first mount.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? autoFocus { get; set; }
    
    /// <summary>
    ///     If `true`, the `input` element is focused during the first mount.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier AutoFocus(bool? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.autoFocus = value);
    
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
    ///     [palette customization guide](https://mui.com/material-ui/customization/palette/#custom-colors).
    ///     <br/>
    ///     @default 'primary'
    /// </summary>
    [ReactProp]
    public string color { get; set; }
    
    /// <summary>
    ///     The color of the component.
    ///     <br/>
    ///     It supports both default and custom theme colors, which can be added as shown in the
    ///     <br/>
    ///     [palette customization guide](https://mui.com/material-ui/customization/palette/#custom-colors).
    ///     <br/>
    ///     @default 'primary'
    /// </summary>
    public static Modifier Color(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.color = value);
    
    /// <summary>
    ///     The default value. Use when the component is not controlled.
    /// </summary>
    [ReactProp]
    public string defaultValue { get; set; }
    
    /// <summary>
    ///     The default value. Use when the component is not controlled.
    /// </summary>
    public static Modifier DefaultValue(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.defaultValue = value);
    
    /// <summary>
    ///     If `true`, the component is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     If `true`, the component is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier Disabled(bool? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.disabled = value);
    
    /// <summary>
    ///     If `true`, the label is displayed in an error state.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? error { get; set; }
    
    /// <summary>
    ///     If `true`, the label is displayed in an error state.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier Error(bool? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.error = value);
    
    /// <summary>
    ///     Props applied to the [`FormHelperText`](https://mui.com/material-ui/api/form-helper-text/) element.
    ///     <br/>
    ///     @deprecated Use `slotProps.formHelperText` instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
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
    ///     If `true`, the input will take up the full width of its container.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier FullWidth(bool? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.fullWidth = value);
    
    /// <summary>
    ///     The helper text content.
    /// </summary>
    [ReactProp]
    public Element helperText { get; set; }
    
    /// <summary>
    ///     The helper text content.
    /// </summary>
    public static Modifier HelperText(Element value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.helperText = value);
    
    /// <summary>
    ///     The id of the `input` element.
    ///     <br/>
    ///     Use this prop to make `label` and `helperText` accessible for screen readers.
    /// </summary>
    [ReactProp]
    public string id { get; set; }
    
    /// <summary>
    ///     The id of the `input` element.
    ///     <br/>
    ///     Use this prop to make `label` and `helperText` accessible for screen readers.
    /// </summary>
    public static Modifier Id(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.id = value);
    
    /// <summary>
    ///     Props applied to the [`InputLabel`](https://mui.com/material-ui/api/input-label/) element.
    ///     <br/>
    ///     Pointer events like `onClick` are enabled if and only if `shrink` is `true`.
    ///     <br/>
    ///     @deprecated Use `slotProps.inputLabel` instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic InputLabelProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     [Attributes](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Attributes) applied to the `input` element.
    ///     <br/>
    ///     @deprecated Use `slotProps.htmlInput` instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic inputProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The label content.
    /// </summary>
    [ReactProp]
    public Element label { get; set; }
    
    /// <summary>
    ///     The label content.
    /// </summary>
    public static Modifier Label(Element value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.label = value);
    
    /// <summary>
    ///     If `true`, a `textarea` element is rendered instead of an input.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? multiline { get; set; }
    
    /// <summary>
    ///     If `true`, a `textarea` element is rendered instead of an input.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier Multiline(bool? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.multiline = value);
    
    /// <summary>
    ///     Name attribute of the `input` element.
    /// </summary>
    [ReactProp]
    public string name { get; set; }
    
    /// <summary>
    ///     Name attribute of the `input` element.
    /// </summary>
    public static Modifier Name(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.name = value);
    
    
    
    /// <summary>
    ///     The short hint displayed in the `input` before the user enters a value.
    /// </summary>
    [ReactProp]
    public string placeholder { get; set; }
    
    /// <summary>
    ///     The short hint displayed in the `input` before the user enters a value.
    /// </summary>
    public static Modifier Placeholder(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.placeholder = value);
    
    /// <summary>
    ///     If `true`, the label is displayed as required and the `input` element is required.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? required { get; set; }
    
    /// <summary>
    ///     If `true`, the label is displayed as required and the `input` element is required.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier Required(bool? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.required = value);
    
    /// <summary>
    ///     Number of rows to display when multiline option is set to true.
    /// </summary>
    [ReactProp]
    public int? rows { get; set; }
    
    /// <summary>
    ///     Number of rows to display when multiline option is set to true.
    /// </summary>
    public static Modifier Rows(int? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.rows = value);
    
    /// <summary>
    ///     Maximum number of rows to display when multiline option is set to true.
    /// </summary>
    [ReactProp]
    public int? maxRows { get; set; }
    
    /// <summary>
    ///     Maximum number of rows to display when multiline option is set to true.
    /// </summary>
    public static Modifier MaxRows(int? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.maxRows = value);
    
    /// <summary>
    ///     Minimum number of rows to display when multiline option is set to true.
    /// </summary>
    [ReactProp]
    public int? minRows { get; set; }
    
    /// <summary>
    ///     Minimum number of rows to display when multiline option is set to true.
    /// </summary>
    public static Modifier MinRows(int? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.minRows = value);
    
    /// <summary>
    ///     Render a [`Select`](https://mui.com/material-ui/api/select/) element while passing the Input element to `Select` as `input` parameter.
    ///     <br/>
    ///     If this option is set you must pass the options of the select as children.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? select { get; set; }
    
    /// <summary>
    ///     Render a [`Select`](https://mui.com/material-ui/api/select/) element while passing the Input element to `Select` as `input` parameter.
    ///     <br/>
    ///     If this option is set you must pass the options of the select as children.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier Select(bool? value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.select = value);
    
    /// <summary>
    ///     Props applied to the [`Select`](https://mui.com/material-ui/api/select/) element.
    ///     <br/>
    ///     @deprecated Use `slotProps.select` instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic SelectProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    [ReactProp]
    public string size { get; set; }
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    public static Modifier Size(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.size = value);
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     Type of the `input` element. It should be [a valid HTML5 input type](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Form_%3Cinput%3E_types).
    /// </summary>
    [ReactProp]
    public string type { get; set; }
    
    /// <summary>
    ///     Type of the `input` element. It should be [a valid HTML5 input type](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Form_%3Cinput%3E_types).
    /// </summary>
    public new static Modifier Type(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.type = value);
    
    /// <summary>
    ///     The value of the `input` element, required for a controlled component.
    /// </summary>
    [ReactProp]
    public string value { get; set; }
    
    /// <summary>
    ///     The value of the `input` element, required for a controlled component.
    /// </summary>
    public static Modifier Value(string value) => CreateThirdPartyReactComponentModifier<TextField>(x => x.value = value);
}
