using System.Collections.Concurrent;
using System.Text;
using System.Web;
using ReactWithDotNet.ThirdPartyLibraries._react_split_;
using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;

namespace ReactWithDotNet.WebSite.HelperApps;

record HtmlToCSharpViewModel
{
    public string RenderPartOfCSharpCode { get; init; }
    public int EditCount { get; init; }
    public string HtmlText { get; init; }
    public string StatusMessage { get; init; }
    public string Utid { get; init; }
}

class HtmlToCSharpView : Component<HtmlToCSharpViewModel>
{
    static readonly ConcurrentDictionary<string, string> Utid_To_GeneratedCode_Cache = [];

    bool Preview => GetQuery("preview") == "true";

    string UtidParameter => GetQuery("utid");

    public Task Refresh()
    {
        return Task.CompletedTask;
    }

    protected override Task constructor()
    {
        if (Preview)
        {
            Client.ListenEvent("RefreshComponentPreview", Refresh);
            return Task.CompletedTask;
        }

        state = new()
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
",

            Utid = UtidParameter ?? Guid.NewGuid().ToString("N")
        };

        CalculateOutput();

        Client.HistoryReplaceState(null, null, Page.LiveEditor.Url + $"?utid={state.Utid}");

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        if (Preview)
        {
            return CreatePreview(UtidParameter);
        }

        var htmlEditor = new Editor
        {
            valueBind                = () => state.HtmlText,
            valueBindDebounceHandler = HtmlText_OnEditFinished,
            valueBindDebounceTimeout = 500,
            defaultLanguage          = "html",
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
            valueBind                = () => state.RenderPartOfCSharpCode,
            valueBindDebounceHandler = RenderPartOfCSharpCode_OnEditFinished,
            valueBindDebounceTimeout = 500,
            defaultLanguage          = "csharp",
            options =
            {
                renderLineHighlight = "none",
                fontFamily          = "consolas, 'IBM Plex Mono Medium', 'Courier New', monospace",
                fontSize            = 11,
                minimap             = new { enabled = false },
                lineNumbers         = "off",
                unicodeHighlight    = new { showExcludeOptions = false },
                readOnly            = false
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
            FontSize13,

            new FlexRow(FontSize23, Padding(10), WidthFull, JustifyContentSpaceBetween)
            {
                "Html to ReactWithDotNet",
                new FlexRow
                {
                    new b(FontWeight500)
                    {
                        "Usage Info"
                    },
                    ": Paste any html input panel then c# code will be genered automatically.",

                    FontSize12
                }
            },
            new FlexRow(SizeFull)
            {
                new TwoRowSplittedForm
                {
                    new FlexColumn(SizeFull, Gap(8))
                    {
                        new GroupBox
                        {
                            Title = "Html input",
                            children =
                            {
                                new FreeScrollBar
                                {
                                    htmlEditor
                                }
                            }
                        },

                        new GroupBox
                        {
                            Title = "c# output",
                            children =
                            {
                                new FreeScrollBar
                                {
                                    new FlexColumn(HeightFull)
                                    {
                                        csharpEditor
                                    }
                                }
                            }
                        }
                    },

                    new GroupBox
                    {
                        Title = "Preview",
                        children =
                        {
                            new FlexColumn(SizeFull, Padding(8))
                            {
                                new iframe
                                {
                                    id    = "g",
                                    src   = Page.LiveEditor.Url + $"?utid={state.Utid}&preview=true",
                                    style = { BorderNone, WidthFull, HeightFull },
                                    title = "Live Editor Preview"
                                }
                            }
                        },
                        style =
                        {
                            // paper
                            BackgroundImage("radial-gradient(#a5a8ed 0.5px, #f8f8f8 0.5px)"),
                            BackgroundSize("10px 10px")
                        }
                    }
                }
            },

            statusMessageEditor
        };
    }

    static Element CreatePreview(string utid)
    {
        if (utid is null)
        {
            return "Utid is null";
        }

        if (Utid_To_GeneratedCode_Cache.TryGetValue(utid, out var csharpCode))
        {
            if (string.IsNullOrWhiteSpace(csharpCode))
            {
                return "Empty CSharp Code";
            }

            var (isTypeFound, type, assemblyLoadContext, sourceCodeHasError, sourceCodeError) = DynamicCode.LoadAndFindType(new[] { csharpCode }, "Preview.SampleComponent");
            if (isTypeFound)
            {
                var instance = type.Assembly.CreateInstance("Preview.SampleComponent");

                return (ReactWithDotNet.Component)instance;
            }

            if (sourceCodeHasError)
            {
                return new pre(Color(Red300)) { sourceCodeError };
            }

            DynamicCode.TryClear(assemblyLoadContext);
        }

        return "Utid not found";
    }

