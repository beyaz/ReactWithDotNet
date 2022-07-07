
using System;
using System.Runtime.Serialization;

namespace ReactDotNet.Html5
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Struct)]
    public sealed class NameAttribute : Attribute
    {
        public readonly string value;

        public NameAttribute(string value)
        {
            this.value = value;
        }
    }

    public sealed class Union<A, B>
    {
        public A a { get; private set; }
        public B b { get; private set; }

        public static implicit operator Union<A, B>(A a)
        {
            return new Union<A, B> { a = a };
        }

        public static implicit operator Union<A, B>(B b)
        {
            return new Union<A, B> { b = b };
        }
    }




    [Serializable]
    public sealed partial class Style
    {
        public Union<string, AlignContent> alignContent { get; set; }

        public Union<string, AlignItems> alignItems { get; set; }

        public Union<string, AlignItems> alignSelf { get; set; }

        public Union<string, All> all { get; set; }

        public string animation { get; set; }

        public string animationDelay { get; set; }

        public Union<string, AnimationDirection> animationDirection { get; set; }

        public string animationDuration { get; set; }

        public Union<string, AnimationFillMode> animationFillMode { get; set; }

        public string animationIterationCount { get; set; }

        public string animationName { get; set; }

        public Union<string, AnimationPlayState> animationPlayState { get; set; }

        public Union<string, TimingFunction> animationTimingFunction { get; set; }

        public Union<string, BackfaceVisibility> backfaceVisibility { get; set; }

        public string background { get; set; }

        public string backgroundAttachment { get; set; }

        public Union<string, BackgroundBlendMode> backgroundBlendMode { get; set; }

        public Union<string, BackgroundClip> backgroundClip { get; set; }

        public string backgroundColor { get; set; }

        public string backgroundImage { get; set; }

        public Union<string, BackgroundClip> backgroundOrigin { get; set; }

        public string backgroundPosition { get; set; }

        public Union<string, BackgroundRepeat> backgroundRepeat { get; set; }

        public string backgroundSize { get; set; }

        public string border { get; set; }

        public string borderBottom { get; set; }

        public string borderBottomColor { get; set; }

        public string borderBottomLeftRadius { get; set; }

        public string borderBottomRightRadius { get; set; }

        public Union<string, BorderStyle> borderBottomStyle { get; set; }

        public Union<string, BorderWidth> borderBottomWidth { get; set; }

        public Union<string, BorderCollapse> borderCollapse { get; set; }

        public string borderColor { get; set; }

        public string borderImage { get; set; }

        public string borderImageOutset { get; set; }

        public Union<string, BorderImageRepeat> borderImageRepeat { get; set; }

        public string borderImageSlice { get; set; }

        public string borderImageSource { get; set; }

        public string borderImageWidth { get; set; }

        public string borderLeft { get; set; }

        public string borderLeftColor { get; set; }

        public Union<string, BorderStyle> borderLeftStyle { get; set; }

        public Union<string, BorderWidth> borderLeftWidth { get; set; }

        public string borderRadius { get; set; }

        public string borderRight { get; set; }

        public string borderRightColor { get; set; }

        public Union<string, BorderStyle> borderRightStyle { get; set; }

        public Union<string, BorderWidth> borderRightWidth { get; set; }

        public string borderSpacing { get; set; }

        public Union<string, BorderStyle> borderStyle { get; set; }

        public string borderTop { get; set; }

        public string borderTopColor { get; set; }

        public string borderTopLeftRadius { get; set; }

        public string borderTopRightRadius { get; set; }

        public Union<string, BorderStyle> borderTopStyle { get; set; }

        public Union<string, BorderWidth> borderTopWidth { get; set; }

        public Union<string, BorderWidth> borderWidth { get; set; }

        public string bottom { get; set; }

        public Union<string, BoxDecorationBreak> boxDecorationBreak { get; set; }

        public string boxShadow { get; set; }

        public Union<string, BoxSizing> boxSizing { get; set; }

        public Union<string, CaptionSide> captionSide { get; set; }

        public Union<string, Clear> clear { get; set; }

        public string clip { get; set; }

        public string clipPath { get; set; }

        public string color { get; set; }

        public string columns { get; set; }

        public string columnCount { get; set; }

        public Union<string, ColumnFill> columnFill { get; set; }

        public string columnGap { get; set; }

        public string columnRule { get; set; }

        public string columnRuleColor { get; set; }

        public Union<string, BorderStyle> columnRuleStyle { get; set; }

        public Union<string, BorderWidth> columnRuleWidth { get; set; }

        public Union<string, ColumnSpan> columnSpan { get; set; }

        public string columnWidth { get; set; }
                      
        public string content { get; set; }
                      
        public string counterIncrement { get; set; }
                      
        public string counterReset { get; set; }

        public Union<string, Float> cssFloat { get; set; }

        public string cssText { get; set; }

        public Union<string, Cursor> cursor { get; set; }

        public Union<string, Direction> direction { get; set; }

        public Union<string, Display> display { get; set; }

        public Union<string, DominantBaseline> dominantBaseline { get; set; }

        public Union<string, EmptyCells> emptyCells { get; set; }

        public string fill { get; set; }

        public string fillOpacity { get; set; }

        public string fillRule { get; set; }

        public string filter { get; set; }

        public string flex { get; set; }

        public string flexBasis { get; set; }

        public Union<string, FlexDirection> flexDirection { get; set; }

        public string flexFlow { get; set; }

        public string flexGrow { get; set; }

        public string flexShrink { get; set; }

        public Union<string, FlexWrap> flexWrap { get; set; }

        public string floodColor { get; set; }

        public string floodOpacity { get; set; }

        public string font { get; set; }

        public string fontFamily { get; set; }

        public string fontFeatureSettings { get; set; }

        public Union<string, FontKerning> fontKerning { get; set; }

        public string fontLanguageOverride { get; set; }

        public string fontSize { get; set; }

        public string fontSizeAdjust { get; set; }

        public Union<string, FontStretch> fontStretch { get; set; }

        public Union<string, FontStyle> fontStyle { get; set; }

        public Union<string, FontSynthesis> fontSynthesis { get; set; }

        public Union<string, FontVariant> fontVariant { get; set; }

        public string fontVariantAlternates { get; set; }

        public Union<string, FontVariantCaps> fontVariantCaps { get; set; }

        public string fontVariantEastAsian { get; set; }

        public Union<string, FontVariantLigatures> fontVariantLigatures { get; set; }

        public string fontVariantNumeric { get; set; }

        public Union<string, FontVariantPosition> fontVariantPosition { get; set; }

        public string fontWeight { get; set; }

        public string grid { get; set; }

        public string gridArea { get; set; }

        public string gridAutoColumns { get; set; }

        public Union<string, GridAutoFlow> gridAutoFlow { get; set; }

        public string gridAutoPosition { get; set; }

        public string gridAutoRows { get; set; }

        public string gridColumn { get; set; }

        public string gridColumnStart { get; set; }

        public string gridColumnEnd { get; set; }

        public string gridRow { get; set; }

        public string gridRowStart { get; set; }

        public string gridRowEnd { get; set; }

        public string gridTemplate { get; set; }

        public string gridTemplateAreas { get; set; }

        public string gridTemplateRows { get; set; }

        public string gridTemplateColumns { get; set; }

        public string height { get; set; }

        public Union<string, Hyphens> hyphens { get; set; }

        public string icon { get; set; }

        public Union<string, ImageRendering> imageRendering { get; set; }

        public string imageResolution { get; set; }

        public string imageOrientation { get; set; }

        public Union<string, ImeMode> imeMode { get; set; }

        public Union<string, JustifyContent> justifyContent { get; set; }

        public string left { get; set; }

        // public int Length{ get; set; }

        public string letterSpacing { get; set; }

        public string lightingColor { get; set; }

        public string lineHeight { get; set; }

        public string listStyle { get; set; }

        public string listStyleImage { get; set; }

        public Union<string, ListStylePosition> listStylePosition { get; set; }

        public Union<string, ListStyleType> listStyleType { get; set; }

        public string margin { get; set; }

        public string marginBottom { get; set; }

        public string marginLeft { get; set; }

        public string marginRight { get; set; }

        public string marginTop { get; set; }

        public string marks { get; set; }

        public string mask { get; set; }

        public Union<string, MaskType> maskType { get; set; }

        public string maxHeight { get; set; }

        public string maxWidth { get; set; }

        public string minHeight { get; set; }

        public string minWidth { get; set; }

        public string mixBlendMode { get; set; }

        public string navDown { get; set; }

        public string navIndex { get; set; }

        public string navLeft { get; set; }

        public string navRight { get; set; }

        public string navUp { get; set; }

        public Union<string, ObjectFit> objectFit { get; set; }

        public string objectPosition { get; set; }

        public string opacity { get; set; }

        public string order { get; set; }

        public string orphans { get; set; }

        public string outline { get; set; }

        public string outlineColor { get; set; }

        public string outlineOffset { get; set; }

        public Union<string, BorderStyle> outlineStyle { get; set; }

        public Union<string, BorderWidth> outlineWidth { get; set; }

        public Union<string, Overflow> overflow { get; set; }

        public string overflowWrap { get; set; }

        public Union<string, Overflow> overflowX { get; set; }

        public Union<string, Overflow> overflowY { get; set; }

        public string overflowClipBox { get; set; }

        public string padding { get; set; }

        public string paddingBottom { get; set; }

        public string paddingLeft { get; set; }

        public string paddingRight { get; set; }

        public string paddingTop { get; set; }

        public Union<string, PageBreak> pageBreakAfter { get; set; }

        public Union<string, PageBreak> pageBreakBefore { get; set; }

        public Union<string, PageBreakInside> pageBreakInside { get; set; }

        public string perspective { get; set; }

        public string perspectiveOrigin { get; set; }

        public Union<string, PointerEvents> pointerEvents { get; set; }

        public Union<string, Position> position { get; set; }

        public string quotes { get; set; }

        public Union<string, Resize> resize { get; set; }

        public string right { get; set; }

        public Union<string, TableLayout> tableLayout { get; set; }

        public string tabSize { get; set; }

        public Union<string, TextAlign> textAlign { get; set; }

        public Union<string, TextAlign> textAlignLast { get; set; }

        public string textCombineHorizontal { get; set; }

        public Union<string, TextDecoration> textDecoration { get; set; }

        public string textDecorationColor { get; set; }

        public Union<string, TextDecorationLine> textDecorationLine { get; set; }

        public Union<string, TextDecorationStyle> textDecorationStyle { get; set; }

        public string textIndent { get; set; }

        public string textOrientation { get; set; }

        public Union<string, TextOverflow> textOverflow { get; set; }

        public Union<string, TextRendering> textRendering { get; set; }

        public string textShadow { get; set; }

        public Union<string, TextTransform> textTransform { get; set; }

        public Union<string, TextUnderlinePosition> textUnderlinePosition { get; set; }

        public string top { get; set; }

        public Union<string, TouchAction> touchAction { get; set; }

        public string transform { get; set; }

        public string transformOrigin { get; set; }

        public Union<string, TransformStyle> transformStyle { get; set; }

        public string transition { get; set; }

        public string transitionDelay { get; set; }

        public string transitionDuration { get; set; }

        public string transitionProperty { get; set; }

        public Union<string, TimingFunction> transitionTimingFunction { get; set; }

        public Union<string, UnicodeBidi> unicodeBidi { get; set; }

        public string unicodeRange { get; set; }

        public Union<string, VerticalAlign> verticalAlign { get; set; }

        public Union<string, Visibility> visibility { get; set; }

        public Union<string, WhiteSpace> whiteSpace { get; set; }

        public string widows { get; set; }

        public string width { get; set; }

        public string willChange { get; set; }

        public Union<string, WordBreak> wordBreak { get; set; }

        public string wordSpacing { get; set; }

        public Union<string, WordWrap> wordWrap { get; set; }

        public Union<string, WritingMode> writingMode { get; set; }

        public string zIndex { get; set; }
    }
    
    public enum BorderCollapse
    {
        none,
        separate,
        collapse,
        inherit
    }

    public enum JustifyContent
    {
        inherit,
        [Name("flex-start")]
        flex_start,
        [Name("flex-end")]
        flex_end,
        center,
        [Name("space-between")]
        space_between,
        [Name("space-around")]
        space_around
    }
    public enum FontKerning
    {
        none,
        normal,
        auto
    }
    public enum FlexWrap
    {
        inherit,
        noWrap,
        wrap,
        [Name("wrap-reverse")]
        wrap_reverse
    }
    public enum AlignContent
    {
        none,
        [Name("flex-start")]
        flex_start,
        [Name("flex-end")]
        flex_end,
        center,
        [Name("space-between")]
        space_between,
        [Name("space-around")]
        space_around,
        stretch,
        inherit
    }
    public enum BorderStyle
    {
        none,
        hidden,
        dotted,
        dashed,
        solid,
        [Name("double")]
        @double,
        groove,
        ridge,
        inset,
        outset,
        inherit
    }
    public enum AnimationDirection
    {
        none,
        normal,
        reverse,
        alternate,
        [Name("alternate-reverse")]
        alternate_reverse
    }
    public enum BorderWidth
    {
        thin,
        medium,
        thick,
        inherit
    }
    public enum AnimationFillMode
    {
        none,
        forwards,
        backwards,
        both
    }
    public enum BackgroundRepeat
    {
        [Name("repeat-x")]
        repeat_x,
        
        [Name("repeat-y")]
        repeat_y,
        
        repeat,
        space,
        round,
        
        [Name("no-repeat")]
        no_repeat
    }
    public enum BackgroundClip
    {
        none,
        
        [Name("border-box")]
        border_box,
        
        [Name("padding-box")]
        padding_box,
        
        [Name("content-box")]
        content_box,
        
        inherit
    }
    public enum AnimationPlayState
    {
        none,
        running,
        pdaused
    }
    public enum BorderImageRepeat
    {
        stretch,
        repeat,
        round,
        inherit
    }
    public enum BackgroundBlendMode
    {
        screen,
        overlay,
        darken,
        lighten,
        
        [Name("color-dodge")]
        color_dodge,
        
        [Name("color-burn")]
        color_burn,
        
        [Name("hard-light")]
        hard_light,
        
        [Name("soft-light")]
        soft_light,
        
        difference,
        exclusion,
        hue,
        saturation,
        color,
        luminosity,
        normal
    }

    public enum BackfaceVisibility
    {
        inherit,
        visible,
        hidden
    }
    public enum TimingFunction
    {
        none,
        ease,
        
        [Name("ease-in")]
        ease_in,
        
        [Name("ease-out")]
        ease_out,
        
        [Name("ease-in-out")]
        ease_in_out,
        
        linear,
        
        [Name("step-start")]
        step_start,
        
        [Name("step-end")]
        step_end
    }
    public enum AlignItems
    {
        /// <summary>
        ///
        /// </summary>
        none,
        /// <summary>
        /// The cross-start margin edge of the flex item is flushed with the cross-start edge of the line.
        /// </summary>
        [Name("flex-start")]
        flex_start,
        /// <summary>
        /// The cross-end margin edge of the flex item is flushed with the cross-end edge of the line.
        /// </summary>
        [Name("flex-end")]
        flex_end,
        /// <summary>
        /// The flex item's margin box is centered within the line on the cross-axis. If the cross-size of the item is larger than the flex container, it will overflow equally in both directions.
        /// </summary>
        center,
        /// <summary>
        /// All flex items are aligned such that their baselines align. The item with the largest distance between its cross-start margin edge and its baseline is flushed with the cross-start edge of the line.
        /// </summary>
        baseline,
        /// <summary>
        /// Flex items are stretched such as the cross-size of the item's margin box is the same as the line while respecting width and height constraints.
        /// </summary>
        stretch,
        /// <summary>
        ///
        /// </summary>
        inherit
    }

    public enum FlexDirection
    {
        /// <summary>
        /// 
        /// </summary>
        inherit,
        /// <summary>
        /// The flex container's main-axis is defined to be the same as the text direction. The main-start and main-end points are the same as the content direction.
        /// </summary>
        row,
        /// <summary>
        /// Behaves the same as row but the main-start and main-end points are permuted.
        /// </summary>
        [Name("row-reverse")] 
        row_reverse,
        /// <summary>
        /// The flex container's main-axis is the same as the block-axis. The main-start and main-end points are the same as the before and after points of the writing-mode.
        /// </summary>
        column,
        /// <summary>
        /// Behaves the same as column but the main-start and main-end are permuted.
        /// </summary>
        [Name("column-reverse")] 
        column_reverse,
    }

    public enum Display
    {
        /// <summary>
        /// Turns off the display of an element (it has no effect on layout); all descendant elements also have their display turned off. The document is rendered as though the element did not exist.
        /// To render an element box's dimensions, yet have its contents be invisible, see the visibility property.
        /// </summary>
        none,
        /// <summary>
        /// The element generates one or more inline element boxes.
        /// </summary>
        inline,
        /// <summary>The element generates a block element box.</summary>
        block,
        /// <summary>
        /// The element generates a block box for the content and a separate list-item inline box.
        /// </summary>
        [Name("list-item")] 
        list_item,
        /// <summary>
        /// The element generates a block element box that will be flowed with surrounding content as if it were a single inline box (behaving much like a replaced element would)
        /// </summary>
        [Name("inline-block")] 
        inline_block,
        /// <summary>
        /// The inline-table value does not have a direct mapping in HTML. It behaves like a &lt;table&gt; HTML element, but as an inline box, rather than a block-level box. Inside the table box is a block-level context.
        /// </summary>
        [Name("inline-table")] 
        inline_table,
        /// <summary>
        /// Behaves like the &lt;table&gt; HTML element. It defines a block-level box.
        /// </summary>
        table,
        /// <summary>Behaves like the &lt;caption&gt; HTML element.</summary>
        [Name("table-caption")] 
        table_caption,
        
        /// <summary>Behaves like the &lt;td&gt; HTML element</summary>
        [Name("table-cell")] 
        table_cell,
        /// <summary>
        /// These elements behave like the corresponding &lt;col&gt; HTML elements.
        /// </summary>
        [Name("table-column")] 
        table_column,
        
        /// <summary>
        /// These elements behave like the corresponding &lt;colgroup&gt; HTML elements.
        /// </summary>
        [Name("table-column-group")] 
        table_column_group,
        /// <summary>
        /// These elements behave like the corresponding &lt;tfoot&gt; HTML elements
        /// </summary>
        [Name("table-footer-group")] 
        table_footer_group,
        /// <summary>
        /// These elements behave like the corresponding &lt;thead&gt; HTML elements
        /// </summary>
        [Name("table-header-group")] 
        table_header_group,
        /// <summary>Behaves like the &lt;tr&gt; HTML element</summary>
        [Name("table-row")] 
        table_row,
        /// <summary>
        /// These elements behave like the corresponding &lt;tbody&gt; HTML elements
        /// </summary>
        [Name("table-row-group")] 
        table_row_group,
        /// <summary>
        /// The element behaves like a block element and lays out its content according to the flexbox model.
        /// </summary>
        [EnumMember(Value = "flex")] 
        flex,
        /// <summary>
        /// The element behaves like an inline element and lays out its content according to the flexbox model.
        /// </summary>
        [Name("inline-flex")] 
        inline_flex,
        /// <summary>
        /// The element behaves like a block element and lay out its content according to the grid model.
        /// </summary>
        grid,
        /// <summary>
        /// The element behaves like an inline element and lay out its content according to the grid model.
        /// </summary>
        [Name("inline-grid")] 
        inline_grid,
    }


    public enum Overflow
    {
        inherit,
        visible,
        hidden,
        scroll,
        auto
    }

    public enum PageBreak
    {
        inherit,
        auto,
        always,
        avoid,
        left,
        right
    }

    public enum PageBreakInside
    {
        inherit,
        auto,
        avoid
    }

    public enum PointerEvents
    {
        inherit,
        auto,
        none,
        visiblePainted,
        visibleFill,
        visibleStroke,
        visible,
        painted,
        fill,
        stroke,
        all
    }

    public enum Position
    {
        inherit,
        @static,
        relative,
        absolute,
        @fixed,
        sticky
    }


    public enum Resize
    {
        inherit,
        none,
        both,
        horizontal,
        vertical
    }

    public enum TableLayout
    {
        inherit,
        auto,
        @fixed
    }


    public enum TextAlign
    {
        inherit,
        start,
        end,
        left,
        right,
        center,
        justify,
        
        [Name("match-parent")]
        match_parent,
        
        [Name("start end")]
        start_end
    }

    public enum TextDecoration
    {
        inherit,
        none,
        underline,
        overline,
        [Name("line-through")]
        line_through
    }


    public enum TextDecorationLine
    {
        inherit,
        none,
        underline,
        overline,
        
        [Name("line-through")]
        line_through,
        
        blink
    }


    public enum TextDecorationStyle
    {
        inherit,
        solid,
        @double,
        dotted,
        dashed,
        wavy
    }


    public enum TextOverflow
    {
        inherit,
        clip,
        ellipsis
    }

    public enum TextRendering
    {
        inherit,
        auto,
        optimizeSpeed,
        optimizeLegibility,
        geometricPrecision
    }

    public enum TextTransform
    {
        inherit,
        capitalize,
        upperCase,
        lowerCase,
        none,
        [Name("full-width")]
        full_width
    }

    public enum TextUnderlinePosition
    {
        inherit,
        auto,
        under,
        left,
        right,
        
        [Name("under left")]
        under_left,
        
        [Name("right under")]
        right_under
    }

    public enum TouchAction
    {
        inherit,
        auto,
        none,
        
        [Name("pan-x")]
        pan_x,
        
        [Name("pan-y")]
        pan_y
    }

    public enum TransformStyle
    {
        inherit,
        
        [Name("preserve-3d")]
        preserve_3d,
        flat
    }

    public enum UnicodeBidi
    {
        inherit,
        normal,
        embed,
        [Name("bidi-override")]
        bidi_override,
        isolate,
        [Name("isolate-override")]
        isolate_override,
        plainText
    }

    public enum VerticalAlign
    {
        inherit,
        baseline,
        sub,
        super,
        [Name("text-top")]
        text_top,
        
        [Name("text-bottom")]
        text_bottom,
        middle,
        top,
        bottom
    }

    public enum Visibility
    {
        inherit,
        visible,
        hidden,
        collapse
    }

    public enum WhiteSpace
    {
        inherit,
        normal,
        noWrap,
        pre,
        
        [Name("pre-wrap")]
        pre_wrap,
        
        [Name("pre-line")]
        pre_line
    }

    public enum WordBreak
    {
        inherit,
        normal,
        
        [Name("break-all")]
        break_all,
        
        [Name("keep-all")]
        keep_all
    }

    public enum WordWrap
    {
        inherit,
        normal,
        
        [Name("break-all")]
        break_all
    }

    public enum WritingMode
    {
        inherit,
        
        [Name("horizontal-tb")]
        horizontal_tb,
        
        [Name("rl-tb")]
        rl_tb,
        
        [Name("vertical-lr")]
        vertical_lr,
        
        [Name("vertical-rl")]
        vertical_rl,
        
        [Name("bt-rl")]
        bt_rl,
        
        [Name("bt-lr")]
        bt_lr,
        
        [Name("lr-bt")]
        lr_bt,
        
        [Name("rl-bt")]
        rl_bt
    }

    public enum BoxDecorationBreak
    {
        none,
        slice,
        clone
    }



    public enum BoxSizing
    {
        none,
        
        [Name("border-box")]
        border_box,
        
        [Name("padding-box")]
        padding_box,
        
        [Name("content-box")]
        content_box,
        
        inherit
    }



    public enum CaptionSide
    {
        none,
        top,
        bottom,
        inherit
    }



    public enum Clear
    {
        none,
        left,
        right,
        both,
        inherit
    }



    public enum ColumnFill
    {
        auto,
        balance,
        inherit
    }



    public enum ColumnSpan
    {
        none,
        all,
        inherit
    }



    public enum Float
    {
        none,
        left,
        right
    }


    public enum Cursor
    {
        auto,
        @default,
        none,
        [Name("context-menu")]
        context_menu,
        
        help,
        pointer,
        progress,
        wait,
        cell,
        crossHair,
        text,
        
        [Name("vertical-text")]
        vertical_text,
        
        alias,
        copy,
        move,
        [Name("no-drop")]
        no_drop,
        
        [Name("not-allowed")]
        not_allowed,
        [Name("all-scroll")]
        all_scroll,
        [Name("col-resize")]
        col_resize,
        [Name("row-resize")]
        row_resize,
        [Name("n-resize")]
        NorthResize,
        [Name("e-resize")]
        EastResize,
        [Name("s-resize")]
        SouthResize,
        [Name("w-resize")]
        WestResize,
        [Name("ne-resize")]
        NorthEastResize,
        [Name("nw-resize")]
        NorthWestResize,
        [Name("se-resize")]
        SouthEastResize,
        [Name("sw-resize")]
        SouthWestResize,
        [Name("ew-resize")]
        EastWestResize,
        [Name("ns-resize")]
        NorthSouthResize,
        [Name("nesw-resize")]
        NorthEastSouthWestResize,
        [Name("nwse-resize")]
        NorthWestSouthEastResize,
        [Name("zoom-in")]
        ZoomIn,
        [Name("zoom-out")]
        ZoomOut,
        Grab,
        Grabbing
    }



    public enum Direction
    {
        inherit,
        ltr,
        rtl
    }



    public enum DominantBaseline
    {
        Auto,
        [Name("use-script")]
        UseScript,
        [Name("no-change")]
        NoChange,
        [Name("reset-size")]
        ResetSize,
        Alphabetic,
        Hanging,
        Ideographic,
        Mathematical,
        Central,
        Middle,
        [Name("text-after-edge")]
        TextAfterEdge,
        [Name("text-before-edge")]
        TextBeforeEdge
    }


    public enum EmptyCells
    {
        inherit,
        Show,
        Hide
    }



    public enum FontStretch
    {
        inherit,
        normal,
        [Name("semi-condensed")]
        SemiCondensed,
        Condensed,
        [Name("extra-condensed")]
        ExtraCondensed,
        [Name("ultra-condensed")]
        UltraCondensed,
        [Name("semi-expanded")]
        SemiExpanded,
        Expanded,
        [Name("extra-expanded")]
        ExtraExpanded,
        [Name("ultra-expanded")]
        UltraExpanded
    }


    public enum FontStyle
    {
        inherit,
        normal,
        italic,
        oblique
    }



    public enum FontSynthesis
    {
        none,
        weight,
        style,
        [Name("weight style")]
        weight_style
    }



    public enum FontVariant
    {
        inherit,
        normal,
        [Name("small-caps")]
        small_caps
    }




    public enum FontVariantCaps
    {
        Inherited,
        normal,
        [Name("small-caps")]
        SmallCaps,
        [Name("all-small-caps")]
        AllSmallCaps,
        [Name("petite-caps")]
        PetiteCaps,
        [Name("all-petite-caps")]
        AllPetiteCaps,
        Unicase,
        [Name("titling-caps")]
        TitlingCaps
    }


    public enum FontVariantLigatures
    {
        inherit,
        normal,
        none,
        [Name("common-ligatures")]
        CommonLigatures,
        [Name("no-common-ligatures")]
        NoCommonLigatures,
        [Name("discretionary-ligatures")]
        DiscretionaryLigatures,
        [Name("no-discretionary-ligatures")]
        NoDiscretionaryLigatures,
        [Name("historical-ligatures")]
        HistoricalLigatures,
        [Name("no-historical-ligatures")]
        NoHistoricalLigatures,
        Contextual,
        [Name("no-contextual")]
        NoContextual
    }



    public enum FontVariantPosition
    {
        inherit,
        normal,
        Sub,
        Super
    }

    public enum GridAutoFlow
    {
        inherit,
        none,
        Rows,
        Columns,
        Dense,
        Sparse
    }


    public enum Hyphens
    {
        inherit,
        none,
        Manual,
        Auto
    }

    public enum All
    {
        none,
        Initial,
        Unset,
        Inherit
    }

    public enum ImageRendering
    {
        inherit,
        Auto,
        [Name("crisp-edges")]
        CrispEdges,
        Pixelated
    }


    public enum ImeMode
    {
        inherit,
        Auto,
        normal,
        Active,
        Inactive,
        Disabled
    }


    public enum ListStylePosition
    {
        inherit,
        Outside,
        Inside
    }



    public enum ListStyleType
    {
        inherit,
        Disc,
        Circle,
        Square,
        Decimal,
        [Name("decimal-leading-zero")]
        DecimalLeadingZero,
        [Name("lower-roman")]
        LowerRoman,
        [Name("upper-roman")]
        UpperRoman,
        [Name("lower-greek")]
        LowerGreek,
        [Name("lower-latin")]
        LowerLatin,
        [Name("upper-latin")]
        UpperLatin,
        Armenian,
        Georgian,
        [Name("lower-alpha")]
        LowerAlpha,
        [Name("upper-alpha")]
        UpperAlpha,
        None
    }




    public enum MaskType
    {
        inherit,
        Luminance,
        Alpha
    }




    public enum ObjectFit
    {
        inherit,
        Fill,
        Contain,
        Cover,
        none,
        [Name("scale-down")]
        ScaleDown
    }

}