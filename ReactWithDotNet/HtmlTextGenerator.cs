using System.Text;
using static ReactWithDotNet.ElementSerializer;

namespace ReactWithDotNet;

static class HtmlTextGenerator
{
    public static string ToHtml(Element element, ReactContext reactContext = null)
    {
        var sb = new StringBuilder();

        Append(sb, 0, element, reactContext ?? new ReactContext());

        return sb.ToString();
    }

    static void Append(StringBuilder sb, int indent, Element element, ReactContext reactContext)
    {
        if (element is HtmlElement htmlElement)
        {
            var tag = htmlElement.Type;

            if (element is HtmlTextNode textNode)
            {
                sb.Append(textNode.innerText);
                
                return;
            }
            
            var padding = "".PadLeft(indent, ' ');

            if (htmlElement is html)
            {
                sb.AppendLine("<!DOCTYPE html>");
            }
            
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

            GetReactAttributedPropertiesOfType(element.GetType())
               .Foreach(propertyAccessInfo =>
                {
                    var propertyValue = propertyAccessInfo.GetValueFunc(element);
                    if (propertyValue != propertyAccessInfo.DefaultValue)
                    {
                        if (propertyValue is string)
                        {
                            sb.Append($" {getHtmlAttributeName(propertyAccessInfo.PropertyInfo.Name)}=\"");
                            sb.Append(propertyValue);
                            sb.Append("\"");
                        }
                    }
                });

            if (htmlElement._children?.Count == 1 && htmlElement._children[0] is HtmlTextNode htmlTextNode)
            {
                sb.Append(">");

                sb.Append(htmlTextNode.innerText);

                sb.Append("</");
                sb.Append(tag);
                sb.Append(">");

                return;
            }

            if (!string.IsNullOrWhiteSpace(htmlElement.dangerouslySetInnerHTML?.__html))
            {
                sb.Append(">");

                sb.Append(htmlElement.dangerouslySetInnerHTML?.__html);

                sb.Append("</");
                sb.Append(tag);
                sb.Append(">");

                return;
            }

            if (!string.IsNullOrWhiteSpace(htmlElement.innerText) && (htmlElement._children == null || htmlElement._children.Count == 0))
            {
                sb.Append(">");

                sb.Append(htmlElement.innerText);

                sb.Append("</");
                sb.Append(tag);
                sb.Append(">");

                return;
            }

            var hasInnerContent = false;

            if (!string.IsNullOrWhiteSpace(htmlElement.innerText))
            {
                sb.Append(">");
                hasInnerContent = true;

                sb.Append(htmlElement.innerText);
            }

            if (htmlElement._children?.Count > 0)
            {
                sb.Append(">");
                hasInnerContent = true;

                foreach (var child in htmlElement.children)
                {
                    sb.AppendLine();
                    Append(sb, indent + 2, child, reactContext);
                }

                sb.AppendLine();
            }

            if (hasInnerContent)
            {
                sb.Append(padding);
                sb.Append("</");
                sb.Append(tag);
                sb.Append(">");
            }
            else
            {
                sb.Append(" />");
            }
        }
        else if (element is ReactStatefulComponent reactComponent)
        {
            reactComponent.Context = reactContext;

            reactComponent.InvokeConstructor();

            var root = reactComponent.InvokeRender();
            Append(sb, indent + 2, root, reactContext);
        }

        static string getHtmlAttributeName(string dotnetPropertyName)
        {
            if (dotnetPropertyName == "httpEquiv")
            {
                return "http-equiv";
            }

            return dotnetPropertyName;
        }
        
    }
    
    
}