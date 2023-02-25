using ReactWithDotNet.Libraries.mui.material;
using ReactWithDotNet.WebSite.Showcases;

namespace ReactWithDotNet.WebSite.Pages;

class PageShowcase : ReactComponent
{
    public string SearchValue { get; set; }
    
    protected override Element render()
    {
        return new FlexRow(WidthMaximized)
        {
            LeftMenu,
            
            new div(Padding(10),WidthMaximized)
            {
                new DemoPanel
                {
                    FullNameOfElement = typeof(MuiCardDemo).FullName,
                    CSharpCode        = "new MuiCardDemo()"
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
        
        return new FlexColumn(Padding(5))
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
            return new FlexRowCentered
            {
                Text(t.Name), 
                BorderRadius(6), 
                PaddingTopBottom(5), 
                PaddingLeftRight(15), 
                Border("1px solid red"),
                CursorDefault,
                Hover(Background(Theme[Context].grey_900))
            };
        }
    }

    static IReadOnlyList<Type> TypeListOfShowcaseElement = new[]
    {
        typeof(MuiCardDemo),
        typeof(MuiTextFieldDemo)
    };

    void OnSearchFinished()
    {
    }
}