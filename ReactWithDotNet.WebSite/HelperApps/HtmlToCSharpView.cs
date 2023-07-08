using System.Text;
using System.Threading.Tasks;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;
using ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror;
using ReactWithDotNet.ThirdPartyLibraries.UIW.ReactTextareaCodeEditor;

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

        Element csharpEditor = new CodeMirror
        {
            extensions = { "javascript", "githubLight" },
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

        csharpEditor = new CodeEditor
        {
            value    = state.CSharpCode,
            language = "csharp",
            style =
            {
                FontSize11,
                LineHeight16,
                BackgroundColorTransparent,
                FontFamily("ui-monospace,SFMono-Regular,SF Mono,Consolas,Liberation Mono,Menlo,monospace")
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
            new style
            {
                
                @"

.cm-editor {
  height: calc(100% - 10px);
}
.cm-theme-light {
  height: calc(100% - 2px);
  font-size: 12px;
}

.ͼ1.cm-editor.cm-focused {
    outline: none;
}

/* left-side-key */
.ͼ18{
    color: #c0bcc8;
    font-weight: bold;
}


/* string */
.ͼ1b{
    color: #f44336;
    font-weight: bold;
}
/* number */
.ͼ19 {
    color: #141413;
    font-weight: bold;
}
/* boolean */
.ͼ1g {
    color: #2c1aeb;
    font-weight: bold;
}
"
            },
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
                new Splitter
                {
                    WidthHeightMaximized,
                    Splitter.Modify(x=>x.gutterSize = 10),
                    new SplitterPanel
                    {
                        SplitterPanel.Modify(x=>x.size =20),
                        new FreeScrollBar
                        {
                            style =
                            {
                                Height(400),
                                WidthMaximized
                            },
                            children = { htmlEditor }
                        }
                        
                    },
                    new SplitterPanel
                    {
                        SplitterPanel.Modify(x=>x.size =20),
                        
                        new FreeScrollBar
                        {
                            Height(400),
                            WidthMaximized,
                            csharpEditor
                        }
                        
                    },
                    
                    new SplitterPanel
                    {
                        SplitterPanel.Modify(x=>x.size =60),
                        WidthMaximized,
                        new FreeScrollBar
                        {
                            Height(400),
                            WidthMaximized,
                            CreatePreview
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
            var renderBody = HtmlToReactWithDotNetCsharpCodeConverter.HtmlToCSharp(state.HtmlText);

            var sb = new StringBuilder();
            sb.AppendLine("using ReactWithDotNet;");
            sb.AppendLine("using static ReactWithDotNet.Mixin;");
            sb.AppendLine();
            sb.AppendLine("namespace Preview;");
            sb.AppendLine();
            sb.AppendLine("class SampleComponent: ReactComponent");
            sb.AppendLine("{");
            
            sb.AppendLine("  public static Element CreateNew(){ return new SampleComponent(); }");
            sb.AppendLine();
            sb.AppendLine("  protected override Element render()");
            sb.AppendLine("  {");
            sb.AppendLine("    return ");

            sb.AppendLine("      // s t a r t ");
            foreach (var line in renderBody.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                sb.AppendLine("      "+line);
            }
            
            sb.AppendLine("      // e n d");

            sb.AppendLine("    ;");
            
            sb.AppendLine("  }");
            
            
            sb.AppendLine("}");


            state.CSharpCode = sb.ToString();

            
        }
        catch (Exception exception)
        {
            state.StatusMessage = "Error occured: " + exception.Message;
        }
    }

    Element CreatePreview()
    {
        //return new textarea
        //{
        //    text = state.CSharpCode,
        //    style = { BorderNone, WidthHeightMaximized}
        //};
        
        
        if (state.CSharpCode?.Length  > 0)
        {
            return (ReactWithDotNet.ReactComponent)DynamicCode.Execute(state.CSharpCode, "Preview.SampleComponent", "CreateNew", new object[] { }).invocationOutput;
        }

        return null;
    }
}