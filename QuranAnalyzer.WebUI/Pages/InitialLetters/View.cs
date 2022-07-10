using System.Collections.Generic;
using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.FactPage;
using ReactDotNet.Html5;
using ReactDotNet.react_xarrows;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;


class InitialLetter : ReactComponent
{

    public string Label { get; set; }

    public string innerText
    {
        set => Label = value;
    }

    public string id { get; set; }

    public string SecondBorderColor { get; set; }
    
    public override Element render()
    {
        var containerDiv = new div
        {
            style = { borderRadius = "0.5rem", padding = "4px", margin = "10px" },

            children =
            {
                new div
                {
                    style     = { border = "thin solid #a9acaa", borderRadius = "0.5rem", padding = "5px", margin = "10px" },
                    id        = id,
                    innerText = Label
                }
            }
        };

        if (SecondBorderColor is not null)
        {
            containerDiv.style.border = $"thin solid {SecondBorderColor}";
        }
        
        return containerDiv;
    }
}

class CountingResult: ReactComponent
{
    public string id { get; set; }

    public int MultipleOf { get; set; }

    public override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "row", marginLeft = "5px", marginTop = "-40px" },
            id    = id,
            children =
            {
                new div($"19 x {MultipleOf}"),
                new a
                {
                    innerText = "incele",
                    href      = "#",
                    style     = { marginLeft = "5px" }
                }
            }
        };
    }
}

class InitialLetterLineGroup: ReactComponent
{
    public List<InitialLetter> Items { get; } = new();

    
    public override Element render()
    {
        return new div
        {
            style =
            {
                display = "flex", margin = "6px"
            },
            Children = Items
        };
    }
}

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
        const string Kaf  = "Kāf";
        const string Ha   = "Hā";
        const string Ya   = "Yāʾ";
        const string Ain = "ʿAin";


        var elifLamMim_2 = new Style { border = "thin solid #a9acaa", borderRadius = "0.5rem", padding = "5px", margin = "10px"};


        var table = new table
        {
            new tbody
            {
                new tr
                {
                    new th { innerText = "Sure" },
                    new th { innerText = "Başlangıç Harfleri" },
                    new th { innerText = "Sayım Sonuçları" }
                },
                new tr
                {
                    new td
                    {
                        new div { innerText = "Sure - 2 (Bakara)", style = { margin = "5px" } },
                    },

                    new td
                    {
                        new div
                        {
                            style =
                            {
                                display = "flex", margin = "6px"
                            },
                            children =
                            {
                                new InitialLetter
                                {
                                    id        = $"2-{Elif}",
                                    innerText = Elif
                                },

                                new InitialLetter
                                {
                                    id        = $"2-{Lam}",
                                    innerText = Lam
                                },

                                new InitialLetter
                                {
                                    id        = $"2-{Mim}",
                                    innerText = Mim
                                }

                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id    = "2-counts",
                            MultipleOf = 521
                        },
                    }
                },

                new tr
                {
                    new td
                    {
                        new div { innerText = "Sure - 3 (İmran Ailesi)", style = { margin = "5px" } },
                    },

                    new td
                    {
                        new div
                        {
                            style =
                            {
                                display = "flex", margin = "6px"
                            },
                            children =
                            {
                                new InitialLetter
                                {
                                    id        = $"3-{Elif}",
                                    innerText = Elif
                                },

                                new InitialLetter
                                {
                                    id        = $"3-{Lam}",
                                    innerText = Lam
                                },

                                new InitialLetter
                                {
                                    id        = $"3-{Mim}",
                                    innerText = Mim
                                }

                            }
                        },
                    },
                    new td
                    {
                        new CountingResult
                        {
                            id    = "3-counts",
                            MultipleOf = 298
                        }
                    }
                },

                new tr
                {
                    new td
                    {
                        new div { innerText = "Sure - 7 (Araf)", style = { margin = "5px" } }
                    },
                    new td
                    {
                        new div
                        {
                            style =
                            {
                                display = "flex", margin = "6px"
                            },
                            children =
                            {
                                new InitialLetter
                                {
                                    id        = $"7-{Elif}",
                                    innerText = Elif
                                },

                               new InitialLetter
                               {
                                   id        = $"7-{Lam}",
                                   innerText = Lam
                               },

                               new InitialLetter
                               {
                                   id        = $"7-{Mim}",
                                   innerText = Mim
                               },

                               new InitialLetter
                               {
                                   id        = $"7-{Sad}",
                                   innerText = Sad
                               }


                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id = "7-counts",
                            MultipleOf = 280
                        }
                    }
                },


                new tr
                {
                    new td
                    {
                        new div { innerText = "Sure - 19 (Meryem)", style = { margin = "5px" } },
                    },

                    new td
                    {
                        new div
                        {
                            style =
                            {
                                display = "flex", margin = "6px"
                            },
                            children =
                            {
                                new InitialLetter
                                {
                                    id        = $"19-{Kaf}",
                                    innerText = Kaf
                                },

                                new InitialLetter
                                {
                                    id        = $"19-{Ha}",
                                    innerText = Ha
                                },

                                new InitialLetter
                                {
                                    id        = $"19-{Ya}",
                                    innerText = Ya
                                },

                                new InitialLetter
                                {
                                    id        = $"19-{Ain}",
                                    innerText = Ain
                                },

                                new InitialLetter
                                {
                                    id        = $"19-{Sad}",
                                    innerText = Sad
                                }

                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id         = "19-counts",
                            MultipleOf = 42
                        }
                    }
                },
            }

        };
        
       
       


        return new div
        {
           style={ display = "flex", flexDirection = "row"},
           children =
           {
               table,

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

               CreatePath($"19-{Kaf}","19-counts","#a9acaa"),
               CreatePath($"19-{Ha}", "19-counts","#a9acaa"),
               CreatePath($"19-{Ya}", "19-counts","#a9acaa"),
               CreatePath($"19-{Ain}","19-counts","#a9acaa"),
               CreatePath($"19-{Sad}","19-counts","#a9acaa"),



               CreatePath($"7-{Sad}","19-counts","yellow"),
               CreatePath($"19-{Sad}","19-counts","yellow"),
           }
        };
        



        static Element CreatePath(string startElementId, string endElementId, string color)
        {
            return new Xarrow
            {
                start = startElementId, end = endElementId, path = "smooth", 
                color = color, strokeWidth = 1, startAnchor = "top",
                
            };
        }
        
    }

   
}