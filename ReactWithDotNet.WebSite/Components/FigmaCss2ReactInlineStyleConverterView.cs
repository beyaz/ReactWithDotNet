using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using ReactWithDotNet.PrimeReact;
using ReactWithDotNet.react_simple_code_editor;
using static Newtonsoft.Json.JsonConvert;

namespace ReactWithDotNet.WebSite.Components;

class FigmaCss2ReactInlineStyleConverterModel
{
    public string FigmaCss { get; set; }
    public string ReactInlineStyle { get; set; }
}
class FigmaCss2ReactInlineStyleConverterView : ReactComponent<FigmaCss2ReactInlineStyleConverterModel>
{
    protected override Element render()
    {
        return new div
        {
            style = { width_height = "100%", padding = "10px", border = "1px solid pink"},
            children =
            {
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
                                new Editor
                                {
                                    onValueChange = OnValueChange,
                                    value = state.FigmaCss,
                                    highlight = "css",
                                    style =
                                    {
                                        height    = "100%",
                                        minHeight = "200px", fontSize = "16px", fontFamily = "ui-monospace,SFMono-Regular,SF Mono,Menlo,Consolas,Liberation Mono,monospace"
                                    }
                                }
                            }
                        },
                        new SplitterPanel
                        {
                            size = 50,
                            children =
                            {
                                new Editor
                                {
                                    value = state.ReactInlineStyle,
                                    highlight = "csharp",
                                    style =
                                    {
                                        height    = "100%",
                                        minHeight = "200px",  fontSize = "16px", fontFamily = "ui-monospace,SFMono-Regular,SF Mono,Menlo,Consolas,Liberation Mono,monospace"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    void OnValueChange(string figmaCssText)
    {
        state.FigmaCss = figmaCssText;

        if (string.IsNullOrWhiteSpace(figmaCssText))
        {
            state.ReactInlineStyle = null;
            return;
        }

        state.ReactInlineStyle = string.Join("," + Environment.NewLine, splitToLines().Select(processLine));
        
        ClientTask.CallJsFunction(JsClient.CopyToClipboard, state.ReactInlineStyle);
        
        IEnumerable<string> splitToLines()
        {
            return figmaCssText.Trim().Split('\n').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x));
        }
        
        static string processLine(string line)
        {
            line = line.Trim();

            if (line.StartsWith("/*"))
            {
                return line;
            }

            var array = line.Split(new []{ ':', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x=>x.Trim()).ToArray();
            if (array.Length == 2)
            {
                return $"{keyToPropertyName(array[0])} = \"{array[1]}\"";
            }

            return line;
        }

        static string keyToPropertyName(string key)
        {
            var names = key.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            if (names.Length == 2)
            {
                return names[0] + char.ToUpper(names[1][0], new CultureInfo("en-US")) + names[1].Substring(1);
            }

            return key;
        }
    }
}