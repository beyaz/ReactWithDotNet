using ReactDotNet;
using ReactDotNet.PrimeReact;
using System;
using static ReactDotNet.Mixin;

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

            Element topNav = new div
            {
                style = { height = "50px" },
                children =
                {
                    //new svg
                    //{
                    //    viewBox="0 0 100 80", width="40", height="50",
                    //    Margin={Left=19,Top=5},
                    //    Children =
                    //    {
                    //        new rect{width="100", height="10"},
                    //        new rect{width="100", height="10",y=20},
                    //        new rect{width="100", height="10",y=40}
                    //    }
                    //},

                    //new svg
                    //{
                    //    viewBox ="0 0 200 20", width = "200px",
                    //    Children =
                    //    {
                    //        new path{d= "M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z", fill = "blue"}
                    //    }
                    //}

                    new svg { viewBox ="0 0 200 20", width = "200px"}
                        + new path{d= "M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z", fill = "blue"}
                }
            };

            topNav = new nav { style = { display = Display.flex, justifyContent = JustifyContent.space_between, alignItems = AlignItems.center }, }.HasBorder()
                + (new svg { viewBox = "0 0 100 100", style = { height = "40px", marginLeft = "14px" } }
                   + new path { d = "M100 34.2c-.4-2.6-3.3-4-5.3-5.3-3.6-2.4-7.1-4.7-10.7-7.1-8.5-5.7-17.1-11.4-25.6-17.1-2-1.3-4-2.7-6-4-1.4-1-3.3-1-4.8 0-5.7 3.8-11.5 7.7-17.2 11.5L5.2 29C3 30.4.1 31.8 0 34.8c-.1 3.3 0 6.7 0 10v16c0 2.9-.6 6.3 2.1 8.1 6.4 4.4 12.9 8.6 19.4 12.9 8 5.3 16 10.7 24 16 2.2 1.5 4.4 3.1 7.1 1.3 2.3-1.5 4.5-3 6.8-4.5 8.9-5.9 17.8-11.9 26.7-17.8l9.9-6.6c.6-.4 1.3-.8 1.9-1.3 1.4-1 2-2.4 2-4.1V37.3c.1-1.1.2-2.1.1-3.1 0-.1 0 .2 0 0zM54.3 12.3L88 34.8 73 44.9 54.3 32.4V12.3zm-8.6 0v20L27.1 44.8 12 34.8l33.7-22.5zM8.6 42.8L19.3 50 8.6 57.2V42.8zm37.1 44.9L12 65.2l15-10.1 18.6 12.5v20.1zM50 60.2L34.8 50 50 39.8 65.2 50 50 60.2zm4.3 27.5v-20l18.6-12.5 15 10.1-33.6 22.4zm37.1-30.5L80.7 50l10.8-7.2-.1 14.4z" })
                +
                (new svg { viewBox = "0 0 20 20", style = { height = "16px", margin = "14px" } }
                + new path { d = "M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z", fill = "blue" });

            var main = new div
            {
                style = {marginTop = "5px"},
                children =
                {
                    new div{ style = {display = Display.flex, justifyContent = JustifyContent.center}, children = {searchText } },
                    factsContainer
                }
                
            }.HasBorder();

            if (state.SelectedFact== "Kaf")
            {
                main = new div
                {
                    style = { marginTop = "5px" },
                    text = "Kaf"

                }.HasBorder();
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
                style= { display = Display.flex, justifyContent=JustifyContent.center, minHeight = "50px", marginTop = "5px"},
                children = { new div { text = "copyright"} }
            }.HasBorder();

            return new div { style = {background = theme.MainPaperBackgroundColor, marginLeft = "20px", marginRight = "20px"}}
                    + topNav
                    + main
                    + footer;
        }
    }
}

// https://codepen.io/Zeeslag/pen/MWpLoKX