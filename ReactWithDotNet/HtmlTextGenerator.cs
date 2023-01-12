using System.Text;

namespace ReactWithDotNet;

static class HtmlTextGenerator
{
    public static string ToHtml(Element element)
    {
        var sb = new StringBuilder();

        Append(sb, 0, element);

        return sb.ToString();
    }

    static void Append(StringBuilder sb, int indent, Element element)
    {
        
        if (element is HtmlElement htmlElement)
        {
            
            var tag = htmlElement.Type;

            var padding = "".PadLeft(indent, ' ');
            
            sb.Append(padding); 
            sb.Append("<");
            sb.Append(tag);

            if (htmlElement._style is not null)
            {
                var css = htmlElement._style.ToCss();
                if (!string.IsNullOrWhiteSpace(css))
                {
                    sb.Append(" style=\"");
                    sb.Append(css);
                    sb.Append("\"");
                }
            }

            var reactAttributedPropertiesOfType = ElementSerializer.GetReactAttributedPropertiesOfType(element.GetType());
            foreach (var propertyAccessInfo in reactAttributedPropertiesOfType)
            {
                var propertyValue = propertyAccessInfo.GetValueFunc(element);
                if (propertyValue == propertyAccessInfo.DefaultValue)
                {
                    continue;
                }

                if (propertyValue is string)
                {
                    sb.Append($" {propertyAccessInfo.PropertyInfo.Name}=\"");
                    sb.Append(propertyValue);
                    sb.Append("\"");
                }
            }

            sb.Append(">");

            if (!string.IsNullOrWhiteSpace(htmlElement.innerText))
            {
                sb.Append(htmlElement.innerText);
            }

            if (!string.IsNullOrWhiteSpace(htmlElement.dangerouslySetInnerHTML?.__html))
            {
                sb.Append(htmlElement.dangerouslySetInnerHTML?.__html);
            }

            if (htmlElement._children?.Count > 0)
            {
                foreach (var child in htmlElement.children)
                {
                    sb.AppendLine();
                    Append(sb, indent + 2, child);
                }

                sb.AppendLine();
            }

            sb.Append(padding);
            sb.Append("</");
            sb.Append(tag);
            sb.Append(">");
        }
        else if (element is ReactStatefulComponent reactComponent)
        {
            var root = reactComponent.InvokeRender();
            Append(sb, indent + 2, root);
        }
    }
}