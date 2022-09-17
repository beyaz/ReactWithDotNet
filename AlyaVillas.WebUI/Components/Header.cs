namespace AlyaVillas.WebUI.Components;


class HeaderState
{
    public bool LanguagePopupIsOpen { get; set; }
}

class Header : ReactComponent<HeaderState>
{
    protected override void constructor()
    {
        state = new HeaderState();
        
    }

    protected override Element render()
    {
        return new container
        {
            new div
            {
                style = { display = "flex", direction = "row", fontSize = "14px"},
                children =
                {
                    new Logo(),
                    new HSpace(50),
                    new div
                    {
                        style = { display = "flex", alignItems = "center", justifyContent = "space-between", width = "100%"},
                        children =
                        {
                            new div
                            {
                                style = { display = "flex", direction = "row", gap = "20px", alignItems = "center"},
                                children =
                                {
                                    Menutem("Bodrum", "/villa/bodrum"),
                                    Menutem("Marmaris"),
                                    Menutem("Maşukiye")
                                }
                            },
                            new div
                            {
                                style = { display = "flex", direction = "row", gap = "20px", alignItems = "center"},
                                children =
                                {
                                    Menutem("Hemen Ara: 0555 055 55 55", "/villa/bodrum"),
                                    Language(),
                                    UserIcon(),
                                    new BrownButton{ Text = "Rezervasyon Yap", Icon = "right-arrow", OnClick = OnLanguageClicked}
                                }
                            }
                        }
                    }

                }
            }
        };


        Element Language()
        {

            return  new div
            {
               onClick = OnLanguageClicked,
               children =
               {
                   new HStack
                   {
                       new div("TR / EUR"),
                       new HSpace(5),
                       new i {className = "icon icon-caret-down"},
                   },
                   LanguagePopup()
               }
                
            };
        }

        Element LanguagePopup()
        {
            return new div
            {
                style =
                {
                    position = "absolute",
                    width = "150px",
                    height = "220px",
                    border = "1px solid red",
                    display = state.LanguagePopupIsOpen ? "" : "none"
                }
            };
        }

        static Element UserIcon()
        {
            // TODO: icon
            return new div { style = { width_height = "20px", border = "1px solid red"}};
        }

        static Element Menutem(string text, string href = null)
        {
            return new HStack
            {
                new i {className = "icon icon-arrow-right-rectangle"},
                new HSpace(8),
                new div(text)
            };
                
            //return new div
            //{
            //    style = { display = "flex", direction = "row", gap = "8px"},
            //    children =
            //    {
            //        new i {className = "icon icon-arrow-right-rectangle"},
            //        new div(text)
            //    }
            //};
        }
    }

    void OnLanguageClicked(string _)
    {
        state.LanguagePopupIsOpen = !state.LanguagePopupIsOpen;
    }

    void OnLanguageClicked2(string _)
    {
        state.LanguagePopupIsOpen = !state.LanguagePopupIsOpen;
    }
}


