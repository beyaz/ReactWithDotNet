using ReactDotNet;
using ReactDotNet.PrimeReact;
using System;

namespace QuranAnalyzer.WebUI
{
    [Serializable]
    public class MainViewModel
    {

    }

    class MainView : ReactComponent<MainViewModel>
    {
        public override Element render()
        {
            static Element Fact(string title)
            {
                return new div
                {
                    style =
                    {
                        BorderRadius = "5px",
                        Width      = 150.AsPixel(),
                        Height     = 200.AsPixel() ,
                        BoxShadow  ="0 4px 8px 0 rgba(0,0,0,0.2)",
                        Margin     = 20.AsPixel(),
                         Display = Display.Flex,
                        JustifyContent = JustifyContent.Center,
                        AlignItems = AlignItems.Center,
                        FlexDirection = FlexDirection.Column,
                        TextAlign = TextAlign.Center,
                        FontFamily = "Verdana,sans-serif"
                    },
                    Children =
                    {
                        new div
                        {

                           style = { Padding = "1px",  },
                           Children =
                           {
                                 new div { style =   {FontSize ="x-large", FontWeight = "600" }, text = title },
                                 new div
                                 {
                                     style =
                                     {
                                         WordBreak = WordBreak.BreakAll,
                                         Margin = "15px",
                                         FontSize=13.AsPixel()
                                     },
                                     text = "Kısa açıklamaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa1gtgtgrtrtğ", Margin={ Top=5} }
                           }
                        }

                    }

                };
            }

            var searchText = new span("p-input-icon-right")
                                + new i("pi pi-search")
                                + new InputText { placeholder = "ara" };

            var factsContainer = new div
            {
                style =
                {
                    Background = "White",
                    Display = Display.Flex,
                    FlexWrap = FlexWrap.Wrap,
                    JustifyContent = JustifyContent.Center
                },
                Children =
                {
                    Fact("Kaf"),
                    Fact("Ha-Mim"),
                    Fact("Nun"),
                    Fact("Ya-sin")
                }
            };

            var topNav = new div
            {
                style = { Height = "50px" },
                Children =
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
            var main = new div
            {
                new div{ style = {Display= Display.Flex,JustifyContent = JustifyContent.Center}, Children = {searchText } },
                factsContainer
            };

            return new div()
                    + topNav
                    + main;
        }
    }
}

// https://codepen.io/Zeeslag/pen/MWpLoKX