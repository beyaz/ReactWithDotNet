namespace ReactWithDotNet;
/// <summary>
///     Specifies independent, self-contained content.
/// </summary>
public sealed class article : HtmlElement
{
    /// <summary>
    ///     Specifies independent, self-contained content.
    /// </summary>
    public article() { }

    /// <summary>
    ///     Specifies independent, self-contained content.
    /// </summary>
    public article(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Specifies independent, self-contained content.
    /// </summary>
    public article(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Specifies independent, self-contained content.
    /// </summary>
    public static implicit operator article(string text) => new() { text = text };

    /// <summary>
    ///     Specifies independent, self-contained content.
    /// </summary>
    public article(Style style) : base(style) { }
}

public sealed class button : HtmlElement
{
    [ReactProp]
    public string type { get; set; }

    [ReactProp]
    public string disabled { get; set; }

    public button() { }

    public button(params IModifier[] modifiers) : base(modifiers) { }

    public button(Style style) : base(style) { }
}

public sealed class div : HtmlElement
{
    public div() { }

    public div(params IModifier[] modifiers) : base(modifiers) { }

    public div(string innerText) : base(innerText) {  }

    public static implicit operator div(string text) => new() { text = text };

    public div(Style style) : base(style) { }
}

/// <summary>
///     Defines a paragraph
/// </summary>
public sealed class p : HtmlElement
{
    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p() { }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public static implicit operator p(string text) => new() { text = text };

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p(Style style) : base(style) { }
}

/// <summary>
///     Preformatted text
/// </summary>
public sealed class pre : HtmlElement
{
    /// <summary>
    ///     Preformatted text
    /// </summary>
    public pre() { }

    /// <summary>
    ///     Preformatted text
    /// </summary>
    public pre(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Preformatted text
    /// </summary>
    public pre(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Preformatted text
    /// </summary>
    public static implicit operator pre(string text) => new() { text = text };

    /// <summary>
    ///     Preformatted text
    /// </summary>
    public pre(Style style) : base(style) { }
}

/// <summary>
///     Define some text as computer code in a document
/// </summary>
public sealed class code : HtmlElement
{
    /// <summary>
    ///     Define some text as computer code in a document
    /// </summary>
    public code() { }

    /// <summary>
    ///     Define some text as computer code in a document
    /// </summary>
    public code(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Define some text as computer code in a document
    /// </summary>
    public code(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Define some text as computer code in a document
    /// </summary>
    public static implicit operator code(string text) => new() { text = text };

    /// <summary>
    ///     Define some text as computer code in a document
    /// </summary>
    public code(Style style) : base(style) { }
}

/// <summary>
///     Ordered list
/// </summary>
public sealed class ol : HtmlElement
{
    /// <summary>
    ///     Ordered list
    /// </summary>
    public ol() { }

    /// <summary>
    ///     Ordered list
    /// </summary>
    public ol(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Ordered list
    /// </summary>
    public ol(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Ordered list
    /// </summary>
    public static implicit operator ol(string text) => new() { text = text };

    /// <summary>
    ///     Ordered list
    /// </summary>
    public ol(Style style) : base(style) { }
}

/// <summary>
///     Unordered (bulleted) list
/// </summary>
public sealed class ul : HtmlElement
{
    /// <summary>
    ///     Unordered (bulleted) list
    /// </summary>
    public ul() { }

    /// <summary>
    ///     Unordered (bulleted) list
    /// </summary>
    public ul(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Unordered (bulleted) list
    /// </summary>
    public ul(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Unordered (bulleted) list
    /// </summary>
    public static implicit operator ul(string text) => new() { text = text };

    /// <summary>
    ///     Unordered (bulleted) list
    /// </summary>
    public ul(Style style) : base(style) { }
}

/// <summary>
///     List item
/// </summary>
public sealed class li : HtmlElement
{
    /// <summary>
    ///     List item
    /// </summary>
    public li() { }

    /// <summary>
    ///     List item
    /// </summary>
    public li(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     List item
    /// </summary>
    public li(string innerText) : base(innerText) {  }

    /// <summary>
    ///     List item
    /// </summary>
    public static implicit operator li(string text) => new() { text = text };

    /// <summary>
    ///     List item
    /// </summary>
    public li(Style style) : base(style) { }
}

public sealed class h1 : HtmlElement
{
    public h1() { }

    public h1(params IModifier[] modifiers) : base(modifiers) { }

    public h1(string innerText) : base(innerText) {  }

    public static implicit operator h1(string text) => new() { text = text };

    public h1(Style style) : base(style) { }
}

public sealed class h2 : HtmlElement
{
    public h2() { }

    public h2(params IModifier[] modifiers) : base(modifiers) { }

    public h2(string innerText) : base(innerText) {  }

    public static implicit operator h2(string text) => new() { text = text };

    public h2(Style style) : base(style) { }
}

public sealed class h3 : HtmlElement
{
    public h3() { }

    public h3(params IModifier[] modifiers) : base(modifiers) { }

    public h3(string innerText) : base(innerText) {  }

    public static implicit operator h3(string text) => new() { text = text };

    public h3(Style style) : base(style) { }
}

public sealed class h4 : HtmlElement
{
    public h4() { }

    public h4(params IModifier[] modifiers) : base(modifiers) { }

    public h4(string innerText) : base(innerText) {  }

    public static implicit operator h4(string text) => new() { text = text };

    public h4(Style style) : base(style) { }
}

public sealed class h5 : HtmlElement
{
    public h5() { }

    public h5(params IModifier[] modifiers) : base(modifiers) { }

    public h5(string innerText) : base(innerText) {  }

    public static implicit operator h5(string text) => new() { text = text };

    public h5(Style style) : base(style) { }
}

public sealed class h6 : HtmlElement
{
    public h6() { }

    public h6(params IModifier[] modifiers) : base(modifiers) { }

    public h6(string innerText) : base(innerText) {  }

    public static implicit operator h6(string text) => new() { text = text };

    public h6(Style style) : base(style) { }
}

public sealed class header : HtmlElement
{
    public header() { }

    public header(params IModifier[] modifiers) : base(modifiers) { }

    public header(string innerText) : base(innerText) {  }

    public static implicit operator header(string text) => new() { text = text };

    public header(Style style) : base(style) { }
}

/// <summary>
///     Inline container used to mark up a part of a text, or a part of a document.
/// </summary>
public sealed class span : HtmlElement
{
    /// <summary>
    ///     Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span() { }

    /// <summary>
    ///     Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public static implicit operator span(string text) => new() { text = text };

    /// <summary>
    ///     Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span(Style style) : base(style) { }
}

/// <summary>
///     Superscript text
/// </summary>
public sealed class sup : HtmlElement
{
    /// <summary>
    ///     Superscript text
    /// </summary>
    public sup() { }

    /// <summary>
    ///     Superscript text
    /// </summary>
    public sup(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Superscript text
    /// </summary>
    public sup(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Superscript text
    /// </summary>
    public static implicit operator sup(string text) => new() { text = text };

    /// <summary>
    ///     Superscript text
    /// </summary>
    public sup(Style style) : base(style) { }
}

/// <summary>
///     Subscript text
/// </summary>
public sealed class sub : HtmlElement
{
    /// <summary>
    ///     Subscript text
    /// </summary>
    public sub() { }

    /// <summary>
    ///     Subscript text
    /// </summary>
    public sub(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Subscript text
    /// </summary>
    public sub(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Subscript text
    /// </summary>
    public static implicit operator sub(string text) => new() { text = text };

    /// <summary>
    ///     Subscript text
    /// </summary>
    public sub(Style style) : base(style) { }
}

/// <summary>
///     Inserted text
/// </summary>
public sealed class ins : HtmlElement
{
    /// <summary>
    ///     Inserted text
    /// </summary>
    public ins() { }

    /// <summary>
    ///     Inserted text
    /// </summary>
    public ins(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Inserted text
    /// </summary>
    public ins(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Inserted text
    /// </summary>
    public static implicit operator ins(string text) => new() { text = text };

    /// <summary>
    ///     Inserted text
    /// </summary>
    public ins(Style style) : base(style) { }
}

/// <summary>
///     Deleted text
/// </summary>
public sealed class del : HtmlElement
{
    /// <summary>
    ///     Deleted text
    /// </summary>
    public del() { }

    /// <summary>
    ///     Deleted text
    /// </summary>
    public del(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Deleted text
    /// </summary>
    public del(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Deleted text
    /// </summary>
    public static implicit operator del(string text) => new() { text = text };

    /// <summary>
    ///     Deleted text
    /// </summary>
    public del(Style style) : base(style) { }
}

/// <summary>
///     Smaller text
/// </summary>
public sealed class small : HtmlElement
{
    /// <summary>
    ///     Smaller text
    /// </summary>
    public small() { }

    /// <summary>
    ///     Smaller text
    /// </summary>
    public small(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Smaller text
    /// </summary>
    public small(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Smaller text
    /// </summary>
    public static implicit operator small(string text) => new() { text = text };

    /// <summary>
    ///     Smaller text
    /// </summary>
    public small(Style style) : base(style) { }
}

/// <summary>
///     Marked text
/// </summary>
public sealed class mark : HtmlElement
{
    /// <summary>
    ///     Marked text
    /// </summary>
    public mark() { }

    /// <summary>
    ///     Marked text
    /// </summary>
    public mark(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Marked text
    /// </summary>
    public mark(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Marked text
    /// </summary>
    public static implicit operator mark(string text) => new() { text = text };

    /// <summary>
    ///     Marked text
    /// </summary>
    public mark(Style style) : base(style) { }
}

/// <summary>
///     Emphasized text
/// </summary>
public sealed class em : HtmlElement
{
    /// <summary>
    ///     Emphasized text
    /// </summary>
    public em() { }

    /// <summary>
    ///     Emphasized text
    /// </summary>
    public em(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Emphasized text
    /// </summary>
    public em(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Emphasized text
    /// </summary>
    public static implicit operator em(string text) => new() { text = text };

    /// <summary>
    ///     Emphasized text
    /// </summary>
    public em(Style style) : base(style) { }
}

/// <summary>
///     Bold text
/// </summary>
public sealed class b : HtmlElement
{
    /// <summary>
    ///     Bold text
    /// </summary>
    public b() { }

    /// <summary>
    ///     Bold text
    /// </summary>
    public b(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Bold text
    /// </summary>
    public b(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Bold text
    /// </summary>
    public static implicit operator b(string text) => new() { text = text };

    /// <summary>
    ///     Bold text
    /// </summary>
    public b(Style style) : base(style) { }
}

/// <summary>
///     Italic text
/// </summary>
public sealed class i : HtmlElement
{
    /// <summary>
    ///     Italic text
    /// </summary>
    public i() { }

    /// <summary>
    ///     Italic text
    /// </summary>
    public i(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Italic text
    /// </summary>
    public i(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Italic text
    /// </summary>
    public static implicit operator i(string text) => new() { text = text };

    /// <summary>
    ///     Italic text
    /// </summary>
    public i(Style style) : base(style) { }
}

/// <summary>
///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
/// </summary>
public sealed class u : HtmlElement
{
    /// <summary>
    ///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
    /// </summary>
    public u() { }

    /// <summary>
    ///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
    /// </summary>
    public u(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
    /// </summary>
    public u(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
    /// </summary>
    public static implicit operator u(string text) => new() { text = text };

    /// <summary>
    ///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
    /// </summary>
    public u(Style style) : base(style) { }
}

/// <summary>
///     Important text
/// </summary>
public sealed class strong : HtmlElement
{
    /// <summary>
    ///     Important text
    /// </summary>
    public strong() { }

    /// <summary>
    ///     Important text
    /// </summary>
    public strong(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Important text
    /// </summary>
    public strong(string innerText) : base(innerText) {  }

    /// <summary>
    ///     Important text
    /// </summary>
    public static implicit operator strong(string text) => new() { text = text };

    /// <summary>
    ///     Important text
    /// </summary>
    public strong(Style style) : base(style) { }
}

/// <summary>
///     Section in a document
/// </summary>
public sealed class section : HtmlElement
{
    /// <summary>
    ///     Section in a document
    /// </summary>
    public section() { }

    /// <summary>
    ///     Section in a document
    /// </summary>
    public section(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Section in a document
    /// </summary>
    public section(Style style) : base(style) { }
}

public sealed class aside : HtmlElement
{
    public aside() { }

    public aside(params IModifier[] modifiers) : base(modifiers) { }

    public aside(Style style) : base(style) { }
}

public sealed class fieldset : HtmlElement
{
    public fieldset() { }

    public fieldset(params IModifier[] modifiers) : base(modifiers) { }

    public fieldset(Style style) : base(style) { }
}

public sealed class legend : HtmlElement
{
    public legend() { }

    public legend(params IModifier[] modifiers) : base(modifiers) { }

    public legend(Style style) : base(style) { }
}

public sealed class nav : HtmlElement
{
    public nav() { }

    public nav(params IModifier[] modifiers) : base(modifiers) { }

    public nav(Style style) : base(style) { }
}

public sealed class main : HtmlElement
{
    public main() { }

    public main(params IModifier[] modifiers) : base(modifiers) { }

    public main(Style style) : base(style) { }
}

public sealed class footer : HtmlElement
{
    public footer() { }

    public footer(params IModifier[] modifiers) : base(modifiers) { }

    public footer(Style style) : base(style) { }
}

public sealed class figure : HtmlElement
{
    public figure() { }

    public figure(params IModifier[] modifiers) : base(modifiers) { }

    public figure(Style style) : base(style) { }
}

public sealed class hr : HtmlElement
{
    public hr() { }

    public hr(params IModifier[] modifiers) : base(modifiers) { }

    public hr(Style style) : base(style) { }
}

public sealed class figcaption : HtmlElement
{
    public figcaption() { }

    public figcaption(params IModifier[] modifiers) : base(modifiers) { }

    public figcaption(string innerText) : base(innerText) {  }

    public static implicit operator figcaption(string text) => new() { text = text };

    public figcaption(Style style) : base(style) { }
}

public sealed class table : HtmlElement
{
    [ReactProp]
    public double? cellSpacing { get; set; }

    [ReactProp]
    public double? cellPadding { get; set; }

    public table() { }

    public table(params IModifier[] modifiers) : base(modifiers) { }

    public table(Style style) : base(style) { }
}

public sealed class thead : HtmlElement
{
    public thead() { }

    public thead(params IModifier[] modifiers) : base(modifiers) { }

    public thead(Style style) : base(style) { }
}

public sealed class tbody : HtmlElement
{
    public tbody() { }

    public tbody(params IModifier[] modifiers) : base(modifiers) { }

    public tbody(Style style) : base(style) { }
}

public sealed class tfoot : HtmlElement
{
    public tfoot() { }

    public tfoot(params IModifier[] modifiers) : base(modifiers) { }

    public tfoot(Style style) : base(style) { }
}

public sealed class th : HtmlElement
{
    [ReactProp]
    public int? colSpan { get; set; }

    [ReactProp]
    public int? rowSpan { get; set; }

    public th() { }

    public th(params IModifier[] modifiers) : base(modifiers) { }

    public th(Style style) : base(style) { }
}

public sealed class td : HtmlElement
{
    [ReactProp]
    public int? colSpan { get; set; }

    [ReactProp]
    public int? rowSpan { get; set; }

    public td() { }

    public td(params IModifier[] modifiers) : base(modifiers) { }

    public td(string innerText) : base(innerText) {  }

    public static implicit operator td(string text) => new() { text = text };

    public td(Style style) : base(style) { }
}

public sealed class tr : HtmlElement
{
    [ReactProp]
    public int? colSpan { get; set; }

    [ReactProp]
    public int? rowSpan { get; set; }

    public tr() { }

    public tr(params IModifier[] modifiers) : base(modifiers) { }

    public tr(Style style) : base(style) { }
}

public sealed class option : HtmlElement
{
    [ReactProp]
    public bool? selected { get; set; }

    [ReactProp]
    public string disabled { get; set; }

    [ReactProp]
    public string value { get; set; }

    public option() { }

    public option(params IModifier[] modifiers) : base(modifiers) { }

    public option(Style style) : base(style) { }
}

public sealed class ellipse : HtmlElement
{
    /// <summary>
    ///     The x-coordinate of the center of the ellipse.
    /// </summary>
    [ReactProp]
    public string cx { get; set; }

    /// <summary>
    ///     The y-coordinate of the center of the ellipse.
    /// </summary>
    [ReactProp]
    public string cy { get; set; }

    /// <summary>
    ///     The radius of the ellipse along the x-axis.
    /// </summary>
    [ReactProp]
    public string rx { get; set; }

    /// <summary>
    ///     The radius of the ellipse along the y-axis.
    /// </summary>
    [ReactProp]
    public string ry { get; set; }

    /// <summary>
    ///     The fill color of the ellipse.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    /// <summary>
    ///     The stroke color of the ellipse.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    /// <summary>
    ///     The width of the stroke.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    public ellipse() { }

    public ellipse(params IModifier[] modifiers) : base(modifiers) { }

    public ellipse(Style style) : base(style) { }
}

public sealed class line : HtmlElement
{
    /// <summary>
    ///     The x-coordinate of the start point of the line.
    /// </summary>
    [ReactProp]
    public string x1 { get; set; }

    /// <summary>
    ///     The y-coordinate of the start point of the line.
    /// </summary>
    [ReactProp]
    public string y1 { get; set; }

    /// <summary>
    ///     The x-coordinate of the end point of the line.
    /// </summary>
    [ReactProp]
    public string x2 { get; set; }

    /// <summary>
    ///     The y-coordinate of the end point of the line.
    /// </summary>
    [ReactProp]
    public string y2 { get; set; }

    /// <summary>
    ///     The stroke (outline) color of the line.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    /// <summary>
    ///     The width of the line's outline.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    /// <summary>
    ///     Pattern of dashes and gaps used in the line's stroke.
    /// </summary>
    [ReactProp]
    public string strokeDasharray { get; set; }

    /// <summary>
    ///     The style of the line's endpoints.
    /// </summary>
    [ReactProp]
    public string strokeLinecap { get; set; }

    /// <summary>
    ///     The style of the line's corners.
    /// </summary>
    [ReactProp]
    public string strokeLinejoin { get; set; }

    /// <summary>
    ///     The opacity (transparency) of the line's stroke.
    /// </summary>
    [ReactProp]
    public string strokeOpacity { get; set; }

    public line() { }

    public line(params IModifier[] modifiers) : base(modifiers) { }

    public line(Style style) : base(style) { }
}

public sealed class polyline : HtmlElement
{
    /// <summary>
    ///     A list of points defining the vertices of the polyline.
    /// </summary>
    [ReactProp]
    public string points { get; set; }

    /// <summary>
    ///     The fill color of the polyline.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    /// <summary>
    ///     The stroke (outline) color of the polyline.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    /// <summary>
    ///     The width of the polyline's outline.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    public polyline() { }

    public polyline(params IModifier[] modifiers) : base(modifiers) { }

    public polyline(Style style) : base(style) { }
}

public sealed class circle : HtmlElement
{
    /// <summary>
    ///     The x-coordinate of the center of the circle.
    /// </summary>
    [ReactProp]
    public string cx { get; set; }

    /// <summary>
    ///     The y-coordinate of the center of the circle.
    /// </summary>
    [ReactProp]
    public string cy { get; set; }

    /// <summary>
    ///     The radius of the circle.
    /// </summary>
    [ReactProp]
    public string r { get; set; }

    /// <summary>
    ///     The fill color of the circle.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    /// <summary>
    ///     The stroke color of the circle.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    /// <summary>
    ///     The width of the stroke of the circle.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    public circle() { }

    public circle(params IModifier[] modifiers) : base(modifiers) { }

    public circle(Style style) : base(style) { }
}

public sealed class polygon : HtmlElement
{
    /// <summary>
    ///     Specifies the coordinates of the polygon's vertices, in (x, y) pairs, separated by commas.
    /// </summary>
    [ReactProp]
    public string points { get; set; }

    /// <summary>
    ///     Specifies the fill color of the polygon.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    /// <summary>
    ///     Specifies the stroke color of the polygon.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    /// <summary>
    ///     Specifies the width of the polygon's stroke, in pixels.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    /// <summary>
    ///     Specifies the type of line cap used for the polygon's stroke.
    /// </summary>
    [ReactProp]
    public string strokeLinecap { get; set; }

    /// <summary>
    ///     Specifies the type of line join used for the polygon's stroke.
    /// </summary>
    [ReactProp]
    public string strokeLinejoin { get; set; }

    /// <summary>
    ///     Specifies how the polygon is filled.
    /// </summary>
    [ReactProp]
    public string fillRule { get; set; }

    public polygon() { }

    public polygon(params IModifier[] modifiers) : base(modifiers) { }

    public polygon(Style style) : base(style) { }
}

public sealed class rect : HtmlElement
{
    /// <summary>
    ///     The x-coordinate of the top-left corner of the rectangle.
    /// </summary>
    [ReactProp]
    public string x { get; set; }

    /// <summary>
    ///     The y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    [ReactProp]
    public string y { get; set; }

    /// <summary>
    ///     The width of the rectangle.
    /// </summary>
    [ReactProp]
    public string width { get; set; }

    /// <summary>
    ///     The height of the rectangle.
    /// </summary>
    [ReactProp]
    public string height { get; set; }

    /// <summary>
    ///     The border radius of the rectangle on the horizontal axis.
    /// </summary>
    [ReactProp]
    public string rx { get; set; }

    /// <summary>
    ///     The border radius of the rectangle on the vertical axis.
    /// </summary>
    [ReactProp]
    public string ry { get; set; }

    /// <summary>
    ///     The fill color of the rectangle.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    /// <summary>
    ///     The stroke color of the rectangle.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    /// <summary>
    ///     The width of the rectangle's stroke.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    /// <summary>
    ///     The linecap style of the rectangle's stroke.
    /// </summary>
    [ReactProp]
    public string strokeLinecap { get; set; }

    /// <summary>
    ///     The linejoin style of the rectangle's stroke.
    /// </summary>
    [ReactProp]
    public string strokeLinejoin { get; set; }

    public rect() { }

    public rect(params IModifier[] modifiers) : base(modifiers) { }

    public rect(Style style) : base(style) { }
}

public sealed class radialGradient : HtmlElement
{
    /// <summary>
    ///     The x-coordinate of the center of the gradient.
    /// </summary>
    [ReactProp]
    public string cx { get; set; }

    /// <summary>
    ///     The y-coordinate of the center of the gradient.
    /// </summary>
    [ReactProp]
    public string cy { get; set; }

    /// <summary>
    ///     The x-coordinate of the focal point of the gradient.
    /// </summary>
    [ReactProp]
    public string fx { get; set; }

    /// <summary>
    ///     The y-coordinate of the focal point of the gradient.
    /// </summary>
    [ReactProp]
    public string fy { get; set; }

    /// <summary>
    ///     The radius of the gradient.
    /// </summary>
    [ReactProp]
    public string r { get; set; }

    /// <summary>
    ///     The method used to spread the gradient.
    /// </summary>
    [ReactProp]
    public string spreadMethod { get; set; }

    /// <summary>
    ///     The units used to specify the gradient.
    /// </summary>
    [ReactProp]
    public string gradientUnits { get; set; }

    /// <summary>
    ///     A transform to apply to the gradient.
    /// </summary>
    [ReactProp]
    public string gradientTransform { get; set; }

    public radialGradient() { }

    public radialGradient(params IModifier[] modifiers) : base(modifiers) { }

    public radialGradient(Style style) : base(style) { }
}

public sealed class clipPath : HtmlElement
{
    /// <summary>
    ///     Specifies the fill rule for the clipping path.
    /// </summary>
    [ReactProp]
    public string clipRule { get; set; }

    /// <summary>
    ///     Specifies the reference box for the clipping path.
    /// </summary>
    [ReactProp]
    public string clipBox { get; set; }

    public clipPath() { }

    public clipPath(params IModifier[] modifiers) : base(modifiers) { }

    public clipPath(Style style) : base(style) { }
}

public sealed class path : HtmlElement
{
    /// <summary>
    ///     Path data
    /// </summary>
    [ReactProp]
    public string d { get; set; }

    /// <summary>
    ///     Fill color
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    /// <summary>
    ///     Stroke color
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    /// <summary>
    ///     Stroke width
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    [ReactProp]
    public string fillRule { get; set; }

    [ReactProp]
    public string clipRule { get; set; }

    [ReactProp]
    public string strokeLinecap { get; set; }

    [ReactProp]
    public string strokeLinejoin { get; set; }

    public path() { }

    public path(params IModifier[] modifiers) : base(modifiers) { }

    public path(Style style) : base(style) { }
}

public sealed class g : HtmlElement
{
    [ReactProp]
    public string opacity { get; set; }

    [ReactProp]
    public string clipPath { get; set; }

    [ReactProp]
    public string transform { get; set; }

    public g() { }

    public g(params IModifier[] modifiers) : base(modifiers) { }

    public g(Style style) : base(style) { }
}

public sealed class meta : HtmlElement
{
    /// <summary>
    ///     Specifies the character encoding of the document.
    /// </summary>
    [ReactProp]
    public string charset { get; set; }

    /// <summary>
    ///     Specifies the name of the HTTP header that the meta tag should be equivalent to.
    /// </summary>
    [ReactProp]
    public string httpEquiv { get; set; }

    /// <summary>
    ///     Specifies the name of the metadata property.
    /// </summary>
    [ReactProp]
    public string name { get; set; }

    /// <summary>
    ///     Specifies the value of the metadata property.
    /// </summary>
    [ReactProp]
    public string content { get; set; }

    /// <summary>
    ///     Specifies the URL scheme for the content attribute of the meta tag.
    /// </summary>
    [ReactProp]
    public string scheme { get; set; }

    /// <summary>
    ///     Specifies the Microdata item property that the meta tag represents.
    /// </summary>
    [ReactProp]
    public string itemprop { get; set; }

    /// <summary>
    ///     Specifies the schema.org property that the meta tag represents.
    /// </summary>
    [ReactProp]
    public string property { get; set; }

    /// <summary>
    ///     Specifies the URL for a resource associated with the meta tag.
    /// </summary>
    [ReactProp]
    public string src { get; set; }

    public meta() { }

    public meta(params IModifier[] modifiers) : base(modifiers) { }

    public meta(Style style) : base(style) { }
}

public sealed class body : HtmlElement
{
    /// <summary>
    ///     Specifies the URL of a background image to be displayed behind the document's content.
    /// </summary>
    [ReactProp]
    public string background { get; set; }

    /// <summary>
    ///     Specifies the background color of the document's body.
    /// </summary>
    [ReactProp]
    public string bgcolor { get; set; }

    /// <summary>
    ///     Specifies the color of unvisited links in the document's body.
    /// </summary>
    [ReactProp]
    public string link { get; set; }

    public body() { }

    public body(params IModifier[] modifiers) : base(modifiers) { }

    public body(Style style) : base(style) { }
}

public sealed class script : HtmlElement
{
    /// <summary>
    ///     Specifies that the script should be executed asynchronously. This means that the browser will not wait for the script to finish executing before continuing to parse the rest of the HTML.
    /// </summary>
    [ReactProp]
    public string async { get; set; }

    /// <summary>
    ///     Specifies that the script should be executed after the browser has finished parsing the rest of the HTML. This is similar to async, but it ensures that scripts are executed in the order they are specified in the HTML.
    /// </summary>
    [ReactProp]
    public string defer { get; set; }

    /// <summary>
    ///     Specifies a subresource integrity (SRI) hash for the script. This helps to protect against man-in-the-middle attacks.
    /// </summary>
    [ReactProp]
    public string integrity { get; set; }

    /// <summary>
    ///     Specifies the scripting language of the script. This is deprecated, but is still supported by most browsers.
    /// </summary>
    [ReactProp]
    public string language { get; set; }

    /// <summary>
    ///     Specifies that the script should be ignored if the browser does not support modules.
    /// </summary>
    [ReactProp]
    public string nomodule { get; set; }

    /// <summary>
    ///     Specifies the URL of an external script file.
    /// </summary>
    [ReactProp]
    public string src { get; set; }

    /// <summary>
    ///     Specifies the type of the script. The most common value is application/javascript.
    /// </summary>
    [ReactProp]
    public string type { get; set; }

    public script() { }

    public script(params IModifier[] modifiers) : base(modifiers) { }

    public script(Style style) : base(style) { }
}

public sealed class title : HtmlElement
{
    /// <summary>
    ///     Specifies the language of the title.
    /// </summary>
    [ReactProp]
    public string language { get; set; }

    public title() { }

    public title(params IModifier[] modifiers) : base(modifiers) { }

    public title(Style style) : base(style) { }
}

public sealed class head : HtmlElement
{
    /// <summary>
    ///     Provides a URL to a profile document for the current document.
    /// </summary>
    [ReactProp]
    public string profile { get; set; }

    /// <summary>
    ///     Provides a link to an external resource, such as a stylesheet or script file.
    /// </summary>
    [ReactProp]
    public string link { get; set; }

    /// <summary>
    ///     Provides metadata about the document, such as the character encoding, author, and keywords.
    /// </summary>
    [ReactProp]
    public string meta { get; set; }

    /// <summary>
    ///     Provides JavaScript code to be executed in the browser.
    /// </summary>
    [ReactProp]
    public string script { get; set; }

    /// <summary>
    ///     Provides content to be displayed if the browser does not support JavaScript.
    /// </summary>
    [ReactProp]
    public string noscript { get; set; }

    public head() { }

    public head(params IModifier[] modifiers) : base(modifiers) { }

    public head(Style style) : base(style) { }
}

public sealed class html : HtmlElement
{
    /// <summary>
    ///     Hides the element from display.
    /// </summary>
    [ReactProp]
    public string hidden { get; set; }

    /// <summary>
    ///     Specifies the URL of a manifest file, which provides information about the web app.
    /// </summary>
    [ReactProp]
    public string manifest { get; set; }

    /// <summary>
    ///     Specifies the namespace of the element.
    /// </summary>
    [ReactProp]
    public string xmlns { get; set; } = "http://www.w3.org/1999/xhtml";

    /// <summary>
    ///     Specifies the prefix of the element.
    /// </summary>
    [ReactProp]
    public string prefix { get; set; }

    /// <summary>
    ///     Specifies the version of the HTML specification to which the element conforms.
    /// </summary>
    [ReactProp]
    public string version { get; set; }

    public html() { }

    public html(params IModifier[] modifiers) : base(modifiers) { }

    public html(Style style) : base(style) { }
}

public sealed class label : HtmlElement
{
    /// <summary>
    ///     Specifies which form element a label is bound to.
    /// </summary>
    [ReactProp]
    public string htmlFor { get; set; }

    /// <summary>
    ///     Specifies whether the element is a drop target.
    /// </summary>
    [ReactProp]
    public string dropzone { get; set; }

    /// <summary>
    ///     Hides the element from view.
    /// </summary>
    [ReactProp]
    public string hidden { get; set; }

    /// <summary>
    ///     Specifies the element's position in the tab order.
    /// </summary>
    [ReactProp]
    public string tabindex { get; set; }

    public label() { }

    public label(params IModifier[] modifiers) : base(modifiers) { }

    public label(string innerText) : base(innerText) {  }

    public static implicit operator label(string text) => new() { text = text };

    public label(Style style) : base(style) { }
}

public sealed class a : HtmlElement
{
    /// <summary>
    ///     The URL of the linked resource.
    /// </summary>
    [ReactProp]
    public string href { get; set; }

    /// <summary>
    ///     Specifies where the linked resource should be opened. Can be `_blank`, `_self`, `_parent`, or `_top`.
    /// </summary>
    [ReactProp]
    public string target { get; set; }

    /// <summary>
    ///     Specifies the relationship between the current document and the linked resource. Can be `alternate`, `author`, `bookmark`, `canonical`, `external`, `help`, `license`, `next`, `nofollow`, `noreferrer`, `noopener`, `prev`, `search`, `sponsored`, or `stylesheet`.
    /// </summary>
    [ReactProp]
    public string rel { get; set; }

    /// <summary>
    ///     Specifies the MIME type of the linked resource, if applicable.
    /// </summary>
    [ReactProp]
    public string type { get; set; }

    /// <summary>
    ///     Specifies whether the linked resource should be downloaded or opened in a new browser tab.
    /// </summary>
    [ReactProp]
    public string download { get; set; }

    /// <summary>
    ///     A list of URLs to which a ping should be sent when the user clicks on the link.
    /// </summary>
    [ReactProp]
    public string ping { get; set; }

    /// <summary>
    ///     Specifies the media types for which the link is relevant.
    /// </summary>
    [ReactProp]
    public string media { get; set; }

    /// <summary>
    ///     Specifies the language of the linked resource.
    /// </summary>
    [ReactProp]
    public string hreflang { get; set; }

    /// <summary>
    ///     Specifies a name for the link. This can be used to target the link with JavaScript.
    /// </summary>
    [ReactProp]
    public string name { get; set; }

    /// <summary>
    ///     Specifies the tab order of the link.
    /// </summary>
    [ReactProp]
    public string tabindex { get; set; }

    public a() { }

    public a(params IModifier[] modifiers) : base(modifiers) { }

    public a(Style style) : base(style) { }
}

