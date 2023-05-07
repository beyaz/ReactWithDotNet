﻿using System.Drawing;
using System.Text.Json;

namespace ReactWithDotNet;

public static partial class Mixin
{
    /// <summary>
    ///     style.backgroundColor = 'transparent'
    /// </summary>
    public static StyleModifier BackgroundColorTransparent =>
        BackgroundColor("transparent");

    /// <summary>
    ///     style.background = 'transparent'
    /// </summary>
    public static StyleModifier BackgroundTransparent =>
        Background("transparent");

  

    public static StyleModifier BoxSizingBorderBox => new(style => style.boxSizing = "border-box");

    public static StyleModifier BoxSizingContentBox => new(style => style.boxSizing = "content-box");

    /// <summary>
    ///     style.clear = 'both'
    /// </summary>
    public static StyleModifier ClearBoth => Clear("both");

    /// <summary>
    ///     style.clear = 'left'
    /// </summary>
    public static StyleModifier ClearLeft => Clear("left");

    /// <summary>
    ///     style.clear = 'right'
    /// </summary>
    public static StyleModifier ClearRight => Clear("right");

    /// <summary>
    ///     style.color = 'white'
    /// </summary>
    public static StyleModifier ColorWhite => Color("white");

    public static StyleModifier CursorDefault => Cursor("default");
    public static StyleModifier CursorPointer => Cursor("pointer");

    /// <summary>
    ///     style.direction = "ltr"
    /// </summary>
    public static StyleModifier DirectionLtr => new(style => style.direction = "ltr");

    /// <summary>
    ///     style.direction = "rtl"
    /// </summary>
    public static StyleModifier DirectionRtl => new(style => style.direction = "rtl");

    /// <summary>
    ///     element.dir = 'ltr'
    /// </summary>
    public static HtmlElementModifier DirLtr => Dir("ltr");

    /// <summary>
    ///     element.dir = 'rtl'
    /// </summary>
    public static HtmlElementModifier DirRtl => Dir("rtl");

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
    ///     style.float = 'left'
    /// </summary>
    public static StyleModifier FloatLeft => Float("left");

    /// <summary>
    ///     style.float = 'right'
    /// </summary>
    public static StyleModifier FloatRight => Float("right");

    

    /// <summary>
    ///     style.height = "100vh"
    /// </summary>
    public static StyleModifier Height100vh => new(style => style.height = "100vh");

    /// <summary>
    ///     style.height = "auto"
    /// </summary>
    public static StyleModifier HeightAuto => new(style => style.height = "auto");

    /// <summary>
    ///     height: 100%
    /// </summary>
    public static StyleModifier HeightMaximized => Height("100%");

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
    ///     <para>style.justifyContent = "space-around"</para>
    /// </summary>
    public static StyleModifier JustifyContentSpaceAround => new(style => style.justifyContent = "space-around");

    /// <summary>
    ///     <para>style.justifyContent = "space-between"</para>
    /// </summary>
    public static StyleModifier JustifyContentSpaceBetween => new(style => style.justifyContent = "space-between");

    /// <summary>
    ///     <para>style.justifyContent = "space-evenly"</para>
    /// </summary>
    public static StyleModifier JustifyContentSpaceEvenly => new(style => style.justifyContent = "space-evenly");

    

    /// <summary>
    ///     overflow = "hidden"
    /// </summary>
    public static StyleModifier OverflowHidden => new(style => style.overflow = "hidden");

    /// <summary>
    ///     overflow = "scroll"
    /// </summary>
    public static StyleModifier OverflowScroll => new(style => style.overflow = "scroll");

    /// <summary>
    ///     style.overflowWrap = 'anywhere'
    /// </summary>
    public static StyleModifier OverflowWrapAnywhere => new(style => style.overflowWrap = "anywhere");

    public static StyleModifier OverflowWrapBreakWord => new(style => style.overflowWrap = "break-word");

