using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.WebSite.Showcases;

namespace ReactWithDotNet.WebSite.Pages;

class PageShowcase : ReactComponent
{
    public string SearchValue { get; set; }

    public string FullTypeNameOfSelectedSample { get; set; } = TypeListOfShowcaseElement[0].FullName;
    
    protected override Element render()
    {
        var boxShadowOfWindow = BoxShadow("0 2px 10px 2px rgb(0 0 0 / 10%)");
        
        return new FlexColumn(WidthMaximized, boxShadowOfWindow, BorderRadius(5))
        {
            new FlexRow{(h4)"Showcases"+ MarginLeft(30)+FontWeight500 + FontSize19, BorderBottom("1px solid rgba(5, 5, 5, 0.1)"), AlignItemsCenter },
            new FlexRow(WidthMaximized, Padding(10))
            {
                LeftMenu,

                new div(Padding(10),WidthMaximized)
                {
                    new DemoPanel
                    {
                        FullNameOfElement = FullTypeNameOfSelectedSample,
                        CSharpCode        = "new MuiCardDemo()"
                    }
                }


            }
        };
    }

    Element LeftMenu()
    {
        var menuItems = TypeListOfShowcaseElement;
        
        if (string.IsNullOrWhiteSpace(SearchValue) is false)
        {
            menuItems = menuItems.Where(t => t.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        
        return new FlexColumn(Padding(5), Gap(5))
        {
            new FlexRow(Gap(5),AlignItemsCenter)
            {
                new span(FontSize(30))
                {
                    className = "material-icons",
                    text      = "filter_list"
                },
                new TextField
                {
                    size="small",
                    valueBind                = ()=>SearchValue,
                    valueBindDebounceTimeout = 500,
                    valueBindDebounceHandler = OnSearchFinished
                }
            },
            new FlexColumn(Gap(5))
            {
                menuItems.Select(asMenuItem)
            }
        };

        Element asMenuItem(Type t)
        {
            var isSelected = FullTypeNameOfSelectedSample == t.FullName;
            
            return new FlexRowCentered
            {
                Id(t.FullName),
                Text(t.Name), 
                BorderRadius(6), 
                PaddingTopBottom(5), 
                PaddingLeftRight(15), 
                Border($"1px solid {Theme.grey_100}"),
                CursorDefault,
                When(isSelected,Background(Theme.grey_100)),
                When(!isSelected, Hover(Background(Theme.grey_100))),
                OnClick(e=>FullTypeNameOfSelectedSample = e.target.id)
            };
        }
    }

    static IReadOnlyList<Type> TypeListOfShowcaseElement = new[]
    {
        typeof(MuiCardDemo),
        typeof(MuiTextFieldDemo),
        typeof(PrimeReactTabViewDemo),
        typeof(RSuiteAutoCompleteDemo),
        typeof(EventBusDemo)
    };

    void OnSearchFinished()
    {
    }
}