﻿namespace ReactWithDotNet;

partial class Mixin
{
    public static StyleModifier BorderNone => Border("none");

   
    public static StyleModifier BorderInline(double widthAsPixel, string borderStyle, string color)
        => new(style => style.borderInline = $"{widthAsPixel.AsPixel()}  {borderStyle} {color}");

    public static StyleModifier Border(string top, string right, string bottom, string left)
        => new(style => style.border = $"{top} {right} {bottom} {left}");

    public static StyleModifier Border(double top, double right, double bottom, double left)
        => Border(top.AsPixel(), right.AsPixel(), bottom.AsPixel(), left.AsPixel());

    public static StyleModifier Border(double widthAsPixel, string borderStyle, string color)
        => new(style => style.border = $"{widthAsPixel.AsPixel()} {borderStyle} {color}");

    /// <summary>
    ///    style.border = <paramref name="widthAsPixel"/>+px <paramref name="borderStyle"/> <paramref name="color"/>
    ///    <br/>
    ///    style.borderRadius = <paramref name="borderRadiusAsPixel"/>+px
    /// </summary>
    public static StyleModifier Border(double widthAsPixel, string borderStyle, string color, double borderRadiusAsPixel)
        => new(style =>
        {
            style.border = $"{widthAsPixel.AsPixel()} {borderStyle} {color}";
            style.borderRadius = borderRadiusAsPixel.AsPixel();
        });

    public static StyleModifier Border(CssUnit width, string borderStyle, string color)
        => new(style => style.border = $"{width} {borderStyle} {color}");

    public static StyleModifier BorderLeft(string width, string borderStyle, string color)
        => BorderLeft($"{width} {borderStyle} {color}");
    
    public static StyleModifier BorderLeft(double width, string borderStyle, string color)
        => BorderLeft($"{width.AsString()}px {borderStyle} {color}");
    
    public static StyleModifier BorderRight(string width, string borderStyle, string color)
        => BorderRight($"{width} {borderStyle} {color}");
    
    public static StyleModifier BorderRight(double width, string borderStyle, string color)
        => BorderRight($"{width.AsString()}px {borderStyle} {color}");
   
    public static StyleModifier BorderTop(string width, string borderStyle, string color)
        => BorderTop($"{width} {borderStyle} {color}");
    
    public static StyleModifier BorderTop(double width, string borderStyle, string color)
        => BorderTop($"{width.AsString()}px {borderStyle} {color}");
    
    public static StyleModifier BorderBottom(string width, string borderStyle, string color)
        => BorderBottom($"{width} {borderStyle} {color}");
    
    public static StyleModifier BorderBottom(double width, string borderStyle, string color)
        => BorderBottom($"{width.AsString()}px {borderStyle} {color}");

    public static StyleModifier BorderRadius(double borderRadius)
        => BorderRadius(borderRadius.AsPixel());



    public static StyleModifier BorderTopLeftRadius(double borderRadius)
        => BorderTopLeftRadius(borderRadius.AsPixel());

  

    public static StyleModifier BorderBottomLeftRadius(double borderRadius)
        => BorderBottomLeftRadius(borderRadius.AsPixel());




    

    public static StyleModifier BorderTopRightRadius(double borderRadius) 
        => BorderTopRightRadius(borderRadius.AsPixel());

    public static StyleModifier BorderBottomRightRadius(double borderRadius)
        => BorderBottomRightRadius(borderRadius.AsPixel());



   

  
    public static StyleModifier BorderRadius(double borderTopLeftRadius, double borderTopRightRadius,double borderBottomRightRadius,double borderBottomLeftRadius)
        => BorderRadius($"{borderTopLeftRadius.AsPixel()} {borderTopRightRadius.AsPixel()} {borderBottomRightRadius.AsPixel()} {borderBottomLeftRadius.AsPixel()}");
  
    
    public static StyleModifier BorderRadius(double borderTopLeftRadius, double borderTopRightAndBottomLeftRadius,double borderBottomRightRadius)
        => BorderRadius($"{borderTopLeftRadius.AsPixel()} {borderTopRightAndBottomLeftRadius.AsPixel()} {borderBottomRightRadius.AsPixel()}");

    
    
 
    
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