    /// <summary>
    ///     style.overflowWrap = 'normal'
    /// </summary>
    public static StyleModifier OverflowWrapNormal => new(style => style.overflowWrap = "normal");

    /// <summary>
    ///     overflowY: auto
    /// </summary>
    public static StyleModifier OverflowYAuto => new(style => style.overflowY = "auto");

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



    /// <summary>
    ///     style.width = "100vw"
    /// </summary>
    public static StyleModifier Width100vw => new(style => style.width = "100vw");

    public static StyleModifier WidthAuto => new(style => style.width = "auto");

    /// <summary>
    ///     width: 100%
    ///     <br />
    ///     height: 100%
    /// </summary>
    public static StyleModifier WidthHeightMaximized => WidthHeight("100%");

    /// <summary>
    ///     width: 100%
    /// </summary>
    public static StyleModifier WidthMaximized => Width("100%");

    public static TParent appendChild<TParent, TChild>(this TParent element, TChild child) where TParent : Element where TChild : Element
    {
        element.children.Add(child);

        return element;
    }

    public static void Apply(this Element element, params IModifier[] modifiers)
    {
        if (modifiers is null)
        {
            return;
        }

        foreach (var modifier in modifiers)
        {
            if (modifier is null)
            {
                continue;
            }

            ModifyHelper.ProcessModifier(element, modifier);
        }
    }

    public static void Apply(this Style style, params StyleModifier[] styleModifiers)
    {
        if (styleModifiers is null)
        {
            return;
        }

        foreach (var styleModifier in styleModifiers)
        {
            if (styleModifier is null)
            {
                continue;
            }

            styleModifier.ModifyStyle(style);
        }
    }

    /// <summary>
    ///     style.backdropFilter = value
    /// </summary>
    public static StyleModifier BackdropFilter(string value) =>
        new(style => style.backdropFilter = value);

    /// <summary>
    ///     style.backdropFilter = blur(px)
    /// </summary>
    public static StyleModifier BackdropFilterBlur(double px) =>
        new(style => style.backdropFilter = $"blur({px}px)");

    public static StyleModifier Background(string background)
        => new(style => style.background = background);

    public static StyleModifier WebkitTextFillColor(string color)
        => new(style => style.webkitTextFillColor = color);


    public static StyleModifier WebkitBackgroundClip(string value)
        => new(style => style.webkitBackgroundClip = value);

    public static StyleModifier WebkitBackgroundClipText=>WebkitBackgroundClip("text");

    public static readonly string Transparent = "transparent";

    

    /// <summary>
    /// style.backgroundImage = <paramref name="backgroundImage"/>
    /// </summary>
    public static StyleModifier BackgroundImage(string backgroundImage) 
        => new(style => style.backgroundImage = backgroundImage);

    public static StyleModifier BackgroundColor(string backgroundColor) => new(style => style.backgroundColor = backgroundColor);

    
    public static StyleModifier Bottom(string bottom) => new(style => style.bottom = bottom);
    public static StyleModifier Bottom(double bottom) => Bottom(bottom.AsPixel());

    public static StyleModifier BottomRight(string bottomAndRight) => Bottom(bottomAndRight) + Right(bottomAndRight);
    public static StyleModifier BottomRight(double bottomAndRight) => Bottom(bottomAndRight) + Right(bottomAndRight);

   

    /// <summary>
    ///     Adds elements to children
    /// </summary>
    public static ElementModifier Children(IEnumerable<Element> children)
    {
        if (children is null)
        {
            return null;
        }

        var array = children.ToArray();

        void modifyElement(Element element)
        {
            element.children.Clear();
            element.children.AddRange(array);
        }

        return new(modifyElement);
    }

    public static ElementModifier Children(params Element[] children)
    {
        if (children is null)
        {
            return null;
        }

        var array = children.ToArray();

        void modifyElement(Element element)
        {
            element.children.Clear();
            element.children.AddRange(array);
        }

        return new(modifyElement);
    }

