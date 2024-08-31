using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Components;

sealed class DemoPanel : Component<DemoPanel.State>
{
    public required string CSharpCode { get; init; }

    public required string FullNameOfElement { get; init; }

    protected override Element render()
    {
        return new FlexRow(WidthFull, FlexWrap, Padding(8), BorderRadius(4), BoxShadow(0, 2, 5, 0, rgba(0, 0, 0, 0.34)))
        {
            new FlexRowCentered(BackgroundColor(Gray200), Padding(40), WidthFull, BorderRadius(8), PositionRelative)
            {
                creatElement,

                new FlexRow(PositionAbsolute, RightBottom(1))
                {
                    ShowHideButton
                }
            },
            state.IsSourceCodeVisible ? new SourceCodeView { CSharpCode = CSharpCode } : null
        };

        Element creatElement()
        {
            if (FullNameOfElement is not null)
            {
                var elementType = Type.GetType(FullNameOfElement);
                if (elementType is not null)
                {
                    return (Element)Activator.CreateInstance(elementType);
                }
            }

            return "Element is empty";
        }

        Element ShowHideButton()
        {
            return new FormControlLabel
            {
                label = state.IsSourceCodeVisible ? "Hide Source Code" : "Show Source Code",
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
            return new fieldset(Border(1, solid, Gray200), SizeFull)
            {
                new legend(DisplayFlexColumnCentered, MarginLeft(8), MarginBottom(-8))
                {
                    new img { Src(Asset("csharp.svg")), Size(24), PaddingX(4), Zindex2 }
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
    }
}