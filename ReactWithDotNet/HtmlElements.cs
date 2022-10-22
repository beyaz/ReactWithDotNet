namespace ReactWithDotNet;

public sealed class div : HtmlElement
{
    public div()
    {
    }

    public div(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string role { get; set; }
}

public class button : HtmlElement
{
    [React]
    public string type { get; set; }
}

public class label : HtmlElement
{
    [React]
    public string htmlFor { get; set; }
}

public class input : HtmlElement
{
    [React]
    public string type { get; set; }

    [React]
    public string name { get; set; }

    [React]
    public string value { get; set; }

    [React]
    public string defaultValue { get; set; }

    [React]
    public string placeholder { get; set; }
    

    [React]
    public bool? readOnly { get; set; }
    

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    public bool? @checked { get; set; }

    [React]
    public bool? defaultChecked { get; set; }

    [React]
    public string autocomplete { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.target.checked", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }
}


/// <summary>
/// Defines a paragraph
/// </summary>
public class p : HtmlElement
{
    /// <summary>
    /// Defines a paragraph
    /// </summary>
    public p()
    {
    }

    /// <summary>
    /// Defines a paragraph
    /// </summary>
    public p(string text)
    {
        this.text = text;
    }
    
    /// <summary>
    /// Defines a paragraph
    /// </summary>
    public static implicit operator p(string text)
    {
        return new p { text = text };
    }

    /// <summary>
    /// Defines a paragraph
    /// </summary>
    public p(params IModifier[] modifiers) : base(modifiers) { }
}
public class pre : HtmlElement
{
}

public class h6 : HtmlElement
{
    public h6()
    {

    }
    public h6(params IModifier[] modifiers) : base(modifiers) { }
    public h6(Style style) : base(style) { }
}
public class h5 : HtmlElement
{
    public h5()
    {
        
    }
    public h5(params IModifier[] modifiers) : base(modifiers) { }
    public h5(Style style) : base(style) { }
}

public class h4 : HtmlElement
{
    public h4()
    {
    }

    public h4(string innerText)
    {
        this.innerText = innerText;
    }
    public h4(params IModifier[] modifiers) : base(modifiers) { }
    public h4(Style style) : base(style) { }
}

public class h3 : HtmlElement
{
    public h3()
    {
    }

    public h3(string innerText)
    {
        this.innerText = innerText;
    }
    public h3(params IModifier[] modifiers) : base(modifiers) { }
    public h3(Style style) : base(style) { }
}

public class h2 : HtmlElement
{


    public h2()
    {
    }

    public h2(string innerText)
    {
        this.innerText = innerText;
    }

    public h2(params IModifier[] modifiers) : base(modifiers) { }
}

public class h1 : HtmlElement
{
    public h1()
    {
    }

    public h1(string innerText)
    {
        this.innerText = innerText;
    }

    public h1(params IModifier[] modifiers) : base(modifiers) { }
}
public class header : HtmlElement
{
    public header()
    {
    }

    public header(string innerText)
    {
        this.innerText = innerText;
    }

    public header(params IModifier[] modifiers):base(modifiers) { }
}

public class a : HtmlElement
{
    [React]
    public string href { get; set; }
    
    [React]
    public string target { get; set; }
    
    public a() { }
    public a(params IModifier[] modifiers) : base(modifiers) { }

}

public class img : HtmlElement
{
    [React]
    public string src { get; set; }

    [React]
    public string alt { get; set; }

    [React]
    public int width { get; set; }

    [React]
    public int height { get; set; }

    [React]
    public string loading { get; set; }

    public img() { }
    public img(params IModifier[] modifiers) : base(modifiers) { }
}




public class nav : HtmlElement
{
    public nav() { }
    public nav(params IModifier[] modifiers) : base(modifiers) { }
}

public class main : HtmlElement
{
    public main() { }
    public main(params IModifier[] modifiers) : base(modifiers) { }
}
public class footer : HtmlElement
{
    public footer() { }
    public footer(params IModifier[] modifiers) : base(modifiers) { }
}



public class HtmlTextNode : HtmlElement
{
    
}