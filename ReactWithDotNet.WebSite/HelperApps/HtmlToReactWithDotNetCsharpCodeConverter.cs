using System.Globalization;
using System.Text;
using HtmlAgilityPack;

namespace ReactWithDotNet.WebSite.HelperApps;

static class HtmlToReactWithDotNetCsharpCodeConverter
{
    static readonly IReadOnlyDictionary<string, string> AttributeRealNameMap = new Dictionary<string, string>
    {
        { "class", "className" },
        { "for", "htmlFor" },
        { "rowspan", "rowSpan" },
        { "colspan", "colSpan" },
        { "cellspacing", "cellSpacing" },
        { "cellpadding", "cellPadding" },
        { "tabindex", "tabIndex" },
        { "preserveaspectratio", "preserveAspectRatio" }
    };

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

        if (value.Contains('\u2028'))
        {
            value = value.Replace('\u2028', '\n');
        }

        if (value.Contains('\n'))
        {
            value = value.Replace("\"", "\"\"");
        }
        else
        {
            value = value.Replace("\"", "\\\"");
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

    static IReadOnlyList<T> Fold<T>(this IEnumerable<IEnumerable<T>> enumerable)
    {
        return enumerable.Aggregate(new List<T>(), (list, item) =>
        {
            list.AddRange(item);
            return list;
        });
    }

    static string GetName(this HtmlAttribute htmlAttribute)
    {
        var name = htmlAttribute.Name;

        if (htmlAttribute.OriginalName != name)
        {
            if (name.All(char.IsLower) && htmlAttribute.OriginalName.Any(char.IsUpper))
            {
                name = htmlAttribute.OriginalName;
            }
        }

        if (AttributeRealNameMap.ContainsKey(name))
        {
            return AttributeRealNameMap[name];
        }

        if (name.Contains(":"))
        {
            var parts = name.Split(":");

            return parts[0] + char.ToUpper(parts[1][0]) + parts[1].Substring(1);
        }

        return name;
    }

    static void Insert(this HtmlAttributeCollection htmlAttributeCollection, int index, string name, string value)
    {
        htmlAttributeCollection.Add(name, value);

        var attribute = htmlAttributeCollection[name];

        htmlAttributeCollection.Remove(attribute);

        htmlAttributeCollection.Insert(index, attribute);
    }

    static IReadOnlyList<HtmlAttribute> RemoveAll(this HtmlAttributeCollection htmlAttributeCollection, Func<HtmlAttribute, bool> match)
    {
        var items = htmlAttributeCollection.Where(match).ToList();

        foreach (var htmlAttribute in items)
        {
            htmlAttributeCollection.Remove(htmlAttribute);
        }

        return items;
    }

    static void RemoveAll(this HtmlNodeCollection htmlNodeCollection, Func<HtmlNode, bool> match)
    {
        var nodes = htmlNodeCollection.Where(match).ToList();

        foreach (var node in nodes)
        {
            htmlNodeCollection.Remove(node);
        }
    }

    /// <summary>
    ///     Removes from end.
    /// </summary>
    static string RemoveFromEnd(this string data, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (data.EndsWith(value, comparison))
        {
            return data.Substring(0, data.Length - value.Length);
        }

        return data;
    }

    /// <summary>
    ///     Removes value from start of str
    /// </summary>
    static string RemoveFromStart(this string data, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (data == null)
        {
            return null;
        }

        if (data.StartsWith(value, comparison))
        {
            return data.Substring(value.Length, data.Length - value.Length);
        }

        return data;
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

        return sb.ToString().Trim();
    }

    static List<string> ToCSharpCode(HtmlNode htmlNode)
    {
        var modifiers = new List<string>();

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

        Style style = null;

        // grab style attribute
        {
            var styleAttribute = htmlNode.Attributes["style"];
            if (styleAttribute != null)
            {
                if (!string.IsNullOrWhiteSpace(styleAttribute.Value))
                {
                    style = Style.ParseCss(styleAttribute.Value);
                }
                
                htmlNode.Attributes.Remove("style");
            }
        }

        string styleAsCode()
        {
            return $"new Style {{ {string.Join(", ", style.ToDictionary().Select(kv => kv.Key + " = \"" + kv.Value + "\""))} }}";
        }

        // aria-*
        {
            static bool isAriaAttribute(HtmlAttribute htmlAttribute)
            {
                return htmlAttribute.Name.StartsWith("aria-", StringComparison.OrdinalIgnoreCase);
            }

            static string toAriaModifier(HtmlAttribute htmlAttribute)
            {
                return $"Aria(\"{htmlAttribute.Name.RemoveFromStart("aria-")}\", \"{htmlAttribute.Value}\")";
            }

            modifiers.AddRange(htmlNode.Attributes.RemoveAll(isAriaAttribute).Select(toAriaModifier));
        }

        // data-*
        {
            static bool isDataAttribute(HtmlAttribute htmlAttribute)
            {
                return htmlAttribute.Name.StartsWith("data-", StringComparison.OrdinalIgnoreCase);
            }

            static string toDataModifier(HtmlAttribute htmlAttribute)
            {
                return $"Data(\"{htmlAttribute.Name.RemoveFromStart("data-")}\", \"{htmlAttribute.Value}\")";
            }

            modifiers.AddRange(htmlNode.Attributes.RemoveAll(isDataAttribute).Select(toDataModifier));
        }

        // remove svg.xmlns
        {
            if (htmlNode.Name == "svg")
            {
                if (htmlNode.Attributes.Contains("xmlns") && htmlNode.Attributes["xmlns"].Value == "http://www.w3.org/2000/svg")
                {
                    htmlNode.Attributes.Remove("xmlns");
                }
            }

            if (htmlNode.Name == "svg" || htmlNode.Name == "path")
            {
                static bool isSnakeCaseAttribute(HtmlAttribute htmlAttribute)
                {
                    return htmlAttribute.Name.IndexOf("-", StringComparison.OrdinalIgnoreCase) > 0;
                }

                foreach (var htmlAttribute in htmlNode.Attributes.RemoveAll(isSnakeCaseAttribute))
                {
                    style[htmlAttribute.Name] = htmlAttribute.Value;
                }
            }
        }

        // innerText
        {
            if (htmlNode.ChildNodes.Count == 1 && htmlNode.ChildNodes[0].Name == "#text")
            {
                if (htmlNode.Attributes.Any() || modifiers.Any())
                {
                    var text = htmlNode.ChildNodes[0].InnerText.Trim();
                    if (string.IsNullOrWhiteSpace(text) is false)
                    {
                        htmlNode.Attributes.Insert(0, "text", text);
                    }

                    htmlNode.ChildNodes.RemoveAt(0);
                }
            }
        }

        // Flex
        {
            if (htmlNodeName == "div")
            {
                if (style is not null)
                {
                    // c o l u m n s
                    if (style.display == "inline-flex" &&
                        style.flexDirection == "column" &&
                        style.justifyContent == "center" &&
                        style.alignItems == "center")
                    {
                        htmlNodeName  = "InlineFlexColumnCentered";
                        style.display = style.flexDirection = style.justifyContent = style.alignItems = null;
                    }

                    if (style.display == "flex" &&
                        style.flexDirection == "column" &&
                        style.justifyContent == "center" &&
                        style.alignItems == "center")
                    {
                        htmlNodeName  = "FlexColumnCentered";
                        style.display = style.flexDirection = style.justifyContent = style.alignItems = null;
                    }

                    if (style.display == "flex" && style.flexDirection == "column")
                    {
                        htmlNodeName  = "FlexColumn";
                        style.display = style.flexDirection = null;
                    }

                    // r o w
                    if (style.display == "inline-flex" &&
                        (style.flexDirection is null || style.flexDirection == "row") &&
                        style.justifyContent == "center" &&
                        style.alignItems == "center")
                    {
                        htmlNodeName  = "InlineFlexRowCentered";
                        style.display = style.flexDirection = style.justifyContent = style.alignItems = null;
                    }

                    if (style.display == "flex" &&
                        (style.flexDirection is null || style.flexDirection == "row") &&
                        style.justifyContent == "center" &&
                        style.alignItems == "center")
                    {
                        htmlNodeName  = "FlexRowCentered";
                        style.display = style.flexDirection = style.justifyContent = style.alignItems = null;
                    }

                    if (style.display == "flex" && style.flexDirection == "row")
                    {
                        htmlNodeName  = "FlexRow";
                        style.display = style.flexDirection = null;
                    }
                }
            }
        }

        // border
        {
            if (style is not null)
            {
                foreach (var prefix in new[] { "borderTop", "borderRight", "borderLeft", "borderBottom" })
                {
                    var xStyle = style[$"{prefix}Style"];
                    var xWidth = style[$"{prefix}Width"];
                    var xColor = style[$"{prefix}Color"];

                    if (style[prefix] is null)
                    {
                        if (string.IsNullOrWhiteSpace(xStyle) is false &&
                            string.IsNullOrWhiteSpace(xWidth) is false &&
                            string.IsNullOrWhiteSpace(xColor) is false)
                        {
                            style[prefix] = $"{xWidth} {xStyle} {xColor}";

                            style[$"{prefix}Style"] = style[$"{prefix}Width"] = style[$"{prefix}Color"] = null;
                        }

                        if (string.IsNullOrWhiteSpace(xStyle) is false &&
                            string.IsNullOrWhiteSpace(xWidth) is false &&
                            string.IsNullOrWhiteSpace(xColor))
                        {
                            style[prefix] = $"{xWidth} {xStyle}";

                            style[$"{prefix}Style"] = style[$"{prefix}Width"] = style[$"{prefix}Color"] = null;
                        }
                    }
                }
            }
        }

        // remove comments
        {
            htmlNode.ChildNodes.RemoveAll(childNode => childNode.Name == "#comment");
        }

        bool canBeExportInOneLine()
        {
            if (htmlNode.Attributes.Contains("text"))
            {
                return false;
            }

            if (style is not null)
            {
                if (canStyleExportInOneLine(style) is false)
                {
                    return false;
                }
            }

            if (htmlNode.Attributes.Count <= 3)
            {
                return true;
            }

            return false;
        }

        static bool canStyleExportInOneLine(Style style)
        {
            return style.ToDictionary().Count <= 3;
        }

        if (htmlNode.ChildNodes.Count == 0)
        {
            List<string> attributeToString(HtmlAttribute attribute)
            {
                if (attribute.Name == "style" && style is not null)
                {
                    if (canStyleExportInOneLine(style))
                    {
                        return new List<string> { $"style = {{ {string.Join(", ", style.ToDictionary().Select(kv => kv.Key + " = \"" + kv.Value + "\""))} }}" };
                    }

                    var returnList = new List<string>
                    {
                        "style =",
                        "{"
                    };

                    returnList.AddRange(style.ToDictionary().Select(toLine));

                    returnList[^1] = returnList[^1].RemoveFromEnd(",");

                    returnList.Add("}");

                    return returnList;

                    static string toLine(KeyValuePair<string, string> kv)
                    {
                        return kv.Key + " = \"" + kv.Value + "\",";
                    }
                }

                return new List<string> { $"{attribute.GetName()} = \"{attribute.Value}\"" };
            }

            if (style is not null)
            {
                htmlNode.Attributes.Add("style", "");
            }

            if (canBeExportInOneLine())
            {
                var sb = new StringBuilder();
                sb.Append($"new {htmlNodeName}");

                if (modifiers.Count == 0 && htmlNode.Attributes.Count == 0)
                {
                    sb.Append("()");
                    return new List<string>
                    {
                        sb.ToString()
                    };
                }

                if (modifiers.Count > 0)
                {
                    sb.Append("(");
                    sb.Append(string.Join(", ", modifiers));
                    sb.Append(")");
                }

                if (htmlNode.Attributes.Count > 0)
                {
                    sb.Append(" { ");
                    sb.Append(string.Join(", ", htmlNode.Attributes.Select(attributeToString).Fold()));
                    sb.Append(" }");
                }

                return new List<string>
                {
                    sb.ToString()
                };
            }

            // multiline
            {
                var sb = new StringBuilder();
                sb.Append($"new {htmlNodeName}");

                if (modifiers.Count > 0)
                {
                    sb.Append("(");
                    sb.Append(string.Join(", ", modifiers));
                    sb.Append(")");
                }

                var lines = new List<string>
                {
                    sb.ToString()
                };

                if (htmlNode.Attributes.Count > 0)
                {
                    lines.Add("{");

                    foreach (var list in htmlNode.Attributes.Select(attributeToString))
                    {
                        if (list.Count > 0)
                        {
                            lines.AddRange(list);
                        }
                        else
                        {
                            lines.Add(list[0]);
                        }

                        lines[^1] += ",";
                    }

                    lines[^1] = lines[^1].RemoveFromEnd(",");
                    lines.Add("}");
                }

                return lines;
            }
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
                $"new {htmlNodeName}({string.Join(", ", modifiers)})",
                "{",
                ConvertToCSharpString(htmlNode.ChildNodes[0].InnerText),
                "}"
            };
        }

        foreach (var htmlAttribute in htmlNode.Attributes)
        {
            modifiers.Add(ToModifier(htmlAttribute));
        }

        if (style is not null)
        {
            modifiers.Add(styleAsCode());
        }

        // multi line
        {
            var lines = new List<string>
            {
                $"new {htmlNodeName}({string.Join(", ", modifiers)})",
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

    static string ToModifier(HtmlAttribute htmlAttribute)
    {
        var attributeName = htmlAttribute.GetName();

        if (attributeName == "target" && htmlAttribute.Value == "_blank")
        {
            return "TargetBlank";
        }

        if (attributeName == "focusable" && htmlAttribute.OwnerNode.Name == "svg")
        {
            return $"svg.Focusable(\"{htmlAttribute.Value}\")";
        }

        if (attributeName.Equals("viewbox", StringComparison.OrdinalIgnoreCase) && htmlAttribute.OwnerNode.Name == "svg")
        {
            return $"ViewBox(\"{htmlAttribute.Value}\")";
        }

        var modifierFullName = $"{CamelCase(attributeName)}{CamelCase(htmlAttribute.Value)}";

        if (typeof(Mixin).GetProperty(modifierFullName) is not null)
        {
            return modifierFullName;
        }

        return $"{CamelCase(attributeName)}(\"{htmlAttribute.Value}\")";
    }
}