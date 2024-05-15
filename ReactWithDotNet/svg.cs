namespace ReactWithDotNet;

partial class svg 
{
    /// <summary>
    ///     svg.width = <paramref name="width" /> + 'px'
    /// </summary>
    public static HtmlElementModifier Width(double width) => Width(width.AsPixel());

    /// <summary>
    ///     svg.height = <paramref name="height" /> + 'px'
    /// </summary>
    public static HtmlElementModifier Height(double height) => Height(height.AsPixel());
    
    /// <summary>
    ///     svg.width = <paramref name="width" /> + 'px'
    ///     <br/>
    ///     svg.height = <paramref name="height" /> + 'px'
    /// </summary>
    public static HtmlElementModifier Size(double width, double height) => Modify(x =>
    {
        x.width = width.AsPixel();
        x.height = height.AsPixel();
    });
    
    /// <summary>
    ///     svg.width = <paramref name="width" />
    ///     <br/>
    ///     svg.height = <paramref name="height" />
    /// </summary>
    public static HtmlElementModifier Size(string width, string height) => Modify(x =>
    {
        x.width  = width;
        x.height = height;
    });

    /// <summary>
    ///     svg.width = <paramref name="size" /> + 'px'
    ///     <br/>
    ///     svg.height = <paramref name="size" /> + 'px'
    /// </summary>
    public static HtmlElementModifier Size(double size) => Modify(x =>
    {
        x.width  = size.AsPixel();
        x.height = size.AsPixel();
    });
    
    /// <summary>
    ///     svg.viewBox = '<paramref name="minX" /> <paramref name="minY" /> <paramref name="width" /> <paramref name="height" />'
    /// </summary>
    public static HtmlElementModifier ViewBox(double  minX, double minY, double width, double height) => ViewBox($"{minX} {minY} {width} {height}");
    
    /// <summary>
    ///     svg.focusable = false
    /// </summary>
    public static HtmlElementModifier FocusableFalse => Focusable("false");
    
    /// <summary>
    ///     svg.focusable = true
    /// </summary>
    public static HtmlElementModifier FocusableTrue => Focusable("true");
    
    /// <summary>
    ///     svg.focusable = auto
    /// </summary>
    public static HtmlElementModifier FocusableAuto => Focusable("auto");

}

partial class Mixin
{
    /// <summary>
    ///     svg.viewBox = <paramref name="viewBox" />
    /// </summary>
    public static HtmlElementModifier ViewBox(string viewBox) 
        => svg.ViewBox(viewBox);
    
    /// <summary>
    ///     svg.viewBox = '<paramref name="minX" /> <paramref name="minY" /> <paramref name="width" /> <paramref name="height" />'
    /// </summary>
    public static HtmlElementModifier ViewBox(double  minX, double minY, double width, double height) 
        => svg.ViewBox(  minX,  minY,  width,  height);
}