    static string GetFullCSharpCodeByRenderPartOfCode(string renderPartOfCSharpCode)
    {
        var sb = new StringBuilder();
        sb.AppendLine("using ReactWithDotNet;");
        sb.AppendLine("using static ReactWithDotNet.Mixin;");
        sb.AppendLine();
        sb.AppendLine("namespace Preview;");
        sb.AppendLine();
        sb.AppendLine("class SampleComponent: Component");
        sb.AppendLine("{");

        sb.AppendLine("  protected override Element render()");
        sb.AppendLine("  {");
        sb.AppendLine("    return ");

        sb.AppendLine("      // s t a r t ");
        foreach (var line in renderPartOfCSharpCode.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            sb.AppendLine("      " + line);
        }

        sb.AppendLine("      // e n d");

        sb.AppendLine("    ;");

        sb.AppendLine("  }");

        sb.AppendLine("  protected override Element componentDidCatch(Exception exceptionOccurredInRender)");
        sb.AppendLine("  {");
        sb.AppendLine("    return new pre(Color(Red300))");
        sb.AppendLine("    {");
        sb.AppendLine("      exceptionOccurredInRender.ToString()");
        sb.AppendLine("    };");
        sb.AppendLine("  }");

        sb.AppendLine("}");

        return sb.ToString();
    }

    static void RefreshComponentPreview(Client client)
    {
        const string jsCode =
            """
            var frame = window.frames[0];
            if(frame)
            {
              var reactWithDotNet = frame.ReactWithDotNet;
              if(reactWithDotNet)
              {
                reactWithDotNet.DispatchEvent('RefreshComponentPreview', []);
              }
            }
            """;

        client.RunJavascript(jsCode);
    }

    void CalculateOutput()
    {
        try
        {
            var renderBody = HtmlToReactWithDotNetCsharpCodeConverter.HtmlToCSharp(state.HtmlText);

            state = state with { RenderPartOfCSharpCode = GetFullCSharpCodeByRenderPartOfCode(renderBody) };

            Utid_To_GeneratedCode_Cache[state.Utid] = state.RenderPartOfCSharpCode;

            RefreshComponentPreview(Client);
        }
        catch (Exception exception)
        {
            state = state with { StatusMessage = "Error occured: " + exception.Message };
        }
    }

    string GetQuery(string name)
    {
        var value = KeyForHttpContext[Context].Request.Query[name].FirstOrDefault();
        if (value != null)
        {
            return value;
        }

        var referer = KeyForHttpContext[Context].Request.Headers["Referer"];
        if (string.IsNullOrWhiteSpace(referer))
        {
            return null;
        }

        var nameValueCollection = HttpUtility.ParseQueryString(new Uri(referer).Query);

        return nameValueCollection[name];
    }

    Task HtmlText_OnEditFinished()
    {
        OnHtmlValueChanged(state.HtmlText);
        return Task.CompletedTask;
    }

    void OnHtmlValueChanged(string htmlText)
    {
        state = state with { EditCount = state.EditCount + 1 };

        state = state with { StatusMessage = null };

        state = state with { HtmlText = htmlText };

        if (string.IsNullOrWhiteSpace(htmlText))
        {
            state = state with { RenderPartOfCSharpCode = null };

            Utid_To_GeneratedCode_Cache[state.Utid] = null;

            return;
        }

        CalculateOutput();
    }

    Task RenderPartOfCSharpCode_OnEditFinished()
    {
        Utid_To_GeneratedCode_Cache[state.Utid] = state.RenderPartOfCSharpCode;

        RefreshComponentPreview(Client);

        return Task.CompletedTask;
    }

    class GroupBox : PureComponent
    {
        public string Title { get; init; }

        protected override Element render()
        {
            return new fieldset(SizeFull, Border(Solid(0.8, rgb(226, 232, 240))), BorderRadius(4))
            {
                new legend(MarginLeftRight(8), DisplayFlexRowCentered, Border(Solid(0.8, rgb(226, 232, 240))), BorderRadius(4))
                {
                    new label(FontWeight600, PaddingLeftRight(4))
                    {
                        Title
                    }
                },

                children
            };
        }
    }

    class TwoRowSplittedForm : PureComponent
    {
        protected override Element render()
        {
            return new FlexRow(SizeFull)
            {
                new style
                {
                    new("gutter",
                    [
                        BackgroundRepeatNoRepeat,
                        BackgroundPosition("50%")
                    ]),
                    new("gutter.gutter-horizontal",
                    [
                        BackgroundImage("url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAeCAYAAADkftS9AAAAIklEQVQoU2M4c+bMfxAGAgYYmwGrIIiDjrELjpo5aiZeMwF+yNnOs5KSvgAAAABJRU5ErkJggg==')"),
                        Cursor("col-resize")
                    ])
                },

                new Split
                {
                    sizes      = [40, 60],
                    gutterSize = 12,
                    style      = { WidthFull, DisplayFlexRow },

                    children =
                    {
                        children
                    }
                }
            };
        }
    }
}