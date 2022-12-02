
namespace QuranAnalyzer.WebUI.Components;

class FixedTopPanelContainerModel
{
    public double MainDivScrollY { get; set; }
}
class FixedTopPanelContainer : ReactComponent<FixedTopPanelContainerModel>
{

    protected override void componentDidMount()
    {
        Client.OnMainContentDivScrollChanged( mainDivScrollY => state.MainDivScrollY = mainDivScrollY);
    }

    protected override Element render()
    {
        var top = new div
        {
            style =
            {
                PositionFixed,
                Top(0),
                Width("100%"),
                Height(50),
                Zindex(1),
                BorderBottom("1px solid #dadce0"),
                Background("white")
            },
            children =
            {
                new nav(DisplayFlex, JustifyContentFlexStart, AlignItemsCenter)
                {
                    new SvgHamburgerIcon(),
                    new SiteTitle()
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