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






public sealed class defs : HtmlElement;


public sealed class linearGradient : HtmlElement;
public sealed class stop : HtmlElement
{
    [ReactProp]
    public string offset { get; set; }


    [ReactProp]
    public string stopColor { get; set; }


    [ReactProp]
    public string stopOpacity { get; set; }
    
}

public sealed class noscript : HtmlElement;



