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
                {
                    new i("pi pi-search"),
                    new InputText{placeholder = "ara"}
                };

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
                style = {Height ="50px"},
                Children =
                {
                    new svg
                    {
                        new path{d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"}
                    }
                    
                }
            };
            var main = new div
            {
                new div{ style = {Display= Display.Flex,JustifyContent = JustifyContent.Center}, Children = {searchText } },
                factsContainer
            };

            return new div { topNav, main };
        }
    }
}