namespace ReactWithDotNet;

partial class Mixin
{
    /// <summary>
    ///     This is default. The image is resized to fill the given dimension. If necessary, the image will be stretched or
    ///     squished to fit
    /// </summary>
    public static StyleModifier ObjectFitFill => ObjectFit("fill");

    /// <summary>
    ///     The image keeps its aspect ratio, but is resized to fit within the given dimension
    /// </summary>
    public static StyleModifier ObjectFitContain => ObjectFit("contain");

    /// <summary>
    ///     The image keeps its aspect ratio and fills the given dimension. The image will be clipped to fit
    /// </summary>
    public static StyleModifier ObjectFitCover => ObjectFit("cover");

    /// <summary>
    ///     The image is not resized
    /// </summary>
    public static StyleModifier ObjectFitNone => ObjectFit("none");


    /// <summary>
    ///     the image is scaled down to the smallest version of none or contain
    /// </summary>
    public static StyleModifier ObjectFitScaleDown => ObjectFit("scale-down");

    /// <summary>
    ///     The CSS object-fit property is used to specify how an img or video should be resized to fit its container.
    ///     <br />
    ///     This property tells the content to fill the container in a variety of ways; such as "preserve that aspect ratio" or
    ///     "stretch up and take up as much space as possible".
    /// </summary>
    public static StyleModifier ObjectFit(string value)
    {
        return new StyleModifier(style => style.objectFit = value);
    }


    /// <summary>
    ///     style.backgroundSize = <paramref name="value" />
    /// </summary>
    public static StyleModifier BackgroundSize(string value) 
        => new(style => style.backgroundSize = value);


    /// <summary>
    ///     style.backgroundSize = cover
    /// </summary>
    public static StyleModifier BackgroundSizeCover => BackgroundSize("cover");

    /// <summary>
    ///     style.backgroundSize = contain
    /// </summary>
    public static StyleModifier BackgroundSizeContain => BackgroundSize("contain");
    
    /// <summary>
    ///     style.backgroundRepeat = <paramref name="value" />
    /// </summary>
    public static StyleModifier BackgroundRepeat(string value) 
        => new(style => style.backgroundRepeat = value);


    /// <summary>
    ///     style.backgroundRepeat = repeat
    /// <br/>
    /// The background image is repeated both vertically and horizontally.  The last image will be clipped if it does not fit. This is default
    /// </summary>
    public static StyleModifier BackgroundRepeatRepeat => BackgroundRepeat("repeat");

    
    /// <summary>
    ///     style.backgroundRepeat = repeat-x
    /// <br/>
    /// The background image is repeated only horizontally
    /// </summary>
    public static StyleModifier BackgroundRepeatRepeatX => BackgroundRepeat("repeat-x");
    
        
    /// <summary>
    ///     style.backgroundRepeat = repeat-y
    /// <br/>
    /// The background image is repeated only vertically
    /// </summary>
    public static StyleModifier BackgroundRepeatRepeatY => BackgroundRepeat("repeat-y");
    
    /// <summary>
    ///     style.backgroundRepeat = no-repeat
    /// <br/>
    /// The background-image is not repeated. The image will only be shown once
    /// </summary>
    public static StyleModifier BackgroundRepeatNoRepeat=> BackgroundRepeat("no-repeat");
    
    /// <summary>
    ///     style.backgroundRepeat = space
    /// <br/>
    /// The background-image is repeated as much as possible without clipping. The first and last image is pinned to either side of the element, and whitespace is distributed evenly between the images
    /// </summary>
    public static StyleModifier BackgroundRepeatSpace=> BackgroundRepeat("space");
    
    
    /// <summary>
    ///     style.backgroundRepeat = round
    /// <br/>
    /// The background-image is repeated and squished or stretched to fill the space (no gaps)
    /// </summary>
    public static StyleModifier BackgroundRepeatRound=> BackgroundRepeat("round");
    
    /// <summary>
    ///     style.backgroundRepeat = initial
    /// <br/>
    /// Sets this property to its default value.
    /// </summary>
    public static StyleModifier BackgroundRepeatInitial=> BackgroundRepeat("initial");
    
    /// <summary>
    ///     style.backgroundRepeat = inherit
    /// <br/>
    /// Inherits this property from its parent element.
    /// </summary>
    public static StyleModifier BackgroundRepeatInherit=> BackgroundRepeat("inherit");

}