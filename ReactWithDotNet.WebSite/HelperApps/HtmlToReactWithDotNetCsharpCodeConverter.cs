using System.Globalization;
using System.Text;
using System.Xml;
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
    
    /// <summary>
    ///     Removes value from start of str
    /// </summary>
     static string RemoveFromStart(this string data, string value)
    {
        return RemoveFromStart(data, value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Removes value from start of str
    /// </summary>
     static string RemoveFromStart(this string data, string value, StringComparison comparison)
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
        borderShortHands();
        targetBlank();

        displayFlexColumnCentered();

        displayFlexRowCentered();

        displayFlexColumn();

        displayFlexRow();
        data();
        aria();
        
        
        void aria()
        {
            var dataList = attributeMap.Where(x => x.Key.StartsWith("aria-", StringComparison.OrdinalIgnoreCase)).ToList();
            
            
            foreach (var (key, value) in dataList)
            {
                attributeMap.Remove(key);

                attributeMap.Add($"Aria(\"{key.RemoveFromStart("data-")}\", \"{value}\")", null);
            }
        }
        
        void data()
        {
            var dataList = attributeMap.Where(x => x.Key.StartsWith("data-", StringComparison.OrdinalIgnoreCase)).ToList();
            
            
            foreach (var (key, value) in dataList)
            {
                attributeMap.Remove(key);

                attributeMap.Add($"Data(\"{key.RemoveFromStart("data-")}\", \"{value}\")", null);
            }
        }
        
        
        
        void displayFlexRow()
        {
            var flexDirection = attributeMap.ContainsKey("flexDirection") ? attributeMap["flexDirection"] : null;
            
            if (attributeMap.TryGetValue("display", out var display))
            {
                if (display == "flex" && (flexDirection == "row" || flexDirection is null) )
                {
                    attributeMap.Remove("display");
                    attributeMap.Remove("flexDirection");

                    attributeMap.Add("DisplayFlexRow", null);
                    return;
                }

                if (display == "inline-flex" && (flexDirection == "row" || flexDirection is null) )
                {
                    attributeMap.Remove("display");
                    attributeMap.Remove("flexDirection");

                    attributeMap.Add("DisplayInlineFlexRow", null);
                }
            }
        }
        
        
        void displayFlexColumn()
        {
            if (attributeMap.TryGetValue("display", out var display))
            {
                if (attributeMap.TryGetValue("flexDirection", out var flexDirection))
                {
                    if (display == "flex" && flexDirection == "column" )
                    {
                        attributeMap.Remove("display");
                        attributeMap.Remove("flexDirection");

                        attributeMap.Add("DisplayFlexColumn", null);
                        return;
                    }

                    if (display == "inline-flex" && flexDirection == "column" )
                    {
                        attributeMap.Remove("display");
                        attributeMap.Remove("flexDirection");

                        attributeMap.Add("DisplayInlineFlexColumn", null);
                    }
                }
            }
        }
        
        
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
                                attributeMap.Remove("display");
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
                                attributeMap.Remove("display");
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
                            attributeMap.Remove("display");
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
                            attributeMap.Remove("display");
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

                    return;
                }

                if (topBottom.EndsWith("px") && 
                    leftRight.EndsWith("px")&&
                    double.TryParse(topBottom.RemoveFromEnd("px"), out var topBottomAsNumber) &&
                    double.TryParse(leftRight.RemoveFromEnd("px"), out var leftRightAsNumber))
                {
                    attributeMap.Remove("paddingTopBottom");
                    attributeMap.Remove("paddingLeftRight");
                    
                    attributeMap.Add($"Padding({topBottomAsNumber}, {leftRightAsNumber})", null);
                    return;
                }
                
                attributeMap.Remove("paddingTopBottom");
                attributeMap.Remove("paddingLeftRight");
                    
                attributeMap.Add($"Padding(\"{topBottom} {leftRight}\")", null);
                
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
                    return;
                }
                
                if (topBottom.EndsWith("px") && 
                    leftRight.EndsWith("px")&&
                    double.TryParse(topBottom.RemoveFromEnd("px"), out var topBottomAsNumber) &&
                    double.TryParse(leftRight.RemoveFromEnd("px"), out var leftRightAsNumber))
                {
                    attributeMap.Remove("marginTopBottom");
                    attributeMap.Remove("marginLeftRight");
                    
                    attributeMap.Add($"Margin({topBottomAsNumber}, {leftRightAsNumber})", null);
                    return;
                }
                
                attributeMap.Remove("marginTopBottom");
                attributeMap.Remove("marginLeftRight");
                    
                attributeMap.Add($"Margin(\"{topBottom} {leftRight}\")", null);
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
            
            
            if (attributeMap.TryGetValue("border", out var borderValue) && borderValue == "0")
            {
                attributeMap.Remove("border");
                attributeMap.Add("BorderNone", null);
            }
        }
        
        
        void borderShortHands()
        {
            foreach (var prefix in new[] { "borderTop", "borderRight", "borderLeft", "borderBottom" })
            {
                if (attributeMap.TryGetValue($"{prefix}Style", out var style) &&
                    attributeMap.TryGetValue($"{prefix}Width", out var width) &&
                    !attributeMap.ContainsKey($"{prefix}Color") &&
                    !attributeMap.ContainsKey($"{prefix}"))
                {
                    attributeMap.Remove($"{prefix}Style");
                    attributeMap.Remove($"{prefix}Width");

                    attributeMap.Add($"{prefix}", $"{width} {style}");
                }
            }
        }
        
        
        void targetBlank()
        {
            if (attributeMap.TryGetValue("target", out var target))
            {
                if (target == "_blank")
                {
                    attributeMap.Remove("target");

                    attributeMap.Add("TargetBlank", "");
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


        Style style= null;

        var styleAttribute = htmlNode.Attributes["style"];
        if (!string.IsNullOrWhiteSpace(styleAttribute?.Value))
        {
            style = Style.ParseCss(styleAttribute.Value);
            
            htmlNode.Attributes.Remove("style");
        }

        string styleAsCode()
        {
            return $"new Style {{ {string.Join(", ", style.ToDictionary().Select(kv => kv.Key + " = \"" + kv.Value+"\""))} }}";
        }

        if (htmlNode.ChildNodes.Count == 0)
        {
            
            string attributeToString(HtmlAttribute attribute)
            {
                if (attribute.Name == "style" && style is not null)
                {
                    return $"{{ {string.Join(", ", style.ToDictionary().Select(kv => kv.Key + " = \"" + kv.Value+"\""))} }}";
                }
                    
                return  $"{attribute.Name} = \"{attribute.Value}\"";
            }
            
            if (style is not null)
            {
                htmlNode.Attributes.Add("style", "");
            }
            
            return new List<string>
            {
                // one line
                $"new {htmlNodeName} {{ {string.Join(", ", htmlNode.Attributes.Select(attributeToString))} }}"
            };
        }


        var attributeMap = htmlNode.Attributes.ToMap();

        bool hasAttribute(string expectedAttributeName, string expectedValue)
        {
            if (attributeMap.ContainsKey(expectedAttributeName) && attributeMap[expectedAttributeName] == expectedValue)
            {
                return true;
            }

            return false;
        }

        if (htmlNode.Name == "svg" && attributeMap.ContainsKey("xmlns") && attributeMap["xmlns"] == "http://www.w3.org/2000/svg")
        {
            attributeMap.Remove("xmlns");
        }

        if (attributeMap.Count > 0 || style is not null)
        {
            ApplyShortHands(attributeMap);

            if (htmlNodeName == "div")
            {
                if (hasAttribute(nameof(DisplayInlineFlexRowCentered), null))
                {
                    htmlNodeName = nameof(InlineFlexRowCentered);
                    attributeMap.Remove(nameof(DisplayInlineFlexRowCentered));
                }
                
                if (hasAttribute(nameof(DisplayInlineFlexRow), null))
                {
                    htmlNodeName = nameof(InlineFlexRow);
                    attributeMap.Remove(nameof(DisplayInlineFlexRow));
                }
                
                if (hasAttribute(nameof(DisplayFlexRow), null))
                {
                    htmlNodeName = nameof(FlexRow);
                    attributeMap.Remove(nameof(DisplayFlexRow));
                }
                
                if (hasAttribute(nameof(DisplayInlineFlexColumnCentered), null))
                {
                    htmlNodeName = nameof(InlineFlexColumnCentered);
                    attributeMap.Remove(nameof(DisplayInlineFlexColumnCentered));
                }
                
                if (hasAttribute(nameof(DisplayInlineFlexColumn), null))
                {
                    htmlNodeName = nameof(InlineFlexColumn);
                    attributeMap.Remove(nameof(DisplayInlineFlexColumn));
                }
                
                if (hasAttribute(nameof(DisplayFlexColumn), null))
                {
                    htmlNodeName = nameof(FlexColumn);
                    attributeMap.Remove(nameof(DisplayFlexColumn));
                }
                
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

            if (style is not null)
            {
                attributeMap.Add("*style*", styleAsCode());
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
                
                
                if (htmlNode.Name == "link" || htmlNode.Name == "path")
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
        
        if (attributeName == "rowspan")
        {
            attributeName = "rowSpan";
        }
        if (attributeName == "colspan")
        {
            attributeName = "colSpan";
        }
        if (attributeName == "cellspacing")
        {
            attributeName = "CellSpacing";
        }
        if (attributeName == "cellpadding")
        {
            attributeName = "cellPadding";
        }
        
        

        if (attributeName == "tabindex")
        {
            attributeName = "tabIndex";
        }

        if (attributeName == "preserveaspectratio")
        {
            attributeName = "preserveAspectRatio";
        }

        if (attributeName.Contains("-") && !attributeName.StartsWith("data-", StringComparison.OrdinalIgnoreCase))
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
        if (name =="*style*")
        {
            return value;
        }
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