    public static ElementModifier Key(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        void modifyElement(Element element)
        {
            element.key = key;
        }

        return new(modifyElement, isModifyReactKey: true);
    }

    public static HtmlElementModifier ClassName(string className) => CreateHtmlElementModifier<HtmlElement>(element => element.className = className);

    /// <summary>
    ///     style.clear = value
    /// </summary>
    public static StyleModifier Clear(string value) =>
        new(style => style.clear = value);

    public static StyleModifier Color(string color) => new(style => style.color = color);

    /// <summary>
    ///     style.content = <paramref name="content" />
    /// </summary>
    public static StyleModifier Content(string content) => new(style => style.content = content);

    /// <summary>
    ///     style.cursor = <paramref name="value" />
    /// </summary>
    public static StyleModifier Cursor(string value) => new(style => style.cursor = value);

    /// <summary>
    ///     initialize dir attribute of html element
    /// </summary>
    public static HtmlElementModifier Dir(string direction) => CreateHtmlElementModifier<HtmlElement>(element => element.dir = direction);

    /// <summary>
    ///     style.flexGrow = <paramref name="growValue" />
    /// </summary>
    public static StyleModifier FlexGrow(double growValue) => new(style => style.flexGrow = growValue + "");

    /// <summary>
    ///     style.float = value
    /// </summary>
    public static StyleModifier Float(string value) =>
        new(style => style.cssFloat = value);

    public static StyleModifier Focus(params StyleModifier[] modifiers)
    {
        return Pseudo(x => x.focus, modifiers);
    }

  

    public static StyleModifier Gap(double gap) => new(style => style.gap = gap.AsPixel());
    public static StyleModifier Gap(string gap) => new(style => style.gap = gap);

    public static string GetFullName(this Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }
    
    /// <summary>
    ///     style.height = 0px
    /// </summary>
    public static StyleModifier Height0 => Height(0);
    
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

    /// <summary>
    ///     Creates a new div with given width
    ///     <br />
    ///     new div{ style = {width = <paramref name="valueInPx" />}}
    /// </summary>
    public static HtmlElement HSpace(double valueInPx) => new div(Width(valueInPx));

    /// <summary>
    ///     initialize id attribute of html element
    /// </summary>
    public static HtmlElementModifier Id(string id) => CreateHtmlElementModifier<HtmlElement>(element => element.id = id);

    /// <summary>
    ///     initialize id attribute of html element
    /// </summary>
    public static HtmlElementModifier Id(int id) => CreateHtmlElementModifier<HtmlElement>(element => element.id = id.ToString());

    /// <summary>
    ///     initialize id attribute of html element
    /// </summary>
    public static HtmlElementModifier Id(long id) => CreateHtmlElementModifier<HtmlElement>(element => element.id = id.ToString());

    public static HtmlElementModifier Lang(string lang) => CreateHtmlElementModifier<HtmlElement>(element => element.lang = lang);

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



    /// <summary>
    ///     style.listStyle = <paramref name="listStyle" />
    /// </summary>
    public static StyleModifier ListStyle(string listStyle) => new(style => style.listStyle = listStyle);

    /// <summary>
    ///     style.listStyle = 'inside'
    /// </summary>
    public static StyleModifier ListStyleInside() => ListStyle("inside");

    /// <summary>
    ///     style.listStyle = 'none'
    /// </summary>
    public static StyleModifier ListStyleNone() => ListStyle("none");

    /// <summary>
    ///     style.listStyle = 'square'
    /// </summary>
    public static StyleModifier ListStyleSquare() => ListStyle("square");

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

    public static HtmlElementModifier OnClick(Action<MouseEvent> onClickHandler)
        => CreateHtmlElementModifier<HtmlElement>(element => element.onClick = onClickHandler);

    public static HtmlElementModifier OnMouseEnter(Action<MouseEvent> onMouseEnterHandler)
        => CreateHtmlElementModifier<HtmlElement>(element => element.onMouseEnter = onMouseEnterHandler);

