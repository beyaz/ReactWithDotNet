using System.Drawing;
using System.Text.Json;

namespace ReactWithDotNet;

public class StyleModifier
{
    Action<Style> _action;

    public StyleModifier(Action<Style> action)
    {
        _action = action;
    }

    public static implicit operator StyleModifier(Action<Style> action)
    {
        return new StyleModifier(action);
    }
}

public delegate void HtmlElementModifier(HtmlElement htmlElement);

public static class Mixin
{
    public static HtmlElementModifier OverflowWrapBreakWord => element => element.style.overflowWrap = "break-word";
    
    public static HtmlElementModifier BoxShadow(string boxShadow) => element => element.style.boxShadow = boxShadow;

    public static HtmlElementModifier Left(string left) => element => element.style.left = left;
    public static HtmlElementModifier Right(string right) => element => element.style.right = right;
    public static HtmlElementModifier LeftRight(string valueForLeftAndRight) => element =>
    {
        element.style.left = valueForLeftAndRight;
        element.style.right = valueForLeftAndRight;
    };
    public static HtmlElementModifier LeftRightBottom(string valueForLeftAndRightAndBottom) => element =>
    {
        element.style.left  = valueForLeftAndRightAndBottom;
        element.style.right = valueForLeftAndRightAndBottom;
        element.style.bottom = valueForLeftAndRightAndBottom;
    };
    
    public static HtmlElementModifier Top(string top) => element => element.style.top = top;
    public static HtmlElementModifier Bottom(string bottom) => element => element.style.bottom = bottom;
    public static HtmlElementModifier TopBottom(string valueForTopAndBottom) => element =>
    {
        element.style.top = valueForTopAndBottom;
        element.style.bottom = valueForTopAndBottom;
    };


    public static HtmlElementModifier WidthAsPercentOf(double valueAsPercent) => element => element.style.width = valueAsPercent + "%";

    public static HtmlElementModifier Width25Percent => WidthAsPercentOf(25);
    public static HtmlElementModifier Width50Percent => WidthAsPercentOf(50);
    public static HtmlElementModifier Width75Percent => WidthAsPercentOf(75);

    /// <summary>
    ///    style.width = "100%"
    /// </summary>
    public static HtmlElementModifier Width100Percent => WidthAsPercentOf(100);


    /// <summary>
    /// height = valueAsPercent + "%"
    /// </summary>
    public static HtmlElementModifier HeightAsPercentOf(double valueAsPercent) => element => element.style.height = valueAsPercent + "%";

    public static HtmlElementModifier Height25Percent => HeightAsPercentOf(25);
    public static HtmlElementModifier Height50Percent => HeightAsPercentOf(50);
    public static HtmlElementModifier Height75Percent => HeightAsPercentOf(75);
    /// <summary>
    ///    style.height = "100%"
    /// </summary>
    public static HtmlElementModifier Height100Percent => HeightAsPercentOf(100);

    
    /// <summary>
    /// width = '100%' , height = '100%'
    /// </summary>
    public static HtmlElementModifier StretchWidthHeight => element => element.style.width_height = "100%";

    /// <summary>
    /// overflow = "hidden"
    /// </summary>
    public static HtmlElementModifier OverflowHidden => element => element.style.overflow = "hidden"; 



    public static HtmlElementModifier Hover(params HtmlElementModifier[] modifiers)
    {
        return element =>
        {
            var temp = element.style;

            element.style = element.style.hover;
            
            element.Apply(modifiers);

            element.style = temp;
        };
    }
    public static HtmlElementModifier WidthHeight(double valuePx) => element => element.style.width_height = valuePx.AsPixel();
    public static HtmlElementModifier WidthHeight(string width_height) => element => element.style.width_height = width_height;
    public static HtmlElementModifier Background(string background) => element => element.style.background = background;
    
    public static HtmlElementModifier DisplayNone => element => element.style.display = "none";
    public static HtmlElementModifier DisplayNull => element => element.style.display = null;

    /// <summary>
    /// textDecoration = "underline"
    /// </summary>
    public static HtmlElementModifier TextDecorationUnderline => TextDecoration("underline");
    /// <summary>
    /// textDecoration = 'line-through'
    /// </summary>
    public static HtmlElementModifier TextDecorationLineThrough=> TextDecoration("line-through");
    /// <summary>
    /// textDecoration = "overline"
    /// </summary>
    public static HtmlElementModifier TextDecorationOverline => TextDecoration("overline");

