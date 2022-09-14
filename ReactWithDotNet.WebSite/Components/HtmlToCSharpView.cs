using System.Globalization;
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
        
        return Convert(document.DocumentNode.FirstChild);
    }

    static string ToString(HtmlAttribute htmlAttribute)
    {
        if (htmlAttribute.Name == "style")
        {
            var map = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(Style.ParseCss(htmlAttribute.Value),new JsonSerializerSettings{DefaultValueHandling = DefaultValueHandling.Ignore}));

            var value = String.Join("," + Environment.NewLine, map.Select(x => $"  {x.Key} = \"{x.Value}\""));
            
            return $"{htmlAttribute.Name} =" + Environment.NewLine+
                   "{" + Environment.NewLine +
                   value + Environment.NewLine +
            "}";
            
        }
        return $"{htmlAttribute.Name} = {htmlAttribute.Value}";
    }
    static string Convert(HtmlNode htmlNode)
    {
        var sb = new StringBuilder();
        

        sb.AppendLine($"new {htmlNode.Name}");
        sb.AppendLine("{");
        
        if (htmlNode.Attributes.Any())
        {
            sb.Append(string.Join(", ", htmlNode.Attributes.Select(ToString)));
        }
        
        if (htmlNode.ChildNodes.Count > 0)
        {
            foreach (var child in htmlNode.ChildNodes)
            {
                if (child == null || child.Name == "#text")
                {
                    continue;
                }
                sb.AppendLine(Convert(child));
            }
            
            sb.AppendLine();
            sb.AppendLine("}");

            return sb.ToString();
        }
        
        
        return sb.ToString();
    }
}