using System;
using System.IO;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public class strong : HtmlElement
{
    public override string ToString() => this.ToHTML();
}

public class span : HtmlElement
{
    public span()
    {
    }
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