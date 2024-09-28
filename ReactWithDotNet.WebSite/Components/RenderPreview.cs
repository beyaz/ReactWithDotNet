using static ReactWithDotNet.WebSite.Components.LivePreview;

namespace ReactWithDotNet.WebSite.Components;

sealed class RenderPreview : Component<RenderPreview.Model>
{
    public string RenderPartOfCSharpCode { get; init; }

    static ScriptManager Scripts => ScriptManager.Instance;

    Func<StyleModifier[], StyleModifier> Break { get; } = SM;

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
        var width = Width(1, 1) + Break([Width(1, 2)]);
        var height = Height(1, 2) + Break([Height(1, 1)]);

        return new FlexColumn(SizeFull, BoxShadow(0, 2, 5, 0, rgba(0, 0, 0, .34)), BorderRadius(3), CursorDefault, MinSize(200))
        {
            Break([DisplayFlexRow]),
            new FlexRowCentered(width, height, BorderRight(Solid(1, rgb(235, 236, 240))))
            {
                new CSharpCodeEditor
                {
                    valueBind                = () => state.RenderPartOfCSharpCode,
                    valueBindDebounceHandler = RenderPartOfCSharpCode_OnEditFinished
                }
            },

            new FlexRowCentered(width, height, Background(rgb(246, 247, 249)))
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