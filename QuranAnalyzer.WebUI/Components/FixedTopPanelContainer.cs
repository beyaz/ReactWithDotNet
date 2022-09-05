
namespace QuranAnalyzer.WebUI.Components;

class FixedTopPanelContainerModel
{
    public double MainDivScrollY { get; set; }
}
class FixedTopPanelContainer : ReactComponent<FixedTopPanelContainerModel>
{

    protected override void componentDidMount()
    {
        ClientTask.ListenEvent(ApplicationEventName.MainContentDivScrollChanged, mainDivScrollY => state.MainDivScrollY = mainDivScrollY);
    }

    protected override Element render()
    {
        var top = new div
        {
            style =
            {
                position = "fixed",
                top      = "0px",
                left     = "0px",

                width        = "100%",
                height       = "50px",
                zIndex       = "1",
                borderBottom = "1px solid #dadce0"
            },
            children =
            {
                new nav
                {
                    children =
                    {
                        new SvgHamburgerIcon(),
                        new div
                        {
                            new SiteTitle()
                        }
                    },
                    style =
                    {
                        display        = "flex",
                        justifyContent = "flex-start",
                        alignItems     = "center"
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