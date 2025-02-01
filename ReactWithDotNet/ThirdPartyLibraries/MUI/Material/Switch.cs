// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public partial class Switch
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
    public static Modifier Color(string value) => CreateThirdPartyReactComponentModifier<Switch>(x => x.color = value);
    
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
    ///     The size of the component.
    ///     <br/>
    ///     `small` is equivalent to the dense switch styling.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    public static Modifier Size(string value) => CreateThirdPartyReactComponentModifier<Switch>(x => x.size = value);
}
