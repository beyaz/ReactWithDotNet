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
    
    public static StyleModifier BorderRightWidth(string borderRightWidth)
        => new(style => style.borderRightWidth = borderRightWidth);
    
    public static StyleModifier BorderLeftWidth(string borderLeftWidth)
        => new(style => style.borderLeftWidth = borderLeftWidth);
    
    public static StyleModifier BorderTopWidth(string borderTopWidth)
        => new(style => style.borderTopWidth = borderTopWidth);
    
    public static StyleModifier BorderBottomWidth(string borderBottomWidth)
        => new(style => style.borderBottomWidth = borderBottomWidth);
    
    /// <summary>
    /// specifies the indentation of the first line in a text-block
    /// <br/>
    /// style.textIndent = <paramref name="value"/>
    /// </summary>
    public static StyleModifier TextIndent(string value)
        => new(style => style.textIndent = value);
    
    /// <summary>
    /// specifies the indentation of the first line in a text-block
    /// <br/>
    /// style.textIndent = <paramref name="valueInPixel"/> + 'px'
    /// </summary>
    public static StyleModifier TextIndent(double valueInPixel)
        => TextIndent(valueInPixel.AsPixel());
        

    /// <summary>
    ///     style.borderTop = <paramref name="borderValue"/>
    /// <br/>
    ///     style.borderRight = <paramref name="borderValue"/>
    /// </summary>
    public static StyleModifier BorderTopRight(string borderValue) => new(style =>
    {
        style.borderTop   = borderValue;
        style.borderRight = borderValue;
    });

    /// <summary>
    ///     style.borderLeft = <paramref name="borderValue"/>
    /// <br/>
    ///     style.borderTop = <paramref name="borderValue"/>
    /// </summary>
    public static StyleModifier BorderLeftTop(string borderValue) => new(style =>
    {
        style.borderLeft   = borderValue;
        style.borderTop = borderValue;
    });

    /// <summary>
    ///     style.borderRight = <paramref name="borderValue"/>
    /// <br/>
    ///     style.borderBottom = <paramref name="borderValue"/>
    /// </summary>
    public static StyleModifier BorderRightBottom(string borderValue) => new(style =>
    {
        style.borderRight = borderValue;
        style.borderBottom  = borderValue;
    });

    /// <summary>
    ///     style.borderLeft = <paramref name="borderValue"/>
    /// <br/>
    ///     style.borderBottom = <paramref name="borderValue"/>
    /// </summary>
    public static StyleModifier BorderLeftBottom(string borderValue) => new(style =>
    {
        style.borderLeft = borderValue;
        style.borderBottom  = borderValue;
    });

    /// <summary>
    ///     style.borderTop = <paramref name="borderValue"/>
    /// <br/>
    ///     style.borderBottom = <paramref name="borderValue"/>
    /// </summary>
    public static StyleModifier BorderTopBottom(string borderValue) => new(style =>
    {
        style.borderTop   = borderValue;
        style.borderBottom = borderValue;
    });

    /// <summary>
    ///     style.borderLeft = <paramref name="borderValue"/>
    /// <br/>
    ///     style.borderRight = <paramref name="borderValue"/>
    /// </summary>
    public static StyleModifier BorderLeftRight(string borderValue) => new(style =>
    {
        style.borderLeft   = borderValue;
        style.borderRight = borderValue;
    });

    public static StyleModifier BoxShadow(string boxShadow)
        => new(style => style.boxShadow = boxShadow);

    public static StyleModifier BoxShadow(double offsetX, double offsetY, double blurRadius, double spreadRadius, string color)
        => BoxShadow($"{offsetX}px {offsetY}px {blurRadius}px {spreadRadius}px {color}");
    
    public static StyleModifier BoxShadow(double offsetX, double offsetY, double blurRadius, string color)
        => BoxShadow($"{offsetX}px {offsetY}px {blurRadius}px {color}");
    
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

    /// <summary>
    /// "url(urlValue)
    /// </summary>
    public static string url(string urlValue) => $"url({url})";
    
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
    
    
    
    /// <summary>
    ///     style.outline = <paramref name="value" />
    /// </summary>
    public static StyleModifier Outline(string value)
        => new(style => style.outline = value);
    
    /// <summary>
    ///     style.outline = none
    /// </summary>
    public static StyleModifier OutlineNone => Outline("none");
    
    
    
    /// <summary>
    /// Sets whether table borders should collapse into a single border or be separated as in standard HTML.
    /// <br/>
    /// style.borderCollapse = <paramref name="value"/>
    /// </summary>
    public static StyleModifier BorderCollapse(string value)
        => new(style => style.borderCollapse = value);
    
    /// <summary>
    ///     style.borderCollapse = collapse
    /// </summary>
    public static StyleModifier BorderCollapseCollapse => BorderCollapse("collapse");
    
    /// <summary>
    ///     style.borderCollapse = separate
    /// </summary>
    public static StyleModifier BorderCollapseSeparate => BorderCollapse("separate");

    /// <summary>
    ///     table.cellSpacing = <paramref name="value" />
    /// </summary>
    public static HtmlElementModifier CellSpacing(double? value) => table.CellSpacing(value);
    
    /// <summary>
    ///     table.cellPadding = <paramref name="value" />
    /// </summary>
    public static HtmlElementModifier CellPadding(double? value) =>
        CreateHtmlElementModifier<table>(element => element.cellPadding = value);

    /// <summary>
    ///     (tr-td).colSpan = <paramref name="value" />
    /// </summary>
    public static HtmlElementModifier ColSpan(int value) => CreateHtmlElementModifier<HtmlElement>(el =>
    {
        if (el is td tdElement)
        {
            tdElement.colSpan = value;
        }
        else if (el is tr trElement)
        {
            trElement.colSpan = value;
        }
        else
        {
            throw new DeveloperException($"ColSpan modifier is invalid for this element. Element is: {el.GetType().FullName}");
        }
    });
    
    /// <summary>
    ///     (tr-td).rowSpan = <paramref name="value" />
    /// </summary>
    public static HtmlElementModifier RowSpan(int value) => CreateHtmlElementModifier<HtmlElement>(el =>
    {
        if (el is td tdElement)
        {
            tdElement.rowSpan = value;
        }
        else if (el is tr trElement)
        {
            trElement.rowSpan = value;
        }
        else
        {
            throw new DeveloperException($"RowSpan modifier is invalid for this element. Element is: {el.GetType().FullName}");
        }
    });
    
    
    
    public static StyleModifier BorderRightStyle(string value) => new(style => style.borderRightStyle = value);
    public static StyleModifier BorderTopStyle(string value) => new(style => style.borderTopStyle = value);
    public static StyleModifier BorderBottomStyle(string value) => new(style => style.borderBottomStyle = value);
    public static StyleModifier BorderLeftStyle(string value) => new(style => style.borderLeftStyle = value);

}