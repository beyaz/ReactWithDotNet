using System.Drawing;
using System.Globalization;
using System.Text.Json;

namespace ReactWithDotNet;

public static partial class Mixin
{
    /// <summary>
    ///     style.flexGrow = <paramref name="growValue"/>
    /// </summary>
    public static StyleModifier FlexGrow(double growValue) => new(style => style.flexGrow = growValue + "");
    
    
    public static string GetFullName(this Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }
    
    public static StyleModifier AlignItemsBaseline => new(style => style.alignItems = "baseline");

    /// <summary>
    ///     <para>style.alignItems = "center"</para>
    /// </summary>
    public static StyleModifier AlignItemsCenter => new(style => style.alignItems = "center");

    public static StyleModifier AlignItemsFlexEnd => new(style => style.alignItems = "flex-end");
    public static StyleModifier AlignItemsFlexStart => new(style => style.alignItems = "flex-start");
    public static StyleModifier AlignItemsStretch => new(style => style.alignItems = "stretch");

    public static StyleModifier BoxSizingBorderBox => new(style => style.boxSizing = "border-box");

    public static StyleModifier BoxSizingContentBox => new(style => style.boxSizing = "content-box");

    /// <summary>
    ///     style.color = 'white'
    /// </summary>
    public static StyleModifier ColorWhite => Color("white");

    public static StyleModifier CursorPointer => new(style => style.cursor = "pointer");

    /// <summary>
    ///     style.direction = "ltr"
    /// </summary>
    public static StyleModifier DirectionLtr => new(style => style.direction = "ltr");

    /// <summary>
    ///     style.direction = "rtl"
    /// </summary>
    public static StyleModifier DirectionRtl => new(style => style.direction = "rtl");

    /// <summary>
    ///     style.display = 'block'
    /// </summary>
    public static StyleModifier DisplayBlock => new(style => style.display = "block");

    /// <summary>
    ///     style.display = 'flex'
    /// </summary>
    public static StyleModifier DisplayFlex => new(style => style.display = "flex");

    /// <summary>
    ///     style.display = "none"
    /// </summary>
    public static StyleModifier DisplayNone => new(style => style.display = "none");

    public static StyleModifier DisplayNull => new(style => style.display = null);

    public static StyleModifier FlexDirectionColumn => new(style => style.flexDirection = "column");

    /// <summary>
    ///     style.flexDirection = "column-reverse"
    /// </summary>
    public static StyleModifier FlexDirectionColumnReverse => new(style => style.flexDirection = "column-reverse");

    /// <summary>
    ///     flexDirection = "row"
    /// </summary>
    public static StyleModifier FlexDirectionRow => new(style => style.flexDirection = "row");

    /// <summary>
    ///     flexDirection = "row-reverse"
    /// </summary>
    public static StyleModifier FlexDirectionRowReverse => new(style => style.flexDirection = "row-reverse");

    public static StyleModifier FlexNoWrap => new(style => style.flexWrap = "nowrap");

    public static StyleModifier FlexWrap => new(style => style.flexWrap = "wrap");
    public static StyleModifier FlexWrapReverse => new(style => style.flexWrap = "wrap-reverse");




    /// <summary>
    /// style.textTransform = <paramref name="textTransform"/>
    /// </summary>
    public static StyleModifier TextTransform(string textTransform) => new(style => style.textTransform = textTransform);

    /// <summary>
    /// style.textTransform = 'uppercase'
    /// </summary>
    public static StyleModifier TextTransformUpperCase => TextTransform("uppercase");

    /// <summary>
    /// style.textTransform = 'lowercase'
    /// </summary>
    public static StyleModifier TextTransformLowerCase => TextTransform("lowercase");

    /// <summary>
    /// style.textTransform = 'capitalize'
    /// </summary>
    public static StyleModifier TextTransformCapitalize => TextTransform("capitalize");


    public static StyleModifier FontSize10 => FontSize(10);
    public static StyleModifier FontSize11 => FontSize(11);
    public static StyleModifier FontSize12 => FontSize(12);
    public static StyleModifier FontSize13 => FontSize(13);
    public static StyleModifier FontSize14 => FontSize(14);
    public static StyleModifier FontSize15 => FontSize(15);
    public static StyleModifier FontSize16 => FontSize(16);
    public static StyleModifier FontSize17 => FontSize(17);
    public static StyleModifier FontSize18 => FontSize(18);
    public static StyleModifier FontSize19 => FontSize(19);
    public static StyleModifier FontSize20 => FontSize(20);
    public static StyleModifier FontSize21 => FontSize(21);
    public static StyleModifier FontSize22 => FontSize(22);
    public static StyleModifier FontSize23 => FontSize(23);
    public static StyleModifier FontSize24 => FontSize(24);
    public static StyleModifier FontSize25 => FontSize(25);
    public static StyleModifier FontSize26 => FontSize(25);
    public static StyleModifier FontSize27 => FontSize(25);
    public static StyleModifier FontSize28 => FontSize(25);
    public static StyleModifier FontSize29 => FontSize(25);
    public static StyleModifier FontSize30 => FontSize(25);
    public static StyleModifier FontSize9 => FontSize(9);

