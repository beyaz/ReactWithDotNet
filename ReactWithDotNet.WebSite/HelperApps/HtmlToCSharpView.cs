using System.Threading.Tasks;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;
using ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror;

namespace ReactWithDotNet.WebSite.HelperApps;

class HtmlToCSharpViewModel
{
    public string CSharpCode { get; set; }
    public int EditCount { get; set; }
    public string HtmlText { get; set; }
    public string StatusMessage { get; set; }
}

class HtmlToCSharpView : ReactComponent<HtmlToCSharpViewModel>
{
    protected override Task constructor()
    {
        state = new HtmlToCSharpViewModel
        {
            HtmlText = @"
<div>
  <button class='xyz' id='4t'>abc</button>
  <img src = 'abc.jpg' alt='abc' >
  <a href = '#'>abc</a>
  <p style='text-align: center;'> abc </p>
</div>
"
        };
        state.CSharpCode = HtmlToReactWithDotNetCsharpCodeConverter.HtmlToCSharp(state.HtmlText);

        return Task.CompletedTask;
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
            new FlexRow(WidthHeightMaximized, Height(400), BorderForPaper, BorderRadiusForPaper)
            {
                new FreeScrollBar
                {
                    style =
                    {
                        Height(400),
                        WidthMaximized
                    },
                    children = { htmlEditor }
                },
                new FreeScrollBar
                {
                    style =
                    {
                        Height(400),
                        WidthMaximized
                    },
                    children = { csharpEditor }
                }
            },

            statusMessageEditor
        };
    }

    void ClearStatusMessage()
    {
        state.StatusMessage = null;
    }

    void OnHtmlValueChanged(string htmlText)
    {
        state.EditCount++;

        state.StatusMessage = null;

        state.HtmlText = htmlText;

        if (string.IsNullOrWhiteSpace(htmlText))
        {
            state.CSharpCode = null;
            return;
        }

        try
        {
            state.CSharpCode = HtmlToReactWithDotNetCsharpCodeConverter.HtmlToCSharp(state.HtmlText);

            if (state.EditCount > 1)
            {
                Client.CopyToClipboard(state.CSharpCode);

                state.StatusMessage = "Copied to clipboard.";
                
                Client.GotoMethod(700, ClearStatusMessage);
            }
        }
        catch (Exception exception)
        {
            state.StatusMessage = "Error occured: " + exception;
        }
    }
}