    public static HtmlElementModifier OnMouseLeave(Action<MouseEvent> onMouseLeaveHandler)
        => CreateHtmlElementModifier<HtmlElement>(element => element.onMouseLeave = onMouseLeaveHandler);

    /// <summary>
    ///     Handler <paramref name="jsMethodName" /> should be in client js codes.<br />
    ///     <br />
    ///     Sample Usage:<br />
    ///     <br />
    ///     ReactWithDotNet.RegisterExternalJsObject(<paramref name="jsMethodName" />, function(e){<br />
    ///     ...<br />
    ///     ...<br />
    ///     });
    /// </summary>
    public static HtmlElementModifier OnScroll(string jsMethodName) => CreateHtmlElementModifier<HtmlElement>(element => element.onScroll = jsMethodName);

    
    /// <summary>
    ///     style.right = right
    /// </summary>
    public static StyleModifier Right(string right) => new(style => style.right = right);

    /// <summary>
    ///     style.right = right + 'px'
    /// </summary>
    public static StyleModifier Right(double right) => new(style => style.right = right.AsPixel());

    /// <summary>
    ///     Roles define the semantic meaning of content, allowing screen readers and other tools to present and support
    ///     interaction with an object in a way that is consistent with user expectations of that type of object.
    /// </summary>
    public static HtmlElementModifier Role(string role) => CreateHtmlElementModifier<HtmlElement>(element => element.role = role);

    public static HtmlElementModifier RowSpan(int? rowSpan) => CreateHtmlElementModifier<td>(element => element.rowSpan = rowSpan);

    
    /// <summary>
    ///     <br>if parent is display='flex' and flexDirection = 'row' then create empty div element with style.width = value</br>
    ///     <br>
    ///         if parent is display='flex' and flexDirection = 'column' then create empty div element with style.height =
    ///         value
    ///     </br>
    /// </summary>
    public static Element Space(string value) => new Space(value);

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
    public static Element Space(double valueInPx) => new Space(valueInPx);

    public static HtmlElementModifier Text(string innerText) => CreateHtmlElementModifier<HtmlElement>(element => element.text = innerText);

    public static StyleModifier TextAlign(string textAlign) => new(style => style.textAlign = textAlign);

    public static StyleModifier TextDecoration(string textDecoration) => new(style => style.textDecoration = textDecoration);

    
    /// <summary>
    ///     element.title = title
    /// </summary>
    public static HtmlElementModifier Title(string title) => CreateHtmlElementModifier<HtmlElement>(element => element.title = title);

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
    ///     Creates a new div with given height
    ///     <br />
    ///     new div{ style = {height = <paramref name="valueInPx" />}}
    /// </summary>
    public static HtmlElement VSpace(double valueInPx) => new div(Height(valueInPx));

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

    public static ElementModifier When(bool condition, params IModifier[] modifiers)
    {
        if (!condition)
        {
            return null;
        }

        return new ElementModifier(apply);

        void apply(Element instance)
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

   

    public static StyleModifier WidthHeight(double valuePx) => new(style => style.width_height = valuePx.AsPixel());
    public static StyleModifier WidthHeight(string width_height) => new(style => style.width_height = width_height);

    public static StyleModifier Zindex(int zIndex) => new(style => style.zIndex = zIndex.ToString());

    internal static string AsPixel(this double value) => value + "px";

    internal static string ToJson(this ComponentResponse value)
    {
        var options = new JsonSerializerOptions();

        options = options.ModifyForReactWithDotNet();

        return JsonSerializer.Serialize(value, options);
    }

    static StyleModifier Pseudo(Func<Style, Style> accessToPseudo, StyleModifier[] styleModifiers)
    {
        void apply(Style instance)
        {
            if (styleModifiers is null)
            {
                return;
            }

            foreach (var styleModifier in styleModifiers)
            {
                if (styleModifier is null)
                {
                    continue;
                }

                styleModifier.ModifyStyle(accessToPseudo(instance));
            }
        }

        return new StyleModifier(apply);
    }

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
}