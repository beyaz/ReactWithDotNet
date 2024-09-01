using System.IO;
using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.WebSite.Showcases;

namespace ReactWithDotNet.WebSite.Pages;

sealed class PageShowcase : Component<PageShowcase.State>
{
    static readonly IReadOnlyList<Type> TypeListOfShowcaseElement =
    [
        typeof(MuiCardDemo),
        typeof(MuiTextFieldDemo),
        typeof(PrimeReactTabViewDemo),
        typeof(RSuiteAutoCompleteDemo),
        typeof(SwiperGalleryDemo),
        typeof(ReactPlayerDemo),
        typeof(MonacoEditorDemo)
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
                            FullNameOfElement = state.FullTypeNameOfSelectedSample,
                            CSharpCode        = File.ReadAllText("Showcases\\" + state.FullTypeNameOfSelectedSample.Split('.').Last() + ".cs")
                        }
                    }
                }
            }
        };
    }

    Element LeftMenu()
    {
        var menuItems = TypeListOfShowcaseElement;

        if (string.IsNullOrWhiteSpace(state.SearchValue) is false)
        {
            menuItems = menuItems.Where(t => t.Name.Contains(state.SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
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

        Element asMenuItem(Type t)
        {
            var isSelected = state.FullTypeNameOfSelectedSample == t.FullName;

            return new FlexRowCentered
            {
                Id(t.FullName),
                Text(t.Name),
                BorderRadius(6),
                PaddingTopBottom(5),
                PaddingLeftRight(15),
                Border(1, solid, Gray200),
                CursorDefault,
                When(isSelected, Background(Gray100)),
                When(!isSelected, Hover(Background(Gray50))),
                OnClick(e =>
                {
                    state = state with { FullTypeNameOfSelectedSample = e.target.id };

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
        public string FullTypeNameOfSelectedSample { get; init; } = TypeListOfShowcaseElement[0].FullName;

        public string SearchValue { get; init; }
    }
}