// auto generated code (do not edit manually)

namespace ReactWithDotNet.Libraries.mui.material;

public sealed class Divider : ElementBase
{
    /// <summary>
    ///     Absolutely position the element.
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public bool? absolute { get; set; }
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [React]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     If `true`, a vertical divider will have the correct height when used in flex container.
    ///     <br/>
    ///     (By default, a vertical divider will have a calculated height of `0px` if it is the child of a flex container.)
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public bool? flexItem { get; set; }
    
    /// <summary>
    ///     If `true`, the divider will have a lighter color.
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public bool? light { get; set; }
    
    /// <summary>
    ///     The component orientation.
    ///     <br/>
    ///     @default 'horizontal'
    /// </summary>
    [React]
    public string orientation { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The text alignment.
    ///     <br/>
    ///     @default 'center'
    /// </summary>
    [React]
    public string textAlign { get; set; }
    
    /// <summary>
    ///     The variant to use.
    ///     <br/>
    ///     @default 'fullWidth'
    /// </summary>
    [React]
    public string variant { get; set; }
}