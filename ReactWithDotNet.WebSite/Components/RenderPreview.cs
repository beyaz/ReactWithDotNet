using static ReactWithDotNet.WebSite.Components.LivePreview;

namespace ReactWithDotNet.WebSite.Components;

sealed class RenderPreview : Component<RenderPreview.Model>
{
    public string RenderPartOfCSharpCode { get; init; }

    static ScriptManager Scripts => ScriptManager.Instance;

    protected override Task constructor()
    {
        state = new()
        {
            RenderPartOfCSharpCode = RenderPartOfCSharpCode,

            Guid = Guid.NewGuid().ToString("N")
        };

        if (DesignMode && state.RenderPartOfCSharpCode is null)
        {
            state = state with
            {
                RenderPartOfCSharpCode =
                """
                new div(Size(100), Border(2, solid, Blue))
                {
                    "Hello world"
                }
                """
            };
        }

        Scripts[state.Guid] = new()
        {
            RenderPartOfCSharpCode = state.RenderPartOfCSharpCode
        };

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var width = Width(100 * percent) + SM(Width(50 * percent));

        var csharpEditor = new CSharpCodeEditor
        {
            valueBind                = () => state.RenderPartOfCSharpCode,
            valueBindDebounceHandler = RenderPartOfCSharpCode_OnEditFinished
        };

        return new FlexRow(SizeFull, BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), BorderRadius(3), CursorDefault, JustifyContentSpaceBetween, FlexWrap)
        {
            new FlexRowCentered(width, BorderRight(Solid(1, rgb(235, 236, 240))))
            {
                csharpEditor
            },

            new FlexRowCentered(width, Background(rgb(246, 247, 249)), MinSize(200))
            {
                new iframe
                {
                    id    = state.Guid,
                    src   = Page.LivePreviewUrl(state.Guid),
                    style = { BorderNone, SizeFull, Padding(8) }
                }
            }
        };
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

    internal record Model
    {
        public string RenderPartOfCSharpCode { get; init; }
        public string Guid { get; init; }
    }
}