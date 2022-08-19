using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Components;

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
            children = { topContent }
        };

        var backDrop = new div { className = "p-blockui p-component-overlay p-component-overlay-enter",style = { zIndex = "3"}};
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
                        IsBackDropActive ? backDrop : null,
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
                left         = "0px",
                marginTop    = "50px",
                marginBottom = "27px",

                width     = "100%",
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