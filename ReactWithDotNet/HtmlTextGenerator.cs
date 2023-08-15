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
        (htmlNode.Attributes ??= new List<HtmlAttribute>()).Add(htmlAttribute);
    }

    static void AddChild(HtmlNode parent, HtmlNode child)
    {
        var children = parent.Children ??= new List<HtmlNode>();

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

        if (jsonMap.Head is { Key: "$tag", Value: "nbsp" })
        {
            htmlNode.IsTextNode = true;

            if (jsonMap.Tail is { Key: nameof(Nbsp.length) })
            {
                htmlNode.Text = string.Join(string.Empty, Enumerable.Range(0, (int)jsonMap.Tail.Value).Select(_ => "&nbsp;"));
                return htmlNode;
            }

            htmlNode.Text = "&nbsp;";

            return htmlNode;
        }

        jsonMap.Foreach((k, v) => ProcessJsonMapNode(htmlNode, k, v));

        return htmlNode;
    }

    static HtmlNode AsHtmlTextNode(string text)
    {
        return new HtmlNode { IsTextNode = true, Text = text };
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
            Tag = "style",

            Attributes = new List<HtmlAttribute> { new() { Name = "id", Value = "ReactWithDotNetDynamicCss" } },

            Text = sb.ToString()
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
            var htmlNode = wrapperNode.Children?[0];

            if (htmlNode?.Tag == "html")
            {
                var firstChild = htmlNode.Children?[0];

                if (firstChild?.Tag == "head")
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

        if (htmlNode.IsThirdPartyComponent && name != "SuspenseFallback")
        {
            return;
        }

        if (name == "$isPureComponent")
        {
            htmlNode.IsVirtualNode = true;
            return;
        }

        if (name == "$tag")
        {
            if (value is string valueAsString)
            {
                if (valueAsString == "React.Fragment")
                {
                    htmlNode.IsReactFragment = true;
                    return;
                }

                if (valueAsString.IndexOf('.', StringComparison.Ordinal) > 0)
                {
                    htmlNode.IsThirdPartyComponent = true;
                    return;
                }

                htmlNode.Tag = valueAsString;
                return;
            }
        }

        if (name == "style")
        {
            if (value is Style valueAsStyle)
            {
                AddAttribute(htmlNode, new HtmlAttribute { Name = "style", Value = valueAsStyle.ToCss() });
                return;
            }

            return;
        }

        if (name == "$text")
        {
            htmlNode.Text = value.ToString();
            return;
        }

        if (name == nameof(HtmlElement.dangerouslySetInnerHTML))
        {
            htmlNode.Text = ((dangerouslySetInnerHTML)value).__html;

            return;
        }

        if (name[0] != '$')
        {
            if (value is string or int or double or bool)
            {
                AddAttribute(htmlNode, new HtmlAttribute { Name = PascalToKebabCase(name), Value = value.ToString() });
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
            htmlNode.IsVirtualNode = true;
        }

        if ((name == "$RootNode" || name == "SuspenseFallback") && value is not null)
        {
            Append(htmlNode, (JsonMap)value);
        }
    }

    static void ToString(StringBuilder sb, int depth, HtmlNode htmlNode)
    {
        int? tagIndex;

        if (htmlNode.IsTextNode)
        {
            sb.Append(htmlNode.Text);
            return;
        }

        var children = htmlNode.Children;

        if ((htmlNode.IsThirdPartyComponent || htmlNode.IsReactFragment || htmlNode.IsVirtualNode))
        {
            if (children?.Count > 0)
            {
                foreach (var child in children)
                {
                    var canPushNewLine = !child.IsTextNode;

                    if (canPushNewLine)
                    {
                        TryAddNewLine(sb);
                    }

                    ToString(sb, depth, child);
                }    
            }

            return;
        }

        if (children is null)
        {
            openTag();

            appendAttributes();

            if (htmlNode.Text != null)
            {
                sb.Append(">");
                sb.Append(htmlNode.Text);
                finishTag();
                return;
            }

            closeTag();

            return;
        }

        openTag();

        appendAttributes();

        sb.Append(">");

        if (htmlNode.Text != null)
        {
            sb.Append(htmlNode.Text);
        }

        if (children.Count == 1 && children[0].IsTextNode)
        {
            ToString(sb, depth, children[0]);
            finishTag();

            return;
        }

        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i];

            var childDepth = depth;

            var canPushNewLine = !(child.IsTextNode || (i > 0 && children[i - 1].IsTextNode));

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
            if (SelfClosingTags.Contains(htmlNode.Tag))
            {
                sb.Append("<");
                sb.Append(htmlNode.Tag);
                sb.Append(">");
                return;
            }

            sb.Append("</");
            sb.Append(htmlNode.Tag);
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

            if (htmlNode.Tag == "html")
            {
                sb.AppendLine("<!DOCTYPE html>");
                pushIndent();
            }

            tagIndex = sb.Length;

            sb.Append("<");
            sb.Append(htmlNode.Tag);
        }

        void closeTag()
        {
            if (SelfClosingTags.Contains(htmlNode.Tag))
            {
                sb.Append(">");
                return;
            }

            sb.Append(">");
            sb.Append("</");
            sb.Append(htmlNode.Tag);
            sb.Append(">");
        }

        void appendAttributes()
        {
            if (htmlNode.Attributes is null)
            {
                return;
            }

            foreach (var htmlAttribute in htmlNode.Attributes)
            {
                sb.Append(" ");
                sb.Append(htmlAttribute.Name);
                sb.Append('=');
                sb.Append('"');
                sb.Append(htmlAttribute.Value);
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
        public string Name, Value;
    }

    sealed class HtmlNode
    {
        public List<HtmlAttribute> Attributes;
        public List<HtmlNode> Children;
        public bool IsReactFragment;
        public bool IsTextNode;
        public bool IsThirdPartyComponent;
        public bool IsVirtualNode;

        public string Tag;
        public string Text;
    }
}