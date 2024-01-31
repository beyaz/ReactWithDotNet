using System.Globalization;
using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;

namespace ReactWithDotNet.WebSite.HelperApps;

class FigmaCss2ReactInlineStyleConverterModel
{
    public int EditCount { get; set; }
    public string FigmaCss { get; set; }
    public string ReactInlineStyle { get; set; }
    public string StatusMessage { get; set; }
}

class FigmaCss2ReactInlineStyleConverterView : Component<FigmaCss2ReactInlineStyleConverterModel>
{
    protected override Task constructor()
    {
        state = new FigmaCss2ReactInlineStyleConverterModel
        {
            FigmaCss = @"
font-family: 'Open Sans';
font-style: normal;
font-weight: 400;
font-size: 16px;
line-height: 24px;
/* or 150% */


/* Neutral/N900 */

color: #4A4A49;
"
        };
        state.ReactInlineStyle = FigmaCssToReactInlineCss(state.FigmaCss);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var cssEditor =  new Editor
        {
            defaultLanguage          = "text",
            valueBind                = () => state.FigmaCss,
            valueBindDebounceTimeout = 300,
            valueBindDebounceHandler = OnKeypressFinished,
            options =
            {
                renderLineHighlight = "none",
                fontFamily          = "consolas, 'IBM Plex Mono Medium', 'Courier New', monospace",
                fontSize            = 11,
                minimap             = new { enabled = false },
                lineNumbers         = "off"
            }
        };

        var csharpEditor = new Editor
        {
            defaultLanguage          = "text",
            valueBind                = () => state.ReactInlineStyle,
            valueBindDebounceTimeout = 300,
            valueBindDebounceHandler = OnKeypressFinished,
            options =
            {
                renderLineHighlight = "none",
                fontFamily          = "consolas, 'IBM Plex Mono Medium', 'Courier New', monospace",
                fontSize            = 11,
                minimap             = new { enabled = false },
                lineNumbers         = "off"
            }
        };

        var statusMessageEditor = new FlexRowCentered
        {
            text     = state.StatusMessage,
            style    = { position = "fixed", zIndex = "5", bottom = "25px", right = "25px", display = state.StatusMessage is null ? "none" : "" }
        };

        return new FlexColumn
        {
            WidthHeightMaximized,
            Padding(10),

            new div(FontSize23, Padding(10), TextAlignCenter)
            {
                "Figma css to React inline style",
                (small)" ( paste any figma css text to left panel )"
            },
            new FlexRow(WidthHeightMaximized, FlexGrow(1), BorderForPaper, BorderRadiusForPaper)
            {
                new FreeScrollBar
                {
                    style =
                    {
                        WidthMaximized
                    },
                    children = { cssEditor }
                },
                new FreeScrollBar
                {
                    style =
                    {
                        WidthMaximized
                    },
                    children = { csharpEditor }
                }
            },

            statusMessageEditor
        };
    }

    Task OnKeypressFinished()
    {
        return OnCssValueChanged(state.FigmaCss);
    }

    static string FigmaCssToReactInlineCss(string figmaCssText)
    {
        return string.Join("," + Environment.NewLine, splitToLines(figmaCssText).Select(processLine));

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

    Task ClearStatusMessage()
    {
        stat
    }

    Task OnCssValueChanged(string figmaCssText)
    {
        state.EditCount++;

        state.StatusMessage = null;

        state.FigmaCss = figmaCssText;

        if (string.IsNullOrWhiteSpace(figmaCssText))
        {
            state.ReactInlineStyle = null;
            return Task.CompletedTask;
        }

        try
        {
            state.ReactInlineStyle = FigmaCssToReactInlineCss(figmaCssText);

            if (state.EditCount > 1)
            {
                Client.CopyToClipboard(state.ReactInlineStyle);
                state.StatusMessage = "Copied to clipboard.";
                Client.GotoMethod(700, ClearStatusMessage);
            }
        }
        catch (Exception exception)
        {
            state.StatusMessage = "Error occured: " + exception;
        }
        
        return Task.CompletedTask;
    }
}