using System;
using System.IO;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public class aside : HtmlElement
{
    public aside() { }
    public aside(params HtmlElementModifier[] modifiers) : base(modifiers) { }
}
public class section : HtmlElement
{
    public section() { }
    public section(params HtmlElementModifier[] modifiers) : base(modifiers) { }
}
public class small : HtmlElement
{
    public small() { }
    public small(params HtmlElementModifier[] modifiers) : base(modifiers) { }
}

public class ul : HtmlElement
{
    public ul() { }
    public ul(params HtmlElementModifier[] modifiers) : base(modifiers) { }
}
public class br : HtmlElement
{
}
public class article : HtmlElement
{
    public article() { }
    public article(params HtmlElementModifier[] modifiers) : base(modifiers) { }
}

public class fieldset : HtmlElement
{
    public fieldset() { }
    public fieldset(params HtmlElementModifier[] modifiers) : base(modifiers) { }
}

public class legend : HtmlElement
{
    public legend() { }
    public legend(params HtmlElementModifier[] modifiers) : base(modifiers) { }
}


public class iframe : HtmlElement
{
    [React]
    public string src { get; set; }
}
public class strong : HtmlElement
{
    public strong() { }
    public strong(params HtmlElementModifier[] modifiers) : base(modifiers) { }

    public override string ToString() => this.ToHTML();
}

public class span : HtmlElement
{
    public span() { }
    public span(params HtmlElementModifier[] modifiers) : base(modifiers) { }

   
    public span(string innerText)
    {
        this.innerText = innerText;
    }

    public override string ToString() => this.ToHTML();
}





static class HtmlTextWriter
{
    public static string ToHTML(this HtmlElement element)
    {
        var css = element.style.ToCss();
        if (css != null)
        {
            css = " style = " + '"' + css + '"';
        }

        var tag = element.Type;

        if (element is span || element is strong)
        {
            return $"<{tag}{css}>{element.innerText}</{tag}>";
        }

        return "to html failed";
    }
}