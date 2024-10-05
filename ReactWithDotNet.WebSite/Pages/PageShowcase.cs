using System.IO;
using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.WebSite.Showcases;

namespace ReactWithDotNet.WebSite.Pages;

sealed class PageShowcase : Component<PageShowcase.State>
{
    static IReadOnlyList<DemoInfo> DemoList =>
    [
        new()
        {
            TargetType = typeof(MuiCardDemo),
            Height     = 400,
            Label      = "Mui Card"
        },
        new()
        {
            TargetType = typeof(MuiTextFieldDemo),
            Height     = 150,
            Label      = "Mui Text Field"
        },
        new()
        {
            TargetType = typeof(PrimeReactTabViewDemo),
            Height     = 300,
            Label      = "PrimeReact Tab"
        },
        new()
        {
            TargetType = typeof(RSuiteAutoCompleteDemo),
            Height     = 200,
            Label      = "RSuite AutoComplete"
        },
        new()
        {
            TargetType = typeof(SwiperGalleryDemo),
            Height     = 500,
            Label      = "Swiper Galery"
        },
        new()
        {
            TargetType = typeof(ReactPlayerDemo),
            Height     = 500,
            Label      = "React Player"
        },
        new()
        {
            TargetType = typeof(MonacoEditorDemo),
            Height     = 500,
            Label      = "Monaco Editor"
        }
    ];

    protected override Task constructor()
    {
        state = new()
        {
            SelectedTypeFullName = DemoList[0].TargetType.FullName
        };

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var boxShadowOfWindow = BoxShadow(0, 2, 10, 2, rgba(0, 0, 0, 0.1));

        return new CommonPageLayout
        {
            new FlexColumn(WidthFull, MinHeight(500), boxShadowOfWindow, BorderRadius(4))
            {
                new FlexRow(PaddingLeft(16), PaddingTopBottom(8))
                {
                    (h4)"Showcases",

                    FontWeight400,
                    FontSize15,
                    BorderBottom(1, solid, rgba(5, 5, 5, 0.1))
                },
                new FlexRow(WidthFull, Padding(10))
                {
                    LeftMenu,

                    new div(Padding(10), WidthFull, OverflowAuto)
                    {
                        new DemoPanel
                        {
                            DemoInfo = DemoList.First(x => x.TargetType.FullName == state.SelectedTypeFullName)
                        }
                    }
                }
            }
        };
    }

    Element LeftMenu()
    {
        var menuItems = DemoList;

        if (string.IsNullOrWhiteSpace(state.SearchValue) is false)
        {
            menuItems = menuItems.Where(t => t.Label.Contains(state.SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return new FlexColumn(Padding(8))
        {
            new FlexRow
            {
                new IconFilter { Size = 24 },
                SpaceX(8),
                new TextField
                {
                    size                     = "small",
                    valueBind                = () => state.SearchValue,
                    valueBindDebounceTimeout = 700,
                    valueBindDebounceHandler = OnSearchFinished,
                    style                    = { WidthFull }
                }
            },
            SpaceY(8),
            new FlexColumn(Gap(8))
            {
                menuItems.Select(asMenuItem)
            }
        };

        Element asMenuItem(DemoInfo demoInfo, int index)
        {
            var isSelected = state.SelectedTypeFullName == demoInfo.TargetType.FullName;

            return new FlexRowCentered
            {
                Id(demoInfo.TargetType.FullName),
                Text(demoInfo.Label),
                BorderRadius(6),
                PaddingTopBottom(5),
                PaddingLeftRight(15),
                Border(1, solid, Gray200),
                CursorDefault,
                When(isSelected, Background(Gray100)),
                When(!isSelected, Hover(Background(Gray50))),
                OnClick(e =>
                {
                    state = state with { SelectedTypeFullName = e.target.id };

                    return Task.CompletedTask;
                })
            };
        }
    }

    Task OnSearchFinished()
    {
        return Task.CompletedTask;
    }

    class DemoPanel : Component<DemoPanelState>
    {
        public required DemoInfo DemoInfo { get; init; }

        protected override Task OverrideStateFromPropsBeforeRender()
        {
            if (DemoInfo.TargetType != state.DemoInfo?.TargetType)
            {
                state = new()
                {
                    DemoInfo = DemoInfo
                };
            }

            return Task.CompletedTask;
        }

        protected override Element render()
        {
            return new FlexColumn(WidthFull, Padding(8), Gap(8), BorderRadius(4), BoxShadow(0, 2, 5, 0, rgba(0, 0, 0, 0.34)))
            {
                new FlexRowCentered(Height(DemoInfo.Height), BackgroundColor(Gray200), Padding(40), WidthFull, BorderRadius(8), PositionRelative, MinWidth(250))
                {
                    creatElement,

                    new FlexRow(PositionAbsolute, RightBottom(1))
                    {
                        ShowHideButton
                    }
                },
                state.IsSourceCodeVisible is false ? null : new FlexRow(WidthFull, OverflowAuto, Height(300), MarginTop(-8))
                {
                    new SourceCodeView { CSharpCode = File.ReadAllText("Showcases\\" + DemoInfo.TargetType.FullName?.Split('.').Last() + ".cs") }
                }
            };

            Element creatElement()
            {
                return new iframe
                {
                    src   = Page.DemoPreviewUrl(DemoInfo.TargetType.FullName),
                    style = { BorderNone, SizeFull, DisplayFlexRowCentered }
                };
            }

            Element ShowHideButton()
            {
                return new FormControlLabel
                {
                    label = (span)(state.IsSourceCodeVisible ? "Hide Source Code" : "Show Source Code") + FontSize12,
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
    }

    internal record State
    {
        public string SelectedTypeFullName { get; init; }

        public string SearchValue { get; init; }
    }

    record DemoPanelState
    {
        public bool IsSourceCodeVisible { get; init; }

        public DemoInfo DemoInfo { get; init; }
    }

    record DemoInfo
    {
        public Type TargetType { get; init; }
        public double Height { get; init; }
        public string Label { get; init; }
    }
}