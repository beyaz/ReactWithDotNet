using System.Text;
using HtmlAgilityPack;
using Newtonsoft.Json;
using ReactWithDotNet.PrimeReact;
using ReactWithDotNet.react_simple_code_editor;

namespace ReactWithDotNet.WebSite.Components;

class HtmlToCSharpViewModel
{
    public string FigmaCss { get; set; }
    public string ReactInlineStyle { get; set; }
    public string StatusMessage { get; set; }
}

class HtmlToCSharpView : ReactComponent<HtmlToCSharpViewModel>
{
    protected override Element render()
    {
        var cssEditor = new Editor
        {
            onValueChange = OnCssValueChanged,
            value         = state.FigmaCss,
            highlight     = "css",
            style =
            {
                height     = "100%",
                minHeight  = "200px",
                fontSize   = "11px",
                fontFamily = "Consolas"
            }
        };
        var csharpEditor = new Editor
        {
            value     = state.ReactInlineStyle,
            highlight = "csharp",
            
            style =
            {
                height     = "100%",
                minHeight  = "200px",
                fontSize   = "11px",
                fontFamily = "Consolas"
            }
        };

        var statusMessageEditor = new Message
        {
            severity = "success",
            text     = state.StatusMessage,
            style    = { position = "fixed", zIndex = "5", bottom = "25px", right = "25px", display = state.StatusMessage is null ? "none" : "" }
        };

        return new div
        {
            style = { width_height = "100%", padding = "10px", display = "flex", flexDirection = "column" },
            children =
            {
                new div(Text("Html to ReactWithDotNet")) { style = { fontSize = "23px", padding = "20px", textAlign = "center" } },
                new Splitter
                {
                    layout = SplitterLayoutType.horizontal,
                    style =
                    {
                        width  = "100%",
                        height = "100%"
                    },
                    children =
                    {
                        new SplitterPanel
                        {
                            size = 30,
                            children =
                            {
                                cssEditor
                            }
                        },
                        new SplitterPanel
                        {
                            size = 70,
                            children =
                            {
                                csharpEditor
                            }
                        }
                    }
                },

                statusMessageEditor
            }
        };
    }

    void OnCssValueChanged(string figmaCssText)
    {
        state.StatusMessage = null;

        state.FigmaCss = figmaCssText;

        if (string.IsNullOrWhiteSpace(figmaCssText))
        {
            state.ReactInlineStyle = null;
            return;
        }

        try
        {
            state.ReactInlineStyle = HtmlToCSharp(state.FigmaCss);

            Client.CopyToClipboard(state.ReactInlineStyle);

            state.StatusMessage = "Copied to clipboard.";

            Client.GotoMethod(2000, ClearStatusMessage);
        }
        catch (Exception exception)
        {
            state.StatusMessage = "Error occured: " + exception;
        }
    }

    void ClearStatusMessage()
    {
        state.StatusMessage = null;
    }

    static string HtmlToCSharp(string htmlRootNode)
    {
        if (string.IsNullOrWhiteSpace(htmlRootNode))
        {
            return null;
        }

        HtmlDocument document = new HtmlDocument();

        document.LoadHtml(htmlRootNode);

        return ToCSharpCode(ToCSharpCode(document.DocumentNode.FirstChild));
    }

    static List<string> ToCSharpCode(HtmlAttribute htmlAttribute)
    {
        var lines = new List<string>();

        var attributeName = htmlAttribute.Name;
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

        



        if (attributeName == "style")
        {
            var map = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(Style.ParseCss(htmlAttribute.Value), new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
            if (map?.Count > 0)
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
        var  attributeLines = new List<string>();
        
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
        var htmlNodeName = htmlNode.Name;
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

            return new List<string> { '"' + htmlNode.InnerText + '"' };

        }
        if (htmlNode.ChildNodes.Count == 1 && htmlNode.ChildNodes[0].Name == "#text")
        {
            var attributeLines = ToCSharpCode(htmlNode.Attributes);

            attributeLines.Insert(0, $"text = \"{htmlNode.ChildNodes[0].InnerText}\"");

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
                if (attribute[^1]=='=' || attribute[^1] == '{' || attribute[^1] == ',')
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
                lines.Add("children =");
                lines.Add("{");
                
                foreach (var child in children)
                {
                    lines.AddRange(child);
                    
                    lines[^1] += ",";
                }

                // remove ,
                lines[^1] = lines[^1].Remove(lines[^1].Length - 1);

                lines.Add("}");
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
            var paddingAsString = "".PadRight(padding*4, ' ');
            if (line == "{")
            {
                sb.AppendLine(paddingAsString + line);
                padding++;
                continue;
            }

            if (line == "}" || line == "},")
            {
                if (padding ==0)
                {
                    throw new InvalidOperationException("Padding is already zero.");
                }
                padding--;
                paddingAsString = "".PadRight(padding*4, ' ');
                sb.AppendLine(paddingAsString + line);
                
                continue;
            }

            sb.AppendLine(paddingAsString + line);
        }

        return sb.ToString();
    }
}