    public static StyleModifier FontWeight400 => FontWeight("400");
    public static StyleModifier FontWeight500 => FontWeight("500");
    public static StyleModifier FontWeight600 => FontWeight("600");
    public static StyleModifier FontWeight700 => FontWeight("700");
    public static StyleModifier FontWeight800 => FontWeight("800");
    public static StyleModifier FontWeight900 => FontWeight("900");

    public static StyleModifier FontWeightBold => FontWeight700;

    /// <summary>
    /// style.fontWeight = '600'
    /// </summary>
    public static StyleModifier FontWeightSemiBold => FontWeight600;

    /// <summary>
    /// style.fontWeight = '800'
    /// </summary>
    public static StyleModifier FontWeightExtraBold => FontWeight800;


    /// <summary>
    /// style.fontWeight = '500'
    /// </summary>
    public static StyleModifier FontWeightMedium=> FontWeight500;

   

    public static StyleModifier Height100vh => new(style => style.height = "100vh");


    /// <summary>
    ///     style.height = "auto"
    /// </summary>
    public static StyleModifier HeightAuto => new(style => style.height = "auto");

    /// <summary>
    ///     <para>justifyContent = "center"</para>
    /// </summary>
    public static StyleModifier JustifyContentCenter => new(style => style.justifyContent = "center");

    /// <summary>
    ///     <para>style.justifyContent = "flex-end"</para>
    /// </summary>
    public static StyleModifier JustifyContentFlexEnd => new(style => style.justifyContent = "flex-end");

    /// <summary>
    ///     <para>style.justifyContent = "flex-start"</para>
    /// </summary>
    public static StyleModifier JustifyContentFlexStart => new(style => style.justifyContent = "flex-start");

    /// <summary>
    ///     <para>style.justifyContent = "space-between"</para>
    /// </summary>
    public static StyleModifier JustifyContentSpaceBetween => new(style => style.justifyContent = "space-between");

    /// <summary>
    ///     <para>style.justifyContent = "space-around"</para>
    /// </summary>
    public static StyleModifier JustifyContentSpaceAround=> new(style => style.justifyContent = "space-around");

    /// <summary>
    ///     <para>style.justifyContent = "space-evenly"</para>
    /// </summary>
    public static StyleModifier JustifyContentSpaceEvenly => new(style => style.justifyContent = "space-evenly");

    public static StyleModifier LineHeight10 => LineHeight(10);
    public static StyleModifier LineHeight11 => LineHeight(11);
    public static StyleModifier LineHeight12 => LineHeight(12);
    public static StyleModifier LineHeight13 => LineHeight(13);
    public static StyleModifier LineHeight14 => LineHeight(14);
    public static StyleModifier LineHeight15 => LineHeight(15);
    public static StyleModifier LineHeight16 => LineHeight(16);
    public static StyleModifier LineHeight17 => LineHeight(17);
    public static StyleModifier LineHeight18 => LineHeight(18);
    public static StyleModifier LineHeight19 => LineHeight(19);
    public static StyleModifier LineHeight20 => LineHeight(20);
    public static StyleModifier LineHeight21 => LineHeight(21);
    public static StyleModifier LineHeight22 => LineHeight(22);
    public static StyleModifier LineHeight23 => LineHeight(23);
    public static StyleModifier LineHeight24 => LineHeight(24);
    public static StyleModifier LineHeight25 => LineHeight(25);

    public static StyleModifier LineHeight26 => LineHeight(25);
    public static StyleModifier LineHeight27 => LineHeight(25);
    public static StyleModifier LineHeight28 => LineHeight(25);
    public static StyleModifier LineHeight29 => LineHeight(25);
    public static StyleModifier LineHeight30 => LineHeight(25);
    public static StyleModifier LineHeight31 => LineHeight(25);
    public static StyleModifier LineHeight32 => LineHeight(25);
    public static StyleModifier LineHeight33 => LineHeight(25);
    public static StyleModifier LineHeight34 => LineHeight(25);
    public static StyleModifier LineHeight35 => LineHeight(25);
    public static StyleModifier LineHeight36 => LineHeight(25);
    public static StyleModifier LineHeight37 => LineHeight(25);
    public static StyleModifier LineHeight38 => LineHeight(25);
    public static StyleModifier LineHeight39 => LineHeight(25);
    public static StyleModifier LineHeight40 => LineHeight(25);

