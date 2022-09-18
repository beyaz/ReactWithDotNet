
namespace AlyaVillas.WebUI.Components;


class HeaderState
{
    public bool LanguagePopupIsOpen { get; set; }
}

class Header : ReactComponent<HeaderState>
{
    public bool IsTransparent { get; set; }
    
    protected override void constructor()
    {
        state = new HeaderState();
    }

    protected override Element render()
    {
        var headerStyle = new Style
        {
            position     = "sticky",
            top          = "0px",
            left         = "0px",
            right        = "0px",
            zIndex       = "2",
            transition   = "0.35sn",
            borderBottom = "1px solid #EDEDED",
            fontWeight = "600",
            color = Primary.W50
            
        };
        
        return new header(headerStyle)
        {
            new container(BoxSizingBorderBox)
            {
                new div
                {
                    style = { boxSizing = "border-box", display = "flex", direction = "row", height = Context.IsMobile() ? "60px" : "90px"},
                    children =
                    {
                        new FlexRow(AlignItemsCenter, JustifyContentSpaceBetween,StretchWidth)
                        {
                            new div
                            {
                                style = { display = "flex", direction = "row", gap = "20px", alignItems = "center"},
                                children =
                                {
                                    new Logo(),
                                    new HSpace(30),
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
                                    Menutem("0850 345 34 65", "/villa/bodrum", "icon-phone-call"),
                                    Language(),
                                    UserIcon(),
                                    // new BrownButton{ Text = "Rezervasyon Yap", Icon = "icon-arrow-right", OnClick = OnLanguageClicked}
                                    BookNow()
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
            return new i(ClassName("icon icon-user"));
        }

        static Element Menutem(string text,  string href = null, string icon = "icon-arrow-right-rectangle")
        {
            return new FlexRow(FontWeight600, AlignItemsCenter, PaddingLeftRight(16))
            {
                new i {className = $"icon {icon}"},
                new HSpace(8),
                new div(text)
            };
        }

        Element BookNow()
        {
            return new a
            {
                style = { textDecoration = "none", color = "inherit", border = "1px solid rgba(74,74,73,0.5)", padding = "8px 16px"},
                href = "#",
                children =
                {
                    new FlexRow(FontWeight600, AlignItemsCenter, PaddingLeftRight(16))
                    {
                        new div(Text("Book Now"), MarginRight(8)),
                        new i(ClassName("icon icon-arrow-right"))
                    }
                }
            };
        }

    }

    void OnLanguageClicked(MouseEvent e)
    {
        state.LanguagePopupIsOpen = !state.LanguagePopupIsOpen;
    }
}


