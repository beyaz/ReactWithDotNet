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
}