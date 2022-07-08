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
                            style = { margin = "10px"},
                            id    = "Elif"
                        },

                        new div("Lam")
                        {
                            style = { margin = "10px"},
                            id    = "Lam"
                        },

                        new div("Mim")
                        {
                            style = { margin = "10px"},
                            id    = "Mim"
                        },

                        

                       
                    }

                },

                new div("19 x 344")
                {
                    style = { margin = "150px"},
                    id    = "Result"
                },

                new Xarrow{start = "Elif", end = "Result", path = "smooth"},
                new Xarrow{start = "Lam", end  = "Result",path = "smooth"},
                new Xarrow{start = "Mim", end  = "Result",path = "smooth"},
            }
        };
        
    }

   
}