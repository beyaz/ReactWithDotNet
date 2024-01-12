namespace ReactWithDotNet;
/// <summary>
///     Specifies independent, self-contained content.
/// </summary>
public sealed class article : HtmlElement
{

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
    public article() { }

    /// <summary>
    ///     Specifies independent, self-contained content.
    /// </summary>
    public article(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Specifies independent, self-contained content.
    /// </summary>
    public article(Style style) : base(style) { }

    /// <summary>
    ///     Specifies independent, self-contained content.
    /// </summary>
    public article(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<article> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class button : HtmlElement
{
    #region string type
    PropertyValueNode<string> __type;
    static readonly PropertyValueDefinition _type = new()
    {
        name = nameof(type)
    };
    #endregion
    /// <summary>
    ///     Specifies the type of button. button, reset, submit
    /// </summary>
    [ReactProp]
    public string type { get; set; }

    #region string value
    PropertyValueNode<string> __value;
    static readonly PropertyValueDefinition _value = new()
    {
        name = nameof(value)
    };
    #endregion
    /// <summary>
    ///     Specifies an initial value for the button
    /// </summary>
    [ReactProp]
    public string value { get; set; }

    #region string autofocus
    PropertyValueNode<string> __autofocus;
    static readonly PropertyValueDefinition _autofocus = new()
    {
        name = nameof(autofocus)
    };
    #endregion
    /// <summary>
    ///     Specifies that the button should have input focus when the page loads. Only one element in a document can have this attribute.
    /// </summary>
    [ReactProp]
    public string autofocus { get; set; }

    #region string disabled
    PropertyValueNode<string> __disabled;
    static readonly PropertyValueDefinition _disabled = new()
    {
        name = nameof(disabled)
    };
    #endregion
    /// <summary>
    ///     Specifies that the button should be disabled. A disabled button cannot be clicked.
    /// </summary>
    [ReactProp]
    public string disabled { get; set; }

    #region string form
    PropertyValueNode<string> __form;
    static readonly PropertyValueDefinition _form = new()
    {
        name = nameof(form)
    };
    #endregion
    /// <summary>
    ///     Specifies the form that the button is associated with.
    /// </summary>
    [ReactProp]
    public string form { get; set; }

    #region string formaction
    PropertyValueNode<string> __formaction;
    static readonly PropertyValueDefinition _formaction = new()
    {
        name = nameof(formaction)
    };
    #endregion
    /// <summary>
    ///     Specifies the URL of the form action when the button is clicked.
    /// </summary>
    [ReactProp]
    public string formaction { get; set; }

    #region string formenctype
    PropertyValueNode<string> __formenctype;
    static readonly PropertyValueDefinition _formenctype = new()
    {
        name = nameof(formenctype)
    };
    #endregion
    /// <summary>
    ///     Specifies the form encoding method (e.g., application/x-www-form-urlencoded, multipart/form-data) when the button is clicked.
    /// </summary>
    [ReactProp]
    public string formenctype { get; set; }

    #region string formmethod
    PropertyValueNode<string> __formmethod;
    static readonly PropertyValueDefinition _formmethod = new()
    {
        name = nameof(formmethod)
    };
    #endregion
    /// <summary>
    ///     Specifies the form submission method (e.g., GET, POST) when the button is clicked.
    /// </summary>
    [ReactProp]
    public string formmethod { get; set; }

    #region string formnovalidate
    PropertyValueNode<string> __formnovalidate;
    static readonly PropertyValueDefinition _formnovalidate = new()
    {
        name = nameof(formnovalidate)
    };
    #endregion
    /// <summary>
    ///     Specifies that the form should not be validated before submission when the button is clicked.
    /// </summary>
    [ReactProp]
    public string formnovalidate { get; set; }

    #region string name
    PropertyValueNode<string> __name;
    static readonly PropertyValueDefinition _name = new()
    {
        name = nameof(name)
    };
    #endregion
    /// <summary>
    ///     Specifies a name for the button. The name attribute is used to reference form-data after the form has been submitted, or to reference the element in JavaScript.
    /// </summary>
    [ReactProp]
    public string name { get; set; }

    public button() { }

    public button(params IModifier[] modifiers) : base(modifiers) { }

    public button(Style style) : base(style) { }

    public button(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<button> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     type = <paramref name="value"/>
    /// <br/>
    ///     Specifies the type of button. button, reset, submit
    /// </summary>
    public static HtmlElementModifier Type(string value) => Modify(x => x.type = value);

    /// <summary>
    ///     value = <paramref name="value"/>
    /// <br/>
    ///     Specifies an initial value for the button
    /// </summary>
    public static HtmlElementModifier Value(string value) => Modify(x => x.value = value);

    /// <summary>
    ///     autofocus = <paramref name="value"/>
    /// <br/>
    ///     Specifies that the button should have input focus when the page loads. Only one element in a document can have this attribute.
    /// </summary>
    public static HtmlElementModifier Autofocus(string value) => Modify(x => x.autofocus = value);

    /// <summary>
    ///     disabled = <paramref name="value"/>
    /// <br/>
    ///     Specifies that the button should be disabled. A disabled button cannot be clicked.
    /// </summary>
    public static HtmlElementModifier Disabled(string value) => Modify(x => x.disabled = value);

    /// <summary>
    ///     form = <paramref name="value"/>
    /// <br/>
    ///     Specifies the form that the button is associated with.
    /// </summary>
    public static HtmlElementModifier Form(string value) => Modify(x => x.form = value);

    /// <summary>
    ///     formaction = <paramref name="value"/>
    /// <br/>
    ///     Specifies the URL of the form action when the button is clicked.
    /// </summary>
    public static HtmlElementModifier Formaction(string value) => Modify(x => x.formaction = value);

    /// <summary>
    ///     formenctype = <paramref name="value"/>
    /// <br/>
    ///     Specifies the form encoding method (e.g., application/x-www-form-urlencoded, multipart/form-data) when the button is clicked.
    /// </summary>
    public static HtmlElementModifier Formenctype(string value) => Modify(x => x.formenctype = value);

    /// <summary>
    ///     formmethod = <paramref name="value"/>
    /// <br/>
    ///     Specifies the form submission method (e.g., GET, POST) when the button is clicked.
    /// </summary>
    public static HtmlElementModifier Formmethod(string value) => Modify(x => x.formmethod = value);

    /// <summary>
    ///     formnovalidate = <paramref name="value"/>
    /// <br/>
    ///     Specifies that the form should not be validated before submission when the button is clicked.
    /// </summary>
    public static HtmlElementModifier Formnovalidate(string value) => Modify(x => x.formnovalidate = value);

    /// <summary>
    ///     name = <paramref name="value"/>
    /// <br/>
    ///     Specifies a name for the button. The name attribute is used to reference form-data after the form has been submitted, or to reference the element in JavaScript.
    /// </summary>
    public static HtmlElementModifier Name(string value) => Modify(x => x.name = value);

}

public sealed class div : HtmlElement
{

    public div(string innerText) : base(innerText) {  }

    public static implicit operator div(string text) => new() { text = text };
    public div() { }

    public div(params IModifier[] modifiers) : base(modifiers) { }

    public div(Style style) : base(style) { }

    public div(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<div> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Defines a paragraph
/// </summary>
public sealed class p : HtmlElement
{

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
    public p() { }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p(Style style) : base(style) { }

    /// <summary>
    ///     Defines a paragraph
    /// </summary>
    public p(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<p> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Preformatted text
/// </summary>
public sealed class pre : HtmlElement
{

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
    public pre() { }

    /// <summary>
    ///     Preformatted text
    /// </summary>
    public pre(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Preformatted text
    /// </summary>
    public pre(Style style) : base(style) { }

    /// <summary>
    ///     Preformatted text
    /// </summary>
    public pre(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<pre> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Define some text as computer code in a document
/// </summary>
public sealed class code : HtmlElement
{

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
    public code() { }

    /// <summary>
    ///     Define some text as computer code in a document
    /// </summary>
    public code(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Define some text as computer code in a document
    /// </summary>
    public code(Style style) : base(style) { }

    /// <summary>
    ///     Define some text as computer code in a document
    /// </summary>
    public code(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<code> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Ordered list
/// </summary>
public sealed class ol : HtmlElement
{

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
    public ol() { }

    /// <summary>
    ///     Ordered list
    /// </summary>
    public ol(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Ordered list
    /// </summary>
    public ol(Style style) : base(style) { }

    /// <summary>
    ///     Ordered list
    /// </summary>
    public ol(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<ol> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Unordered (bulleted) list
/// </summary>
public sealed class ul : HtmlElement
{

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
    public ul() { }

    /// <summary>
    ///     Unordered (bulleted) list
    /// </summary>
    public ul(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Unordered (bulleted) list
    /// </summary>
    public ul(Style style) : base(style) { }

    /// <summary>
    ///     Unordered (bulleted) list
    /// </summary>
    public ul(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<ul> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     List item
/// </summary>
public sealed class li : HtmlElement
{

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
    public li() { }

    /// <summary>
    ///     List item
    /// </summary>
    public li(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     List item
    /// </summary>
    public li(Style style) : base(style) { }

    /// <summary>
    ///     List item
    /// </summary>
    public li(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<li> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class h1 : HtmlElement
{

    public h1(string innerText) : base(innerText) {  }

    public static implicit operator h1(string text) => new() { text = text };
    public h1() { }

    public h1(params IModifier[] modifiers) : base(modifiers) { }

    public h1(Style style) : base(style) { }

    public h1(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<h1> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class h2 : HtmlElement
{

    public h2(string innerText) : base(innerText) {  }

    public static implicit operator h2(string text) => new() { text = text };
    public h2() { }

    public h2(params IModifier[] modifiers) : base(modifiers) { }

    public h2(Style style) : base(style) { }

    public h2(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<h2> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class h3 : HtmlElement
{

    public h3(string innerText) : base(innerText) {  }

    public static implicit operator h3(string text) => new() { text = text };
    public h3() { }

    public h3(params IModifier[] modifiers) : base(modifiers) { }

    public h3(Style style) : base(style) { }

    public h3(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<h3> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class h4 : HtmlElement
{

    public h4(string innerText) : base(innerText) {  }

    public static implicit operator h4(string text) => new() { text = text };
    public h4() { }

    public h4(params IModifier[] modifiers) : base(modifiers) { }

    public h4(Style style) : base(style) { }

    public h4(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<h4> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class h5 : HtmlElement
{

    public h5(string innerText) : base(innerText) {  }

    public static implicit operator h5(string text) => new() { text = text };
    public h5() { }

    public h5(params IModifier[] modifiers) : base(modifiers) { }

    public h5(Style style) : base(style) { }

    public h5(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<h5> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class h6 : HtmlElement
{

    public h6(string innerText) : base(innerText) {  }

    public static implicit operator h6(string text) => new() { text = text };
    public h6() { }

    public h6(params IModifier[] modifiers) : base(modifiers) { }

    public h6(Style style) : base(style) { }

    public h6(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<h6> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class header : HtmlElement
{

    public header(string innerText) : base(innerText) {  }

    public static implicit operator header(string text) => new() { text = text };
    public header() { }

    public header(params IModifier[] modifiers) : base(modifiers) { }

    public header(Style style) : base(style) { }

    public header(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<header> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Inline container used to mark up a part of a text, or a part of a document.
/// </summary>
public sealed class span : HtmlElement
{

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
    public span() { }

    /// <summary>
    ///     Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span(Style style) : base(style) { }

    /// <summary>
    ///     Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<span> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Superscript text
/// </summary>
public sealed class sup : HtmlElement
{

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
    public sup() { }

    /// <summary>
    ///     Superscript text
    /// </summary>
    public sup(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Superscript text
    /// </summary>
    public sup(Style style) : base(style) { }

    /// <summary>
    ///     Superscript text
    /// </summary>
    public sup(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<sup> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Subscript text
/// </summary>
public sealed class sub : HtmlElement
{

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
    public sub() { }

    /// <summary>
    ///     Subscript text
    /// </summary>
    public sub(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Subscript text
    /// </summary>
    public sub(Style style) : base(style) { }

    /// <summary>
    ///     Subscript text
    /// </summary>
    public sub(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<sub> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Inserted text
/// </summary>
public sealed class ins : HtmlElement
{

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
    public ins() { }

    /// <summary>
    ///     Inserted text
    /// </summary>
    public ins(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Inserted text
    /// </summary>
    public ins(Style style) : base(style) { }

    /// <summary>
    ///     Inserted text
    /// </summary>
    public ins(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<ins> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Deleted text
/// </summary>
public sealed class del : HtmlElement
{

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
    public del() { }

    /// <summary>
    ///     Deleted text
    /// </summary>
    public del(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Deleted text
    /// </summary>
    public del(Style style) : base(style) { }

    /// <summary>
    ///     Deleted text
    /// </summary>
    public del(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<del> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Smaller text
/// </summary>
public sealed class small : HtmlElement
{

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
    public small() { }

    /// <summary>
    ///     Smaller text
    /// </summary>
    public small(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Smaller text
    /// </summary>
    public small(Style style) : base(style) { }

    /// <summary>
    ///     Smaller text
    /// </summary>
    public small(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<small> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Marked text
/// </summary>
public sealed class mark : HtmlElement
{

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
    public mark() { }

    /// <summary>
    ///     Marked text
    /// </summary>
    public mark(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Marked text
    /// </summary>
    public mark(Style style) : base(style) { }

    /// <summary>
    ///     Marked text
    /// </summary>
    public mark(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<mark> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Emphasized text
/// </summary>
public sealed class em : HtmlElement
{

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
    public em() { }

    /// <summary>
    ///     Emphasized text
    /// </summary>
    public em(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Emphasized text
    /// </summary>
    public em(Style style) : base(style) { }

    /// <summary>
    ///     Emphasized text
    /// </summary>
    public em(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<em> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Bold text
/// </summary>
public sealed class b : HtmlElement
{

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
    public b() { }

    /// <summary>
    ///     Bold text
    /// </summary>
    public b(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Bold text
    /// </summary>
    public b(Style style) : base(style) { }

    /// <summary>
    ///     Bold text
    /// </summary>
    public b(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<b> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Italic text
/// </summary>
public sealed class i : HtmlElement
{

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
    public i() { }

    /// <summary>
    ///     Italic text
    /// </summary>
    public i(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Italic text
    /// </summary>
    public i(Style style) : base(style) { }

    /// <summary>
    ///     Italic text
    /// </summary>
    public i(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<i> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
/// </summary>
public sealed class u : HtmlElement
{

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
    public u() { }

    /// <summary>
    ///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
    /// </summary>
    public u(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
    /// </summary>
    public u(Style style) : base(style) { }

    /// <summary>
    ///     Represents some text that is unarticulated and styled differently from normal text, such as misspelled words or proper names in Chinese text. The content inside is typically displayed with an underline.
    /// </summary>
    public u(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<u> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

/// <summary>
///     Important text
/// </summary>
public sealed class strong : HtmlElement
{

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
    public strong() { }

    /// <summary>
    ///     Important text
    /// </summary>
    public strong(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    ///     Important text
    /// </summary>
    public strong(Style style) : base(style) { }

    /// <summary>
    ///     Important text
    /// </summary>
    public strong(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<strong> modifyAction) => CreateHtmlElementModifier(modifyAction);
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

    /// <summary>
    ///     Section in a document
    /// </summary>
    public section(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<section> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class aside : HtmlElement
{
    public aside() { }

    public aside(params IModifier[] modifiers) : base(modifiers) { }

    public aside(Style style) : base(style) { }

    public aside(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<aside> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class fieldset : HtmlElement
{
    public fieldset() { }

    public fieldset(params IModifier[] modifiers) : base(modifiers) { }

    public fieldset(Style style) : base(style) { }

    public fieldset(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<fieldset> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class legend : HtmlElement
{
    public legend() { }

    public legend(params IModifier[] modifiers) : base(modifiers) { }

    public legend(Style style) : base(style) { }

    public legend(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<legend> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class nav : HtmlElement
{
    public nav() { }

    public nav(params IModifier[] modifiers) : base(modifiers) { }

    public nav(Style style) : base(style) { }

    public nav(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<nav> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class main : HtmlElement
{
    public main() { }

    public main(params IModifier[] modifiers) : base(modifiers) { }

    public main(Style style) : base(style) { }

    public main(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<main> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class footer : HtmlElement
{
    public footer() { }

    public footer(params IModifier[] modifiers) : base(modifiers) { }

    public footer(Style style) : base(style) { }

    public footer(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<footer> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class figure : HtmlElement
{
    public figure() { }

    public figure(params IModifier[] modifiers) : base(modifiers) { }

    public figure(Style style) : base(style) { }

    public figure(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<figure> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class hr : HtmlElement
{
    public hr() { }

    public hr(params IModifier[] modifiers) : base(modifiers) { }

    public hr(Style style) : base(style) { }

    public hr(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<hr> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class figcaption : HtmlElement
{

    public figcaption(string innerText) : base(innerText) {  }

    public static implicit operator figcaption(string text) => new() { text = text };
    public figcaption() { }

    public figcaption(params IModifier[] modifiers) : base(modifiers) { }

    public figcaption(Style style) : base(style) { }

    public figcaption(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<figcaption> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class table : HtmlElement
{
    #region string cellSpacing
    PropertyValueNode<double?> __cellSpacing;
    static readonly PropertyValueDefinition _cellSpacing = new()
    {
        name = nameof(cellSpacing)
    };
    #endregion
    [ReactProp]
    public double? cellSpacing { get; set; }

    #region string cellPadding
    PropertyValueNode<double?> __cellPadding;
    static readonly PropertyValueDefinition _cellPadding = new()
    {
        name = nameof(cellPadding)
    };
    #endregion
    [ReactProp]
    public double? cellPadding { get; set; }

    public table() { }

    public table(params IModifier[] modifiers) : base(modifiers) { }

    public table(Style style) : base(style) { }

    public table(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<table> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier CellSpacing(double? value) => Modify(x => x.cellSpacing = value);

    public static HtmlElementModifier CellPadding(double? value) => Modify(x => x.cellPadding = value);

}

public sealed class thead : HtmlElement
{
    public thead() { }

    public thead(params IModifier[] modifiers) : base(modifiers) { }

    public thead(Style style) : base(style) { }

    public thead(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<thead> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class tbody : HtmlElement
{
    public tbody() { }

    public tbody(params IModifier[] modifiers) : base(modifiers) { }

    public tbody(Style style) : base(style) { }

    public tbody(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<tbody> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class tfoot : HtmlElement
{
    public tfoot() { }

    public tfoot(params IModifier[] modifiers) : base(modifiers) { }

    public tfoot(Style style) : base(style) { }

    public tfoot(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<tfoot> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class th : HtmlElement
{
    #region string colSpan
    PropertyValueNode<int?> __colSpan;
    static readonly PropertyValueDefinition _colSpan = new()
    {
        name = nameof(colSpan)
    };
    #endregion
    [ReactProp]
    public int? colSpan { get; set; }

    #region string rowSpan
    PropertyValueNode<int?> __rowSpan;
    static readonly PropertyValueDefinition _rowSpan = new()
    {
        name = nameof(rowSpan)
    };
    #endregion
    [ReactProp]
    public int? rowSpan { get; set; }

    public th() { }

    public th(params IModifier[] modifiers) : base(modifiers) { }

    public th(Style style) : base(style) { }

    public th(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<th> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier ColSpan(int? value) => Modify(x => x.colSpan = value);

    public static HtmlElementModifier RowSpan(int? value) => Modify(x => x.rowSpan = value);

}

public sealed class td : HtmlElement
{
    #region string colSpan
    PropertyValueNode<int?> __colSpan;
    static readonly PropertyValueDefinition _colSpan = new()
    {
        name = nameof(colSpan)
    };
    #endregion
    [ReactProp]
    public int? colSpan { get; set; }

    #region string rowSpan
    PropertyValueNode<int?> __rowSpan;
    static readonly PropertyValueDefinition _rowSpan = new()
    {
        name = nameof(rowSpan)
    };
    #endregion
    [ReactProp]
    public int? rowSpan { get; set; }


    public td(string innerText) : base(innerText) {  }

    public static implicit operator td(string text) => new() { text = text };
    public td() { }

    public td(params IModifier[] modifiers) : base(modifiers) { }

    public td(Style style) : base(style) { }

    public td(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<td> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier ColSpan(int? value) => Modify(x => x.colSpan = value);

    public static HtmlElementModifier RowSpan(int? value) => Modify(x => x.rowSpan = value);

}

public sealed class tr : HtmlElement
{
    #region string colSpan
    PropertyValueNode<int?> __colSpan;
    static readonly PropertyValueDefinition _colSpan = new()
    {
        name = nameof(colSpan)
    };
    #endregion
    [ReactProp]
    public int? colSpan { get; set; }

    #region string rowSpan
    PropertyValueNode<int?> __rowSpan;
    static readonly PropertyValueDefinition _rowSpan = new()
    {
        name = nameof(rowSpan)
    };
    #endregion
    [ReactProp]
    public int? rowSpan { get; set; }

    public tr() { }

    public tr(params IModifier[] modifiers) : base(modifiers) { }

    public tr(Style style) : base(style) { }

    public tr(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<tr> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier ColSpan(int? value) => Modify(x => x.colSpan = value);

    public static HtmlElementModifier RowSpan(int? value) => Modify(x => x.rowSpan = value);

}

public sealed class option : HtmlElement
{
    #region string selected
    PropertyValueNode<bool?> __selected;
    static readonly PropertyValueDefinition _selected = new()
    {
        name = nameof(selected)
    };
    #endregion
    [ReactProp]
    public bool? selected { get; set; }

    #region string disabled
    PropertyValueNode<string> __disabled;
    static readonly PropertyValueDefinition _disabled = new()
    {
        name = nameof(disabled)
    };
    #endregion
    [ReactProp]
    public string disabled { get; set; }

    #region string value
    PropertyValueNode<string> __value;
    static readonly PropertyValueDefinition _value = new()
    {
        name = nameof(value)
    };
    #endregion
    [ReactProp]
    public string value { get; set; }

    public option() { }

    public option(params IModifier[] modifiers) : base(modifiers) { }

    public option(Style style) : base(style) { }

    public option(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<option> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Selected(bool? value) => Modify(x => x.selected = value);

    public static HtmlElementModifier Disabled(string value) => Modify(x => x.disabled = value);

    public static HtmlElementModifier Value(string value) => Modify(x => x.value = value);

}

public sealed class ellipse : HtmlElement
{
    #region string cx
    PropertyValueNode<string> __cx;
    static readonly PropertyValueDefinition _cx = new()
    {
        name = nameof(cx)
    };
    #endregion
    /// <summary>
    ///     The x-coordinate of the center of the ellipse.
    /// </summary>
    [ReactProp]
    public string cx { get; set; }

    #region string cy
    PropertyValueNode<string> __cy;
    static readonly PropertyValueDefinition _cy = new()
    {
        name = nameof(cy)
    };
    #endregion
    /// <summary>
    ///     The y-coordinate of the center of the ellipse.
    /// </summary>
    [ReactProp]
    public string cy { get; set; }

    #region string rx
    PropertyValueNode<string> __rx;
    static readonly PropertyValueDefinition _rx = new()
    {
        name = nameof(rx)
    };
    #endregion
    /// <summary>
    ///     The radius of the ellipse along the x-axis.
    /// </summary>
    [ReactProp]
    public string rx { get; set; }

    #region string ry
    PropertyValueNode<string> __ry;
    static readonly PropertyValueDefinition _ry = new()
    {
        name = nameof(ry)
    };
    #endregion
    /// <summary>
    ///     The radius of the ellipse along the y-axis.
    /// </summary>
    [ReactProp]
    public string ry { get; set; }

    #region string fill
    PropertyValueNode<string> __fill;
    static readonly PropertyValueDefinition _fill = new()
    {
        name = nameof(fill)
    };
    #endregion
    /// <summary>
    ///     The fill color of the ellipse.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    #region string stroke
    PropertyValueNode<string> __stroke;
    static readonly PropertyValueDefinition _stroke = new()
    {
        name = nameof(stroke)
    };
    #endregion
    /// <summary>
    ///     The stroke color of the ellipse.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    #region string strokeWidth
    PropertyValueNode<string> __strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth = new()
    {
        name = nameof(strokeWidth)
    };
    #endregion
    /// <summary>
    ///     The width of the stroke.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    public ellipse() { }

    public ellipse(params IModifier[] modifiers) : base(modifiers) { }

    public ellipse(Style style) : base(style) { }

    public ellipse(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<ellipse> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     cx = <paramref name="value"/>
    /// <br/>
    ///     The x-coordinate of the center of the ellipse.
    /// </summary>
    public static HtmlElementModifier Cx(string value) => Modify(x => x.cx = value);

    /// <summary>
    ///     cy = <paramref name="value"/>
    /// <br/>
    ///     The y-coordinate of the center of the ellipse.
    /// </summary>
    public static HtmlElementModifier Cy(string value) => Modify(x => x.cy = value);

    /// <summary>
    ///     rx = <paramref name="value"/>
    /// <br/>
    ///     The radius of the ellipse along the x-axis.
    /// </summary>
    public static HtmlElementModifier Rx(string value) => Modify(x => x.rx = value);

    /// <summary>
    ///     ry = <paramref name="value"/>
    /// <br/>
    ///     The radius of the ellipse along the y-axis.
    /// </summary>
    public static HtmlElementModifier Ry(string value) => Modify(x => x.ry = value);

    /// <summary>
    ///     fill = <paramref name="value"/>
    /// <br/>
    ///     The fill color of the ellipse.
    /// </summary>
    public static HtmlElementModifier Fill(string value) => Modify(x => x.fill = value);

    /// <summary>
    ///     stroke = <paramref name="value"/>
    /// <br/>
    ///     The stroke color of the ellipse.
    /// </summary>
    public static HtmlElementModifier Stroke(string value) => Modify(x => x.stroke = value);

    /// <summary>
    ///     strokeWidth = <paramref name="value"/>
    /// <br/>
    ///     The width of the stroke.
    /// </summary>
    public static HtmlElementModifier StrokeWidth(string value) => Modify(x => x.strokeWidth = value);

}

public sealed class line : HtmlElement
{
    #region string x1
    PropertyValueNode<string> __x1;
    static readonly PropertyValueDefinition _x1 = new()
    {
        name = nameof(x1)
    };
    #endregion
    /// <summary>
    ///     The x-coordinate of the start point of the line.
    /// </summary>
    [ReactProp]
    public string x1 { get; set; }

    #region string y1
    PropertyValueNode<string> __y1;
    static readonly PropertyValueDefinition _y1 = new()
    {
        name = nameof(y1)
    };
    #endregion
    /// <summary>
    ///     The y-coordinate of the start point of the line.
    /// </summary>
    [ReactProp]
    public string y1 { get; set; }

    #region string x2
    PropertyValueNode<string> __x2;
    static readonly PropertyValueDefinition _x2 = new()
    {
        name = nameof(x2)
    };
    #endregion
    /// <summary>
    ///     The x-coordinate of the end point of the line.
    /// </summary>
    [ReactProp]
    public string x2 { get; set; }

    #region string y2
    PropertyValueNode<string> __y2;
    static readonly PropertyValueDefinition _y2 = new()
    {
        name = nameof(y2)
    };
    #endregion
    /// <summary>
    ///     The y-coordinate of the end point of the line.
    /// </summary>
    [ReactProp]
    public string y2 { get; set; }

    #region string stroke
    PropertyValueNode<string> __stroke;
    static readonly PropertyValueDefinition _stroke = new()
    {
        name = nameof(stroke)
    };
    #endregion
    /// <summary>
    ///     The stroke (outline) color of the line.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    #region string strokeWidth
    PropertyValueNode<string> __strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth = new()
    {
        name = nameof(strokeWidth)
    };
    #endregion
    /// <summary>
    ///     The width of the line's outline.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    #region string strokeDasharray
    PropertyValueNode<string> __strokeDasharray;
    static readonly PropertyValueDefinition _strokeDasharray = new()
    {
        name = nameof(strokeDasharray)
    };
    #endregion
    /// <summary>
    ///     Pattern of dashes and gaps used in the line's stroke.
    /// </summary>
    [ReactProp]
    public string strokeDasharray { get; set; }

    #region string strokeLinecap
    PropertyValueNode<string> __strokeLinecap;
    static readonly PropertyValueDefinition _strokeLinecap = new()
    {
        name = nameof(strokeLinecap)
    };
    #endregion
    /// <summary>
    ///     The style of the line's endpoints.
    /// </summary>
    [ReactProp]
    public string strokeLinecap { get; set; }

    #region string strokeLinejoin
    PropertyValueNode<string> __strokeLinejoin;
    static readonly PropertyValueDefinition _strokeLinejoin = new()
    {
        name = nameof(strokeLinejoin)
    };
    #endregion
    /// <summary>
    ///     The style of the line's corners.
    /// </summary>
    [ReactProp]
    public string strokeLinejoin { get; set; }

    #region string strokeOpacity
    PropertyValueNode<string> __strokeOpacity;
    static readonly PropertyValueDefinition _strokeOpacity = new()
    {
        name = nameof(strokeOpacity)
    };
    #endregion
    /// <summary>
    ///     The opacity (transparency) of the line's stroke.
    /// </summary>
    [ReactProp]
    public string strokeOpacity { get; set; }

    public line() { }

    public line(params IModifier[] modifiers) : base(modifiers) { }

    public line(Style style) : base(style) { }

    public line(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<line> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     x1 = <paramref name="value"/>
    /// <br/>
    ///     The x-coordinate of the start point of the line.
    /// </summary>
    public static HtmlElementModifier X1(string value) => Modify(x => x.x1 = value);

    /// <summary>
    ///     y1 = <paramref name="value"/>
    /// <br/>
    ///     The y-coordinate of the start point of the line.
    /// </summary>
    public static HtmlElementModifier Y1(string value) => Modify(x => x.y1 = value);

    /// <summary>
    ///     x2 = <paramref name="value"/>
    /// <br/>
    ///     The x-coordinate of the end point of the line.
    /// </summary>
    public static HtmlElementModifier X2(string value) => Modify(x => x.x2 = value);

    /// <summary>
    ///     y2 = <paramref name="value"/>
    /// <br/>
    ///     The y-coordinate of the end point of the line.
    /// </summary>
    public static HtmlElementModifier Y2(string value) => Modify(x => x.y2 = value);

    /// <summary>
    ///     stroke = <paramref name="value"/>
    /// <br/>
    ///     The stroke (outline) color of the line.
    /// </summary>
    public static HtmlElementModifier Stroke(string value) => Modify(x => x.stroke = value);

    /// <summary>
    ///     strokeWidth = <paramref name="value"/>
    /// <br/>
    ///     The width of the line's outline.
    /// </summary>
    public static HtmlElementModifier StrokeWidth(string value) => Modify(x => x.strokeWidth = value);

    /// <summary>
    ///     strokeDasharray = <paramref name="value"/>
    /// <br/>
    ///     Pattern of dashes and gaps used in the line's stroke.
    /// </summary>
    public static HtmlElementModifier StrokeDasharray(string value) => Modify(x => x.strokeDasharray = value);

    /// <summary>
    ///     strokeLinecap = <paramref name="value"/>
    /// <br/>
    ///     The style of the line's endpoints.
    /// </summary>
    public static HtmlElementModifier StrokeLinecap(string value) => Modify(x => x.strokeLinecap = value);

    /// <summary>
    ///     strokeLinejoin = <paramref name="value"/>
    /// <br/>
    ///     The style of the line's corners.
    /// </summary>
    public static HtmlElementModifier StrokeLinejoin(string value) => Modify(x => x.strokeLinejoin = value);

    /// <summary>
    ///     strokeOpacity = <paramref name="value"/>
    /// <br/>
    ///     The opacity (transparency) of the line's stroke.
    /// </summary>
    public static HtmlElementModifier StrokeOpacity(string value) => Modify(x => x.strokeOpacity = value);

}

public sealed class polyline : HtmlElement
{
    #region string points
    PropertyValueNode<string> __points;
    static readonly PropertyValueDefinition _points = new()
    {
        name = nameof(points)
    };
    #endregion
    /// <summary>
    ///     A list of points defining the vertices of the polyline.
    /// </summary>
    [ReactProp]
    public string points { get; set; }

    #region string fill
    PropertyValueNode<string> __fill;
    static readonly PropertyValueDefinition _fill = new()
    {
        name = nameof(fill)
    };
    #endregion
    /// <summary>
    ///     The fill color of the polyline.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    #region string stroke
    PropertyValueNode<string> __stroke;
    static readonly PropertyValueDefinition _stroke = new()
    {
        name = nameof(stroke)
    };
    #endregion
    /// <summary>
    ///     The stroke (outline) color of the polyline.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    #region string strokeWidth
    PropertyValueNode<string> __strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth = new()
    {
        name = nameof(strokeWidth)
    };
    #endregion
    /// <summary>
    ///     The width of the polyline's outline.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    public polyline() { }

    public polyline(params IModifier[] modifiers) : base(modifiers) { }

    public polyline(Style style) : base(style) { }

    public polyline(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<polyline> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     points = <paramref name="value"/>
    /// <br/>
    ///     A list of points defining the vertices of the polyline.
    /// </summary>
    public static HtmlElementModifier Points(string value) => Modify(x => x.points = value);

    /// <summary>
    ///     fill = <paramref name="value"/>
    /// <br/>
    ///     The fill color of the polyline.
    /// </summary>
    public static HtmlElementModifier Fill(string value) => Modify(x => x.fill = value);

    /// <summary>
    ///     stroke = <paramref name="value"/>
    /// <br/>
    ///     The stroke (outline) color of the polyline.
    /// </summary>
    public static HtmlElementModifier Stroke(string value) => Modify(x => x.stroke = value);

    /// <summary>
    ///     strokeWidth = <paramref name="value"/>
    /// <br/>
    ///     The width of the polyline's outline.
    /// </summary>
    public static HtmlElementModifier StrokeWidth(string value) => Modify(x => x.strokeWidth = value);

}

public sealed class circle : HtmlElement
{
    #region string cx
    PropertyValueNode<UnionProp<string,double>> __cx;
    static readonly PropertyValueDefinition _cx = new()
    {
        name = nameof(cx)
    };
    #endregion
    /// <summary>
    ///     The x-coordinate of the center of the circle.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> cx { get; set; }

    #region string cy
    PropertyValueNode<UnionProp<string,double>> __cy;
    static readonly PropertyValueDefinition _cy = new()
    {
        name = nameof(cy)
    };
    #endregion
    /// <summary>
    ///     The y-coordinate of the center of the circle.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> cy { get; set; }

    #region string r
    PropertyValueNode<UnionProp<string,double>> __r;
    static readonly PropertyValueDefinition _r = new()
    {
        name = nameof(r)
    };
    #endregion
    /// <summary>
    ///     The radius of the circle.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> r { get; set; }

    #region string fill
    PropertyValueNode<string> __fill;
    static readonly PropertyValueDefinition _fill = new()
    {
        name = nameof(fill)
    };
    #endregion
    /// <summary>
    ///     The fill color of the circle.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    #region string stroke
    PropertyValueNode<string> __stroke;
    static readonly PropertyValueDefinition _stroke = new()
    {
        name = nameof(stroke)
    };
    #endregion
    /// <summary>
    ///     The stroke color of the circle.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    #region string strokeWidth
    PropertyValueNode<string> __strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth = new()
    {
        name = nameof(strokeWidth)
    };
    #endregion
    /// <summary>
    ///     The width of the stroke of the circle.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    public circle() { }

    public circle(params IModifier[] modifiers) : base(modifiers) { }

    public circle(Style style) : base(style) { }

    public circle(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<circle> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     cx = <paramref name="value"/>
    /// <br/>
    ///     The x-coordinate of the center of the circle.
    /// </summary>
    public static HtmlElementModifier Cx(UnionProp<string,double> value) => Modify(x => x.cx = value);

    /// <summary>
    ///     cy = <paramref name="value"/>
    /// <br/>
    ///     The y-coordinate of the center of the circle.
    /// </summary>
    public static HtmlElementModifier Cy(UnionProp<string,double> value) => Modify(x => x.cy = value);

    /// <summary>
    ///     r = <paramref name="value"/>
    /// <br/>
    ///     The radius of the circle.
    /// </summary>
    public static HtmlElementModifier R(UnionProp<string,double> value) => Modify(x => x.r = value);

    /// <summary>
    ///     fill = <paramref name="value"/>
    /// <br/>
    ///     The fill color of the circle.
    /// </summary>
    public static HtmlElementModifier Fill(string value) => Modify(x => x.fill = value);

    /// <summary>
    ///     stroke = <paramref name="value"/>
    /// <br/>
    ///     The stroke color of the circle.
    /// </summary>
    public static HtmlElementModifier Stroke(string value) => Modify(x => x.stroke = value);

    /// <summary>
    ///     strokeWidth = <paramref name="value"/>
    /// <br/>
    ///     The width of the stroke of the circle.
    /// </summary>
    public static HtmlElementModifier StrokeWidth(string value) => Modify(x => x.strokeWidth = value);

}

public sealed class polygon : HtmlElement
{
    #region string points
    PropertyValueNode<string> __points;
    static readonly PropertyValueDefinition _points = new()
    {
        name = nameof(points)
    };
    #endregion
    /// <summary>
    ///     Specifies the coordinates of the polygon's vertices, in (x, y) pairs, separated by commas.
    /// </summary>
    [ReactProp]
    public string points { get; set; }

    #region string fill
    PropertyValueNode<string> __fill;
    static readonly PropertyValueDefinition _fill = new()
    {
        name = nameof(fill)
    };
    #endregion
    /// <summary>
    ///     Specifies the fill color of the polygon.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    #region string stroke
    PropertyValueNode<string> __stroke;
    static readonly PropertyValueDefinition _stroke = new()
    {
        name = nameof(stroke)
    };
    #endregion
    /// <summary>
    ///     Specifies the stroke color of the polygon.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    #region string strokeWidth
    PropertyValueNode<string> __strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth = new()
    {
        name = nameof(strokeWidth)
    };
    #endregion
    /// <summary>
    ///     Specifies the width of the polygon's stroke, in pixels.
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    #region string strokeLinecap
    PropertyValueNode<string> __strokeLinecap;
    static readonly PropertyValueDefinition _strokeLinecap = new()
    {
        name = nameof(strokeLinecap)
    };
    #endregion
    /// <summary>
    ///     Specifies the type of line cap used for the polygon's stroke.
    /// </summary>
    [ReactProp]
    public string strokeLinecap { get; set; }

    #region string strokeLinejoin
    PropertyValueNode<string> __strokeLinejoin;
    static readonly PropertyValueDefinition _strokeLinejoin = new()
    {
        name = nameof(strokeLinejoin)
    };
    #endregion
    /// <summary>
    ///     Specifies the type of line join used for the polygon's stroke.
    /// </summary>
    [ReactProp]
    public string strokeLinejoin { get; set; }

    #region string fillRule
    PropertyValueNode<string> __fillRule;
    static readonly PropertyValueDefinition _fillRule = new()
    {
        name = nameof(fillRule)
    };
    #endregion
    /// <summary>
    ///     Specifies how the polygon is filled.
    /// </summary>
    [ReactProp]
    public string fillRule { get; set; }

    public polygon() { }

    public polygon(params IModifier[] modifiers) : base(modifiers) { }

    public polygon(Style style) : base(style) { }

    public polygon(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<polygon> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     points = <paramref name="value"/>
    /// <br/>
    ///     Specifies the coordinates of the polygon's vertices, in (x, y) pairs, separated by commas.
    /// </summary>
    public static HtmlElementModifier Points(string value) => Modify(x => x.points = value);

    /// <summary>
    ///     fill = <paramref name="value"/>
    /// <br/>
    ///     Specifies the fill color of the polygon.
    /// </summary>
    public static HtmlElementModifier Fill(string value) => Modify(x => x.fill = value);

    /// <summary>
    ///     stroke = <paramref name="value"/>
    /// <br/>
    ///     Specifies the stroke color of the polygon.
    /// </summary>
    public static HtmlElementModifier Stroke(string value) => Modify(x => x.stroke = value);

    /// <summary>
    ///     strokeWidth = <paramref name="value"/>
    /// <br/>
    ///     Specifies the width of the polygon's stroke, in pixels.
    /// </summary>
    public static HtmlElementModifier StrokeWidth(string value) => Modify(x => x.strokeWidth = value);

    /// <summary>
    ///     strokeLinecap = <paramref name="value"/>
    /// <br/>
    ///     Specifies the type of line cap used for the polygon's stroke.
    /// </summary>
    public static HtmlElementModifier StrokeLinecap(string value) => Modify(x => x.strokeLinecap = value);

    /// <summary>
    ///     strokeLinejoin = <paramref name="value"/>
    /// <br/>
    ///     Specifies the type of line join used for the polygon's stroke.
    /// </summary>
    public static HtmlElementModifier StrokeLinejoin(string value) => Modify(x => x.strokeLinejoin = value);

    /// <summary>
    ///     fillRule = <paramref name="value"/>
    /// <br/>
    ///     Specifies how the polygon is filled.
    /// </summary>
    public static HtmlElementModifier FillRule(string value) => Modify(x => x.fillRule = value);

}

public sealed class rect : HtmlElement
{
    #region string x
    PropertyValueNode<UnionProp<string,double>> __x;
    static readonly PropertyValueDefinition _x = new()
    {
        name = nameof(x)
    };
    #endregion
    /// <summary>
    ///     The x-coordinate of the top-left corner of the rectangle.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> x { get; set; }

    #region string y
    PropertyValueNode<UnionProp<string,double>> __y;
    static readonly PropertyValueDefinition _y = new()
    {
        name = nameof(y)
    };
    #endregion
    /// <summary>
    ///     The y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> y { get; set; }

    #region string width
    PropertyValueNode<UnionProp<string,double>> __width;
    static readonly PropertyValueDefinition _width = new()
    {
        name = nameof(width)
    };
    #endregion
    /// <summary>
    ///     The width of the rectangle.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> width { get; set; }

    #region string height
    PropertyValueNode<UnionProp<string,double>> __height;
    static readonly PropertyValueDefinition _height = new()
    {
        name = nameof(height)
    };
    #endregion
    /// <summary>
    ///     The height of the rectangle.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> height { get; set; }

    #region string rx
    PropertyValueNode<UnionProp<string,double>> __rx;
    static readonly PropertyValueDefinition _rx = new()
    {
        name = nameof(rx)
    };
    #endregion
    /// <summary>
    ///     The border radius of the rectangle on the horizontal axis.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> rx { get; set; }

    #region string ry
    PropertyValueNode<UnionProp<string,double>> __ry;
    static readonly PropertyValueDefinition _ry = new()
    {
        name = nameof(ry)
    };
    #endregion
    /// <summary>
    ///     The border radius of the rectangle on the vertical axis.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> ry { get; set; }

    #region string fill
    PropertyValueNode<string> __fill;
    static readonly PropertyValueDefinition _fill = new()
    {
        name = nameof(fill)
    };
    #endregion
    /// <summary>
    ///     The fill color of the rectangle.
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    #region string stroke
    PropertyValueNode<string> __stroke;
    static readonly PropertyValueDefinition _stroke = new()
    {
        name = nameof(stroke)
    };
    #endregion
    /// <summary>
    ///     The stroke color of the rectangle.
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    #region string strokeWidth
    PropertyValueNode<UnionProp<string,double>> __strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth = new()
    {
        name = nameof(strokeWidth)
    };
    #endregion
    /// <summary>
    ///     The width of the rectangle's stroke.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> strokeWidth { get; set; }

    #region string strokeLinecap
    PropertyValueNode<string> __strokeLinecap;
    static readonly PropertyValueDefinition _strokeLinecap = new()
    {
        name = nameof(strokeLinecap)
    };
    #endregion
    /// <summary>
    ///     The linecap style of the rectangle's stroke.
    /// </summary>
    [ReactProp]
    public string strokeLinecap { get; set; }

    #region string strokeLinejoin
    PropertyValueNode<string> __strokeLinejoin;
    static readonly PropertyValueDefinition _strokeLinejoin = new()
    {
        name = nameof(strokeLinejoin)
    };
    #endregion
    /// <summary>
    ///     The linejoin style of the rectangle's stroke.
    /// </summary>
    [ReactProp]
    public string strokeLinejoin { get; set; }

    public rect() { }

    public rect(params IModifier[] modifiers) : base(modifiers) { }

    public rect(Style style) : base(style) { }

    public rect(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<rect> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     x = <paramref name="value"/>
    /// <br/>
    ///     The x-coordinate of the top-left corner of the rectangle.
    /// </summary>
    public static HtmlElementModifier X(UnionProp<string,double> value) => Modify(x => x.x = value);

    /// <summary>
    ///     y = <paramref name="value"/>
    /// <br/>
    ///     The y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    public static HtmlElementModifier Y(UnionProp<string,double> value) => Modify(x => x.y = value);

    /// <summary>
    ///     width = <paramref name="value"/>
    /// <br/>
    ///     The width of the rectangle.
    /// </summary>
    public static HtmlElementModifier Width(UnionProp<string,double> value) => Modify(x => x.width = value);

    /// <summary>
    ///     height = <paramref name="value"/>
    /// <br/>
    ///     The height of the rectangle.
    /// </summary>
    public static HtmlElementModifier Height(UnionProp<string,double> value) => Modify(x => x.height = value);

    /// <summary>
    ///     rx = <paramref name="value"/>
    /// <br/>
    ///     The border radius of the rectangle on the horizontal axis.
    /// </summary>
    public static HtmlElementModifier Rx(UnionProp<string,double> value) => Modify(x => x.rx = value);

    /// <summary>
    ///     ry = <paramref name="value"/>
    /// <br/>
    ///     The border radius of the rectangle on the vertical axis.
    /// </summary>
    public static HtmlElementModifier Ry(UnionProp<string,double> value) => Modify(x => x.ry = value);

    /// <summary>
    ///     fill = <paramref name="value"/>
    /// <br/>
    ///     The fill color of the rectangle.
    /// </summary>
    public static HtmlElementModifier Fill(string value) => Modify(x => x.fill = value);

    /// <summary>
    ///     stroke = <paramref name="value"/>
    /// <br/>
    ///     The stroke color of the rectangle.
    /// </summary>
    public static HtmlElementModifier Stroke(string value) => Modify(x => x.stroke = value);

    /// <summary>
    ///     strokeWidth = <paramref name="value"/>
    /// <br/>
    ///     The width of the rectangle's stroke.
    /// </summary>
    public static HtmlElementModifier StrokeWidth(UnionProp<string,double> value) => Modify(x => x.strokeWidth = value);

    /// <summary>
    ///     strokeLinecap = <paramref name="value"/>
    /// <br/>
    ///     The linecap style of the rectangle's stroke.
    /// </summary>
    public static HtmlElementModifier StrokeLinecap(string value) => Modify(x => x.strokeLinecap = value);

    /// <summary>
    ///     strokeLinejoin = <paramref name="value"/>
    /// <br/>
    ///     The linejoin style of the rectangle's stroke.
    /// </summary>
    public static HtmlElementModifier StrokeLinejoin(string value) => Modify(x => x.strokeLinejoin = value);

}

public sealed class radialGradient : HtmlElement
{
    #region string cx
    PropertyValueNode<string> __cx;
    static readonly PropertyValueDefinition _cx = new()
    {
        name = nameof(cx)
    };
    #endregion
    /// <summary>
    ///     The x-coordinate of the center of the gradient.
    /// </summary>
    [ReactProp]
    public string cx { get; set; }

    #region string cy
    PropertyValueNode<string> __cy;
    static readonly PropertyValueDefinition _cy = new()
    {
        name = nameof(cy)
    };
    #endregion
    /// <summary>
    ///     The y-coordinate of the center of the gradient.
    /// </summary>
    [ReactProp]
    public string cy { get; set; }

    #region string fx
    PropertyValueNode<string> __fx;
    static readonly PropertyValueDefinition _fx = new()
    {
        name = nameof(fx)
    };
    #endregion
    /// <summary>
    ///     The x-coordinate of the focal point of the gradient.
    /// </summary>
    [ReactProp]
    public string fx { get; set; }

    #region string fy
    PropertyValueNode<string> __fy;
    static readonly PropertyValueDefinition _fy = new()
    {
        name = nameof(fy)
    };
    #endregion
    /// <summary>
    ///     The y-coordinate of the focal point of the gradient.
    /// </summary>
    [ReactProp]
    public string fy { get; set; }

    #region string r
    PropertyValueNode<string> __r;
    static readonly PropertyValueDefinition _r = new()
    {
        name = nameof(r)
    };
    #endregion
    /// <summary>
    ///     The radius of the gradient.
    /// </summary>
    [ReactProp]
    public string r { get; set; }

    #region string spreadMethod
    PropertyValueNode<string> __spreadMethod;
    static readonly PropertyValueDefinition _spreadMethod = new()
    {
        name = nameof(spreadMethod)
    };
    #endregion
    /// <summary>
    ///     The method used to spread the gradient.
    /// </summary>
    [ReactProp]
    public string spreadMethod { get; set; }

    #region string gradientUnits
    PropertyValueNode<string> __gradientUnits;
    static readonly PropertyValueDefinition _gradientUnits = new()
    {
        name = nameof(gradientUnits)
    };
    #endregion
    /// <summary>
    ///     The units used to specify the gradient.
    /// </summary>
    [ReactProp]
    public string gradientUnits { get; set; }

    #region string gradientTransform
    PropertyValueNode<string> __gradientTransform;
    static readonly PropertyValueDefinition _gradientTransform = new()
    {
        name = nameof(gradientTransform)
    };
    #endregion
    /// <summary>
    ///     A transform to apply to the gradient.
    /// </summary>
    [ReactProp]
    public string gradientTransform { get; set; }

    public radialGradient() { }

    public radialGradient(params IModifier[] modifiers) : base(modifiers) { }

    public radialGradient(Style style) : base(style) { }

    public radialGradient(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<radialGradient> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     cx = <paramref name="value"/>
    /// <br/>
    ///     The x-coordinate of the center of the gradient.
    /// </summary>
    public static HtmlElementModifier Cx(string value) => Modify(x => x.cx = value);

    /// <summary>
    ///     cy = <paramref name="value"/>
    /// <br/>
    ///     The y-coordinate of the center of the gradient.
    /// </summary>
    public static HtmlElementModifier Cy(string value) => Modify(x => x.cy = value);

    /// <summary>
    ///     fx = <paramref name="value"/>
    /// <br/>
    ///     The x-coordinate of the focal point of the gradient.
    /// </summary>
    public static HtmlElementModifier Fx(string value) => Modify(x => x.fx = value);

    /// <summary>
    ///     fy = <paramref name="value"/>
    /// <br/>
    ///     The y-coordinate of the focal point of the gradient.
    /// </summary>
    public static HtmlElementModifier Fy(string value) => Modify(x => x.fy = value);

    /// <summary>
    ///     r = <paramref name="value"/>
    /// <br/>
    ///     The radius of the gradient.
    /// </summary>
    public static HtmlElementModifier R(string value) => Modify(x => x.r = value);

    /// <summary>
    ///     spreadMethod = <paramref name="value"/>
    /// <br/>
    ///     The method used to spread the gradient.
    /// </summary>
    public static HtmlElementModifier SpreadMethod(string value) => Modify(x => x.spreadMethod = value);

    /// <summary>
    ///     gradientUnits = <paramref name="value"/>
    /// <br/>
    ///     The units used to specify the gradient.
    /// </summary>
    public static HtmlElementModifier GradientUnits(string value) => Modify(x => x.gradientUnits = value);

    /// <summary>
    ///     gradientTransform = <paramref name="value"/>
    /// <br/>
    ///     A transform to apply to the gradient.
    /// </summary>
    public static HtmlElementModifier GradientTransform(string value) => Modify(x => x.gradientTransform = value);

}

public sealed class clipPath : HtmlElement
{
    #region string clipRule
    PropertyValueNode<string> __clipRule;
    static readonly PropertyValueDefinition _clipRule = new()
    {
        name = nameof(clipRule)
    };
    #endregion
    /// <summary>
    ///     Specifies the fill rule for the clipping path.
    /// </summary>
    [ReactProp]
    public string clipRule { get; set; }

    #region string clipBox
    PropertyValueNode<string> __clipBox;
    static readonly PropertyValueDefinition _clipBox = new()
    {
        name = nameof(clipBox)
    };
    #endregion
    /// <summary>
    ///     Specifies the reference box for the clipping path.
    /// </summary>
    [ReactProp]
    public string clipBox { get; set; }

    public clipPath() { }

    public clipPath(params IModifier[] modifiers) : base(modifiers) { }

    public clipPath(Style style) : base(style) { }

    public clipPath(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<clipPath> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     clipRule = <paramref name="value"/>
    /// <br/>
    ///     Specifies the fill rule for the clipping path.
    /// </summary>
    public static HtmlElementModifier ClipRule(string value) => Modify(x => x.clipRule = value);

    /// <summary>
    ///     clipBox = <paramref name="value"/>
    /// <br/>
    ///     Specifies the reference box for the clipping path.
    /// </summary>
    public static HtmlElementModifier ClipBox(string value) => Modify(x => x.clipBox = value);

}

public sealed class path : HtmlElement
{
    #region string d
    PropertyValueNode<string> __d;
    static readonly PropertyValueDefinition _d = new()
    {
        name = nameof(d)
    };
    #endregion
    /// <summary>
    ///     Path data
    /// </summary>
    [ReactProp]
    public string d { get; set; }

    #region string fill
    PropertyValueNode<string> __fill;
    static readonly PropertyValueDefinition _fill = new()
    {
        name = nameof(fill)
    };
    #endregion
    /// <summary>
    ///     Fill color
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    #region string stroke
    PropertyValueNode<string> __stroke;
    static readonly PropertyValueDefinition _stroke = new()
    {
        name = nameof(stroke)
    };
    #endregion
    /// <summary>
    ///     Stroke color
    /// </summary>
    [ReactProp]
    public string stroke { get; set; }

    #region string strokeWidth
    PropertyValueNode<string> __strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth = new()
    {
        name = nameof(strokeWidth)
    };
    #endregion
    /// <summary>
    ///     Stroke width
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    #region string fillRule
    PropertyValueNode<string> __fillRule;
    static readonly PropertyValueDefinition _fillRule = new()
    {
        name = nameof(fillRule)
    };
    #endregion
    [ReactProp]
    public string fillRule { get; set; }

    #region string clipRule
    PropertyValueNode<string> __clipRule;
    static readonly PropertyValueDefinition _clipRule = new()
    {
        name = nameof(clipRule)
    };
    #endregion
    [ReactProp]
    public string clipRule { get; set; }

    #region string strokeLinecap
    PropertyValueNode<string> __strokeLinecap;
    static readonly PropertyValueDefinition _strokeLinecap = new()
    {
        name = nameof(strokeLinecap)
    };
    #endregion
    [ReactProp]
    public string strokeLinecap { get; set; }

    #region string strokeLinejoin
    PropertyValueNode<string> __strokeLinejoin;
    static readonly PropertyValueDefinition _strokeLinejoin = new()
    {
        name = nameof(strokeLinejoin)
    };
    #endregion
    [ReactProp]
    public string strokeLinejoin { get; set; }

    public path() { }

    public path(params IModifier[] modifiers) : base(modifiers) { }

    public path(Style style) : base(style) { }

    public path(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<path> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     d = <paramref name="value"/>
    /// <br/>
    ///     Path data
    /// </summary>
    public static HtmlElementModifier D(string value) => Modify(x => x.d = value);

    /// <summary>
    ///     fill = <paramref name="value"/>
    /// <br/>
    ///     Fill color
    /// </summary>
    public static HtmlElementModifier Fill(string value) => Modify(x => x.fill = value);

    /// <summary>
    ///     stroke = <paramref name="value"/>
    /// <br/>
    ///     Stroke color
    /// </summary>
    public static HtmlElementModifier Stroke(string value) => Modify(x => x.stroke = value);

    /// <summary>
    ///     strokeWidth = <paramref name="value"/>
    /// <br/>
    ///     Stroke width
    /// </summary>
    public static HtmlElementModifier StrokeWidth(string value) => Modify(x => x.strokeWidth = value);

    public static HtmlElementModifier FillRule(string value) => Modify(x => x.fillRule = value);

    public static HtmlElementModifier ClipRule(string value) => Modify(x => x.clipRule = value);

    public static HtmlElementModifier StrokeLinecap(string value) => Modify(x => x.strokeLinecap = value);

    public static HtmlElementModifier StrokeLinejoin(string value) => Modify(x => x.strokeLinejoin = value);

}

public sealed class g : HtmlElement
{
    #region string opacity
    PropertyValueNode<string> __opacity;
    static readonly PropertyValueDefinition _opacity = new()
    {
        name = nameof(opacity)
    };
    #endregion
    [ReactProp]
    public string opacity { get; set; }

    #region string clipPath
    PropertyValueNode<string> __clipPath;
    static readonly PropertyValueDefinition _clipPath = new()
    {
        name = nameof(clipPath)
    };
    #endregion
    [ReactProp]
    public string clipPath { get; set; }

    #region string transform
    PropertyValueNode<string> __transform;
    static readonly PropertyValueDefinition _transform = new()
    {
        name = nameof(transform)
    };
    #endregion
    [ReactProp]
    public string transform { get; set; }

    public g() { }

    public g(params IModifier[] modifiers) : base(modifiers) { }

    public g(Style style) : base(style) { }

    public g(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<g> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Opacity(string value) => Modify(x => x.opacity = value);

    public static HtmlElementModifier ClipPath(string value) => Modify(x => x.clipPath = value);

    public static HtmlElementModifier Transform(string value) => Modify(x => x.transform = value);

}

public sealed class mask : HtmlElement
{
    #region string height
    PropertyValueNode<string> __height;
    static readonly PropertyValueDefinition _height = new()
    {
        name = nameof(height)
    };
    #endregion
    /// <summary>
    ///     This attribute defines the height of the masking area. Value type: length ; Default value: 120%; Animatable: yes
    /// </summary>
    [ReactProp]
    public string height { get; set; }

    #region string maskContentUnits
    PropertyValueNode<string> __maskContentUnits;
    static readonly PropertyValueDefinition _maskContentUnits = new()
    {
        name = nameof(maskContentUnits)
    };
    #endregion
    /// <summary>
    ///     This attribute defines the coordinate system for the contents of the mask. Value type: userSpaceOnUse|objectBoundingBox ; Default value: userSpaceOnUse; Animatable: yes
    /// </summary>
    [ReactProp]
    public string maskContentUnits { get; set; }

    #region string maskUnits
    PropertyValueNode<string> __maskUnits;
    static readonly PropertyValueDefinition _maskUnits = new()
    {
        name = nameof(maskUnits)
    };
    #endregion
    /// <summary>
    ///     This attribute defines the coordinate system for attributes x, y, width and height on the mask. Value type: userSpaceOnUse|objectBoundingBox ; Default value: objectBoundingBox; Animatable: yes
    /// </summary>
    [ReactProp]
    public string maskUnits { get; set; }

    #region string x
    PropertyValueNode<string> __x;
    static readonly PropertyValueDefinition _x = new()
    {
        name = nameof(x)
    };
    #endregion
    /// <summary>
    ///     This attribute defines the x-axis coordinate of the top-left corner of the masking area. Value type: 'coordinate' ; Default value: -10%; Animatable: yes
    /// </summary>
    [ReactProp]
    public string x { get; set; }

    #region string y
    PropertyValueNode<string> __y;
    static readonly PropertyValueDefinition _y = new()
    {
        name = nameof(y)
    };
    #endregion
    /// <summary>
    ///     This attribute defines the y-axis coordinate of the top-left corner of the masking area. Value type: 'coordinate' ; Default value: -10%; Animatable: yes
    /// </summary>
    [ReactProp]
    public string y { get; set; }

    #region string width
    PropertyValueNode<string> __width;
    static readonly PropertyValueDefinition _width = new()
    {
        name = nameof(width)
    };
    #endregion
    /// <summary>
    ///     This attribute defines the width of the masking area. Value type: 'length' ; Default value: 120%; Animatable: yes
    /// </summary>
    [ReactProp]
    public string width { get; set; }

    public mask() { }

    public mask(params IModifier[] modifiers) : base(modifiers) { }

    public mask(Style style) : base(style) { }

    public mask(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<mask> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     height = <paramref name="value"/>
    /// <br/>
    ///     This attribute defines the height of the masking area. Value type: length ; Default value: 120%; Animatable: yes
    /// </summary>
    public static HtmlElementModifier Height(string value) => Modify(x => x.height = value);

    /// <summary>
    ///     maskContentUnits = <paramref name="value"/>
    /// <br/>
    ///     This attribute defines the coordinate system for the contents of the mask. Value type: userSpaceOnUse|objectBoundingBox ; Default value: userSpaceOnUse; Animatable: yes
    /// </summary>
    public static HtmlElementModifier MaskContentUnits(string value) => Modify(x => x.maskContentUnits = value);

    /// <summary>
    ///     maskUnits = <paramref name="value"/>
    /// <br/>
    ///     This attribute defines the coordinate system for attributes x, y, width and height on the mask. Value type: userSpaceOnUse|objectBoundingBox ; Default value: objectBoundingBox; Animatable: yes
    /// </summary>
    public static HtmlElementModifier MaskUnits(string value) => Modify(x => x.maskUnits = value);

    /// <summary>
    ///     x = <paramref name="value"/>
    /// <br/>
    ///     This attribute defines the x-axis coordinate of the top-left corner of the masking area. Value type: 'coordinate' ; Default value: -10%; Animatable: yes
    /// </summary>
    public static HtmlElementModifier X(string value) => Modify(x => x.x = value);

    /// <summary>
    ///     y = <paramref name="value"/>
    /// <br/>
    ///     This attribute defines the y-axis coordinate of the top-left corner of the masking area. Value type: 'coordinate' ; Default value: -10%; Animatable: yes
    /// </summary>
    public static HtmlElementModifier Y(string value) => Modify(x => x.y = value);

    /// <summary>
    ///     width = <paramref name="value"/>
    /// <br/>
    ///     This attribute defines the width of the masking area. Value type: 'length' ; Default value: 120%; Animatable: yes
    /// </summary>
    public static HtmlElementModifier Width(string value) => Modify(x => x.width = value);

}

public sealed class meta : HtmlElement
{
    #region string charset
    PropertyValueNode<string> __charset;
    static readonly PropertyValueDefinition _charset = new()
    {
        name = nameof(charset)
    };
    #endregion
    /// <summary>
    ///     Specifies the character encoding of the document.
    /// </summary>
    [ReactProp]
    public string charset { get; set; }

    #region string httpEquiv
    PropertyValueNode<string> __httpEquiv;
    static readonly PropertyValueDefinition _httpEquiv = new()
    {
        name = nameof(httpEquiv)
    };
    #endregion
    /// <summary>
    ///     Specifies the name of the HTTP header that the meta tag should be equivalent to.
    /// </summary>
    [ReactProp]
    public string httpEquiv { get; set; }

    #region string name
    PropertyValueNode<string> __name;
    static readonly PropertyValueDefinition _name = new()
    {
        name = nameof(name)
    };
    #endregion
    /// <summary>
    ///     Specifies the name of the metadata property.
    /// </summary>
    [ReactProp]
    public string name { get; set; }

    #region string content
    PropertyValueNode<string> __content;
    static readonly PropertyValueDefinition _content = new()
    {
        name = nameof(content)
    };
    #endregion
    /// <summary>
    ///     Specifies the value of the metadata property.
    /// </summary>
    [ReactProp]
    public string content { get; set; }

    #region string scheme
    PropertyValueNode<string> __scheme;
    static readonly PropertyValueDefinition _scheme = new()
    {
        name = nameof(scheme)
    };
    #endregion
    /// <summary>
    ///     Specifies the URL scheme for the content attribute of the meta tag.
    /// </summary>
    [ReactProp]
    public string scheme { get; set; }

    #region string itemprop
    PropertyValueNode<string> __itemprop;
    static readonly PropertyValueDefinition _itemprop = new()
    {
        name = nameof(itemprop)
    };
    #endregion
    /// <summary>
    ///     Specifies the Microdata item property that the meta tag represents.
    /// </summary>
    [ReactProp]
    public string itemprop { get; set; }

    #region string property
    PropertyValueNode<string> __property;
    static readonly PropertyValueDefinition _property = new()
    {
        name = nameof(property)
    };
    #endregion
    /// <summary>
    ///     Specifies the schema.org property that the meta tag represents.
    /// </summary>
    [ReactProp]
    public string property { get; set; }

    #region string src
    PropertyValueNode<string> __src;
    static readonly PropertyValueDefinition _src = new()
    {
        name = nameof(src)
    };
    #endregion
    /// <summary>
    ///     Specifies the URL for a resource associated with the meta tag.
    /// </summary>
    [ReactProp]
    public string src { get; set; }

    public meta() { }

    public meta(params IModifier[] modifiers) : base(modifiers) { }

    public meta(Style style) : base(style) { }

    public meta(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<meta> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     charset = <paramref name="value"/>
    /// <br/>
    ///     Specifies the character encoding of the document.
    /// </summary>
    public static HtmlElementModifier Charset(string value) => Modify(x => x.charset = value);

    /// <summary>
    ///     httpEquiv = <paramref name="value"/>
    /// <br/>
    ///     Specifies the name of the HTTP header that the meta tag should be equivalent to.
    /// </summary>
    public static HtmlElementModifier HttpEquiv(string value) => Modify(x => x.httpEquiv = value);

    /// <summary>
    ///     name = <paramref name="value"/>
    /// <br/>
    ///     Specifies the name of the metadata property.
    /// </summary>
    public static HtmlElementModifier Name(string value) => Modify(x => x.name = value);

    /// <summary>
    ///     content = <paramref name="value"/>
    /// <br/>
    ///     Specifies the value of the metadata property.
    /// </summary>
    public static HtmlElementModifier Content(string value) => Modify(x => x.content = value);

    /// <summary>
    ///     scheme = <paramref name="value"/>
    /// <br/>
    ///     Specifies the URL scheme for the content attribute of the meta tag.
    /// </summary>
    public static HtmlElementModifier Scheme(string value) => Modify(x => x.scheme = value);

    /// <summary>
    ///     itemprop = <paramref name="value"/>
    /// <br/>
    ///     Specifies the Microdata item property that the meta tag represents.
    /// </summary>
    public static HtmlElementModifier Itemprop(string value) => Modify(x => x.itemprop = value);

    /// <summary>
    ///     property = <paramref name="value"/>
    /// <br/>
    ///     Specifies the schema.org property that the meta tag represents.
    /// </summary>
    public static HtmlElementModifier Property(string value) => Modify(x => x.property = value);

    /// <summary>
    ///     src = <paramref name="value"/>
    /// <br/>
    ///     Specifies the URL for a resource associated with the meta tag.
    /// </summary>
    public static HtmlElementModifier Src(string value) => Modify(x => x.src = value);

}

public sealed class body : HtmlElement
{
    #region string background
    PropertyValueNode<string> __background;
    static readonly PropertyValueDefinition _background = new()
    {
        name = nameof(background)
    };
    #endregion
    /// <summary>
    ///     Specifies the URL of a background image to be displayed behind the document's content.
    /// </summary>
    [ReactProp]
    public string background { get; set; }

    #region string bgcolor
    PropertyValueNode<string> __bgcolor;
    static readonly PropertyValueDefinition _bgcolor = new()
    {
        name = nameof(bgcolor)
    };
    #endregion
    /// <summary>
    ///     Specifies the background color of the document's body.
    /// </summary>
    [ReactProp]
    public string bgcolor { get; set; }

    #region string link
    PropertyValueNode<string> __link;
    static readonly PropertyValueDefinition _link = new()
    {
        name = nameof(link)
    };
    #endregion
    /// <summary>
    ///     Specifies the color of unvisited links in the document's body.
    /// </summary>
    [ReactProp]
    public string link { get; set; }

    public body() { }

    public body(params IModifier[] modifiers) : base(modifiers) { }

    public body(Style style) : base(style) { }

    public body(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<body> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     background = <paramref name="value"/>
    /// <br/>
    ///     Specifies the URL of a background image to be displayed behind the document's content.
    /// </summary>
    public static HtmlElementModifier Background(string value) => Modify(x => x.background = value);

    /// <summary>
    ///     bgcolor = <paramref name="value"/>
    /// <br/>
    ///     Specifies the background color of the document's body.
    /// </summary>
    public static HtmlElementModifier Bgcolor(string value) => Modify(x => x.bgcolor = value);

    /// <summary>
    ///     link = <paramref name="value"/>
    /// <br/>
    ///     Specifies the color of unvisited links in the document's body.
    /// </summary>
    public static HtmlElementModifier Link(string value) => Modify(x => x.link = value);

}

public sealed class script : HtmlElement
{
    #region string async
    PropertyValueNode<string> __async;
    static readonly PropertyValueDefinition _async = new()
    {
        name = nameof(async)
    };
    #endregion
    /// <summary>
    ///     Specifies that the script should be executed asynchronously. This means that the browser will not wait for the script to finish executing before continuing to parse the rest of the HTML.
    /// </summary>
    [ReactProp]
    public string async { get; set; }

    #region string defer
    PropertyValueNode<string> __defer;
    static readonly PropertyValueDefinition _defer = new()
    {
        name = nameof(defer)
    };
    #endregion
    /// <summary>
    ///     Specifies that the script should be executed after the browser has finished parsing the rest of the HTML. This is similar to async, but it ensures that scripts are executed in the order they are specified in the HTML.
    /// </summary>
    [ReactProp]
    public string defer { get; set; }

    #region string integrity
    PropertyValueNode<string> __integrity;
    static readonly PropertyValueDefinition _integrity = new()
    {
        name = nameof(integrity)
    };
    #endregion
    /// <summary>
    ///     Specifies a subresource integrity (SRI) hash for the script. This helps to protect against man-in-the-middle attacks.
    /// </summary>
    [ReactProp]
    public string integrity { get; set; }

    #region string language
    PropertyValueNode<string> __language;
    static readonly PropertyValueDefinition _language = new()
    {
        name = nameof(language)
    };
    #endregion
    /// <summary>
    ///     Specifies the scripting language of the script. This is deprecated, but is still supported by most browsers.
    /// </summary>
    [ReactProp]
    public string language { get; set; }

    #region string nomodule
    PropertyValueNode<string> __nomodule;
    static readonly PropertyValueDefinition _nomodule = new()
    {
        name = nameof(nomodule)
    };
    #endregion
    /// <summary>
    ///     Specifies that the script should be ignored if the browser does not support modules.
    /// </summary>
    [ReactProp]
    public string nomodule { get; set; }

    #region string src
    PropertyValueNode<string> __src;
    static readonly PropertyValueDefinition _src = new()
    {
        name = nameof(src)
    };
    #endregion
    /// <summary>
    ///     Specifies the URL of an external script file.
    /// </summary>
    [ReactProp]
    public string src { get; set; }

    #region string type
    PropertyValueNode<string> __type;
    static readonly PropertyValueDefinition _type = new()
    {
        name = nameof(type)
    };
    #endregion
    /// <summary>
    ///     Specifies the type of the script. The most common value is application/javascript.
    /// </summary>
    [ReactProp]
    public string type { get; set; }

    public script() { }

    public script(params IModifier[] modifiers) : base(modifiers) { }

    public script(Style style) : base(style) { }

    public script(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<script> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     async = <paramref name="value"/>
    /// <br/>
    ///     Specifies that the script should be executed asynchronously. This means that the browser will not wait for the script to finish executing before continuing to parse the rest of the HTML.
    /// </summary>
    public static HtmlElementModifier Async(string value) => Modify(x => x.async = value);

    /// <summary>
    ///     defer = <paramref name="value"/>
    /// <br/>
    ///     Specifies that the script should be executed after the browser has finished parsing the rest of the HTML. This is similar to async, but it ensures that scripts are executed in the order they are specified in the HTML.
    /// </summary>
    public static HtmlElementModifier Defer(string value) => Modify(x => x.defer = value);

    /// <summary>
    ///     integrity = <paramref name="value"/>
    /// <br/>
    ///     Specifies a subresource integrity (SRI) hash for the script. This helps to protect against man-in-the-middle attacks.
    /// </summary>
    public static HtmlElementModifier Integrity(string value) => Modify(x => x.integrity = value);

    /// <summary>
    ///     language = <paramref name="value"/>
    /// <br/>
    ///     Specifies the scripting language of the script. This is deprecated, but is still supported by most browsers.
    /// </summary>
    public static HtmlElementModifier Language(string value) => Modify(x => x.language = value);

    /// <summary>
    ///     nomodule = <paramref name="value"/>
    /// <br/>
    ///     Specifies that the script should be ignored if the browser does not support modules.
    /// </summary>
    public static HtmlElementModifier Nomodule(string value) => Modify(x => x.nomodule = value);

    /// <summary>
    ///     src = <paramref name="value"/>
    /// <br/>
    ///     Specifies the URL of an external script file.
    /// </summary>
    public static HtmlElementModifier Src(string value) => Modify(x => x.src = value);

    /// <summary>
    ///     type = <paramref name="value"/>
    /// <br/>
    ///     Specifies the type of the script. The most common value is application/javascript.
    /// </summary>
    public static HtmlElementModifier Type(string value) => Modify(x => x.type = value);

}

public sealed class title : HtmlElement
{
    #region string language
    PropertyValueNode<string> __language;
    static readonly PropertyValueDefinition _language = new()
    {
        name = nameof(language)
    };
    #endregion
    /// <summary>
    ///     Specifies the language of the title.
    /// </summary>
    [ReactProp]
    public string language { get; set; }

    public title() { }

    public title(params IModifier[] modifiers) : base(modifiers) { }

    public title(Style style) : base(style) { }

    public title(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<title> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     language = <paramref name="value"/>
    /// <br/>
    ///     Specifies the language of the title.
    /// </summary>
    public static HtmlElementModifier Language(string value) => Modify(x => x.language = value);

}

public sealed class head : HtmlElement
{
    #region string profile
    PropertyValueNode<string> __profile;
    static readonly PropertyValueDefinition _profile = new()
    {
        name = nameof(profile)
    };
    #endregion
    /// <summary>
    ///     Provides a URL to a profile document for the current document.
    /// </summary>
    [ReactProp]
    public string profile { get; set; }

    #region string link
    PropertyValueNode<string> __link;
    static readonly PropertyValueDefinition _link = new()
    {
        name = nameof(link)
    };
    #endregion
    /// <summary>
    ///     Provides a link to an external resource, such as a stylesheet or script file.
    /// </summary>
    [ReactProp]
    public string link { get; set; }

    #region string meta
    PropertyValueNode<string> __meta;
    static readonly PropertyValueDefinition _meta = new()
    {
        name = nameof(meta)
    };
    #endregion
    /// <summary>
    ///     Provides metadata about the document, such as the character encoding, author, and keywords.
    /// </summary>
    [ReactProp]
    public string meta { get; set; }

    #region string script
    PropertyValueNode<string> __script;
    static readonly PropertyValueDefinition _script = new()
    {
        name = nameof(script)
    };
    #endregion
    /// <summary>
    ///     Provides JavaScript code to be executed in the browser.
    /// </summary>
    [ReactProp]
    public string script { get; set; }

    #region string noscript
    PropertyValueNode<string> __noscript;
    static readonly PropertyValueDefinition _noscript = new()
    {
        name = nameof(noscript)
    };
    #endregion
    /// <summary>
    ///     Provides content to be displayed if the browser does not support JavaScript.
    /// </summary>
    [ReactProp]
    public string noscript { get; set; }

    public head() { }

    public head(params IModifier[] modifiers) : base(modifiers) { }

    public head(Style style) : base(style) { }

    public head(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<head> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     profile = <paramref name="value"/>
    /// <br/>
    ///     Provides a URL to a profile document for the current document.
    /// </summary>
    public static HtmlElementModifier Profile(string value) => Modify(x => x.profile = value);

    /// <summary>
    ///     link = <paramref name="value"/>
    /// <br/>
    ///     Provides a link to an external resource, such as a stylesheet or script file.
    /// </summary>
    public static HtmlElementModifier Link(string value) => Modify(x => x.link = value);

    /// <summary>
    ///     meta = <paramref name="value"/>
    /// <br/>
    ///     Provides metadata about the document, such as the character encoding, author, and keywords.
    /// </summary>
    public static HtmlElementModifier Meta(string value) => Modify(x => x.meta = value);

    /// <summary>
    ///     script = <paramref name="value"/>
    /// <br/>
    ///     Provides JavaScript code to be executed in the browser.
    /// </summary>
    public static HtmlElementModifier Script(string value) => Modify(x => x.script = value);

    /// <summary>
    ///     noscript = <paramref name="value"/>
    /// <br/>
    ///     Provides content to be displayed if the browser does not support JavaScript.
    /// </summary>
    public static HtmlElementModifier Noscript(string value) => Modify(x => x.noscript = value);

}

public sealed class html : HtmlElement
{
    #region string hidden
    PropertyValueNode<string> __hidden;
    static readonly PropertyValueDefinition _hidden = new()
    {
        name = nameof(hidden)
    };
    #endregion
    /// <summary>
    ///     Hides the element from display.
    /// </summary>
    [ReactProp]
    public string hidden { get; set; }

    #region string manifest
    PropertyValueNode<string> __manifest;
    static readonly PropertyValueDefinition _manifest = new()
    {
        name = nameof(manifest)
    };
    #endregion
    /// <summary>
    ///     Specifies the URL of a manifest file, which provides information about the web app.
    /// </summary>
    [ReactProp]
    public string manifest { get; set; }

    #region string xmlns
    PropertyValueNode<string> __xmlns;
    static readonly PropertyValueDefinition _xmlns = new()
    {
        name = nameof(xmlns)
    };
    #endregion
    /// <summary>
    ///     Specifies the namespace of the element.
    /// </summary>
    [ReactProp]
    public string xmlns { get; set; } = "http://www.w3.org/1999/xhtml";

    #region string prefix
    PropertyValueNode<string> __prefix;
    static readonly PropertyValueDefinition _prefix = new()
    {
        name = nameof(prefix)
    };
    #endregion
    /// <summary>
    ///     Specifies the prefix of the element.
    /// </summary>
    [ReactProp]
    public string prefix { get; set; }

    #region string version
    PropertyValueNode<string> __version;
    static readonly PropertyValueDefinition _version = new()
    {
        name = nameof(version)
    };
    #endregion
    /// <summary>
    ///     Specifies the version of the HTML specification to which the element conforms.
    /// </summary>
    [ReactProp]
    public string version { get; set; }

    public html() { }

    public html(params IModifier[] modifiers) : base(modifiers) { }

    public html(Style style) : base(style) { }

    public html(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<html> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     hidden = <paramref name="value"/>
    /// <br/>
    ///     Hides the element from display.
    /// </summary>
    public static HtmlElementModifier Hidden(string value) => Modify(x => x.hidden = value);

    /// <summary>
    ///     manifest = <paramref name="value"/>
    /// <br/>
    ///     Specifies the URL of a manifest file, which provides information about the web app.
    /// </summary>
    public static HtmlElementModifier Manifest(string value) => Modify(x => x.manifest = value);

    /// <summary>
    ///     xmlns = <paramref name="value"/>
    /// <br/>
    ///     Specifies the namespace of the element.
    /// </summary>
    public static HtmlElementModifier Xmlns(string value) => Modify(x => x.xmlns = value);

    /// <summary>
    ///     prefix = <paramref name="value"/>
    /// <br/>
    ///     Specifies the prefix of the element.
    /// </summary>
    public static HtmlElementModifier Prefix(string value) => Modify(x => x.prefix = value);

    /// <summary>
    ///     version = <paramref name="value"/>
    /// <br/>
    ///     Specifies the version of the HTML specification to which the element conforms.
    /// </summary>
    public static HtmlElementModifier Version(string value) => Modify(x => x.version = value);

}

public sealed class label : HtmlElement
{
    #region string htmlFor
    PropertyValueNode<string> __htmlFor;
    static readonly PropertyValueDefinition _htmlFor = new()
    {
        name = nameof(htmlFor)
    };
    #endregion
    /// <summary>
    ///     Specifies which form element a label is bound to.
    /// </summary>
    [ReactProp]
    public string htmlFor { get; set; }

    #region string dropzone
    PropertyValueNode<string> __dropzone;
    static readonly PropertyValueDefinition _dropzone = new()
    {
        name = nameof(dropzone)
    };
    #endregion
    /// <summary>
    ///     Specifies whether the element is a drop target.
    /// </summary>
    [ReactProp]
    public string dropzone { get; set; }

    #region string hidden
    PropertyValueNode<string> __hidden;
    static readonly PropertyValueDefinition _hidden = new()
    {
        name = nameof(hidden)
    };
    #endregion
    /// <summary>
    ///     Hides the element from view.
    /// </summary>
    [ReactProp]
    public string hidden { get; set; }

    #region string tabindex
    PropertyValueNode<string> __tabindex;
    static readonly PropertyValueDefinition _tabindex = new()
    {
        name = nameof(tabindex)
    };
    #endregion
    /// <summary>
    ///     Specifies the element's position in the tab order.
    /// </summary>
    [ReactProp]
    public string tabindex { get; set; }


    public label(string innerText) : base(innerText) {  }

    public static implicit operator label(string text) => new() { text = text };
    public label() { }

    public label(params IModifier[] modifiers) : base(modifiers) { }

    public label(Style style) : base(style) { }

    public label(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<label> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     htmlFor = <paramref name="value"/>
    /// <br/>
    ///     Specifies which form element a label is bound to.
    /// </summary>
    public static HtmlElementModifier HtmlFor(string value) => Modify(x => x.htmlFor = value);

    /// <summary>
    ///     dropzone = <paramref name="value"/>
    /// <br/>
    ///     Specifies whether the element is a drop target.
    /// </summary>
    public static HtmlElementModifier Dropzone(string value) => Modify(x => x.dropzone = value);

    /// <summary>
    ///     hidden = <paramref name="value"/>
    /// <br/>
    ///     Hides the element from view.
    /// </summary>
    public static HtmlElementModifier Hidden(string value) => Modify(x => x.hidden = value);

    /// <summary>
    ///     tabindex = <paramref name="value"/>
    /// <br/>
    ///     Specifies the element's position in the tab order.
    /// </summary>
    public static HtmlElementModifier Tabindex(string value) => Modify(x => x.tabindex = value);

}

public sealed class a : HtmlElement
{
    #region string href
    PropertyValueNode<string> __href;
    static readonly PropertyValueDefinition _href = new()
    {
        name = nameof(href)
    };
    #endregion
    /// <summary>
    ///     The URL of the linked resource.
    /// </summary>
    [ReactProp]
    public string href { get; set; }

    #region string target
    PropertyValueNode<string> __target;
    static readonly PropertyValueDefinition _target = new()
    {
        name = nameof(target)
    };
    #endregion
    /// <summary>
    ///     Specifies where the linked resource should be opened. Can be `_blank`, `_self`, `_parent`, or `_top`.
    /// </summary>
    [ReactProp]
    public string target { get; set; }

    #region string rel
    PropertyValueNode<string> __rel;
    static readonly PropertyValueDefinition _rel = new()
    {
        name = nameof(rel)
    };
    #endregion
    /// <summary>
    ///     Specifies the relationship between the current document and the linked resource. Can be `alternate`, `author`, `bookmark`, `canonical`, `external`, `help`, `license`, `next`, `nofollow`, `noreferrer`, `noopener`, `prev`, `search`, `sponsored`, or `stylesheet`.
    /// </summary>
    [ReactProp]
    public string rel { get; set; }

    #region string type
    PropertyValueNode<string> __type;
    static readonly PropertyValueDefinition _type = new()
    {
        name = nameof(type)
    };
    #endregion
    /// <summary>
    ///     Specifies the MIME type of the linked resource, if applicable.
    /// </summary>
    [ReactProp]
    public string type { get; set; }

    #region string download
    PropertyValueNode<string> __download;
    static readonly PropertyValueDefinition _download = new()
    {
        name = nameof(download)
    };
    #endregion
    /// <summary>
    ///     Specifies whether the linked resource should be downloaded or opened in a new browser tab.
    /// </summary>
    [ReactProp]
    public string download { get; set; }

    #region string ping
    PropertyValueNode<string> __ping;
    static readonly PropertyValueDefinition _ping = new()
    {
        name = nameof(ping)
    };
    #endregion
    /// <summary>
    ///     A list of URLs to which a ping should be sent when the user clicks on the link.
    /// </summary>
    [ReactProp]
    public string ping { get; set; }

    #region string media
    PropertyValueNode<string> __media;
    static readonly PropertyValueDefinition _media = new()
    {
        name = nameof(media)
    };
    #endregion
    /// <summary>
    ///     Specifies the media types for which the link is relevant.
    /// </summary>
    [ReactProp]
    public string media { get; set; }

    #region string hreflang
    PropertyValueNode<string> __hreflang;
    static readonly PropertyValueDefinition _hreflang = new()
    {
        name = nameof(hreflang)
    };
    #endregion
    /// <summary>
    ///     Specifies the language of the linked resource.
    /// </summary>
    [ReactProp]
    public string hreflang { get; set; }

    #region string name
    PropertyValueNode<string> __name;
    static readonly PropertyValueDefinition _name = new()
    {
        name = nameof(name)
    };
    #endregion
    /// <summary>
    ///     Specifies a name for the link. This can be used to target the link with JavaScript.
    /// </summary>
    [ReactProp]
    public string name { get; set; }

    #region string tabindex
    PropertyValueNode<string> __tabindex;
    static readonly PropertyValueDefinition _tabindex = new()
    {
        name = nameof(tabindex)
    };
    #endregion
    /// <summary>
    ///     Specifies the tab order of the link.
    /// </summary>
    [ReactProp]
    public string tabindex { get; set; }

    public a() { }

    public a(params IModifier[] modifiers) : base(modifiers) { }

    public a(Style style) : base(style) { }

    public a(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<a> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     href = <paramref name="value"/>
    /// <br/>
    ///     The URL of the linked resource.
    /// </summary>
    public static HtmlElementModifier Href(string value) => Modify(x => x.href = value);

    /// <summary>
    ///     target = <paramref name="value"/>
    /// <br/>
    ///     Specifies where the linked resource should be opened. Can be `_blank`, `_self`, `_parent`, or `_top`.
    /// </summary>
    public static HtmlElementModifier Target(string value) => Modify(x => x.target = value);

    /// <summary>
    ///     rel = <paramref name="value"/>
    /// <br/>
    ///     Specifies the relationship between the current document and the linked resource. Can be `alternate`, `author`, `bookmark`, `canonical`, `external`, `help`, `license`, `next`, `nofollow`, `noreferrer`, `noopener`, `prev`, `search`, `sponsored`, or `stylesheet`.
    /// </summary>
    public static HtmlElementModifier Rel(string value) => Modify(x => x.rel = value);

    /// <summary>
    ///     type = <paramref name="value"/>
    /// <br/>
    ///     Specifies the MIME type of the linked resource, if applicable.
    /// </summary>
    public static HtmlElementModifier Type(string value) => Modify(x => x.type = value);

    /// <summary>
    ///     download = <paramref name="value"/>
    /// <br/>
    ///     Specifies whether the linked resource should be downloaded or opened in a new browser tab.
    /// </summary>
    public static HtmlElementModifier Download(string value) => Modify(x => x.download = value);

    /// <summary>
    ///     ping = <paramref name="value"/>
    /// <br/>
    ///     A list of URLs to which a ping should be sent when the user clicks on the link.
    /// </summary>
    public static HtmlElementModifier Ping(string value) => Modify(x => x.ping = value);

    /// <summary>
    ///     media = <paramref name="value"/>
    /// <br/>
    ///     Specifies the media types for which the link is relevant.
    /// </summary>
    public static HtmlElementModifier Media(string value) => Modify(x => x.media = value);

    /// <summary>
    ///     hreflang = <paramref name="value"/>
    /// <br/>
    ///     Specifies the language of the linked resource.
    /// </summary>
    public static HtmlElementModifier Hreflang(string value) => Modify(x => x.hreflang = value);

    /// <summary>
    ///     name = <paramref name="value"/>
    /// <br/>
    ///     Specifies a name for the link. This can be used to target the link with JavaScript.
    /// </summary>
    public static HtmlElementModifier Name(string value) => Modify(x => x.name = value);

    /// <summary>
    ///     tabindex = <paramref name="value"/>
    /// <br/>
    ///     Specifies the tab order of the link.
    /// </summary>
    public static HtmlElementModifier Tabindex(string value) => Modify(x => x.tabindex = value);

}

public sealed class img : HtmlElement
{
    #region string src
    PropertyValueNode<string> __src;
    static readonly PropertyValueDefinition _src = new()
    {
        name = nameof(src)
    };
    #endregion
    /// <summary>
    ///     The URL of the image file.
    /// </summary>
    [ReactProp]
    public string src { get; set; }

    #region string srcset
    PropertyValueNode<string> __srcset;
    static readonly PropertyValueDefinition _srcset = new()
    {
        name = nameof(srcset)
    };
    #endregion
    /// <summary>
    ///     A list of image files to use in different situations, such as different screen sizes or device types.
    /// </summary>
    [ReactProp]
    public string srcset { get; set; }

    #region string usemap
    PropertyValueNode<string> __usemap;
    static readonly PropertyValueDefinition _usemap = new()
    {
        name = nameof(usemap)
    };
    #endregion
    /// <summary>
    ///     Specifies an image as a client-side image map.
    /// </summary>
    [ReactProp]
    public string usemap { get; set; }

    #region string alt
    PropertyValueNode<string> __alt;
    static readonly PropertyValueDefinition _alt = new()
    {
        name = nameof(alt)
    };
    #endregion
    /// <summary>
    ///     An alternate text for the image, if the image for some reason cannot be displayed.
    /// </summary>
    [ReactProp]
    public string alt { get; set; }

    #region string width
    PropertyValueNode<UnionProp<string,double?>> __width;
    static readonly PropertyValueDefinition _width = new()
    {
        name = nameof(width)
    };
    #endregion
    /// <summary>
    ///     The width of the image, in pixels.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double?> width { get; set; }

    #region string height
    PropertyValueNode<UnionProp<string,double?>> __height;
    static readonly PropertyValueDefinition _height = new()
    {
        name = nameof(height)
    };
    #endregion
    /// <summary>
    ///     The height of the image, in pixels.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double?> height { get; set; }

    #region string ismap
    PropertyValueNode<string> __ismap;
    static readonly PropertyValueDefinition _ismap = new()
    {
        name = nameof(ismap)
    };
    #endregion
    /// <summary>
    ///     A Boolean attribute that indicates whether the image is an image map.
    /// </summary>
    [ReactProp]
    public string ismap { get; set; }

    #region string longdesc
    PropertyValueNode<string> __longdesc;
    static readonly PropertyValueDefinition _longdesc = new()
    {
        name = nameof(longdesc)
    };
    #endregion
    /// <summary>
    ///     A longer description of the image, for use by screen readers and other assistive technologies.
    /// </summary>
    [ReactProp]
    public string longdesc { get; set; }

    #region string crossorigin
    PropertyValueNode<string> __crossorigin;
    static readonly PropertyValueDefinition _crossorigin = new()
    {
        name = nameof(crossorigin)
    };
    #endregion
    /// <summary>
    ///     A string that specifies the CORS setting for the image.
    /// </summary>
    [ReactProp]
    public string crossorigin { get; set; }

    #region string loading
    PropertyValueNode<string> __loading;
    static readonly PropertyValueDefinition _loading = new()
    {
        name = nameof(loading)
    };
    #endregion
    /// <summary>
    ///     A string that specifies how the image should be loaded.
    /// </summary>
    [ReactProp]
    public string loading { get; set; }

    #region string decoding
    PropertyValueNode<string> __decoding;
    static readonly PropertyValueDefinition _decoding = new()
    {
        name = nameof(decoding)
    };
    #endregion
    /// <summary>
    ///     A string that specifies how the image should be decoded.
    /// </summary>
    [ReactProp]
    public string decoding { get; set; }

    #region string referrerpolicy
    PropertyValueNode<string> __referrerpolicy;
    static readonly PropertyValueDefinition _referrerpolicy = new()
    {
        name = nameof(referrerpolicy)
    };
    #endregion
    /// <summary>
    ///     A string that specifies how much referrer information is sent with requests for the image.
    /// </summary>
    [ReactProp]
    public string referrerpolicy { get; set; }

    public img() { }

    public img(params IModifier[] modifiers) : base(modifiers) { }

    public img(Style style) : base(style) { }

    public img(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<img> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     src = <paramref name="value"/>
    /// <br/>
    ///     The URL of the image file.
    /// </summary>
    public static HtmlElementModifier Src(string value) => Modify(x => x.src = value);

    /// <summary>
    ///     srcset = <paramref name="value"/>
    /// <br/>
    ///     A list of image files to use in different situations, such as different screen sizes or device types.
    /// </summary>
    public static HtmlElementModifier Srcset(string value) => Modify(x => x.srcset = value);

    /// <summary>
    ///     usemap = <paramref name="value"/>
    /// <br/>
    ///     Specifies an image as a client-side image map.
    /// </summary>
    public static HtmlElementModifier Usemap(string value) => Modify(x => x.usemap = value);

    /// <summary>
    ///     alt = <paramref name="value"/>
    /// <br/>
    ///     An alternate text for the image, if the image for some reason cannot be displayed.
    /// </summary>
    public static HtmlElementModifier Alt(string value) => Modify(x => x.alt = value);

    /// <summary>
    ///     width = <paramref name="value"/>
    /// <br/>
    ///     The width of the image, in pixels.
    /// </summary>
    public static HtmlElementModifier Width(UnionProp<string,double?> value) => Modify(x => x.width = value);

    /// <summary>
    ///     height = <paramref name="value"/>
    /// <br/>
    ///     The height of the image, in pixels.
    /// </summary>
    public static HtmlElementModifier Height(UnionProp<string,double?> value) => Modify(x => x.height = value);

    /// <summary>
    ///     ismap = <paramref name="value"/>
    /// <br/>
    ///     A Boolean attribute that indicates whether the image is an image map.
    /// </summary>
    public static HtmlElementModifier Ismap(string value) => Modify(x => x.ismap = value);

    /// <summary>
    ///     longdesc = <paramref name="value"/>
    /// <br/>
    ///     A longer description of the image, for use by screen readers and other assistive technologies.
    /// </summary>
    public static HtmlElementModifier Longdesc(string value) => Modify(x => x.longdesc = value);

    /// <summary>
    ///     crossorigin = <paramref name="value"/>
    /// <br/>
    ///     A string that specifies the CORS setting for the image.
    /// </summary>
    public static HtmlElementModifier Crossorigin(string value) => Modify(x => x.crossorigin = value);

    /// <summary>
    ///     loading = <paramref name="value"/>
    /// <br/>
    ///     A string that specifies how the image should be loaded.
    /// </summary>
    public static HtmlElementModifier Loading(string value) => Modify(x => x.loading = value);

    /// <summary>
    ///     decoding = <paramref name="value"/>
    /// <br/>
    ///     A string that specifies how the image should be decoded.
    /// </summary>
    public static HtmlElementModifier Decoding(string value) => Modify(x => x.decoding = value);

    /// <summary>
    ///     referrerpolicy = <paramref name="value"/>
    /// <br/>
    ///     A string that specifies how much referrer information is sent with requests for the image.
    /// </summary>
    public static HtmlElementModifier Referrerpolicy(string value) => Modify(x => x.referrerpolicy = value);

}

public sealed partial class svg : HtmlElement
{
    #region string focusable
    PropertyValueNode<string> __focusable;
    static readonly PropertyValueDefinition _focusable = new()
    {
        name = nameof(focusable)
    };
    #endregion
    [ReactProp]
    public string focusable { get; set; }

    #region string xlinkHref
    PropertyValueNode<string> __xlinkHref;
    static readonly PropertyValueDefinition _xlinkHref = new()
    {
        name = nameof(xlinkHref)
    };
    #endregion
    [ReactProp]
    public string xlinkHref { get; set; }

    #region string xmlnsXlink
    PropertyValueNode<string> __xmlnsXlink;
    static readonly PropertyValueDefinition _xmlnsXlink = new()
    {
        name = nameof(xmlnsXlink)
    };
    #endregion
    [ReactProp]
    public string xmlnsXlink { get; set; }

    #region string preserveAspectRatio
    PropertyValueNode<string> __preserveAspectRatio;
    static readonly PropertyValueDefinition _preserveAspectRatio = new()
    {
        name = nameof(preserveAspectRatio)
    };
    #endregion
    /// <summary>
    ///     Specifies how the SVG element should be scaled and aligned to fit its viewport.
    /// </summary>
    [ReactProp]
    public string preserveAspectRatio { get; set; }

    #region string width
    PropertyValueNode<string> __width;
    static readonly PropertyValueDefinition _width = new()
    {
        name = nameof(width)
    };
    #endregion
    /// <summary>
    ///     The width of the SVG element in pixels.
    /// </summary>
    [ReactProp]
    public string width { get; set; }

    #region string height
    PropertyValueNode<string> __height;
    static readonly PropertyValueDefinition _height = new()
    {
        name = nameof(height)
    };
    #endregion
    /// <summary>
    ///     The height of the SVG element in pixels.
    /// </summary>
    [ReactProp]
    public string height { get; set; }

    #region string xmlns
    PropertyValueNode<string> __xmlns;
    static readonly PropertyValueDefinition _xmlns = new()
    {
        name = nameof(xmlns)
    };
    #endregion
    /// <summary>
    ///     The namespace URI for the SVG element.
    /// </summary>
    [ReactProp]
    public string xmlns { get; set; } = "http://www.w3.org/2000/svg";

    #region string version
    PropertyValueNode<string> __version;
    static readonly PropertyValueDefinition _version = new()
    {
        name = nameof(version)
    };
    #endregion
    /// <summary>
    ///     The SVG version of the element.
    /// </summary>
    [ReactProp]
    public string version { get; set; }

    #region string viewBox
    PropertyValueNode<string> __viewBox;
    static readonly PropertyValueDefinition _viewBox = new()
    {
        name = nameof(viewBox)
    };
    #endregion
    [ReactProp]
    public string viewBox { get; set; }

    #region string fill
    PropertyValueNode<string> __fill;
    static readonly PropertyValueDefinition _fill = new()
    {
        name = nameof(fill)
    };
    #endregion
    [ReactProp]
    public string fill { get; set; }

    public svg() { }

    public svg(params IModifier[] modifiers) : base(modifiers) { }

    public svg(Style style) : base(style) { }

    public svg(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<svg> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Focusable(string value) => Modify(x => x.focusable = value);

    public static HtmlElementModifier XlinkHref(string value) => Modify(x => x.xlinkHref = value);

    public static HtmlElementModifier XmlnsXlink(string value) => Modify(x => x.xmlnsXlink = value);

    /// <summary>
    ///     preserveAspectRatio = <paramref name="value"/>
    /// <br/>
    ///     Specifies how the SVG element should be scaled and aligned to fit its viewport.
    /// </summary>
    public static HtmlElementModifier PreserveAspectRatio(string value) => Modify(x => x.preserveAspectRatio = value);

    /// <summary>
    ///     width = <paramref name="value"/>
    /// <br/>
    ///     The width of the SVG element in pixels.
    /// </summary>
    public static HtmlElementModifier Width(string value) => Modify(x => x.width = value);

    /// <summary>
    ///     height = <paramref name="value"/>
    /// <br/>
    ///     The height of the SVG element in pixels.
    /// </summary>
    public static HtmlElementModifier Height(string value) => Modify(x => x.height = value);

    /// <summary>
    ///     xmlns = <paramref name="value"/>
    /// <br/>
    ///     The namespace URI for the SVG element.
    /// </summary>
    public static HtmlElementModifier Xmlns(string value) => Modify(x => x.xmlns = value);

    /// <summary>
    ///     version = <paramref name="value"/>
    /// <br/>
    ///     The SVG version of the element.
    /// </summary>
    public static HtmlElementModifier Version(string value) => Modify(x => x.version = value);

    public static HtmlElementModifier ViewBox(string value) => Modify(x => x.viewBox = value);

    public static HtmlElementModifier Fill(string value) => Modify(x => x.fill = value);

}

public sealed class stop : HtmlElement
{
    #region string offset
    PropertyValueNode<string> __offset;
    static readonly PropertyValueDefinition _offset = new()
    {
        name = nameof(offset)
    };
    #endregion
    [ReactProp]
    public string offset { get; set; }

    #region string stopColor
    PropertyValueNode<string> __stopColor;
    static readonly PropertyValueDefinition _stopColor = new()
    {
        name = nameof(stopColor)
    };
    #endregion
    [ReactProp]
    public string stopColor { get; set; }

    #region string stopOpacity
    PropertyValueNode<string> __stopOpacity;
    static readonly PropertyValueDefinition _stopOpacity = new()
    {
        name = nameof(stopOpacity)
    };
    #endregion
    [ReactProp]
    public string stopOpacity { get; set; }

    public stop() { }

    public stop(params IModifier[] modifiers) : base(modifiers) { }

    public stop(Style style) : base(style) { }

    public stop(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<stop> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Offset(string value) => Modify(x => x.offset = value);

    public static HtmlElementModifier StopColor(string value) => Modify(x => x.stopColor = value);

    public static HtmlElementModifier StopOpacity(string value) => Modify(x => x.stopOpacity = value);

}

public sealed class linearGradient : HtmlElement
{
    public linearGradient() { }

    public linearGradient(params IModifier[] modifiers) : base(modifiers) { }

    public linearGradient(Style style) : base(style) { }

    public linearGradient(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<linearGradient> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class noscript : HtmlElement
{
    public noscript() { }

    public noscript(params IModifier[] modifiers) : base(modifiers) { }

    public noscript(Style style) : base(style) { }

    public noscript(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<noscript> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class defs : HtmlElement
{
    public defs() { }

    public defs(params IModifier[] modifiers) : base(modifiers) { }

    public defs(Style style) : base(style) { }

    public defs(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<defs> modifyAction) => CreateHtmlElementModifier(modifyAction);
}

public sealed class form : HtmlElement
{
    #region string action
    PropertyValueNode<string> __action;
    static readonly PropertyValueDefinition _action = new()
    {
        name = nameof(action)
    };
    #endregion
    /// <summary>
    ///     Specifies the URL of the page where the form data will be submitted.
    /// </summary>
    [ReactProp]
    public string action { get; set; }

    #region string method
    PropertyValueNode<string> __method;
    static readonly PropertyValueDefinition _method = new()
    {
        name = nameof(method)
    };
    #endregion
    /// <summary>
    ///     Specifies how the form data will be sent to the server. Possible values are 'get' and 'post'.
    /// </summary>
    [ReactProp]
    public string method { get; set; }

    #region string enctype
    PropertyValueNode<string> __enctype;
    static readonly PropertyValueDefinition _enctype = new()
    {
        name = nameof(enctype)
    };
    #endregion
    /// <summary>
    ///     Specifies the encoding type for form data. Possible values are 'application/x-www-form-urlencoded' and 'multipart/form-data'.
    /// </summary>
    [ReactProp]
    public string enctype { get; set; }

    #region string target
    PropertyValueNode<string> __target;
    static readonly PropertyValueDefinition _target = new()
    {
        name = nameof(target)
    };
    #endregion
    /// <summary>
    ///     Specifies the name of the frame where the form will be submitted. The default value is '_self', which means the form will be submitted in the current frame.
    /// </summary>
    [ReactProp]
    public string target { get; set; }

    #region string name
    PropertyValueNode<string> __name;
    static readonly PropertyValueDefinition _name = new()
    {
        name = nameof(name)
    };
    #endregion
    /// <summary>
    ///     Specifies a name for the form. This name is used to reference the form in JavaScript or to reference form data after a form is submitted.
    /// </summary>
    [ReactProp]
    public string name { get; set; }

    #region string novalidate
    PropertyValueNode<string> __novalidate;
    static readonly PropertyValueDefinition _novalidate = new()
    {
        name = nameof(novalidate)
    };
    #endregion
    /// <summary>
    ///     Disables form validation. This attribute is useful when you want to submit the form without validating the user input.
    /// </summary>
    [ReactProp]
    public string novalidate { get; set; }

    #region string autocomplete
    PropertyValueNode<string> __autocomplete;
    static readonly PropertyValueDefinition _autocomplete = new()
    {
        name = nameof(autocomplete)
    };
    #endregion
    /// <summary>
    ///     Specifies whether the browser should automatically fill in form fields based on the user's past input.
    /// </summary>
    [ReactProp]
    public string autocomplete { get; set; }

    public form() { }

    public form(params IModifier[] modifiers) : base(modifiers) { }

    public form(Style style) : base(style) { }

    public form(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<form> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     action = <paramref name="value"/>
    /// <br/>
    ///     Specifies the URL of the page where the form data will be submitted.
    /// </summary>
    public static HtmlElementModifier Action(string value) => Modify(x => x.action = value);

    /// <summary>
    ///     method = <paramref name="value"/>
    /// <br/>
    ///     Specifies how the form data will be sent to the server. Possible values are 'get' and 'post'.
    /// </summary>
    public static HtmlElementModifier Method(string value) => Modify(x => x.method = value);

    /// <summary>
    ///     enctype = <paramref name="value"/>
    /// <br/>
    ///     Specifies the encoding type for form data. Possible values are 'application/x-www-form-urlencoded' and 'multipart/form-data'.
    /// </summary>
    public static HtmlElementModifier Enctype(string value) => Modify(x => x.enctype = value);

    /// <summary>
    ///     target = <paramref name="value"/>
    /// <br/>
    ///     Specifies the name of the frame where the form will be submitted. The default value is '_self', which means the form will be submitted in the current frame.
    /// </summary>
    public static HtmlElementModifier Target(string value) => Modify(x => x.target = value);

    /// <summary>
    ///     name = <paramref name="value"/>
    /// <br/>
    ///     Specifies a name for the form. This name is used to reference the form in JavaScript or to reference form data after a form is submitted.
    /// </summary>
    public static HtmlElementModifier Name(string value) => Modify(x => x.name = value);

    /// <summary>
    ///     novalidate = <paramref name="value"/>
    /// <br/>
    ///     Disables form validation. This attribute is useful when you want to submit the form without validating the user input.
    /// </summary>
    public static HtmlElementModifier Novalidate(string value) => Modify(x => x.novalidate = value);

    /// <summary>
    ///     autocomplete = <paramref name="value"/>
    /// <br/>
    ///     Specifies whether the browser should automatically fill in form fields based on the user's past input.
    /// </summary>
    public static HtmlElementModifier Autocomplete(string value) => Modify(x => x.autocomplete = value);

}

public sealed partial class textarea : HtmlElement
{
    #region string name
    PropertyValueNode<string> __name;
    static readonly PropertyValueDefinition _name = new()
    {
        name = nameof(name)
    };
    #endregion
    /// <summary>
    ///     Specifies a name for the textarea element.
    /// </summary>
    [ReactProp]
    public string name { get; set; }

    #region string cols
    PropertyValueNode<UnionProp<string,int?>> __cols;
    static readonly PropertyValueDefinition _cols = new()
    {
        name = nameof(cols)
    };
    #endregion
    /// <summary>
    ///     Specifies the visible width of the textarea element in characters.
    /// </summary>
    [ReactProp]
    public UnionProp<string,int?> cols { get; set; }

    #region string rows
    PropertyValueNode<UnionProp<string,int?>> __rows;
    static readonly PropertyValueDefinition _rows = new()
    {
        name = nameof(rows)
    };
    #endregion
    /// <summary>
    ///     Specifies the number of visible lines in the textarea element.
    /// </summary>
    [ReactProp]
    public UnionProp<string,int?> rows { get; set; }

    #region string placeholder
    PropertyValueNode<string> __placeholder;
    static readonly PropertyValueDefinition _placeholder = new()
    {
        name = nameof(placeholder)
    };
    #endregion
    /// <summary>
    ///     Specifies a short hint that describes the expected value of the textarea element.
    /// </summary>
    [ReactProp]
    public string placeholder { get; set; }

    #region string readOnly
    PropertyValueNode<UnionProp<string,bool>> __readOnly;
    static readonly PropertyValueDefinition _readOnly = new()
    {
        name = nameof(readOnly)
    };
    #endregion
    /// <summary>
    ///     Disables user input in the textarea element.
    /// </summary>
    [ReactProp]
    public UnionProp<string,bool> readOnly { get; set; }

    #region string required
    PropertyValueNode<string> __required;
    static readonly PropertyValueDefinition _required = new()
    {
        name = nameof(required)
    };
    #endregion
    /// <summary>
    ///     Indicates that the textarea element must be filled out before the form is submitted.
    /// </summary>
    [ReactProp]
    public string required { get; set; }

    #region string autofocus
    PropertyValueNode<string> __autofocus;
    static readonly PropertyValueDefinition _autofocus = new()
    {
        name = nameof(autofocus)
    };
    #endregion
    /// <summary>
    ///     Automatically gives focus to the textarea element when the page loads.
    /// </summary>
    [ReactProp]
    public string autofocus { get; set; }

    #region string autocomplete
    PropertyValueNode<string> __autocomplete;
    static readonly PropertyValueDefinition _autocomplete = new()
    {
        name = nameof(autocomplete)
    };
    #endregion
    /// <summary>
    ///     Specifies that the user's browser should automatically complete the textarea element's value.
    /// </summary>
    [ReactProp]
    public string autocomplete { get; set; }

    #region string dirname
    PropertyValueNode<string> __dirname;
    static readonly PropertyValueDefinition _dirname = new()
    {
        name = nameof(dirname)
    };
    #endregion
    /// <summary>
    ///     Specifies the directory to use as the default value for the 'file' input type.
    /// </summary>
    [ReactProp]
    public string dirname { get; set; }

    #region string form
    PropertyValueNode<string> __form;
    static readonly PropertyValueDefinition _form = new()
    {
        name = nameof(form)
    };
    #endregion
    /// <summary>
    ///     Specifies the ID of the form that the textarea element belongs to.
    /// </summary>
    [ReactProp]
    public string form { get; set; }

    #region string maxlength
    PropertyValueNode<string> __maxlength;
    static readonly PropertyValueDefinition _maxlength = new()
    {
        name = nameof(maxlength)
    };
    #endregion
    /// <summary>
    ///     Specifies the maximum number of characters that can be entered into the textarea element.
    /// </summary>
    [ReactProp]
    public string maxlength { get; set; }

    #region string minlength
    PropertyValueNode<string> __minlength;
    static readonly PropertyValueDefinition _minlength = new()
    {
        name = nameof(minlength)
    };
    #endregion
    /// <summary>
    ///     Specifies the minimum number of characters that must be entered into the textarea element.
    /// </summary>
    [ReactProp]
    public string minlength { get; set; }

    #region string wrap
    PropertyValueNode<string> __wrap;
    static readonly PropertyValueDefinition _wrap = new()
    {
        name = nameof(wrap)
    };
    #endregion
    /// <summary>
    ///     Specifies whether the text in the textarea element should wrap to the next line when it reaches the end of the visible area.
    /// </summary>
    [ReactProp]
    public string wrap { get; set; }

    #region string defaultValue
    PropertyValueNode<string> __defaultValue;
    static readonly PropertyValueDefinition _defaultValue = new()
    {
        name = nameof(defaultValue)
    };
    #endregion
    /// <summary>
    ///     A string. Specifies the initial value for a text area.
    /// </summary>
    [ReactProp]
    public string defaultValue { get; set; }

    #region string value
    PropertyValueNode<string> __value;
    static readonly PropertyValueDefinition _value = new()
    {
        name = nameof(value)
    };
    #endregion
    [ReactProp]
    public string value { get; set; }

    #region string disabled
    PropertyValueNode<string> __disabled;
    static readonly PropertyValueDefinition _disabled = new()
    {
        name = nameof(disabled)
    };
    #endregion
    [ReactProp]
    public string disabled { get; set; }

    public textarea() { }

    public textarea(params IModifier[] modifiers) : base(modifiers) { }

    public textarea(Style style) : base(style) { }

    public textarea(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<textarea> modifyAction) => CreateHtmlElementModifier(modifyAction);
    /// <summary>
    ///     name = <paramref name="value"/>
    /// <br/>
    ///     Specifies a name for the textarea element.
    /// </summary>
    public static HtmlElementModifier Name(string value) => Modify(x => x.name = value);

    /// <summary>
    ///     cols = <paramref name="value"/>
    /// <br/>
    ///     Specifies the visible width of the textarea element in characters.
    /// </summary>
    public static HtmlElementModifier Cols(UnionProp<string,int?> value) => Modify(x => x.cols = value);

    /// <summary>
    ///     rows = <paramref name="value"/>
    /// <br/>
    ///     Specifies the number of visible lines in the textarea element.
    /// </summary>
    public static HtmlElementModifier Rows(UnionProp<string,int?> value) => Modify(x => x.rows = value);

    /// <summary>
    ///     placeholder = <paramref name="value"/>
    /// <br/>
    ///     Specifies a short hint that describes the expected value of the textarea element.
    /// </summary>
    public static HtmlElementModifier Placeholder(string value) => Modify(x => x.placeholder = value);

    /// <summary>
    ///     readOnly = <paramref name="value"/>
    /// <br/>
    ///     Disables user input in the textarea element.
    /// </summary>
    public static HtmlElementModifier ReadOnly(UnionProp<string,bool> value) => Modify(x => x.readOnly = value);

    /// <summary>
    ///     required = <paramref name="value"/>
    /// <br/>
    ///     Indicates that the textarea element must be filled out before the form is submitted.
    /// </summary>
    public static HtmlElementModifier Required(string value) => Modify(x => x.required = value);

    /// <summary>
    ///     autofocus = <paramref name="value"/>
    /// <br/>
    ///     Automatically gives focus to the textarea element when the page loads.
    /// </summary>
    public static HtmlElementModifier Autofocus(string value) => Modify(x => x.autofocus = value);

    /// <summary>
    ///     autocomplete = <paramref name="value"/>
    /// <br/>
    ///     Specifies that the user's browser should automatically complete the textarea element's value.
    /// </summary>
    public static HtmlElementModifier Autocomplete(string value) => Modify(x => x.autocomplete = value);

    /// <summary>
    ///     dirname = <paramref name="value"/>
    /// <br/>
    ///     Specifies the directory to use as the default value for the 'file' input type.
    /// </summary>
    public static HtmlElementModifier Dirname(string value) => Modify(x => x.dirname = value);

    /// <summary>
    ///     form = <paramref name="value"/>
    /// <br/>
    ///     Specifies the ID of the form that the textarea element belongs to.
    /// </summary>
    public static HtmlElementModifier Form(string value) => Modify(x => x.form = value);

    /// <summary>
    ///     maxlength = <paramref name="value"/>
    /// <br/>
    ///     Specifies the maximum number of characters that can be entered into the textarea element.
    /// </summary>
    public static HtmlElementModifier Maxlength(string value) => Modify(x => x.maxlength = value);

    /// <summary>
    ///     minlength = <paramref name="value"/>
    /// <br/>
    ///     Specifies the minimum number of characters that must be entered into the textarea element.
    /// </summary>
    public static HtmlElementModifier Minlength(string value) => Modify(x => x.minlength = value);

    /// <summary>
    ///     wrap = <paramref name="value"/>
    /// <br/>
    ///     Specifies whether the text in the textarea element should wrap to the next line when it reaches the end of the visible area.
    /// </summary>
    public static HtmlElementModifier Wrap(string value) => Modify(x => x.wrap = value);

    /// <summary>
    ///     defaultValue = <paramref name="value"/>
    /// <br/>
    ///     A string. Specifies the initial value for a text area.
    /// </summary>
    public static HtmlElementModifier DefaultValue(string value) => Modify(x => x.defaultValue = value);

    public static HtmlElementModifier Value(string value) => Modify(x => x.value = value);

    public static HtmlElementModifier Disabled(string value) => Modify(x => x.disabled = value);

}

public sealed class link : HtmlElement
{
    #region string href
    PropertyValueNode<string> __href;
    static readonly PropertyValueDefinition _href = new()
    {
        name = nameof(href)
    };
    #endregion
    [ReactProp]
    public string href { get; set; }

    #region string media
    PropertyValueNode<string> __media;
    static readonly PropertyValueDefinition _media = new()
    {
        name = nameof(media)
    };
    #endregion
    [ReactProp]
    public string media { get; set; }

    #region string rel
    PropertyValueNode<string> __rel;
    static readonly PropertyValueDefinition _rel = new()
    {
        name = nameof(rel)
    };
    #endregion
    [ReactProp]
    public string rel { get; set; }

    #region string sizes
    PropertyValueNode<string> __sizes;
    static readonly PropertyValueDefinition _sizes = new()
    {
        name = nameof(sizes)
    };
    #endregion
    [ReactProp]
    public string sizes { get; set; }

    #region string type
    PropertyValueNode<string> __type;
    static readonly PropertyValueDefinition _type = new()
    {
        name = nameof(type)
    };
    #endregion
    [ReactProp]
    public string type { get; set; }

    #region string @as
    PropertyValueNode<string> __as;
    static readonly PropertyValueDefinition _as = new()
    {
        name = nameof(@as)
    };
    #endregion
    [ReactProp]
    public string @as { get; set; }

    #region string integrity
    PropertyValueNode<string> __integrity;
    static readonly PropertyValueDefinition _integrity = new()
    {
        name = nameof(integrity)
    };
    #endregion
    [ReactProp]
    public string integrity { get; set; }

    #region string crossorigin
    PropertyValueNode<string> __crossorigin;
    static readonly PropertyValueDefinition _crossorigin = new()
    {
        name = nameof(crossorigin)
    };
    #endregion
    [ReactProp]
    public string crossorigin { get; set; }

    #region string referrerpolicy
    PropertyValueNode<string> __referrerpolicy;
    static readonly PropertyValueDefinition _referrerpolicy = new()
    {
        name = nameof(referrerpolicy)
    };
    #endregion
    [ReactProp]
    public string referrerpolicy { get; set; }

    public link() { }

    public link(params IModifier[] modifiers) : base(modifiers) { }

    public link(Style style) : base(style) { }

    public link(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<link> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Href(string value) => Modify(x => x.href = value);

    public static HtmlElementModifier Media(string value) => Modify(x => x.media = value);

    public static HtmlElementModifier Rel(string value) => Modify(x => x.rel = value);

    public static HtmlElementModifier Sizes(string value) => Modify(x => x.sizes = value);

    public static HtmlElementModifier Type(string value) => Modify(x => x.type = value);

    public static HtmlElementModifier As(string value) => Modify(x => x.@as = value);

    public static HtmlElementModifier Integrity(string value) => Modify(x => x.integrity = value);

    public static HtmlElementModifier Crossorigin(string value) => Modify(x => x.crossorigin = value);

    public static HtmlElementModifier Referrerpolicy(string value) => Modify(x => x.referrerpolicy = value);

}

public sealed class iframe : HtmlElement
{
    #region string src
    PropertyValueNode<string> __src;
    static readonly PropertyValueDefinition _src = new()
    {
        name = nameof(src)
    };
    #endregion
    [ReactProp]
    public string src { get; set; }

    public iframe() { }

    public iframe(params IModifier[] modifiers) : base(modifiers) { }

    public iframe(Style style) : base(style) { }

    public iframe(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<iframe> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Src(string value) => Modify(x => x.src = value);

}

public sealed partial class select : HtmlElement
{
    #region string value
    PropertyValueNode<string> __value;
    static readonly PropertyValueDefinition _value = new()
    {
        name = nameof(value)
    };
    #endregion
    [ReactProp]
    public string value { get; set; }

    #region string disabled
    PropertyValueNode<string> __disabled;
    static readonly PropertyValueDefinition _disabled = new()
    {
        name = nameof(disabled)
    };
    #endregion
    [ReactProp]
    public string disabled { get; set; }

    public select() { }

    public select(params IModifier[] modifiers) : base(modifiers) { }

    public select(Style style) : base(style) { }

    public select(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<select> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Value(string value) => Modify(x => x.value = value);

    public static HtmlElementModifier Disabled(string value) => Modify(x => x.disabled = value);

}

public sealed partial class input : HtmlElement
{
    #region string required
    PropertyValueNode<string> __required;
    static readonly PropertyValueDefinition _required = new()
    {
        name = nameof(required)
    };
    #endregion
    [ReactProp]
    public string required { get; set; }

    #region string autoComplete
    PropertyValueNode<string> __autoComplete;
    static readonly PropertyValueDefinition _autoComplete = new()
    {
        name = nameof(autoComplete)
    };
    #endregion
    [ReactProp]
    public string autoComplete { get; set; }

    #region string @checked
    PropertyValueNode<bool?> __checked;
    static readonly PropertyValueDefinition _checked = new()
    {
        name = nameof(@checked)
    };
    #endregion
    [ReactProp]
    public bool? @checked { get; set; }

    #region string defaultChecked
    PropertyValueNode<bool?> __defaultChecked;
    static readonly PropertyValueDefinition _defaultChecked = new()
    {
        name = nameof(defaultChecked)
    };
    #endregion
    [ReactProp]
    public bool? defaultChecked { get; set; }

    #region string defaultValue
    PropertyValueNode<string> __defaultValue;
    static readonly PropertyValueDefinition _defaultValue = new()
    {
        name = nameof(defaultValue)
    };
    #endregion
    [ReactProp]
    public string defaultValue { get; set; }

    #region string disabled
    PropertyValueNode<bool?> __disabled;
    static readonly PropertyValueDefinition _disabled = new()
    {
        name = nameof(disabled)
    };
    #endregion
    [ReactProp]
    public bool? disabled { get; set; }

    #region string autoFocus
    PropertyValueNode<bool?> __autoFocus;
    static readonly PropertyValueDefinition _autoFocus = new()
    {
        name = nameof(autoFocus)
    };
    #endregion
    /// <summary>
    ///     Element must automatically get focus when the page loads.
    /// </summary>
    [ReactProp]
    public bool? autoFocus { get; set; }

    #region string name
    PropertyValueNode<string> __name;
    static readonly PropertyValueDefinition _name = new()
    {
        name = nameof(name)
    };
    #endregion
    [ReactProp]
    public string name { get; set; }

    #region string placeholder
    PropertyValueNode<string> __placeholder;
    static readonly PropertyValueDefinition _placeholder = new()
    {
        name = nameof(placeholder)
    };
    #endregion
    [ReactProp]
    public string placeholder { get; set; }

    #region string readOnly
    PropertyValueNode<bool?> __readOnly;
    static readonly PropertyValueDefinition _readOnly = new()
    {
        name = nameof(readOnly)
    };
    #endregion
    [ReactProp]
    public bool? readOnly { get; set; }

    #region string type
    PropertyValueNode<string> __type;
    static readonly PropertyValueDefinition _type = new()
    {
        name = nameof(type)
    };
    #endregion
    [ReactProp]
    public string type { get; set; }

    #region string max
    PropertyValueNode<int?> __max;
    static readonly PropertyValueDefinition _max = new()
    {
        name = nameof(max)
    };
    #endregion
    [ReactProp]
    public int? max { get; set; }

    #region string min
    PropertyValueNode<int?> __min;
    static readonly PropertyValueDefinition _min = new()
    {
        name = nameof(min)
    };
    #endregion
    [ReactProp]
    public int? min { get; set; }

    #region string step
    PropertyValueNode<int?> __step;
    static readonly PropertyValueDefinition _step = new()
    {
        name = nameof(step)
    };
    #endregion
    [ReactProp]
    public int? step { get; set; }

    public input() { }

    public input(params IModifier[] modifiers) : base(modifiers) { }

    public input(Style style) : base(style) { }

    public input(StyleModifier[] styleModifiers) : base(styleModifiers) { }

    public static HtmlElementModifier Modify(Action<input> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Required(string value) => Modify(x => x.required = value);

    public static HtmlElementModifier AutoComplete(string value) => Modify(x => x.autoComplete = value);

    public static HtmlElementModifier Checked(bool? value) => Modify(x => x.@checked = value);

    public static HtmlElementModifier DefaultChecked(bool? value) => Modify(x => x.defaultChecked = value);

    public static HtmlElementModifier DefaultValue(string value) => Modify(x => x.defaultValue = value);

    public static HtmlElementModifier Disabled(bool? value) => Modify(x => x.disabled = value);

    /// <summary>
    ///     autoFocus = <paramref name="value"/>
    /// <br/>
    ///     Element must automatically get focus when the page loads.
    /// </summary>
    public static HtmlElementModifier AutoFocus(bool? value) => Modify(x => x.autoFocus = value);

    public static HtmlElementModifier Name(string value) => Modify(x => x.name = value);

    public static HtmlElementModifier Placeholder(string value) => Modify(x => x.placeholder = value);

    public static HtmlElementModifier ReadOnly(bool? value) => Modify(x => x.readOnly = value);

    public static HtmlElementModifier Type(string value) => Modify(x => x.type = value);

    public static HtmlElementModifier Max(int? value) => Modify(x => x.max = value);

    public static HtmlElementModifier Min(int? value) => Modify(x => x.min = value);

    public static HtmlElementModifier Step(int? value) => Modify(x => x.step = value);

}

public  partial class HtmlElement
{
    #region string accesskey
    PropertyValueNode<string> __accesskey;
    static readonly PropertyValueDefinition _accesskey = new()
    {
        name = nameof(accesskey)
    };
    #endregion
    [ReactProp]
    public string accesskey { get; set; }

    #region string draggable
    PropertyValueNode<string> __draggable;
    static readonly PropertyValueDefinition _draggable = new()
    {
        name = nameof(draggable)
    };
    #endregion
    [ReactProp]
    public string draggable { get; set; }

    #region string contenteditable
    PropertyValueNode<string> __contenteditable;
    static readonly PropertyValueDefinition _contenteditable = new()
    {
        name = nameof(contenteditable)
    };
    #endregion
    [ReactProp]
    public string contenteditable { get; set; }

    #region string className
    PropertyValueNode<string> __className;
    static readonly PropertyValueDefinition _className = new()
    {
        name = nameof(className)
    };
    #endregion
    [ReactProp]
    public string className { get; set; }

    #region string dangerouslySetInnerHTML
    PropertyValueNode<dangerouslySetInnerHTML> __dangerouslySetInnerHTML;
    static readonly PropertyValueDefinition _dangerouslySetInnerHTML = new()
    {
        name = nameof(dangerouslySetInnerHTML)
    };
    #endregion
    [ReactProp]
    public dangerouslySetInnerHTML dangerouslySetInnerHTML { get; set; }

    #region string dir
    PropertyValueNode<string> __dir;
    static readonly PropertyValueDefinition _dir = new()
    {
        name = nameof(dir)
    };
    #endregion
    [ReactProp]
    public string dir { get; set; }

    #region string id
    PropertyValueNode<string> __id;
    static readonly PropertyValueDefinition _id = new()
    {
        name = nameof(id)
    };
    #endregion
    [ReactProp]
    public string id { get; set; }

    #region string lang
    PropertyValueNode<string> __lang;
    static readonly PropertyValueDefinition _lang = new()
    {
        name = nameof(lang)
    };
    #endregion
    [ReactProp]
    public string lang { get; set; }

    #region string part
    PropertyValueNode<string> __part;
    static readonly PropertyValueDefinition _part = new()
    {
        name = nameof(part)
    };
    #endregion
    [ReactProp]
    public string part { get; set; }

    #region string role
    PropertyValueNode<string> __role;
    static readonly PropertyValueDefinition _role = new()
    {
        name = nameof(role)
    };
    #endregion
    [ReactProp]
    public string role { get; set; }

    #region string spellcheck
    PropertyValueNode<string> __spellcheck;
    static readonly PropertyValueDefinition _spellcheck = new()
    {
        name = nameof(spellcheck)
    };
    #endregion
    [ReactProp]
    public string spellcheck { get; set; }

    #region string tabIndex
    PropertyValueNode<string> __tabIndex;
    static readonly PropertyValueDefinition _tabIndex = new()
    {
        name = nameof(tabIndex)
    };
    #endregion
    [ReactProp]
    public string tabIndex { get; set; }

    #region string title
    PropertyValueNode<string> __title;
    static readonly PropertyValueDefinition _title = new()
    {
        name = nameof(title)
    };
    #endregion
    [ReactProp]
    public string title { get; set; }

    #region string translate
    PropertyValueNode<string> __translate;
    static readonly PropertyValueDefinition _translate = new()
    {
        name = nameof(translate)
    };
    #endregion
    [ReactProp]
    public string translate { get; set; }

    #region string onClick
    PropertyValueNode<MouseEventHandler> __onClick;
    static readonly PropertyValueDefinition _onClick = new()
    {
        name = nameof(onClick),
        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments"
    };
    #endregion
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public MouseEventHandler onClick { get; set; }

    #region string onMouseEnter
    PropertyValueNode<MouseEventHandler> __onMouseEnter;
    static readonly PropertyValueDefinition _onMouseEnter = new()
    {
        name = nameof(onMouseEnter),
        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments"
    };
    #endregion
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public MouseEventHandler onMouseEnter { get; set; }

    #region string onMouseLeave
    PropertyValueNode<MouseEventHandler> __onMouseLeave;
    static readonly PropertyValueDefinition _onMouseLeave = new()
    {
        name = nameof(onMouseLeave),
        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments"
    };
    #endregion
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public MouseEventHandler onMouseLeave { get; set; }

    #region string onScroll
    PropertyValueNode<ScrollEventHandler> __onScroll;
    static readonly PropertyValueDefinition _onScroll = new()
    {
        name = nameof(onScroll)
    };
    #endregion
    [ReactProp]
    public ScrollEventHandler onScroll { get; set; }

    #region string onKeyDown
    PropertyValueNode<KeyboardEventHandler> __onKeyDown;
    static readonly PropertyValueDefinition _onKeyDown = new()
    {
        name = nameof(onKeyDown)
    };
    #endregion
    [ReactProp]
    public KeyboardEventHandler onKeyDown { get; set; }


    public static HtmlElementModifier Modify(Action<HtmlElement> modifyAction) => CreateHtmlElementModifier(modifyAction);
    public static HtmlElementModifier Accesskey(string value) => Modify(x => x.accesskey = value);

    public static HtmlElementModifier Draggable(string value) => Modify(x => x.draggable = value);

    public static HtmlElementModifier Contenteditable(string value) => Modify(x => x.contenteditable = value);

    public static HtmlElementModifier ClassName(string value) => Modify(x => x.className = value);

    public static HtmlElementModifier DangerouslySetInnerHTML(dangerouslySetInnerHTML value) => Modify(x => x.dangerouslySetInnerHTML = value);

    public static HtmlElementModifier Dir(string value) => Modify(x => x.dir = value);

    public static HtmlElementModifier Id(string value) => Modify(x => x.id = value);

    public static HtmlElementModifier Lang(string value) => Modify(x => x.lang = value);

    public static HtmlElementModifier Part(string value) => Modify(x => x.part = value);

    public static HtmlElementModifier Role(string value) => Modify(x => x.role = value);

    public static HtmlElementModifier Spellcheck(string value) => Modify(x => x.spellcheck = value);

    public static HtmlElementModifier TabIndex(string value) => Modify(x => x.tabIndex = value);

    public static HtmlElementModifier Title(string value) => Modify(x => x.title = value);

    public static HtmlElementModifier Translate(string value) => Modify(x => x.translate = value);

    public static HtmlElementModifier OnClick(MouseEventHandler value) => Modify(x => x.onClick = value);

    public static HtmlElementModifier OnMouseEnter(MouseEventHandler value) => Modify(x => x.onMouseEnter = value);

    public static HtmlElementModifier OnMouseLeave(MouseEventHandler value) => Modify(x => x.onMouseLeave = value);

    public static HtmlElementModifier OnScroll(ScrollEventHandler value) => Modify(x => x.onScroll = value);

    public static HtmlElementModifier OnKeyDown(KeyboardEventHandler value) => Modify(x => x.onKeyDown = value);

}

