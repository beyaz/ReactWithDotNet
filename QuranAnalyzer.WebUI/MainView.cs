using ReactDotNet;
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
                    style = { Background = "#D8D8D8", Width = 250.AsPixel(), 
                        Height = 250.AsPixel() ,
                        BoxShadow ="0 4px 8px 0 rgba(0,0,0,0.2)",
                        Margin = 20.AsPixel(),
                    },
                    text = title

                };
            }

            return new div
            {
                style =
                {
                    Background = "White"
                },
                Children =
                {
                    Fact("Kaf"),
                    Fact("Ha-Mim"),
                    Fact("Nun"),
                    Fact("Ya-sin")
                }
            };
        }
    }
}