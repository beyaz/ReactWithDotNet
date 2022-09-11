namespace AlyaVillas.VillaSearch;

class VillaSearchResultCardModel
{
    public string CommonTitle { get; set; } = "Dubleks Özel Havuzlu Villa";
    public int YatakSayisi { get; set; } = 2;
    public int BanyoSayisi { get; set; } = 1;
    public int ToplamdaKacKişilik { get; set; } = 7;
    public decimal ToplamFiyat { get; set; } = 7.500M;
}

class VillaSearchResultCardView : ReactComponent
{
    public VillaSearchResultCardModel Model { get; set; }
    
    protected override Element render()
    {
        var model = Model ?? new VillaSearchResultCardModel();
        
        return new div
        {
            style = { border = "1px solid #DBDBDB", display = "flex"},
            children =
            {
                new img { src = "wwwroot/img/Temp.png", style = { width = "328px", height = "auto", display = "block" } },
                new div
                {
                    style = { padding = "32px", width = "100%"},
                    children =
                    {
                        new div(model.CommonTitle)
                        {
                            style =
                            {
                                fontFamily = "'Source sans pro'",
                                fontWeight = "600",

                                color      = "#4A4A49",
                                fontSize   = "18px",
                                lineHeight = "24px"
                            }
                        },

                        new div("Fotoğraflar ve Olanaklar")
                        {
                            style =
                            {
                                fontFamily = "'Open Sans'",
                                fontWeight = "600",

                                color      = "#429777",
                                fontSize   = "14px",
                                lineHeight = "24px"
                            }
                        },
                        new VSpace(12),
                        new div("Tam Pansiyon")
                        {
                            style =
                            {
                                fontFamily = "'Source sans pro'",
                                fontWeight = "600",

                                color      = "#4A4A49",
                                fontSize   = "16px",
                                lineHeight = "24px"
                            }
                        },
                        new VSpace(12),
                        newInfo("pull-door.svg","4 Misafir"),
                        new VSpace(16),
                        new HStack
                        {
                            newInfo("pull-door.svg","3 Yatak Odası"),
                            new HSpace(10),
                            newInfo("pull-door.svg",$"{model.YatakSayisi} Yatak")
                        },
                        new VSpace(16),
                        newInfo("pull-door.svg",$"{model.BanyoSayisi} Banyo"),

                        new VSpace(12),
                            
                        // f o o t e r
                        new div
                        {
                            style = { display = "flex", justifyContent = "space-between", alignItems = "flex-end", width = "100%"},
                            children =
                            {
                                new div($"{model.ToplamdaKacKişilik} kişilik"),
                                new div
                                {
                                    style = { display = "flex"},
                                    children =
                                    {
                                        new div
                                        {
                                            new div("10.000 TL"){style = { fontSize = "14px", textDecorationLine = "line-through", color = "#A8A9AD"}},
                                            new div($"{model.ToplamFiyat} TL"){style  = { color    = "#4A4A49", fontSize        = "24px", lineHeight    = "18px", fontWeight = "600"}}
                                        },
                                        new HSpace(20),
                                        new div
                                        {
                                            text  = "Seç",
                                            style = { paddingLeftRight = "37px", paddingTopBottom = "12px", background = "#A08139", color = "#F7F1E4" }
                                        }
                                            
                                    }
                                }
                            }
                        }
                    }

                }
            }
        };

        static Element newInfo(string svgName, string text)
        {
            return new div
            {
                style = { display = "flex" },
                children =
                {
                    new img { src = $"wwwroot/img/{svgName}", style = { width_height = "20px" } },
                    new HSpace(5),
                    new div(text)
                    {
                        style =
                        {
                            fontFamily = "'Source sans pro'",
                            fontWeight = "400",
                            color      = "#4A4A49",
                            fontSize   = "14px",
                            lineHeight = "20px"
                        }
                    }
                }
            };
        }
    }
}