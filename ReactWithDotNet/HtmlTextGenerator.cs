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

    static void AddAttribute(HtmlNode htmlNode, HtmlAttribute htmlAttribute)
    {
        (htmlNode.attributes ??= new List<HtmlAttribute>()).Add(htmlAttribute);
    }

    static void AddChild(HtmlNode parent, HtmlNode child)
    {
        var children = parent.children ??= new List<HtmlNode>();

        children.Add(child);
    }

    static void Append(HtmlNode parentHtmlNode, JsonMap jsonMap)
    {
        var htmlNode = AsHtmlNode(jsonMap);

        AddChild(parentHtmlNode, htmlNode);
    }

    static HtmlNode AsHtmlNode(JsonMap jsonMap)
    {
        var htmlNode = new HtmlNode();

        if (jsonMap.head is { key: "$tag", value: "nbsp" })
        {
            htmlNode.isTextNode = true;

            if (jsonMap.tail is { key: nameof(Nbsp.length) })
            {
                htmlNode.text = string.Join(string.Empty, Enumerable.Range(0, (int)jsonMap.tail.value).Select(_ => "&nbsp;"));
                return htmlNode;
            }

            htmlNode.text = "&nbsp;";

            return htmlNode;
        }

        jsonMap.Foreach((k, v) => ProcessJsonMapNode(htmlNode, k, v));

        return htmlNode;
    }

    static HtmlNode AsHtmlTextNode(string text)
    {
        return new HtmlNode { isTextNode = true, text = text };
    }

    static HtmlNode CalculateDynamicStylesAsHtmlStyleNode(JsonMap dynamicStylesMap)
    {
        var sb = new StringBuilder();

        var padding = "".PadLeft(4, ' ');

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

        return new HtmlNode
        {
            tag = "style",

            attributes = new List<HtmlAttribute> { new() { name = "id", value = "ReactWithDotNetDynamicCss" } },

            text = sb.ToString()
        };
    }

    static string CalculateHtml(JsonMap element, JsonMap dynamicStyles)
    {
        var sb = new StringBuilder();

        var wrapperNode = AsHtmlNode(element);

        tryAddDynamicStylesToHeadNode();

        ToString(sb, 0, wrapperNode);

        return sb.ToString().Trim();

        void tryAddDynamicStylesToHeadNode()
        {
            var htmlNode = wrapperNode.children?[0];

            if (htmlNode?.tag == "html")
            {
                var firstChild = htmlNode.children?[0];

                if (firstChild?.tag == "head")
                {
                    AddChild(firstChild, CalculateDynamicStylesAsHtmlStyleNode(dynamicStyles));
                }
            }
        }
    }

    static bool IsEndsWithNewLine(this StringBuilder sb)
    {
        var length = sb.Length;

        return length > 2 && sb[length - 2] == '\r' && sb[length - 1] == '\n';
    }

    static string PascalToKebabCase(string dotnetPropertyName)
    {
        if (dotnetPropertyName.StartsWith("aria-", StringComparison.OrdinalIgnoreCase) ||
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

    static void ProcessJsonMapNode(HtmlNode htmlNode, string name, object value)
    {
        if (name == "key" || name == "DotNetProperties" || name == "onClick" || name == "$DotNetComponentUniqueIdentifier" ||
            name == "$State" || name == "$Type" || name == "$TypeOfState" ||
            name == "$ClientTasks")
        {
            return;
        }

        if (htmlNode.isThirdPartyComponent && name != "SuspenseFallback")
        {
            return;
        }

        if (name == "$isPureComponent")
        {
            htmlNode.isVirtualNode = true;
            return;
        }

        if (name == "$tag")
        {
            if (value is string valueAsString)
            {
                if (valueAsString == "React.Fragment")
                {
                    htmlNode.isReactFragment = true;
                    return;
                }

                if (valueAsString.IndexOf('.', StringComparison.Ordinal) > 0)
                {
                    htmlNode.isThirdPartyComponent = true;
                    return;
                }

                htmlNode.tag = valueAsString;
                return;
            }
        }

        if (name == "style")
        {
            if (value is Style valueAsStyle)
            {
                AddAttribute(htmlNode, new HtmlAttribute { name = "style", value = valueAsStyle.ToCss() });
                return;
            }

            return;
        }

        if (name == "$text")
        {
            htmlNode.text = value.ToString();
            return;
        }

        if (name == nameof(HtmlElement.dangerouslySetInnerHTML))
        {
            htmlNode.text = ((dangerouslySetInnerHTML)value).__html;

            return;
        }

        if (name[0] != '$')
        {
            if (value is string or int or double or bool)
            {
                AddAttribute(htmlNode, new HtmlAttribute { name = PascalToKebabCase(name), value = value.ToString() });
                return;
            }
        }

        if (name == "$children")
        {
            foreach (var child in (IEnumerable)value)
            {
                if (child is null)
                {
                    continue;
                }

                if (child is JsonMap childAsJsonMap)
                {
                    AddChild(htmlNode, AsHtmlNode(childAsJsonMap));
                    continue;
                }

                if (child is string childAsString)
                {
                    AddChild(htmlNode, AsHtmlTextNode(childAsString));
                    continue;
                }

                throw DeveloperException("Invalid child.");
            }

            return;
        }

        if (name == "$RootNode")
        {
            htmlNode.isVirtualNode = true;
        }

        if ((name == "$RootNode" || name == "SuspenseFallback") && value is not null)
        {
            Append(htmlNode, (JsonMap)value);
        }
    }

    static void ToString(StringBuilder sb, int depth, HtmlNode htmlNode)
    {
        int? tagIndex;

        if (htmlNode.isTextNode)
        {
            sb.Append(htmlNode.text);
            return;
        }

        var children = htmlNode.children;

        if ((htmlNode.isThirdPartyComponent || htmlNode.isReactFragment || htmlNode.isVirtualNode) && children?.Count > 0)
        {
            foreach (var child in children)
            {
                var canPushNewLine = !child.isTextNode;

                if (canPushNewLine)
                {
                    TryAddNewLine(sb);
                }

                ToString(sb, depth, child);
            }

            return;
        }

        if (children is null)
        {
            openTag();

            appendAttributes();

            if (htmlNode.text != null)
            {
                sb.Append(">");
                sb.Append(htmlNode.text);
                finishTag();
                return;
            }

            closeTag();

            return;
        }

        openTag();

        appendAttributes();

        sb.Append(">");

        if (htmlNode.text != null)
        {
            sb.Append(htmlNode.text);
        }

        if (children.Count == 1 && children[0].isTextNode)
        {
            ToString(sb, depth, children[0]);
            finishTag();

            return;
        }

        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i];

            var childDepth = depth;

            var canPushNewLine = !(child.isTextNode || (i > 0 && children[i - 1].isTextNode));

            if (canPushNewLine)
            {
                TryAddNewLine(sb);
                childDepth = depth + 1;
            }

            ToString(sb, childDepth, child);
        }

        if (hasNewLineFromTagToEnd())
        {
            TryAddNewLine(sb);
        }

        pushIndent();
        finishTag();

        bool hasNewLineFromTagToEnd()
        {
            if (tagIndex.HasValue)
            {
                var length = sb.Length;

                for (var i = tagIndex.Value; i < length; i++)
                {
                    if (sb[i] == '\n')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void finishTag()
        {
            if (SelfClosingTags.Contains(htmlNode.tag))
            {
                sb.Append("<");
                sb.Append(htmlNode.tag);
                sb.Append(">");
                return;
            }

            sb.Append("</");
            sb.Append(htmlNode.tag);
            sb.Append(">");
        }

        void pushIndent()
        {
            if (sb.IsEndsWithNewLine())
            {
                sb.Append("".PadLeft(depth * 2, ' '));
            }
        }

        void openTag()
        {
            pushIndent();

            if (htmlNode.tag == "html")
            {
                sb.AppendLine("<!DOCTYPE html>");
                pushIndent();
            }

            tagIndex = sb.Length;

            sb.Append("<");
            sb.Append(htmlNode.tag);
        }

        void closeTag()
        {
            if (SelfClosingTags.Contains(htmlNode.tag))
            {
                sb.Append(">");
                return;
            }

            sb.Append(">");
            sb.Append("</");
            sb.Append(htmlNode.tag);
            sb.Append(">");
        }

        void appendAttributes()
        {
            if (htmlNode.attributes is null)
            {
                return;
            }

            foreach (var htmlAttribute in htmlNode.attributes)
            {
                sb.Append(" ");
                sb.Append(htmlAttribute.name);
                sb.Append('=');
                sb.Append('"');
                sb.Append(htmlAttribute.value);
                sb.Append('"');
            }
        }
    }

    static void TryAddNewLine(StringBuilder sb)
    {
        if (sb.IsEndsWithNewLine())
        {
            return;
        }

        sb.AppendLine();
    }

    sealed class HtmlAttribute
    {
        public string name, value;
    }

    sealed class HtmlNode
    {
        public List<HtmlAttribute> attributes;
        public List<HtmlNode> children;
        public bool isReactFragment;
        public bool isTextNode;
        public bool isThirdPartyComponent;
        public bool isVirtualNode;

        public string tag;
        public string text;
    }
}