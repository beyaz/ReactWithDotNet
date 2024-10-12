using ReactWithDotNet.ThirdPartyLibraries._react_split_;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;
using static ReactWithDotNet.WebSite.Components.LivePreview;

namespace ReactWithDotNet.WebSite.HelperApps;

record HtmlToCSharpViewModel
{
    public string RenderPartOfCSharpCode { get; init; }
    public int EditCount { get; init; }
    public string HtmlText { get; init; }
    public string StatusMessage { get; init; }
    public string Guid { get; init; }
}

class HtmlToCSharpView : Component<HtmlToCSharpViewModel>
{
    static ScriptManager Scripts => ScriptManager.Instance;

    public Task Refresh()
    {
        return Task.CompletedTask;
    }

    protected override Task constructor()
    {
        const string htmlText
            = """
              <div style="border-radius:6px;box-shadow:rgba(0, 0, 0, 0.03) 0px 4px 10px 0px, rgba(0, 0, 0, 0.06) 0px 0px 2px 0px, rgba(0, 0, 0, 0.12) 0px 2px 6px 0px;padding:24px;background-color:rgb(255, 255, 255);box-sizing:border-box;">
                  <div style="margin-bottom:32px;font-size:20px;font-weight:500;color:rgb(33, 33, 33);box-sizing:border-box;">Latest News</div>
                  <ul style="list-style:outside none none;margin:0px;padding:0px;box-sizing:border-box;">
                      <li style="border-bottom-width:1px;border-bottom-style:solid;padding-bottom:16px;border-color:rgb(223, 231, 239);box-sizing:border-box;">
                          <div style="margin-bottom:8px;font-weight:500;color:rgb(33, 33, 33);box-sizing:border-box;">Aenean euismod elementum</div>
                          <div style="max-width: 30rem;line-height:24px;color:rgb(117, 117, 117);box-sizing:border-box;">Vitae turpis massa sed elementum tempus egestas sed sed risus. In metus vulputate eu scelerisque felis imperdiet proin.</div>
                      </li>
                      <li style="border-bottom-width:1px;border-bottom-style:solid;padding-top:16px;padding-bottom:16px;border-color:rgb(223, 231, 239);box-sizing:border-box;">
                          <div style="margin-bottom:8px;font-weight:500;color:rgb(33, 33, 33);box-sizing:border-box;">In iaculis nunc sed augue lacus</div>
                          <div style="max-width: 30rem;line-height:24px;color:rgb(117, 117, 117);box-sizing:border-box;">Viverra vitae congue. Nisi scelerisque eu ultrices vitae auctor eu augue ut lectus. Elementum eu facilisis sed odio morbi.</div>
                      </li>
                      <li style="border-bottom-width:1px;border-bottom-style:solid;padding-top:16px;padding-bottom:16px;border-color:rgb(223, 231, 239);box-sizing:border-box;">
                          <div style="margin-bottom:8px;font-weight:500;color:rgb(33, 33, 33);box-sizing:border-box;">Proin sagittis nisl rhoncus</div>
                          <div style="max-width: 30rem;line-height:24px;color:rgb(117, 117, 117);box-sizing:border-box;">In pellentesque massa placerat duis ultricies lacus. Ac feugiat sed lectus vestibulum mattis ullamcorper.</div>
                      </li>
                  </ul>
                  <div style="padding-top:16px;justify-content:space-between;display:flex;box-sizing:border-box;"><button aria-label="Clear All" style="width: 50% !important;margin-right:8px;margin:0px 8px 0px 0px;display:flex;cursor:pointer;user-select:none;align-items:center;vertical-align:bottom;text-align:center;overflow:hidden;position:relative;box-sizing:border-box;background-color:rgba(0, 0, 0, 0);color:rgb(100, 116, 139);border:1px solid rgb(100, 116, 139);background:rgba(0, 0, 0, 0) none repeat scroll 0% 0% / auto padding-box border-box;padding:12px 20px;font-size:16px;transition:background-color 0.2s, color 0.2s, border-color 0.2s, box-shadow 0.2s;border-radius:6px;font-family:-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';font-weight:400;"><span style="flex: 1 1 auto;box-sizing:border-box;font-weight:700;transition-duration:0.2s;">Clear All</span><span role="presentation" style="height: 215.75px; width: 215.75px;width: 215.75px;display:block;position:absolute;background:rgba(255, 255, 255, 0.5) none repeat scroll 0% 0% / auto padding-box border-box;border-radius:100%;transform:matrix(0, 0, 0, 0, 0, 0);box-sizing:border-box;"></span></button><button aria-label="New Entry" style="width: 50% !important;margin-left:8px;margin:0px 0px 0px 8px;display:flex;cursor:pointer;user-select:none;align-items:center;vertical-align:bottom;text-align:center;overflow:hidden;position:relative;box-sizing:border-box;background-color:rgba(0, 0, 0, 0);color:rgb(99, 102, 241);border:1px solid rgb(99, 102, 241);background:rgba(0, 0, 0, 0) none repeat scroll 0% 0% / auto padding-box border-box;padding:12px 20px;font-size:16px;transition:background-color 0.2s, color 0.2s, border-color 0.2s, box-shadow 0.2s;border-radius:6px;font-family:-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';font-weight:400;"><span style="flex: 1 1 auto;box-sizing:border-box;font-weight:700;transition-duration:0.2s;">New Entry</span><span role="presentation" style="height: 215.75px; width: 215.75px;width: 215.75px;display:block;position:absolute;background:rgba(255, 255, 255, 0.5) none repeat scroll 0% 0% / auto padding-box border-box;border-radius:100%;transform:matrix(0, 0, 0, 0, 0, 0);box-sizing:border-box;"></span></button></div>
              </div>

              """;

        state = new()
        {
            HtmlText = htmlText,
            Guid     = Guid.NewGuid().ToString("N")
        };

        CalculateOutput();

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var htmlEditor = new CSharpCodeEditor
        {
            valueBind                = () => state.HtmlText,
            valueBindDebounceHandler = HtmlText_OnEditFinished
        };

        var csharpEditor = new CSharpCodeEditor
        {
            valueBind                = () => state.RenderPartOfCSharpCode,
            valueBindDebounceHandler = RenderPartOfCSharpCode_OnEditFinished
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
                new FlexRow(AlignItemsCenter, FontSize12)
                {
                    new b(FontWeight500)
                    {
                        "Usage Info"
                    },
                    ": Paste any html input panel then c# code will be genered automatically.",

                    SpaceX(4),
                    new PlayButton
                    {
                        Label    = "Play tutorial (2 min)",
                        VideoUrl = Asset("HtmlToCSharpView.mp4")
                    }
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
                                    id    = state.Guid,
                                    src   = Page.LivePreviewUrl(state.Guid),
                                    style = { BorderNone, SizeFull },
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

    void CalculateOutput()
    {
        try
        {
            state = state with
            {
                RenderPartOfCSharpCode = HtmlToReactWithDotNetCsharpCodeConverter.HtmlToCSharp(state.HtmlText)
            };

            Scripts[state.Guid] = new()
            {
                RenderPartOfCSharpCode = state.RenderPartOfCSharpCode
            };

            RefreshComponentPreview(Client, state.Guid);
        }
        catch (Exception exception)
        {
            state = state with { StatusMessage = "Error occured: " + exception.Message };
        }
    }

    Task HtmlText_OnEditFinished()
    {
        OnHtmlValueChanged(state.HtmlText);
        return Task.CompletedTask;
    }

    void OnHtmlValueChanged(string htmlText)
    {
        state = state with
        {
            EditCount = state.EditCount + 1,
            StatusMessage = null,
            HtmlText = htmlText
        };

        if (string.IsNullOrWhiteSpace(htmlText))
        {
            state = state with
            {
                RenderPartOfCSharpCode = null
            };

            Scripts[state.Guid] = null;

            return;
        }

        CalculateOutput();
    }

    Task RenderPartOfCSharpCode_OnEditFinished()
    {
        Scripts[state.Guid] = new()
        {
            RenderPartOfCSharpCode = state.RenderPartOfCSharpCode
        };

        RefreshComponentPreview(Client, state.Guid);

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