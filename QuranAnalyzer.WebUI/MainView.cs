using ReactDotNet;
using ReactDotNet.PrimeReact;
using System;
using static ReactDotNet.Mixin;
using static QuranAnalyzer.Mixin;
using System.Threading;
using System.Linq;

namespace QuranAnalyzer.WebUI
{
    class Theme
    {
        public readonly string MainPaperBackgroundColor = "white";
    }

    [Serializable]
    public class MainViewModel
    {
        public string SelectedFact { get; set; }
        public string SummaryText { get;  set; }

        public ClientTask ClientTask { get; set; }
        public string OperationName { get; set; }
        public bool IsBlocked { get; set; }

        public bool HamburgerMenuIsOpen { get; set; }
    }

    class MainView : ReactComponent<MainViewModel>
    {
        Theme theme = new Theme();
        public MainView()
        {
            state = new MainViewModel();
        }

        void OnClicked(string name)
        {
            state.SelectedFact = name;
        }

        

        public override Element render()
        {

            Element kaf(Action onClick)
            {
                var searchBar = new HPanel
                {
                    new div
                    {
                        new div{text = "Sure:"},
                        new InputText{value = "42:* // 42. surenin tamamında ar",}
                    },
                    new div
                    {
                        new div{text = "Karaktere"},
                        new InputText{value = state.SelectedFact}
                    },
                    
                    new Button("p-button-outlined") { text="Ara", onClick = onClick },
                };

                var results = new TabView
                {
                    new TabPanel
                    {
                        header = "Özet", 
                        children = 
                        {
                            new div(){text = state.SummaryText},
                            new HPanel
                            { 
                                new div { text = "42. suredeki Kaf harfi geçiş sayısı "},
                                new pre { text = " '19 ", style = {fontFamily = "'Source Sans Pro', 'Helvetica Neue', Arial, sans-serif", color= "red" } },
                                new div { text = "x 6' dır."},
                            }
                        } 
                    },
                    new TabPanel
                    {
                        header = "Detaylı Tablo", 
                        children = 
                        {
                            new div(){text = "detaylı tablo"}
                        } 
                    }
                };

                return new div { searchBar, results } + new Style { marginTop = px(55), marginBottom = px(55)};

            }

            static Element Fact(string title, Action onClick)
            {
                return new div
                {
                    style =
                    {
                        borderRadius = "5px",
                        width      = px(150),
                        height     = px(200) ,
                        //boxShadow  ="0 4px 8px 0 rgba(0,0,0,0.2)",
                        margin     = px(20),
                         display = Display.flex,
                        justifyContent = JustifyContent.center,
                        alignItems = AlignItems.center,
                        flexDirection = FlexDirection.column,
                        textAlign = TextAlign.center,
                        fontFamily = "Verdana,sans-serif"
                    },
                    onClick = onClick,
                    children =
                    {
                        new div
                        {

                           style = { padding = "1px",  },
                           children =
                           {
                                 new div { style =   {color ="#08090a", fontSize = "17px", fontWeight = "600" }, text = title },
                                 new div
                                 {
                                     style =
                                     {
                                         wordBreak = WordBreak.break_all,
                                         margin = "15px",
                                         fontSize= px(13),
                                         color="#546285"
                                     },
                                     text = "Kısa açıklamaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa1gtgtgrtrtğ", Margin={ Top=5} }
                           }
                        }

                    }

                }.HasBorder();
            }

            Element createHamburgerIcon()
            {
                Action onClick = () =>
                {
                    state.HamburgerMenuIsOpen = !state.HamburgerMenuIsOpen;
                };

                if (state.HamburgerMenuIsOpen)
                {
                    var svg = new svg { viewBox = "0 0 15 15", style = { height = "20px", margin = "14px" }, onClick = onClick };

                    var path = new path { d = "M15 8a.5.5 0 0 0-.5-.5H2.707l3.147-3.146a.5.5 0 1 0-.708-.708l-4 4a.5.5 0 0 0 0 .708l4 4a.5.5 0 0 0 .708-.708L2.707 8.5H14.5A.5.5 0 0 0 15 8z", fill = "blue" };

                    return svg.appendChild(path);
                }

                {
                    var svg = new svg { viewBox = "0 0 20 20", style = { height = "20px", margin = "14px" },  onClick = onClick };

                    var path = new path { d = "M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z", fill = "blue" };

                    return svg.appendChild(path);
                }
            }

            var searchText = new span("p-input-icon-right")
                                + new i("pi pi-search")
                                + new InputText { placeholder = "ara" };

            var factsContainer = new div
            {
                style =
                {
                    background = theme.MainPaperBackgroundColor,
                    display = Display.flex,
                    flexWrap = FlexWrap.wrap,
                    justifyContent = JustifyContent.center
                },
                children =
                {
                    Fact("Kaf", () => OnClicked("Kaf")),
                    Fact("Ha-Mim", () => OnClicked("Kaf")),
                    Fact("Nun", () => OnClicked("Nun")),
                    Fact("Ya-sin", () => OnClicked("Kaf")),
                    Fact("Ya-sin", () => OnClicked("Kaf")),
                    Fact("Ya-sin", () => OnClicked("Kaf")),
                    Fact("Ya-sin", () => OnClicked("Kaf")),
                    Fact("Ya-sin", () => OnClicked("Kaf")),
                    Fact("Ya-sin", () => OnClicked("Kaf")),
                    Fact("Ya-sin", () => OnClicked("Kaf"))
                }
            };
            var topNav = new nav { style = { display = Display.flex, justifyContent = JustifyContent.space_between, alignItems = AlignItems.center }, }
                //+ (new svg { viewBox = "0 0 100 100", style = { height = "40px", marginLeft = "14px" } }
                //   + new path { d = "M100 34.2c-.4-2.6-3.3-4-5.3-5.3-3.6-2.4-7.1-4.7-10.7-7.1-8.5-5.7-17.1-11.4-25.6-17.1-2-1.3-4-2.7-6-4-1.4-1-3.3-1-4.8 0-5.7 3.8-11.5 7.7-17.2 11.5L5.2 29C3 30.4.1 31.8 0 34.8c-.1 3.3 0 6.7 0 10v16c0 2.9-.6 6.3 2.1 8.1 6.4 4.4 12.9 8.6 19.4 12.9 8 5.3 16 10.7 24 16 2.2 1.5 4.4 3.1 7.1 1.3 2.3-1.5 4.5-3 6.8-4.5 8.9-5.9 17.8-11.9 26.7-17.8l9.9-6.6c.6-.4 1.3-.8 1.9-1.3 1.4-1 2-2.4 2-4.1V37.3c.1-1.1.2-2.1.1-3.1 0-.1 0 .2 0 0zM54.3 12.3L88 34.8 73 44.9 54.3 32.4V12.3zm-8.6 0v20L27.1 44.8 12 34.8l33.7-22.5zM8.6 42.8L19.3 50 8.6 57.2V42.8zm37.1 44.9L12 65.2l15-10.1 18.6 12.5v20.1zM50 60.2L34.8 50 50 39.8 65.2 50 50 60.2zm4.3 27.5v-20l18.6-12.5 15 10.1-33.6 22.4zm37.1-30.5L80.7 50l10.8-7.2-.1 14.4z" })
                +
                createHamburgerIcon();

            Element main = factsContainer;

            void OnKafSelectClicked()
            {
                if (state.IsBlocked == false)
                {
                    state.OperationName = "Hesaplanıyor...";
                    state.IsBlocked = true;
                    state.ClientTask = new ClientTask { ComebackWithLastAction = true };
                    return;
                }
                Thread.Sleep(3000);
                state.IsBlocked = false;

                state.SummaryText = "42. suredeki Kaf harfi toplam geçiş adeti <span>19</span> x 6";
            }

            if (state.SelectedFact == "Kaf")
            {
                main = kaf(OnKafSelectClicked).HasBorder();
            }
            if (state.SelectedFact== "Nun")
            {
                main = new div
                {
                    style = { marginTop = "5px" },
                    text = "Nun"

                }.HasBorder();
            }

            var footer = new div
            {
                 new div { text = "Anasayfa"},
                 new div { text = "İletişim"},
                 new div { text = "Soru-Cevap"},
            } +
            new Style 
            { 
                display        = Display.flex, 
                justifyContent =JustifyContent.space_between
            };

            Element blockUI(Element content)
            {
                return new BlockUI
                {
                    blocked = state.IsBlocked,
                    template = new div
                    {
                        new i { className = "pi pi-spin pi-spinner" },
                        new div { Margin = { Left = 5 }, style = { color = "White" }, text = state.OperationName }
                    }.MakeCenter(),

                    children =
                    {
                        content
                    }
                };
            }

            Element createMenuItem(string text)
            {
                return new div { text = text } + new Style { fontSize = px(17) , marginTop = px(20) };
            }

            var menu = new div 
            {
                createMenuItem("Anasayfa"), 
                createMenuItem("Başlangıç Harfleri"), 
                createMenuItem("Soru - Cevap"), 
                createMenuItem("İletişim"),
            } 
            + 
            new Style 
            { 
                position   = Position.@fixed, 
                height     = "100%",
                width      = "70%",
                top        = px(50),
                background = "rgb(248, 249, 249)",
                zIndex     = "1",
                display = Display.flex,
                flexDirection = FlexDirection.column,
                alignItems = AlignItems.center,
                fontSize = px(18)

            };
            
            

            return CreatePage(topNav, main, footer, menu);


            return blockUI(new div { style = {background = theme.MainPaperBackgroundColor, marginLeft = "20px", marginRight = "20px"}}
                    + topNav
                    + main
                    + footer);
        }

