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
        const string Mim  = "Mim";
        const string Sad  = "Sad";

        var elifLamMim_2 = new Style { border = "thin solid #a9acaa", borderRadius = "0.5rem", padding = "5px", margin = "9px"};


        var containerOfChapters = new div
        {
            new div { innerText = "Sure - 2 (Bakara)", style       = { margin = "28px"}},
            new div { innerText = "Sure - 3 (İmran Ailesi)", style = { margin = "28px"}},
            new div { innerText = "Sure - 7 (Araf)", style = { margin = "28px"}}
        };

        var containerOfInitialLetters = new div
        {
            style ={},
            children =
            {
                new div
                {
                    style =
                    {
                        display = "flex", margin = "6px"
                    },
                    children =
                    {
                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"2-{Elif}",
                            innerText = Elif
                        },

                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"2-{Lam}",
                            innerText = Lam
                        },

                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"2-{Mim}",
                            innerText = Mim
                        }

                    }
                },

                new div
                {
                    style =
                    {
                        display = "flex", margin = "6px"
                    },
                    children =
                    {
                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"3-{Elif}",
                            innerText = Elif
                        },

                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"3-{Lam}",
                            innerText = Lam
                        },

                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"3-{Mim}",
                            innerText = Mim
                        }

                    }
                },

                new div
                {
                    style =
                    {
                        display = "flex", margin = "6px"
                    },
                    children =
                    {
                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"7-{Elif}",
                            innerText = Elif
                        },

                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"7-{Lam}",
                            innerText = Lam
                        },

                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"7-{Mim}",
                            innerText = Mim
                        },

                        new div
                        {
                            Style     = elifLamMim_2,
                            id        = $"7-{Sad}",
                            innerText = Sad
                        }

                    }
                }
            }
        };

        var containerOfCounts = new div
        {
            new div
            {
                style = { display = "flex", flexDirection = "row", margin = "58px"},
                id    = "2-counts",
                children =
                {
                    new div("19 x 521"),
                    new a
                    {
                        innerText = "incele",
                        href = "#",
                        style = { marginLeft = "5px"}
                    }
                }
            },

            new div
            {
                style = { display = "flex", flexDirection = "row", margin = "58px"},
                id    = "3-counts",
                children =
                {
                    new div("19 x 298"),
                    new a
                    {
                        innerText = "incele",
                        href      = "#",
                        style     = { marginLeft = "5px"}
                    }
                }
            },

            new div
            {
                style = { display = "flex", flexDirection = "row", margin = "22px"},
                id    = "7-counts",
                children =
                {
                    new div("19 x 280"),
                    new a
                    {
                        innerText = "incele",
                        href      = "#",
                        style     = { marginLeft = "5px"}
                    }
                }
            }
        };

        return new div
        {
           style={ display = "flex", flexDirection = "row"},
           children =
           { 
               containerOfChapters,
               containerOfInitialLetters,
               containerOfCounts,

               CreatePath($"2-{Elif}","2-counts","#a9acaa"),
               CreatePath($"2-{Lam}","2-counts","#a9acaa"),
               CreatePath($"2-{Mim}","2-counts","#a9acaa"),


               CreatePath($"3-{Elif}","3-counts","#a9acaa"),
               CreatePath($"3-{Lam}","3-counts","#a9acaa"),
               CreatePath($"3-{Mim}","3-counts","#a9acaa"),

               CreatePath($"7-{Elif}","7-counts","#a9acaa"),
               CreatePath($"7-{Lam}","7-counts","#a9acaa"),
               CreatePath($"7-{Mim}","7-counts","#a9acaa"),
               CreatePath($"7-{Sad}","7-counts","#a9acaa"),
           }
        };

        return new div
        {
            children =
            {
               

                new div("19 x 344")
                {
                    
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

                // CreatePath("Elif","Result","blue"),
                
                //new Xarrow{start = "Lam", end  = "Result",path  = "smooth", color = "red", strokeWidth    = 1},
                //new Xarrow{start = "Mim", end  = "Result",path  = "smooth",color  = "yellow", strokeWidth = 1},
                //new Xarrow{start = "2-Mim-1", end  = "Result2",path  = "smooth",color  = "green", strokeWidth = 2, labels = "134", startAnchor="right"},
            }
        };



        static Element CreatePath(string startElementId, string endElementId, string color)
        {
            return new Xarrow { start = startElementId, end = endElementId, path = "smooth", color = color, strokeWidth = 1, startAnchor = "bottom"};
        }
        
    }

   
}