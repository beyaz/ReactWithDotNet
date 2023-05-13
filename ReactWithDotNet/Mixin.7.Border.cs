namespace ReactWithDotNet;

partial class Mixin
{
    public static StyleModifier BorderNone => Border("none");

    public static StyleModifier Border(string border)
        => new(style => style.border = border);

    public static StyleModifier Border(string top, string right, string bottom, string left)
        => new(style => style.border = $"{top} {right} {bottom} {left}");

    public static StyleModifier Border(double top, double right, double bottom, double left)
        => Border(top.AsPixel(), right.AsPixel(), bottom.AsPixel(), left.AsPixel());

    public static StyleModifier BorderBottom(string borderBottom)
        => new(style => style.borderBottom = borderBottom);

    /// <summary>
    ///     style.borderColor = <paramref name="color" />
    /// </summary>
    public static StyleModifier BorderColor(string color)
        => new(style => style.borderColor = color);

    public static StyleModifier BorderLeft(string borderLeft)
        => new(style => style.borderLeft = borderLeft);

    public static StyleModifier BorderRadius(string borderRadius)
        => new(style => style.borderRadius = borderRadius);

    public static StyleModifier BorderRadius(double borderRadius)
        => BorderRadius(borderRadius.AsPixel());


    public static StyleModifier BorderTopLeftRadius(string borderRadius)
        => new(style => style.borderTopLeftRadius = borderRadius);

    public static StyleModifier BorderTopLeftRadius(double borderRadius)
        => BorderTopLeftRadius(borderRadius.AsPixel());

    public static StyleModifier BorderBottomLeftRadius(string borderRadius)
        => new(style => style.borderBottomLeftRadius = borderRadius);

    public static StyleModifier BorderBottomLeftRadius(double borderRadius)
        => BorderBottomLeftRadius(borderRadius.AsPixel());


    public static StyleModifier BorderTopRightRadius(string borderRadius)
        => new(style => style.borderTopRightRadius = borderRadius);

    public static StyleModifier BorderBottomRightRadius(string borderRadius)
        => new(style => style.borderBottomRightRadius = borderRadius);

    public static StyleModifier BorderTopRightRadius(double borderRadius) 
        => BorderTopRightRadius(borderRadius.AsPixel());

    public static StyleModifier BorderBottomRightRadius(double borderRadius)
        => BorderBottomRightRadius(borderRadius.AsPixel());



    public static StyleModifier BorderRight(string borderRight)
        => new(style => style.borderRight = borderRight);

    public static StyleModifier BorderTop(string borderTop)
        => new(style => style.borderTop = borderTop);

    public static StyleModifier BoxShadow(string boxShadow)
        => new(style => style.boxShadow = boxShadow);

    public static StyleModifier BoxShadow(double offsetX, double offsetY, double blurRadius, double spreadRadius, string color)
        => BoxShadow($@"{offsetX}px {offsetY}px {blurRadius}px {spreadRadius}px {color}");
    
    /// <summary>
    ///     Returns a string like <paramref name="widthAsPx" /><b> + px dashed + </b> <paramref name="color" />"
    /// </summary>
    public static string Dashed(double widthAsPx, string color)
        => BorderStyle("dashed", widthAsPx, color);

    /// <summary>
    ///     Returns a string like <paramref name="widthAsPx" /><b> + px dotted + </b> <paramref name="color" />"
    /// </summary>
    public static string Dotted(double widthAsPx, string color)
        => BorderStyle("dotted", widthAsPx, color);

    /// <summary>
    ///     Returns a string like <paramref name="widthAsPx" /><b> + px solid + </b> <paramref name="color" />"
    /// </summary>
    public static string Solid(double widthAsPx, string color)
        => BorderStyle("solid", widthAsPx, color);

    static string BorderStyle(string styleName, double widthAsPx, string color)
    {
        if (styleName == null)
        {
            throw new ArgumentNullException(nameof(styleName));
        }

        if (color == null)
        {
            throw new ArgumentNullException(nameof(color));
        }

        return $"{widthAsPx}px {styleName} {color}";
    }
}