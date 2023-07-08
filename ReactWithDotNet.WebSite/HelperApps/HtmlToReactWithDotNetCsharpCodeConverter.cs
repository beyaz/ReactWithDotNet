using System.Globalization;
using System.Text;
using HtmlAgilityPack;

namespace ReactWithDotNet.WebSite.HelperApps;

static class HtmlToReactWithDotNetCsharpCodeConverter
{
    public static string HtmlToCSharp(string htmlRootNode)
    {
        if (string.IsNullOrWhiteSpace(htmlRootNode))
        {
            return null;
        }

        var document = new HtmlDocument();

        document.LoadHtml(htmlRootNode.Trim());

        return ToCSharpCode(ToCSharpCode(document.DocumentNode.FirstChild));
    }

    /// <summary>
    ///     Removes value from end of str
    /// </summary>
    public static string RemoveFromEnd(this string data, string value)
    {
        return RemoveFromEnd(data, value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Removes from end.
    /// </summary>
    public static string RemoveFromEnd(this string data, string value, StringComparison comparison)
    {
        if (data.EndsWith(value, comparison))
        {
            return data.Substring(0, data.Length - value.Length);
        }

        return data;
    }

    static void ApplyShortHands(Dictionary<string, string> attributeMap)
    {
        widthHeightMaxiimized();

        paddingLeftRight();
        paddingTopBottom();
        padding();

        marginLeftRight();
        marginTopBottom();
        margin();

        borderLeftRight();
        borderTopBottom();
        border();

        displayFlexColumnCentered();

        displayFlexRowCentered();

        void displayFlexColumnCentered()
        {
            if (attributeMap.TryGetValue("display", out var display))
            {
                if (attributeMap.TryGetValue("flexDirection", out var flexDirection))
                {
                    if (attributeMap.TryGetValue("justifyContent", out var justifyContent))
                    {
                        if (attributeMap.TryGetValue("alignItems", out var alignItems))
                        {
                            if (display == "flex" &&
                                flexDirection == "column" &&
                                justifyContent == "center" &&
                                alignItems == "alignItems")
                            {
                                attributeMap.Remove("flex");
                                attributeMap.Remove("flexDirection");
                                attributeMap.Remove("justifyContent");
                                attributeMap.Remove("alignItems");

                                attributeMap.Add("DisplayFlexColumnCentered", null);
                                return;
                            }

                            if (display == "inline-flex" &&
                                flexDirection == "column" &&
                                justifyContent == "center" &&
                                alignItems == "alignItems")
                            {
                                attributeMap.Remove("flex");
                                attributeMap.Remove("flexDirection");
                                attributeMap.Remove("justifyContent");
                                attributeMap.Remove("alignItems");

                                attributeMap.Add("DisplayInlineFlexColumnCentered", null);
                            }
                        }
                    }
                }
            }
        }

        void displayFlexRowCentered()
        {
            var flexDirection = attributeMap.ContainsKey("flexDirection") ? attributeMap["flexDirection"] : null;

            if (attributeMap.TryGetValue("display", out var display))
            {
                if (attributeMap.TryGetValue("justifyContent", out var justifyContent))
                {
                    if (attributeMap.TryGetValue("alignItems", out var alignItems))
                    {
                        if (display == "flex" &&
                            (flexDirection is null || flexDirection == "row") &&
                            justifyContent == "center" &&
                            alignItems == "alignItems")
                        {
                            attributeMap.Remove("flex");
                            attributeMap.Remove("flexDirection");
                            attributeMap.Remove("justifyContent");
                            attributeMap.Remove("alignItems");

                            attributeMap.Add("DisplayFlexRowCentered", null);
                            return;
                        }

                        if (display == "inline-flex" &&
                            (flexDirection is null || flexDirection == "row") &&
                            justifyContent == "center" &&
                            alignItems == "alignItems")
                        {
                            attributeMap.Remove("flex");
                            attributeMap.Remove("flexDirection");
                            attributeMap.Remove("justifyContent");
                            attributeMap.Remove("alignItems");

                            attributeMap.Add("DisplayInlineFlexRowCentered", null);
                        }
                    }
                }
            }
        }

        void widthHeightMaxiimized()
        {
            if (attributeMap.TryGetValue("width", out var width))
            {
                if (attributeMap.TryGetValue("height", out var height))
                {
                    if (width == "100%" && height == "100%")
                    {
                        attributeMap.Remove("width");
                        attributeMap.Remove("height");

                        attributeMap.Add("WidthHeightMaximized", null);
                    }
                }
            }
        }

        void paddingLeftRight()
        {
            if (attributeMap.TryGetValue("paddingLeft", out var left))
            {
                if (attributeMap.TryGetValue("paddingRight", out var right))
                {
                    if (left == right)
                    {
                        attributeMap.Remove("paddingLeft");
                        attributeMap.Remove("paddingRight");

                        attributeMap.Add("paddingLeftRight", left);
                    }
                }
            }
        }

        void paddingTopBottom()
        {
            if (attributeMap.TryGetValue("paddingTop", out var top) &&
                attributeMap.TryGetValue("paddingBottom", out var bottom))
            {
                if (top == bottom)
                {
                    attributeMap.Remove("paddingTop");
                    attributeMap.Remove("paddingBottom");

                    attributeMap.Add("paddingTopBottom", top);
                }
            }
        }

        void padding()
        {
            if (attributeMap.TryGetValue("paddingTopBottom", out var topBottom) &&
                attributeMap.TryGetValue("paddingLeftRight", out var leftRight))
            {
                if (topBottom == leftRight)
                {
                    attributeMap.Remove("paddingTopBottom");
                    attributeMap.Remove("paddingLeftRight");

                    attributeMap.Add("padding", topBottom);
                }
            }
        }

        void marginLeftRight()
        {
            if (attributeMap.TryGetValue("marginLeft", out var left))
            {
                if (attributeMap.TryGetValue("marginRight", out var right))
                {
                    if (left == right)
                    {
                        attributeMap.Remove("marginLeft");
                        attributeMap.Remove("marginRight");

                        attributeMap.Add("marginLeftRight", left);
                    }
                }
            }
        }

        void marginTopBottom()
        {
            if (attributeMap.TryGetValue("marginTop", out var top) &&
                attributeMap.TryGetValue("marginBottom", out var bottom))
            {
                if (top == bottom)
                {
                    attributeMap.Remove("marginTop");
                    attributeMap.Remove("marginBottom");

                    attributeMap.Add("marginTopBottom", top);
                }
            }
        }

        void margin()
        {
            if (attributeMap.TryGetValue("marginTopBottom", out var topBottom) &&
                attributeMap.TryGetValue("marginLeftRight", out var leftRight))
            {
                if (topBottom == leftRight)
                {
                    attributeMap.Remove("marginTopBottom");
                    attributeMap.Remove("marginLeftRight");

                    attributeMap.Add("margin", topBottom);
                }
            }
        }

        void borderLeftRight()
        {
            if (attributeMap.TryGetValue("borderLeft", out var left))
            {
                if (attributeMap.TryGetValue("borderRight", out var right))
                {
                    if (left == right)
                    {
                        attributeMap.Remove("borderLeft");
                        attributeMap.Remove("borderRight");

                        attributeMap.Add("borderLeftRight", left);
                    }
                }
            }
        }

        void borderTopBottom()
        {
            if (attributeMap.TryGetValue("borderTop", out var top) &&
                attributeMap.TryGetValue("borderBottom", out var bottom))
            {
                if (top == bottom)
                {
                    attributeMap.Remove("borderTop");
                    attributeMap.Remove("borderBottom");

                    attributeMap.Add("borderTopBottom", top);
                }
            }
        }

        void border()
        {
            if (attributeMap.TryGetValue("borderTopBottom", out var topBottom) &&
                attributeMap.TryGetValue("borderLeftRight", out var leftRight))
            {
                if (topBottom == leftRight)
                {
                    attributeMap.Remove("borderTopBottom");
                    attributeMap.Remove("borderLeftRight");

                    attributeMap.Add("border", topBottom);
                }
            }
        }
    }

    static string CamelCase(string str)
    {
        if (str == null)
        {
            return null;
        }

        if (str.IndexOf('-') > 0)
        {
            return string.Join(string.Empty, str.Split("-").Select(CamelCase));
        }

        if (str == "lowercase")
        {
            return "LowerCase";
        }

        if (str == "uppercase")
        {
            return "UpperCase";
        }

        return char.ToUpper(str[0], new CultureInfo("en-US")) + str.Substring(1);
    }

    static string ConvertToCSharpString(string value)
    {
        if (value == null)
        {
            return null;
        }

        if (value.IndexOf('"') >= 0)
        {
            value = value.Replace('"'.ToString(), '"' + string.Empty + '"');
        }

        if (value.Contains("&nbsp;"))
        {
            return string.Join(", nbsp, ", value.Split(new[] { "&nbsp;" }, StringSplitOptions.RemoveEmptyEntries).Select(ConvertToCSharpString));
        }

        value = '"' + value + '"';

        if (value.Contains('\n'))
        {
            value = '@' + value;
        }

        return value;
    }

    static string ToCSharpCode(IEnumerable<string> lines)
    {
        var sb = new StringBuilder();

        var padding = 0;

        foreach (var line in lines)
        {
            var paddingAsString = "".PadRight(padding * 4, ' ');
            if (line == "{")
            {
                sb.AppendLine(paddingAsString + line);
                padding++;
                continue;
            }

            if (line == "}" || line == "},")
            {
                if (padding == 0)
                {
                    throw new InvalidOperationException("Padding is already zero.");
                }

                padding--;
                paddingAsString = "".PadRight(padding * 4, ' ');
                sb.AppendLine(paddingAsString + line);

                continue;
            }

            sb.AppendLine(paddingAsString + line);
        }

        return sb.ToString();
    }

    static List<string> ToCSharpCode(HtmlNode htmlNode)
    {
        var htmlNodeName = htmlNode.OriginalName;
        if (htmlNodeName == "clippath")
        {
            htmlNodeName = "clipPath";
        }

        if (htmlNodeName == "#text")
        {
            if (string.IsNullOrWhiteSpace(htmlNode.InnerText))
            {
                return Enumerable.Empty<string>().ToList();
            }

            if (htmlNode.InnerText == "&nbsp;")
            {
                return new List<string> { "nbsp" };
            }

            return new List<string> { ConvertToCSharpString(htmlNode.InnerText) };
        }

        if (htmlNodeName == "br")
        {
            return new List<string> { "br" };
        }

        string constructorPart = null;

        var attributeMap = htmlNode.Attributes.ToMap();

        bool hasAttribute(string expectedAttributeName, string expectedValue)
        {
            if (attributeMap.ContainsKey(expectedAttributeName) && attributeMap[expectedAttributeName] == expectedValue)
            {
                return true;
            }

            return false;
        }

        if (attributeMap.Count > 0)
        {
            ApplyShortHands(attributeMap);

            if (htmlNodeName == "div")
            {
                if (hasAttribute("display", "flex"))
                {
                    if (hasAttribute("flexDirection", "column"))
                    {
                        htmlNodeName = "FlexColumn";
                        attributeMap.Remove("display");
                        attributeMap.Remove("flexDirection");

                        if (hasAttribute("JustifyContent", "center") && hasAttribute("AlignItems", "center"))
                        {
                            htmlNodeName = "FlexColumnCenter";
                            attributeMap.Remove("JustifyContent");
                            attributeMap.Remove("AlignItems");
                        }
                    }
                    else if (hasAttribute("flexDirection", "row"))
                    {
                        htmlNodeName = "FlexRow";
                        attributeMap.Remove("display");
                        attributeMap.Remove("flexDirection");

                        if (hasAttribute("JustifyContent", "center") && hasAttribute("AlignItems", "center"))
                        {
                            htmlNodeName = "FlexRowCenter";
                            attributeMap.Remove("JustifyContent");
                            attributeMap.Remove("AlignItems");
                        }
                    }
                }
            }

            constructorPart = $"({string.Join(", ", attributeMap.Select(p => ToModifier(p.Key, p.Value)))})";
        }

        if (htmlNode.ChildNodes.Count == 1 && htmlNode.ChildNodes[0].Name == "#text")
        {
            if (htmlNode.Attributes.Count == 0)
            {
                return new List<string> { $"({htmlNodeName})" + ConvertToCSharpString(htmlNode.ChildNodes[0].InnerText) };
            }

            return new List<string>
            {
                // one line
                $"new {htmlNodeName}{constructorPart}",
                "{",
                ConvertToCSharpString(htmlNode.ChildNodes[0].InnerText),
                "}"
            };
        }

        // check can be written in one line
        {
            if (htmlNode.ChildNodes.Count == 0)
            {
                if (htmlNode.Name == "link")
                {
                    return new List<string>
                    {
                        // one line
                        $"new {htmlNodeName} {{ {string.Join(", ", attributeMap.Select(p => $"{p.Key} = \"{p.Value}\""))} }}"
                    };
                }

