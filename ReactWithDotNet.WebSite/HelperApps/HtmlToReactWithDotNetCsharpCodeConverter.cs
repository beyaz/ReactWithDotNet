using System.Reflection;
using System.Text;
using HtmlAgilityPack;
using PropertyInfo = System.Reflection.PropertyInfo;

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

    public static string HtmlToCSharp(string htmlRootNode, bool smartMode, int maxAttributeCountPerLine)
    {
        if (string.IsNullOrWhiteSpace(htmlRootNode))
        {
            return null;
        }

        var document = new HtmlDocument();

        document.LoadHtml(htmlRootNode.Trim());

        return ToCSharpCode(ToCSharpCode(document.DocumentNode.FirstChild, smartMode, maxAttributeCountPerLine));
    }

    static string CamelCase(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
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

        return char.ToUpper(str[0], new("en-US")) + str.Substring(1);
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
            return string.Join(", nbsp, ", value.Split(["&nbsp;"], StringSplitOptions.None).Select(ConvertToCSharpString));
        }

        value = '"' + value + '"';

        if (value.Contains('\n'))
        {
            value = '@' + value;
        }

        return value;
    }

    static bool EndsWithPixel(this string value)
    {
        return value?.EndsWith("px", StringComparison.OrdinalIgnoreCase) == true;
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
            if (parts.Length == 2)
            {
                return parts[0] + char.ToUpper(parts[1][0]) + parts[1][1..];
            }
        }

        return name;
    }

    static string GetTagName(this HtmlAttribute htmlAttribute)
    {
        return htmlAttribute.OwnerNode.Name;
    }

    static bool HasValue(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    static void Insert(this HtmlAttributeCollection htmlAttributeCollection, int index, string name, string value)
    {
        htmlAttributeCollection.Add(name, value);

        var attribute = htmlAttributeCollection[name];

        htmlAttributeCollection.Remove(attribute);

        htmlAttributeCollection.Insert(index, attribute);
    }

    static bool IsEndsWithPixel(string x)
    {
        return x.EndsWith("px", StringComparison.OrdinalIgnoreCase);
    }

    static bool IsGlobalDeclaredStringVariable(string value)
    {
        if (value == none ||
            value == auto ||
            value == inset ||
            value == inherit ||
            value == transparent ||
            value == solid ||
            value == dotted)
        {
            return true;
        }

        return false;
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

    static string RemovePixelFromEnd(this string value)
    {
        return value?.RemoveFromEnd("px");
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

    record Data
    {
        public HtmlNode htmlNode { get; init; }
        public bool smartMode { get; init; }
        public int maxAttributeCountPerLine { get; init; }
        
        public HtmlAttribute classNameAttribute { get; init; }
        
        public string htmlNodeName { get; init; }

        public Style style{ get; init; }
        public List<ModifierCode> modifiers{ get; init; }
    }

    static List<string> ToCSharpCode(HtmlNode htmlNode, bool smartMode, int maxAttributeCountPerLine)
    {
        return ToCSharpCode(new Data
        {
            htmlNode                 = htmlNode,
            smartMode                = smartMode,
            maxAttributeCountPerLine = maxAttributeCountPerLine
        });
    }
    static List<string> ToCSharpCode(Data data)
    {
        data = tryRemoveClassAttribute(data);
        data = ignoreSmartModeSpecificTags(data);
        data = initHtmlNodeName(data);
        
        if (data.htmlNodeName == "#text")
        {
            if (string.IsNullOrWhiteSpace(data.htmlNode.InnerText))
            {
                return Enumerable.Empty<string>().ToList();
            }

            if (data.htmlNode.InnerText == "&nbsp;")
            {
                return ["nbsp"];
            }

            return [ConvertToCSharpString(data.htmlNode.InnerText)];
        }

        if (data.htmlNodeName == "br")
        {
            return ["br"];
        }

        data = data with { modifiers = new() };

        data = grabStyleAttribute(data);
        data = moveAriaAttributesToModifiers(data);
        data = moveDataAttributesToModifiers(data);
        data = arrangeSvgSizeAttribute(data);
        data = moveStylableAttributesToStyleForSvgAndPath(data);
        data = tryArrangeInnerNodeText(data);
        data = arrangeFlex(data);
        data = arrangeShortwayStyle(data);
        data = removeComments(data);
        data = whenSmartModeMoveAllStyleToModifiers(data);

        if (data.htmlNode.ChildNodes.Count == 0)
        {
            List<string> attributeToString(HtmlAttribute attribute)
            {
                if (attribute.Name == "style" && data.style is not null)
                {
                    if (canStyleExportInOneLine(data.style))
                    {
                        if (data.smartMode)
                        {
                            return [string.Join(", ", data.style.ToDictionary().Select(p => TryConvertToModifier_From_Mixin_Extension(p.Key, p.Value)).Where(x => x.success).Select(x => x.modifierCode))];
                        }

                        return [$"style = {{ {string.Join(", ", data.style.ToDictionary().Select(kv => kv.Key + " = \"" + kv.Value + "\""))} }}"];
                    }

                    var returnList = new List<string>
                    {
                        "style =",
                        "{"
                    };

                    returnList.AddRange(data.style.ToDictionary().Select(toLine));

                    returnList[^1] = returnList[^1].RemoveFromEnd(",");

                    returnList.Add("}");

                    return returnList;

                    static string toLine(KeyValuePair<string, string> kv)
                    {
                        if (IsGlobalDeclaredStringVariable(kv.Value))
                        {
                            return kv.Key + " = " + kv.Value + ",";
                        }

                        return kv.Key + " = \"" + kv.Value + "\",";
                    }
                }

                var propertyInfo = TryFindProperty(attribute.GetTagName(), attribute.GetName());
                if (propertyInfo is not null)
                {
                    if (data.smartMode)
                    {
                        return [TryConvertToModifier(attribute).modifierCode];
                    }

                    if (propertyInfo.PropertyType.IsGenericType)
                    {
                        if (propertyInfo.PropertyType.GetGenericTypeDefinition().Name.StartsWith("UnionProp`"))
                        {
                            var genericArguments = propertyInfo.PropertyType.GetGenericArguments();

                            if (genericArguments.Contains(typeof(double)) ||
                                genericArguments.Contains(typeof(double)) ||
                                genericArguments.Contains(typeof(double?)))
                            {
                                if (double.TryParse(attribute.Value.Replace(".", ""), out _))
                                {
                                    return [$"{propertyInfo.Name} = {attribute.Value}"];
                                }
                            }
                        }
                    }

                    if (IsGlobalDeclaredStringVariable(attribute.Value))
                    {
                        return [$"{propertyInfo.Name} = {attribute.Value}"];
                    }

                    return [$"{propertyInfo.Name} = \"{attribute.Value}\""];
                }

                if (canBeExportInOneLine(data))
                {
                    return [$"/* {attribute.GetName()} = \"{attribute.Value}\"*/"];
                }

                return [$"// {attribute.GetName()} = \"{attribute.Value}\""];
            }

            if (data.style is not null)
            {
                data.htmlNode.Attributes.Add("style", "");
            }

            if (canBeExportInOneLine(data))
            {
                if (data.smartMode && data.modifiers.Count > 0)
                {
                    return [$"new {data.htmlNodeName} {{ {JoinModifiers(data.modifiers)} }}"];
                }

                var sb = new StringBuilder();
                sb.Append($"new {data.htmlNodeName}");

                if (data.modifiers.Count == 0 && data.htmlNode.Attributes.Count == 0)
                {
                    sb.Append("()");
                    return [sb.ToString()];
                }

                if (data.modifiers.Count > 0)
                {
                    sb.Append("(");
                    sb.Append(JoinModifiers(data.modifiers));
                    sb.Append(")");
                }

                if (data.htmlNode.Attributes.Count > 0)
                {
                    sb.Append(" { ");
                    sb.Append(string.Join(", ", data.htmlNode.Attributes.Select(attributeToString).Fold()));
                    sb.Append(" }");
                }

                return [sb.ToString()];
            }

            // multiline
            {
                if (data.smartMode)
                {
                    if (data.modifiers.Count > 0 && data.htmlNode.Attributes.Count == 0)
                    {
                        var lines = new List<string>
                        {
                            $"new {data.htmlNodeName}",
                            "{"
                        };

                        foreach (var modifier in data.modifiers.Where(x => !x.Success).Select(x => x.Code))
                        {
                            lines.Add($"// {modifier}");
                        }

                        foreach (var modifier in data.modifiers.Where(x => x.Success).Select(x => x.Code))
                        {
                            lines.Add(modifier);

                            lines[^1] += ",";
                        }

                        lines[^1] = lines[^1].RemoveFromEnd(",");
                        lines.Add("}");

                        return lines;
                    }
                }

                var sb = new StringBuilder();
                sb.Append($"new {data.htmlNodeName}");
                
                
                
                if (data.modifiers.Count > 0)
                {
                    sb.Append("(");
                    sb.Append(JoinModifiers(data.modifiers));
                    sb.Append(")");
                }
                
                if (data.modifiers.Count == 0 && data.classNameAttribute is not null)
                {
                    sb.Append("(");
                    sb.Append('"');
                    sb.Append(data.classNameAttribute.Value);
                    sb.Append('"');
                    sb.Append(")");
                }

                {
                    var lines = new List<string>
                    {
                        sb.ToString()
                    };

                    if (data.htmlNode.Attributes.Count == 1 && data.htmlNode.Attributes[0].Name=="text")
                    {
                        lines.Add("{");
                        lines.Add(ConvertToCSharpString(data.htmlNode.Attributes[0].Value));
                        lines.Add("}");
                        return lines;
                    }
                    

                    if (data.htmlNode.Attributes.Count > 0)
                    {
                        lines.Add("{");

                        foreach (var list in data.htmlNode.Attributes.Select(attributeToString))
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
        }

        data = convertAllAttributesToModifiers(data);

        if (data.htmlNode.ChildNodes.Count == 1 && data.htmlNode.ChildNodes[0].Name == "#text")
        {
            if (data.htmlNode.Attributes.Count == 0 && data.style is null)
            {
                return [$"({data.htmlNodeName})" + ConvertToCSharpString(data.htmlNode.ChildNodes[0].InnerText)];
            }

            return
            [
                $"new {data.htmlNodeName}({JoinModifiers(data.modifiers)})",
                "{",
                ConvertToCSharpString(data.htmlNode.ChildNodes[0].InnerText),
                "}"
            ];
        }


        return exportMultiLine(data);

        static Data grabStyleAttribute(Data data)
        {
            var styleAttribute = data.htmlNode.Attributes["style"];
            if (styleAttribute != null)
            {
                if (!string.IsNullOrWhiteSpace(styleAttribute.Value))
                {
                    data = data with { style = Style.ParseCss(styleAttribute.Value) };
                }

                data.htmlNode.Attributes.Remove("style");
            }

            return data;
        }

        static Data moveAriaAttributesToModifiers(Data data)
        {
            static bool isAriaAttribute(HtmlAttribute htmlAttribute)
            {
                return htmlAttribute.Name.StartsWith("aria-", StringComparison.OrdinalIgnoreCase);
            }

            static string toAriaModifier(HtmlAttribute htmlAttribute)
            {
                return $"Aria(\"{htmlAttribute.Name.RemoveFromStart("aria-")}\", \"{htmlAttribute.Value}\")";
            }

            data.modifiers.AddRange(data.htmlNode.Attributes.RemoveAll(isAriaAttribute).Select(toAriaModifier).Select(ModifierCode.FromString));

            return data;
        }

        static Data moveDataAttributesToModifiers(Data data)
        {
            static bool isDataAttribute(HtmlAttribute htmlAttribute)
            {
                return htmlAttribute.Name.StartsWith("data-", StringComparison.OrdinalIgnoreCase);
            }

            static string toDataModifier(HtmlAttribute htmlAttribute)
            {
                return $"Data(\"{htmlAttribute.Name.RemoveFromStart("data-")}\", \"{htmlAttribute.Value}\")";
            }

            data.modifiers.AddRange(data.htmlNode.Attributes.RemoveAll(isDataAttribute).Select(toDataModifier).Select(ModifierCode.FromString));
            
            return data;
        }

        static List<string> exportMultiLine(Data data)
        {
            var partConstructor = "";
            if (data.modifiers.Count > 0)
            {
                partConstructor = $"({JoinModifiers(data.modifiers)})";
            }

            var lines = new List<string>
            {
                $"new {data.htmlNodeName}{partConstructor}",
                "{"
            };

            foreach (var items in data.htmlNode.ChildNodes.Select(x => ToCSharpCode(x, data.smartMode, data.maxAttributeCountPerLine)))
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
        

        static Data convertAllAttributesToModifiers(Data data)
        {
        
        
            foreach (var htmlAttribute in data.htmlNode.Attributes)
            {
                var (success, modifierCode) = TryConvertToModifier(htmlAttribute);
                if (success)
                {
                    data.modifiers.Add(modifierCode);
                }
            }

            if (data.style is not null)
            {
                data.modifiers.Add(styleAsCode(data.style));
            }

            return data;
        }

        static bool canBeExportInOneLine(Data data)
        {
            if (data.htmlNode.Attributes.Contains("text"))
            {
                return false;
            }

            if (data.style is not null)
            {
                if (canStyleExportInOneLine(data.style) is false)
                {
                    return false;
                }
            }

            if (data.htmlNode.ChildNodes.Count == 0 && data.smartMode && data.modifiers.Count > data.maxAttributeCountPerLine)
            {
                return false;
            }

            if (data.htmlNode.Attributes.Count > data.maxAttributeCountPerLine)
            {
                return false;
            }

            return true;
        }

        static Data whenSmartModeMoveAllStyleToModifiers(Data data)
        {

         
        
            if (data.smartMode && data.style is not null)
            {
                data.modifiers.AddRange(data.htmlNode.Attributes.Select(TryConvertToModifier).Select(ModifierCode.From));

                ((ICollection<HtmlAttribute>)data.htmlNode.Attributes).Clear();
                data.modifiers.AddRange(data.style.ToDictionary().Select(p => TryConvertToModifier_From_Mixin_Extension(p.Key, p.Value)).Select(ModifierCode.From));

                data = data with { style = null };
            }
        
            return data;
        }

        static Data removeComments(Data data)
        {
            data.htmlNode.ChildNodes.RemoveAll(childNode => childNode.Name == "#comment");

            return data;
        }

        static Data arrangeShortwayStyle(Data data)
        {

         
            if (data.style is not null)
            {
                // border
                foreach (var prefix in new[] { "borderTop", "borderRight", "borderLeft", "borderBottom" })
                {
                    var xStyle = data.style[$"{prefix}Style"];
                    var xWidth = data.style[$"{prefix}Width"];
                    var xColor = data.style[$"{prefix}Color"];

                    if (data.style[prefix] is null)
                    {
                        if (string.IsNullOrWhiteSpace(xStyle) is false &&
                            string.IsNullOrWhiteSpace(xWidth) is false &&
                            string.IsNullOrWhiteSpace(xColor) is false)
                        {
                            data.style[prefix] = $"{xWidth} {xStyle} {xColor}";

                            data.style[$"{prefix}Style"] = data.style[$"{prefix}Width"] = data.style[$"{prefix}Color"] = null;
                        }

                        if (string.IsNullOrWhiteSpace(xStyle) is false &&
                            string.IsNullOrWhiteSpace(xWidth) is false &&
                            string.IsNullOrWhiteSpace(xColor))
                        {
                            data.style[prefix] = $"{xWidth} {xStyle}";

                            data.style[$"{prefix}Style"] = data.style[$"{prefix}Width"] = data.style[$"{prefix}Color"] = null;
                        }
                    }
                }

                // p a d d i n g
                if (data.style.paddingTop.HasValue() &&
                    data.style.paddingRight.HasValue() &&
                    data.style.paddingBottom.HasValue() &&
                    data.style.paddingLeft.HasValue())
                {
                    data.style.padding = $"{data.style.paddingTop} {data.style.paddingRight} {data.style.paddingBottom} {data.style.paddingLeft}";

                    data.style.paddingTop = data.style.paddingRight = data.style.paddingBottom = data.style.paddingLeft = null;
                }

                // m a r g i n
                if (data.style.marginTop.HasValue() &&
                    data.style.marginRight.HasValue() &&
                    data.style.marginBottom.HasValue() &&
                    data.style.marginLeft.HasValue())
                {
                    data.style.margin = $"{data.style.marginTop} {data.style.marginRight} {data.style.marginBottom} {data.style.marginLeft}";

                    data.style.marginTop = data.style.marginRight = data.style.marginBottom = data.style.marginLeft = null;
                }

                if (data.smartMode)
                {
                    // padding: TopBottom
                    if (data.style.paddingTop.EndsWithPixel() &&
                        data.style.paddingBottom.EndsWithPixel() &&
                        data.style.paddingTop == data.style.paddingBottom)
                    {
                        data.style.padding = MarkAsAlreadyCalculatedModifier($"PaddingTopBottom({data.style.paddingTop.RemovePixelFromEnd()})");

                        data.style.paddingTop = data.style.paddingBottom = null;
                    }

                    // padding: LeftRight
                    if (data.style.paddingLeft.EndsWithPixel() &&
                        data.style.paddingRight.EndsWithPixel() &&
                        data.style.paddingLeft == data.style.paddingRight)
                    {
                        data.style.padding = MarkAsAlreadyCalculatedModifier($"PaddingLeftRight({data.style.paddingLeft.RemovePixelFromEnd()})");

                        data.style.paddingLeft = data.style.paddingRight = null;
                    }

                    // margin: TopBottom
                    if (data.style.marginTop.EndsWithPixel() &&
                        data.style.marginBottom.EndsWithPixel() &&
                        data.style.marginTop == data.style.marginBottom)
                    {
                        data.style.margin = MarkAsAlreadyCalculatedModifier($"MarginTopBottom({data.style.marginTop.RemovePixelFromEnd()})");

                        data.style.marginTop = data.style.marginBottom = null;
                    }

                    // margin: LeftRight
                    if (data.style.marginLeft.EndsWithPixel() &&
                        data.style.marginRight.EndsWithPixel() &&
                        data.style.marginLeft == data.style.marginRight)
                    {
                        data.style.margin = MarkAsAlreadyCalculatedModifier($"MarginLeftRight({data.style.marginLeft.RemovePixelFromEnd()})");

                        data.style.marginLeft = data.style.marginRight = null;
                    }

                    // padding: SizeFull
                    if (data.style.width == "100%" && data.style.height == "100%")
                    {
                        data.style.width = MarkAsAlreadyCalculatedModifier("SizeFull");

                        data.style.height = null;
                    }

                    // margin: WidthHeight
                    if (data.style.width.EndsWithPixel() &&
                        data.style.height.EndsWithPixel() &&
                        data.style.width == data.style.height)
                    {
                        data.style.width = MarkAsAlreadyCalculatedModifier($"Size({data.style.width.RemovePixelFromEnd()})");

                        data.style.height = null;
                    }
                }
            }
            return data;
        }

        static Data arrangeFlex(Data data)
        {
            if (data.htmlNodeName == "div")
            {
                if (data.style is not null)
                {
                    // c o l u m n s
                    if (data.style.display == "inline-flex" &&
                        data.style.flexDirection == "column" &&
                        data.style.justifyContent == "center" &&
                        data.style.alignItems == "center")
                    {
                        data = data with { htmlNodeName = "InlineFlexColumnCentered" };
                        
                        data.style.display = data.style.flexDirection = data.style.justifyContent = data.style.alignItems = null;
                    }

                    if (data.style.display == "inline-flex" &&
                        data.style.flexDirection == "column")
                    {
                        data = data with { htmlNodeName = "InlineFlexColumn" };
                        
                        data.style.display = data.style.flexDirection = null;
                    }

                    if (data.style.display == "flex" &&
                        data.style.flexDirection == "column" &&
                        data.style.justifyContent == "center" &&
                        data.style.alignItems == "center")
                    {
                        data = data with { htmlNodeName = "FlexColumnCentered" };
                        
                        data.style.display = data.style.flexDirection = data.style.justifyContent = data.style.alignItems = null;
                    }

                    if (data.style.display == "flex" && data.style.flexDirection == "column")
                    {
                        data = data with { htmlNodeName = "FlexColumn" };
                        
                        data.style.display = data.style.flexDirection = null;
                    }

                    // r o w
                    if (data.style.display == "inline-flex" &&
                        (data.style.flexDirection is null || data.style.flexDirection == "row") &&
                        data.style.justifyContent == "center" &&
                        data.style.alignItems == "center")
                    {
                        data = data with { htmlNodeName = "InlineFlexRowCentered" };
                        
                        data.style.display = data.style.flexDirection = data.style.justifyContent = data.style.alignItems = null;
                    }

                    if (data.style.display == "inline-flex" &&
                        (data.style.flexDirection is null || data.style.flexDirection == "row"))
                    {
                        data = data with { htmlNodeName = "InlineFlexRow" };
                        
                        data.style.display = data.style.flexDirection = null;
                    }

                    if (data.style.display == "flex" &&
                        (data.style.flexDirection is null || data.style.flexDirection == "row") &&
                        data.style.justifyContent == "center" &&
                        data.style.alignItems == "center")
                    {
                        data = data with { htmlNodeName = "FlexRowCentered" };
                        
                        data.style.display = data.style.flexDirection = data.style.justifyContent = data.style.alignItems = null;
                    }

                    if (data.style.display == "flex" && (data.style.flexDirection is null || data.style.flexDirection == "row"))
                    {
                        data = data with { htmlNodeName = "FlexRow" };
                        
                        data.style.display = data.style.flexDirection = null;
                    }
                }
            }

            return data;
        }

        static Data tryArrangeInnerNodeText(Data data)
        {
            if (data.htmlNode.ChildNodes.Count == 1 && data.htmlNode.ChildNodes[0].Name == "#text")
            {
                var text = data.htmlNode.ChildNodes[0].InnerText.Trim();

                if (string.IsNullOrWhiteSpace(text) is false)
                {
                    if (data.smartMode)
                    {
                        data.modifiers.Insert(0, ConvertToCSharpString(text));
                    }
                    else //if (htmlNode.Attributes.Any())
                    {
                        data.htmlNode.Attributes.Insert(0, "text", text);
                    }
                }

                data.htmlNode.ChildNodes.RemoveAt(0);
            }

            return data;
        }

        static Data moveStylableAttributesToStyleForSvgAndPath(Data data)
        {
            if (data.htmlNode.Name == "svg" || data.htmlNode.Name == "path")
            {
                static bool isStyleAttribute(Data data, HtmlAttribute htmlAttribute)
                {
                    if (data.htmlNode.Name == "svg" && "size".Equals(htmlAttribute.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }

                    if (TryFindProperty(data.htmlNode.Name, htmlAttribute.Name) is null)
                    {
                        if (typeof(Style).GetProperty(htmlAttribute.Name.Replace("-", ""), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase) is not null)
                        {
                            return true;
                        }
                    }

                    return false;
                }

                foreach (var htmlAttribute in data.htmlNode.Attributes.RemoveAll(x=>isStyleAttribute(data,x)))
                {
                    data = data with { style = data.style ?? new() };

                    data.style[htmlAttribute.Name] = htmlAttribute.Value;
                }
            }

            return data;
        }

        static Data arrangeSvgSizeAttribute(Data data)
        {
            if (data.htmlNode.Name == "svg")
            {
                if (data.htmlNode.Attributes.Contains("xmlns") && data.htmlNode.Attributes["xmlns"].Value == "http://www.w3.org/2000/svg")
                {
                    data.htmlNode.Attributes.Remove("xmlns");
                }

                if (data.htmlNode.Attributes.Contains("width") &&
                    data.htmlNode.Attributes.Contains("height") &&
                    data.htmlNode.Attributes["width"].Value == data.htmlNode.Attributes["height"].Value)
                {
                    data.htmlNode.Attributes.Add("size", data.htmlNode.Attributes["width"].Value);

                    data.htmlNode.Attributes.Remove("width");
                    data.htmlNode.Attributes.Remove("height");
                }
            }

            return data;
        }

        static Data initHtmlNodeName(Data data)
        {
            var htmlNodeName = data.htmlNode.OriginalName;
            if (htmlNodeName == "clippath")
            {
                htmlNodeName = "clipPath";
            }

            return data with { htmlNodeName = htmlNodeName };
        }

        static Data ignoreSmartModeSpecificTags(Data data)
        {
            // ignore smart mode for specific case beautiful code format
            var smartModeIgnoredTags = new List<string> { "rect", "path", "circle", "line" };
            if (data.htmlNode.ChildNodes.Count == 0 && smartModeIgnoredTags.Any(tag => data.htmlNode.Name.Equals(tag, StringComparison.OrdinalIgnoreCase)))
            {
                return data with { smartMode = false };
            }

            return data;
        }

        static Data tryRemoveClassAttribute(Data data)
        {
            var htmlNode = data.htmlNode;
            
            var classNameAttribute = htmlNode.Attributes.FirstOrDefault(x => x.Name == "class");
            if (classNameAttribute is null)
            {
                return data;
            }
            
            htmlNode.Attributes.Remove(classNameAttribute);

            return data with { classNameAttribute = classNameAttribute };
        }

        static bool canStyleExportInOneLine(Style style)
        {
            return style.ToDictionary().Count <= 3;
        }

        static string styleAsCode(Style style)
        {
            return $"new Style {{ {string.Join(", ", style.ToDictionary().Select(kv => kv.Key + " = \"" + kv.Value + "\""))} }}";
        }

        static string JoinModifiers(IReadOnlyList<ModifierCode> modifiers)
        {
            return string.Join(" ", modifiers.Where(ModifierCode.IsFail).Select(x => x.Code)) +
                   string.Join(", ", modifiers.Where(ModifierCode.IsSuccess).Select(x => x.Code));
        }
    }

    static (bool success, string modifierCode) TryConvertToModifier(HtmlAttribute htmlAttribute)
    {
        var name = htmlAttribute.GetName();
        var value = htmlAttribute.Value;
        var tagName = htmlAttribute.OwnerNode.Name;

        var success = (string modifierCode) => (true, modifierCode);

        if (tagName == "iframe" && name.Equals("src", StringComparison.OrdinalIgnoreCase))
        {
            return success($"iframe.Src({value})");
        }

        if (tagName == "svg" && name.Equals("size", StringComparison.OrdinalIgnoreCase) && double.TryParse(value, out _))
        {
            return success($"svg.Size({value})");
        }

        if (tagName == "symbol" && name.Equals("viewBox", StringComparison.OrdinalIgnoreCase))
        {
            return success($"symbol.ViewBox(\"{value}\")");
        }

        if (tagName == "source" && name.Equals("src", StringComparison.OrdinalIgnoreCase))
        {
            return success($"source.Src(\"{value}\")");
        }

        if (tagName == "svg" && name.Equals("width", StringComparison.OrdinalIgnoreCase) && double.TryParse(value, out _))
        {
            return success($"svg.Width({value})");
        }

        if (tagName == "svg" && name.Equals("height", StringComparison.OrdinalIgnoreCase) && double.TryParse(value, out _))
        {
            return success($"svg.Height({value})");
        }

        if (name == "focusable" && tagName == "svg")
        {
            if ("true".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return success($"svg.{nameof(svg.FocusableTrue)}");
            }

            if ("false".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return success($"svg.{nameof(svg.FocusableFalse)}");
            }

            if ("auto".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return success($"svg.{nameof(svg.FocusableAuto)}");
            }

            return success($"svg.Focusable(\"{value}\")");
        }

        if (name == "type" && tagName == "button")
        {
            return success($"button.Type(\"{value}\")");
        }

        if (name.Equals("viewbox", StringComparison.OrdinalIgnoreCase) && tagName == "svg")
        {
            var parseResponse = tryParseViewBoxValues(value);
            if (parseResponse.success)
            {
                return success($"ViewBox({string.Join(", ", parseResponse.parameters)})");
            }

            return success($"ViewBox(\"{value}\")");
        }

        var response = TryConvertToModifier_From_Mixin_Extension(name, value);
        if (response.success)
        {
            return response;
        }

        var propertyInfo = TryFindProperty(tagName, name);
        if (propertyInfo is not null)
        {
            if (propertyInfo.PropertyType == typeof(double?) || propertyInfo.PropertyType == typeof(double))
            {
                if (double.TryParse(value, out var valueAsDouble))
                {
                    return success($"{tagName}.{UpperCaseFirstChar(propertyInfo.Name)}({valueAsDouble})");
                }
            }

            if (propertyInfo.PropertyType == typeof(int?))
            {
                if (int.TryParse(value, out var valueAsInt32))
                {
                    return success($"{tagName}.{UpperCaseFirstChar(propertyInfo.Name)}({valueAsInt32})");
                }
            }

            return success($"{tagName}.{UpperCaseFirstChar(propertyInfo.Name)}(\"{value}\")");
        }

        return (success: false, modifierCode: $"/* {tagName}.{name} = \"{value}\"*/");

        static string UpperCaseFirstChar(string str)
        {
            return char.ToUpper(str[0], new("en-US")) + str.Substring(1);
        }

        static (bool success, string[] parameters) tryParseViewBoxValues(string value)
        {
            var parameters = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (parameters.Length == 4)
            {
                return (true, parameters);
            }

            return default;
        }
    }

    static (bool success, string modifierCode) TryConvertToModifier_From_Mixin_Extension(string name, string value)
    {
        var success = (string modifierCode) => (true, modifierCode);

        value ??= string.Empty;

        if (name == "target" && value == "_blank")
        {
            return success("TargetBlank");
        }

        if (name.Equals("Width", StringComparison.OrdinalIgnoreCase) && value == "100%")
        {
            return success("WidthFull");
        }

        if (name.Equals("Height", StringComparison.OrdinalIgnoreCase) && value == "100%")
        {
            return success("HeightFull");
        }

        if (name.Equals("boxShadow", StringComparison.OrdinalIgnoreCase))
        {
            var parseResponse = TryParseBoxShadow(value);
            if (parseResponse.success)
            {
                return success($"BoxShadow({string.Join(", ", parseResponse.parameters)})");
            }

            return success($"{CamelCase(name)}(\"{value}\")");
        }

        if (name == "borderLeft" || name == "borderRight" || name == "borderTop" || name == "borderBottom")
        {
            var parameterList = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

            if (parameterList.Count == 3 && parameterList[0].EndsWithPixel())
            {
                return success($"{CamelCase(name)}({parameterList[0].RemovePixelFromEnd()}, {asParameter(parameterList[1])}, {asParameter(parameterList[2])})");

                static string asParameter(string parameter)
                {
                    if (parameter == "solid")
                    {
                        return "solid";
                    }

                    return '"' + parameter + '"';
                }
            }
        }

        if (IsMarkedAsAlreadyCalculatedModifier(value))
        {
            return success(UnMarkAsAlreadyCalculatedModifier(value));
        }

        if (name == nameof(Style.padding) || name == nameof(Style.margin) || name == nameof(Style.borderRadius))
        {
            var parameters = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

            if (parameters.TrueForAll(IsEndsWithPixel))
            {
                var methodName = CamelCase(name);

                var joinAllParameters = () => { return $"{methodName}({string.Join(", ", parameters.Select(x => x.RemovePixelFromEnd()))})"; };

                switch (parameters.Count)
                {
                    // 5px 5px
                    case 2 when parameters[0] == parameters[1]:
                        return success($"{methodName}({parameters[0].RemovePixelFromEnd()})");

                    // 5px 5px 5px 5px
                    case 4 when parameters[0] == parameters[1] &&
                                parameters[0] == parameters[2] &&
                                parameters[0] == parameters[3]:
                        return success($"{methodName}({parameters[0].RemovePixelFromEnd()})");

                    // 5px 6px 5px 6px
                    case 4 when parameters[0] == parameters[2] &&
                                parameters[1] == parameters[3]:
                        return success($"{methodName}({parameters[0].RemovePixelFromEnd()}, {parameters[1].RemovePixelFromEnd()})");

                    // 5px 6px
                    // 5px 6px 8px
                    // 5px 6px 7px 8px

                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return success(joinAllParameters());
                }
            }
        }

        if (name == "flex")
        {
            var parameters = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            if (parameters.Count == 3 && parameters.TrueForAll(x => double.TryParse(x, out _)))
            {
                return success($"Flex({parameters[0]}, {parameters[1]}, {parameters[2]})");
            }

            if (parameters.Count == 3 && double.TryParse(parameters[0], out _) && double.TryParse(parameters[1], out _) && parameters[2] == "auto")
            {
                return success($"Flex({parameters[0]}, {parameters[1]}, {parameters[2]})");
            }
        }

        var modifierFullName = $"{CamelCase(name)}{CamelCase(value.RemovePixelFromEnd())}";

        if (typeof(Mixin).GetProperty(modifierFullName) is not null)
        {
            return success(modifierFullName);
        }

        if (typeof(Mixin).GetMethod(CamelCase(name), [typeof(string)]) is not null)
        {
            if (typeof(Mixin).GetMethod(CamelCase(name), [typeof(double)]) is not null &&
                value.EndsWithPixel() && value.IndexOf(' ') < 0)
            {
                return success($"{CamelCase(name)}({value.RemovePixelFromEnd()})");
            }

            if (value.StartsWith("rgb(", StringComparison.OrdinalIgnoreCase) &&
                value.EndsWith(")", StringComparison.OrdinalIgnoreCase))
            {
                return success($"{CamelCase(name)}({value})");
            }

            if (IsGlobalDeclaredStringVariable(value))
            {
                return success($"{CamelCase(name)}({value})");
            }

            if (typeof(Mixin).GetMethod(CamelCase(name), [typeof(int)]) is not null &&
                int.TryParse(value, out _))
            {
                return success($"{CamelCase(name)}({value})");
            }

            // todo: lineHeight fixme
            if (typeof(Mixin).GetMethod(CamelCase(name), [typeof(double)]) is not null &&
                double.TryParse(value, out _))
            {
                return success($"{CamelCase(name)}({value})");
            }

            return success($"{CamelCase(name)}(\"{value}\")");
        }

        return default;

        static (bool success, IReadOnlyList<string> parameters) TryParseBoxShadow(string boxShadow)
        {
            // sample: "0.1px 1px 2px rgba(28, 43, 61, 0.12)"

            if (boxShadow is null)
            {
                return default;
            }

            var index = boxShadow.IndexOf("rgba", StringComparison.OrdinalIgnoreCase);
            if (index <= 0)
            {
                index = boxShadow.IndexOf("rgb", StringComparison.OrdinalIgnoreCase);
            }

            if (index > 0)
            {
                var firstPart = boxShadow.Substring(0, index);

                var list = firstPart.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                if (list.TrueForAll(x => x.EndsWithPixel()))
                {
                    var parameters = list.Select(x => x.RemovePixelFromEnd()).ToList();
                    parameters.Add(boxShadow.Substring(index));
                    return (success: true, parameters);
                }
            }

            return default;
        }
    }

    static PropertyInfo TryFindProperty(string htmlTagName, string attributeName)
    {
        var propertyName = string.Join(string.Empty, attributeName.Split(":-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));

        return TryFindTypeOfHtmlTag(htmlTagName)?.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
    }

    static Type TryFindTypeOfHtmlTag(string htmlTagName)
    {
        return typeof(div).Assembly.GetType(nameof(ReactWithDotNet) + "." + htmlTagName, false, true);
    }

    sealed class ModifierCode
    {
        public string Code { get; private init; }
        public bool Success { get; private init; }

        public static ModifierCode From((bool success, string modifierCode) tuple)
        {
            return new() { Code = tuple.modifierCode, Success = tuple.success };
        }

        public static ModifierCode FromString(string code)
        {
            return new() { Code = code, Success = true };
        }

        public static bool IsFail(ModifierCode item)
        {
            return item.Success is false;
        }

        public static bool IsSuccess(ModifierCode item)
        {
            return item.Success;
        }

        public static implicit operator ModifierCode(string code)
        {
            return FromString(code);
        }
    }

    #region already calculated modifier

    static string MarkAsAlreadyCalculatedModifier(string modifierCode)
    {
        return "|" + modifierCode;
    }

    static string UnMarkAsAlreadyCalculatedModifier(string modifierCode)
    {
        return modifierCode.Substring(1);
    }

    static bool IsMarkedAsAlreadyCalculatedModifier(string modifierCode)
    {
        return modifierCode?.Length > 2 && modifierCode[0] == '|';
    }

    #endregion
}