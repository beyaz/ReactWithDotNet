// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public class ButtonBase : ElementBase
{
    /// <summary>
    ///     If `true`, the ripples are centered.
    ///     <br/>
    ///     They won't start at the cursor interaction position.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? centerRipple { get; set; }
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     If `true`, the component is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     If `true`, the ripple effect is disabled.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     ⚠️ Without a ripple there is no styling for :focus-visible by default. Be sure
    ///     <br/>
    ///     to highlight the element by applying separate styles with the `.Mui-focusVisible` class.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disableRipple { get; set; }
    
    /// <summary>
    ///     If `true`, the touch ripple effect is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disableTouchRipple { get; set; }
    
    /// <summary>
    ///     If `true`, the base button will have a keyboard focus ripple.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? focusRipple { get; set; }
    
    /// <summary>
    ///     This prop can help identify which element has keyboard focus.
    ///     <br/>
    ///     The class name will be applied when the element gains the focus through keyboard interaction.
    ///     <br/>
    ///     It's a polyfill for the [CSS :focus-visible selector](https://drafts.csswg.org/selectors-4/#the-focus-visible-pseudo).
    ///     <br/>
    ///     The rationale for using this feature [is explained here](https://github.com/WICG/focus-visible/blob/HEAD/explainer.md).
    ///     <br/>
    ///     A [polyfill can be used](https://github.com/WICG/focus-visible) to apply a `focus-visible` class to other components
    ///     <br/>
    ///     if needed.
    /// </summary>
    [ReactProp]
    public string focusVisibleClassName { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     Props applied to the `TouchRipple` element.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic TouchRippleProps { get; } = new ExpandoObject();
}
