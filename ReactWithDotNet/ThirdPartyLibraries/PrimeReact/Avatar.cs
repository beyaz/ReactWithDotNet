// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public sealed class Avatar : ElementBase
{
    /// <summary>
    ///     Defines the icon to display.
    /// </summary>
    [ReactProp]
    public string icon { get; set; }
    
    /// <summary>
    ///     Defines the image to display.
    /// </summary>
    [ReactProp]
    public string image { get; set; }
    
    /// <summary>
    ///     It specifies an alternate text for an image, if the image cannot be displayed.
    ///     <br/>
    ///     @defaultValue avatar
    /// </summary>
    [ReactProp]
    public string imageAlt { get; set; }
    
    /// <summary>
    ///     Defines a fallback image or URL if the main image fails to load. If "default" will fallback to label then icon.
    ///     <br/>
    ///     @defaultValue default
    /// </summary>
    [ReactProp]
    public object imageFallback { get; set; }
    
    /// <summary>
    ///     Defines the text to display.
    /// </summary>
    [ReactProp]
    public string label { get; set; }
    
    /// <summary>
    ///     Shape of the element.
    ///     <br/>
    ///     @defaultValue square
    /// </summary>
    [ReactProp]
    public string shape { get; set; }
    
    /// <summary>
    ///     Size of the element.
    ///     <br/>
    ///     @defaultValue normal
    /// </summary>
    [ReactProp]
    public string size { get; set; }
    
    /// <summary>
    ///     Template of the content.
    /// </summary>
    [ReactProp]
    public Element template { get; set; }
    
    /// <summary>
    ///     Used to configure passthrough(pt) options of the component.
    ///     <br/>
    ///     @type {PassThroughOptions}
    /// </summary>
    [ReactProp]
    public object ptOptions { get; set; }
    
    /// <summary>
    ///     When enabled, it removes component related styles in the core.
    ///     <br/>
    ///     @defaultValue false
    /// </summary>
    [ReactProp]
    public bool? unstyled { get; set; }
    
}
