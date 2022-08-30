
namespace QuranAnalyzer.WebUI.Components;

class BackdropModel
{
    
    
}

class BackdropView: ReactComponent<BackdropModel>
{
    public bool IsActive { get; set; }
    
    public override Element render()
    {
        return new div
        {
            className = "p-blockui p-component-overlay p-component-overlay-enter", 
            style = { zIndex = "3" , display = IsActive ? "": "none"},
            onClick = _=>ClientTask.DispatchEvent(ApplicationEventName.BackdropClicked,"")
        };
    }
}
class ApplicationLayout : ReactComponent
{
    public Element topContent;
    public Element mainContent;
    public Element menu;
    public double mainDivScrollY;

    public bool IsBackDropActive { get; set; }
   
    public override Element render()
    {
        var top = new FixedTopPanelContainer
        {
            topContent
        };

        var backDrop = new BackdropView { IsActive = IsBackDropActive };
        var main = new div
        {
            id = "main",
            children =
            {
                new div
                {
                    style = { display = "flex", justifyContent = "center", height = "100%"},
                    children =
                    {
                        backDrop,
                        new div
                        {
                            style    = { marginLeftRight = "10px", marginTop = "10px", maxWidth = "800px", width = "100%"},
                            children = { mainContent }
                        }
                    }
                }
            },
            
            style =
            {
                position     = "fixed",
                top          = "0px",
                left         = (IsBackDropActive ? "400px" : "0px"),
                marginTop    = "50px",
                marginBottom = "27px",

                width     = IsBackDropActive ? "calc(100% - 400px)" : "100%",
                height    = "calc(100% - 65px)",
                overflowY = "auto"
            }
        };
        
        return new div
        {
            children ={ top, menu, main }, 
            style = { width_height = "100%" }
        };
    }
}