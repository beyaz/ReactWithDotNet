using QuranAnalyzer.WebUI.Pages.MainPage;

namespace QuranAnalyzer.WebUI.Components;

class FixedLeftMenuContainer : ReactComponent
{
    public bool IsOpen { get; set; }

    protected override Element render()
    {
        return new div
        {
            children = { new LeftMenuContent_old() },
            style =
            {
                position   = "fixed",
                height     = "calc(100% - 50px)",
                width      =  IsOpen ? "70%" : "0px",
                maxWidth   = "400px",
                top        = "50px",
                background = "white",
                boxShadow  = "5px 0 5px -5px rgb(0 0 0 / 28%)",
                zIndex     = IsOpen ? "1" : "-9999",
                visibility = IsOpen ? "visible" : "collapsed",
                opacity    = IsOpen ? "1" : "0",
                transition = "0.5s"
            }
        };
    }
}