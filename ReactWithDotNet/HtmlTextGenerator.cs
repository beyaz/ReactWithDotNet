using System.Collections;
using System.Globalization;
using System.Text;

namespace ReactWithDotNet;

static class HtmlTextGenerator
{
    static readonly IReadOnlyList<string> SelfClosingTags = "area,base,br,col,embed,hr,img,input,keygen,link,meta,param,source,track,wbr".Split(',');

    public static string ToHtml(ComponentResponse componentResponse)
    {
        return CalculateHtml((JsonMap)componentResponse.ElementAsJson, (JsonMap)componentResponse.DynamicStyles);
    }

    static void Append(OutputContext context, int indent, JsonMap element)
    {
        if (element == null)
        {
            return;
        }
        
        var sb = context.sb;

        var padding = "".PadLeft(indent, ' ');

        string tag = null;

        var hasInnerContent = false;

        var isThirdPartyReactComponent = false;

        element.Foreach((a, b) =>
        {
            if (isThirdPartyReactComponent && a != "SuspenseFallback")
            {
                return;
            }
            ProcessEntry(a, b, context, indent, ref tag, ref hasInnerContent, ref isThirdPartyReactComponent);
        });

        if (tag == null)
        {
            return;
        }

        if (tag == "head")
        {
            context.HeadTagFinishIndex = sb.Length;
        }

        if (hasInnerContent)
        {
            if (sb[^1] == '\n')
            {
                sb.Append(padding);
            }

            // try to insert dynamically created styles when reached to end of html
            {
                if (tag == "html")
                {
                    var headTagFinishIndex = context.HeadTagFinishIndex;
                    if (headTagFinishIndex.HasValue)
                    {
                        sb.Insert(headTagFinishIndex.Value, CalculateDynamicStylesAsHtmlStyleNode(context.dynamicStyles));
                    }
                }
            }

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

    static string CalculateDynamicStylesAsHtmlStyleNode(JsonMap dynamicStylesMap)
    {
        var sb = new StringBuilder();

        var padding = "".PadLeft(4, ' ');

        sb.AppendLine(padding + "<style id='ReactWithDotNetDynamicCss'>");
        dynamicStylesMap?.Foreach((cssSelector, cssBody) =>
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

    static string CalculateHtml(JsonMap element, JsonMap dynamicStyles)
    {
        var sb = new StringBuilder();

        Append(new OutputContext { sb = sb, dynamicStyles = dynamicStyles }, 0, element);

        return sb.ToString();
    }

    static string PascalToKebabCase(string dotnetPropertyName)
    {
        if (dotnetPropertyName.StartsWith("aria-",StringComparison.OrdinalIgnoreCase) ||
            dotnetPropertyName.StartsWith("data-", StringComparison.OrdinalIgnoreCase))
        {
            return dotnetPropertyName;
        }
        
        if (dotnetPropertyName == "className")
        {
            return "class";
        }

        if (dotnetPropertyName == "htmlFor")
        {
            return "for";
        }

        if (dotnetPropertyName == "cssFloat")
        {
            return "float";
        }

        if (dotnetPropertyName == "viewBox")
        {
            return "viewBox";
        }

        static IEnumerable<char> convertChar(char c, int index)
        {
            if (char.IsUpper(c) && index != 0) yield return '-';
            yield return char.ToLower(c, new CultureInfo("EN-us"));
        }

        return string.Concat(dotnetPropertyName.SelectMany(convertChar));
    }

    static void ProcessEntry(string name, object value, OutputContext context, int indent, ref string tag, ref bool hasInnerContent, ref bool isThirdPartyReactComponent)
    {
        if (name == "key" || name == "DotNetProperties" || name == "onClick" || name == "$DotNetComponentUniqueIdentifier" ||
            name == "$State" ||
            name == "$Type" ||
            name == "$TypeOfState")
        {
            return;
        }

        var sb = context.sb;

        var padding = "".PadLeft(indent, ' ');

        if (name == "$tag")
        {
            if (value is string valueAsString)
            {
                if (valueAsString == "html")
                {
                    sb.AppendLine("<!DOCTYPE html>");
                }

                if (valueAsString.IndexOf('.',StringComparison.Ordinal) > 0)
                {
                    isThirdPartyReactComponent = true;
                    return;
                }
                
                tag = valueAsString;
                sb.Append(padding);
                sb.Append("<");
                sb.Append(tag);

                return;
            }

            return;
        }

        if (name == "style")
        {
            if (value is Style valueAsStyle)
            {
                sb.Append(" style=\"");
                sb.Append(valueAsStyle.ToCss());
                sb.Append('"');

                return;
            }

            return;
        }

        if (name == nameof(HtmlElement.dangerouslySetInnerHTML))
        {
            sb.Append(">");
            hasInnerContent = true;
            sb.Append(((dangerouslySetInnerHTML)value).__html);
            return;
        }
        
        if (name[0] != '$')
        {
            if (value is string or int or double or bool)
            {
                sb.Append(" ");
                sb.Append(PascalToKebabCase(name));
                sb.Append("=");
                sb.Append('"');
                sb.Append(value);
                sb.Append('"');

                return;
            }
        }

        if (name == "$text")
        {
            sb.Append(">");
            hasInnerContent = true;

            sb.Append(value);
        }

        if (name == "$children")
        {
            sb.Append(">");
            hasInnerContent = true;

            var needNewLine = false;

            foreach (var child in (IEnumerable)value)
            {
                if (child is null)
                {
                    continue;
                }

                if (child is JsonMap childAsJsonMap)
                {
                    needNewLine = true;
                    sb.AppendLine();
                    Append(context, indent + 2, childAsJsonMap);
                    continue;
                }

                if (child is string childAsString)
                {
                    sb.Append(childAsString);
                    continue;
                }

                throw DeveloperException("Invalid child.");
            }

            if (needNewLine)
            {
                sb.AppendLine();
            }
        }

        const string RootNode = "$RootNode";

        if (name == RootNode)
        {
            Append(context, sb.Length == 0 ? indent : indent + 2, (JsonMap)value);
        }

        if (name == "SuspenseFallback")
        {
            Append(context, indent, (JsonMap)value);
        }
    }

    sealed class OutputContext
    {
        public JsonMap dynamicStyles;
        public int? HeadTagFinishIndex;
        public StringBuilder sb;
    }
}