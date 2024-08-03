using System.Drawing;
using System.Globalization;
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
    
    /// <summary>
    ///     style.background = 'white'
    /// </summary>
    public static StyleModifier BackgroundWhite=>
        Background("white");

  

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
    
    /// <summary>
    ///     style.color = 'inherit'
    /// </summary>
    public static StyleModifier ColorInherit=> Color("inherit");

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
    public static StyleModifier DisplayBlock 
        => new(style => style.display = "block");

    /// <summary>
    ///     style.display = inline-block
    /// </summary>
    public static StyleModifier DisplayInlineBlock
        => new (style => style.display = "inline-block");

    /// <summary>
    ///     style.display = 'flex'
    /// </summary>
    public static StyleModifier DisplayFlex => new(style => style.display = "flex");
    
    /// <summary>
    ///     style.display = 'inline-flex'
    /// </summary>
    public static StyleModifier DisplayInlineFlex => new(style => style.display = "inline-flex");

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
    ///     overflow = "auto"
    /// </summary>
    public static StyleModifier OverflowAuto => new(style => style.overflow = "auto");

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
    ///     overflowX: auto
    /// </summary>
    public static StyleModifier OverflowXAuto => new(style => style.overflowX = "auto");
    
    /// <summary>
    ///     overflowX: visible
    /// </summary>
    public static StyleModifier OverflowXVisible=> new(style => style.overflowX = "visible");
    
    /// <summary>
    ///     overflowY: visible
    /// </summary>
    public static StyleModifier OverflowYVisible=> new(style => style.overflowY = "visible");
    
    /// <summary>
    ///     overflowY: scroll
    /// </summary>
    public static StyleModifier OverflowYScroll=> new(style => style.overflowY = "scroll");
    
    /// <summary>
    ///     overflowX: scroll
    /// </summary>
    public static StyleModifier OverflowXScroll=> new(style => style.overflowX = "scroll");

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
    /// </summary>
    public static StyleModifier WidthMax => Width("100%");
    
    /// <summary>
    ///     height: 100%
    /// </summary>
    public static StyleModifier HeightMax => Height("100%");
    
    /// <summary>
    ///     width: fit-content
    /// </summary>
    public static StyleModifier WidthFitContent => Width("fit-content");
    
    /// <summary>
    ///     height: fit-content
    /// </summary>
    public static StyleModifier HeightFitContent => Height("fit-content");

    public static TParent appendChild<TParent, TChild>(this TParent element, TChild child) where TParent : Element where TChild : Element
    {
        element.children.Add(child);

        return element;
    }

    public static void Apply(this Element element, params Modifier[] modifiers)
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
    ///     style.backdropFilter = blur(px)
    /// </summary>
    public static StyleModifier BackdropFilterBlur(double valueAsPixel) =>
        new(style => style.backdropFilter = $"blur({valueAsPixel.AsPixel()})");

 

   



    public static StyleModifier WebkitBackgroundClipText=>WebkitBackgroundClip("text");
    
    /// <summary>
    ///     style.cackgroundClip = 'text'
    /// </summary>
    public static StyleModifier BackgroundClipText=>BackgroundClip("text");

    public static readonly string Transparent = "transparent";

    

    
    
    /// <summary>
    /// The background-image property sets one or more background images for an element.
    /// <br/>
    /// By default, a background-image is placed at the top-left corner of an element, and repeated both vertically and horizontally.
    /// <br/>
    /// style.backgroundImage = <paramref name="backgroundImages[0]"/>, <paramref name="backgroundImages[1]"/>, <paramref name="backgroundImages[...]"/>
    /// </summary>
    public static StyleModifier BackgroundImage(params string[] backgroundImages) 
        => new(style => style.backgroundImage = string.Join(", ", backgroundImages));



   
    
    public static StyleModifier Bottom(double bottom) => Bottom(bottom.AsPixel());

    public static StyleModifier BottomRight(string bottomAndRight) => Bottom(bottomAndRight) + Right(bottomAndRight);
    public static StyleModifier BottomRight(double bottomAndRight) => Bottom(bottomAndRight) + Right(bottomAndRight);
    public static StyleModifier TopRight(double topAndRight) => Top(topAndRight) + Right(topAndRight);

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
    ///     initialize dir attribute of html element
    /// </summary>
    public static HtmlElementModifier Dir(string direction) => CreateHtmlElementModifier<HtmlElement>(element => element.dir = direction);

    /// <summary>
    ///     style.flexGrow = <paramref name="growValue" />
    /// </summary>
    public static StyleModifier FlexGrow(double growValue) 
        => new(style => style.flexGrow = growValue + "");

    /// <summary>
    ///     Specifies how the item will shrink relative to the rest of the flexible items inside the same container.
    ///     <br/>
    ///     style.flexShrink = <paramref name="value" />
    ///     <br/>
    ///     Note: If the element is not a flexible item, the flex-shrink property has no effect.
    /// </summary>
    public static StyleModifier FlexShrink(double value)
        => new(style => style.flexShrink = value + "");

   

    /// <summary>
    ///     Specifies the initial length of a flexible item.
    ///     <br/>
    ///     style.flexBasis = <paramref name="value" /> + <b>px</b>
    ///     <br/>
    ///     Note: If the element is not a flexible item, the flex-shrink property has no effect.
    /// </summary>
    public static StyleModifier FlexBasis(double value)
        => new(style => style.flexBasis = value.AsPixel());

    /// <summary>
    ///     Specifies the initial length of a flexible item.
    ///     <br/>
    ///     style.flexBasis = auto
    ///     <br/>
    ///     Note: If the element is not a flexible item, the flex-shrink property has no effect.
    /// </summary>
    public static StyleModifier FlexBasisAuto => FlexBasis("auto");

    /// <summary>
    ///     style.float = value
    /// </summary>
    public static StyleModifier Float(string value) =>
        new(style => style.cssFloat = value);

    public static StyleModifier Focus(params StyleModifier[] modifiers)
    {
        return Pseudo(x => x.focus, modifiers);
    }
    
    public static StyleModifier FocusVisible(params StyleModifier[] modifiers)
    {
        return Pseudo(x => x.focusVisible, modifiers);
    }
    
    public static StyleModifier Gap(double gap) => new(style => style.gap = gap.AsPixel());
    
    public static StyleModifier Gap(CssUnit gap) => Gap(gap.ToString());

    /// <summary>
    ///     style.height = 0px
    /// </summary>
    public static StyleModifier Height0 => Height(0);
    
    public static StyleModifier Height(double height) => Height(height.AsPixel());



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
    ///     initialize tabIndex attribute of html element
    /// </summary>
    public static HtmlElementModifier TabIndex(string tabIndex) => CreateHtmlElementModifier<HtmlElement>(element => element.tabIndex = tabIndex);

    /// <summary>
    ///     initialize tabIndex attribute of html element
    /// </summary>
    public static HtmlElementModifier TabIndex(int tabIndex) => TabIndex(tabIndex.ToString());
    
    
    /// <summary>
    ///     initialize id attribute of html element
    /// </summary>
    public static HtmlElementModifier Id(string id) 
        => CreateHtmlElementModifier<HtmlElement>(element => element.id = id);
    
    
    /// <summary>
    /// Specifies whether an element is draggable or not.
    /// <br/>
    /// Tip: Links and images are draggable by default.
    /// </summary>
    public static HtmlElementModifier Draggable(string value) 
        => CreateHtmlElementModifier<HtmlElement>(element => element.draggable = value);
    
    

    
    
    

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
    
    public static StyleModifier LeftRight(string valueForLeftAndRight) 
        => new(style => style.leftRight = valueForLeftAndRight);
    
    /// <summary>
    ///     left = <paramref name="value"/>
    ///     <br/>
    ///     bottom = <paramref name="value"/>
    /// </summary>
    public static StyleModifier LeftBottom(string value) 
        => new(style => style.leftBottom = value);

    /// <summary>
    ///     left = <paramref name="value"/> + 'px'
    ///     <br/>
    ///     bottom = <paramref name="value"/> + 'px'
    /// </summary>
    public static StyleModifier LeftBottom(double value) => LeftBottom(value.AsPixel());
    
    /// <summary>
    ///     right = <paramref name="value"/>
    ///     <br/>
    ///     bottom = <paramref name="value"/>
    /// </summary>
    public static StyleModifier RightBottom(string value) 
        => new(style => style.rightBottom = value);

    /// <summary>
    ///     right = <paramref name="value"/> + 'px'
    ///     <br/>
    ///     bottom = <paramref name="value"/> + 'px'
    /// </summary>
    public static StyleModifier RightBottom(double value) => RightBottom(value.AsPixel());

    /// <summary>
    ///     style.left = <paramref name="pixelValue" /> + 'px'
    ///     <br />
    ///     style.right = <paramref name="pixelValue" /> + 'px'
    /// </summary>
    public static StyleModifier LeftRight(double pixelValue) => new(style => style.leftRight = pixelValue.AsPixel());

    /// <summary>
    ///     style.left = <paramref name="value" /> 
    ///     <br />
    ///     style.right = <paramref name="value" />
    ///     <br />
    ///     style.bottom = <paramref name="value" />
    /// </summary>
    public static StyleModifier LeftRightBottom(string value) 
        => new(style => style.leftRightBottom = value);
    
    
    /// <summary>
    ///     style.left = <paramref name="value" /> + 'px'
    ///     <br />
    ///     style.right = <paramref name="value" /> + 'px'
    ///     <br />
    ///     style.bottom = <paramref name="value" /> + 'px'
    /// </summary>
    public static StyleModifier LeftRightBottom(double value) 
        => new(style => style.leftRightBottom = value.AsPixel());




    /// <summary>
    ///     style.listStyle = 'inside'
    /// </summary>
    public static StyleModifier ListStyleInside => ListStyle("inside");

    /// <summary>
    ///     style.listStyle = 'none'
    /// </summary>
    public static StyleModifier ListStyleNone => ListStyle("none");

    /// <summary>
    ///     style.listStyle = 'square'
    /// </summary>
    public static StyleModifier ListStyleSquare => ListStyle("square");


    /// <summary>
    ///     style.maxHeight = <paramref name="maxHeight" /> + "px"
    /// </summary>
    public static StyleModifier MaxHeight(double maxHeight) => MaxHeight(maxHeight.AsPixel());


    /// <summary>
    ///     style.maxWidth = <paramref name="maxWidth" /> + 'px'
    /// </summary>
    public static StyleModifier MaxWidth(double maxWidth) => MaxWidth(maxWidth.AsPixel());



    /// <summary>
    ///     style.minHeight = <paramref name="minHeight" /> + "px"
    /// </summary>
    public static StyleModifier MinHeight(double minHeight) => new(style => style.minHeight = minHeight.AsPixel());


    /// <summary>
    ///     style.minWidth = minWidth + 'px'
    /// </summary>
    public static StyleModifier MinWidth(double minWidth) => MinWidth(minWidth.AsPixel());

    public static JsonSerializerOptions ModifyForReactWithDotNet(this JsonSerializerOptions options)
    {
        return JsonSerializationOptionHelper.Modify(options);
    }

    public static HtmlElementModifier OnClick(MouseEventHandler onClickHandler)
        => CreateHtmlElementModifier<HtmlElement>(element => element.onClick = onClickHandler);
    
    public static HtmlElementModifier OnClickPreview(Action onClickPreview)
        => CreateHtmlElementModifier<HtmlElement>(element => element.onClickPreview = onClickPreview);

    public static HtmlElementModifier OnMouseEnter(MouseEventHandler onMouseEnterHandler)
        => CreateHtmlElementModifier<HtmlElement>(element => element.onMouseEnter = onMouseEnterHandler);

    public static HtmlElementModifier OnMouseLeave(MouseEventHandler onMouseLeaveHandler)
        => CreateHtmlElementModifier<HtmlElement>(element => element.onMouseLeave = onMouseLeaveHandler);

    public static HtmlElementModifier OnScroll(ScrollEventHandler handler, int debounceTimeout = 400) 
        => CreateHtmlElementModifier<HtmlElement>(element =>
        {
            element.onScroll = handler;
            
            element.onScrollDebounceTimeout = debounceTimeout;
        });
    
    public static HtmlElementModifier OnKeyDown(KeyboardEventHandler onKeyDownHandler)
        => CreateHtmlElementModifier<HtmlElement>(element => element.onKeyDown = onKeyDownHandler);

    

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
    ///     Creates new div element wihth given width
    /// </summary>
    public static Element SpaceX(double widthInPx) => new div { style = { width = widthInPx.AsPixel() } };
    
    /// <summary>
    ///     Creates new div element wihth given height
    /// </summary>
    public static Element SpaceY(double heightInPx) => new div { style = { height = heightInPx.AsPixel() } };
    
    /// <summary>
    ///     Creates new div element wihth given height
    /// </summary>
    public static Element SpaceY(double heightInPx, Func<StyleModifier[], StyleModifier> breakpoint, double heightInPxOnBreakpointMatched) => new div
    {
        Height(heightInPx),
        breakpoint([Height(heightInPxOnBreakpointMatched)])
    };
    
    /// <summary>
    ///     Creates new div element wihth given width
    /// </summary>
    public static Element SpaceX(double widthInPx,Func<StyleModifier[], StyleModifier> breakpoint, double widthtInPxOnBreakpointMatched) => new div
    {
        Width(widthInPx),
        breakpoint([Width(widthtInPxOnBreakpointMatched)])
    };

    public static HtmlElementModifier Text(string innerText) 
        => CreateHtmlElementModifier<HtmlElement>(element => element.text = innerText);

    /// <summary>
    ///     Assign dangerouslySetInnerHTML of given element
    /// </summary>
    public static HtmlElementModifier DangerouslySetInnerHTML(string html)
        => CreateHtmlElementModifier<HtmlElement>(element => element.dangerouslySetInnerHTML = html);



    
    /// <summary>
    ///     element.title = title
    /// </summary>
    public static HtmlElementModifier Title(string title) => CreateHtmlElementModifier<HtmlElement>(element => element.title = title);

    public static StyleModifier Top(double top) => Top(top.AsPixel());

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

    public static ElementModifier When(bool condition, params Modifier[] modifiers)
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
    ///     Calls given <paramref name="elementFunc" /> when condition is true otherwise returns null
    /// </summary>
    public static Element When(bool? condition, Func<Element> elementFunc)
    {
        return condition == true ? elementFunc() : null;
    }
    
    internal static readonly CultureInfo CultureInfo_en_US = new ("en-US");

    internal static string AsPixel(this double value) => value.ToString(CultureInfo_en_US) + "px";

    // todo: think better solution write to output stream or maybe span
    internal static ReadOnlySpan<char> ToJson(this ComponentResponse value)
    {
        return JsonSerializer.Serialize(value, JsonSerializerOptionsInstance);
    }
    
    internal static Task ToJson(this ComponentResponse value, Stream stream)
    {
        return JsonSerializer.SerializeAsync(stream, value, JsonSerializerOptionsInstance);
    }
 
    
    internal static object DeserializeJsonBySystemTextJson(string  json, Type returnType)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return returnType.IsValueType ? Activator.CreateInstance(returnType) : null;
        }
        
        return JsonSerializer.Deserialize(json,returnType, JsonSerializerOptionsInstance);
    }
    internal static T DeserializeJsonBySystemTextJson<T>(string  json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }
        
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptionsInstance);
    }

    internal static readonly JsonSerializerOptions JsonSerializerOptionsInstance = new JsonSerializerOptions().ModifyForReactWithDotNet();
    

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
    public static StyleModifier Margin(double margin) => new(style => style.margin = margin.AsPixel());
    public static StyleModifier Margin(double topBottomPixel, double lefRighttPixel) => Margin($"{topBottomPixel.AsPixel()} {lefRighttPixel.AsPixel()}");
    public static StyleModifier Margin(double topPixel, double lefRightPixel, double bottomPixel) => Margin($"{topPixel.AsPixel()} {lefRightPixel.AsPixel()} {bottomPixel.AsPixel()}");
    public static StyleModifier Margin(double topPixel, double rightPixel, double bottomPixel, double leftPixel) => Margin($"{topPixel.AsPixel()} {rightPixel.AsPixel()} {bottomPixel.AsPixel()} {leftPixel.AsPixel()}");
    

    public static StyleModifier MarginLeft(double leftPixel) => new(style => style.marginLeft = leftPixel.AsPixel());
    public static StyleModifier MarginRight(double rightPixel) => new(style => style.marginRight = rightPixel.AsPixel());
    public static StyleModifier MarginTop(double topPixel) => new(style => style.marginTop = topPixel.AsPixel());
    public static StyleModifier MarginBottom(double bottomPixel) => new(style => style.marginBottom = bottomPixel.AsPixel());

    public static StyleModifier MarginLeftRight(string leftRightPixel) => new(style => style.marginLeftRight = leftRightPixel);
    public static StyleModifier MarginTopBottom(string topBottomPixel) => new(style => style.marginTopBottom = topBottomPixel);
    public static StyleModifier MarginLeftTop(string leftTopPixel) => new(style => style.marginLeftTop = leftTopPixel);
    public static StyleModifier MarginLeftBottom(string leftBottomPixel) => new(style => style.marginLeftBottom = leftBottomPixel);
    public static StyleModifier MarginTopRight(string topRightPixel) => new(style => style.marginTopRight = topRightPixel);

    public static StyleModifier MarginLeftRight(double leftRightPixel) => new(style => style.marginLeftRight = leftRightPixel.AsPixel());
    public static StyleModifier MarginTopBottom(double topBottomPixel) => new(style => style.marginTopBottom = topBottomPixel.AsPixel());
    public static StyleModifier MarginLeftTop(double leftTopPixel) => new(style => style.marginLeftTop = leftTopPixel.AsPixel());
    public static StyleModifier MarginLeftBottom(double leftBottomPixel) => new(style => style.marginLeftBottom = leftBottomPixel.AsPixel());
    public static StyleModifier MarginTopRight(double topRightPixel) => new(style => style.marginTopRight = topRightPixel.AsPixel());
    
    public static StyleModifier MarginX(double leftRightPixel)=>MarginLeftRight(leftRightPixel);
    public static StyleModifier MarginY(double topBottomPixel)=>MarginTopBottom(topBottomPixel);
    #endregion

    #region Padding
    public static StyleModifier Padding(double paddingPixel) => new(style => style.padding = paddingPixel.AsPixel());
    
    public static StyleModifier Padding(double topBottomPixel, double lefRightPixel) => Padding($"{topBottomPixel.AsPixel()} {lefRightPixel.AsPixel()}");
    
    public static StyleModifier Padding(double topPixel, double rightPixel, double bottomPixel, double leftPixel) => Padding($"{topPixel.AsPixel()} {rightPixel.AsPixel()} {bottomPixel.AsPixel()} {leftPixel.AsPixel()}");
    
    public static StyleModifier Padding(double topPixel, double lefRightPixel, double bottomPixel) => Padding($"{topPixel.AsPixel()} {lefRightPixel.AsPixel()} {bottomPixel.AsPixel()}");

    public static StyleModifier PaddingLeft(double leftPixel) => new(style => style.paddingLeft = leftPixel.AsPixel());
    public static StyleModifier PaddingRight(double rightPixel) => new(style => style.paddingRight = rightPixel.AsPixel());
    public static StyleModifier PaddingTop(double topPixel) => new(style => style.paddingTop = topPixel.AsPixel());
    public static StyleModifier PaddingBottom(double bottomPixel) => new(style => style.paddingBottom = bottomPixel.AsPixel());

    public static StyleModifier PaddingLeftRight(string leftRightPixel) => new(style => style.paddingLeftRight = leftRightPixel);
    public static StyleModifier PaddingTopBottom(string topBottomPixel) => new(style => style.paddingTopBottom = topBottomPixel);
    public static StyleModifier PaddingLeftTop(string leftTopPixel) => new(style => style.paddingLeftTop = leftTopPixel);
    public static StyleModifier PaddingLeftBottom(string leftBottomPixel) => new(style => style.paddingLeftBottom = leftBottomPixel);
    public static StyleModifier PaddingTopRight(string topRightPixel) => new(style => style.paddingTopRight = topRightPixel);

    public static StyleModifier PaddingLeftRight(double leftRightPixel) => new(style => style.paddingLeftRight = leftRightPixel.AsPixel());
    public static StyleModifier PaddingTopBottom(double topBottomPixel) => new(style => style.paddingTopBottom = topBottomPixel.AsPixel());
    public static StyleModifier PaddingLeftTop(double leftTopPixel) => new(style => style.paddingLeftTop = leftTopPixel.AsPixel());
    public static StyleModifier PaddingLeftBottom(double leftBottomPixel) => new(style => style.paddingLeftBottom = leftBottomPixel.AsPixel());
    public static StyleModifier PaddingTopRight(double topRightPixel) => new(style => style.paddingTopRight = topRightPixel.AsPixel());
    
    public static StyleModifier PaddingX(double leftRightPixel)=>PaddingLeftRight(leftRightPixel);
    public static StyleModifier PaddingY(double topBottomPixel)=>PaddingTopBottom(topBottomPixel);
    #endregion

    
}