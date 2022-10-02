using System.Drawing;
using System.Text.Json;

namespace ReactWithDotNet;

public static class Mixin
{
    public static Modifier AlignItemsBaseline => new(style => style.alignItems = "baseline");

    /// <summary>
    ///     <para>style.alignItems = "center"</para>
    /// </summary>
    public static Modifier AlignItemsCenter => new(style => style.alignItems = "center");

    public static Modifier AlignItemsFlexEnd => new(style => style.alignItems = "flex-end");
    public static Modifier AlignItemsFlexStart => new(style => style.alignItems = "flex-start");
    public static Modifier AlignItemsStretch => new(style => style.alignItems = "stretch");

    public static Modifier BoxSizingBorderBox => new(style => style.boxSizing = "border-box");

    /// <summary>
    ///     style.color = 'white'
    /// </summary>
    public static Modifier ColorWhite => Color("white");

    public static Modifier CursorPointer => new(style => style.cursor = "pointer");

    /// <summary>
    ///     style.direction = "ltr"
    /// </summary>
    public static Modifier DirectionLtr => new(style => style.direction = "ltr");

    /// <summary>
    ///     style.direction = "rtl"
    /// </summary>
    public static Modifier DirectionRtl => new(style => style.direction = "rtl");

    public static Modifier DisplayFlex => new(style => style.display = "flex");

    /// <summary>
    ///     style.display = "none"
    /// </summary>
    public static Modifier DisplayNone => new(style => style.display = "none");

    public static Modifier DisplayNull => new(style => style.display = null);

    public static Modifier FlexDirectionColumn => new(style => style.flexDirection = "column");

    /// <summary>
    ///     flexDirection = "row"
    /// </summary>
    public static Modifier FlexDirectionRow => new(style => style.flexDirection = "row");

    /// <summary>
    ///     flexDirection = "row-reverse"
    /// </summary>
    public static Modifier FlexDirectionRowReverse => new(style => style.flexDirection = "row-reverse");

    public static Modifier FlexNoWrap => new(style => style.flexWrap = "nowrap");

    public static Modifier FlexWrap => new(style => style.flexWrap = "wrap");
    public static Modifier FlexWrapReverse => new(style => style.flexWrap = "wrap-reverse");

    public static Modifier FontSize10 => FontSize(10);
    public static Modifier FontSize11 => FontSize(11);
    public static Modifier FontSize12 => FontSize(12);
    public static Modifier FontSize13 => FontSize(13);
    public static Modifier FontSize14 => FontSize(14);
    public static Modifier FontSize15 => FontSize(15);
    public static Modifier FontSize16 => FontSize(16);
    public static Modifier FontSize17 => FontSize(17);
    public static Modifier FontSize18 => FontSize(18);
    public static Modifier FontSize19 => FontSize(19);
    public static Modifier FontSize20 => FontSize(20);
    public static Modifier FontSize21 => FontSize(21);
    public static Modifier FontSize22 => FontSize(22);
    public static Modifier FontSize23 => FontSize(23);
    public static Modifier FontSize24 => FontSize(24);
    public static Modifier FontSize25 => FontSize(25);

    public static Modifier FontSize9 => FontSize(9);

    public static Modifier FontWeight400 => FontWeight("400");
    public static Modifier FontWeight500 => FontWeight("500");
    public static Modifier FontWeight600 => FontWeight("600");
    public static Modifier FontWeight700 => FontWeight("700");
    public static Modifier FontWeight800 => FontWeight("800");

    /// <summary>
    ///     style.height = "100%"
    /// </summary>
    public static Modifier Height100Percent => HeightAsPercentOf(100);

    public static Modifier Height100vh => new(style => style.height = "100vh");

    public static Modifier Height25Percent => HeightAsPercentOf(25);
    public static Modifier Height50Percent => HeightAsPercentOf(50);
    public static Modifier Height75Percent => HeightAsPercentOf(75);

    /// <summary>
    ///     <para>justifyContent = "center"</para>
    /// </summary>
    public static Modifier JustifyContentCenter => new(style => style.justifyContent = "center");

    /// <summary>
    ///     <para>style.justifyContent = "flex-start"</para>
    /// </summary>
    public static Modifier JustifyContentFlexStart => new(style => style.justifyContent = "flex-start");

    /// <summary>
    ///     <para>style.justifyContent = "space-between"</para>
    /// </summary>
    public static Modifier JustifyContentSpaceBetween => new(style => style.justifyContent = "space-between");

