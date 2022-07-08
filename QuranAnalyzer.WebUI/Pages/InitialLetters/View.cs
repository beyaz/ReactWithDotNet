using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.FactPage;
using ReactDotNet.Html5;
using ReactDotNet.react_xarrows;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

public class View : PageBase
{
    public override string id { get; set; } = PageId;

    public static string PageId => nameof(InitialLetters);

    public override Element render()
    {

        return new div
        {
            children =
            {
                new div
                {

                    style =
                    {
                        display = "flex"
                    },
                    children =
                    {
                        new div("Elif")
                        {
                            style = { margin = "10px" , border = "2px solid blue"},
                            id    = "Elif"
                        },

                        new div("Lam")
                        {
                            style = { margin = "10px", border = "2px solid red"},
                            id    = "Lam"
                        },

                        new div
                        {
                            style = {padding = "3px", border = "2px solid green", borderRadius = "7px" },
                            id    = "2-Mim-1",
                            children =
                            {
                                new div("Mim")
                                {
                                    style = { margin = "10px", border = "2px solid yellow", borderRadius = "7px"},
                                    id    = "Mim"
                                }
                            }
                        }
                       

                        

                       
                    }

                },

                new div("19 x 344")
                {
                    style = { margin = "150px"},
                    id    = "Result"
                },

                new div()
                {
                    style = { marginTop = "160px", marginLeft = "320px"},
                    id    = "Result2",
                    children =
                    {
                        new div("19 x 44"),
                        new a{href = "#",text = "incele"}
                    }
                },

                new Xarrow{start = "Elif", end = "Result", path = "smooth", color = "blue",strokeWidth    = 1},
                new Xarrow{start = "Lam", end  = "Result",path  = "smooth", color = "red", strokeWidth    = 1},
                new Xarrow{start = "Mim", end  = "Result",path  = "smooth",color  = "yellow", strokeWidth = 1},
                new Xarrow{start = "2-Mim-1", end  = "Result2",path  = "smooth",color  = "green", strokeWidth = 2, labels = "134", startAnchor="right"},
            }
        };
        
    }

   
}