using System.Text;
using HtmlAgilityPack;
using ReactWithDotNet.Libraries.PrimeReact;
using ReactWithDotNet.Libraries.react_free_scrollbar;
using ReactWithDotNet.Libraries.uiw.react_codemirror;

namespace ReactWithDotNet.WebSite.HelperApps;

class HtmlToCSharpViewModel
{
    public string CSharpCode { get; set; }
    public string HtmlText { get; set; }
    public string StatusMessage { get; set; }
}

class HtmlToCSharpView : ReactComponent<HtmlToCSharpViewModel>
{
    protected override void constructor()
    {
        state = new HtmlToCSharpViewModel
        {
            HtmlText = "<button class='xyz' id='4t'>abc</button>",
        };
        state.CSharpCode = HtmlToCSharp(state.HtmlText);
    }

    protected override Element render()
    {
        var htmlEditor = new CodeMirror
        {
            extensions = { "html", "githubLight" },
            onChange   = OnHtmlValueChanged,
            value      = state.HtmlText,
            basicSetup =
            {
                highlightActiveLine       = false,
                highlightActiveLineGutter = false,
            },
            style =
            {
                HeightMaximized,
                MinHeight(200),
                BorderRadius(3),
                Border("1px solid #d9d9d9"),
                FontSize11,
                FontFamily("Consolas"),
                MaxWidth(500)
            }
        };

        var csharpEditor = new CodeMirror
        {
            extensions = { "java", "githubLight" },
            value      = state.CSharpCode,
            basicSetup =
            {
                highlightActiveLine       = false,
                highlightActiveLineGutter = false,
            },
            style =
            {
                HeightMaximized,
                MinHeight(200),
                BorderRadius(3),
                Border("1px solid #d9d9d9"),
                FontSize11,
                FontFamily("Consolas")
            }
        };

        var statusMessageEditor = new Message
        {
            severity = "success",
            text     = state.StatusMessage,
            style    = { position = "fixed", zIndex = "5", bottom = "25px", right = "25px", display = state.StatusMessage is null ? "none" : "" }
        };

        return new FlexColumn
        {
            WidthHeightMaximized,
            Padding(10),

            PrimeReactCssLibs,

            new div(FontSize23, Padding(10), TextAlignCenter)
            {
                "Html to ReactWithDotNet",
                (small)" ( paste any html text to left panel )"
            },
            new FlexRow(WidthHeightMaximized, Height(500), Border("1px solid red"))
            {
                new FreeScrollBar
                {
                    style =
                    {
                        Height(500),
                        WidthMaximized
                    },
                    children = { htmlEditor }
                },
                new FreeScrollBar
                {
                    style =
                    {
                        Height(500),
                        WidthMaximized
                    },
                    children = { csharpEditor }
                }
            },

            statusMessageEditor
        };
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

    static string HtmlToCSharp(string htmlRootNode)
    {
        if (string.IsNullOrWhiteSpace(htmlRootNode))
        {
            return null;
        }

        var document = new HtmlDocument();

        document.LoadHtml(htmlRootNode.Trim());

        return ToCSharpCode(ToCSharpCode(document.DocumentNode.FirstChild));
    }

    static List<string> ToCSharpCode(HtmlAttribute htmlAttribute)
    {
        var lines = new List<string>();

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
            return lines;
        }

        if (attributeName == "color" || attributeName == "width")
        {
            if (htmlAttribute.OwnerNode.Name == "div")
            {
                return lines;
            }
        }

        if (attributeName == "style" && !string.IsNullOrWhiteSpace(htmlAttribute.Value))
        {
            var map = Style.ParseCss(htmlAttribute.Value).ToDictionary();
            if (map.Count > 0)
            {
                // as one line
                if (map.Count <= 3)
                {
                    lines.Add($"{attributeName} = {{ {string.Join(", ", map.Select(x => $"{x.Key} = \"{x.Value}\""))} }}");
                    return lines;
                }

                // as multi line
                lines.AddRange(map.Select(x => $"{x.Key} = \"{x.Value}\""));

                for (var i = 0; i < lines.Count - 1; i++)
                {
                    lines[i] += ",";
                }

                lines.Insert(0, $"{attributeName} =");
                lines.Insert(1, "{");
                lines.Add("}");

                return lines;
            }

            return lines;
        }

        lines.Add($"{attributeName} = \"{htmlAttribute.Value}\"");

        return lines;
    }

    static List<string> ToCSharpCode(HtmlAttributeCollection htmlAttributes)
    {
        var attributeLines = new List<string>();

        if (htmlAttributes.Any())
        {
            attributeLines.AddRange(htmlAttributes.Select(ToCSharpCode).Aggregate((a, b) =>
            {
                a.AddRange(b);
                return a;
            }));
        }

        if (attributeLines.Count > 0 && attributeLines.Count <= 3)
        {
            return new List<string>
            {
                string.Join(", ", attributeLines)
            };
        }

        return attributeLines;
    }

    static IReadOnlyList<string> ToCSharpCode(HtmlNode htmlNode)
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

        if (htmlNode.ChildNodes.Count == 1 && htmlNode.ChildNodes[0].Name == "#text")
        {
            if (htmlNode.Attributes.Count == 0)
            {
                return new List<string> { $"({htmlNodeName})" + ConvertToCSharpString(htmlNode.ChildNodes[0].InnerText) };
            }

            var attributeLines = ToCSharpCode(htmlNode.Attributes);

            attributeLines.Insert(0, $"text = {ConvertToCSharpString(htmlNode.ChildNodes[0].InnerText)}");

            // one line
            if (attributeLines.Count < 3)
            {
                return new List<string>
                {
                    // one line
                    $"new {htmlNodeName} {{ {string.Join(", ", attributeLines)} }}"
                };
            }

            // multi line
            {
                var lines = new List<string>
                {
                    $"new {htmlNodeName}",
                    "{"
                };
                lines.AddRange(attributeLines);
                lines.Add("}");

                return lines;
            }
        }

        // check can be written in one line
        {
            if (htmlNode.ChildNodes.Count == 0)
            {
                var attributeLines = ToCSharpCode(htmlNode.Attributes);
                if (attributeLines.Count > 0 && attributeLines.Count < 3)
                {
                    return new List<string>
                    {
                        // one line
                        $"new {htmlNodeName} {{ {string.Join(", ", attributeLines)} }}"
                    };
                }

                if (attributeLines.Count == 0)
                {
                    return new List<string>
                    {
                        // one line
                        $"new {htmlNodeName}()"
                    };
                }
            }
        }

        // multi line
        {
            var lines = new List<string>
            {
                $"new {htmlNodeName}",
                "{"
            };

            var attributes = ToCSharpCode(htmlNode.Attributes);
            foreach (var attribute in attributes)
            {
                if (attribute[^1] == '=' || attribute[^1] == '{' || attribute[^1] == ',')
                {
                    lines.Add(attribute);
                    continue;
                }

                lines.Add(attribute + ",");
            }

            var children = new List<IReadOnlyList<string>>();

            foreach (var child in htmlNode.ChildNodes)
            {
                children.Add(ToCSharpCode(child));
            }

            // remove empty childs
            children.RemoveAll(x => x.Count == 0);

            if (children.Count > 0)
            {
                var openChildren = attributes.Count > 0;

                if (openChildren)
                {
                    lines.Add("children =");
                    lines.Add("{");
                }

                if (htmlNode.InnerText.Contains('\n'))
                {
                    foreach (var child in children)
                    {
                        lines.AddRange(child);

                        lines[^1] += ",";
                    }

                    // remove ,
                    lines[^1] = lines[^1].Remove(lines[^1].Length - 1);
                }
                else
                {
                    lines.Add(children.Aggregate(new List<string>(), (list, child) =>
                    {
                        list.Add(string.Join(", ", child));
                        return list;
                    }, list => string.Join(", ", list)));
                }

                if (openChildren)
                {
                    lines.Add("}");
                }
            }

            if (lines[^1].EndsWith(",", StringComparison.OrdinalIgnoreCase))
            {
                lines[^1] = lines[^1].Remove(lines[^1].Length - 1);
            }

            lines.Add("}");

            return lines;
        }
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

    void ClearStatusMessage()
    {
        state.StatusMessage = null;
    }

    void OnHtmlValueChanged(string htmlText)
    {
        state.StatusMessage = null;

        state.HtmlText = htmlText;

        if (string.IsNullOrWhiteSpace(htmlText))
        {
            state.CSharpCode = null;
            return;
        }

        try
        {
            state.CSharpCode = HtmlToCSharp(state.HtmlText);

            Client.CopyToClipboard(state.CSharpCode);

            state.StatusMessage = "Copied to clipboard.";

            Client.GotoMethod(2000, ClearStatusMessage);
        }
        catch (Exception exception)
        {
            state.StatusMessage = "Error occured: " + exception;
        }
    }
}