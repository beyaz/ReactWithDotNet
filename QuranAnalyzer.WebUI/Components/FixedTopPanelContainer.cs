
namespace QuranAnalyzer.WebUI.Components;

class FixedTopPanelContainerModel
{
    public double MainDivScrollY { get; set; }
}
class FixedTopPanelContainer : ReactComponent<FixedTopPanelContainerModel>
{

    public void ComponentDidMount()
    {
        ClientTask.ListenEvent(ApplicationEventName.MainContentDivScrollChanged, nameof(OnMainContentDivScrollChanged));
    }
    
    public void OnMainContentDivScrollChanged(double mainDivScrollY)
    {
        state.MainDivScrollY = mainDivScrollY;
    }

    public override Element render()
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
                            new SiteTitle("19 Sistemi Nedir")
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