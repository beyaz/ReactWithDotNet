using AlyaVillas.WebUI.Components;
using static AlyaVillas.WebUI.Mixin;
using static AlyaVillas.WebUI.Extension;

namespace AlyaVillas.WebUI.Layout;

public class Footer: ReactComponent
{
    protected override Element render()
    {
        var menus = new[]
        {
            new 
            {
                title = "İletişim Bilgilerimiz",
                items = new[]
                {

                    new
                    {
                        icon  = "phone-call",
                        title = "0 212 000 00 00",
                        url   = "tel=+902120000000"
                    },
                    new
                    {
                        icon  = "mail",
                        title = "bilgi@alyavillahotel.com",
                        url   = "mailto=bilgi@alyavillahotel.com"
                    }
                }
            },
            new
            {
                title = "Menü",
                items =new[]
                {
                    new
                    {
                        icon  =string.Empty,
                        title = "Hakkımızda",
                        url   = "/info/about"
                    },
                    new
                    {
                        icon  =string.Empty,
                        title = "Blog",
                        url   = "/blog"
                    },
                    new
                    {
                        icon  =string.Empty,
                        title = "Sıkça Sorulan Sorular",
                        url   = "/faq"
                    },
                    new
                    {
                        icon  =string.Empty,
                        title = "İletişim",
                        url   = "/contact"
                    }
                }
            },
            new
            {
                title = "Villalarımız",
                items = new[]
                {

                    new
                    {
                        icon  =string.Empty,
                        title = "Sierra Villa Bodrum",
                        url   = "/villa/sierro-villa-bodrum",
                    },
                    new
                    {
                        icon  =string.Empty,
                        title = "Villa Butik Marmaris",
                        url   = "/villa/villa-butik-marmaris",
                    },
                    new
                    {
                        icon  =string.Empty,
                        title = "Piynar Villa Marmaris",
                        url   = "/villa/piynar-villa-marmaris",
                    },
                    new
                    {
                        icon  =string.Empty,
                        title = "Alya Villa Maşukiye",
                        url   = "/villa/alya-villa-masukiye",
                    }
                }
            }
        };
            
        return new footer
        {
            style =
            {
                background = "#4A4A49",
                color      = "#F6F1E4",
                paddingTop = "64px"
            },
            children =
            {
                new container
                {
                    new div
                    {
                        style = { display = "flex", alignItems = "center", justifyContent = "space-between", paddingBottom = "48px"},
                        children =
                        {
                            new Logo { On = "dark" },

                            new img
                            {
                                className = "tursab", src = ImageUrl("/assets/general/tursab.png"), alt = "TURSAB",
                                style     = { width = "117px", height = "40px" }
                            }
                        }
                    },
                    new div
                    {
                        style    = { display = "flex", justifyContent = "space-between",marginLeft = "-48px" },
                        Children = ListOf(menus.Select(AsMenu),bültenMenu())
                    },
                    new VSpace(48),
                    copyRight()
                }
            }
        };

            

    }
    static Element copyRight()
    {
        return new div(new Style {display = "flex" ,alignItems = "center", justifyContent = "space-between", fontSize = "14px", borderTop = $"1px solid {w50}", paddingTopBottom = "16px"})
        {
            new a{text = "Güvenlik Gizlilik"},
            new a{text = "® 2022 Alya Villas"}
        };
    }
    static Element bültenMenu()
    {
        return new div(new Style{width = "400px"})
        {
            MenuTitle("Fırsatlarımızdan habedar olmak için bültenimize kayıt olun!")
        };
    }
        
    static Element AsMenu(dynamic menu)
    {
        return new div(new Style{ paddingLeftRight = "48px" })
        {
            Children = ListOf(MenuTitle(menu.title) , ((IEnumerable<dynamic>)menu.items).Select(AsMenuItem))
        };
    }


    static Element MenuTitle(string title)
    {
        return new div
        {
            text = title,
            style =
            {
                fontSize     = "18px",
                fontWeight   = "600",
                marginBottom = "12px"
            }
        };
    }
    static Element AsMenuItem(dynamic item)
    {
        return new div
        {
            style = { alignItems = "center", padding = "4px 0" },
            children =
            {
                item.icon == string.Empty? null : new i{className = $"icon {item.icon}", style = { marginRight = "8px"}},
                new a { text = item.title, title = item.title, href = item.url, style = { color = "inherit"}}
            }
        };
    }
}