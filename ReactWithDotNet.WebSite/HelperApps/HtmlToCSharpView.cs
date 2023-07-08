using System.Text;
using System.Threading.Tasks;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;
using ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor;

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
<div style='width: 100%; height: 100%; border-left: 0.50px #DBDBDB solid; border-top: 0.50px #DBDBDB solid; border-right: 0.50px #DBDBDB solid; border-bottom: 0.50px #DBDBDB solid; justify-content: flex-start; align-items: flex-start; gap: 10px; display: inline-flex'>
    <div style='flex: 1 1 0; height: 433px; flex-direction: column; justify-content: flex-start; align-items: flex-start; gap: 15px; display: inline-flex'>
        <img style='width: 405px; height: 186px; background: linear-gradient(0deg, #C4C4C4 0%, #C4C4C4 100%)' src='https://cdn.gezbegen.com/wp-content/uploads/2016/03/maldivler.jpg' />
        <div style='align-self: stretch; height: 180px; padding-left: 16px; padding-right: 16px; flex-direction: column; justify-content: flex-start; align-items: flex-start; gap: 8px; display: flex'>
            <div style='align-self: stretch; height: 36px; justify-content: space-between; align-items: center; gap: 32px; display: inline-flex'>
                <div style='padding-left: 10px; padding-right: 10px; padding-top: 4px; padding-bottom: 4px; background: #F6F6F6; justify-content: flex-start; align-items: flex-start; gap: 10px; display: flex'>
                    <div style='color: #4A4A49; font-size: 14px; font-family: Open Sans; font-weight: 600; line-height: 24px; word-wrap: break-word'>Geziler</div>
                </div>
                <div style='padding: 10px; justify-content: flex-start; align-items: flex-start; gap: 10px; display: flex'>
                    <div style='color: #79797B; font-size: 12px; font-family: Open Sans; font-weight: 600; line-height: 16px; word-wrap: break-word'>5 dk okuma</div>
                </div>
            </div>
            <div style='align-self: stretch; color: #4A4A49; font-size: 16px; font-family: SF Pro Text; font-weight: 600; line-height: 24px; word-wrap: break-word'>Massa aenean tortor nunc egestas. At amet, risus facilisi sed.</div>
            <div style='align-self: stretch; height: 84px; color: #4A4A49; font-size: 14px; font-family: Open Sans; font-weight: 400; line-height: 20px; word-wrap: break-word'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Tempus tristique id leo scelerisque dui a, ultricies. Massa aenean tortor nunc egestas. At amet, risus.</div>
            <div style='align-self: stretch; color: #79797B; font-size: 12px; font-family: Open Sans; font-weight: 700; text-transform: uppercase; line-height: 16px; word-wrap: break-word'>Bugün</div>
        </div>
    </div>
</div>
"
        };

        OnHtmlValueChanged(state.HtmlText);

        return Task.CompletedTask;
    }

    void CSharpCode_OnEditFinished()
    {
        
    }
    
    void HtmlText_OnEditFinished()
    {
        OnHtmlValueChanged(state.HtmlText);
    }
    
    protected override Element render()
    {
        var htmlEditor = new Editor
        {
            valueBind                = ()=>state.HtmlText,
            valueBindDebounceHandler = HtmlText_OnEditFinished,
            valueBindDebounceTimeout = 500,
            highlight                = "html",
            style =
            {
                BorderNone,
                FontSize11,
                LineHeight16,
                BackgroundColorTransparent,
                FontFamily("ui-monospace,SFMono-Regular,SF Mono,Consolas,Liberation Mono,Menlo,monospace")
            }
        };

        

        var csharpEditor = new Editor
        {
            valueBind                = ()=>state.CSharpCode,
            valueBindDebounceHandler = CSharpCode_OnEditFinished,
            valueBindDebounceTimeout = 500,
            highlight                = "clike",
            style =
            {
                BorderNone,
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
            new style{ text = @"
.npm__react-simple-code-editor__textarea:focus{  outline: none; }

"},
            WidthHeightMaximized,
            Padding(10),
            

            new div(FontSize23, Padding(10), TextAlignCenter)
            {
                "Html to ReactWithDotNet",
                (small)" ( paste any html text to left panel )"
            },
            new FlexRow(WidthHeightMaximized, BorderForPaper, BorderRadiusForPaper)
            {
                new Splitter(WidthHeightMaximized, x=>x.gutterSize = 10)
                {
                    
                    new SplitterPanel
                    {
                        new Splitter(x=>x.layout="vertical", x=>x.gutterSize = 10)
                        {
                            new SplitterPanel
                            {
                                new FreeScrollBar(WidthHeightMaximized)
                                {
                                    htmlEditor
                                }
                        
                            },
                            new SplitterPanel
                            {
                                new FreeScrollBar(WidthHeightMaximized)
                                {
                                    csharpEditor
                                }
                        
                            }
                        }
                    }
                    
                    ,
                    
                    new SplitterPanel
                    {
                        new FreeScrollBar(WidthHeightMaximized)
                        {
                            // paper
                            BackgroundImage("radial-gradient(#a5a8ed 0.5px, #f8f8f8 0.5px)"),
                            BackgroundSize("10px 10px"),
                            
                            new FlexRowCentered(WidthHeightMaximized, Padding(15))
                            {
                                CreatePreview
                            }
                        }
                        
                        
                        
                    }
                }
            },

            statusMessageEditor
        };
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
            var (isTypeFound, type, assemblyLoadContext, sourceCodeHasError, sourceCodeError) = DynamicCode.LoadAndFindType(state.CSharpCode, "Preview.SampleComponent");
            if (isTypeFound)
            {
                var instance = type.Assembly.CreateInstance("Preview.SampleComponent");
                
                return (ReactWithDotNet.ReactComponent)instance;
            }

            if (sourceCodeHasError)
            {
                return sourceCodeError;
            }

            DynamicCode.TryClear(assemblyLoadContext);
        }

        return null;
    }
}