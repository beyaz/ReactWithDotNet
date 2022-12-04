
namespace QuranAnalyzer.WebUI.Components;

class FixedTopPanelContainerModel
{
    public double MainDivScrollY { get; set; }
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

                    new SvgHamburgerIcon() + MarginRight(30)
                    
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