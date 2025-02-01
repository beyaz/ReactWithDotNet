// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class CircularProgress : ElementBase
{
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
    public static Modifier Color(string value) => CreateThirdPartyReactComponentModifier<CircularProgress>(x => x.color = value);
    
    /// <summary>
    ///     If `true`, the shrink animation is disabled.
    ///     <br/>
    ///     This only works if variant is `indeterminate`.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disableShrink { get; set; }
    
    /// <summary>
    ///     If `true`, the shrink animation is disabled.
    ///     <br/>
    ///     This only works if variant is `indeterminate`.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier DisableShrink(bool? value) => CreateThirdPartyReactComponentModifier<CircularProgress>(x => x.disableShrink = value);
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     If using a number, the pixel unit is assumed.
    ///     <br/>
    ///     If using a string, you need to provide the CSS unit, for example '3rem'.
    ///     <br/>
    ///     @default 40
    /// </summary>
    [ReactProp]
    public object size { get; set; }
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     If using a number, the pixel unit is assumed.
    ///     <br/>
    ///     If using a string, you need to provide the CSS unit, for example '3rem'.
    ///     <br/>
    ///     @default 40
    /// </summary>
    public static Modifier Size(object value) => CreateThirdPartyReactComponentModifier<CircularProgress>(x => x.size = value);
    
    /// <summary>
    ///     The thickness of the circle.
    ///     <br/>
    ///     @default 3.6
    /// </summary>
    [ReactProp]
    public double? thickness { get; set; }
    
    /// <summary>
    ///     The thickness of the circle.
    ///     <br/>
    ///     @default 3.6
    /// </summary>
    public static Modifier Thickness(double? value) => CreateThirdPartyReactComponentModifier<CircularProgress>(x => x.thickness = value);
    
    /// <summary>
    ///     The value of the progress indicator for the determinate variant.
    ///     <br/>
    ///     Value between 0 and 100.
    ///     <br/>
    ///     @default 0
    /// </summary>
    [ReactProp]
    public double? value { get; set; }
    
    /// <summary>
    ///     The value of the progress indicator for the determinate variant.
    ///     <br/>
    ///     Value between 0 and 100.
    ///     <br/>
    ///     @default 0
    /// </summary>
    public static Modifier Value(double? value) => CreateThirdPartyReactComponentModifier<CircularProgress>(x => x.value = value);
    
    /// <summary>
    ///     The variant to use.
    ///     <br/>
    ///     Use indeterminate when there is no progress value.
    ///     <br/>
    ///     @default 'indeterminate'
    /// </summary>
    [ReactProp]
    public string variant { get; set; }
    
    /// <summary>
    ///     The variant to use.
    ///     <br/>
    ///     Use indeterminate when there is no progress value.
    ///     <br/>
    ///     @default 'indeterminate'
    /// </summary>
    public static Modifier Variant(string value) => CreateThirdPartyReactComponentModifier<CircularProgress>(x => x.variant = value);
}
