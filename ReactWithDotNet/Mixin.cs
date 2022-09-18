using System.Text.Json;

namespace ReactWithDotNet;

public static class Mixin
{
    public static Action<HtmlElement> DisplayFlex => element => element.style.display = "flex";
    public static Action<HtmlElement> FlexDirectionRow => element => element.style.flexDirection = "row";
    public static Action<HtmlElement> FlexDirectionColumn=> element => element.style.flexDirection = "column";
    public static Action<HtmlElement> JustifyContentSpaceBetween => element => element.style.justifyContent = "space-between";
    public static Action<HtmlElement> AlignItemsCenter=> element => element.style.alignItems = "center";
    public static Action<HtmlElement> StretchWidth => element => element.style.width = "100%";
    public static Action<HtmlElement> StretchHeight => element => element.style.height = "100%";
    public static Action<HtmlElement> StretchWidthHeight => element => element.style.width_height = "100%";
    public static Action<HtmlElement> Border(string border) => element => element.style.border = border;
    public static Action<HtmlElement> ClassName(string className) => element => element.className = className;
    public static Action<HtmlElement> FontWeight400  => element=> element.style.fontWeight = "400";
    public static Action<HtmlElement> FontWeight500 => element => element.style.fontWeight = "500";
    public static Action<HtmlElement> FontWeight600 => element => element.style.fontWeight = "600";
    public static Action<HtmlElement> FontWeight700 => element => element.style.fontWeight = "700";
    public static Action<HtmlElement> FontWeight800 => element => element.style.fontWeight = "800";
    public static Action<HtmlElement> PaddingLeftRight(int px) => element => element.style.paddingLeftRight = px+"px";
    public static Action<HtmlElement> BoxSizingBorderBox => element => element.style.boxSizing = "border-box";
    public static Action<HtmlElement> Zindex(int zIndex) => element => element.style.zIndex = zIndex.ToString();
    public static Action<HtmlElement> Text(string innerText) => element => element.text = innerText;
    
    #region Margin
    public static Action<HtmlElement> MarginRight(string marginRight) => element => element.style.marginRight = marginRight;
    public static Action<HtmlElement> MarginLeft(string marginLeft) => element => element.style.marginLeft = marginLeft;
    public static Action<HtmlElement> MarginTop(string marginTop) => element => element.style.marginTop = marginTop;
    public static Action<HtmlElement> MarginBottom(string marginBottom) => element => element.style.marginBottom = marginBottom;

    public static Action<HtmlElement> MarginLeft(double marginLeftAsPx) => element => element.style.marginLeft = marginLeftAsPx.AsPixel();
    public static Action<HtmlElement> MarginRight(double marginRightAsPx) => element => element.style.marginRight = marginRightAsPx.AsPixel();
    public static Action<HtmlElement> MarginTop(double marginTopAsPx) => element => element.style.marginTop = marginTopAsPx.AsPixel();
    public static Action<HtmlElement> MarginBottom(double marginBottomAsPx) => element => element.style.marginBottom = marginBottomAsPx.AsPixel();

    public static Action<HtmlElement> MarginLeftRight(string marginLeftRight) => element => element.style.marginLeftRight = marginLeftRight;
    public static Action<HtmlElement> MarginTopBottom(string marginTopBottom) => element => element.style.marginTopBottom = marginTopBottom;
    public static Action<HtmlElement> MarginLeftTop(string marginLeftTop) => element => element.style.marginLeftTop = marginLeftTop;
    public static Action<HtmlElement> MarginLeftBottom(string marginLeftBottom) => element => element.style.marginLeftBottom = marginLeftBottom;
    public static Action<HtmlElement> MarginTopRight(string marginTopRight) => element => element.style.marginTopRight = marginTopRight;

    public static Action<HtmlElement> MarginLeftRight(double marginLeftRight) => element => element.style.marginLeftRight = marginLeftRight.AsPixel();
    public static Action<HtmlElement> MarginTopBottom(double marginTopBottom) => element => element.style.marginTopBottom = marginTopBottom.AsPixel();
    public static Action<HtmlElement> MarginLeftTop(double marginLeftTop) => element => element.style.marginLeftTop = marginLeftTop.AsPixel();
    public static Action<HtmlElement> MarginLeftBottom(double marginLeftBottom) => element => element.style.marginLeftBottom = marginLeftBottom.AsPixel();
    public static Action<HtmlElement> MarginTopRight(double marginTopRight) => element => element.style.marginTopRight = marginTopRight.AsPixel();

    #endregion
    


    internal static string AsPixel(this double value) => value + "px";

    public static void Apply(this HtmlElement htmlElement, params Action<HtmlElement>[] modifiers)
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