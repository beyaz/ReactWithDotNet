using System.Collections;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;

namespace ReactWithDotNet;

static class HtmlTextGenerator
{
    static readonly string[] SelfClosingTags =
    [
        "area", "base", "br", "col", "embed", "hr", "img", "input", "keygen", "link", "meta", "param", "source", "track", "wbr"
    ];

    static readonly string[] SkipThisProperties =
    [
        "key",
        "DotNetProperties",
        "onClick",
        "$DotNetComponentUniqueIdentifier",
        "$State",
        "$Type",
        "$ClientTasks"
    ];

    static readonly Type TypeOfBoolean = typeof(bool);
    static readonly Type TypeOfDouble = typeof(double);
    static readonly Type TypeOfInt16 = typeof(short);
    static readonly Type TypeOfInt32 = typeof(int);

    static readonly Type TypeOfString = typeof(string);
    static readonly Type TypeOfUnionProp = typeof(UnionProp<,>);

    public static StringBuilder ToHtml(ComponentResponse componentResponse)
    {
        return CalculateHtml((JsonMap)componentResponse.ElementAsJson, (JsonMap)componentResponse.DynamicStyles);
    }

    static void AddAttribute(HtmlNode htmlNode, HtmlAttribute htmlAttribute)
    {
        (htmlNode.Attributes ??= []).Add(htmlAttribute);
    }

    static void AddChild(HtmlNode parent, HtmlNode child)
    {
        var children = parent.Children ??= [];

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
                htmlNode.Text = Repeat("&nbsp;", (int)jsonMap.Tail.Value);
                return htmlNode;
            }

            htmlNode.Text = "&nbsp;";

