// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class IconButton : ButtonBase
{
    /// <summary>
    ///     The color of the component.
    ///     <br/>
    ///     It supports both default and custom theme colors, which can be added as shown in the
    ///     <br/>
    ///     [palette customization guide](https://mui.com/material-ui/customization/palette/#custom-colors).
    ///     <br/>
    ///     @default 'default'
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
    ///     @default 'default'
    /// </summary>
    public static Modifier Color(string value) => CreateThirdPartyReactComponentModifier<IconButton>(x => x.color = value);
    
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
    public static Modifier DisableFocusRipple(bool? value) => CreateThirdPartyReactComponentModifier<IconButton>(x => x.disableFocusRipple = value);
    
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
    public static Modifier Edge(object value) => CreateThirdPartyReactComponentModifier<IconButton>(x => x.edge = value);
    
    /// <summary>
    ///     If `true`, the loading indicator is visible and the button is disabled.
    ///     <br/>
    ///     If `true | false`, the loading wrapper is always rendered before the children to prevent [Google Translation Crash](https://github.com/mui/material-ui/issues/27853).
    ///     <br/>
    ///     @default null
    /// </summary>
    [ReactProp]
    public object loading { get; set; }
    
    /// <summary>
    ///     If `true`, the loading indicator is visible and the button is disabled.
    ///     <br/>
    ///     If `true | false`, the loading wrapper is always rendered before the children to prevent [Google Translation Crash](https://github.com/mui/material-ui/issues/27853).
    ///     <br/>
    ///     @default null
    /// </summary>
    public static Modifier Loading(object value) => CreateThirdPartyReactComponentModifier<IconButton>(x => x.loading = value);
    
    /// <summary>
    ///     Element placed before the children if the button is in loading state.
    ///     <br/>
    ///     The node should contain an element with `role="progressbar"` with an accessible name.
    ///     <br/>
    ///     By default, it renders a `CircularProgress` that is labeled by the button itself.
    ///     <br/>
    ///     @default &lt;CircularProgress color="inherit" size={16} /&gt;
    /// </summary>
    [ReactProp]
    public Element loadingIndicator { get; set; }
    
    /// <summary>
    ///     Element placed before the children if the button is in loading state.
    ///     <br/>
    ///     The node should contain an element with `role="progressbar"` with an accessible name.
    ///     <br/>
    ///     By default, it renders a `CircularProgress` that is labeled by the button itself.
    ///     <br/>
    ///     @default &lt;CircularProgress color="inherit" size={16} /&gt;
    /// </summary>
    public static Modifier LoadingIndicator(Element value) => CreateThirdPartyReactComponentModifier<IconButton>(x => x.loadingIndicator = value);
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     `small` is equivalent to the dense button styling.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    [ReactProp]
    public string size { get; set; }
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     `small` is equivalent to the dense button styling.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    public static Modifier Size(string value) => CreateThirdPartyReactComponentModifier<IconButton>(x => x.size = value);
    
    [ReactProp]
    public string type { get; set; }
}
