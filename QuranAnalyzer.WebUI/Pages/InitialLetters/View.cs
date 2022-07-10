using System.Collections.Generic;
using ReactDotNet.Html5;
using ReactDotNet.react_xarrows;

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
            style = { borderRadius = "0.5rem", padding = "4px", margin = "1px" },

            children =
            {
                new div
                {
                    style     = { border = "thin solid #a9acaa", borderRadius = "0.5rem", padding = "5px", margin = "5px" },
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

class Chapter : ReactComponent
{
    public int ChapterNo { get; set; }
    
    public string ChapterName { get; set; }

    public override Element render()
    {
        return new div
        {
            style = { margin = "5px" },
            
            children =
            {
                new div{innerText  = $"Sure - {ChapterNo}"},
                new HPanel(new Style{ fontWeight = "600"})
                {
                    new div{ innerText = "("},
                    new div{ innerText = ChapterName, style = { fontStyle = "bold"}},
                    new div{ innerText = ")"}
                }
            }
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
        const string Ain  = "ʿAin";
        const string Ra   = "Rāʾ";
        const string Ta   = "Ṭāʾ";
        const string Sin  = "Sīn";
        const string Nun  = "Nūn";


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
                    new td { new Chapter{ ChapterNo = 2, ChapterName = "Bakara"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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

                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td
                    {
                        new Chapter{ ChapterNo = 3, ChapterName = "İmran Ailesi"}
                    },

                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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
                        new Chapter{ ChapterNo = 7, ChapterName = "Araf"}
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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
                        new Chapter{ ChapterNo = 10, ChapterName = "___"}
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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
                                    id        = $"7-{Ra}",
                                    innerText = Mim
                                }
                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id         = "10-counts",
                            MultipleOf = 280
                        }
                    }
                },
                new tr
                {
                    new td
                    {
                        new Chapter{ ChapterNo = 11, ChapterName = "___"}
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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
                                    id        = $"7-{Ra}",
                                    innerText = Mim
                                }
                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id         = "10-counts",
                            MultipleOf = 280
                        }
                    }
                },

                new tr
                {
                    new td
                    {
                        new Chapter{ ChapterNo = 12, ChapterName = "___"}
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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
                                    id        = $"7-{Ra}",
                                    innerText = Mim
                                }
                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id         = "10-counts",
                            MultipleOf = 280
                        }
                    }
                },

                new tr
                {
                    new td
                    {
                        new Chapter{ ChapterNo = 13, ChapterName = "___"}
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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
                                    id        = $"7-{Ra}",
                                    innerText = Mim
                                }
                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id         = "10-counts",
                            MultipleOf = 280
                        }
                    }
                },

                new tr
                {
                    new td
                    {
                        new Chapter{ ChapterNo = 14, ChapterName = "___"}
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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
                                    id        = $"7-{Ra}",
                                    innerText = Mim
                                }
                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id         = "10-counts",
                            MultipleOf = 280
                        }
                    }
                },

                new tr
                {
                    new td
                    {
                        new Chapter{ ChapterNo = 15, ChapterName = "___"}
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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
                                    id        = $"7-{Ra}",
                                    innerText = Mim
                                }
                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id         = "10-counts",
                            MultipleOf = 280
                        }
                    }
                },

                new tr
                {
                    new td
                    {
                        new Chapter{ ChapterNo = 19, ChapterName = "Meryem"}
                    },

                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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

                new tr
                {
                    new td
                    {
                        new Chapter{ ChapterNo = 20, ChapterName = "Ta-ha"}
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter
                                {
                                    id        = $"7-{Ta}",
                                    innerText = Elif
                                },

                                new InitialLetter
                                {
                                    id        = $"7-{Ha}",
                                    innerText = Lam
                                }
                            }
                        }
                    },

                    new td
                    {
                        new CountingResult
                        {
                            id         = "10-counts",
                            MultipleOf = 280
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 26, ChapterName = "Ta-Sin-Mim"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"7-{Ta}", innerText  = Elif },
                                new InitialLetter { id = $"7-{Sin}", innerText = Sin },
                                new InitialLetter { id = $"7-{Mim}", innerText = Mim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "10-counts", MultipleOf = 280 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 27, ChapterName = "Ta-Sin"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"27-{Ta}", innerText  = Ta },
                                new InitialLetter { id = $"27-{Sin}", innerText = Sin },
                            }
                        }
                    },

                    new td { new CountingResult { id = "10-counts", MultipleOf = 280 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 28, ChapterName = "Ta-Sin-Mim"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"7-{Ta}", innerText  = Elif },
                                new InitialLetter { id = $"7-{Sin}", innerText = Sin },
                                new InitialLetter { id = $"7-{Mim}", innerText = Mim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "10-counts", MultipleOf = 280 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 29, ChapterName = "Bakara"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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

                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 30, ChapterName = "Bakara"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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

                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 32, ChapterName = "Bakara"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
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

                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 36, ChapterName = "Yasin"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter
                                {
                                    id        = $"36-{Ya}",
                                    innerText = Ya
                                },

                                new InitialLetter
                                {
                                    id        = $"36-{Sin}",
                                    innerText = Sin
                                }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 38, ChapterName = "Sad"} },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter
                                {
                                    id        = $"38-{Sad}",
                                    innerText = Sad
                                }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 40, ChapterName = "Ḥāʾ–Mīm" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Ha}", innerText = Ha },
                                new InitialLetter { id = $"38-{Mim}", innerText = Mim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },
                new tr
                {
                    new td { new Chapter{ ChapterNo = 41, ChapterName = "Ḥāʾ–Mīm" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Ha}", innerText  = Ha },
                                new InitialLetter { id = $"38-{Mim}", innerText = Mim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 42, ChapterName = "Ḥāʾ–Mīm" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Ha}", innerText  = Ha },
                                new InitialLetter { id = $"38-{Mim}", innerText = Mim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 43, ChapterName = "Ḥāʾ–Mīm" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Ha}", innerText  = Ha },
                                new InitialLetter { id = $"38-{Mim}", innerText = Mim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 44, ChapterName = "Ḥāʾ–Mīm" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Ha}", innerText  = Ha },
                                new InitialLetter { id = $"38-{Mim}", innerText = Mim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },
                new tr
                {
                    new td { new Chapter{ ChapterNo = 45, ChapterName = "Ḥāʾ–Mīm" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Ha}", innerText  = Ha },
                                new InitialLetter { id = $"38-{Mim}", innerText = Mim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 46, ChapterName = "Ḥāʾ–Mīm" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Ha}", innerText  = Ha },
                                new InitialLetter { id = $"38-{Mim}", innerText = Mim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 50, ChapterName = "Qāf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"50-{Kaf}", innerText  = Kaf }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                },

                new tr
                {
                    new td { new Chapter{ ChapterNo = 68, ChapterName = "Nun" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"50-{Nun}", innerText = Nun }
                            }
                        }
                    },
                    new td { new CountingResult { id = "2-counts", MultipleOf = 521 } }
                }
            }

        };
        
       
       


        return new div
        {
           style={ display = "flex", flexDirection = "row"},
           children =
           {
               table,

               new Arrow{startElementId=$"2-{Elif}", endElementId = "2-counts", color="#a9acaa"},
               new Arrow{startElementId=$"2-{Lam}",endElementId = "2-counts",color="#a9acaa"},
               new Arrow{startElementId=$"2-{Mim}",endElementId = "2-counts",color="#a9acaa"},


               new Arrow{startElementId=$"3-{Elif}",endElementId = "3-counts",color="#a9acaa"},
               new Arrow{startElementId=$"3-{Lam}",endElementId = "3-counts",color="#a9acaa"},
               new Arrow{startElementId=$"3-{Mim}",endElementId = "3-counts",color="#a9acaa"},

               new Arrow{startElementId=$"7-{Elif}",endElementId = "7-counts",color="#a9acaa"},
               new Arrow{startElementId=$"7-{Lam}",endElementId = "7-counts",color="#a9acaa"},
               new Arrow{startElementId=$"7-{Mim}",endElementId = "7-counts",color="#a9acaa"},
               new Arrow{startElementId=$"7-{Sad}",endElementId = "7-counts",color="#a9acaa"},

               new Arrow{startElementId=$"19-{Kaf}",endElementId = "19-counts",color="#a9acaa"},
               new Arrow{startElementId=$"19-{Ha}",endElementId =  "19-counts",color="#a9acaa"},
               new Arrow{startElementId=$"19-{Ya}",endElementId =  "19-counts",color="#a9acaa"},
               new Arrow{startElementId=$"19-{Ain}",endElementId = "19-counts",color="#a9acaa"},
               new Arrow{startElementId=$"19-{Sad}",endElementId = "19-counts",color="#a9acaa"},



           }
        };
        


        
        
    }

   
}

class Arrow: ReactComponent
{
   public  string startElementId;
   public  string endElementId;
   public  string color;
   
    public override Element render()
    {
        return new Xarrow
        {
            start       = startElementId,
            end         = endElementId,
            path        = "smooth",
            color       = color,
            strokeWidth = 1,
            startAnchor = "top",

        };
    }
}