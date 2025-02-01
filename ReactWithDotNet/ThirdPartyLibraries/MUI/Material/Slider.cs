// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Slider : ElementBase
{
    /// <summary>
    ///     The label of the slider.
    /// </summary>
    [ReactProp]
    public string ariaLabel { get; set; }
    
    /// <summary>
    ///     The label of the slider.
    /// </summary>
    public static Modifier AriaLabel(string value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.ariaLabel = value);
    
    /// <summary>
    ///     The id of the element containing a label for the slider.
    /// </summary>
    [ReactProp]
    public string ariaLabelledby { get; set; }
    
    /// <summary>
    ///     The id of the element containing a label for the slider.
    /// </summary>
    public static Modifier AriaLabelledby(string value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.ariaLabelledby = value);
    
    /// <summary>
    ///     A string value that provides a user-friendly name for the current value of the slider.
    /// </summary>
    [ReactProp]
    public string ariaValuetext { get; set; }
    
    /// <summary>
    ///     A string value that provides a user-friendly name for the current value of the slider.
    /// </summary>
    public static Modifier AriaValuetext(string value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.ariaValuetext = value);
    
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
    public static Modifier Color(string value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.color = value);
    
    /// <summary>
    ///     The components used for each slot inside.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @deprecated use the `slots` prop instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @default {}
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic components { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The extra props for the slot components.
    ///     <br/>
    ///     You can override the existing props or add new ones.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @deprecated use the `slotProps` prop instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @default {}
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic componentsProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The default value. Use when the component is not controlled.
    /// </summary>
    [ReactProp]
    public object defaultValue { get; set; }
    
    /// <summary>
    ///     The default value. Use when the component is not controlled.
    /// </summary>
    public static Modifier DefaultValue(object value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.defaultValue = value);
    
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
    public static Modifier Disabled(bool? value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.disabled = value);
    
    /// <summary>
    ///     If `true`, the active thumb doesn't swap when moving pointer over a thumb while dragging another thumb.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disableSwap { get; set; }
    
    /// <summary>
    ///     If `true`, the active thumb doesn't swap when moving pointer over a thumb while dragging another thumb.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier DisableSwap(bool? value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.disableSwap = value);
    
    /// <summary>
    ///     Accepts a function which returns a string value that provides a user-friendly name for the thumb labels of the slider.
    ///     <br/>
    ///     This is important for screen reader users.
    ///     <br/>
    ///     @param {number} index The thumb label's index to format.
    ///     <br/>
    ///     @returns {string}
    /// </summary>
    
    /// <summary>
    ///     Accepts a function which returns a string value that provides a user-friendly name for the current value of the slider.
    ///     <br/>
    ///     This is important for screen reader users.
    ///     <br/>
    ///     @param {number} value The thumb label's value to format.
    ///     <br/>
    ///     @param {number} index The thumb label's index to format.
    ///     <br/>
    ///     @returns {string}
    /// </summary>
    
    /// <summary>
    ///     Marks indicate predetermined values to which the user can move the slider.
    ///     <br/>
    ///     If `true` the marks are spaced according the value of the `step` prop.
    ///     <br/>
    ///     If an array, it should contain objects with `value` and an optional `label` keys.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public object marks { get; set; }
    
    /// <summary>
    ///     Marks indicate predetermined values to which the user can move the slider.
    ///     <br/>
    ///     If `true` the marks are spaced according the value of the `step` prop.
    ///     <br/>
    ///     If an array, it should contain objects with `value` and an optional `label` keys.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier Marks(object value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.marks = value);
    
    /// <summary>
    ///     The maximum allowed value of the slider.
    ///     <br/>
    ///     Should not be equal to min.
    ///     <br/>
    ///     @default 100
    /// </summary>
    [ReactProp]
    public double? max { get; set; }
    
    /// <summary>
    ///     The maximum allowed value of the slider.
    ///     <br/>
    ///     Should not be equal to min.
    ///     <br/>
    ///     @default 100
    /// </summary>
    public static Modifier Max(double? value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.max = value);
    
    /// <summary>
    ///     The minimum allowed value of the slider.
    ///     <br/>
    ///     Should not be equal to max.
    ///     <br/>
    ///     @default 0
    /// </summary>
    [ReactProp]
    public double? min { get; set; }
    
    /// <summary>
    ///     The minimum allowed value of the slider.
    ///     <br/>
    ///     Should not be equal to max.
    ///     <br/>
    ///     @default 0
    /// </summary>
    public static Modifier Min(double? value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.min = value);
    
    /// <summary>
    ///     Name attribute of the hidden `input` element.
    /// </summary>
    [ReactProp]
    public string name { get; set; }
    
    /// <summary>
    ///     Name attribute of the hidden `input` element.
    /// </summary>
    public static Modifier Name(string value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.name = value);
    
    /// <summary>
    ///     Callback function that is fired when the slider's value changed.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {Event} event The event source of the callback.
    ///     <br/>
    ///     You can pull out the new value by accessing `event.target.value` (any).
    ///     <br/>
    ///     **Warning**: This is a generic event not a change event.
    ///     <br/>
    ///     @param {Value} value The new value.
    ///     <br/>
    ///     @param {number} activeThumb Index of the currently moved thumb.
    /// </summary>
    
    /// <summary>
    ///     Callback function that is fired when the `mouseup` is triggered.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {React.SyntheticEvent | Event} event The event source of the callback. **Warning**: This is a generic event not a change event.
    ///     <br/>
    ///     @param {Value} value The new value.
    /// </summary>
    [ReactProp]
    public MouseEvent onChangeCommitted { get; set; }
    
    /// <summary>
    ///     Callback function that is fired when the `mouseup` is triggered.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {React.SyntheticEvent | Event} event The event source of the callback. **Warning**: This is a generic event not a change event.
    ///     <br/>
    ///     @param {Value} value The new value.
    /// </summary>
    public static Modifier OnChangeCommitted(MouseEvent value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.onChangeCommitted = value);
    
    /// <summary>
    ///     The component orientation.
    ///     <br/>
    ///     @default 'horizontal'
    /// </summary>
    [ReactProp]
    public string orientation { get; set; }
    
    /// <summary>
    ///     The component orientation.
    ///     <br/>
    ///     @default 'horizontal'
    /// </summary>
    public static Modifier Orientation(string value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.orientation = value);
    
    /// <summary>
    ///     A transformation function, to change the scale of the slider.
    ///     <br/>
    ///     @param {any} x
    ///     <br/>
    ///     @returns {any}
    ///     <br/>
    ///     @default function Identity(x) {
    ///     <br/>
    ///     return x;
    ///     <br/>
    ///     }
    /// </summary>
    
    /// <summary>
    ///     The granularity with which the slider can step through values when using Page Up/Page Down or Shift + Arrow Up/Arrow Down.
    ///     <br/>
    ///     @default 10
    /// </summary>
    [ReactProp]
    public double? shiftStep { get; set; }
    
    /// <summary>
    ///     The granularity with which the slider can step through values when using Page Up/Page Down or Shift + Arrow Up/Arrow Down.
    ///     <br/>
    ///     @default 10
    /// </summary>
    public static Modifier ShiftStep(double? value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.shiftStep = value);
    
    /// <summary>
    ///     The size of the slider.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    [ReactProp]
    public string size { get; set; }
    
    /// <summary>
    ///     The size of the slider.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    public static Modifier Size(string value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.size = value);
    
    /// <summary>
    ///     The props used for each slot inside the Slider.
    ///     <br/>
    ///     @default {}
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic slotProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The components used for each slot inside the Slider.
    ///     <br/>
    ///     Either a string to use a HTML element or a component.
    ///     <br/>
    ///     @default {}
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic slots { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The granularity with which the slider can step through values. (A "discrete" slider.)
    ///     <br/>
    ///     The `min` prop serves as the origin for the valid values.
    ///     <br/>
    ///     We recommend (max - min) to be evenly divisible by the step.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     When step is `null`, the thumb can only be slid onto marks provided with the `marks` prop.
    ///     <br/>
    ///     @default 1
    /// </summary>
    [ReactProp]
    public object step { get; set; }
    
    /// <summary>
    ///     The granularity with which the slider can step through values. (A "discrete" slider.)
    ///     <br/>
    ///     The `min` prop serves as the origin for the valid values.
    ///     <br/>
    ///     We recommend (max - min) to be evenly divisible by the step.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     When step is `null`, the thumb can only be slid onto marks provided with the `marks` prop.
    ///     <br/>
    ///     @default 1
    /// </summary>
    public static Modifier Step(object value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.step = value);
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     Tab index attribute of the hidden `input` element.
    /// </summary>
    [ReactProp]
    public double? tabIndex { get; set; }
    
    /// <summary>
    ///     Tab index attribute of the hidden `input` element.
    /// </summary>
    public static Modifier TabIndex(double? value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.tabIndex = value);
    
    /// <summary>
    ///     The track presentation:
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     - `normal` the track will render a bar representing the slider value.
    ///     <br/>
    ///     - `inverted` the track will render a bar representing the remaining slider value.
    ///     <br/>
    ///     - `false` the track will render without a bar.
    ///     <br/>
    ///     @default 'normal'
    /// </summary>
    [ReactProp]
    public object track { get; set; }
    
    /// <summary>
    ///     The track presentation:
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     - `normal` the track will render a bar representing the slider value.
    ///     <br/>
    ///     - `inverted` the track will render a bar representing the remaining slider value.
    ///     <br/>
    ///     - `false` the track will render without a bar.
    ///     <br/>
    ///     @default 'normal'
    /// </summary>
    public static Modifier Track(object value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.track = value);
    
    /// <summary>
    ///     The value of the slider.
    ///     <br/>
    ///     For ranged sliders, provide an array with two values.
    /// </summary>
    [ReactProp]
    public object value { get; set; }
    
    /// <summary>
    ///     The value of the slider.
    ///     <br/>
    ///     For ranged sliders, provide an array with two values.
    /// </summary>
    public static Modifier Value(object value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.value = value);
    
    /// <summary>
    ///     Controls when the value label is displayed:
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     - `auto` the value label will display when the thumb is hovered or focused.
    ///     <br/>
    ///     - `on` will display persistently.
    ///     <br/>
    ///     - `off` will never display.
    ///     <br/>
    ///     @default 'off'
    /// </summary>
    [ReactProp]
    public string valueLabelDisplay { get; set; }
    
    /// <summary>
    ///     Controls when the value label is displayed:
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     - `auto` the value label will display when the thumb is hovered or focused.
    ///     <br/>
    ///     - `on` will display persistently.
    ///     <br/>
    ///     - `off` will never display.
    ///     <br/>
    ///     @default 'off'
    /// </summary>
    public static Modifier ValueLabelDisplay(string value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.valueLabelDisplay = value);
    
    /// <summary>
    ///     The format function the value label's value.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     When a function is provided, it should have the following signature:
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     - {number} value The value label's value to format
    ///     <br/>
    ///     - {number} index The value label's index to format
    ///     <br/>
    ///     @param {any} x
    ///     <br/>
    ///     @returns {any}
    ///     <br/>
    ///     @default function Identity(x) {
    ///     <br/>
    ///     return x;
    ///     <br/>
    ///     }
    /// </summary>
    [ReactProp]
    public Element valueLabelFormat { get; set; }
    
    /// <summary>
    ///     The format function the value label's value.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     When a function is provided, it should have the following signature:
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     - {number} value The value label's value to format
    ///     <br/>
    ///     - {number} index The value label's index to format
    ///     <br/>
    ///     @param {any} x
    ///     <br/>
    ///     @returns {any}
    ///     <br/>
    ///     @default function Identity(x) {
    ///     <br/>
    ///     return x;
    ///     <br/>
    ///     }
    /// </summary>
    public static Modifier ValueLabelFormat(Element value) => CreateThirdPartyReactComponentModifier<Slider>(x => x.valueLabelFormat = value);
}
