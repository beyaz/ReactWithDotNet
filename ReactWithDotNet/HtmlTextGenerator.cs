using System.Text;
using static ReactWithDotNet.ElementSerializer;

namespace ReactWithDotNet;

static class HtmlTextGenerator
{
    static readonly ReactContextKey<DynamicStyleContentForEmbeddInClient> DynamicStyles = new(nameof(DynamicStyles));
    static readonly ReactContextKey<int?> HeadTagFinishIndex = new(nameof(HeadTagFinishIndex));

    static readonly IReadOnlyList<string> SelfClosingTags = "area,base,br,col,embed,hr,img,input,keygen,link,meta,param,source,track,wbr".Split(',');

    public static string ToHtml(Element element, ReactContext reactContext = null)
    {
        var sb = new StringBuilder();

        reactContext ??= new ReactContext();

        reactContext.Set(DynamicStyles, new DynamicStyleContentForEmbeddInClient());

        Append(sb, 0, element, reactContext);

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

                var pseudos = CalculatePseudos(htmlElement._style);
                if (pseudos?.Count > 0 || htmlElement._style._mediaQueries?.Count > 0)
                {
                    var className = reactContext.TryGetValue(DynamicStyles).GetClassName(new CssClassInfo
                    {
                        Name         = "rwd",
                        Pseudos      = pseudos,
                        MediaQueries = htmlElement._style._mediaQueries?.Select(pair => (pair.query, pair.style.ToCssWithImportant())).ToList()
                    });

                    htmlElement.AddClass(className);
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

            if (htmlElement._aria is not null)
            {
                foreach (var (name, value) in htmlElement._aria)
                {
                    sb.Append($" aria-{name}=\"");
                    sb.Append(value);
                    sb.Append("\"");
                }
            }

            if (htmlElement._data is not null)
            {
                foreach (var (name, value) in htmlElement._data)
                {
                    sb.Append($" data-{name}=\"");
                    sb.Append(value);
                    sb.Append("\"");
                }
            }

            if (htmlElement._children?.Count == 1 && htmlElement._children[0] is HtmlTextNode htmlTextNode)
            {
                sb.Append(">");

                sb.Append(htmlTextNode.innerText);

                sb.Append("<");
                if (!SelfClosingTags.Contains(tag))
                {
                    sb.Append("/");
                }

                sb.Append(tag);
                sb.Append(">");

                return;
            }

            if (!string.IsNullOrWhiteSpace(htmlElement.dangerouslySetInnerHTML?.__html))
            {
                sb.Append(">");

                sb.Append(htmlElement.dangerouslySetInnerHTML?.__html);

                sb.Append("<");
                if (!SelfClosingTags.Contains(tag))
                {
                    sb.Append("/");
                }

                sb.Append(tag);
                sb.Append(">");

                return;
            }

            if (!string.IsNullOrWhiteSpace(htmlElement.innerText) && (htmlElement._children == null || htmlElement._children.Count == 0))
            {
                sb.Append(">");

                sb.Append(htmlElement.innerText);

                sb.Append("<");
                if (!SelfClosingTags.Contains(tag))
                {
                    sb.Append("/");
                }

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

            if (!hasInnerContent && element is script)
            {
                sb.Append("></");
                sb.Append(tag);
                sb.Append(">");
                return;
            }

            if (tag == "head")
            {
                reactContext.Set(HeadTagFinishIndex, sb.Length);
            }

            if (hasInnerContent)
            {
                // try to insert dynamically created styles when reached to end of html
                {
                    if (tag == "html")
                    {
                        var headTagFinishIndex = reactContext.TryGetValue(HeadTagFinishIndex);
                        if (headTagFinishIndex.HasValue)
                        {
                            sb.Insert(headTagFinishIndex.Value, CalculateDynamicStylesAsHtmlStyleNode(reactContext));
                        }
                    }
                }

                sb.Append(padding);
                sb.Append("<");
                if (!SelfClosingTags.Contains(tag))
                {
                    sb.Append("/");
                }

                sb.Append(tag);
                sb.Append(">");
            }
            else
            {
                if (SelfClosingTags.Contains(tag))
                {
                    sb.Append(">");
                }
                else
                {
                    sb.Append("></");
                    sb.Append(tag);
                    sb.Append(">");
                }
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

            if (dotnetPropertyName == "className")
            {
                return "class";
            }

            return dotnetPropertyName;
        }

        static string CalculateDynamicStylesAsHtmlStyleNode(ReactContext reactContext)
        {
            var sb = new StringBuilder();

            var padding = "".PadLeft(4,' ');
            
            sb.AppendLine(padding+"<style>");
            reactContext.TryGetValue(DynamicStyles).CalculateCssClassList().Foreach((cssSelector, cssBody) =>
            {
                sb.Append(padding + cssSelector);
                sb.AppendLine(padding + "{");
                sb.Append(padding + "    ");
                sb.Append(cssBody);
                if (cssSelector.IndexOf("@media ", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    sb.AppendLine();
                    sb.AppendLine(padding + "}");
                }

                sb.AppendLine();
                sb.AppendLine(padding + "}");
            });
            sb.AppendLine(padding + "</style>");

            return sb.ToString();
        }
    }
}