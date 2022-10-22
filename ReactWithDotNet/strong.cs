namespace ReactWithDotNet;

public class aside : HtmlElement
{
    public aside()
    {
    }

    public aside(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public class section : HtmlElement
{
    public section()
    {
    }

    public section(params IModifier[] modifiers) : base(modifiers)
    {
    }
}




public class br : HtmlElement
{
}

public class fieldset : HtmlElement
{
    public fieldset()
    {
    }

    public fieldset(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public class legend : HtmlElement
{
    public legend()
    {
    }

    public legend(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public class iframe : HtmlElement
{
    [React]
    public string src { get; set; }
}

/// <summary>
/// Important text
/// </summary>
public class strong : HtmlElement
{
    /// <summary>
    /// Important text
    /// </summary>
    public strong()
    {
    }

    /// <summary>
    /// Important text
    /// </summary>
    public strong(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    /// Important text
    /// </summary>
    public static implicit operator strong(string text)
    {
        return new strong { text = text };
    }
}

/// <summary>
/// Italic text
/// </summary>
public class i : HtmlElement
{
    /// <summary>
    /// Italic text
    /// </summary>
    public i() { }

    /// <summary>
    /// Italic text
    /// </summary>
    public i(params IModifier[] modifiers) : base(modifiers) { }

    /// <summary>
    /// Italic text
    /// </summary>
    public static implicit operator i(string text)
    {
        return new i { text = text };
    }
}


/// <summary>
/// Bold text
/// </summary>
public class b : HtmlElement
{
    /// <summary>
    /// Bold text
    /// </summary>
    public b()
    {
    }
    /// <summary>
    /// Bold text
    /// </summary>
    public b(params IModifier[] modifiers) : base(modifiers)
    {
    }
    /// <summary>
    /// Bold text
    /// </summary>
    public static implicit operator b(string text)
    {
        return new b { text = text };
    }
}


/// <summary>
/// Emphasized text
/// </summary>
public class em : HtmlElement
{
    /// <summary>
    /// Emphasized text
    /// </summary>
    public em()
    {
    }

    /// <summary>
    /// Emphasized text
    /// </summary>
    public em(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    /// Emphasized text
    /// </summary>
    public static implicit operator em(string text)
    {
        return new em { text = text };
    }
}

/// <summary>
/// Marked text
/// </summary>
public class mark : HtmlElement
{
    /// <summary>
    /// Marked text
    /// </summary>
    public mark()
    {
    }

    /// <summary>
    /// Marked text
    /// </summary>
    public mark(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    /// Marked text
    /// </summary>
    public static implicit operator mark(string text)
    {
        return new mark { text = text };
    }
}


/// <summary>
/// Smaller text
/// </summary>
public class small : HtmlElement
{
    /// <summary>
    /// Smaller text
    /// </summary>
    public small()
    {
    }

    /// <summary>
    /// Smaller text
    /// </summary>
    public small(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    /// Smaller text
    /// </summary>
    public static implicit operator small(string text)
    {
        return new small { text = text };
    }
}

/// <summary>
/// Deleted text
/// </summary>
public class del : HtmlElement
{
    /// <summary>
    /// Deleted text
    /// </summary>
    public del()
    {
    }

    /// <summary>
    /// Deleted text
    /// </summary>
    public del(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    /// Deleted text
    /// </summary>
    public static implicit operator del(string text)
    {
        return new del { text = text };
    }
}

/// <summary>
///Inserted text
/// </summary>
public class ins : HtmlElement
{
    /// <summary>
    /// Inserted text
    /// </summary>
    public ins()
    {
    }

    /// <summary>
    /// Inserted text
    /// </summary>
    public ins(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    /// Inserted text
    /// </summary>
    public static implicit operator ins(string text)
    {
        return new ins { text = text };
    }
}

/// <summary>
///Subscript text
/// </summary>
public class sub : HtmlElement
{
    /// <summary>
    ///Subscript text
    /// </summary>
    public sub()
    {
    }

    /// <summary>
    ///Subscript text
    /// </summary>
    public sub(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    ///Subscript text
    /// </summary>
    public static implicit operator sub(string text)
    {
        return new sub { text = text };
    }
}

/// <summary>
///Superscript text
/// </summary>
public class sup : HtmlElement
{
    /// <summary>
    ///Superscript text
    /// </summary>
    public sup()
    {
    }

    /// <summary>
    ///Superscript text
    /// </summary>
    public sup(params IModifier[] modifiers) : base(modifiers)
    {
    }
    
    /// <summary>
    ///Superscript text
    /// </summary>
    public static implicit operator sup(string text)
    {
        return new sup { text = text };
    }
}


/// <summary>
/// Inline container used to mark up a part of a text, or a part of a document.
/// </summary>
public class span : HtmlElement
{
    /// <summary>
    /// Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span()
    {
    }

    /// <summary>
    /// Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public span(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    /// Inline container used to mark up a part of a text, or a part of a document.
    /// </summary>
    public static implicit operator span(string text)
    {
        return new span { text = text };
    }
}

/// <summary>
/// Specifies independent, self-contained content.
/// </summary>
public class article : HtmlElement
{
    /// <summary>
    /// Specifies independent, self-contained content.
    /// </summary>
    public article()
    {
    }

    /// <summary>
    /// Specifies independent, self-contained content.
    /// </summary>
    public article(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    /// Specifies independent, self-contained content.
    /// </summary>
    public static implicit operator article(string text)
    {
        return new article { text = text };
    }
}
public class ul : HtmlElement
{
    public ul()
    {
    }

    public ul(params IModifier[] modifiers) : base(modifiers)
    {
    }
}



public class select : HtmlElement
{
    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = nameof(onChange))]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public Action<ChangeEvent> onChange { get; set; }

    public select()
    {
    }

    public select(params IModifier[] modifiers) : base(modifiers)
    {
    }
}
public class option : HtmlElement
{
    [React]
    public bool? selected { get; set; }

    [React]
    public string value { get; set; }
    
    public option()
    {
    }

    public option(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public class style : HtmlElement
{
    
}
