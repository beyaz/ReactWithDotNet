
using System;
using System.Runtime.Serialization;

namespace ReactDotNet
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
    public sealed class CSSStyleDeclaration
    {
		public Union<string, AlignContent> AlignContent{ get; set; }

		public Union<string, AlignItems> AlignItems{ get; set; }

		public Union<string, AlignItems> AlignSelf{ get; set; }

		public Union<string, All> All{ get; set; }

		public string Animation{ get; set; }

		public string AnimationDelay{ get; set; }

		public Union<string, AnimationDirection> AnimationDirection{ get; set; }

		public string AnimationDuration{ get; set; }

		public Union<string, AnimationFillMode> AnimationFillMode{ get; set; }

		public string AnimationIterationCount{ get; set; }

		public string AnimationName{ get; set; }

		public Union<string, AnimationPlayState> AnimationPlayState{ get; set; }

		public Union<string, TimingFunction> AnimationTimingFunction{ get; set; }

		public Union<string, BackfaceVisibility> BackfaceVisibility{ get; set; }

		public string Background{ get; set; }

		public string BackgroundAttachment{ get; set; }

		public Union<string, BackgroundBlendMode> BackgroundBlendMode{ get; set; }

		public Union<string, BackgroundClip> BackgroundClip{ get; set; }

		public string BackgroundColor{ get; set; }

		public string BackgroundImage{ get; set; }

		public Union<string, BackgroundClip> BackgroundOrigin{ get; set; }

		public string BackgroundPosition{ get; set; }

		public Union<string, BackgroundRepeat> BackgroundRepeat{ get; set; }

		public string BackgroundSize{ get; set; }

		public string Border{ get; set; }

		public string BorderBottom{ get; set; }

		public string BorderBottomColor{ get; set; }

		public string BorderBottomLeftRadius{ get; set; }

		public string BorderBottomRightRadius{ get; set; }

		public Union<string, BorderStyle> BorderBottomStyle{ get; set; }

		public Union<string, BorderWidth> BorderBottomWidth{ get; set; }

		public Union<string, BorderCollapse> BorderCollapse{ get; set; }

		public string BorderColor{ get; set; }

		public string BorderImage{ get; set; }

		public string BorderImageOutset{ get; set; }

		public Union<string, BorderImageRepeat> BorderImageRepeat{ get; set; }

		public string BorderImageSlice{ get; set; }

		public string BorderImageSource{ get; set; }

		public string BorderImageWidth{ get; set; }

		public string BorderLeft{ get; set; }

		public string BorderLeftColor{ get; set; }

		public Union<string, BorderStyle> BorderLeftStyle{ get; set; }

		public Union<string, BorderWidth> BorderLeftWidth{ get; set; }

		public string BorderRadius{ get; set; }

		public string BorderRight{ get; set; }

		public string BorderRightColor{ get; set; }

		public Union<string, BorderStyle> BorderRightStyle{ get; set; }

		public Union<string, BorderWidth> BorderRightWidth{ get; set; }

		public string BorderSpacing{ get; set; }

		public Union<string, BorderStyle> BorderStyle{ get; set; }

		public string BorderTop{ get; set; }

		public string BorderTopColor{ get; set; }

		public string BorderTopLeftRadius{ get; set; }

		public string BorderTopRightRadius{ get; set; }

		public Union<string, BorderStyle> BorderTopStyle{ get; set; }

		public Union<string, BorderWidth> BorderTopWidth{ get; set; }

		public Union<string, BorderWidth> BorderWidth{ get; set; }

		public string Bottom{ get; set; }

		public Union<string, BoxDecorationBreak> BoxDecorationBreak{ get; set; }

		public string BoxShadow{ get; set; }

		public Union<string, BoxSizing> BoxSizing{ get; set; }

		public Union<string, CaptionSide> CaptionSide{ get; set; }

		public Union<string, Clear> Clear{ get; set; }

		public string Clip{ get; set; }

		public string ClipPath{ get; set; }

		public string Color{ get; set; }

		public string Columns{ get; set; }

		public string ColumnCount{ get; set; }

		public Union<string, ColumnFill> ColumnFill{ get; set; }

		public string ColumnGap{ get; set; }

		public string ColumnRule{ get; set; }

		public string ColumnRuleColor{ get; set; }

		public Union<string, BorderStyle> ColumnRuleStyle{ get; set; }

		public Union<string, BorderWidth> ColumnRuleWidth{ get; set; }

		public Union<string, ColumnSpan> ColumnSpan{ get; set; }

		public string ColumnWidth{ get; set; }

		public string Content{ get; set; }

		public string CounterIncrement{ get; set; }

		public string CounterReset{ get; set; }

		public Union<string, Float> CssFloat{ get; set; }

		public string CssText{ get; set; }

		public Union<string, Cursor> Cursor{ get; set; }

		public Union<string, Direction> Direction{ get; set; }

		public Union<string, Display> Display{ get; set; }

		public Union<string, DominantBaseline> DominantBaseline{ get; set; }

		public Union<string, EmptyCells> EmptyCells{ get; set; }

		public string Fill{ get; set; }

		public string FillOpacity{ get; set; }

		public string FillRule{ get; set; }

		public string Filter{ get; set; }

		public string Flex{ get; set; }

		public string FlexBasis{ get; set; }

		public Union<string, FlexDirection> FlexDirection{ get; set; }

		public string FlexFlow{ get; set; }

		public string FlexGrow{ get; set; }

		public string FlexShrink{ get; set; }

		public Union<string, FlexWrap> FlexWrap{ get; set; }

		public string FloodColor{ get; set; }

		public string FloodOpacity{ get; set; }

		public string Font{ get; set; }

		public string FontFamily{ get; set; }

		public string FontFeatureSettings{ get; set; }

		public Union<string, FontKerning> FontKerning{ get; set; }

		public string FontLanguageOverride{ get; set; }

		public string FontSize{ get; set; }

		public string FontSizeAdjust{ get; set; }

		public Union<string, FontStretch> FontStretch{ get; set; }

		public Union<string, FontStyle> FontStyle{ get; set; }

		public Union<string, FontSynthesis> FontSynthesis{ get; set; }

		public Union<string, FontVariant> FontVariant{ get; set; }

		public string FontVariantAlternates{ get; set; }

		public Union<string, FontVariantCaps> FontVariantCaps{ get; set; }

		public string FontVariantEastAsian{ get; set; }

		public Union<string, FontVariantLigatures> FontVariantLigatures{ get; set; }

		public string FontVariantNumeric{ get; set; }

		public Union<string, FontVariantPosition> FontVariantPosition{ get; set; }

		public string FontWeight{ get; set; }

		public string Grid{ get; set; }

		public string GridArea{ get; set; }

		public string GridAutoColumns{ get; set; }

		public Union<string, GridAutoFlow> GridAutoFlow{ get; set; }

		public string GridAutoPosition{ get; set; }

		public string GridAutoRows{ get; set; }

		public string GridColumn{ get; set; }

		public string GridColumnStart{ get; set; }

		public string GridColumnEnd{ get; set; }

		public string GridRow{ get; set; }

		public string GridRowStart{ get; set; }

		public string GridRowEnd{ get; set; }

		public string GridTemplate{ get; set; }

		public string GridTemplateAreas{ get; set; }

		public string GridTemplateRows{ get; set; }

		public string GridTemplateColumns{ get; set; }

		public string Height{ get; set; }

		public Union<string, Hyphens> Hyphens{ get; set; }

		public string Icon{ get; set; }

		public Union<string, ImageRendering> ImageRendering{ get; set; }

		public string ImageResolution{ get; set; }

		public string ImageOrientation{ get; set; }

		public Union<string, ImeMode> ImeMode{ get; set; }

		public Union<string, JustifyContent> JustifyContent{ get; set; }

		public string Left{ get; set; }

		// public int Length{ get; set; }

		public string LetterSpacing{ get; set; }

		public string LightingColor{ get; set; }

		public string LineHeight{ get; set; }

		public string ListStyle{ get; set; }

		public string ListStyleImage{ get; set; }

		public Union<string, ListStylePosition> ListStylePosition{ get; set; }

		public Union<string, ListStyleType> ListStyleType{ get; set; }

		public string Margin{ get; set; }

		public string MarginBottom{ get; set; }

		public string MarginLeft{ get; set; }

		public string MarginRight{ get; set; }

		public string MarginTop{ get; set; }

		public string Marks{ get; set; }

		public string Mask{ get; set; }

		public Union<string, MaskType> MaskType{ get; set; }

		public string MaxHeight{ get; set; }

		public string MaxWidth{ get; set; }

		public string MinHeight{ get; set; }

		public string MinWidth{ get; set; }

		public string MixBlendMode{ get; set; }

		public string NavDown{ get; set; }

		public string NavIndex{ get; set; }

		public string NavLeft{ get; set; }

		public string NavRight{ get; set; }

		public string NavUp{ get; set; }

		public Union<string, ObjectFit> ObjectFit{ get; set; }

		public string ObjectPosition{ get; set; }

		public string Opacity{ get; set; }

		public string Order{ get; set; }

		public string Orphans{ get; set; }

		public string Outline{ get; set; }

		public string OutlineColor{ get; set; }

		public string OutlineOffset{ get; set; }

		public Union<string, BorderStyle> OutlineStyle{ get; set; }

		public Union<string, BorderWidth> OutlineWidth{ get; set; }

		public Union<string, Overflow> Overflow{ get; set; }

		public string OverflowWrap{ get; set; }

		public Union<string, Overflow> OverflowX{ get; set; }

		public Union<string, Overflow> OverflowY{ get; set; }

		public string OverflowClipBox{ get; set; }

		public string Padding{ get; set; }

		public string PaddingBottom{ get; set; }

		public string PaddingLeft{ get; set; }

		public string PaddingRight{ get; set; }

		public string PaddingTop{ get; set; }

		public Union<string, PageBreak> PageBreakAfter{ get; set; }

		public Union<string, PageBreak> PageBreakBefore{ get; set; }

		public Union<string, PageBreakInside> PageBreakInside{ get; set; }

		public string Perspective{ get; set; }

		public string PerspectiveOrigin{ get; set; }

		public Union<string, PointerEvents> PointerEvents{ get; set; }

		public Union<string, Position> Position{ get; set; }

		public string Quotes{ get; set; }

		public Union<string, Resize> Resize{ get; set; }

		public string Right{ get; set; }

		public Union<string, TableLayout> TableLayout{ get; set; }

		public string TabSize{ get; set; }

		public Union<string, TextAlign> TextAlign{ get; set; }

		public Union<string, TextAlign> TextAlignLast{ get; set; }

		public string TextCombineHorizontal{ get; set; }

		public Union<string, TextDecoration> TextDecoration{ get; set; }

		public string TextDecorationColor{ get; set; }

		public Union<string, TextDecorationLine> TextDecorationLine{ get; set; }

		public Union<string, TextDecorationStyle> TextDecorationStyle{ get; set; }

		public string TextIndent{ get; set; }

		public string TextOrientation{ get; set; }

		public Union<string, TextOverflow> TextOverflow{ get; set; }

		public Union<string, TextRendering> TextRendering{ get; set; }

		public string TextShadow{ get; set; }

		public Union<string, TextTransform> TextTransform{ get; set; }

		public Union<string, TextUnderlinePosition> TextUnderlinePosition{ get; set; }

		public string Top{ get; set; }

		public Union<string, TouchAction> TouchAction{ get; set; }

		public string Transform{ get; set; }

		public string TransformOrigin{ get; set; }

		public Union<string, TransformStyle> TransformStyle{ get; set; }

		public string Transition{ get; set; }

		public string TransitionDelay{ get; set; }

		public string TransitionDuration{ get; set; }

		public string TransitionProperty{ get; set; }

		public Union<string, TimingFunction> TransitionTimingFunction{ get; set; }

		public Union<string, UnicodeBidi> UnicodeBidi{ get; set; }

		public string UnicodeRange{ get; set; }

		public Union<string, VerticalAlign> VerticalAlign{ get; set; }

		public Union<string, Visibility> Visibility{ get; set; }

		public Union<string, WhiteSpace> WhiteSpace{ get; set; }

		public string Widows{ get; set; }

		public string Width{ get; set; }

		public string WillChange{ get; set; }

		public Union<string, WordBreak> WordBreak{ get; set; }

		public string WordSpacing{ get; set; }

		public Union<string, WordWrap> WordWrap{ get; set; }

		public Union<string, WritingMode> WritingMode{ get; set; }

		public string ZIndex{ get; set; }
	}
	public enum BorderCollapse
	{
		None,
		Separate,
		Collapse,
		Inherit
	}

	public enum JustifyContent
    {
        Inherit,
        [Name("flex-start")]
        FlexStart,
        [Name("flex-end")]
        FlexEnd,
        Center,
        [Name("space-between")]
        SpaceBetween,
        [Name("space-around")]
        SpaceAround
    }
	public enum FontKerning
	{
		None,
		Normal,
		Auto
	}
	public enum FlexWrap
	{
		Inherit,
		NoWrap,
		Wrap,
		[Name("wrap-reverse")]
		WrapReverse
	}
	public enum AlignContent
    {
        None,
        [Name("flex-start")]
        FlexStart,
        [Name("flex-end")]
        FlexEnd,
        Center,
        [Name("space-between")]
        SpaceBetween,
        [Name("space-around")]
        SpaceAround,
        Stretch,
        Inherit
    }
	public enum BorderStyle
	{
		None,
		Hidden,
		Dotted,
		Dashed,
		Solid,
		Double,
		Groove,
		Ridge,
		Inset,
		Outset,
		Inherit
	}
	public enum AnimationDirection
	{
		None,
		Normal,
		Reverse,
		Alternate,
		[Name("alternate-reverse")]
		AlternateReverse
	}
	public enum BorderWidth
	{
		Thin,
		Medium,
		Thick,
		Inherit
	}
	public enum AnimationFillMode
	{
		None,
		Forwards,
		Backwards,
		Both
	}
	public enum BackgroundRepeat
	{
		[Name("repeat-x")]
		RepeatX,
		[Name("repeat-y")]
		RepeatY,
		Repeat,
		Space,
		round,
		[Name("no-repeat")]
		NoRepeat
	}
	public enum BackgroundClip
	{
		None,
		[Name("border-box")]
		BorderBox,
		[Name("padding-box")]
		PaddingBox,
		[Name("content-box")]
		ContentBox,
		Inherit
	}
	public enum AnimationPlayState
	{
		None,
		Running,
		Paused
	}
	public enum BorderImageRepeat
	{
		Stretch,
		Repeat,
		Round,
		Inherit
	}
	public enum BackgroundBlendMode
	{
		Screen,
		Overlay,
		Darken,
		Lighten,
		[Name("color-dodge")]
		ColorDodge,
		[Name("color-burn")]
		ColorBurn,
		[Name("hard-light")]
		HardLight,
		[Name("soft-light")]
		SoftLight,
		Difference,
		Exclusion,
		Hue,
		Saturation,
		Color,
		Luminosity,
		Normal
	}

	public enum BackfaceVisibility
	{
		Inherit,
		Visible,
		Hidden
	}
	public enum TimingFunction
	{
		None,
		Ease,
		[Name("ease-in")]
		EaseIn,
		[Name("ease-out")]
		EaseOut,
		[Name("ease-in-out")]
		EaseInOut,
		Linear,
		[Name("step-start")]
		StepStart,
		[Name("step-end")]
		StepEnd
	}
	public enum AlignItems
    {
        /// <summary>
        ///
        /// </summary>
        None,
        /// <summary>
        /// The cross-start margin edge of the flex item is flushed with the cross-start edge of the line.
        /// </summary>
        [Name("flex-start")]
        FlexStart,
        /// <summary>
        /// The cross-end margin edge of the flex item is flushed with the cross-end edge of the line.
        /// </summary>
        [Name("flex-end")]
        FlexEnd,
        /// <summary>
        /// The flex item's margin box is centered within the line on the cross-axis. If the cross-size of the item is larger than the flex container, it will overflow equally in both directions.
        /// </summary>
        Center,
        /// <summary>
        /// All flex items are aligned such that their baselines align. The item with the largest distance between its cross-start margin edge and its baseline is flushed with the cross-start edge of the line.
        /// </summary>
        Baseline,
        /// <summary>
        /// Flex items are stretched such as the cross-size of the item's margin box is the same as the line while respecting width and height constraints.
        /// </summary>
        Stretch,
        /// <summary>
        ///
        /// </summary>
        Inherit
    }

    public enum FlexDirection
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,
        /// <summary>
        /// The flex container's main-axis is defined to be the same as the text direction. The main-start and main-end points are the same as the content direction.
        /// </summary>
        Row,
        /// <summary>
        /// Behaves the same as row but the main-start and main-end points are permuted.
        /// </summary>
        [Name("row-reverse")] RowReverse,
        /// <summary>
        /// The flex container's main-axis is the same as the block-axis. The main-start and main-end points are the same as the before and after points of the writing-mode.
        /// </summary>
        Column,
        /// <summary>
        /// Behaves the same as column but the main-start and main-end are permuted.
        /// </summary>
        [Name("column-reverse")] ColumnReverse,
    }

    public enum Display
    {
        /// <summary>
        /// Turns off the display of an element (it has no effect on layout); all descendant elements also have their display turned off. The document is rendered as though the element did not exist.
        /// To render an element box's dimensions, yet have its contents be invisible, see the visibility property.
        /// </summary>
        None,
        /// <summary>
        /// The element generates one or more inline element boxes.
        /// </summary>
        Inline,
        /// <summary>The element generates a block element box.</summary>
        Block,
        /// <summary>
        /// The element generates a block box for the content and a separate list-item inline box.
        /// </summary>
        [Name("list-item")] ListItem,
        /// <summary>
        /// The element generates a block element box that will be flowed with surrounding content as if it were a single inline box (behaving much like a replaced element would)
        /// </summary>
        [Name("inline-block")] InlineBlock,
        /// <summary>
        /// The inline-table value does not have a direct mapping in HTML. It behaves like a &lt;table&gt; HTML element, but as an inline box, rather than a block-level box. Inside the table box is a block-level context.
        /// </summary>
        [Name("inline-table")] InlineTable,
        /// <summary>
        /// Behaves like the &lt;table&gt; HTML element. It defines a block-level box.
        /// </summary>
        Table,
        /// <summary>Behaves like the &lt;caption&gt; HTML element.</summary>
        [Name("table-caption")] TableCaption,
        /// <summary>Behaves like the &lt;td&gt; HTML element</summary>
        [Name("table-cell")] TableCell,
        /// <summary>
        /// These elements behave like the corresponding &lt;col&gt; HTML elements.
        /// </summary>
        [Name("table-column")] TableColumn,
        /// <summary>
        /// These elements behave like the corresponding &lt;colgroup&gt; HTML elements.
        /// </summary>
        [Name("table-column-group")] TableColumnGroup,
        /// <summary>
        /// These elements behave like the corresponding &lt;tfoot&gt; HTML elements
        /// </summary>
        [Name("table-footer-group")] TableFooterGroup,
        /// <summary>
        /// These elements behave like the corresponding &lt;thead&gt; HTML elements
        /// </summary>
        [Name("table-header-group")] TableHeaderGroup,
        /// <summary>Behaves like the &lt;tr&gt; HTML element</summary>
        [Name("table-row")] TableRow,
        /// <summary>
        /// These elements behave like the corresponding &lt;tbody&gt; HTML elements
        /// </summary>
        [Name("table-row-group")] TableRowGroup,
        /// <summary>
        /// The element behaves like a block element and lays out its content according to the flexbox model.
        /// </summary>
        [EnumMember(Value = "flex")] Flex,
        /// <summary>
        /// The element behaves like an inline element and lays out its content according to the flexbox model.
        /// </summary>
        [Name("inline-flex")] InlineFlex,
        /// <summary>
        /// The element behaves like a block element and lay out its content according to the grid model.
        /// </summary>
        Grid,
        /// <summary>
        /// The element behaves like an inline element and lay out its content according to the grid model.
        /// </summary>
        [Name("inline-grid")] InlineGrid,
    }


	public enum Overflow
	{
		Inherit,
		Visible,
		Hidden,
		Scroll,
		Auto
	}

	public enum PageBreak
	{
		Inherit,
		Auto,
		Always,
		Avoid,
		Left,
		Right
	}

	public enum PageBreakInside
	{
		Inherit,
		Auto,
		Avoid
	}

	public enum PointerEvents
	{
		Inherit,
		Auto,
		None,
		VisiblePainted,
		VisibleFill,
		VisibleStroke,
		Visible,
		Painted,
		Fill,
		Stroke,
		All
	}

	public enum Position
	{
		Inherit,
		Static,
		Relative,
		Absolute,
		Fixed,
		Sticky
	}


	public enum Resize
	{
		Inherit,
		None,
		Both,
		Horizontal,
		Vertical
	}

	public enum TableLayout
	{
		Inherit,
		Auto,
		Fixed
	}


	public enum TextAlign
	{
		Inherit,
		Start,
		End,
		Left,
		Right,
		Center,
		Justify,
		[Name("match-parent")]
		MatchParent,
		[Name("start end")]
		StartEnd
	}

	public enum TextDecoration
	{
		Inherit,
		None,
		Underline,
		Overline,
		[Name("line-through")]
		LineThrough
	}


	public enum TextDecorationLine
	{
		Inherit,
		none,
		Underline,
		Overline,
		[Name("line-through")]
		LineThrough,
		Blink
	}


	public enum TextDecorationStyle
	{
		Inherit,
		Solid,
		Double,
		Dotted,
		Dashed,
		Wavy
	}


	public enum TextOverflow
	{
		Inherit,
		Clip,
		Ellipsis
	}

	public enum TextRendering
	{
		Inherit,
		Auto,
		OptimizeSpeed,
		OptimizeLegibility,
		GeometricPrecision
	}

	public enum TextTransform
	{
		Inherit,
		Capitalize,
		UpperCase,
		LowerCase,
		None,
		[Name("full-width")]
		FullWidth
	}

	public enum TextUnderlinePosition
	{
		Inherit,
		Auto,
		Under,
		Left,
		Right,
		[Name("under left")]
		UnderLeft,
		[Name("right under")]
		RightUnder
	}

	public enum TouchAction
	{
		Inherit,
		Auto,
		None,
		[Name("pan-x")]
		PanX,
		[Name("pan-y")]
		PanY
	}

	public enum TransformStyle
	{
		Inherit,
		[Name("preserve-3d")]
		Preserve3D,
		Flat
	}

	public enum UnicodeBidi
	{
		Inherit,
		Normal,
		Embed,
		[Name("bidi-override")]
		BidiOverride,
		Isolate,
		[Name("isolate-override")]
		IsolateOverride,
		PlainText
	}

	public enum VerticalAlign
	{
		Inherit,
		Baseline,
		Sub,
		Super,
		[Name("text-top")]
		TextTop,
		[Name("text-bottom")]
		TextBottom,
		Middle,
		Top,
		Bottom
	}

	public enum Visibility
	{
		Inherit,
		Visible,
		Hidden,
		Collapse
	}

	public enum WhiteSpace
	{
		Inherit,
		Normal,
		NoWrap,
		Pre,
		[Name("pre-wrap")]
		PreWrap,
		[Name("pre-line")]
		PreLine
	}

	public enum WordBreak
	{
		Inherit,
		Normal,
		[Name("break-all")]
		BreakAll,
		[Name("keep-all")]
		KeepAll
	}

	public enum WordWrap
	{
		Inherit,
		Normal,
		[Name("break-all")]
		BreakAll
	}

	public enum WritingMode
	{
		Inherit,
		[Name("horizontal-tb")]
		HorizontalTB,
		[Name("rl-tb")]
		RL_TB,
		[Name("vertical-lr")]
		VerticalLR,
		[Name("vertical-rl")]
		VerticalRL,
		[Name("bt-rl")]
		BT_RL,
		[Name("bt-lr")]
		BT_LR,
		[Name("lr-bt")]
		LR_BT,
		[Name("rl-bt")]
		RL_BT
	}

	public enum BoxDecorationBreak
	{
		None,
		Slice,
		Clone
	}



	public enum BoxSizing
	{
		None,
		[Name("border-box")]
		BorderBox,
		[Name("padding-box")]
		PaddingBox,
		[Name("content-box")]
		ContentBox,
		Inherit
	}



	public enum CaptionSide
	{
		None,
		Top,
		Bottom,
		Inherit
	}



	public enum Clear
	{
		None,
		Left,
		Right,
		Both,
		Inherit
	}



	public enum ColumnFill
	{
		Auto,
		Balance,
		Inherit
	}



	public enum ColumnSpan
	{
		None,
		All,
		Inherit
	}



	public enum Float
	{
		None,
		Left,
		Right
	}


	public enum Cursor
	{
		Auto,
		Default,
		None,
		[Name("context-menu")]
		ContextMenu,
		Help,
		Pointer,
		Progress,
		Wait,
		Cell,
		CrossHair,
		Text,
		[Name("vertical-text")]
		VerticalText,
		Alias,
		Copy,
		Move,
		[Name("no-drop")]
		NoDrop,
		[Name("not-allowed")]
		NotAllowed,
		[Name("all-scroll")]
		AllScroll,
		[Name("col-resize")]
		ColResize,
		[Name("row-resize")]
		RowResize,
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
		Inherit,
		Ltr,
		Rtl
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
		Inherit,
		Show,
		Hide
	}



	public enum FontStretch
	{
		Inherit,
		Normal,
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
		Inherit,
		Normal,
		Italic,
		Oblique
	}



	public enum FontSynthesis
	{
		None,
		Weight,
		Style,
		[Name("weight style")]
		WeightStyle
	}



	public enum FontVariant
	{
		Inherit,
		Normal,
		[Name("small-caps")]
		SmallCaps
	}




	public enum FontVariantCaps
	{
		Inherited,
		Normal,
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
		Inherit,
		Normal,
		None,
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
		Inherit,
		Normal,
		Sub,
		Super
	}

	public enum GridAutoFlow
	{
		Inherit,
		None,
		Rows,
		Columns,
		Dense,
		Sparse
	}


	public enum Hyphens
	{
		Inherit,
		None,
		Manual,
		Auto
	}

	public enum All
	{
		None,
		Initial,
		Unset,
		Inherit
	}

	public enum ImageRendering
	{
		Inherit,
		Auto,
		[Name("crisp-edges")]
		CrispEdges,
		Pixelated
	}


	public enum ImeMode
	{
		Inherit,
		Auto,
		Normal,
		Active,
		Inactive,
		Disabled
	}


	public enum ListStylePosition
	{
		Inherit,
		Outside,
		Inside
	}



	public enum ListStyleType
	{
		Inherit,
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
		Inherit,
		Luminance,
		Alpha
	}




	public enum ObjectFit
	{
		Inherit,
		Fill,
		Contain,
		Cover,
		None,
		[Name("scale-down")]
		ScaleDown
	}

}