    public static StyleModifier LineHeight9 => LineHeight(9);

    /// <summary>
    ///     overflow = "hidden"
    /// </summary>
    public static StyleModifier OverflowHidden => new(style => style.overflow = "hidden");

    /// <summary>
    ///     overflow = "scroll"
    /// </summary>
    public static StyleModifier OverflowScroll => new(style => style.overflow = "scroll");

    public static StyleModifier OverflowWrapBreakWord => new(style => style.overflowWrap = "break-word");

    /// <summary>
    ///     width = '100%' , height = '100%'
    /// </summary>
    public static StyleModifier StretchWidthHeight => new(style => style.width_height = "100%");

    public static StyleModifier TextAlignCenter => TextAlign("center");
    public static StyleModifier TextAlignLeft => TextAlign("left");
    public static StyleModifier TextAlignRight => TextAlign("right");

    /// <summary>
    ///     textDecoration = 'line-through'
    /// </summary>
    public static StyleModifier TextDecorationLineThrough => TextDecoration("line-through");

    public static StyleModifier TextDecorationNone => TextDecoration("none");

    /// <summary>
    ///     textDecoration = "overline"
    /// </summary>
    public static StyleModifier TextDecorationOverline => TextDecoration("overline");

    /// <summary>
    ///     textDecoration = "underline"
    /// </summary>
    public static StyleModifier TextDecorationUnderline => TextDecoration("underline");

   


    
    public static StyleModifier WidthAuto => new(style => style.width = "auto");

    public static TParent appendChild<TParent, TChild>(this TParent element, TChild child) where TParent : Element where TChild : Element
    {
        element.children.Add(child);

        return element;
    }

    public static void Apply(this HtmlElement htmlElement, params IModifier[] modifiers)
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

            if (modify is StyleModifier styleModifier)
            {
                styleModifier.Modify(htmlElement.style);
                continue;
            }

