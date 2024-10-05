using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.WebSite.Showcases;

namespace ReactWithDotNet.WebSite.Pages;

record DemoInfo
{
    public Type TargetType { get; init; }
    public double Height { get; init; }
    public string Label { get; init; }
}

sealed class PageShowcase : Component<PageShowcase.State>
{
    protected override Task constructor()
    {
        state = new()
        {
            SelectedTypeFullName = DemoList[0].TargetType.FullName
        };
        
        return Task.CompletedTask;
    }

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
                            DemoInfo = DemoList.First(x=>x.TargetType.FullName == state.SelectedTypeFullName)
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
                new IconFilter { Size = 32 },
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
                    state = state with { SelectedTypeFullName  = e.target.id };

                    return Task.CompletedTask;
                })
            };
        }
    }

    Task OnSearchFinished()
    {
        return Task.CompletedTask;
    }

    internal record State
    {
        public string SelectedTypeFullName { get; init; }

        public string SearchValue { get; init; }
    }
}