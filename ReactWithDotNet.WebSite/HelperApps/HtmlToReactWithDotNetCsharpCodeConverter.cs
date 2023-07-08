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
                if (htmlNode.Name =="link")
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


