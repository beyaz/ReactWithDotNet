using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;
using ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror;

namespace ReactWithDotNet.WebSite.HelperApps;

class CSharpPropertyMapperModel
{
    public int EditCount { get; set; }
    public string FigmaCss { get; set; }
    public string ReactInlineStyle { get; set; }
    public string StatusMessage { get; set; }
}

class CSharpPropertyMapperView : Component<CSharpPropertyMapperModel>
{
    protected override Task constructor()
    {
        state = new()
        {
            FigmaCss = @"

class AnyClass
{
  public string A {get; set;}
  public int B {get; set;}
  public double C {get; set;}
}
"
        };
        state.ReactInlineStyle = FigmaCssToReactInlineCss(state.FigmaCss);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var cssEditor = new CodeMirror
        {
            extensions = { "cpp", "githubLight" },
            onChange   = OnCssValueChanged,
            value      = state.FigmaCss,
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
            value      = state.ReactInlineStyle,
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
            SizeFull,
            Padding(10),

            PrimeReactCssLibs,
            new div(FontSize23, Padding(10), TextAlignCenter)
            {
                "C# code property mapper generator",
                (small)" ( paste any part of c# class text to left panel )"
            },
            new FlexRow(SizeFull, Height(400), BorderForPaper, BorderRadiusForPaper)
            {
                new FreeScrollBar
                {
                    style =
                    {
                        Height(400),
                        WidthMaximized
                    },
                    children = { cssEditor }
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

    static string FigmaCssToReactInlineCss(string figmaCssText)
    {
        return TextTransformer.Transform(figmaCssText);
    }

    Task ClearStatusMessage()
    {
        state.StatusMessage = null;
        
        return Task.CompletedTask;
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





