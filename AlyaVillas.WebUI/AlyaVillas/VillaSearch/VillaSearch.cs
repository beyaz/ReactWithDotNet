using System.Text;

namespace AlyaVillas.VillaSearch;

class VillaSearchViewModel
{
    public string CommonTitle { get; set; } = "Dubleks Özel Havuzlu Villa";
    public int YatakSayisi { get; set; } = 2;
    public int BanyoSayisi { get; set; } = 1;
    public int ToplamdaKacKişilik { get; set; } = 7;
    public decimal ToplamFiyat { get; set; } = 7.500M;
}

class VillaSearchView : ReactComponent
{
    public VillaSearchViewModel Model { get; set; }
    
    protected override Element render()
    {
        var model = Model ?? new VillaSearchViewModel();


        return new div
        {
            style = { border = "1px solid #DBDBDB", display = "flex", flexDirection = "column", paddingLeftRight = "10px" },
            children =
            {
                new VSpace(24),
                new div
                {
                    style =
                    {
                        boxShadow      = "0px 4px 8px rgba(16, 24, 64, 0.08)",
                        padding        = "16px",
                        display        = "flex",
                        flexDirection  = "row",
                        justifyContent = "space-around",
                        alignItems     = "center"
                    },
                    children =
                    {
                        //BuildGrisCıkışTarih(),
                        new div { style = { width = "1px", background = "#C9C9C8", height = "36px" } },
                        BuildGrisCıkışTarih(),
                        new div { style = { width = "1px", background = "#C9C9C8", height = "36px" } },
                        //BuildGrisCıkışTarih(),
                        new div
                        {
                            style =
                            {
                                display        = "flex",
                                flexDirection  = "row",
                                justifyContent = "center",
                                alignItems     = "center",
                                padding        = "8px 16px",
                                gap            = "8px",
                                border         = "1px solid #A08139",
                                boxSizing      = "border-box",
                                borderRadius   = "3px"
                            },
                            children =
                            {

                                new svg
                                {
                                    width = "16", height = "16", viewBox = "0 0 16 16", fill = "none", xmlns = "http://www.w3.org/2000/svg",
                                    children =
                                    {
                                        new path { d = "M7.33334 2.66659C4.75601 2.66659 2.66668 4.75592 2.66668 7.33325C2.66668 9.91058 4.75601 11.9999 7.33334 11.9999C9.91067 11.9999 12 9.91058 12 7.33325C12 4.75592 9.91067 2.66659 7.33334 2.66659ZM1.33334 7.33325C1.33334 4.01954 4.01963 1.33325 7.33334 1.33325C10.6471 1.33325 13.3333 4.01954 13.3333 7.33325C13.3333 10.647 10.6471 13.3333 7.33334 13.3333C4.01963 13.3333 1.33334 10.647 1.33334 7.33325Z", fill = "#A08139" },
                                        new path { d = "M10.8619 10.8618C11.1223 10.6014 11.5444 10.6014 11.8048 10.8618L14.4714 13.5284C14.7318 13.7888 14.7318 14.2109 14.4714 14.4712C14.2111 14.7316 13.789 14.7316 13.5286 14.4712L10.8619 11.8046C10.6016 11.5442 10.6016 11.1221 10.8619 10.8618Z", fill                                                                                                                                                                  = "#A08139" }

                                    }

                                },

                                new div("Bul")
                                {
                                    style =
                                    {
                                        fontFamily   = "'Open Sans'",
                                        fontStyle    = "normal",
                                        fontWeight   = "500",
                                        fontSize     = "14px",
                                        lineHeight   = "16px",
                                        color        = "#A08139",
                                        

                                    }
                                }
                            }
                        }
                    }
                },
                new VSpace(24),
                new div
                {
                    text = "Aile için Villalar",
                    style =
                    {

                        fontFamily = "'Marcellus'",
                        fontStyle  = "normal",
                        fontWeight = "400",
                        fontSize   = "32px",
                        lineHeight = "40px",
                        color      = "#4A4A49"
                    }
                },
                new div
                {
                    children =
                    {
                        new VillaSearchResultCardView
                        {
                            Model = new VillaSearchResultCardModel
                            {
                                BanyoSayisi = 3, CommonTitle = "Villa 1", ToplamFiyat = 7.600M, ToplamdaKacKişilik = 6, YatakSayisi = 4
                            }
                        },
                        new VSpace(16),
                        new VillaSearchResultCardView
                        {
                            Model = new VillaSearchResultCardModel
                            {
                                BanyoSayisi = 3, CommonTitle = "Villa 1", ToplamFiyat = 7.600M, ToplamdaKacKişilik = 6, YatakSayisi = 4
                            }
                        },
                        new VSpace(16),
                        new VillaSearchResultCardView
                        {
                            Model = new VillaSearchResultCardModel
                            {
                                BanyoSayisi = 3, CommonTitle = "Villa 1", ToplamFiyat = 7.600M, ToplamdaKacKişilik = 6, YatakSayisi = 4
                            }
                        }
                    }
                }

            }
        };


    }

    bool showMenu;
    
    Element BuildGrisCıkışTarih()
    {
        var detail = new div
        {
            style =
            {
                position = "absolute",
                width = "260px",
                height = "auto",
                background = "white",
                boxShadow = "rgb(0 0 0 / 20%) 0px 5px 5px -3px, rgb(0 0 0 / 14%) 0px 8px 10px 1px, rgb(0 0 0 / 12%) 0px 3px 14px 2px",
                borderRadius = "5px"
                
            },
            onClick = CloseDetail,
            children =
            {
                new div{style = { width_height = "30px", background = "red"}},
                new div{style = { width_height = "30px", background = "red"}},
                new div{style = { width_height = "30px", background = "red"}}
            }
        };
        if (!showMenu)
        {
            detail.style.display = "none";
        }
        
        
        return new VStack
        {
            new div("Girş - Çıkış Tarih")
            {
                style =
                {
                    fontFamily = "'Open Sans'",
                    fontStyle  = "normal",
                    fontWeight = "600",
                    fontSize   = "14px",
                    lineHeight = "24px",
                    color      = "#4A4A49"
                }
            },
            
            new div("Seçiniz")
            {
                onClick = ShowDetail,
                style =
                {
                    fontFamily = "'Open Sans'",
                    fontStyle  = "normal",
                    fontWeight = "400",
                    fontSize   = "14px",
                    lineHeight = "20px",
                    color      = "#98999D"
                }
            },
            new div
            {
                detail
            }
        };

    }

    void ShowDetail(string _)
    {
        this.showMenu = true;
    }

    void CloseDetail(string _)
    {
        this.showMenu = false;
    }
}