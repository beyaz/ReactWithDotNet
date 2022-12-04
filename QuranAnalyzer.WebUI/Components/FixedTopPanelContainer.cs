
using QuranAnalyzer.WebUI.Pages.MainPage;

namespace QuranAnalyzer.WebUI.Components;

class FixedTopPanelContainerModel
{
    public double MainDivScrollY { get; set; }

    public bool IsMenuVisible { get; set; }
}
class FixedTopPanelContainer : ReactComponent<FixedTopPanelContainerModel>
{

    protected override void componentDidMount()
    {
        Client.OnMainContentDivScrollChangedOverZero( mainDivScrollY => state.MainDivScrollY = mainDivScrollY);
    }

    protected override Element render()
    {
        var top = new FlexColumn(JustifyContentCenter)
        {
            style =
            {
                PositionSticky,
                Top(0),
                WidthMaximized,
                Height(50),
                Zindex(2),
                BorderBottom("1px solid #dadce0"),
                Background("white")
            },
            children =
            {
                new nav(DisplayFlex, FlexDirectionRow, JustifyContentSpaceBetween, AlignItemsCenter)
                {
                    new SiteTitle() + MarginLeft(30),

                    new div(PositionRelative)
                    {
                        DisplayNone, MediaQueryOnSmartphone(new Style{DisplayBlock}),
                        MarginRight(30),
                        OnClick(_=>state.IsMenuVisible = !state.IsMenuVisible),
                        new SvgHamburgerIcon()  + new Style
                        {
                            When(state.IsMenuVisible, DisplayNone),
                        },

                        new MenuCloseIcon()  + new Style
                        {
                            DisplayNone, When(state.IsMenuVisible, DisplayBlock)
                        },
                        
                        new div(PositionAbsolute)
                        {
                            DisplayNone, When(state.IsMenuVisible, DisplayBlock),
                            
                            Background("white"),
                            MarginLeft(-200), MarginTop(-10),
                            Zindex(3),
                            BoxShadow( "0px 0px 8px rgb(0 0 0 / 20%)"),
                            Padding(30),
                            BorderRadius(5),
                            new LeftMenu
                            {
                                SelectedPageId = Context.Query[QueryKey.Page]
                            }
                        }
                    }
                  
                    
                }


            }
        };

        if (state.MainDivScrollY > 0)
        {
            top.style.borderBottom = "";
            top.style.boxShadow  = "0px 0px 8px rgb(0 0 0 / 20%)";
        }
        
        return top;
    }
}