    /// <summary>
    ///     overflow = "hidden"
    /// </summary>
    public static Modifier OverflowHidden => new(style => style.overflow = "hidden");

    public static Modifier OverflowWrapBreakWord => new(style => style.overflowWrap = "break-word");

    /// <summary>
    ///     width = '100%' , height = '100%'
    /// </summary>
    public static Modifier StretchWidthHeight => new(style => style.width_height = "100%");

    public static Modifier TextAlignCenter => TextAlign("center");
    public static Modifier TextAlignLeft => TextAlign("left");
    public static Modifier TextAlignRight => TextAlign("right");

    /// <summary>
    ///     textDecoration = 'line-through'
    /// </summary>
    public static Modifier TextDecorationLineThrough => TextDecoration("line-through");

    public static Modifier TextDecorationNone => TextDecoration("none");

    /// <summary>
    ///     textDecoration = "overline"
    /// </summary>
    public static Modifier TextDecorationOverline => TextDecoration("overline");

    /// <summary>
    ///     textDecoration = "underline"
    /// </summary>
    public static Modifier TextDecorationUnderline => TextDecoration("underline");

    /// <summary>
    ///     style.width = "100%"
    /// </summary>
    public static Modifier Width100Percent => WidthAsPercentOf(100);

    public static Modifier Width25Percent => WidthAsPercentOf(25);
    public static Modifier Width50Percent => WidthAsPercentOf(50);
    public static Modifier Width75Percent => WidthAsPercentOf(75);
    public static Modifier WidthAuto => new(style => style.width = "auto");

    public static TParent appendChild<TParent, TChild>(this TParent element, TChild child) where TParent : Element where TChild : Element
    {
        element.children.Add(child);

        return element;
    }

    public static void Apply(this HtmlElement htmlElement, params Modifier[] modifiers)
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

