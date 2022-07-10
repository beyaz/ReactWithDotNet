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
        const string Elif = "Elif";
        const string Lam  = "Lam";
        const string Mim = "Mim";

        var elifLamMim_2 = new Style { border = "2px solid blue" };
        
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
                        new div
                        {
                            Style = elifLamMim_2,
                            id    = $"2-{Elif}",
                            innerText = Elif
                        },

                        new div
                        {
                            style = { border = "2px solid red"},
                            id    = $"2-{Lam}",
                            innerText  = Lam
                        },

                        new div
                        {
                            style = { border = "2px solid red",borderRadius = "7px"},
                            id    = $"2-{Mim}",
                            innerText  = Mim
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
                        new a{href = "#",innerText = "incele"}
                    }
                },

                CreatePath("Elif","Result","blue"),
                
                new Xarrow{start = "Lam", end  = "Result",path  = "smooth", color = "red", strokeWidth    = 1},
                new Xarrow{start = "Mim", end  = "Result",path  = "smooth",color  = "yellow", strokeWidth = 1},
                new Xarrow{start = "2-Mim-1", end  = "Result2",path  = "smooth",color  = "green", strokeWidth = 2, labels = "134", startAnchor="right"},
            }
        };



        static Element CreatePath(string startElementId, string endElementId, string color)
        {
            return new Xarrow { start = startElementId, end = endElementId, path = "smooth", color = color, strokeWidth = 1 };
        }
        
    }

   
}