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