            modify.Apply(htmlElement);
        }
    }

    public static void Apply(this Style style, params Modifier[] modifiers)
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

            modify.Apply(style);
        }
    }

    public static Modifier Background(string background) => new(style => style.background = background);

    public static Modifier BackgroundColor(string backgroundColor) => new(style => style.backgroundColor = backgroundColor);

    public static Modifier Border(string border) => new(style => style.border = border);
    public static Modifier BorderBottom(string borderBottom) => new(style => style.borderBottom = borderBottom);
    public static Modifier BorderLeft(string borderLeft) => new(style => style.borderLeft = borderLeft);

    public static Modifier BorderRadius(string borderRadius) => new(style => style.borderRadius = borderRadius);
    public static Modifier BorderRadius(double borderRadius) => BorderRadius(borderRadius.AsPixel());
    public static Modifier BorderRight(string borderRight) => new(style => style.borderRight = borderRight);
    public static Modifier BorderTop(string borderTop) => new(style => style.borderTop = borderTop);
    public static Modifier Bottom(string bottom) => new(style => style.bottom = bottom);

    public static Modifier BoxShadow(string boxShadow) => new(style => style.boxShadow = boxShadow);
    public static Modifier ClassName(string className) => new(element => element.className = className);

    public static Modifier Color(string color) => new(style => style.color = color);

    /// <summary>
    ///     style.fontFamily = fontFamily
    /// </summary>
    public static Modifier FontFamily(string fontFamily) => new(style => style.fontFamily = fontFamily);

    public static Modifier FontSize(string fontSize) => new(style => style.fontSize = fontSize);
    public static Modifier FontSize(double fontSizePx) => FontSize(fontSizePx.AsPixel());

    public static Modifier FontWeight(string fontWeight) => new(style => style.fontWeight = fontWeight);

    public static Modifier Gap(double gap) => new(style => style.gap = gap.AsPixel());
    public static Modifier Gap(string gap) => new(style => style.gap = gap);

    public static Modifier Height(double height) => Height(height.AsPixel());
    public static Modifier Height(string height) => new(style => style.height = height);

    /// <summary>
    ///     height = valueAsPercent + "%"
    /// </summary>
    public static Modifier HeightAsPercentOf(double valueAsPercent) => new(style => style.height = valueAsPercent + "%");

    public static string HexToRgb(string hexColor, double opacity = 1)
    {
        var color = ColorTranslator.FromHtml(hexColor);

        int r = Convert.ToInt16(color.R);
        int g = Convert.ToInt16(color.G);
        int b = Convert.ToInt16(color.B);

        return $"rgba({r}, {g}, {b}, {opacity})";
    }

    public static Modifier Hover(params Modifier[] modifiers)
    {
        void apply(Style instance)
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

                modify.Apply(instance.hover);
            }
        }

        return new Modifier(apply);
    }

    public static Modifier id(string id) => new(element => element.id = id);

    public static Modifier Left(double left) => Left(left.AsPixel());
    public static Modifier Left(string left) => new(style => style.left = left);
    public static Modifier LeftRight(string valueForLeftAndRight) => new(style => style.leftRight = valueForLeftAndRight);
    public static Modifier LeftRightBottom(string valueForLeftAndRightAndBottom) => new(style => style.leftRightBottom = valueForLeftAndRightAndBottom);

    public static Modifier MaxHeight(string height) => new(style => style.maxHeight = height);
    public static Modifier MaxHeight(double height) => MaxHeight(height.AsPixel());

    public static Modifier MaxWidth(string maxWidth) => new(style => style.maxWidth = maxWidth);
    public static Modifier MaxWidth(double maxWidth) => new(style => style.maxWidth = maxWidth.AsPixel());

    public static JsonSerializerOptions ModifyForReactWithDotNet(this JsonSerializerOptions options)
    {
        return JsonSerializationOptionHelper.Modify(options);
    }

    public static Modifier OnClick(Action<MouseEvent> onClickHandler) => new(element => element.onClick = onClickHandler);

    public static Modifier Right(string right) => new(style => style.right = right);
    public static Modifier Text(string innerText) => new(element => element.text = innerText);
    public static Modifier TextAlign(string textAlign) => new(style => style.textAlign = textAlign);

    public static Modifier TextDecoration(string textDecoration) => new(style => style.textDecoration = textDecoration);

    public static string ToJson(this ComponentResponse value)
    {
        var options = new JsonSerializerOptions();

        options = options.ModifyForReactWithDotNet();

        return JsonSerializer.Serialize(value, options);
    }

    public static Modifier Top(double top) => Top(top.AsPixel());
    public static Modifier Top(string top) => new(style => style.top = top);
    public static Modifier TopBottom(string valueForTopAndBottom) => new(style => style.topBottom = valueForTopAndBottom);

    public static Modifier Transition(string transition) => new(style => style.transition = transition);

    /// <summary>
    ///     Apply given modifiers when condition is true
    /// </summary>
    public static Modifier When(bool condition, params Modifier[] modifiers)
    {
        if (!condition)
        {
            return null;
        }

        return new Modifier(apply);

        void apply(Style instance)
        {
            instance.Apply(modifiers);
        }
    }

    public static Modifier Width(double width) => new(style => style.width = width.AsPixel());
    public static Modifier Width(string width) => new(style => style.width = width);

    public static Modifier WidthAsPercentOf(double valueAsPercent) => new(style => style.width = valueAsPercent + "%");
    public static Modifier WidthHeight(double valuePx) => new(style => style.width_height = valuePx.AsPixel());
    public static Modifier WidthHeight(string width_height) => new(style => style.width_height = width_height);

    public static Modifier Zindex(int zIndex) => new(style => style.zIndex = zIndex.ToString());

    internal static string AsPixel(this double value) => value + "px";

    #region Position
    public static Modifier PositionRelative => new(style => style.position = "relative");
    public static Modifier PositionFixed => new(style => style.position = "fixed");
    public static Modifier PositionAbsolute => new(style => style.position = "absolute");
    public static Modifier PositionSticky => new(style => style.position = "sticky");
    public static Modifier PositionStatic => new(style => style.position = "static");
    #endregion

    #region Margin
    public static Modifier Margin(string margin) => new(style => style.margin = margin);
    public static Modifier Margin(double margin) => new(style => style.margin = margin.AsPixel());
    public static Modifier MarginRight(string marginRight) => new(style => style.marginRight = marginRight);
    public static Modifier MarginLeft(string marginLeft) => new(style => style.marginLeft = marginLeft);
    public static Modifier MarginTop(string marginTop) => new(style => style.marginTop = marginTop);
    public static Modifier MarginBottom(string marginBottom) => new(style => style.marginBottom = marginBottom);

    public static Modifier MarginLeft(double marginLeftAsPx) => new(style => style.marginLeft = marginLeftAsPx.AsPixel());
    public static Modifier MarginRight(double marginRightAsPx) => new(style => style.marginRight = marginRightAsPx.AsPixel());
    public static Modifier MarginTop(double marginTopAsPx) => new(style => style.marginTop = marginTopAsPx.AsPixel());
    public static Modifier MarginBottom(double marginBottomAsPx) => new(style => style.marginBottom = marginBottomAsPx.AsPixel());

    public static Modifier MarginLeftRight(string marginLeftRight) => new(style => style.marginLeftRight = marginLeftRight);
    public static Modifier MarginTopBottom(string marginTopBottom) => new(style => style.marginTopBottom = marginTopBottom);
    public static Modifier MarginLeftTop(string marginLeftTop) => new(style => style.marginLeftTop = marginLeftTop);
    public static Modifier MarginLeftBottom(string marginLeftBottom) => new(style => style.marginLeftBottom = marginLeftBottom);
    public static Modifier MarginTopRight(string marginTopRight) => new(style => style.marginTopRight = marginTopRight);

    public static Modifier MarginLeftRight(double marginLeftRight) => new(style => style.marginLeftRight = marginLeftRight.AsPixel());
    public static Modifier MarginTopBottom(double marginTopBottom) => new(style => style.marginTopBottom = marginTopBottom.AsPixel());
    public static Modifier MarginLeftTop(double marginLeftTop) => new(style => style.marginLeftTop = marginLeftTop.AsPixel());
    public static Modifier MarginLeftBottom(double marginLeftBottom) => new(style => style.marginLeftBottom = marginLeftBottom.AsPixel());
    public static Modifier MarginTopRight(double marginTopRight) => new(style => style.marginTopRight = marginTopRight.AsPixel());
    #endregion

    #region Padding
    public static Modifier Padding(double paddingPx) => new(style => style.padding = paddingPx.AsPixel());
    public static Modifier Padding(string padding) => new(style => style.padding = padding);
    public static Modifier Padding(double topBottomPx, double rightLeftPx) => Padding($"{topBottomPx}px {rightLeftPx}px");
    public static Modifier PaddingRight(string paddingRight) => new(style => style.paddingRight = paddingRight);
    public static Modifier PaddingLeft(string paddingLeft) => new(style => style.paddingLeft = paddingLeft);
    public static Modifier PaddingTop(string paddingTop) => new(style => style.paddingTop = paddingTop);
    public static Modifier PaddingBottom(string paddingBottom) => new(style => style.paddingBottom = paddingBottom);

    public static Modifier PaddingLeft(double paddingLeftAsPx) => new(style => style.paddingLeft = paddingLeftAsPx.AsPixel());
    public static Modifier PaddingRight(double paddingRightAsPx) => new(style => style.paddingRight = paddingRightAsPx.AsPixel());
    public static Modifier PaddingTop(double paddingTopAsPx) => new(style => style.paddingTop = paddingTopAsPx.AsPixel());
    public static Modifier PaddingBottom(double paddingBottomAsPx) => new(style => style.paddingBottom = paddingBottomAsPx.AsPixel());

    public static Modifier PaddingLeftRight(string paddingLeftRight) => new(style => style.paddingLeftRight = paddingLeftRight);
    public static Modifier PaddingTopBottom(string paddingTopBottom) => new(style => style.paddingTopBottom = paddingTopBottom);
    public static Modifier PaddingLeftTop(string paddingLeftTop) => new(style => style.paddingLeftTop = paddingLeftTop);
    public static Modifier PaddingLeftBottom(string paddingLeftBottom) => new(style => style.paddingLeftBottom = paddingLeftBottom);
    public static Modifier PaddingTopRight(string paddingTopRight) => new(style => style.paddingTopRight = paddingTopRight);

    public static Modifier PaddingLeftRight(double paddingLeftRight) => new(style => style.paddingLeftRight = paddingLeftRight.AsPixel());
    public static Modifier PaddingTopBottom(double paddingTopBottom) => new(style => style.paddingTopBottom = paddingTopBottom.AsPixel());
    public static Modifier PaddingLeftTop(double paddingLeftTop) => new(style => style.paddingLeftTop = paddingLeftTop.AsPixel());
    public static Modifier PaddingLeftBottom(double paddingLeftBottom) => new(style => style.paddingLeftBottom = paddingLeftBottom.AsPixel());
    public static Modifier PaddingTopRight(double paddingTopRight) => new(style => style.paddingTopRight = paddingTopRight.AsPixel());
    #endregion
}