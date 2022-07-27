using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Components;

class FixedTopPanelContainer : ReactComponent
{
    public bool HasShadow { get; set; }
    
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

        if (HasShadow)
        {
            top.style.borderBottom = "";
            top.style.boxShadow    = "0 1px 2px hsla(0,0%,0%,0.05),0 1px 4px hsla(0,0%,0%,0.05),0 2px 8px hsla(0,0%,0%,0.05)";
        }
        
        return top;
    }
}