            modify.Modify(htmlElement);
        }
    }

    public static void Apply(this Style style, params StyleModifier[] modifiers)
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

            modify.Modify(style);
        }
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

            modify.Modify(htmlElement);
        }
    }

    public static StyleModifier Background(string background) => new(style => style.background = background);

    public static StyleModifier BackgroundColor(string backgroundColor) => new(style => style.backgroundColor = backgroundColor);

    public static StyleModifier Border(string border) => new(style => style.border = border);

    public static StyleModifier Border(string top, string right, string bottom, string left) => new(style => style.border = $"{top} {right} {bottom} {left}");

    public static StyleModifier Border(double top, double right, double bottom, double left) => Border(top.AsPixel(), right.AsPixel(), bottom.AsPixel(), left.AsPixel());

    public static StyleModifier BorderBottom(string borderBottom) => new(style => style.borderBottom = borderBottom);
    public static StyleModifier BorderLeft(string borderLeft) => new(style => style.borderLeft = borderLeft);

    public static StyleModifier BorderRadius(string borderRadius) => new(style => style.borderRadius = borderRadius);
    public static StyleModifier BorderRadius(double borderRadius) => BorderRadius(borderRadius.AsPixel());
    public static StyleModifier BorderRight(string borderRight) => new(style => style.borderRight = borderRight);
    public static StyleModifier BorderTop(string borderTop) => new(style => style.borderTop = borderTop);
    public static StyleModifier Bottom(string bottom) => new(style => style.bottom = bottom);
    public static StyleModifier Bottom(double bottom) => Bottom(bottom.AsPixel());

    public static StyleModifier BottomRight(string bottomAndRight) => Bottom(bottomAndRight) | Right(bottomAndRight);
    public static StyleModifier BottomRight(double bottomAndRight) => Bottom(bottomAndRight) | Right(bottomAndRight);

    public static StyleModifier BoxShadow(string boxShadow) => new(style => style.boxShadow = boxShadow);

    /// <summary>
    ///     Adds elements to children
    /// </summary>
    public static HtmlElementModifier Children(IEnumerable<Element> children)
    {
        if (children is null)
        {
            return null;
        }

        var array = children.ToArray();

        void modifyHtmlElement(HtmlElement element)
        {
            element.children.Clear();
            element.children.AddRange(array);
        }

        return new(modifyHtmlElement);
    }

    public static HtmlElementModifier Children(params Element[] children)
    {
        if (children is null)
        {
            return null;
        }

        var array = children.ToArray();

        void modifyHtmlElement(HtmlElement element)
        {
            element.children.Clear();
            element.children.AddRange(array);
        }

        return new(modifyHtmlElement);
    }

    public static HtmlElementModifier ClassName(string className) => new(element => element.className = className);

    public static StyleModifier Color(string color) => new(style => style.color = color);

    /// <summary>
    ///     style.fontFamily = fontFamily
    /// </summary>
    public static StyleModifier FontFamily(string fontFamily) => new(style => style.fontFamily = fontFamily);

    public static StyleModifier FontSize(string fontSize) => new(style => style.fontSize = fontSize);


    public static StyleModifier FontStyle(string fontStyle) => new(style => style.fontStyle = fontStyle);

    public static StyleModifier FontStyleNormal => FontStyle("normal");
    public static StyleModifier FontStyleItalic=> FontStyle("italic");

    public static StyleModifier FontSize(double fontSizePx) => FontSize(fontSizePx.AsPixel());

    public static StyleModifier FontWeight(string fontWeight) => new(style => style.fontWeight = fontWeight);

    public static StyleModifier Gap(double gap) => new(style => style.gap = gap.AsPixel());
    public static StyleModifier Gap(string gap) => new(style => style.gap = gap);

    public static StyleModifier Height(double height) => Height(height.AsPixel());
    public static StyleModifier Height(string height) => new(style => style.height = height);

    
    public static string HexToRgb(string hexColor, double opacity = 1)
    {
        var color = ColorTranslator.FromHtml(hexColor);

        int r = Convert.ToInt16(color.R);
        int g = Convert.ToInt16(color.G);
        int b = Convert.ToInt16(color.B);

        return $"rgba({r}, {g}, {b}, {opacity})";
    }

    public static StyleModifier Hover(params StyleModifier[] modifiers)
    {
        return Pseudo(x => x.hover, modifiers);
    }

    public static StyleModifier Focus(params StyleModifier[] modifiers)
    {
        return Pseudo(x => x.focus, modifiers);
    }

    static StyleModifier Pseudo(Func<Style, Style> accessToPseudo, StyleModifier[] modifiers)
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

                modify.Modify(accessToPseudo(instance));
            }
        }

        return new StyleModifier(apply);
    }

    /// <summary>
    ///     a.href = <paramref name="href" />
    /// </summary>
    public static HtmlElementModifier Href(string href) => new(element => ((a)element).href = href);

    /// <summary>
    ///     initialize id attribute of html element
    /// </summary>
    public static HtmlElementModifier Id(string id) => new(element => element.id = id);

    /// <summary>
    ///     initialize id attribute of html element
    /// </summary>
    public static HtmlElementModifier Id(int id) => new(element => element.id = id.ToString());

    /// <summary>
    ///     initialize id attribute of html element
    /// </summary>
    public static HtmlElementModifier Id(long id) => new(element => element.id = id.ToString());

    public static StyleModifier Left(double left) => Left(left.AsPixel());
    public static StyleModifier Left(string left) => new(style => style.left = left);
    public static StyleModifier LeftRight(string valueForLeftAndRight) => new(style => style.leftRight = valueForLeftAndRight);

    /// <summary>
    ///     style.left = <paramref name="pixelValue" /> + 'px'
    ///     <br />
    ///     style.right = <paramref name="pixelValue" /> + 'px'
    /// </summary>
    public static StyleModifier LeftRight(double pixelValue) => new(style => style.leftRight = pixelValue.AsPixel());

    public static StyleModifier LeftRightBottom(string valueForLeftAndRightAndBottom) => new(style => style.leftRightBottom = valueForLeftAndRightAndBottom);

    public static StyleModifier LineHeight(string lineHeight) => new(style => style.lineHeight = lineHeight);
    public static StyleModifier LineHeight(double lineHeightPx) => LineHeight(lineHeightPx.AsPixel());

    /// <summary>
    ///     style.maxHeight = <paramref name="maxHeight" />
    /// </summary>
    public static StyleModifier MaxHeight(string maxHeight) => new(style => style.maxHeight = maxHeight);

    /// <summary>
    ///     style.maxHeight = <paramref name="maxHeight" /> + "px"
    /// </summary>
    public static StyleModifier MaxHeight(double maxHeight) => MaxHeight(maxHeight.AsPixel());

    /// <summary>
    ///     style.maxWidth = <paramref name="maxWidth" />
    /// </summary>
    public static StyleModifier MaxWidth(string maxWidth) => new(style => style.maxWidth = maxWidth);

    /// <summary>
    ///     style.maxWidth = <paramref name="maxWidth" /> + 'px'
    /// </summary>
    public static StyleModifier MaxWidth(double maxWidth) => MaxWidth(maxWidth.AsPixel());

    /// <summary>
    ///     style.minHeight = <paramref name="minHeight" />
    /// </summary>
    public static StyleModifier MinHeight(string minHeight) => new(style => style.minHeight = minHeight);

    /// <summary>
    ///     style.minHeight = <paramref name="minHeight" /> + "px"
    /// </summary>
    public static StyleModifier MinHeight(double minHeight) => new(style => style.minHeight = minHeight.AsPixel());

    /// <summary>
    ///     style.minWidth = minWidth
    /// </summary>
    public static StyleModifier MinWidth(string minWidth) => new(style => style.minWidth = minWidth);

    /// <summary>
    ///     style.minWidth = minWidth + 'px'
    /// </summary>
    public static StyleModifier MinWidth(double minWidth) => MinWidth(minWidth.AsPixel());

    public static JsonSerializerOptions ModifyForReactWithDotNet(this JsonSerializerOptions options)
    {
        return JsonSerializationOptionHelper.Modify(options);
    }

    public static HtmlElementModifier OnClick(Action<MouseEvent> onClickHandler) => new(element => element.onClick = onClickHandler);

    /// <summary>
    ///     style.opacity = <paramref name="opacity" />
    /// </summary>
    public static StyleModifier Opacity(string opacity) => new(style => style.opacity = opacity);

    /// <summary>
    ///     style.opacity = <paramref name="opacity" />
    /// </summary>
    public static StyleModifier Opacity(double opacity) => new(style => style.opacity = opacity.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    ///     style.right = right
    /// </summary>
    public static StyleModifier Right(string right) => new(style => style.right = right);

    /// <summary>
    ///     style.right = right + 'px'
    /// </summary>
    public static StyleModifier Right(double right) => new(style => style.right = right.AsPixel());

    /// <summary>
    ///     Returns a string value like "1px solid <paramref name="color" />"
    /// </summary>
    public static string Solid_1px_(string color) => "1px solid " + (color ?? throw new ArgumentNullException(nameof(color)));


    /// <summary>
    ///     <br>if parent is display='flex' and flexDirection = 'row' then create empty div element with style.width = value</br>
    ///     <br>
    ///         if parent is display='flex' and flexDirection = 'column' then create empty div element with style.height =
    ///         value
    ///     </br>
    /// </summary>
    public static HtmlElement Space(string value) => new Space(value);

    /// <summary>
    ///     <br>
    ///         if parent is display='flex' and flexDirection = 'row' then create empty div element with style.width = value +
    ///         'px'
    ///     </br>
    ///     <br>
    ///         if parent is display='flex' and flexDirection = 'column' then create empty div element with style.height =
    ///         value + 'px'
    ///     </br>
    /// </summary>
    public static HtmlElement Space(double valueInPx) => new Space(valueInPx);

    /// <summary>
    ///     img.src = <paramref name="src" />
    /// </summary>
    public static HtmlElementModifier Src(string src) => new(element => ((img)element).src = src);

    public static HtmlElementModifier Text(string innerText) => new(element => element.text = innerText);


    public static StyleModifier TextAlign(string textAlign) => new(style => style.textAlign = textAlign);

    public static StyleModifier TextDecoration(string textDecoration) => new(style => style.textDecoration = textDecoration);

    public static string ToJson(this ComponentResponse value)
    {
        var options = new JsonSerializerOptions();

        options = options.ModifyForReactWithDotNet();

        return JsonSerializer.Serialize(value, options);
    }

    public static StyleModifier Top(double top) => Top(top.AsPixel());
    public static StyleModifier Top(string top) => new(style => style.top = top);

    public static StyleModifier TopBottom(string valueForTopAndBottom) => new(style => style.topBottom = valueForTopAndBottom);

    /// <summary>
    ///     style.top = <paramref name="pixelValue" /> + 'px'
    ///     <br />
    ///     style.bottom = <paramref name="pixelValue" /> + 'px'
    /// </summary>
    public static StyleModifier TopBottom(double pixelValue) => new(style => style.topBottom = pixelValue.AsPixel());

    /// <summary>
    ///     style.transform = <paramref name="transform" />
    /// </summary>
    public static StyleModifier Transform(string transform) => new(style => style.transform = transform);

    /// <summary>
    ///     style.transition = <paramref name="transition" />
    /// </summary>
    public static StyleModifier Transition(string transition) => new(style => style.transition = transition);

    /// <summary>
    ///     style.width = <paramref name="pixelValue" /> + 'px'
    ///     <br />
    ///     style.height = <paramref name="pixelValue" /> + 'px'
    /// </summary>
    public static StyleModifier wh(double pixelValue) => WidthHeight(pixelValue);

    /// <summary>
    ///     Apply given modifiers when condition is true
    /// </summary>
    public static StyleModifier When(bool condition, params StyleModifier[] modifiers)
    {
        if (!condition)
        {
            return null;
        }

        return new StyleModifier(apply);

        void apply(Style instance)
        {
            instance.Apply(modifiers);
        }
    }

    public static HtmlElementModifier When(bool condition, params HtmlElementModifier[] modifiers)
    {
        if (!condition)
        {
            return null;
        }

        return new HtmlElementModifier(apply);

        void apply(HtmlElement instance)
        {
            instance.Apply(modifiers);
        }
    }

    /// <summary>
    ///     Returns given <paramref name="element" /> when condition is true otherwise returns null
    /// </summary>
    public static Element When(bool condition, Element element)
    {
        return condition ? element : null;
    }

    /// <summary>
    ///     Calls given <paramref name="elementFunc" /> when condition is true otherwise returns null
    /// </summary>
    public static Element When(bool condition, Func<Element> elementFunc)
    {
        return condition ? elementFunc() : null;
    }

    public static StyleModifier Width(double width) => new(style => style.width = width.AsPixel());
    public static StyleModifier Width(string width) => new(style => style.width = width);

   

    public static StyleModifier WidthHeight(double valuePx) => new(style => style.width_height = valuePx.AsPixel());
    public static StyleModifier WidthHeight(string width_height) => new(style => style.width_height = width_height);

    public static StyleModifier Zindex(int zIndex) => new(style => style.zIndex = zIndex.ToString());

    internal static string AsPixel(this double value) => value + "px";

    #region Position
    public static StyleModifier PositionRelative => new(style => style.position = "relative");
    public static StyleModifier PositionFixed => new(style => style.position = "fixed");
    public static StyleModifier PositionAbsolute => new(style => style.position = "absolute");
    public static StyleModifier PositionSticky => new(style => style.position = "sticky");
    public static StyleModifier PositionStatic => new(style => style.position = "static");
    #endregion

    #region Margin
    public static StyleModifier Margin(string margin) => new(style => style.margin = margin);
    public static StyleModifier Margin(double margin) => new(style => style.margin = margin.AsPixel());
    public static StyleModifier MarginRight(string marginRight) => new(style => style.marginRight = marginRight);
    public static StyleModifier MarginLeft(string marginLeft) => new(style => style.marginLeft = marginLeft);
    public static StyleModifier MarginTop(string marginTop) => new(style => style.marginTop = marginTop);
    public static StyleModifier MarginBottom(string marginBottom) => new(style => style.marginBottom = marginBottom);

    public static StyleModifier MarginLeft(double marginLeftAsPx) => new(style => style.marginLeft = marginLeftAsPx.AsPixel());
    public static StyleModifier MarginRight(double marginRightAsPx) => new(style => style.marginRight = marginRightAsPx.AsPixel());
    public static StyleModifier MarginTop(double marginTopAsPx) => new(style => style.marginTop = marginTopAsPx.AsPixel());
    public static StyleModifier MarginBottom(double marginBottomAsPx) => new(style => style.marginBottom = marginBottomAsPx.AsPixel());

    public static StyleModifier MarginLeftRight(string marginLeftRight) => new(style => style.marginLeftRight = marginLeftRight);
    public static StyleModifier MarginTopBottom(string marginTopBottom) => new(style => style.marginTopBottom = marginTopBottom);
    public static StyleModifier MarginLeftTop(string marginLeftTop) => new(style => style.marginLeftTop = marginLeftTop);
    public static StyleModifier MarginLeftBottom(string marginLeftBottom) => new(style => style.marginLeftBottom = marginLeftBottom);
    public static StyleModifier MarginTopRight(string marginTopRight) => new(style => style.marginTopRight = marginTopRight);

    public static StyleModifier MarginLeftRight(double marginLeftRight) => new(style => style.marginLeftRight = marginLeftRight.AsPixel());
    public static StyleModifier MarginTopBottom(double marginTopBottom) => new(style => style.marginTopBottom = marginTopBottom.AsPixel());
    public static StyleModifier MarginLeftTop(double marginLeftTop) => new(style => style.marginLeftTop = marginLeftTop.AsPixel());
    public static StyleModifier MarginLeftBottom(double marginLeftBottom) => new(style => style.marginLeftBottom = marginLeftBottom.AsPixel());
    public static StyleModifier MarginTopRight(double marginTopRight) => new(style => style.marginTopRight = marginTopRight.AsPixel());
    #endregion

    #region Padding
    public static StyleModifier Padding(double paddingPx) => new(style => style.padding = paddingPx.AsPixel());
    public static StyleModifier Padding(string padding) => new(style => style.padding = padding);
    public static StyleModifier Padding(double topBottomPx, double rightLeftPx) => Padding($"{topBottomPx}px {rightLeftPx}px");
    public static StyleModifier PaddingRight(string paddingRight) => new(style => style.paddingRight = paddingRight);
    public static StyleModifier PaddingLeft(string paddingLeft) => new(style => style.paddingLeft = paddingLeft);
    public static StyleModifier PaddingTop(string paddingTop) => new(style => style.paddingTop = paddingTop);
    public static StyleModifier PaddingBottom(string paddingBottom) => new(style => style.paddingBottom = paddingBottom);

    public static StyleModifier PaddingLeft(double paddingLeftAsPx) => new(style => style.paddingLeft = paddingLeftAsPx.AsPixel());
    public static StyleModifier PaddingRight(double paddingRightAsPx) => new(style => style.paddingRight = paddingRightAsPx.AsPixel());
    public static StyleModifier PaddingTop(double paddingTopAsPx) => new(style => style.paddingTop = paddingTopAsPx.AsPixel());
    public static StyleModifier PaddingBottom(double paddingBottomAsPx) => new(style => style.paddingBottom = paddingBottomAsPx.AsPixel());

    public static StyleModifier PaddingLeftRight(string paddingLeftRight) => new(style => style.paddingLeftRight = paddingLeftRight);
    public static StyleModifier PaddingTopBottom(string paddingTopBottom) => new(style => style.paddingTopBottom = paddingTopBottom);
    public static StyleModifier PaddingLeftTop(string paddingLeftTop) => new(style => style.paddingLeftTop = paddingLeftTop);
    public static StyleModifier PaddingLeftBottom(string paddingLeftBottom) => new(style => style.paddingLeftBottom = paddingLeftBottom);
    public static StyleModifier PaddingTopRight(string paddingTopRight) => new(style => style.paddingTopRight = paddingTopRight);

    public static StyleModifier PaddingLeftRight(double paddingLeftRight) => new(style => style.paddingLeftRight = paddingLeftRight.AsPixel());
    public static StyleModifier PaddingTopBottom(double paddingTopBottom) => new(style => style.paddingTopBottom = paddingTopBottom.AsPixel());
    public static StyleModifier PaddingLeftTop(double paddingLeftTop) => new(style => style.paddingLeftTop = paddingLeftTop.AsPixel());
    public static StyleModifier PaddingLeftBottom(double paddingLeftBottom) => new(style => style.paddingLeftBottom = paddingLeftBottom.AsPixel());
    public static StyleModifier PaddingTopRight(double paddingTopRight) => new(style => style.paddingTopRight = paddingTopRight.AsPixel());
    #endregion

    #region short
    #region margin
    /// <summary>
    ///     style.margin = <paramref name="margin" /> + 'px'
    /// </summary>
    public static StyleModifier m(double margin) => Margin(margin);

    /// <summary>
    ///     style.margin = <paramref name="margin" />
    /// </summary>
    public static StyleModifier m(string margin) => Margin(margin);

    /// <summary>
    ///     style.marginRight = <paramref name="marginRight" /> + 'px'
    /// </summary>
    public static StyleModifier mr(double marginRight) => MarginRight(marginRight);

    /// <summary>
    ///     style.marginRight = <paramref name="marginRight" />
    /// </summary>
    public static StyleModifier mr(string marginRight) => MarginRight(marginRight);

    /// <summary>
    ///     style.marginLeft = <paramref name="marginLeft" /> + 'px'
    /// </summary>
    public static StyleModifier ml(double marginLeft) => MarginLeft(marginLeft);

    /// <summary>
    ///     style.marginLeft = <paramref name="marginLeft" />
    /// </summary>
    public static StyleModifier ml(string marginLeft) => MarginLeft(marginLeft);

    /// <summary>
    ///     style.marginTop = <paramref name="marginTop" /> + 'px'
    /// </summary>
    public static StyleModifier mt(double marginTop) => MarginTop(marginTop);

    /// <summary>
    ///     style.marginTop = <paramref name="marginTop" />
    /// </summary>
    public static StyleModifier mt(string marginTop) => MarginTop(marginTop);

    /// <summary>
    ///     style.marginBottom = <paramref name="marginBottom" /> + 'px'
    /// </summary>
    public static StyleModifier mb(double marginBottom) => MarginBottom(marginBottom);

    /// <summary>
    ///     style.marginBottom = <paramref name="marginBottom" />
    /// </summary>
    public static StyleModifier mb(string marginBottom) => MarginBottom(marginBottom);

    /// <summary>
    ///     style.marginLeft = <paramref name="value" /> + 'px'
    ///     <br />
    ///     style.marginRight = <paramref name="value" /> + 'px'
    /// </summary>
    public static StyleModifier mx(double value) => MarginLeftRight(value);

    /// <summary>
    ///     style.marginLeft = <paramref name="value" />
    ///     <br />
    ///     style.marginRight = <paramref name="value" />
    /// </summary>
    public static StyleModifier mx(string value) => MarginLeftRight(value);

    /// <summary>
    ///     style.marginTop = <paramref name="value" /> + 'px'
    ///     <br />
    ///     style.marginBottom = <paramref name="value" /> + 'px'
    /// </summary>
    public static StyleModifier my(double value) => MarginTopBottom(value);

    /// <summary>
    ///     style.marginTop = <paramref name="value" />
    ///     <br />
    ///     style.marginBottom = <paramref name="value" />
    /// </summary>
    public static StyleModifier my(string value) => MarginTopBottom(value);
    #endregion

    #region padding
    /// <summary>
    ///     style.padding = <paramref name="padding" /> + 'px'
    /// </summary>
    public static StyleModifier p(double padding) => Padding(padding);

    /// <summary>
    ///     style.padding = <paramref name="padding" />
    /// </summary>
    public static StyleModifier p(string padding) => Padding(padding);

    /// <summary>
    ///     style.paddingRight = <paramref name="paddingRight" /> + 'px'
    /// </summary>
    public static StyleModifier pr(double paddingRight) => PaddingRight(paddingRight);

    /// <summary>
    ///     style.paddingRight = <paramref name="paddingRight" />
    /// </summary>
    public static StyleModifier pr(string paddingRight) => PaddingRight(paddingRight);

    /// <summary>
    ///     style.paddingLeft = <paramref name="paddingLeft" /> + 'px'
    /// </summary>
    public static StyleModifier pl(double paddingLeft) => PaddingLeft(paddingLeft);

    /// <summary>
    ///     style.paddingLeft = <paramref name="paddingLeft" />
    /// </summary>
    public static StyleModifier pl(string paddingLeft) => PaddingLeft(paddingLeft);

    /// <summary>
    ///     style.paddingTop = <paramref name="paddingTop" /> + 'px'
    /// </summary>
    public static StyleModifier pt(double paddingTop) => PaddingTop(paddingTop);

    /// <summary>
    ///     style.paddingTop = <paramref name="paddingTop" />
    /// </summary>
    public static StyleModifier pt(string paddingTop) => PaddingTop(paddingTop);

    /// <summary>
    ///     style.paddingBottom = <paramref name="paddingBottom" /> + 'px'
    /// </summary>
    public static StyleModifier pb(double paddingBottom) => PaddingBottom(paddingBottom);

    /// <summary>
    ///     style.paddingBottom = <paramref name="paddingBottom" />
    /// </summary>
    public static StyleModifier pb(string paddingBottom) => PaddingBottom(paddingBottom);

    /// <summary>
    ///     style.paddingLeft = <paramref name="value" /> + 'px'
    ///     <br />
    ///     style.paddingRight = <paramref name="value" /> + 'px'
    /// </summary>
    public static StyleModifier px(double value) => PaddingLeftRight(value);

    /// <summary>
    ///     style.paddingLeft = <paramref name="value" />
    ///     <br />
    ///     style.paddingRight = <paramref name="value" />
    /// </summary>
    public static StyleModifier px(string value) => PaddingLeftRight(value);

    /// <summary>
    ///     style.paddingTop = <paramref name="value" /> + 'px'
    ///     <br />
    ///     style.paddingBottom = <paramref name="value" /> + 'px'
    /// </summary>
    public static StyleModifier py(double value) => PaddingTopBottom(value);

    /// <summary>
    ///     style.paddingTop = <paramref name="value" />
    ///     <br />
    ///     style.paddingBottom = <paramref name="value" />
    /// </summary>
    public static StyleModifier py(string value) => PaddingTopBottom(value);
    #endregion
    #endregion



    public static HtmlElementModifier RowSpan(int? rowSpan) => new(element => ((td)element).rowSpan = rowSpan);
}