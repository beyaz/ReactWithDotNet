// auto generated code (do not edit manually)

namespace ReactWithDotNet.Libraries.mui.material;

public sealed class CardActions : ElementBase
{
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [React]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     If `true`, the actions do not have additional margin.
    ///     <br/>
    ///     @default false
    /// </summary>
    [React]
    public bool? disableSpacing { get; set; }
}