    public static StyleModifier BoxShadow( string type,string color, double offsetX, double offsetY, double blurRadius, double spreadRadius)
        => BoxShadow($"{type} {color} {offsetX.AsPixel()} {offsetY.AsPixel()} {blurRadius.AsPixel()} {spreadRadius.AsPixel()}");

    
    public static StyleModifier BoxShadow( string type, double offsetX, double offsetY, double blurRadius, double spreadRadius, string color)
        => BoxShadow($"{type} {offsetX.AsPixel()} {offsetY.AsPixel()} {blurRadius.AsPixel()} {spreadRadius.AsPixel()} {color}");
    
    public static StyleModifier BoxShadow(double offsetX, double offsetY, double blurRadius, double spreadRadius, string color, string type)
        => BoxShadow($"{offsetX.AsPixel()} {offsetY.AsPixel()} {blurRadius.AsPixel()} {spreadRadius.AsPixel()} {color} {type}");

    public static StyleModifier BoxShadow(double offsetX, double offsetY, double blurRadius, double spreadRadius, string color)
        => BoxShadow($"{offsetX.AsPixel()} {offsetY.AsPixel()} {blurRadius.AsPixel()} {spreadRadius.AsPixel()} {color}");
    
    public static StyleModifier BoxShadow(double offsetX, double offsetY, double blurRadius, string color)
        => BoxShadow($"{offsetX.AsPixel()} {offsetY.AsPixel()} {blurRadius.AsPixel()} {color}");
    
    public static StyleModifier BoxShadow(string color, double offsetX, double offsetY, double blurRadius)
        => BoxShadow($"{color} {offsetX.AsPixel()} {offsetY.AsPixel()} {blurRadius.AsPixel()}");
    
    /// <summary>
    ///     style.boxShadow = <strong>none</strong>
    /// </summary>
    public static StyleModifier BoxShadowNone
        => BoxShadow("none");
    
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

    // todo: remove
    /// <summary>
    ///     Returns a string like <paramref name="widthAsPx" /><b> + px solid + </b> <paramref name="color" />"
    /// </summary>
    public static string Solid(double widthAsPx, string color)
        => BorderStyle("solid", widthAsPx, color);
    
    /// <summary>
    ///     style.borderWidth = <paramref name="widthAsPx" /> + 'px'
    /// </summary>
    public static StyleModifier BorderWidth(double widthAsPx) => BorderWidth(widthAsPx.AsPixel());
    
    public static StyleModifier BorderWidth(double topAsPx, double rightAsPx, double bottomAsPx, double leftAsPx) 
        => BorderWidth($"{topAsPx.AsPixel()} {rightAsPx.AsPixel()} {bottomAsPx.AsPixel()} {leftAsPx.AsPixel()}");
    
    public static StyleModifier BorderWidth(double topBottomAsPx, double rightLeftAsPx) 
        => BorderWidth($"{topBottomAsPx.AsPixel()} {rightLeftAsPx.AsPixel()}");

    /// <summary>
    /// "url(urlValue)
    /// </summary>
    public static string url(string urlValue) => $"url({urlValue})";
    
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

        return $"{widthAsPx.AsPixel()} {styleName} {color}";
    }
    
    
    
    
    
    /// <summary>
    ///     style.outline = none
    /// </summary>
    public static StyleModifier OutlineNone => Outline("none");
    
    
    
    
    
    /// <summary>
    ///     style.borderCollapse = collapse
    /// </summary>
    public static StyleModifier BorderCollapseCollapse => BorderCollapse("collapse");
    
    /// <summary>
    ///     style.borderCollapse = separate
    /// </summary>
    public static StyleModifier BorderCollapseSeparate => BorderCollapse("separate");
    
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
    
    
    
   
    
    
    
    
    

}