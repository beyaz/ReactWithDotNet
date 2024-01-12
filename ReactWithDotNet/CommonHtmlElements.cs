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
    PropertyValueNode<string> _type;
    static readonly PropertyValueDefinition _type_ = new()
    {
        name = nameof(type)
    };
    /// <summary>
    ///     Specifies the type of button. button, reset, submit
    /// </summary>
    public string type
    {
        get => _type?.value;
        set => SetValue(_type_, ref _type, value);
    }
    #endregion


    #region string value
    PropertyValueNode<string> _value;
    static readonly PropertyValueDefinition _value_ = new()
    {
        name = nameof(value)
    };
    /// <summary>
    ///     Specifies an initial value for the button
    /// </summary>
    public string value
    {
        get => _value?.value;
        set => SetValue(_value_, ref _value, value);
    }
    #endregion


    #region string autofocus
    PropertyValueNode<string> _autofocus;
    static readonly PropertyValueDefinition _autofocus_ = new()
    {
        name = nameof(autofocus)
    };
    /// <summary>
    ///     Specifies that the button should have input focus when the page loads. Only one element in a document can have this attribute.
    /// </summary>
    public string autofocus
    {
        get => _autofocus?.value;
        set => SetValue(_autofocus_, ref _autofocus, value);
    }
    #endregion


    #region string disabled
    PropertyValueNode<string> _disabled;
    static readonly PropertyValueDefinition _disabled_ = new()
    {
        name = nameof(disabled)
    };
    /// <summary>
    ///     Specifies that the button should be disabled. A disabled button cannot be clicked.
    /// </summary>
    public string disabled
    {
        get => _disabled?.value;
        set => SetValue(_disabled_, ref _disabled, value);
    }
    #endregion


    #region string form
    PropertyValueNode<string> _form;
    static readonly PropertyValueDefinition _form_ = new()
    {
        name = nameof(form)
    };
    /// <summary>
    ///     Specifies the form that the button is associated with.
    /// </summary>
    public string form
    {
        get => _form?.value;
        set => SetValue(_form_, ref _form, value);
    }
    #endregion


    #region string formaction
    PropertyValueNode<string> _formaction;
    static readonly PropertyValueDefinition _formaction_ = new()
    {
        name = nameof(formaction)
    };
    /// <summary>
    ///     Specifies the URL of the form action when the button is clicked.
    /// </summary>
    public string formaction
    {
        get => _formaction?.value;
        set => SetValue(_formaction_, ref _formaction, value);
    }
    #endregion


    #region string formenctype
    PropertyValueNode<string> _formenctype;
    static readonly PropertyValueDefinition _formenctype_ = new()
    {
        name = nameof(formenctype)
    };
    /// <summary>
    ///     Specifies the form encoding method (e.g., application/x-www-form-urlencoded, multipart/form-data) when the button is clicked.
    /// </summary>
    public string formenctype
    {
        get => _formenctype?.value;
        set => SetValue(_formenctype_, ref _formenctype, value);
    }
    #endregion


    #region string formmethod
    PropertyValueNode<string> _formmethod;
    static readonly PropertyValueDefinition _formmethod_ = new()
    {
        name = nameof(formmethod)
    };
    /// <summary>
    ///     Specifies the form submission method (e.g., GET, POST) when the button is clicked.
    /// </summary>
    public string formmethod
    {
        get => _formmethod?.value;
        set => SetValue(_formmethod_, ref _formmethod, value);
    }
    #endregion


    #region string formnovalidate
    PropertyValueNode<string> _formnovalidate;
    static readonly PropertyValueDefinition _formnovalidate_ = new()
    {
        name = nameof(formnovalidate)
    };
    /// <summary>
    ///     Specifies that the form should not be validated before submission when the button is clicked.
    /// </summary>
    public string formnovalidate
    {
        get => _formnovalidate?.value;
        set => SetValue(_formnovalidate_, ref _formnovalidate, value);
    }
    #endregion


    #region string name
    PropertyValueNode<string> _name;
    static readonly PropertyValueDefinition _name_ = new()
    {
        name = nameof(name)
    };
    /// <summary>
    ///     Specifies a name for the button. The name attribute is used to reference form-data after the form has been submitted, or to reference the element in JavaScript.
    /// </summary>
    public string name
    {
        get => _name?.value;
        set => SetValue(_name_, ref _name, value);
    }
    #endregion


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
    PropertyValueNode<double?> _cellSpacing;
    static readonly PropertyValueDefinition _cellSpacing_ = new()
    {
        name = nameof(cellSpacing)
    };
    public double? cellSpacing
    {
        get => _cellSpacing?.value;
        set => SetValue(_cellSpacing_, ref _cellSpacing, value);
    }
    #endregion


    #region string cellPadding
    PropertyValueNode<double?> _cellPadding;
    static readonly PropertyValueDefinition _cellPadding_ = new()
    {
        name = nameof(cellPadding)
    };
    public double? cellPadding
    {
        get => _cellPadding?.value;
        set => SetValue(_cellPadding_, ref _cellPadding, value);
    }
    #endregion


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
    PropertyValueNode<int?> _colSpan;
    static readonly PropertyValueDefinition _colSpan_ = new()
    {
        name = nameof(colSpan)
    };
    public int? colSpan
    {
        get => _colSpan?.value;
        set => SetValue(_colSpan_, ref _colSpan, value);
    }
    #endregion


    #region string rowSpan
    PropertyValueNode<int?> _rowSpan;
    static readonly PropertyValueDefinition _rowSpan_ = new()
    {
        name = nameof(rowSpan)
    };
    public int? rowSpan
    {
        get => _rowSpan?.value;
        set => SetValue(_rowSpan_, ref _rowSpan, value);
    }
    #endregion


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
    PropertyValueNode<int?> _colSpan;
    static readonly PropertyValueDefinition _colSpan_ = new()
    {
        name = nameof(colSpan)
    };
    public int? colSpan
    {
        get => _colSpan?.value;
        set => SetValue(_colSpan_, ref _colSpan, value);
    }
    #endregion


    #region string rowSpan
    PropertyValueNode<int?> _rowSpan;
    static readonly PropertyValueDefinition _rowSpan_ = new()
    {
        name = nameof(rowSpan)
    };
    public int? rowSpan
    {
        get => _rowSpan?.value;
        set => SetValue(_rowSpan_, ref _rowSpan, value);
    }
    #endregion



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
    PropertyValueNode<int?> _colSpan;
    static readonly PropertyValueDefinition _colSpan_ = new()
    {
        name = nameof(colSpan)
    };
    public int? colSpan
    {
        get => _colSpan?.value;
        set => SetValue(_colSpan_, ref _colSpan, value);
    }
    #endregion


    #region string rowSpan
    PropertyValueNode<int?> _rowSpan;
    static readonly PropertyValueDefinition _rowSpan_ = new()
    {
        name = nameof(rowSpan)
    };
    public int? rowSpan
    {
        get => _rowSpan?.value;
        set => SetValue(_rowSpan_, ref _rowSpan, value);
    }
    #endregion


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
    PropertyValueNode<bool?> _selected;
    static readonly PropertyValueDefinition _selected_ = new()
    {
        name = nameof(selected)
    };
    public bool? selected
    {
        get => _selected?.value;
        set => SetValue(_selected_, ref _selected, value);
    }
    #endregion


    #region string disabled
    PropertyValueNode<string> _disabled;
    static readonly PropertyValueDefinition _disabled_ = new()
    {
        name = nameof(disabled)
    };
    public string disabled
    {
        get => _disabled?.value;
        set => SetValue(_disabled_, ref _disabled, value);
    }
    #endregion


    #region string value
    PropertyValueNode<string> _value;
    static readonly PropertyValueDefinition _value_ = new()
    {
        name = nameof(value)
    };
    public string value
    {
        get => _value?.value;
        set => SetValue(_value_, ref _value, value);
    }
    #endregion


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
    PropertyValueNode<string> _cx;
    static readonly PropertyValueDefinition _cx_ = new()
    {
        name = nameof(cx)
    };
    /// <summary>
    ///     The x-coordinate of the center of the ellipse.
    /// </summary>
    public string cx
    {
        get => _cx?.value;
        set => SetValue(_cx_, ref _cx, value);
    }
    #endregion


    #region string cy
    PropertyValueNode<string> _cy;
    static readonly PropertyValueDefinition _cy_ = new()
    {
        name = nameof(cy)
    };
    /// <summary>
    ///     The y-coordinate of the center of the ellipse.
    /// </summary>
    public string cy
    {
        get => _cy?.value;
        set => SetValue(_cy_, ref _cy, value);
    }
    #endregion


    #region string rx
    PropertyValueNode<string> _rx;
    static readonly PropertyValueDefinition _rx_ = new()
    {
        name = nameof(rx)
    };
    /// <summary>
    ///     The radius of the ellipse along the x-axis.
    /// </summary>
    public string rx
    {
        get => _rx?.value;
        set => SetValue(_rx_, ref _rx, value);
    }
    #endregion


    #region string ry
    PropertyValueNode<string> _ry;
    static readonly PropertyValueDefinition _ry_ = new()
    {
        name = nameof(ry)
    };
    /// <summary>
    ///     The radius of the ellipse along the y-axis.
    /// </summary>
    public string ry
    {
        get => _ry?.value;
        set => SetValue(_ry_, ref _ry, value);
    }
    #endregion


    #region string fill
    PropertyValueNode<string> _fill;
    static readonly PropertyValueDefinition _fill_ = new()
    {
        name = nameof(fill)
    };
    /// <summary>
    ///     The fill color of the ellipse.
    /// </summary>
    public string fill
    {
        get => _fill?.value;
        set => SetValue(_fill_, ref _fill, value);
    }
    #endregion


    #region string stroke
    PropertyValueNode<string> _stroke;
    static readonly PropertyValueDefinition _stroke_ = new()
    {
        name = nameof(stroke)
    };
    /// <summary>
    ///     The stroke color of the ellipse.
    /// </summary>
    public string stroke
    {
        get => _stroke?.value;
        set => SetValue(_stroke_, ref _stroke, value);
    }
    #endregion


    #region string strokeWidth
    PropertyValueNode<string> _strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth_ = new()
    {
        name = nameof(strokeWidth)
    };
    /// <summary>
    ///     The width of the stroke.
    /// </summary>
    public string strokeWidth
    {
        get => _strokeWidth?.value;
        set => SetValue(_strokeWidth_, ref _strokeWidth, value);
    }
    #endregion


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
    PropertyValueNode<string> _x1;
    static readonly PropertyValueDefinition _x1_ = new()
    {
        name = nameof(x1)
    };
    /// <summary>
    ///     The x-coordinate of the start point of the line.
    /// </summary>
    public string x1
    {
        get => _x1?.value;
        set => SetValue(_x1_, ref _x1, value);
    }
    #endregion


    #region string y1
    PropertyValueNode<string> _y1;
    static readonly PropertyValueDefinition _y1_ = new()
    {
        name = nameof(y1)
    };
    /// <summary>
    ///     The y-coordinate of the start point of the line.
    /// </summary>
    public string y1
    {
        get => _y1?.value;
        set => SetValue(_y1_, ref _y1, value);
    }
    #endregion


    #region string x2
    PropertyValueNode<string> _x2;
    static readonly PropertyValueDefinition _x2_ = new()
    {
        name = nameof(x2)
    };
    /// <summary>
    ///     The x-coordinate of the end point of the line.
    /// </summary>
    public string x2
    {
        get => _x2?.value;
        set => SetValue(_x2_, ref _x2, value);
    }
    #endregion


    #region string y2
    PropertyValueNode<string> _y2;
    static readonly PropertyValueDefinition _y2_ = new()
    {
        name = nameof(y2)
    };
    /// <summary>
    ///     The y-coordinate of the end point of the line.
    /// </summary>
    public string y2
    {
        get => _y2?.value;
        set => SetValue(_y2_, ref _y2, value);
    }
    #endregion


    #region string stroke
    PropertyValueNode<string> _stroke;
    static readonly PropertyValueDefinition _stroke_ = new()
    {
        name = nameof(stroke)
    };
    /// <summary>
    ///     The stroke (outline) color of the line.
    /// </summary>
    public string stroke
    {
        get => _stroke?.value;
        set => SetValue(_stroke_, ref _stroke, value);
    }
    #endregion


    #region string strokeWidth
    PropertyValueNode<string> _strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth_ = new()
    {
        name = nameof(strokeWidth)
    };
    /// <summary>
    ///     The width of the line's outline.
    /// </summary>
    public string strokeWidth
    {
        get => _strokeWidth?.value;
        set => SetValue(_strokeWidth_, ref _strokeWidth, value);
    }
    #endregion


    #region string strokeDasharray
    PropertyValueNode<string> _strokeDasharray;
    static readonly PropertyValueDefinition _strokeDasharray_ = new()
    {
        name = nameof(strokeDasharray)
    };
    /// <summary>
    ///     Pattern of dashes and gaps used in the line's stroke.
    /// </summary>
    public string strokeDasharray
    {
        get => _strokeDasharray?.value;
        set => SetValue(_strokeDasharray_, ref _strokeDasharray, value);
    }
    #endregion


    #region string strokeLinecap
    PropertyValueNode<string> _strokeLinecap;
    static readonly PropertyValueDefinition _strokeLinecap_ = new()
    {
        name = nameof(strokeLinecap)
    };
    /// <summary>
    ///     The style of the line's endpoints.
    /// </summary>
    public string strokeLinecap
    {
        get => _strokeLinecap?.value;
        set => SetValue(_strokeLinecap_, ref _strokeLinecap, value);
    }
    #endregion


    #region string strokeLinejoin
    PropertyValueNode<string> _strokeLinejoin;
    static readonly PropertyValueDefinition _strokeLinejoin_ = new()
    {
        name = nameof(strokeLinejoin)
    };
    /// <summary>
    ///     The style of the line's corners.
    /// </summary>
    public string strokeLinejoin
    {
        get => _strokeLinejoin?.value;
        set => SetValue(_strokeLinejoin_, ref _strokeLinejoin, value);
    }
    #endregion


    #region string strokeOpacity
    PropertyValueNode<string> _strokeOpacity;
    static readonly PropertyValueDefinition _strokeOpacity_ = new()
    {
        name = nameof(strokeOpacity)
    };
    /// <summary>
    ///     The opacity (transparency) of the line's stroke.
    /// </summary>
    public string strokeOpacity
    {
        get => _strokeOpacity?.value;
        set => SetValue(_strokeOpacity_, ref _strokeOpacity, value);
    }
    #endregion


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
    PropertyValueNode<string> _points;
    static readonly PropertyValueDefinition _points_ = new()
    {
        name = nameof(points)
    };
    /// <summary>
    ///     A list of points defining the vertices of the polyline.
    /// </summary>
    public string points
    {
        get => _points?.value;
        set => SetValue(_points_, ref _points, value);
    }
    #endregion


    #region string fill
    PropertyValueNode<string> _fill;
    static readonly PropertyValueDefinition _fill_ = new()
    {
        name = nameof(fill)
    };
    /// <summary>
    ///     The fill color of the polyline.
    /// </summary>
    public string fill
    {
        get => _fill?.value;
        set => SetValue(_fill_, ref _fill, value);
    }
    #endregion


    #region string stroke
    PropertyValueNode<string> _stroke;
    static readonly PropertyValueDefinition _stroke_ = new()
    {
        name = nameof(stroke)
    };
    /// <summary>
    ///     The stroke (outline) color of the polyline.
    /// </summary>
    public string stroke
    {
        get => _stroke?.value;
        set => SetValue(_stroke_, ref _stroke, value);
    }
    #endregion


    #region string strokeWidth
    PropertyValueNode<string> _strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth_ = new()
    {
        name = nameof(strokeWidth)
    };
    /// <summary>
    ///     The width of the polyline's outline.
    /// </summary>
    public string strokeWidth
    {
        get => _strokeWidth?.value;
        set => SetValue(_strokeWidth_, ref _strokeWidth, value);
    }
    #endregion


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
    PropertyValueNode<UnionProp<string,double>> _cx;
    static readonly PropertyValueDefinition _cx_ = new()
    {
        name = nameof(cx)
    };
    /// <summary>
    ///     The x-coordinate of the center of the circle.
    /// </summary>
    public UnionProp<string,double> cx
    {
        get => _cx?.value;
        set => SetValue(_cx_, ref _cx, value);
    }
    #endregion


    #region string cy
    PropertyValueNode<UnionProp<string,double>> _cy;
    static readonly PropertyValueDefinition _cy_ = new()
    {
        name = nameof(cy)
    };
    /// <summary>
    ///     The y-coordinate of the center of the circle.
    /// </summary>
    public UnionProp<string,double> cy
    {
        get => _cy?.value;
        set => SetValue(_cy_, ref _cy, value);
    }
    #endregion


    #region string r
    PropertyValueNode<UnionProp<string,double>> _r;
    static readonly PropertyValueDefinition _r_ = new()
    {
        name = nameof(r)
    };
    /// <summary>
    ///     The radius of the circle.
    /// </summary>
    public UnionProp<string,double> r
    {
        get => _r?.value;
        set => SetValue(_r_, ref _r, value);
    }
    #endregion


    #region string fill
    PropertyValueNode<string> _fill;
    static readonly PropertyValueDefinition _fill_ = new()
    {
        name = nameof(fill)
    };
    /// <summary>
    ///     The fill color of the circle.
    /// </summary>
    public string fill
    {
        get => _fill?.value;
        set => SetValue(_fill_, ref _fill, value);
    }
    #endregion


    #region string stroke
    PropertyValueNode<string> _stroke;
    static readonly PropertyValueDefinition _stroke_ = new()
    {
        name = nameof(stroke)
    };
    /// <summary>
    ///     The stroke color of the circle.
    /// </summary>
    public string stroke
    {
        get => _stroke?.value;
        set => SetValue(_stroke_, ref _stroke, value);
    }
    #endregion


    #region string strokeWidth
    PropertyValueNode<string> _strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth_ = new()
    {
        name = nameof(strokeWidth)
    };
    /// <summary>
    ///     The width of the stroke of the circle.
    /// </summary>
    public string strokeWidth
    {
        get => _strokeWidth?.value;
        set => SetValue(_strokeWidth_, ref _strokeWidth, value);
    }
    #endregion


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
    PropertyValueNode<string> _points;
    static readonly PropertyValueDefinition _points_ = new()
    {
        name = nameof(points)
    };
    /// <summary>
    ///     Specifies the coordinates of the polygon's vertices, in (x, y) pairs, separated by commas.
    /// </summary>
    public string points
    {
        get => _points?.value;
        set => SetValue(_points_, ref _points, value);
    }
    #endregion


    #region string fill
    PropertyValueNode<string> _fill;
    static readonly PropertyValueDefinition _fill_ = new()
    {
        name = nameof(fill)
    };
    /// <summary>
    ///     Specifies the fill color of the polygon.
    /// </summary>
    public string fill
    {
        get => _fill?.value;
        set => SetValue(_fill_, ref _fill, value);
    }
    #endregion


    #region string stroke
    PropertyValueNode<string> _stroke;
    static readonly PropertyValueDefinition _stroke_ = new()
    {
        name = nameof(stroke)
    };
    /// <summary>
    ///     Specifies the stroke color of the polygon.
    /// </summary>
    public string stroke
    {
        get => _stroke?.value;
        set => SetValue(_stroke_, ref _stroke, value);
    }
    #endregion


    #region string strokeWidth
    PropertyValueNode<string> _strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth_ = new()
    {
        name = nameof(strokeWidth)
    };
    /// <summary>
    ///     Specifies the width of the polygon's stroke, in pixels.
    /// </summary>
    public string strokeWidth
    {
        get => _strokeWidth?.value;
        set => SetValue(_strokeWidth_, ref _strokeWidth, value);
    }
    #endregion


    #region string strokeLinecap
    PropertyValueNode<string> _strokeLinecap;
    static readonly PropertyValueDefinition _strokeLinecap_ = new()
    {
        name = nameof(strokeLinecap)
    };
    /// <summary>
    ///     Specifies the type of line cap used for the polygon's stroke.
    /// </summary>
    public string strokeLinecap
    {
        get => _strokeLinecap?.value;
        set => SetValue(_strokeLinecap_, ref _strokeLinecap, value);
    }
    #endregion


    #region string strokeLinejoin
    PropertyValueNode<string> _strokeLinejoin;
    static readonly PropertyValueDefinition _strokeLinejoin_ = new()
    {
        name = nameof(strokeLinejoin)
    };
    /// <summary>
    ///     Specifies the type of line join used for the polygon's stroke.
    /// </summary>
    public string strokeLinejoin
    {
        get => _strokeLinejoin?.value;
        set => SetValue(_strokeLinejoin_, ref _strokeLinejoin, value);
    }
    #endregion


    #region string fillRule
    PropertyValueNode<string> _fillRule;
    static readonly PropertyValueDefinition _fillRule_ = new()
    {
        name = nameof(fillRule)
    };
    /// <summary>
    ///     Specifies how the polygon is filled.
    /// </summary>
    public string fillRule
    {
        get => _fillRule?.value;
        set => SetValue(_fillRule_, ref _fillRule, value);
    }
    #endregion


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
    PropertyValueNode<UnionProp<string,double>> _x;
    static readonly PropertyValueDefinition _x_ = new()
    {
        name = nameof(x)
    };
    /// <summary>
    ///     The x-coordinate of the top-left corner of the rectangle.
    /// </summary>
    public UnionProp<string,double> x
    {
        get => _x?.value;
        set => SetValue(_x_, ref _x, value);
    }
    #endregion


    #region string y
    PropertyValueNode<UnionProp<string,double>> _y;
    static readonly PropertyValueDefinition _y_ = new()
    {
        name = nameof(y)
    };
    /// <summary>
    ///     The y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    public UnionProp<string,double> y
    {
        get => _y?.value;
        set => SetValue(_y_, ref _y, value);
    }
    #endregion


    #region string width
    PropertyValueNode<UnionProp<string,double>> _width;
    static readonly PropertyValueDefinition _width_ = new()
    {
        name = nameof(width)
    };
    /// <summary>
    ///     The width of the rectangle.
    /// </summary>
    public UnionProp<string,double> width
    {
        get => _width?.value;
        set => SetValue(_width_, ref _width, value);
    }
    #endregion


    #region string height
    PropertyValueNode<UnionProp<string,double>> _height;
    static readonly PropertyValueDefinition _height_ = new()
    {
        name = nameof(height)
    };
    /// <summary>
    ///     The height of the rectangle.
    /// </summary>
    public UnionProp<string,double> height
    {
        get => _height?.value;
        set => SetValue(_height_, ref _height, value);
    }
    #endregion


    #region string rx
    PropertyValueNode<UnionProp<string,double>> _rx;
    static readonly PropertyValueDefinition _rx_ = new()
    {
        name = nameof(rx)
    };
    /// <summary>
    ///     The border radius of the rectangle on the horizontal axis.
    /// </summary>
    public UnionProp<string,double> rx
    {
        get => _rx?.value;
        set => SetValue(_rx_, ref _rx, value);
    }
    #endregion


    #region string ry
    PropertyValueNode<UnionProp<string,double>> _ry;
    static readonly PropertyValueDefinition _ry_ = new()
    {
        name = nameof(ry)
    };
    /// <summary>
    ///     The border radius of the rectangle on the vertical axis.
    /// </summary>
    public UnionProp<string,double> ry
    {
        get => _ry?.value;
        set => SetValue(_ry_, ref _ry, value);
    }
    #endregion


    #region string fill
    PropertyValueNode<string> _fill;
    static readonly PropertyValueDefinition _fill_ = new()
    {
        name = nameof(fill)
    };
    /// <summary>
    ///     The fill color of the rectangle.
    /// </summary>
    public string fill
    {
        get => _fill?.value;
        set => SetValue(_fill_, ref _fill, value);
    }
    #endregion


    #region string stroke
    PropertyValueNode<string> _stroke;
    static readonly PropertyValueDefinition _stroke_ = new()
    {
        name = nameof(stroke)
    };
    /// <summary>
    ///     The stroke color of the rectangle.
    /// </summary>
    public string stroke
    {
        get => _stroke?.value;
        set => SetValue(_stroke_, ref _stroke, value);
    }
    #endregion


    #region string strokeWidth
    PropertyValueNode<UnionProp<string,double>> _strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth_ = new()
    {
        name = nameof(strokeWidth)
    };
    /// <summary>
    ///     The width of the rectangle's stroke.
    /// </summary>
    public UnionProp<string,double> strokeWidth
    {
        get => _strokeWidth?.value;
        set => SetValue(_strokeWidth_, ref _strokeWidth, value);
    }
    #endregion


    #region string strokeLinecap
    PropertyValueNode<string> _strokeLinecap;
    static readonly PropertyValueDefinition _strokeLinecap_ = new()
    {
        name = nameof(strokeLinecap)
    };
    /// <summary>
    ///     The linecap style of the rectangle's stroke.
    /// </summary>
    public string strokeLinecap
    {
        get => _strokeLinecap?.value;
        set => SetValue(_strokeLinecap_, ref _strokeLinecap, value);
    }
    #endregion


    #region string strokeLinejoin
    PropertyValueNode<string> _strokeLinejoin;
    static readonly PropertyValueDefinition _strokeLinejoin_ = new()
    {
        name = nameof(strokeLinejoin)
    };
    /// <summary>
    ///     The linejoin style of the rectangle's stroke.
    /// </summary>
    public string strokeLinejoin
    {
        get => _strokeLinejoin?.value;
        set => SetValue(_strokeLinejoin_, ref _strokeLinejoin, value);
    }
    #endregion


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
    PropertyValueNode<string> _cx;
    static readonly PropertyValueDefinition _cx_ = new()
    {
        name = nameof(cx)
    };
    /// <summary>
    ///     The x-coordinate of the center of the gradient.
    /// </summary>
    public string cx
    {
        get => _cx?.value;
        set => SetValue(_cx_, ref _cx, value);
    }
    #endregion


    #region string cy
    PropertyValueNode<string> _cy;
    static readonly PropertyValueDefinition _cy_ = new()
    {
        name = nameof(cy)
    };
    /// <summary>
    ///     The y-coordinate of the center of the gradient.
    /// </summary>
    public string cy
    {
        get => _cy?.value;
        set => SetValue(_cy_, ref _cy, value);
    }
    #endregion


    #region string fx
    PropertyValueNode<string> _fx;
    static readonly PropertyValueDefinition _fx_ = new()
    {
        name = nameof(fx)
    };
    /// <summary>
    ///     The x-coordinate of the focal point of the gradient.
    /// </summary>
    public string fx
    {
        get => _fx?.value;
        set => SetValue(_fx_, ref _fx, value);
    }
    #endregion


    #region string fy
    PropertyValueNode<string> _fy;
    static readonly PropertyValueDefinition _fy_ = new()
    {
        name = nameof(fy)
    };
    /// <summary>
    ///     The y-coordinate of the focal point of the gradient.
    /// </summary>
    public string fy
    {
        get => _fy?.value;
        set => SetValue(_fy_, ref _fy, value);
    }
    #endregion


    #region string r
    PropertyValueNode<string> _r;
    static readonly PropertyValueDefinition _r_ = new()
    {
        name = nameof(r)
    };
    /// <summary>
    ///     The radius of the gradient.
    /// </summary>
    public string r
    {
        get => _r?.value;
        set => SetValue(_r_, ref _r, value);
    }
    #endregion


    #region string spreadMethod
    PropertyValueNode<string> _spreadMethod;
    static readonly PropertyValueDefinition _spreadMethod_ = new()
    {
        name = nameof(spreadMethod)
    };
    /// <summary>
    ///     The method used to spread the gradient.
    /// </summary>
    public string spreadMethod
    {
        get => _spreadMethod?.value;
        set => SetValue(_spreadMethod_, ref _spreadMethod, value);
    }
    #endregion


    #region string gradientUnits
    PropertyValueNode<string> _gradientUnits;
    static readonly PropertyValueDefinition _gradientUnits_ = new()
    {
        name = nameof(gradientUnits)
    };
    /// <summary>
    ///     The units used to specify the gradient.
    /// </summary>
    public string gradientUnits
    {
        get => _gradientUnits?.value;
        set => SetValue(_gradientUnits_, ref _gradientUnits, value);
    }
    #endregion


    #region string gradientTransform
    PropertyValueNode<string> _gradientTransform;
    static readonly PropertyValueDefinition _gradientTransform_ = new()
    {
        name = nameof(gradientTransform)
    };
    /// <summary>
    ///     A transform to apply to the gradient.
    /// </summary>
    public string gradientTransform
    {
        get => _gradientTransform?.value;
        set => SetValue(_gradientTransform_, ref _gradientTransform, value);
    }
    #endregion


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
    PropertyValueNode<string> _clipRule;
    static readonly PropertyValueDefinition _clipRule_ = new()
    {
        name = nameof(clipRule)
    };
    /// <summary>
    ///     Specifies the fill rule for the clipping path.
    /// </summary>
    public string clipRule
    {
        get => _clipRule?.value;
        set => SetValue(_clipRule_, ref _clipRule, value);
    }
    #endregion


    #region string clipBox
    PropertyValueNode<string> _clipBox;
    static readonly PropertyValueDefinition _clipBox_ = new()
    {
        name = nameof(clipBox)
    };
    /// <summary>
    ///     Specifies the reference box for the clipping path.
    /// </summary>
    public string clipBox
    {
        get => _clipBox?.value;
        set => SetValue(_clipBox_, ref _clipBox, value);
    }
    #endregion


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
    PropertyValueNode<string> _d;
    static readonly PropertyValueDefinition _d_ = new()
    {
        name = nameof(d)
    };
    /// <summary>
    ///     Path data
    /// </summary>
    public string d
    {
        get => _d?.value;
        set => SetValue(_d_, ref _d, value);
    }
    #endregion


    #region string fill
    PropertyValueNode<string> _fill;
    static readonly PropertyValueDefinition _fill_ = new()
    {
        name = nameof(fill)
    };
    /// <summary>
    ///     Fill color
    /// </summary>
    public string fill
    {
        get => _fill?.value;
        set => SetValue(_fill_, ref _fill, value);
    }
    #endregion


    #region string stroke
    PropertyValueNode<string> _stroke;
    static readonly PropertyValueDefinition _stroke_ = new()
    {
        name = nameof(stroke)
    };
    /// <summary>
    ///     Stroke color
    /// </summary>
    public string stroke
    {
        get => _stroke?.value;
        set => SetValue(_stroke_, ref _stroke, value);
    }
    #endregion


    #region string strokeWidth
    PropertyValueNode<string> _strokeWidth;
    static readonly PropertyValueDefinition _strokeWidth_ = new()
    {
        name = nameof(strokeWidth)
    };
    /// <summary>
    ///     Stroke width
    /// </summary>
    public string strokeWidth
    {
        get => _strokeWidth?.value;
        set => SetValue(_strokeWidth_, ref _strokeWidth, value);
    }
    #endregion


    #region string fillRule
    PropertyValueNode<string> _fillRule;
    static readonly PropertyValueDefinition _fillRule_ = new()
    {
        name = nameof(fillRule)
    };
    public string fillRule
    {
        get => _fillRule?.value;
        set => SetValue(_fillRule_, ref _fillRule, value);
    }
    #endregion


    #region string clipRule
    PropertyValueNode<string> _clipRule;
    static readonly PropertyValueDefinition _clipRule_ = new()
    {
        name = nameof(clipRule)
    };
    public string clipRule
    {
        get => _clipRule?.value;
        set => SetValue(_clipRule_, ref _clipRule, value);
    }
    #endregion


    #region string strokeLinecap
    PropertyValueNode<string> _strokeLinecap;
    static readonly PropertyValueDefinition _strokeLinecap_ = new()
    {
        name = nameof(strokeLinecap)
    };
    public string strokeLinecap
    {
        get => _strokeLinecap?.value;
        set => SetValue(_strokeLinecap_, ref _strokeLinecap, value);
    }
    #endregion


    #region string strokeLinejoin
    PropertyValueNode<string> _strokeLinejoin;
    static readonly PropertyValueDefinition _strokeLinejoin_ = new()
    {
        name = nameof(strokeLinejoin)
    };
    public string strokeLinejoin
    {
        get => _strokeLinejoin?.value;
        set => SetValue(_strokeLinejoin_, ref _strokeLinejoin, value);
    }
    #endregion


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
    PropertyValueNode<string> _opacity;
    static readonly PropertyValueDefinition _opacity_ = new()
    {
        name = nameof(opacity)
    };
    public string opacity
    {
        get => _opacity?.value;
        set => SetValue(_opacity_, ref _opacity, value);
    }
    #endregion


    #region string clipPath
    PropertyValueNode<string> _clipPath;
    static readonly PropertyValueDefinition _clipPath_ = new()
    {
        name = nameof(clipPath)
    };
    public string clipPath
    {
        get => _clipPath?.value;
        set => SetValue(_clipPath_, ref _clipPath, value);
    }
    #endregion


    #region string transform
    PropertyValueNode<string> _transform;
    static readonly PropertyValueDefinition _transform_ = new()
    {
        name = nameof(transform)
    };
    public string transform
    {
        get => _transform?.value;
        set => SetValue(_transform_, ref _transform, value);
    }
    #endregion


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
    PropertyValueNode<string> _height;
    static readonly PropertyValueDefinition _height_ = new()
    {
        name = nameof(height)
    };
    /// <summary>
    ///     This attribute defines the height of the masking area. Value type: length ; Default value: 120%; Animatable: yes
    /// </summary>
    public string height
    {
        get => _height?.value;
        set => SetValue(_height_, ref _height, value);
    }
    #endregion


    #region string maskContentUnits
    PropertyValueNode<string> _maskContentUnits;
    static readonly PropertyValueDefinition _maskContentUnits_ = new()
    {
        name = nameof(maskContentUnits)
    };
    /// <summary>
    ///     This attribute defines the coordinate system for the contents of the mask. Value type: userSpaceOnUse|objectBoundingBox ; Default value: userSpaceOnUse; Animatable: yes
    /// </summary>
    public string maskContentUnits
    {
        get => _maskContentUnits?.value;
        set => SetValue(_maskContentUnits_, ref _maskContentUnits, value);
    }
    #endregion


    #region string maskUnits
    PropertyValueNode<string> _maskUnits;
    static readonly PropertyValueDefinition _maskUnits_ = new()
    {
        name = nameof(maskUnits)
    };
    /// <summary>
    ///     This attribute defines the coordinate system for attributes x, y, width and height on the mask. Value type: userSpaceOnUse|objectBoundingBox ; Default value: objectBoundingBox; Animatable: yes
    /// </summary>
    public string maskUnits
    {
        get => _maskUnits?.value;
        set => SetValue(_maskUnits_, ref _maskUnits, value);
    }
    #endregion


    #region string x
    PropertyValueNode<string> _x;
    static readonly PropertyValueDefinition _x_ = new()
    {
        name = nameof(x)
    };
    /// <summary>
    ///     This attribute defines the x-axis coordinate of the top-left corner of the masking area. Value type: 'coordinate' ; Default value: -10%; Animatable: yes
    /// </summary>
    public string x
    {
        get => _x?.value;
        set => SetValue(_x_, ref _x, value);
    }
    #endregion


    #region string y
    PropertyValueNode<string> _y;
    static readonly PropertyValueDefinition _y_ = new()
    {
        name = nameof(y)
    };
    /// <summary>
    ///     This attribute defines the y-axis coordinate of the top-left corner of the masking area. Value type: 'coordinate' ; Default value: -10%; Animatable: yes
    /// </summary>
    public string y
    {
        get => _y?.value;
        set => SetValue(_y_, ref _y, value);
    }
    #endregion


    #region string width
    PropertyValueNode<string> _width;
    static readonly PropertyValueDefinition _width_ = new()
    {
        name = nameof(width)
    };
    /// <summary>
    ///     This attribute defines the width of the masking area. Value type: 'length' ; Default value: 120%; Animatable: yes
    /// </summary>
    public string width
    {
        get => _width?.value;
        set => SetValue(_width_, ref _width, value);
    }
    #endregion


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
    PropertyValueNode<string> _charset;
    static readonly PropertyValueDefinition _charset_ = new()
    {
        name = nameof(charset)
    };
    /// <summary>
    ///     Specifies the character encoding of the document.
    /// </summary>
    public string charset
    {
        get => _charset?.value;
        set => SetValue(_charset_, ref _charset, value);
    }
    #endregion


    #region string httpEquiv
    PropertyValueNode<string> _httpEquiv;
    static readonly PropertyValueDefinition _httpEquiv_ = new()
    {
        name = nameof(httpEquiv)
    };
    /// <summary>
    ///     Specifies the name of the HTTP header that the meta tag should be equivalent to.
    /// </summary>
    public string httpEquiv
    {
        get => _httpEquiv?.value;
        set => SetValue(_httpEquiv_, ref _httpEquiv, value);
    }
    #endregion


    #region string name
    PropertyValueNode<string> _name;
    static readonly PropertyValueDefinition _name_ = new()
    {
        name = nameof(name)
    };
    /// <summary>
    ///     Specifies the name of the metadata property.
    /// </summary>
    public string name
    {
        get => _name?.value;
        set => SetValue(_name_, ref _name, value);
    }
    #endregion


    #region string content
    PropertyValueNode<string> _content;
    static readonly PropertyValueDefinition _content_ = new()
    {
        name = nameof(content)
    };
    /// <summary>
    ///     Specifies the value of the metadata property.
    /// </summary>
    public string content
    {
        get => _content?.value;
        set => SetValue(_content_, ref _content, value);
    }
    #endregion


    #region string scheme
    PropertyValueNode<string> _scheme;
    static readonly PropertyValueDefinition _scheme_ = new()
    {
        name = nameof(scheme)
    };
    /// <summary>
    ///     Specifies the URL scheme for the content attribute of the meta tag.
    /// </summary>
    public string scheme
    {
        get => _scheme?.value;
        set => SetValue(_scheme_, ref _scheme, value);
    }
    #endregion


    #region string itemprop
    PropertyValueNode<string> _itemprop;
    static readonly PropertyValueDefinition _itemprop_ = new()
    {
        name = nameof(itemprop)
    };
    /// <summary>
    ///     Specifies the Microdata item property that the meta tag represents.
    /// </summary>
    public string itemprop
    {
        get => _itemprop?.value;
        set => SetValue(_itemprop_, ref _itemprop, value);
    }
    #endregion


    #region string property
    PropertyValueNode<string> _property;
    static readonly PropertyValueDefinition _property_ = new()
    {
        name = nameof(property)
    };
    /// <summary>
    ///     Specifies the schema.org property that the meta tag represents.
    /// </summary>
    public string property
    {
        get => _property?.value;
        set => SetValue(_property_, ref _property, value);
    }
    #endregion


    #region string src
    PropertyValueNode<string> _src;
    static readonly PropertyValueDefinition _src_ = new()
    {
        name = nameof(src)
    };
    /// <summary>
    ///     Specifies the URL for a resource associated with the meta tag.
    /// </summary>
    public string src
    {
        get => _src?.value;
        set => SetValue(_src_, ref _src, value);
    }
    #endregion


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
    PropertyValueNode<string> _background;
    static readonly PropertyValueDefinition _background_ = new()
    {
        name = nameof(background)
    };
    /// <summary>
    ///     Specifies the URL of a background image to be displayed behind the document's content.
    /// </summary>
    public string background
    {
        get => _background?.value;
        set => SetValue(_background_, ref _background, value);
    }
    #endregion


    #region string bgcolor
    PropertyValueNode<string> _bgcolor;
    static readonly PropertyValueDefinition _bgcolor_ = new()
    {
        name = nameof(bgcolor)
    };
    /// <summary>
    ///     Specifies the background color of the document's body.
    /// </summary>
    public string bgcolor
    {
        get => _bgcolor?.value;
        set => SetValue(_bgcolor_, ref _bgcolor, value);
    }
    #endregion


    #region string link
    PropertyValueNode<string> _link;
    static readonly PropertyValueDefinition _link_ = new()
    {
        name = nameof(link)
    };
    /// <summary>
    ///     Specifies the color of unvisited links in the document's body.
    /// </summary>
    public string link
    {
        get => _link?.value;
        set => SetValue(_link_, ref _link, value);
    }
    #endregion


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
    PropertyValueNode<string> _async;
    static readonly PropertyValueDefinition _async_ = new()
    {
        name = nameof(async)
    };
    /// <summary>
    ///     Specifies that the script should be executed asynchronously. This means that the browser will not wait for the script to finish executing before continuing to parse the rest of the HTML.
    /// </summary>
    public string async
    {
        get => _async?.value;
        set => SetValue(_async_, ref _async, value);
    }
    #endregion


    #region string defer
    PropertyValueNode<string> _defer;
    static readonly PropertyValueDefinition _defer_ = new()
    {
        name = nameof(defer)
    };
    /// <summary>
    ///     Specifies that the script should be executed after the browser has finished parsing the rest of the HTML. This is similar to async, but it ensures that scripts are executed in the order they are specified in the HTML.
    /// </summary>
    public string defer
    {
        get => _defer?.value;
        set => SetValue(_defer_, ref _defer, value);
    }
    #endregion


    #region string integrity
    PropertyValueNode<string> _integrity;
    static readonly PropertyValueDefinition _integrity_ = new()
    {
        name = nameof(integrity)
    };
    /// <summary>
    ///     Specifies a subresource integrity (SRI) hash for the script. This helps to protect against man-in-the-middle attacks.
    /// </summary>
    public string integrity
    {
        get => _integrity?.value;
        set => SetValue(_integrity_, ref _integrity, value);
    }
    #endregion


    #region string language
    PropertyValueNode<string> _language;
    static readonly PropertyValueDefinition _language_ = new()
    {
        name = nameof(language)
    };
    /// <summary>
    ///     Specifies the scripting language of the script. This is deprecated, but is still supported by most browsers.
    /// </summary>
    public string language
    {
        get => _language?.value;
        set => SetValue(_language_, ref _language, value);
    }
    #endregion


    #region string nomodule
    PropertyValueNode<string> _nomodule;
    static readonly PropertyValueDefinition _nomodule_ = new()
    {
        name = nameof(nomodule)
    };
    /// <summary>
    ///     Specifies that the script should be ignored if the browser does not support modules.
    /// </summary>
    public string nomodule
    {
        get => _nomodule?.value;
        set => SetValue(_nomodule_, ref _nomodule, value);
    }
    #endregion


    #region string src
    PropertyValueNode<string> _src;
    static readonly PropertyValueDefinition _src_ = new()
    {
        name = nameof(src)
    };
    /// <summary>
    ///     Specifies the URL of an external script file.
    /// </summary>
    public string src
    {
        get => _src?.value;
        set => SetValue(_src_, ref _src, value);
    }
    #endregion


    #region string type
    PropertyValueNode<string> _type;
    static readonly PropertyValueDefinition _type_ = new()
    {
        name = nameof(type)
    };
    /// <summary>
    ///     Specifies the type of the script. The most common value is application/javascript.
    /// </summary>
    public string type
    {
        get => _type?.value;
        set => SetValue(_type_, ref _type, value);
    }
    #endregion


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
    PropertyValueNode<string> _language;
    static readonly PropertyValueDefinition _language_ = new()
    {
        name = nameof(language)
    };
    /// <summary>
    ///     Specifies the language of the title.
    /// </summary>
    public string language
    {
        get => _language?.value;
        set => SetValue(_language_, ref _language, value);
    }
    #endregion


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
    PropertyValueNode<string> _profile;
    static readonly PropertyValueDefinition _profile_ = new()
    {
        name = nameof(profile)
    };
    /// <summary>
    ///     Provides a URL to a profile document for the current document.
    /// </summary>
    public string profile
    {
        get => _profile?.value;
        set => SetValue(_profile_, ref _profile, value);
    }
    #endregion


    #region string link
    PropertyValueNode<string> _link;
    static readonly PropertyValueDefinition _link_ = new()
    {
        name = nameof(link)
    };
    /// <summary>
    ///     Provides a link to an external resource, such as a stylesheet or script file.
    /// </summary>
    public string link
    {
        get => _link?.value;
        set => SetValue(_link_, ref _link, value);
    }
    #endregion


    #region string meta
    PropertyValueNode<string> _meta;
    static readonly PropertyValueDefinition _meta_ = new()
    {
        name = nameof(meta)
    };
    /// <summary>
    ///     Provides metadata about the document, such as the character encoding, author, and keywords.
    /// </summary>
    public string meta
    {
        get => _meta?.value;
        set => SetValue(_meta_, ref _meta, value);
    }
    #endregion


    #region string script
    PropertyValueNode<string> _script;
    static readonly PropertyValueDefinition _script_ = new()
    {
        name = nameof(script)
    };
    /// <summary>
    ///     Provides JavaScript code to be executed in the browser.
    /// </summary>
    public string script
    {
        get => _script?.value;
        set => SetValue(_script_, ref _script, value);
    }
    #endregion


    #region string noscript
    PropertyValueNode<string> _noscript;
    static readonly PropertyValueDefinition _noscript_ = new()
    {
        name = nameof(noscript)
    };
    /// <summary>
    ///     Provides content to be displayed if the browser does not support JavaScript.
    /// </summary>
    public string noscript
    {
        get => _noscript?.value;
        set => SetValue(_noscript_, ref _noscript, value);
    }
    #endregion


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
    PropertyValueNode<string> _hidden;
    static readonly PropertyValueDefinition _hidden_ = new()
    {
        name = nameof(hidden)
    };
    /// <summary>
    ///     Hides the element from display.
    /// </summary>
    public string hidden
    {
        get => _hidden?.value;
        set => SetValue(_hidden_, ref _hidden, value);
    }
    #endregion


    #region string manifest
    PropertyValueNode<string> _manifest;
    static readonly PropertyValueDefinition _manifest_ = new()
    {
        name = nameof(manifest)
    };
    /// <summary>
    ///     Specifies the URL of a manifest file, which provides information about the web app.
    /// </summary>
    public string manifest
    {
        get => _manifest?.value;
        set => SetValue(_manifest_, ref _manifest, value);
    }
    #endregion


    #region string xmlns
    PropertyValueNode<string> _xmlns;
    static readonly PropertyValueDefinition _xmlns_ = new()
    {
        name = nameof(xmlns)
    };
    /// <summary>
    ///     Specifies the namespace of the element.
    /// </summary>
    public string xmlns
    {
        get => _xmlns?.value;
        set => SetValue(_xmlns_, ref _xmlns, value);
    }
    #endregion


    #region string prefix
    PropertyValueNode<string> _prefix;
    static readonly PropertyValueDefinition _prefix_ = new()
    {
        name = nameof(prefix)
    };
    /// <summary>
    ///     Specifies the prefix of the element.
    /// </summary>
    public string prefix
    {
        get => _prefix?.value;
        set => SetValue(_prefix_, ref _prefix, value);
    }
    #endregion


    #region string version
    PropertyValueNode<string> _version;
    static readonly PropertyValueDefinition _version_ = new()
    {
        name = nameof(version)
    };
    /// <summary>
    ///     Specifies the version of the HTML specification to which the element conforms.
    /// </summary>
    public string version
    {
        get => _version?.value;
        set => SetValue(_version_, ref _version, value);
    }
    #endregion


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
    PropertyValueNode<string> _htmlFor;
    static readonly PropertyValueDefinition _htmlFor_ = new()
    {
        name = nameof(htmlFor)
    };
    /// <summary>
    ///     Specifies which form element a label is bound to.
    /// </summary>
    public string htmlFor
    {
        get => _htmlFor?.value;
        set => SetValue(_htmlFor_, ref _htmlFor, value);
    }
    #endregion


    #region string dropzone
    PropertyValueNode<string> _dropzone;
    static readonly PropertyValueDefinition _dropzone_ = new()
    {
        name = nameof(dropzone)
    };
    /// <summary>
    ///     Specifies whether the element is a drop target.
    /// </summary>
    public string dropzone
    {
        get => _dropzone?.value;
        set => SetValue(_dropzone_, ref _dropzone, value);
    }
    #endregion


    #region string hidden
    PropertyValueNode<string> _hidden;
    static readonly PropertyValueDefinition _hidden_ = new()
    {
        name = nameof(hidden)
    };
    /// <summary>
    ///     Hides the element from view.
    /// </summary>
    public string hidden
    {
        get => _hidden?.value;
        set => SetValue(_hidden_, ref _hidden, value);
    }
    #endregion


    #region string tabindex
    PropertyValueNode<string> _tabindex;
    static readonly PropertyValueDefinition _tabindex_ = new()
    {
        name = nameof(tabindex)
    };
    /// <summary>
    ///     Specifies the element's position in the tab order.
    /// </summary>
    public string tabindex
    {
        get => _tabindex?.value;
        set => SetValue(_tabindex_, ref _tabindex, value);
    }
    #endregion



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
    PropertyValueNode<string> _href;
    static readonly PropertyValueDefinition _href_ = new()
    {
        name = nameof(href)
    };
    /// <summary>
    ///     The URL of the linked resource.
    /// </summary>
    public string href
    {
        get => _href?.value;
        set => SetValue(_href_, ref _href, value);
    }
    #endregion


    #region string target
    PropertyValueNode<string> _target;
    static readonly PropertyValueDefinition _target_ = new()
    {
        name = nameof(target)
    };
    /// <summary>
    ///     Specifies where the linked resource should be opened. Can be `_blank`, `_self`, `_parent`, or `_top`.
    /// </summary>
    public string target
    {
        get => _target?.value;
        set => SetValue(_target_, ref _target, value);
    }
    #endregion


    #region string rel
    PropertyValueNode<string> _rel;
    static readonly PropertyValueDefinition _rel_ = new()
    {
        name = nameof(rel)
    };
    /// <summary>
    ///     Specifies the relationship between the current document and the linked resource. Can be `alternate`, `author`, `bookmark`, `canonical`, `external`, `help`, `license`, `next`, `nofollow`, `noreferrer`, `noopener`, `prev`, `search`, `sponsored`, or `stylesheet`.
    /// </summary>
    public string rel
    {
        get => _rel?.value;
        set => SetValue(_rel_, ref _rel, value);
    }
    #endregion


    #region string type
    PropertyValueNode<string> _type;
    static readonly PropertyValueDefinition _type_ = new()
    {
        name = nameof(type)
    };
    /// <summary>
    ///     Specifies the MIME type of the linked resource, if applicable.
    /// </summary>
    public string type
    {
        get => _type?.value;
        set => SetValue(_type_, ref _type, value);
    }
    #endregion


    #region string download
    PropertyValueNode<string> _download;
    static readonly PropertyValueDefinition _download_ = new()
    {
        name = nameof(download)
    };
    /// <summary>
    ///     Specifies whether the linked resource should be downloaded or opened in a new browser tab.
    /// </summary>
    public string download
    {
        get => _download?.value;
        set => SetValue(_download_, ref _download, value);
    }
    #endregion


    #region string ping
    PropertyValueNode<string> _ping;
    static readonly PropertyValueDefinition _ping_ = new()
    {
        name = nameof(ping)
    };
    /// <summary>
    ///     A list of URLs to which a ping should be sent when the user clicks on the link.
    /// </summary>
    public string ping
    {
        get => _ping?.value;
        set => SetValue(_ping_, ref _ping, value);
    }
    #endregion


    #region string media
    PropertyValueNode<string> _media;
    static readonly PropertyValueDefinition _media_ = new()
    {
        name = nameof(media)
    };
    /// <summary>
    ///     Specifies the media types for which the link is relevant.
    /// </summary>
    public string media
    {
        get => _media?.value;
        set => SetValue(_media_, ref _media, value);
    }
    #endregion


    #region string hreflang
    PropertyValueNode<string> _hreflang;
    static readonly PropertyValueDefinition _hreflang_ = new()
    {
        name = nameof(hreflang)
    };
    /// <summary>
    ///     Specifies the language of the linked resource.
    /// </summary>
    public string hreflang
    {
        get => _hreflang?.value;
        set => SetValue(_hreflang_, ref _hreflang, value);
    }
    #endregion


    #region string name
    PropertyValueNode<string> _name;
    static readonly PropertyValueDefinition _name_ = new()
    {
        name = nameof(name)
    };
    /// <summary>
    ///     Specifies a name for the link. This can be used to target the link with JavaScript.
    /// </summary>
    public string name
    {
        get => _name?.value;
        set => SetValue(_name_, ref _name, value);
    }
    #endregion


    #region string tabindex
    PropertyValueNode<string> _tabindex;
    static readonly PropertyValueDefinition _tabindex_ = new()
    {
        name = nameof(tabindex)
    };
    /// <summary>
    ///     Specifies the tab order of the link.
    /// </summary>
    public string tabindex
    {
        get => _tabindex?.value;
        set => SetValue(_tabindex_, ref _tabindex, value);
    }
    #endregion


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
    PropertyValueNode<string> _src;
    static readonly PropertyValueDefinition _src_ = new()
    {
        name = nameof(src)
    };
    /// <summary>
    ///     The URL of the image file.
    /// </summary>
    public string src
    {
        get => _src?.value;
        set => SetValue(_src_, ref _src, value);
    }
    #endregion


    #region string srcset
    PropertyValueNode<string> _srcset;
    static readonly PropertyValueDefinition _srcset_ = new()
    {
        name = nameof(srcset)
    };
    /// <summary>
    ///     A list of image files to use in different situations, such as different screen sizes or device types.
    /// </summary>
    public string srcset
    {
        get => _srcset?.value;
        set => SetValue(_srcset_, ref _srcset, value);
    }
    #endregion


    #region string usemap
    PropertyValueNode<string> _usemap;
    static readonly PropertyValueDefinition _usemap_ = new()
    {
        name = nameof(usemap)
    };
    /// <summary>
    ///     Specifies an image as a client-side image map.
    /// </summary>
    public string usemap
    {
        get => _usemap?.value;
        set => SetValue(_usemap_, ref _usemap, value);
    }
    #endregion


    #region string alt
    PropertyValueNode<string> _alt;
    static readonly PropertyValueDefinition _alt_ = new()
    {
        name = nameof(alt)
    };
    /// <summary>
    ///     An alternate text for the image, if the image for some reason cannot be displayed.
    /// </summary>
    public string alt
    {
        get => _alt?.value;
        set => SetValue(_alt_, ref _alt, value);
    }
    #endregion


    #region string width
    PropertyValueNode<UnionProp<string,double?>> _width;
    static readonly PropertyValueDefinition _width_ = new()
    {
        name = nameof(width)
    };
    /// <summary>
    ///     The width of the image, in pixels.
    /// </summary>
    public UnionProp<string,double?> width
    {
        get => _width?.value;
        set => SetValue(_width_, ref _width, value);
    }
    #endregion


    #region string height
    PropertyValueNode<UnionProp<string,double?>> _height;
    static readonly PropertyValueDefinition _height_ = new()
    {
        name = nameof(height)
    };
    /// <summary>
    ///     The height of the image, in pixels.
    /// </summary>
    public UnionProp<string,double?> height
    {
        get => _height?.value;
        set => SetValue(_height_, ref _height, value);
    }
    #endregion


    #region string ismap
    PropertyValueNode<string> _ismap;
    static readonly PropertyValueDefinition _ismap_ = new()
    {
        name = nameof(ismap)
    };
    /// <summary>
    ///     A Boolean attribute that indicates whether the image is an image map.
    /// </summary>
    public string ismap
    {
        get => _ismap?.value;
        set => SetValue(_ismap_, ref _ismap, value);
    }
    #endregion


    #region string longdesc
    PropertyValueNode<string> _longdesc;
    static readonly PropertyValueDefinition _longdesc_ = new()
    {
        name = nameof(longdesc)
    };
    /// <summary>
    ///     A longer description of the image, for use by screen readers and other assistive technologies.
    /// </summary>
    public string longdesc
    {
        get => _longdesc?.value;
        set => SetValue(_longdesc_, ref _longdesc, value);
    }
    #endregion


    #region string crossorigin
    PropertyValueNode<string> _crossorigin;
    static readonly PropertyValueDefinition _crossorigin_ = new()
    {
        name = nameof(crossorigin)
    };
    /// <summary>
    ///     A string that specifies the CORS setting for the image.
    /// </summary>
    public string crossorigin
    {
        get => _crossorigin?.value;
        set => SetValue(_crossorigin_, ref _crossorigin, value);
    }
    #endregion


    #region string loading
    PropertyValueNode<string> _loading;
    static readonly PropertyValueDefinition _loading_ = new()
    {
        name = nameof(loading)
    };
    /// <summary>
    ///     A string that specifies how the image should be loaded.
    /// </summary>
    public string loading
    {
        get => _loading?.value;
        set => SetValue(_loading_, ref _loading, value);
    }
    #endregion


    #region string decoding
    PropertyValueNode<string> _decoding;
    static readonly PropertyValueDefinition _decoding_ = new()
    {
        name = nameof(decoding)
    };
    /// <summary>
    ///     A string that specifies how the image should be decoded.
    /// </summary>
    public string decoding
    {
        get => _decoding?.value;
        set => SetValue(_decoding_, ref _decoding, value);
    }
    #endregion


    #region string referrerpolicy
    PropertyValueNode<string> _referrerpolicy;
    static readonly PropertyValueDefinition _referrerpolicy_ = new()
    {
        name = nameof(referrerpolicy)
    };
    /// <summary>
    ///     A string that specifies how much referrer information is sent with requests for the image.
    /// </summary>
    public string referrerpolicy
    {
        get => _referrerpolicy?.value;
        set => SetValue(_referrerpolicy_, ref _referrerpolicy, value);
    }
    #endregion


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
    PropertyValueNode<string> _focusable;
    static readonly PropertyValueDefinition _focusable_ = new()
    {
        name = nameof(focusable)
    };
    public string focusable
    {
        get => _focusable?.value;
        set => SetValue(_focusable_, ref _focusable, value);
    }
    #endregion


    #region string xlinkHref
    PropertyValueNode<string> _xlinkHref;
    static readonly PropertyValueDefinition _xlinkHref_ = new()
    {
        name = nameof(xlinkHref)
    };
    public string xlinkHref
    {
        get => _xlinkHref?.value;
        set => SetValue(_xlinkHref_, ref _xlinkHref, value);
    }
    #endregion


    #region string xmlnsXlink
    PropertyValueNode<string> _xmlnsXlink;
    static readonly PropertyValueDefinition _xmlnsXlink_ = new()
    {
        name = nameof(xmlnsXlink)
    };
    public string xmlnsXlink
    {
        get => _xmlnsXlink?.value;
        set => SetValue(_xmlnsXlink_, ref _xmlnsXlink, value);
    }
    #endregion


    #region string preserveAspectRatio
    PropertyValueNode<string> _preserveAspectRatio;
    static readonly PropertyValueDefinition _preserveAspectRatio_ = new()
    {
        name = nameof(preserveAspectRatio)
    };
    /// <summary>
    ///     Specifies how the SVG element should be scaled and aligned to fit its viewport.
    /// </summary>
    public string preserveAspectRatio
    {
        get => _preserveAspectRatio?.value;
        set => SetValue(_preserveAspectRatio_, ref _preserveAspectRatio, value);
    }
    #endregion


    #region string width
    PropertyValueNode<string> _width;
    static readonly PropertyValueDefinition _width_ = new()
    {
        name = nameof(width)
    };
    /// <summary>
    ///     The width of the SVG element in pixels.
    /// </summary>
    public string width
    {
        get => _width?.value;
        set => SetValue(_width_, ref _width, value);
    }
    #endregion


    #region string height
    PropertyValueNode<string> _height;
    static readonly PropertyValueDefinition _height_ = new()
    {
        name = nameof(height)
    };
    /// <summary>
    ///     The height of the SVG element in pixels.
    /// </summary>
    public string height
    {
        get => _height?.value;
        set => SetValue(_height_, ref _height, value);
    }
    #endregion


    #region string xmlns
    PropertyValueNode<string> _xmlns;
    static readonly PropertyValueDefinition _xmlns_ = new()
    {
        name = nameof(xmlns)
    };
    /// <summary>
    ///     The namespace URI for the SVG element.
    /// </summary>
    public string xmlns
    {
        get => _xmlns?.value;
        set => SetValue(_xmlns_, ref _xmlns, value);
    }
    #endregion


    #region string version
    PropertyValueNode<string> _version;
    static readonly PropertyValueDefinition _version_ = new()
    {
        name = nameof(version)
    };
    /// <summary>
    ///     The SVG version of the element.
    /// </summary>
    public string version
    {
        get => _version?.value;
        set => SetValue(_version_, ref _version, value);
    }
    #endregion


    #region string viewBox
    PropertyValueNode<string> _viewBox;
    static readonly PropertyValueDefinition _viewBox_ = new()
    {
        name = nameof(viewBox)
    };
    public string viewBox
    {
        get => _viewBox?.value;
        set => SetValue(_viewBox_, ref _viewBox, value);
    }
    #endregion


    #region string fill
    PropertyValueNode<string> _fill;
    static readonly PropertyValueDefinition _fill_ = new()
    {
        name = nameof(fill)
    };
    public string fill
    {
        get => _fill?.value;
        set => SetValue(_fill_, ref _fill, value);
    }
    #endregion


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
    PropertyValueNode<string> _offset;
    static readonly PropertyValueDefinition _offset_ = new()
    {
        name = nameof(offset)
    };
    public string offset
    {
        get => _offset?.value;
        set => SetValue(_offset_, ref _offset, value);
    }
    #endregion


    #region string stopColor
    PropertyValueNode<string> _stopColor;
    static readonly PropertyValueDefinition _stopColor_ = new()
    {
        name = nameof(stopColor)
    };
    public string stopColor
    {
        get => _stopColor?.value;
        set => SetValue(_stopColor_, ref _stopColor, value);
    }
    #endregion


    #region string stopOpacity
    PropertyValueNode<string> _stopOpacity;
    static readonly PropertyValueDefinition _stopOpacity_ = new()
    {
        name = nameof(stopOpacity)
    };
    public string stopOpacity
    {
        get => _stopOpacity?.value;
        set => SetValue(_stopOpacity_, ref _stopOpacity, value);
    }
    #endregion


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
    PropertyValueNode<string> _action;
    static readonly PropertyValueDefinition _action_ = new()
    {
        name = nameof(action)
    };
    /// <summary>
    ///     Specifies the URL of the page where the form data will be submitted.
    /// </summary>
    public string action
    {
        get => _action?.value;
        set => SetValue(_action_, ref _action, value);
    }
    #endregion


    #region string method
    PropertyValueNode<string> _method;
    static readonly PropertyValueDefinition _method_ = new()
    {
        name = nameof(method)
    };
    /// <summary>
    ///     Specifies how the form data will be sent to the server. Possible values are 'get' and 'post'.
    /// </summary>
    public string method
    {
        get => _method?.value;
        set => SetValue(_method_, ref _method, value);
    }
    #endregion


    #region string enctype
    PropertyValueNode<string> _enctype;
    static readonly PropertyValueDefinition _enctype_ = new()
    {
        name = nameof(enctype)
    };
    /// <summary>
    ///     Specifies the encoding type for form data. Possible values are 'application/x-www-form-urlencoded' and 'multipart/form-data'.
    /// </summary>
    public string enctype
    {
        get => _enctype?.value;
        set => SetValue(_enctype_, ref _enctype, value);
    }
    #endregion


    #region string target
    PropertyValueNode<string> _target;
    static readonly PropertyValueDefinition _target_ = new()
    {
        name = nameof(target)
    };
    /// <summary>
    ///     Specifies the name of the frame where the form will be submitted. The default value is '_self', which means the form will be submitted in the current frame.
    /// </summary>
    public string target
    {
        get => _target?.value;
        set => SetValue(_target_, ref _target, value);
    }
    #endregion


    #region string name
    PropertyValueNode<string> _name;
    static readonly PropertyValueDefinition _name_ = new()
    {
        name = nameof(name)
    };
    /// <summary>
    ///     Specifies a name for the form. This name is used to reference the form in JavaScript or to reference form data after a form is submitted.
    /// </summary>
    public string name
    {
        get => _name?.value;
        set => SetValue(_name_, ref _name, value);
    }
    #endregion


    #region string novalidate
    PropertyValueNode<string> _novalidate;
    static readonly PropertyValueDefinition _novalidate_ = new()
    {
        name = nameof(novalidate)
    };
    /// <summary>
    ///     Disables form validation. This attribute is useful when you want to submit the form without validating the user input.
    /// </summary>
    public string novalidate
    {
        get => _novalidate?.value;
        set => SetValue(_novalidate_, ref _novalidate, value);
    }
    #endregion


    #region string autocomplete
    PropertyValueNode<string> _autocomplete;
    static readonly PropertyValueDefinition _autocomplete_ = new()
    {
        name = nameof(autocomplete)
    };
    /// <summary>
    ///     Specifies whether the browser should automatically fill in form fields based on the user's past input.
    /// </summary>
    public string autocomplete
    {
        get => _autocomplete?.value;
        set => SetValue(_autocomplete_, ref _autocomplete, value);
    }
    #endregion


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
    PropertyValueNode<string> _name;
    static readonly PropertyValueDefinition _name_ = new()
    {
        name = nameof(name)
    };
    /// <summary>
    ///     Specifies a name for the textarea element.
    /// </summary>
    public string name
    {
        get => _name?.value;
        set => SetValue(_name_, ref _name, value);
    }
    #endregion


    #region string cols
    PropertyValueNode<UnionProp<string,int?>> _cols;
    static readonly PropertyValueDefinition _cols_ = new()
    {
        name = nameof(cols)
    };
    /// <summary>
    ///     Specifies the visible width of the textarea element in characters.
    /// </summary>
    public UnionProp<string,int?> cols
    {
        get => _cols?.value;
        set => SetValue(_cols_, ref _cols, value);
    }
    #endregion


    #region string rows
    PropertyValueNode<UnionProp<string,int?>> _rows;
    static readonly PropertyValueDefinition _rows_ = new()
    {
        name = nameof(rows)
    };
    /// <summary>
    ///     Specifies the number of visible lines in the textarea element.
    /// </summary>
    public UnionProp<string,int?> rows
    {
        get => _rows?.value;
        set => SetValue(_rows_, ref _rows, value);
    }
    #endregion


    #region string placeholder
    PropertyValueNode<string> _placeholder;
    static readonly PropertyValueDefinition _placeholder_ = new()
    {
        name = nameof(placeholder)
    };
    /// <summary>
    ///     Specifies a short hint that describes the expected value of the textarea element.
    /// </summary>
    public string placeholder
    {
        get => _placeholder?.value;
        set => SetValue(_placeholder_, ref _placeholder, value);
    }
    #endregion


    #region string readOnly
    PropertyValueNode<UnionProp<string,bool>> _readOnly;
    static readonly PropertyValueDefinition _readOnly_ = new()
    {
        name = nameof(readOnly)
    };
    /// <summary>
    ///     Disables user input in the textarea element.
    /// </summary>
    public UnionProp<string,bool> readOnly
    {
        get => _readOnly?.value;
        set => SetValue(_readOnly_, ref _readOnly, value);
    }
    #endregion


    #region string required
    PropertyValueNode<string> _required;
    static readonly PropertyValueDefinition _required_ = new()
    {
        name = nameof(required)
    };
    /// <summary>
    ///     Indicates that the textarea element must be filled out before the form is submitted.
    /// </summary>
    public string required
    {
        get => _required?.value;
        set => SetValue(_required_, ref _required, value);
    }
    #endregion


    #region string autofocus
    PropertyValueNode<string> _autofocus;
    static readonly PropertyValueDefinition _autofocus_ = new()
    {
        name = nameof(autofocus)
    };
    /// <summary>
    ///     Automatically gives focus to the textarea element when the page loads.
    /// </summary>
    public string autofocus
    {
        get => _autofocus?.value;
        set => SetValue(_autofocus_, ref _autofocus, value);
    }
    #endregion


    #region string autocomplete
    PropertyValueNode<string> _autocomplete;
    static readonly PropertyValueDefinition _autocomplete_ = new()
    {
        name = nameof(autocomplete)
    };
    /// <summary>
    ///     Specifies that the user's browser should automatically complete the textarea element's value.
    /// </summary>
    public string autocomplete
    {
        get => _autocomplete?.value;
        set => SetValue(_autocomplete_, ref _autocomplete, value);
    }
    #endregion


    #region string dirname
    PropertyValueNode<string> _dirname;
    static readonly PropertyValueDefinition _dirname_ = new()
    {
        name = nameof(dirname)
    };
    /// <summary>
    ///     Specifies the directory to use as the default value for the 'file' input type.
    /// </summary>
    public string dirname
    {
        get => _dirname?.value;
        set => SetValue(_dirname_, ref _dirname, value);
    }
    #endregion


    #region string form
    PropertyValueNode<string> _form;
    static readonly PropertyValueDefinition _form_ = new()
    {
        name = nameof(form)
    };
    /// <summary>
    ///     Specifies the ID of the form that the textarea element belongs to.
    /// </summary>
    public string form
    {
        get => _form?.value;
        set => SetValue(_form_, ref _form, value);
    }
    #endregion


    #region string maxlength
    PropertyValueNode<string> _maxlength;
    static readonly PropertyValueDefinition _maxlength_ = new()
    {
        name = nameof(maxlength)
    };
    /// <summary>
    ///     Specifies the maximum number of characters that can be entered into the textarea element.
    /// </summary>
    public string maxlength
    {
        get => _maxlength?.value;
        set => SetValue(_maxlength_, ref _maxlength, value);
    }
    #endregion


    #region string minlength
    PropertyValueNode<string> _minlength;
    static readonly PropertyValueDefinition _minlength_ = new()
    {
        name = nameof(minlength)
    };
    /// <summary>
    ///     Specifies the minimum number of characters that must be entered into the textarea element.
    /// </summary>
    public string minlength
    {
        get => _minlength?.value;
        set => SetValue(_minlength_, ref _minlength, value);
    }
    #endregion


    #region string wrap
    PropertyValueNode<string> _wrap;
    static readonly PropertyValueDefinition _wrap_ = new()
    {
        name = nameof(wrap)
    };
    /// <summary>
    ///     Specifies whether the text in the textarea element should wrap to the next line when it reaches the end of the visible area.
    /// </summary>
    public string wrap
    {
        get => _wrap?.value;
        set => SetValue(_wrap_, ref _wrap, value);
    }
    #endregion


    #region string defaultValue
    PropertyValueNode<string> _defaultValue;
    static readonly PropertyValueDefinition _defaultValue_ = new()
    {
        name = nameof(defaultValue)
    };
    /// <summary>
    ///     A string. Specifies the initial value for a text area.
    /// </summary>
    public string defaultValue
    {
        get => _defaultValue?.value;
        set => SetValue(_defaultValue_, ref _defaultValue, value);
    }
    #endregion


    #region string value
    PropertyValueNode<string> _value;
    static readonly PropertyValueDefinition _value_ = new()
    {
        name = nameof(value)
    };
    public string value
    {
        get => _value?.value;
        set => SetValue(_value_, ref _value, value);
    }
    #endregion


    #region string disabled
    PropertyValueNode<string> _disabled;
    static readonly PropertyValueDefinition _disabled_ = new()
    {
        name = nameof(disabled)
    };
    public string disabled
    {
        get => _disabled?.value;
        set => SetValue(_disabled_, ref _disabled, value);
    }
    #endregion


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
    PropertyValueNode<string> _href;
    static readonly PropertyValueDefinition _href_ = new()
    {
        name = nameof(href)
    };
    public string href
    {
        get => _href?.value;
        set => SetValue(_href_, ref _href, value);
    }
    #endregion


    #region string media
    PropertyValueNode<string> _media;
    static readonly PropertyValueDefinition _media_ = new()
    {
        name = nameof(media)
    };
    public string media
    {
        get => _media?.value;
        set => SetValue(_media_, ref _media, value);
    }
    #endregion


    #region string rel
    PropertyValueNode<string> _rel;
    static readonly PropertyValueDefinition _rel_ = new()
    {
        name = nameof(rel)
    };
    public string rel
    {
        get => _rel?.value;
        set => SetValue(_rel_, ref _rel, value);
    }
    #endregion


    #region string sizes
    PropertyValueNode<string> _sizes;
    static readonly PropertyValueDefinition _sizes_ = new()
    {
        name = nameof(sizes)
    };
    public string sizes
    {
        get => _sizes?.value;
        set => SetValue(_sizes_, ref _sizes, value);
    }
    #endregion


    #region string type
    PropertyValueNode<string> _type;
    static readonly PropertyValueDefinition _type_ = new()
    {
        name = nameof(type)
    };
    public string type
    {
        get => _type?.value;
        set => SetValue(_type_, ref _type, value);
    }
    #endregion


    #region string @as
    PropertyValueNode<string> _as;
    static readonly PropertyValueDefinition _as_ = new()
    {
        name = nameof(@as)
    };
    public string @as
    {
        get => _as?.value;
        set => SetValue(_as_, ref _as, value);
    }
    #endregion


    #region string integrity
    PropertyValueNode<string> _integrity;
    static readonly PropertyValueDefinition _integrity_ = new()
    {
        name = nameof(integrity)
    };
    public string integrity
    {
        get => _integrity?.value;
        set => SetValue(_integrity_, ref _integrity, value);
    }
    #endregion


    #region string crossorigin
    PropertyValueNode<string> _crossorigin;
    static readonly PropertyValueDefinition _crossorigin_ = new()
    {
        name = nameof(crossorigin)
    };
    public string crossorigin
    {
        get => _crossorigin?.value;
        set => SetValue(_crossorigin_, ref _crossorigin, value);
    }
    #endregion


    #region string referrerpolicy
    PropertyValueNode<string> _referrerpolicy;
    static readonly PropertyValueDefinition _referrerpolicy_ = new()
    {
        name = nameof(referrerpolicy)
    };
    public string referrerpolicy
    {
        get => _referrerpolicy?.value;
        set => SetValue(_referrerpolicy_, ref _referrerpolicy, value);
    }
    #endregion


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
    PropertyValueNode<string> _src;
    static readonly PropertyValueDefinition _src_ = new()
    {
        name = nameof(src)
    };
    public string src
    {
        get => _src?.value;
        set => SetValue(_src_, ref _src, value);
    }
    #endregion


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
    PropertyValueNode<string> _value;
    static readonly PropertyValueDefinition _value_ = new()
    {
        name = nameof(value)
    };
    public string value
    {
        get => _value?.value;
        set => SetValue(_value_, ref _value, value);
    }
    #endregion


    #region string disabled
    PropertyValueNode<string> _disabled;
    static readonly PropertyValueDefinition _disabled_ = new()
    {
        name = nameof(disabled)
    };
    public string disabled
    {
        get => _disabled?.value;
        set => SetValue(_disabled_, ref _disabled, value);
    }
    #endregion


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
    PropertyValueNode<string> _required;
    static readonly PropertyValueDefinition _required_ = new()
    {
        name = nameof(required)
    };
    public string required
    {
        get => _required?.value;
        set => SetValue(_required_, ref _required, value);
    }
    #endregion


    #region string autoComplete
    PropertyValueNode<string> _autoComplete;
    static readonly PropertyValueDefinition _autoComplete_ = new()
    {
        name = nameof(autoComplete)
    };
    public string autoComplete
    {
        get => _autoComplete?.value;
        set => SetValue(_autoComplete_, ref _autoComplete, value);
    }
    #endregion


    #region string @checked
    PropertyValueNode<bool?> _checked;
    static readonly PropertyValueDefinition _checked_ = new()
    {
        name = nameof(@checked)
    };
    public bool? @checked
    {
        get => _checked?.value;
        set => SetValue(_checked_, ref _checked, value);
    }
    #endregion


    #region string defaultChecked
    PropertyValueNode<bool?> _defaultChecked;
    static readonly PropertyValueDefinition _defaultChecked_ = new()
    {
        name = nameof(defaultChecked)
    };
    public bool? defaultChecked
    {
        get => _defaultChecked?.value;
        set => SetValue(_defaultChecked_, ref _defaultChecked, value);
    }
    #endregion


    #region string defaultValue
    PropertyValueNode<string> _defaultValue;
    static readonly PropertyValueDefinition _defaultValue_ = new()
    {
        name = nameof(defaultValue)
    };
    public string defaultValue
    {
        get => _defaultValue?.value;
        set => SetValue(_defaultValue_, ref _defaultValue, value);
    }
    #endregion


    #region string disabled
    PropertyValueNode<bool?> _disabled;
    static readonly PropertyValueDefinition _disabled_ = new()
    {
        name = nameof(disabled)
    };
    public bool? disabled
    {
        get => _disabled?.value;
        set => SetValue(_disabled_, ref _disabled, value);
    }
    #endregion


    #region string autoFocus
    PropertyValueNode<bool?> _autoFocus;
    static readonly PropertyValueDefinition _autoFocus_ = new()
    {
        name = nameof(autoFocus)
    };
    /// <summary>
    ///     Element must automatically get focus when the page loads.
    /// </summary>
    public bool? autoFocus
    {
        get => _autoFocus?.value;
        set => SetValue(_autoFocus_, ref _autoFocus, value);
    }
    #endregion


    #region string name
    PropertyValueNode<string> _name;
    static readonly PropertyValueDefinition _name_ = new()
    {
        name = nameof(name)
    };
    public string name
    {
        get => _name?.value;
        set => SetValue(_name_, ref _name, value);
    }
    #endregion


    #region string placeholder
    PropertyValueNode<string> _placeholder;
    static readonly PropertyValueDefinition _placeholder_ = new()
    {
        name = nameof(placeholder)
    };
    public string placeholder
    {
        get => _placeholder?.value;
        set => SetValue(_placeholder_, ref _placeholder, value);
    }
    #endregion


    #region string readOnly
    PropertyValueNode<bool?> _readOnly;
    static readonly PropertyValueDefinition _readOnly_ = new()
    {
        name = nameof(readOnly)
    };
    public bool? readOnly
    {
        get => _readOnly?.value;
        set => SetValue(_readOnly_, ref _readOnly, value);
    }
    #endregion


    #region string type
    PropertyValueNode<string> _type;
    static readonly PropertyValueDefinition _type_ = new()
    {
        name = nameof(type)
    };
    public string type
    {
        get => _type?.value;
        set => SetValue(_type_, ref _type, value);
    }
    #endregion


    #region string max
    PropertyValueNode<int?> _max;
    static readonly PropertyValueDefinition _max_ = new()
    {
        name = nameof(max)
    };
    public int? max
    {
        get => _max?.value;
        set => SetValue(_max_, ref _max, value);
    }
    #endregion


    #region string min
    PropertyValueNode<int?> _min;
    static readonly PropertyValueDefinition _min_ = new()
    {
        name = nameof(min)
    };
    public int? min
    {
        get => _min?.value;
        set => SetValue(_min_, ref _min, value);
    }
    #endregion


    #region string step
    PropertyValueNode<int?> _step;
    static readonly PropertyValueDefinition _step_ = new()
    {
        name = nameof(step)
    };
    public int? step
    {
        get => _step?.value;
        set => SetValue(_step_, ref _step, value);
    }
    #endregion


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
    PropertyValueNode<string> _accesskey;
    static readonly PropertyValueDefinition _accesskey_ = new()
    {
        name = nameof(accesskey)
    };
    public string accesskey
    {
        get => _accesskey?.value;
        set => SetValue(_accesskey_, ref _accesskey, value);
    }
    #endregion


    #region string draggable
    PropertyValueNode<string> _draggable;
    static readonly PropertyValueDefinition _draggable_ = new()
    {
        name = nameof(draggable)
    };
    public string draggable
    {
        get => _draggable?.value;
        set => SetValue(_draggable_, ref _draggable, value);
    }
    #endregion


    #region string contenteditable
    PropertyValueNode<string> _contenteditable;
    static readonly PropertyValueDefinition _contenteditable_ = new()
    {
        name = nameof(contenteditable)
    };
    public string contenteditable
    {
        get => _contenteditable?.value;
        set => SetValue(_contenteditable_, ref _contenteditable, value);
    }
    #endregion


    #region string className
    PropertyValueNode<string> _className;
    static readonly PropertyValueDefinition _className_ = new()
    {
        name = nameof(className)
    };
    public string className
    {
        get => _className?.value;
        set => SetValue(_className_, ref _className, value);
    }
    #endregion


    #region string dangerouslySetInnerHTML
    PropertyValueNode<dangerouslySetInnerHTML> _dangerouslySetInnerHTML;
    static readonly PropertyValueDefinition _dangerouslySetInnerHTML_ = new()
    {
        name = nameof(dangerouslySetInnerHTML)
    };
    public dangerouslySetInnerHTML dangerouslySetInnerHTML
    {
        get => _dangerouslySetInnerHTML?.value;
        set => SetValue(_dangerouslySetInnerHTML_, ref _dangerouslySetInnerHTML, value);
    }
    #endregion


    #region string dir
    PropertyValueNode<string> _dir;
    static readonly PropertyValueDefinition _dir_ = new()
    {
        name = nameof(dir)
    };
    public string dir
    {
        get => _dir?.value;
        set => SetValue(_dir_, ref _dir, value);
    }
    #endregion


    #region string id
    PropertyValueNode<string> _id;
    static readonly PropertyValueDefinition _id_ = new()
    {
        name = nameof(id)
    };
    public string id
    {
        get => _id?.value;
        set => SetValue(_id_, ref _id, value);
    }
    #endregion


    #region string lang
    PropertyValueNode<string> _lang;
    static readonly PropertyValueDefinition _lang_ = new()
    {
        name = nameof(lang)
    };
    public string lang
    {
        get => _lang?.value;
        set => SetValue(_lang_, ref _lang, value);
    }
    #endregion


    #region string part
    PropertyValueNode<string> _part;
    static readonly PropertyValueDefinition _part_ = new()
    {
        name = nameof(part)
    };
    public string part
    {
        get => _part?.value;
        set => SetValue(_part_, ref _part, value);
    }
    #endregion


    #region string role
    PropertyValueNode<string> _role;
    static readonly PropertyValueDefinition _role_ = new()
    {
        name = nameof(role)
    };
    public string role
    {
        get => _role?.value;
        set => SetValue(_role_, ref _role, value);
    }
    #endregion


    #region string spellcheck
    PropertyValueNode<string> _spellcheck;
    static readonly PropertyValueDefinition _spellcheck_ = new()
    {
        name = nameof(spellcheck)
    };
    public string spellcheck
    {
        get => _spellcheck?.value;
        set => SetValue(_spellcheck_, ref _spellcheck, value);
    }
    #endregion


    #region string tabIndex
    PropertyValueNode<string> _tabIndex;
    static readonly PropertyValueDefinition _tabIndex_ = new()
    {
        name = nameof(tabIndex)
    };
    public string tabIndex
    {
        get => _tabIndex?.value;
        set => SetValue(_tabIndex_, ref _tabIndex, value);
    }
    #endregion


    #region string title
    PropertyValueNode<string> _title;
    static readonly PropertyValueDefinition _title_ = new()
    {
        name = nameof(title)
    };
    public string title
    {
        get => _title?.value;
        set => SetValue(_title_, ref _title, value);
    }
    #endregion


    #region string translate
    PropertyValueNode<string> _translate;
    static readonly PropertyValueDefinition _translate_ = new()
    {
        name = nameof(translate)
    };
    public string translate
    {
        get => _translate?.value;
        set => SetValue(_translate_, ref _translate, value);
    }
    #endregion


    #region string onClick
    PropertyValueNode<MouseEventHandler> _onClick;
    static readonly PropertyValueDefinition _onClick_ = new()
    {
        name = nameof(onClick),
        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments",
        isIsVoidTaskDelegate = true
    };
    public MouseEventHandler onClick
    {
        get => _onClick?.value;
        set => SetValue(_onClick_, ref _onClick, value);
    }
    #endregion


    #region string onMouseEnter
    PropertyValueNode<MouseEventHandler> _onMouseEnter;
    static readonly PropertyValueDefinition _onMouseEnter_ = new()
    {
        name = nameof(onMouseEnter),
        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments",
        isIsVoidTaskDelegate = true
    };
    public MouseEventHandler onMouseEnter
    {
        get => _onMouseEnter?.value;
        set => SetValue(_onMouseEnter_, ref _onMouseEnter, value);
    }
    #endregion


    #region string onMouseLeave
    PropertyValueNode<MouseEventHandler> _onMouseLeave;
    static readonly PropertyValueDefinition _onMouseLeave_ = new()
    {
        name = nameof(onMouseLeave),
        GrabEventArgumentsByUsingFunction = "ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments",
        isIsVoidTaskDelegate = true
    };
    public MouseEventHandler onMouseLeave
    {
        get => _onMouseLeave?.value;
        set => SetValue(_onMouseLeave_, ref _onMouseLeave, value);
    }
    #endregion


    #region string onScroll
    PropertyValueNode<ScrollEventHandler> _onScroll;
    static readonly PropertyValueDefinition _onScroll_ = new()
    {
        name = nameof(onScroll),
        isIsVoidTaskDelegate = true,
        isScrollEvent = true
    };
    public ScrollEventHandler onScroll
    {
        get => _onScroll?.value;
        set => SetValue(_onScroll_, ref _onScroll, value);
    }
    #endregion


    #region string onKeyDown
    PropertyValueNode<KeyboardEventHandler> _onKeyDown;
    static readonly PropertyValueDefinition _onKeyDown_ = new()
    {
        name = nameof(onKeyDown),
        isIsVoidTaskDelegate = true
    };
    public KeyboardEventHandler onKeyDown
    {
        get => _onKeyDown?.value;
        set => SetValue(_onKeyDown_, ref _onKeyDown, value);
    }
    #endregion



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