    public static HtmlElementModifier TextDecorationNone => TextDecoration("none");
    
    public static HtmlElementModifier TextDecoration(string textDecoration) => element => element.style.textDecoration = textDecoration;
    
    
    public static HtmlElementModifier BackgroundColor(string backgroundColor) => element => element.style.backgroundColor = backgroundColor;

    public static HtmlElementModifier BorderRadius(string borderRadius) => element => element.style.borderRadius = borderRadius;
    public static HtmlElementModifier BorderRadius(double borderRadius) => BorderRadius(borderRadius.AsPixel());

    public static HtmlElementModifier TextAlignCenter => TextAlign("center");
    public static HtmlElementModifier TextAlignRight=> TextAlign("right");
    public static HtmlElementModifier TextAlignLeft => TextAlign("left");
    public static HtmlElementModifier TextAlign(string textAlign) => element => element.style.textAlign = textAlign;

    public static HtmlElementModifier Width(double width) => element => element.style.width = width.AsPixel();
    public static HtmlElementModifier Width(string width) => element => element.style.width = width;

    public static HtmlElementModifier Height(double height) => Height(height.AsPixel());
    public static HtmlElementModifier Height(string height) => element => element.style.height = height;
    public static HtmlElementModifier Height100vh => element => element.style.height = "100vh";

    public static HtmlElementModifier MaxHeight(string height) => element => element.style.maxHeight = height;
    public static HtmlElementModifier MaxHeight(double height) => MaxHeight(height.AsPixel());



    public static HtmlElementModifier Gap(double gap) => element => element.style.gap = gap.AsPixel();
    public static HtmlElementModifier Gap(string gap) => element => element.style.gap = gap;


    public static HtmlElementModifier Transition(string transition) => element => element.style.transition = transition;


    #region Position
    public static HtmlElementModifier PositionRelative => element => element.style.position = "relative";
    public static HtmlElementModifier PositionFixed => element => element.style.position = "fixed";
    public static HtmlElementModifier PositionAbsolute => element => element.style.position = "absolute";
    public static HtmlElementModifier PositionSticky => element => element.style.position = "sticky";
    public static HtmlElementModifier PositionStatic => element => element.style.position = "static";
    #endregion

    public static HtmlElementModifier DisplayFlex => element => element.style.display = "flex";
    /// <summary>
    /// flexDirection = "row"
    /// </summary>
    public static HtmlElementModifier FlexDirectionRow => element => element.style.flexDirection = "row";

    /// <summary>
    /// flexDirection = "row-reverse"
    /// </summary>
    public static HtmlElementModifier FlexDirectionRowReverse => element => element.style.flexDirection = "row-reverse";
    
    public static HtmlElementModifier FlexDirectionColumn=> element => element.style.flexDirection = "column";
    public static HtmlElementModifier JustifyContentSpaceBetween => element => element.style.justifyContent = "space-between";

    /// <summary>
    ///    justifyContent = "center"
    /// </summary>
    public static HtmlElementModifier JustifyContentCenter => element => element.style.justifyContent = "center";

    public static HtmlElementModifier AlignItemsCenter=> element => element.style.alignItems = "center";
    public static HtmlElementModifier AlignItemsFlexStart => element => element.style.alignItems = "flex-start";
    public static HtmlElementModifier AlignItemsFlexEnd => element => element.style.alignItems = "flex-end";
    public static HtmlElementModifier AlignItemsStretch => element => element.style.alignItems = "stretch";
    public static HtmlElementModifier AlignItemsBaseline=> element => element.style.alignItems = "baseline";

    public static HtmlElementModifier FontSize(string fontSize) => element => element.style.fontSize = fontSize;
    public static HtmlElementModifier FontSize(double fontSizePx) => FontSize(fontSizePx.AsPixel());

