// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class CardMedia : ElementBase
{
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     Image to be displayed as a background image.
    ///     <br/>
    ///     Either `image` or `src` prop must be specified.
    ///     <br/>
    ///     Note that caller must specify height otherwise the image will not be visible.
    /// </summary>
    [ReactProp]
    public string image { get; set; }
    
    /// <summary>
    ///     An alias for `image` property.
    ///     <br/>
    ///     Available only with media components.
    ///     <br/>
    ///     Media components: `video`, `audio`, `picture`, `iframe`, `img`.
    /// </summary>
    [ReactProp]
    public string src { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
    
    [ReactProp]
    public string title { get; set; }
}
