﻿using ReactWithDotNet.Libraries.mui.material;

namespace ReactWithDotNet.WebSite.Showcases
{
    public class MuiTextFieldDemo : ReactPureComponent
    {
        protected override Element render()
        {
            return new Paper
            {
                component = "form",
                sx        = { p = "2px 4px", display = "flex", alignItems = "center", width = 400 },
                children =
                {
                    // new InputBase { sx  = { ml = 1, flex = 1 }, placeholder = "Search in samples" },
                    new TextField { sx = { ml = 1, flex = 1 } },
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
}