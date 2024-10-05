using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Components;

sealed class DemoPanel : Component<DemoPanel.State>
{
    public required string CSharpCode { get; init; }

    public required string FullNameOfElement { get; init; }

    protected override Task OverrideStateFromPropsBeforeRender()
    {
        if (FullNameOfElement != state.FullNameOfElement)
        {
            state = new()
            {
                FullNameOfElement = FullNameOfElement
            };
        }
        
        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new FlexColumn(WidthFull, Padding(8), Gap(8), BorderRadius(4), BoxShadow(0, 2, 5, 0, rgba(0, 0, 0, 0.34)))
        {
            new FlexRowCentered(BackgroundColor(Gray200), Padding(40), WidthFull, BorderRadius(8), PositionRelative, MinWidth(250))
            {
                creatElement,

                new FlexRow(PositionAbsolute, RightBottom(1))
                {
                    ShowHideButton
                }
            },
            state.IsSourceCodeVisible is false ? null : new FlexRow(WidthFull, OverflowAuto, Height(300), MarginTop(-8))
            {
                new SourceCodeView { CSharpCode = CSharpCode }
            }
        };

        Element creatElement()
        {
            return new iframe
            {
                src = Page.DemoPreviewUrl(FullNameOfElement),
                style = { BorderNone, SizeFull  }
            };
        }

        Element ShowHideButton()
        {
            return new FormControlLabel
            {
                label = ((span)(state.IsSourceCodeVisible ? "Hide Source Code" : "Show Source Code")) + FontSize12,
                control = new Switch
                {
                    size     = "small",
                    value    = (!state.IsSourceCodeVisible).ToString(),
                    @checked = state.IsSourceCodeVisible,
                    onChange = _ =>
                    {
                        state = state with { IsSourceCodeVisible = !state.IsSourceCodeVisible };

                        return Task.CompletedTask;
                    }
                }
            };
        }
    }

    sealed class SourceCodeView : PureComponent
    {
        public string CSharpCode { get; init; }

        protected override Element render()
        {
            return new fieldset(Border(1, solid, Gray200), SizeFull, OverflowScroll)
            {
                new legend(DisplayFlexColumnCentered, MarginLeft(8), MarginBottom(-8))
                {
                    new img { Src(Asset("csharp.svg")), Size(20), PaddingX(4), Zindex2, Height(16) }
                },

                new FlexRowCentered(SizeFull, Padding(0, 4, 4, 4))
                {
                    new CSharpCodePanel { Code = CSharpCode }
                }
            };
        }
    }

    internal sealed record State
    {
        public bool IsSourceCodeVisible { get; init; }
        
        public string FullNameOfElement { get; init; }
    }
}