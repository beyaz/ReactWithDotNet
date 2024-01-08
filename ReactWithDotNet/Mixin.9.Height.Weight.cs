namespace ReactWithDotNet;

partial class Mixin
{
    /// <summary>
    ///     style.backgroundRepeat = inherit
    ///     <br />
    ///     Inherits this property from its parent element.
    /// </summary>
    public static StyleModifier BackgroundRepeatInherit => BackgroundRepeat("inherit");

    /// <summary>
    ///     style.backgroundRepeat = initial
    ///     <br />
    ///     Sets this property to its default value.
    /// </summary>
    public static StyleModifier BackgroundRepeatInitial => BackgroundRepeat("initial");

    /// <summary>
    ///     style.backgroundRepeat = no-repeat
    ///     <br />
    ///     The background-image is not repeated. The image will only be shown once
    /// </summary>
    public static StyleModifier BackgroundRepeatNoRepeat => BackgroundRepeat("no-repeat");

    /// <summary>
    ///     style.backgroundRepeat = repeat
    ///     <br />
    ///     The background image is repeated both vertically and horizontally.  The last image will be clipped if it does not
    ///     fit. This is default
    /// </summary>
    public static StyleModifier BackgroundRepeatRepeat => BackgroundRepeat("repeat");

    /// <summary>
    ///     style.backgroundRepeat = repeat-x
    ///     <br />
    ///     The background image is repeated only horizontally
    /// </summary>
    public static StyleModifier BackgroundRepeatRepeatX => BackgroundRepeat("repeat-x");

    /// <summary>
    ///     style.backgroundRepeat = repeat-y
    ///     <br />
    ///     The background image is repeated only vertically
    /// </summary>
    public static StyleModifier BackgroundRepeatRepeatY => BackgroundRepeat("repeat-y");

    /// <summary>
    ///     style.backgroundRepeat = round
    ///     <br />
    ///     The background-image is repeated and squished or stretched to fill the space (no gaps)
    /// </summary>
    public static StyleModifier BackgroundRepeatRound => BackgroundRepeat("round");

    /// <summary>
    ///     style.backgroundRepeat = space
    ///     <br />
    ///     The background-image is repeated as much as possible without clipping. The first and last image is pinned to either
    ///     side of the element, and whitespace is distributed evenly between the images
    /// </summary>
    public static StyleModifier BackgroundRepeatSpace => BackgroundRepeat("space");

    /// <summary>
    ///     style.backgroundSize = contain
    /// </summary>
    public static StyleModifier BackgroundSizeContain => BackgroundSize("contain");

    /// <summary>
    ///     style.backgroundSize = cover
    /// </summary>
    public static StyleModifier BackgroundSizeCover => BackgroundSize("cover");

    /// <summary>
    ///     The image keeps its aspect ratio, but is resized to fit within the given dimension
    /// </summary>
    public static StyleModifier ObjectFitContain => ObjectFit("contain");

    /// <summary>
    ///     The image keeps its aspect ratio and fills the given dimension. The image will be clipped to fit
    /// </summary>
    public static StyleModifier ObjectFitCover => ObjectFit("cover");

    /// <summary>
    ///     This is default. The image is resized to fill the given dimension. If necessary, the image will be stretched or
    ///     squished to fit
    /// </summary>
    public static StyleModifier ObjectFitFill => ObjectFit("fill");

    /// <summary>
    ///     The image is not resized
    /// </summary>
    public static StyleModifier ObjectFitNone => ObjectFit("none");

    /// <summary>
    ///     the image is scaled down to the smallest version of none or contain
    /// </summary>
    public static StyleModifier ObjectFitScaleDown => ObjectFit("scale-down");

   

   
    
   
    
    public static StyleModifier WidthHeight(double valuePx) => new(style => style.width_height = valuePx.AsPixel());
    public static StyleModifier WidthHeight(string width_height) => new(style => style.width_height = width_height);
    public static StyleModifier Zindex(int zIndex) => new(style => style.zIndex = zIndex.ToString());


    public static StyleModifier Zindex0 => Zindex(0);
    public static StyleModifier Zindex1 => Zindex(1);
    public static StyleModifier Zindex2 => Zindex(2);
    public static StyleModifier Zindex3 => Zindex(3);
    public static StyleModifier Zindex4 => Zindex(4);
    public static StyleModifier Zindex5 => Zindex(5);
    public static StyleModifier Zindex6 => Zindex(6);
    public static StyleModifier Zindex7 => Zindex(7);
    public static StyleModifier Zindex8 => Zindex(8);
    public static StyleModifier Zindex9 => Zindex(9);
    
    /// <summary>
    ///     style.width = <paramref name="width" /> + 'px'
    ///     <br/>
    ///     style.height = <paramref name="height" /> + 'px'
    /// </summary>
    public static StyleModifier Size(double width, double height) => new(style =>
    {
        style.width  = width.AsPixel();
        style.height = height.AsPixel();
    });

    /// <summary>
    ///     style.width = <paramref name="width_and_height" /> + 'px'
    ///     <br/>
    ///     style.height = <paramref name="width_and_height" /> + 'px'
    /// </summary>
    public static StyleModifier Size(double width_and_height) => new(style =>
    {
        style.width  = width_and_height.AsPixel();
        style.height = width_and_height.AsPixel();
    });

    /// <summary>
    ///     width: 100%
    /// </summary>
    public static StyleModifier w_full = WidthMaximized;


    /// <summary>
    ///     width: <paramref name="percentOfTotal"/> / <paramref name="total"/> * 100 + '%'
    ///     <br/>Example:<br/>
    ///     Width(3, 4) means Width("75%")
    /// </summary>
    public static StyleModifier Width(double percentOfTotal, double total) => Width( percentOfTotal / total * 100 + "%");
    
    /// <summary>
    ///     height:  <paramref name="percentOfTotal"/> / <paramref name="total"/> * 100 + '%'
    ///     <br/>Example:<br/>
    ///     Height(3, 4) means Height("75%")
    /// </summary>
    public static StyleModifier Height(double percentOfTotal, double total) => Height(total / percentOfTotal * 100 + "%");
    
    
    /// <summary>
    ///     height: 100%
    /// </summary>
    public static StyleModifier h_full = HeightMaximized;
    
    /// <summary>
    ///     width: 100%
    ///     <br />
    ///     height: 100%
    /// </summary>
    public static StyleModifier size_full = WidthHeightMaximized;
    
    /// <summary>
    ///     width: 100%
    ///     <br />
    ///     height: 100%
    /// </summary>
    public static StyleModifier SizeFull => WidthHeightMaximized;
    
    /// <summary>
    ///     height: 100%
    /// </summary>
    public static StyleModifier HeightFull => HeightMaximized;
    
    /// <summary>
    ///     width: 100%
    /// </summary>
    public static StyleModifier WidthFull => WidthMaximized;


    /// <summary>
    ///     inset: "0"
    /// </summary>
    public static StyleModifier Inset0 => Inset("0");




}