            return htmlNode;
        }

        var node = jsonMap.Head;
        while (node is not null)
        {
            ProcessJsonMapNode(htmlNode, node.Key, node.Value);
            node = node.Next;
        }

        return htmlNode;
    }

    static HtmlNode AsHtmlTextNode(string text)
    {
        return new() { IsTextNode = true, Text = text };
    }

    static HtmlNode AsHtmlTextNode(StringBuilder stringBuilder)
    {
        return new() { IsTextNode = true, StringBuilder = stringBuilder };
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

        return new()
        {
            Tag = "style",

            Attributes = [new HtmlAttribute { Name = "id", Value = "ReactWithDotNetDynamicCss" }],

            Text = sb.ToString()
        };
    }

    static StringBuilder CalculateHtml(JsonMap element, JsonMap dynamicStyles)
    {
        var sb = new StringBuilder();

        var wrapperNode = AsHtmlNode(element);

        tryAddDynamicStylesToHeadNode();

        ToString(sb, 0, wrapperNode);

        return sb;

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

    static void ProcessJsonMapNode(HtmlNode htmlNode, string name, object value)
    {
        if (Array.IndexOf(SkipThisProperties, name) >= 0)
        {
            return;
        }

        if (htmlNode.IsThirdPartyComponent && name != "$SuspenseFallback")
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
                AddAttribute(htmlNode, new() { Name = "style", Value = valueAsStyle.ToCss() });
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
            var valueType = value.GetType();

            if (valueType == TypeOfString ||
                valueType == TypeOfDouble ||
                valueType == TypeOfInt32 ||
                valueType == TypeOfInt16 ||
                valueType == TypeOfBoolean ||
                (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == TypeOfUnionProp))
            {
                AddAttribute(htmlNode, new() { Name = PascalToKebabCaseHelper.PascalToKebabCase(name), Value = value.ToString() });
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

                if (child is StringBuilder stringBuilder)
                {
                    AddChild(htmlNode, AsHtmlTextNode(stringBuilder));
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

        if ((name == "$RootNode" || name == "$SuspenseFallback") && value is not null)
        {
            Append(htmlNode, (JsonMap)value);
        }
    }

    static string Repeat(this string text, int n)
    {
        var textAsSpan = text.AsSpan();

        var span = new Span<char>(new char[textAsSpan.Length * n]);
        for (var i = 0; i < n; i++)
        {
            textAsSpan.CopyTo(span.Slice(i * textAsSpan.Length, textAsSpan.Length));
        }

        return span.ToString();
    }

    static void ToString(StringBuilder sb, int depth, HtmlNode htmlNode)
    {
        if (htmlNode.IsTextNode)
        {
            if (htmlNode.StringBuilder is not null)
            {
                sb.Append(htmlNode.StringBuilder);
                return;
            }

            sb.Append(htmlNode.Text);
            return;
        }

        var children = htmlNode.Children;

        if (htmlNode.IsThirdPartyComponent || htmlNode.IsReactFragment || htmlNode.IsVirtualNode)
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
            openTag(sb, depth, htmlNode);

            appendAttributes(sb, htmlNode);

            if (htmlNode.Text != null)
            {
                sb.Append(">");
                sb.Append(htmlNode.Text);
                finishTag(sb, htmlNode);
                return;
            }

            closeTag(sb, htmlNode);

            return;
        }

        openTag(sb, depth, htmlNode);

        appendAttributes(sb, htmlNode);

        sb.Append(">");

        if (htmlNode.Text != null)
        {
            sb.Append(htmlNode.Text);
        }

        if (children.Count == 1 && children[0].IsTextNode)
        {
            ToString(sb, depth, children[0]);
            finishTag(sb, htmlNode);

            return;
        }

        var childrenHasNewLine = false;

        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i];

            var childDepth = depth;

            var canPushNewLine = !(child.IsTextNode || (i > 0 && children[i - 1].IsTextNode));

            if (canPushNewLine)
            {
                childrenHasNewLine = true;

                TryAddNewLine(sb);
                childDepth = depth + 1;
            }

            ToString(sb, childDepth, child);
        }

        if (childrenHasNewLine)
        {
            TryAddNewLine(sb);
        }

        pushIndent(sb, depth);
        finishTag(sb, htmlNode);

        return;

        static void finishTag(StringBuilder sb, HtmlNode htmlNode)
        {
            if (Array.IndexOf(SelfClosingTags, htmlNode.Tag) >= 0)
            {
                sb.Append("<" + htmlNode.Tag + ">");
                return;
            }

            sb.Append("</" + htmlNode.Tag + ">");
        }

        static void pushIndent(StringBuilder sb, int depth)
        {
            if (sb.IsEndsWithNewLine())
            {
                sb.Append("".PadLeft(depth * 2, ' '));
            }
        }

        static void openTag(StringBuilder sb, int depth, HtmlNode htmlNode)
        {
            pushIndent(sb, depth);

            if (htmlNode.Tag == "html")
            {
                sb.AppendLine("<!DOCTYPE html>");
                pushIndent(sb, depth);
            }

            sb.Append("<" + htmlNode.Tag);
        }

        static void closeTag(StringBuilder sb, HtmlNode htmlNode)
        {
            if (Array.IndexOf(SelfClosingTags, htmlNode.Tag) >= 0)
            {
                sb.Append(">");
                return;
            }

            sb.Append(">" + "</" + htmlNode.Tag + ">");
        }

        static void appendAttributes(StringBuilder sb, HtmlNode htmlNode)
        {
            if (htmlNode.Attributes is null)
            {
                return;
            }

            var attributes = CollectionsMarshal.AsSpan(htmlNode.Attributes);

            var length = attributes.Length;

            for (var i = 0; i < length; i++)
            {
                var attribute = attributes[i];

                sb.Append(" ");
                sb.Append(attribute.Name);
                sb.Append('=');
                sb.Append('"');
                sb.Append(attribute.Value);
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

    static class PascalToKebabCaseHelper
    {
        static readonly ConcurrentDictionary<string, string> Cache = new();

        static PascalToKebabCaseHelper()
        {
            Cache.TryAdd("className", "class");
            Cache.TryAdd("htmlFor", "for");
            Cache.TryAdd("cssFloat", "float");
            Cache.TryAdd("viewBox", "viewBox");
        }

        public static string PascalToKebabCase(string dotnetPropertyName)
        {
            if (Cache.TryGetValue(dotnetPropertyName, out var value))
            {
                return value;
            }

            if (dotnetPropertyName.StartsWith("aria-", StringComparison.OrdinalIgnoreCase) ||
                dotnetPropertyName.StartsWith("data-", StringComparison.OrdinalIgnoreCase))
            {
                return dotnetPropertyName;
            }

            var upperCharIndex = indexOfUpperChar(dotnetPropertyName, 1);
            if (upperCharIndex < 0)
            {
                Cache.TryAdd(dotnetPropertyName, dotnetPropertyName);

                return dotnetPropertyName;
            }

            value = pascalToKebabCaseInternal(dotnetPropertyName);

            Cache.TryAdd(dotnetPropertyName, value);

            return value;

            static string pascalToKebabCaseInternal(string str)
            {
                return string.Concat(str.SelectMany(convertChar));

                static IEnumerable<char> convertChar(char c, int index)
                {
                    if (char.IsUpper(c) && index != 0)
                    {
                        yield return '-';
                    }

                    yield return char.ToLower(c, CultureInfo_en_US);
                }
            }

            static int indexOfUpperChar(ReadOnlySpan<char> str, int from)
            {
                var length = str.Length;

                for (var i = from; i < length; i++)
                {
                    if (char.IsUpper(str[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
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

        public StringBuilder StringBuilder;

        public string Tag;
        public string Text;
    }
}