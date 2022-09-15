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
                fontSize   = "15px",
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
                fontSize   = "15px",
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
                new div("Figma css to React inline style") { style = { fontSize = "23px", padding = "20px", textAlign = "center" } },
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
                            size = 50,
                            children =
                            {
                                cssEditor
                            }
                        },
                        new SplitterPanel
                        {
                            size = 50,
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

            ClientTask.CallJsFunction(JsClient.CopyToClipboard, state.ReactInlineStyle);

            state.StatusMessage = "Copied to clipboard.";

            ClientTask.GotoMethod(2000, ClearStatusMessage);
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

        if (htmlAttribute.Name == "style")
        {
            var map = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(Style.ParseCss(htmlAttribute.Value), new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
            if (map != null)
            {
                // as one line
                if (map.Count <= 3)
                {
                    lines.Add($"{htmlAttribute.Name} = {{ {string.Join(", ", map.Select(x => $"{x.Key} = \"{x.Value}\""))} }}");
                    return lines;
                }

                // as multi line
                lines.AddRange(map.Select(x => $"{x.Key} = \"{x.Value}\""));

                for (var i = 0; i < lines.Count - 1; i++)
                {
                    lines[i] += ",";
                }

                lines.Insert(0, $"{htmlAttribute.Name} =");
                lines.Insert(1, "{");
                lines.Add("}");

                return lines;
            }

            return lines;
        }

        lines.Add($"{htmlAttribute.Name} = \"{htmlAttribute.Value}\"");

        return lines;
    }

    static IReadOnlyList<string> ToCSharpCode(HtmlAttributeCollection htmlAttributes)
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
        

        var lines = new List<string>();

        if (attributeLines.Count > 1 && attributeLines.Count <= 3)
        {
            lines.Add(string.Join(", ", attributeLines));

            return lines;
        }

        for (var i = 0; i < attributeLines.Count - 1; i++)
        {
            attributeLines[i] += ",";
        }

        return lines;
    }

    static IReadOnlyList<string> ToCSharpCode(HtmlNode htmlNode)
    {
        var lines = new List<string>();

        if (htmlNode.ChildNodes.Count(x=>x.Name != "#text") == 0)
        {
            var attributeLines = ToCSharpCode(htmlNode.Attributes);

            if (attributeLines.Count == 1)
            {
                // one line
                lines.Add($"new {htmlNode.Name} {{ {attributeLines[0]} }}");

                return lines;
            }

            lines.Add($"new {htmlNode.Name}()");

            return lines;
        }

        lines.Add($"new {htmlNode.Name}");
        lines.Add("{");

        lines.AddRange(ToCSharpCode(htmlNode.Attributes));

        foreach (var child in htmlNode.ChildNodes)
        {
            if (child == null || child.Name == "#text")
            {
                continue;
            }

            if (!lines[^1].EndsWith("{", StringComparison.OrdinalIgnoreCase))
            {
                if (!lines[^1].EndsWith(",", StringComparison.OrdinalIgnoreCase))
                {
                    lines[^1] += ",";
                }
            }

            lines.AddRange(ToCSharpCode(child));
        }

        lines.Add("}");

        return lines;
    }

    static string ToCSharpCode(IEnumerable<string> lines)
    {
        var sb = new StringBuilder();

        var padding = 0;

        foreach (var line in lines)
        {
            var paddingAsString = "".PadRight(padding, ' ');
            if (line == "{")
            {
                sb.AppendLine(paddingAsString + line);
                padding++;
                continue;
            }

            if (line == "}")
            {
                padding--;
                paddingAsString = "".PadRight(padding, ' ');
                sb.AppendLine(paddingAsString + line);
                
                continue;
            }

            sb.AppendLine(paddingAsString + line);
        }

        return sb.ToString();
    }
}