        Element CreatePage(Element topContent, Element mainContent, Element footerContent, Element menu)
        {
            var top = new div { topContent } + new Style
            { 
                position       = Position.@fixed, 
                top            = px(0),
                left = px(0),

                width = "100%",
                height         = px(50),
                zIndex = "1",
                background = "rgb(248, 249, 249)",
                boxShadow = "0 1px 2px hsla(0,0%,0%,0.05),0 1px 4px hsla(0,0%,0%,0.05),0 2px 8px hsla(0,0%,0%,0.05)"
            };

            var main   = new div { mainContent } + new Style 
            { 
                position       = Position.@fixed, 
                top            = px(0),
                left = px(0),
                marginTop= px(50),
                marginBottom= px(27),
                
                width = "100%",
                height = "calc(100% - 65px)",
                overflowY = Overflow.auto,
                background = "blue"
            };

            var footer = new div { footerContent } + new Style 
            { 
                position       = Position.@fixed, 
               
                height      = px(20),
                width = "100%",
                bottom = px(0),
                background = "whitesmoke",
                zIndex = "1"
            };

            if (state.HamburgerMenuIsOpen)
            {
                return new div {  top, menu,  main, footer } + new Style { height = "100vh" , width = "100%" };
            }

            return new div {  top,  main, footer } + new Style { height = "100vh" , width = "100%" };
        }
        


    }
}

// https://codepen.io/Zeeslag/pen/MWpLoKX