    public static HtmlElementModifier FontSize9 => FontSize(9);
    public static HtmlElementModifier FontSize10 => FontSize(10);
    public static HtmlElementModifier FontSize11 => FontSize(11);
    public static HtmlElementModifier FontSize12 => FontSize(12);
    public static HtmlElementModifier FontSize13 => FontSize(13);
    public static HtmlElementModifier FontSize14 => FontSize(14);
    public static HtmlElementModifier FontSize15 => FontSize(15);
    public static HtmlElementModifier FontSize16 => FontSize(16);
    public static HtmlElementModifier FontSize17 => FontSize(17);
    public static HtmlElementModifier FontSize18 => FontSize(18);
    public static HtmlElementModifier FontSize19 => FontSize(19);
    public static HtmlElementModifier FontSize20 => FontSize(20);
    public static HtmlElementModifier FontSize21 => FontSize(21);
    public static HtmlElementModifier FontSize22 => FontSize(22);
    public static HtmlElementModifier FontSize23 => FontSize(23);
    public static HtmlElementModifier FontSize24 => FontSize(24);
    public static HtmlElementModifier FontSize25 => FontSize(25);

    public static HtmlElementModifier Color(string color) => element => element.style.color = color;

    public static HtmlElementModifier MaxWidth(string maxWidth) => element => element.style.maxWidth = maxWidth;
    public static HtmlElementModifier MaxWidth(double maxWidth) => element => element.style.maxWidth = maxWidth.AsPixel();
    public static HtmlElementModifier WidthAuto => element => element.style.width = "auto";

    
    
    public static HtmlElementModifier Border(string border) => element => element.style.border = border;
    public static HtmlElementModifier ClassName(string className) => element => element.className = className;
    public static HtmlElementModifier FontWeight400  => element=> element.style.fontWeight = "400";
    public static HtmlElementModifier FontWeight500 => element => element.style.fontWeight = "500";
    public static HtmlElementModifier FontWeight600 => element => element.style.fontWeight = "600";
    public static HtmlElementModifier FontWeight700 => element => element.style.fontWeight = "700";
    public static HtmlElementModifier FontWeight800 => element => element.style.fontWeight = "800";
    
    public static HtmlElementModifier BoxSizingBorderBox => element => element.style.boxSizing = "border-box";
    public static HtmlElementModifier Zindex(int zIndex) => element => element.style.zIndex = zIndex.ToString();
    public static HtmlElementModifier Text(string innerText) => element => element.text = innerText;
    
    
    #region Margin
    public static HtmlElementModifier MarginRight(string marginRight) => element => element.style.marginRight = marginRight;
    public static HtmlElementModifier MarginLeft(string marginLeft) => element => element.style.marginLeft = marginLeft;
    public static HtmlElementModifier MarginTop(string marginTop) => element => element.style.marginTop = marginTop;
    public static HtmlElementModifier MarginBottom(string marginBottom) => element => element.style.marginBottom = marginBottom;

    public static HtmlElementModifier MarginLeft(double marginLeftAsPx) => element => element.style.marginLeft = marginLeftAsPx.AsPixel();
    public static HtmlElementModifier MarginRight(double marginRightAsPx) => element => element.style.marginRight = marginRightAsPx.AsPixel();
    public static HtmlElementModifier MarginTop(double marginTopAsPx) => element => element.style.marginTop = marginTopAsPx.AsPixel();
    public static HtmlElementModifier MarginBottom(double marginBottomAsPx) => element => element.style.marginBottom = marginBottomAsPx.AsPixel();

    public static HtmlElementModifier MarginLeftRight(string marginLeftRight) => element => element.style.marginLeftRight = marginLeftRight;
    public static HtmlElementModifier MarginTopBottom(string marginTopBottom) => element => element.style.marginTopBottom = marginTopBottom;
    public static HtmlElementModifier MarginLeftTop(string marginLeftTop) => element => element.style.marginLeftTop = marginLeftTop;
    public static HtmlElementModifier MarginLeftBottom(string marginLeftBottom) => element => element.style.marginLeftBottom = marginLeftBottom;
    public static HtmlElementModifier MarginTopRight(string marginTopRight) => element => element.style.marginTopRight = marginTopRight;

    public static HtmlElementModifier MarginLeftRight(double marginLeftRight) => element => element.style.marginLeftRight = marginLeftRight.AsPixel();
    public static HtmlElementModifier MarginTopBottom(double marginTopBottom) => element => element.style.marginTopBottom = marginTopBottom.AsPixel();
    public static HtmlElementModifier MarginLeftTop(double marginLeftTop) => element => element.style.marginLeftTop = marginLeftTop.AsPixel();
    public static HtmlElementModifier MarginLeftBottom(double marginLeftBottom) => element => element.style.marginLeftBottom = marginLeftBottom.AsPixel();
    public static HtmlElementModifier MarginTopRight(double marginTopRight) => element => element.style.marginTopRight = marginTopRight.AsPixel();