                return new List<string>
                {
                    // one line
                    $"new {htmlNodeName}{constructorPart ?? "()"}"
                };
            }
        }

        // multi line
        {
            var lines = new List<string>
            {
                $"new {htmlNodeName}{constructorPart}",
                "{"
            };

            foreach (var items in htmlNode.ChildNodes.Select(ToCSharpCode))
            {
                if (items.Count > 0)
                {
                    items[^1] += ",";
                }

                lines.AddRange(items);
            }

            if (lines[^1].EndsWith(",", StringComparison.OrdinalIgnoreCase))
            {
                lines[^1] = lines[^1].Remove(lines[^1].Length - 1);
            }

            lines.Add("}");

            return lines;
        }
    }

    static IReadOnlyDictionary<string, string> ToDictionary(HtmlAttribute htmlAttribute)
    {
        if (htmlAttribute.Name == "style" && !string.IsNullOrWhiteSpace(htmlAttribute.Value))
        {
            return Style.ParseCss(htmlAttribute.Value).ToDictionary();
        }

        var lines = new Dictionary<string, string>();

        var attributeName = htmlAttribute.OriginalName;
        if (attributeName == "class")
        {
            attributeName = "className";
        }

        if (attributeName == "for")
        {
            attributeName = "htmlFor";
        }

        if (attributeName == "viewbox")
        {
            attributeName = "viewBox";
        }

        if (attributeName == "tabindex")
        {
            attributeName = "tabIndex";
        }

        if (attributeName == "preserveaspectratio")
        {
            attributeName = "preserveAspectRatio";
        }

        if (attributeName.Contains("-"))
        {
            var parts = attributeName.Split("-");

            attributeName = parts[0] + char.ToUpper(parts[1][0]) + parts[1].Substring(1);
        }

        if (attributeName.Contains(":"))
        {
            var parts = attributeName.Split(":");

            attributeName = parts[0] + char.ToUpper(parts[1][0]) + parts[1].Substring(1);
        }

        lines.Add(attributeName, htmlAttribute.Value);

        return lines;
    }

    static Dictionary<string, string> ToMap(this HtmlAttributeCollection htmlAttributes)
    {
        var map = new Dictionary<string, string>();

        foreach (var htmlAttribute in htmlAttributes)
        {
            foreach (var (key, value) in ToDictionary(htmlAttribute))
            {
                map.Add(key, value);
            }
        }

        return map;
    }

    static string ToModifier(string name, string value)
    {
        if (char.IsUpper(name[0]) && string.IsNullOrEmpty(value))
        {
            return name;
        }

        if (name == "flex" && value.Split(' ').Length == 3)
        {
            return $"Flex({string.Join(", ", value.Split(' '))})";
        }

        if (value.EndsWith("px"))
        {
            if ("LineHeight FontSize".Split(' ').Any(x => x == CamelCase(name)))
            {
                if (int.TryParse(value.RemoveFromEnd("px"), out var valueAsNumber) && valueAsNumber <= 40)
                {
                    return $"{CamelCase(name)}{valueAsNumber}";
                }
            }

            return $"{CamelCase(name)}({value.RemoveFromEnd("px")})";
        }

        if (value.EndsWith("%") || value.StartsWith("#") || value.Contains(' ') || value.Contains('/') || name == "background")
        {
            if ("Width Height".Split(' ').Any(x => x == CamelCase(name)))
            {
                if (int.TryParse(value.RemoveFromEnd("%"), out var valueAsNumber) && valueAsNumber == 100)
                {
                    return $"{CamelCase(name)}Maximized";
                }
            }

            return $"{CamelCase(name)}(\"{value}\")";
        }

        if (decimal.TryParse(value, out var valueAsNumeric))
        {
            if ("FontWeight LineHeight".Split(' ').Any(x => x == CamelCase(name)))
            {
                return $"{CamelCase(name)}{CamelCase(value)}";
            }

            return $"{CamelCase(name)}({valueAsNumeric})";
        }

        if ("Id ClassName Alt Src Href".Split(' ').Any(x => x == CamelCase(name)))
        {
            return $"{CamelCase(name)}(\"{CamelCase(value)}\")";
        }

        var modifierFullName = $"{CamelCase(name)}{CamelCase(value)}";

        if (typeof(Mixin).GetProperty(modifierFullName) is not null)
        {
            return modifierFullName;
        }

        return $"{CamelCase(name)}(\"{CamelCase(value)}\")";
    }
}