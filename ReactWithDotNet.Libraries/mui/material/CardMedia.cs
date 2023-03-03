// auto generated code (do not edit manually)

namespace ReactWithDotNet.Libraries.mui.material;

public sealed class CardMedia : ElementBase
{
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic classes { get; } = new ExpandoObject();
    
    /// <summary>
    ///     Image to be displayed as a background image.
    ///     <br/>
    ///     Either `image` or `src` prop must be specified.
    ///     <br/>
    ///     Note that caller must specify height otherwise the image will not be visible.
    /// </summary>
    [React]
    public string image { get; set; }
    
    /// <summary>
    ///     An alias for `image` property.
    ///     <br/>
    ///     Available only with media components.
    ///     <br/>
    ///     Media components: `video`, `audio`, `picture`, `iframe`, `img`.
    /// </summary>
    [React]
    public string src { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
    
    [React]
    public string title { get; set; }
}
