using System.Drawing;
using System.Text.Json;

namespace ReactWithDotNet;

public delegate void HtmlElementModifier(HtmlElement htmlElement);

public static class Mixin
{

    public static HtmlElementModifier Gap(double gap) => element => element.style.gap = gap.AsPixel();
    public static HtmlElementModifier Gap(string gap) => element => element.style.gap = gap;

    public static HtmlElementModifier DisplayFlex => element => element.style.display = "flex";
    public static HtmlElementModifier FlexDirectionRow => element => element.style.flexDirection = "row";
    public static HtmlElementModifier FlexDirectionColumn=> element => element.style.flexDirection = "column";
    public static HtmlElementModifier JustifyContentSpaceBetween => element => element.style.justifyContent = "space-between";
    public static HtmlElementModifier AlignItemsCenter=> element => element.style.alignItems = "center";

    public static HtmlElementModifier FontSize(string fontSize) => element => element.style.fontSize = fontSize;
    public static HtmlElementModifier FontSize(double fontSize) => element => element.style.fontSize = fontSize.AsPixel();

    public static HtmlElementModifier Color(string color) => element => element.style.color = color;

    public static HtmlElementModifier MaxWidth(string maxWidth) => element => element.style.maxWidth = maxWidth;
    public static HtmlElementModifier MaxWidth(double maxWidth) => element => element.style.maxWidth = maxWidth.AsPixel();

    public static HtmlElementModifier StretchWidth => element => element.style.width = "100%";
    public static HtmlElementModifier StretchHeight => element => element.style.height = "100%";
    public static HtmlElementModifier StretchWidthHeight => element => element.style.width_height = "100%";
    public static HtmlElementModifier Border(string border) => element => element.style.border = border;
    public static HtmlElementModifier ClassName(string className) => element => element.className = className;
    public static HtmlElementModifier FontWeight400  => element=> element.style.fontWeight = "400";
    public static HtmlElementModifier FontWeight500 => element => element.style.fontWeight = "500";
    public static HtmlElementModifier FontWeight600 => element => element.style.fontWeight = "600";
    public static HtmlElementModifier FontWeight700 => element => element.style.fontWeight = "700";
    public static HtmlElementModifier FontWeight800 => element => element.style.fontWeight = "800";
    public static HtmlElementModifier PaddingLeftRight(int px) => element => element.style.paddingLeftRight = px+"px";
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