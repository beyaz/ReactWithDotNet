using System;
using System.Text.Json.Serialization;

namespace ReactDotNet.Html5;

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
        if (element is span || element is strong)
        {
            var css = element.style.ToCss();
            if (css != null)
            {
                css = " style = " + '"' + css + '"';
            }

            var tag = element.Type;
            
            return $"<{tag}{css}>{element.innerText}</{tag}>";
        }

        throw new NotImplementedException(element.Type);

    }
}