    #endregion




    #region Padding
    public static HtmlElementModifier Padding(double paddingPx) => element => element.style.padding = paddingPx.AsPixel();
    public static HtmlElementModifier Padding(string padding) => element => element.style.padding = padding;
    public static HtmlElementModifier Padding(double topBottomPx, double rightLeftPx) => Padding($"{topBottomPx}px {rightLeftPx}px");
    public static HtmlElementModifier PaddingRight(string paddingRight) => element => element.style.paddingRight = paddingRight;
    public static HtmlElementModifier PaddingLeft(string paddingLeft) => element => element.style.paddingLeft = paddingLeft;
    public static HtmlElementModifier PaddingTop(string paddingTop) => element => element.style.paddingTop = paddingTop;
    public static HtmlElementModifier PaddingBottom(string paddingBottom) => element => element.style.paddingBottom = paddingBottom;

    public static HtmlElementModifier PaddingLeft(double paddingLeftAsPx) => element => element.style.paddingLeft = paddingLeftAsPx.AsPixel();
    public static HtmlElementModifier PaddingRight(double paddingRightAsPx) => element => element.style.paddingRight = paddingRightAsPx.AsPixel();
    public static HtmlElementModifier PaddingTop(double paddingTopAsPx) => element => element.style.paddingTop = paddingTopAsPx.AsPixel();
    public static HtmlElementModifier PaddingBottom(double paddingBottomAsPx) => element => element.style.paddingBottom = paddingBottomAsPx.AsPixel();

    public static HtmlElementModifier PaddingLeftRight(string paddingLeftRight) => element => element.style.paddingLeftRight = paddingLeftRight;
    public static HtmlElementModifier PaddingTopBottom(string paddingTopBottom) => element => element.style.paddingTopBottom = paddingTopBottom;
    public static HtmlElementModifier PaddingLeftTop(string paddingLeftTop) => element => element.style.paddingLeftTop = paddingLeftTop;
    public static HtmlElementModifier PaddingLeftBottom(string paddingLeftBottom) => element => element.style.paddingLeftBottom = paddingLeftBottom;
    public static HtmlElementModifier PaddingTopRight(string paddingTopRight) => element => element.style.paddingTopRight = paddingTopRight;

    public static HtmlElementModifier PaddingLeftRight(double paddingLeftRight) => element => element.style.paddingLeftRight = paddingLeftRight.AsPixel();
    public static HtmlElementModifier PaddingTopBottom(double paddingTopBottom) => element => element.style.paddingTopBottom = paddingTopBottom.AsPixel();
    public static HtmlElementModifier PaddingLeftTop(double paddingLeftTop) => element => element.style.paddingLeftTop = paddingLeftTop.AsPixel();
    public static HtmlElementModifier PaddingLeftBottom(double paddingLeftBottom) => element => element.style.paddingLeftBottom = paddingLeftBottom.AsPixel();
    public static HtmlElementModifier PaddingTopRight(double paddingTopRight) => element => element.style.paddingTopRight = paddingTopRight.AsPixel();

    #endregion



    internal static string AsPixel(this double value) => value + "px";

    

    public static string HexToRgb(string hexColor, double opacity = 1)
    {
        var color = ColorTranslator.FromHtml(hexColor);
        
        int   r     = Convert.ToInt16(color.R);
        int   g     = Convert.ToInt16(color.G);
        int   b     = Convert.ToInt16(color.B);
        
        return $"rgba({r}, {g}, {b}, {opacity})";
    }

    public static void Apply(this HtmlElement htmlElement, params HtmlElementModifier[] modifiers)
    {
        if (modifiers is null)
        {
            return;
        }

        foreach (var modify in modifiers)
        {
            if (modify is null)
            {
                continue;
            }

            modify(htmlElement);
        }
    }

    public static string ToJson(this ComponentResponse value)
    {
        var options = new JsonSerializerOptions();

        options = options.ModifyForReactWithDotNet();
            
        return JsonSerializer.Serialize(value, options);
    }

    public static JsonSerializerOptions ModifyForReactWithDotNet(this JsonSerializerOptions options)
    {
        return JsonSerializationOptionHelper.Modify(options);
    }

    public static TParent appendChild<TParent,TChild>(this TParent element, TChild child) where TParent : Element where TChild : Element
    {
        element.children.Add(child);

        return element;
    }
}