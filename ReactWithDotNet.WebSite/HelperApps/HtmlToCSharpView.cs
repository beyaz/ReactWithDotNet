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
        var htmlEditor = new CodeEditor
        {
            value    = state.HtmlText,
            onChange = e=>OnHtmlValueChanged(e.target.value),
            language = "html",
            style =
            {
                HeightMaximized,
                FontSize11,
                LineHeight16,
                BackgroundColorTransparent,
                FontFamily("ui-monospace,SFMono-Regular,SF Mono,Consolas,Liberation Mono,Menlo,monospace")
            }
        };

        var csharpEditor = new CodeEditor
        {
            value    = state.CSharpCode,
            language = "csharp",
            style =
            {
                HeightMaximized,
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
            WidthHeightMaximized,
            Padding(10),

            new div(FontSize23, Padding(10), TextAlignCenter)
            {
                "Html to ReactWithDotNet",
                (small)" ( paste any html text to left panel )"
            },
            new FlexRow(WidthHeightMaximized, BorderForPaper, BorderRadiusForPaper)
            {
                new Splitter
                {
                    WidthHeightMaximized,
                    Splitter.Modify(x=>x.gutterSize = 10),
                    new SplitterPanel
                    {
                        SplitterPanel.Modify(x=>x.size =20),
                        new FreeScrollBar(WidthHeightMaximized)
                        {
                            htmlEditor
                        }
                        
                    },
                    new SplitterPanel
                    {
                        SplitterPanel.Modify(x=>x.size =20),
                        
                        new FreeScrollBar
                        {
                            
                            WidthHeightMaximized,
                            csharpEditor
                        }
                        
                    },
                    
                    new SplitterPanel
                    {
                        SplitterPanel.Modify(x=>x.size =60),
                        WidthMaximized,
                        new FreeScrollBar
                        {
                         
                            WidthHeightMaximized,
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
        if (state.CSharpCode?.Length  > 0)
        {
            return (ReactWithDotNet.ReactComponent)DynamicCode.Execute(state.CSharpCode, "Preview.SampleComponent", "CreateNew", new object[] { }).invocationOutput;
        }

        return null;
    }
}