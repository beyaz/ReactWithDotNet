namespace ReactWithDotNet;

public sealed class svg : HtmlElement
{
    public svg()
    {
    }

    public svg(params IModifier[] modifiers) : base(modifiers)
    {
    }
    [ReactProp]
    public string version { get; set; }
    [ReactProp]
    public string fill { get; set; }

    [ReactProp]
    public string height { get; set; }

    [ReactProp]
    public string preserveAspectRatio { get; set; }

    [ReactProp]
    public string viewBox { get; set; }

    [ReactProp]
    public string width { get; set; }

    [ReactProp]
    public string xmlns { get; set; } = "http://www.w3.org/2000/svg";
    
    [ReactProp]
    public string xlinkHref { get; set; }
    
    [ReactProp]
    public string xmlnsXlink { get; set; }

    /// <summary>
    ///     svg.width = <paramref name="width" />
    /// </summary>
    public static HtmlElementModifier Width(string width) => Modify<svg>(el => el.width = width);

    /// <summary>
    ///     svg.width = <paramref name="width" /> + 'px'
    /// </summary>
    public static HtmlElementModifier Width(double width) => Width(width.AsPixel());

    /// <summary>
    ///     svg.width = <paramref name="widthAndHeight" />
    /// <br/>
    ///     svg.height = <paramref name="widthAndHeight" />
    /// </summary>
    public static HtmlElementModifier WidthHeight(string widthAndHeight) => Modify<svg>(el =>
    {
        el.width  = widthAndHeight;
        el.height = widthAndHeight;
    });

    /// <summary>
    ///     svg.width = <paramref name="widthAndHeight" /> + 'px'
    /// <br/>
    ///     svg.height = <paramref name="widthAndHeight" /> + 'px'
    /// </summary>
    public static HtmlElementModifier WidthHeight(double widthAndHeight) => WidthHeight(widthAndHeight.AsPixel());

    /// <summary>
    ///     svg.height = <paramref name="height" />
    /// </summary>
    public static HtmlElementModifier Height(string height) => Modify<svg>(el => el.height = height);

    /// <summary>
    ///     svg.height = <paramref name="height" /> + 'px'
    /// </summary>
    public static HtmlElementModifier Height(double height) => Height(height.AsPixel());

    /// <summary>
    ///     svg.fill = <paramref name="color" />
    /// </summary>
    public static HtmlElementModifier Fill(string color) => Modify<svg>(el => el.fill = color);

    /// <summary>
    ///     svg.viewBox = <paramref name="viewBox" />
    /// </summary>
    public static HtmlElementModifier ViewBox(string viewBox) => Modify<svg>(el => el.viewBox = viewBox);

    /// <summary>
    ///     svg.viewBox = '<paramref name="minX" /> <paramref name="minY" /> <paramref name="width" /> <paramref name="height" />'
    /// </summary>
    public static HtmlElementModifier ViewBox(double  minX, double minY, double width, double height) => ViewBox($"{minX} {minY} {width} {height}");
}

public sealed class path : HtmlElement
{
    public path()
    {
    }

    public path(params IModifier[] modifiers) : base(modifiers)
    {
    }
    
    [ReactProp]
    public string d { get; set; }

    [ReactProp]
    public string fill { get; set; }

    [ReactProp]
    public string stroke { get; set; }
}

public sealed class g : HtmlElement
{
    public g()
    {
    }

    public g(params IModifier[] modifiers) : base(modifiers)
    {
    }
    
    [ReactProp]
    public string opacity { get; set; }

    [ReactProp]
    public string transform { get; set; }
}

public sealed class defs : HtmlElement;

public sealed class clipPath : HtmlElement;
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
public sealed class radialGradient : HtmlElement
{
    [ReactProp]
    public string cx { get; set; }
    [ReactProp] public string cy { get; set; }
    [ReactProp] public string r { get; set; }

    [ReactProp] public string fx { get; set; }
    [ReactProp] public string fy { get; set; }
    [ReactProp] public string xlinkHref { get; set; }
    [ReactProp] public string gradientUnits { get; set; }
    [ReactProp] public string gradientTransform { get; set; }
}

public sealed class rect : HtmlElement
{
    public rect()
    {
    }

    public rect(params IModifier[] modifiers) : base(modifiers)
    {
    }
    
    [ReactProp]
    public string stroke { get; set; }
   

    /// <summary>
    /// The horizontal corner radius of the rect
    /// <br/>
    /// Default value: auto
    /// </summary>
    [ReactProp]
    public string rx { get; set; }


    /// <summary>
    /// The vertical corner radius of the rect
    /// <br/>
    /// Default value: auto
    /// </summary>
    [ReactProp]
    public string ry { get; set; }

    [ReactProp]
    public string fill { get; set; }


    [ReactProp]
    public string height { get; set; }

    [ReactProp]
    public string width { get; set; }

    /// <summary>
    /// The x coordinate of the rect
    /// </summary>
    [ReactProp]
    public string x { get; set; }

    /// <summary>
    /// The y coordinate of the rect
    /// </summary>
    [ReactProp]
    public string y { get; set; }
}

public sealed class polygon : HtmlElement
{
    public polygon()
    {
    }

    public polygon(params IModifier[] modifiers) : base(modifiers)
    {
    }
    [ReactProp]
    public string fill { get; set; }

    [ReactProp]
    public string points { get; set; }

    [ReactProp]
    public string stroke { get; set; }

    [ReactProp]
    public string strokeWidth { get; set; }

    [ReactProp]
    public string transform { get; set; }
}


public sealed class circle : HtmlElement
{
    public circle()
    {
    }

    public circle(params IModifier[] modifiers) : base(modifiers)
    {
    }
    
    [ReactProp]
    public string cx{ get; set; }
    [ReactProp] public string cy{ get; set; }
    [ReactProp] public string r{ get; set; }
    [ReactProp] public string stroke{ get; set; }
    [ReactProp] public string fill{ get; set; }
}
public sealed class ellipse : HtmlElement
{
    public ellipse()
    {
    }

    public ellipse(params IModifier[] modifiers) : base(modifiers)
    {
    }
    [ReactProp]public string cx{ get; set; }
   [ReactProp]public string cy{ get; set; }
   [ReactProp]public string rx{ get; set; }
   [ReactProp]public string ry{ get; set; }
   [ReactProp]public string stroke{ get; set; }
    [ReactProp] public string fill{ get; set; }
}
public sealed class line : HtmlElement
{
    public line()
    {
    }

    public line(params IModifier[] modifiers) : base(modifiers)
    {
    }
    
    [ReactProp]  public string x1{ get; set; }
  [ReactProp]  public string x2{ get; set; }
  [ReactProp]  public string y1{ get; set; }
  [ReactProp]  public string y2{ get; set; }
    [ReactProp] public string stroke{ get; set; }
}
public sealed class polyline : HtmlElement
{
    public polyline()
    {
    }

    public polyline(params IModifier[] modifiers) : base(modifiers)
    {
    }
    
    [ReactProp]  public string points{ get; set; }
  [ReactProp]  public string fill{ get; set; }
    [ReactProp] public string stroke{ get; set; }
}

partial class Mixin
{
    /// <summary>
    ///     svg.fill = <paramref name="color" />
    /// </summary>
    public static HtmlElementModifier Fill(string color) => svg.Fill(color);
    
    /// <summary>
    ///     svg.viewBox = <paramref name="viewBox" />
    /// </summary>
    public static HtmlElementModifier ViewBox(string viewBox) => svg.ViewBox(viewBox);
}
