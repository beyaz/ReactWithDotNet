using System.Text;

namespace ReactWithDotNet;

static class HtmlTextGenerator
{
    public static string ToHtml(Element element)
    {
        var sb = new StringBuilder();
        
        Append(sb,0, element);

        return sb.ToString();
    }

    static void Append(StringBuilder sb, int indent, Element element)
    {
        void push(string value)
        {
            sb.Append("".PadLeft(indent, ' '));
            sb.Append(value);
        }
        
        if (element is HtmlElement htmlElement)
        {
            var tag = htmlElement.Type;
            
            push("<");
            push(tag);
            

            if (htmlElement._style is not null)
            {
                var css = htmlElement._style.ToCss();
                if (!string.IsNullOrWhiteSpace(css))
                {
                    push(" style=\"");
                    push(css);
                    push("\"");
                }
            }
            push(">");

            if (!string.IsNullOrWhiteSpace(htmlElement.innerText))
            {
                sb.AppendLine();
                push(htmlElement.innerText);
            }
            
            if (htmlElement._children is not null)
            {
                foreach (var child in htmlElement.children)
                {
                    sb.AppendLine();
                    Append(sb,indent+2,child);
                }
            }

            push("</");
            push(tag);
            push(">");
            
            return;
        }
        
    }
}