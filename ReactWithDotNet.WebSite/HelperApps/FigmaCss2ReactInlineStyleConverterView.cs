using System.Globalization;
using ReactWithDotNet.Libraries.PrimeReact;
using ReactWithDotNet.Libraries.uiw.react_codemirror;

namespace ReactWithDotNet.WebSite.HelperApps;

class FigmaCss2ReactInlineStyleConverterModel
{
    public string FigmaCss { get; set; }
    public string ReactInlineStyle { get; set; }
    public string StatusMessage { get; set; }
}

class FigmaCss2ReactInlineStyleConverterView : ReactComponent<FigmaCss2ReactInlineStyleConverterModel>
{
    protected override Element render()
    {
        var cssEditor = new CodeMirror
        {
            extensions = { "css", "githubLight" },
            onChange = OnCssValueChanged,
            value = state.FigmaCss,
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

        var csharpEditor = new CodeMirror
        {
            extensions = { "java", "githubLight" },
            value = state.ReactInlineStyle,
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
            text = state.StatusMessage,
            style = { position = "fixed", zIndex = "5", bottom = "25px", right = "25px", display = state.StatusMessage is null ? "none" : "" }
        };

        return new FlexColumn
        {
            WidthHeightMaximized,
            Padding(10),

            PrimeReactCssLibs,

            new div(Text("Figma css to React inline style")) { style = { fontSize = "23px", padding = "20px", textAlign = "center" } },
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
        };
    }

    void ClearStatusMessage()
    {
        state.StatusMessage = null;
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
            state.ReactInlineStyle = string.Join("," + Environment.NewLine, splitToLines(figmaCssText).Select(processLine));

            Client.CopyToClipboard(state.ReactInlineStyle);

            state.StatusMessage = "Copied to clipboard.";

            Client.GotoMethod(2000, ClearStatusMessage);
        }
        catch (Exception exception)
        {
            state.StatusMessage = "Error occured: " + exception;
        }

        static IEnumerable<string> splitToLines(string figmaCssText)
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

            var array = line.Split(new[] { ':', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            if (array.Length >= 2)
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