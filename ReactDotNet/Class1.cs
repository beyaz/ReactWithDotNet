
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

    [Serializable]
    public sealed class CSSStyleDeclaration
    {
        /// <summary>
        /// The margin-bottom CSS property of an element sets the margin space required on the bottom of an element. A negative value is also allowed.
        /// </summary>
        public string MarginBottom;
        /// <summary>
        /// The margin-left CSS property of an element sets the margin space required on the left side of a box associated with an element. A negative value is also allowed.
        /// </summary>
        public string MarginLeft;
        /// <summary>
        /// The margin-right CSS property of an element sets the margin space required on the right side of an element. A negative value is also allowed.
        /// </summary>
        public string MarginRight;
        /// <summary>
        /// The margin-top CSS property of an element sets the margin space required on the top of an element. A negative value is also allowed.
        /// </summary>
        public string MarginTop;

        /// <summary>
        /// The padding CSS property sets the required padding space on all sides of an element. The padding area is the space between the content of the element and its border. Negative values are not allowed.
        /// </summary>
        public string Padding;
        /// <summary>
        /// The padding-bottom CSS property of an element sets the height of the padding area at the bottom of an element. The padding area is the space between the content of the element and it's border. Contrary to margin-bottom values, negative values of padding-bottom are invalid.
        /// </summary>
        public string PaddingBottom;
        /// <summary>
        /// The padding-left CSS property of an element sets the padding space required on the left side of an element. The padding area is the space between the content of the element and it's border. A negative value is not allowed.
        /// </summary>
        public string PaddingLeft;
        /// <summary>
        /// The padding-right CSS property of an element sets the padding space required on the right side of an element. The padding area is the space between the content of the element and its border. Negative values are not allowed.
        /// </summary>
        public string PaddingRight;
        /// <summary>
        /// The padding-top CSS property of an element sets the padding space required on the top of an element. The padding area is the space between the content of the element and its border. Contrary to margin-top values, negative values of padding-top are invalid.
        /// </summary>
        public string PaddingTop;

        /// <summary>
        /// The display CSS property specifies the type of rendering box used for an element. In HTML, default display property values are taken from behaviors described in the HTML specifications or from the browser/user default stylesheet. The default value in XML is inline.
        /// </summary>
        public Union<string, Display> Display { get; set; }

        /// <summary>
        /// The CSS flex-direction property specifies how flex items are placed in t
        /// </summary>
        public FlexDirection FlexDirection;


        /// <summary>
        /// The CSS align-items property aligns flex items of the current flex line the same way as justify-content but in the perpendicular direction.
        /// </summary>
        public Union<string, AlignItems> AlignItems;

        /// <summary>
        /// The height CSS property specifies the height of the content area of an element. The content area is inside the padding, border, and margin of the element.
        /// </summary>
        public string Height { get; set; }

        public string  Width { get; set; }

        public Union<string, AlignContent> AlignContent { get; set; }
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


    public sealed class Union<A,B>
    {
        public A a { get; private set; }
        public B b { get; private set; }

        public static implicit operator Union<A,B>(A a)
        {
            return new Union<A, B> { a = a };
        }

        public static implicit operator Union<A, B>(B b)
        {
            return new Union<A, B> { b=b };
        }
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
}