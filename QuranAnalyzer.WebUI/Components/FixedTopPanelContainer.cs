
namespace QuranAnalyzer.WebUI.Components;

class FixedTopPanelContainerModel
{
    public double MainDivScrollY { get; set; }
}
class FixedTopPanelContainer : ReactComponent<FixedTopPanelContainerModel>
{

    public FixedTopPanelContainer()
    {
        state = new FixedTopPanelContainerModel();
    }


    public void ComponentDidMount()
    {
        Context.ClientTask.ListenEvent("MainContentDivScrollChanged", nameof(OnMainContentDivScrollChanged));
        Context.ClientTask.CallJsFunction("RegisterScrollEvents");
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
            Children = children
        };

        if (state.MainDivScrollY > 0)
        {
            top.style.borderBottom = "";
            top.style.boxShadow  = "0px 0px 8px rgb(0 0 0 / 20%)";
        }
        
        return top;
    }
}