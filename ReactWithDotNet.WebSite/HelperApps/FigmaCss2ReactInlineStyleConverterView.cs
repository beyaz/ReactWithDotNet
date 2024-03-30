using System.Globalization;
using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;

namespace ReactWithDotNet.WebSite.HelperApps;

class FigmaCss2ReactInlineStyleConverterModel
{
    public int EditCount { get; set; }
    public string FigmaCss { get; set; }
    public string ReactInlineStyle { get; set; }
}

class FigmaCss2ReactInlineStyleConverterView : Component<FigmaCss2ReactInlineStyleConverterModel>
{
    protected override Task constructor()
    {
        state = new()
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

        Client.ListenEventThenOnlyUpdateState<CopyClick>(OnCopyClicked);
        
        
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
                lineNumbers         = "off",
                unicodeHighlight    = new { showExcludeOptions = false }
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
                lineNumbers         = "off",
                unicodeHighlight    = new { showExcludeOptions = false }
            }
        };

        
        return new FlexColumn
        {
            SizeFull,
            Padding(10),

            new div(FontSize23, Padding(10), TextAlignCenter)
            {
                "Figma css to React inline style",
                (small)" ( paste any figma css text to left panel )"
            },
            new FlexRow(SizeFull, FlexGrow(1), BorderForPaper, BorderRadiusForPaper)
            {
                new FreeScrollBar
                {
                    style =
                    {
                        WidthFull
                    },
                    children = { cssEditor }
                },
                new FreeScrollBar
                {
                    style =
                    {
                        WidthFull
                    },
                    children = { csharpEditor }
                }
            },

            CopyButton()
        };
    }

    static Element CopyButton()
    {
        var showCopyButton = true;
        
        return FC(cmp =>
        {
            var content = new CopySvg() + OnClick(onCopyClicked);

            if (showCopyButton is false)
            {
                content = new CheckIcon();
            }
            return new FlexRowCentered
            {
                content,
                new[]
                {
                    PositionFixed,
                    Zindex2,
                    BottomRight(25)
                }
            };
            
            Task onCopyClicked(MouseEvent e)
            {
                showCopyButton = false;
                
                cmp.Client.DispatchEvent<CopyClick>();
               
                cmp.Client.GotoMethod(500,clearState);
        
                return Task.CompletedTask;
            }
            
            Task clearState()
            {
                showCopyButton = true;
        
                return Task.CompletedTask;
            }
        });

    }
    
    Task OnCopyClicked()
    {
        Client.CopyToClipboard(state.ReactInlineStyle);
        
        return Task.CompletedTask;
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

    delegate Task CopyClick();

    Task OnCssValueChanged(string figmaCssText)
    {
        state.EditCount++;

        

        state.FigmaCss = figmaCssText;

        if (string.IsNullOrWhiteSpace(figmaCssText))
        {
            state.ReactInlineStyle = null;
            return Task.CompletedTask;
        }

        state.ReactInlineStyle = FigmaCssToReactInlineCss(figmaCssText);
        
        return Task.CompletedTask;
    }

    class CopySvg : PureComponent
    {
        protected override Element render()
        {
            return new svg(ViewBox(0, 0, 24, 24), svg.Size(64))
            {
                new path
                {
                    d = "M0 6.75C0 5.784.784 5 1.75 5h1.5a.75.75 0 0 1 0 1.5h-1.5a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-1.5a.75.75 0 0 1 1.5 0v1.5A1.75 1.75 0 0 1 9.25 16h-7.5A1.75 1.75 0 0 1 0 14.25Z" ,
                    
                    fill = Gray700
                },
                new path
                {
                    d = "M5 1.75C5 .784 5.784 0 6.75 0h7.5C15.216 0 16 .784 16 1.75v7.5A1.75 1.75 0 0 1 14.25 11h-7.5A1.75 1.75 0 0 1 5 9.25Zm1.75-.25a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-7.5a.25.25 0 0 0-.25-.25Z",
                    
                    fill = Gray700
                }
            };
        }
    }
    
    class CheckIcon : PureComponent
    {
        protected override Element render()
        {
            return new svg(ViewBox(0, 0, 24, 24), svg.Size(64), Fill(none))
            {
                new path { d = "m6 13 4 4 8-10", stroke =Gray700, strokeWidth = 2, strokeLinecap = "round", strokeLinejoin = "round" },
              
            };
        }
    }
}