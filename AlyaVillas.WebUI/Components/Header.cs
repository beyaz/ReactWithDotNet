

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
            zIndex       = "3",
            transition   = "0.35sn",
            borderBottom = IsTransparent ? "none" : $"1px solid {Natural.W75}",
            background   = IsTransparent ? "transparent" : Natural.W0,
            fontWeight   = "600",
            color        = Primary.W50
            
        };
        
        return new header(headerStyle)
        {
            new container(BoxSizingBorderBox)
            {
                new FlexRow(BoxSizingBorderBox,Height(IsMobile ? 60 : 90))
                {
                    new FlexRow(AlignItemsCenter, JustifyContentSpaceBetween,StretchWidth)
                    {
                        new FlexRow(AlignItemsCenter, Gap(65))
                        {
                            new Logo{On = IsTransparent ? "dark" : "light"},
                            new FlexRow
                            {
                                Menutem("Bodrum", "/villa/bodrum"),
                                Menutem("Marmaris"),
                                Menutem("Maşukiye")
                            }
                        },
                        new FlexRow(Gap(20), AlignItemsCenter)
                        {
                            Menutem("0850 345 34 65", "/villa/bodrum", "icon-phone-call"),
                            Language(),
                            UserIcon(),
                            new BookNow{IsTransparent = IsTransparent}
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
                   new FlexRow(AlignItemsCenter)
                   {
                       new div("TR / EUR"),
                       new HSpace(5),
                       new i(Color("white")) {className = "icon icon-caret-down"},
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

        
        
        

    }

    void OnLanguageClicked(MouseEvent e)
    {
        state.LanguagePopupIsOpen = !state.LanguagePopupIsOpen;
    }
}

class BookNow : ReactComponent, ISupportMouseEnter
{
    public bool IsMouseEntered { get; set; }
    
    public bool IsTransparent { get; set; }
    
    protected override Element render()
    {
        var borderColor = IsTransparent ? Primary.W50 : Natural.W900;

        borderColor = HexToRgb(borderColor, IsMouseEntered ? 1 : 0.5);
        
        return new a
        {
            style = { textDecoration = "none", color = "inherit", border = $"1px solid {borderColor}", padding = "8px 16px" },
            href  = "#",
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

