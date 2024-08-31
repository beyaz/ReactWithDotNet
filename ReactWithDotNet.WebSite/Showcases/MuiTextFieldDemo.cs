using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Showcases;

public class MuiTextFieldDemo : PureComponent
{
    protected override Element render()
    {
        return new Paper
        {
            component = "form",
            sx        = { p = "2px 4px", display = "flex", alignItems = "center", width = 400 },
            children =
            {
                new TextField { sx = { ml = 1, flex = 1 }, size = "small" },
                new IconButton
                {
                    type = "button",
                    sx   = { p = "10px" },
                    children =
                    {
                        new span { className = "material-icons", text = "search" }
                    }
                }
            }
        };
    }
}