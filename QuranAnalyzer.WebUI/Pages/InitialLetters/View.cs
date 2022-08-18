using ReactWithDotNet.react_xarrows;
using static QuranAnalyzer.ArabicLetter;
using static QuranAnalyzer.WebUI.Extensions;

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
            containerDiv.style.border = $"1px solid {SecondBorderColor}";
        }
        
        return containerDiv;
    }
}

class CountingResult: ReactComponent
{
    public string id { get; set; }

    public int MultipleOf { get; set; }

    public string SearchScript { get; set; }

    public bool DirectionIsColumn { get; set; }

    public override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = DirectionIsColumn ? "column":"row", flexWrap = "wrap", marginLeft = "5px", marginTop = "60px" },
            id    = id,
            children =
            {
                new div($"19 x {MultipleOf}"),
                new a
                {
                    innerText = "incele",
                    href      = $"?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={SearchScript}",
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
                display = "flex", margin = "1px"
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
            style = { margin = "3px" },
            
            children =
            {
                new div{innerText  = $"Sure - {ChapterNo}"},
                new HStack
                {
                    style={fontWeight = "600"},
                    children=
                    {
                        new div{ innerText = $"({ChapterName})"}
                    }
                }
            }
        };
    }
}

public class View : ReactComponent
{
    public override Element render()
    {
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
                    new td { new Chapter { ChapterNo = 2, ChapterName = "Bakara" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"2-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"2-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"2-{Miim}", innerText  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "2-counts", MultipleOf = 521, SearchScript = GetLetterCountingScript("2:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 3, ChapterName = "İmran Ailesi" } },

                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"3-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"3-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"3-{Miim}", innerText  = Miim }
                            }
                        },
                    },
                    new td { new CountingResult { id = "3-counts", MultipleOf = 298, SearchScript = GetLetterCountingScript("3:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 7, ChapterName = "Araf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"7-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"7-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"7-{Miim}", innerText  = Miim },
                                new InitialLetter { id = $"7-{Saad}", innerText  = Saad}
                            }
                        }
                    },

                    new td { new CountingResult { id = "7-counts", MultipleOf = 280, SearchScript = GetLetterCountingScript("7:*", Alif, Laam, Miim,Saad) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 10, ChapterName = "Yunus" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"10-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"10-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"10-{Raa}", innerText   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "10-counts", MultipleOf = 131, SearchScript = GetLetterCountingScript("10:*", Alif, Laam, Raa) } }
                },
                new tr
                {
                    new td { new Chapter { ChapterNo = 11, ChapterName = "Hûd" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"11-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"11-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"11-{Raa}", innerText   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "11-counts", MultipleOf = 131, SearchScript = GetLetterCountingScript("11:*", Alif, Laam, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 12, ChapterName = "Yusuf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"12-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"12-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"12-{Raa}", innerText   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "12-counts", MultipleOf = 125, SearchScript = GetLetterCountingScript("12:*", Alif, Laam, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 13, ChapterName = "Rad" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"13-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"13-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"13-{Miim}", innerText  = Miim },
                                new InitialLetter { id = $"13-{Raa}", innerText   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "13-counts", MultipleOf = 78, SearchScript = GetLetterCountingScript("13:*", Alif, Laam,Miim, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 14, ChapterName = "İbrahim" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"14-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"14-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"14-{Raa}", innerText   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "14-counts", MultipleOf = 63, SearchScript = GetLetterCountingScript("14:*", Alif, Laam, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 15, ChapterName = "Hicr" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"15-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"15-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"15-{Raa}", innerText   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "15-counts", MultipleOf = 48, SearchScript = GetLetterCountingScript("15:*", Alif, Laam, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 19, ChapterName = "Meryem" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"19-{Qaaf}", innerText = Qaaf },
                                new InitialLetter { id = $"19-{Haa}", innerText  = Haa_ },
                                new InitialLetter { id = $"19-{Yaa}", innerText = Yaa },
                                new InitialLetter { id = $"19-{Ayn}", innerText = Ayn },
                                new InitialLetter { id = $"19-{Saad}", innerText = Saad }

                            }
                        }
                    },
                    new td { new CountingResult { id = "19-counts", MultipleOf = 42, SearchScript = GetLetterCountingScript("19:*", Kaaf, Haa_, Yaa, Ayn, Saad) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 20, ChapterName = "Taha" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"20-{Taa_}", innerText = Taa_ },
                                new InitialLetter { id = $"20-{Haa}", innerText = Haa }
                            }
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 26, ChapterName = "Şuara" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"26-{Taa_}", innerText  = Taa_ },
                                new InitialLetter { id = $"26-{Siin}", innerText = Siin },
                                new InitialLetter { id = $"26-{Miim}", innerText = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { 
                        id = "TaSinMim", MultipleOf = 93, 
                        
                        SearchScript = GetLetterCountingScript("19:*",  Haa_) + ";" +
                                       GetLetterCountingScript("20:*", Taa_, Haa_) + ";" +
                                       GetLetterCountingScript("26:*", Taa_, Siin, Miim) + ";" +
                                       GetLetterCountingScript("27:*", Taa_, Siin) + ";"+
                                       GetLetterCountingScript("28:*", Taa_, Siin, Miim)
                    } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 27, ChapterName = "Neml" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"27-{Taa_}", innerText  = Taa_ },
                                new InitialLetter { id = $"27-{Siin}", innerText = Siin }
                            }
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 28, ChapterName = "Kasas" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"28-{Taa_}", innerText  = Taa_ },
                                new InitialLetter { id = $"28-{Siin}", innerText = Siin },
                                new InitialLetter { id = $"28-{Miim}", innerText = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "Three-Sad", MultipleOf = 8, SearchScript  = GetLetterCountingScript("7:*,19:*,38:*", Saad) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 29, ChapterName = "Ankebut" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"29-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"29-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"29-{Miim}", innerText  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "29-counts", MultipleOf = 88, SearchScript = GetLetterCountingScript("29:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 30, ChapterName = "Rum" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"30-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"30-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"30-{Miim}", innerText  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "30-counts", MultipleOf = 66, SearchScript = GetLetterCountingScript("30:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 31, ChapterName = "Lokman" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"31-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"31-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"31-{Miim}", innerText  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "31-counts", MultipleOf = 43, SearchScript = GetLetterCountingScript("31:*", Alif, Laam, Miim) } }
                },
                
                new tr
                {
                    new td { new Chapter { ChapterNo = 32, ChapterName = "Secde" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"32-{Alif}", innerText = Alif },
                                new InitialLetter { id = $"32-{Laam}", innerText  = Laam },
                                new InitialLetter { id = $"32-{Miim}", innerText  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "32-counts", MultipleOf = 30, SearchScript = GetLetterCountingScript("32:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 36, ChapterName = "Yasin" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"36-{Yaa}", innerText  = Yaa },
                                new InitialLetter { id = $"36-{Siin}", innerText = Siin }
                            }
                        }
                    },
                    new td { new CountingResult { id = "36-counts", MultipleOf = 15, SearchScript = GetLetterCountingScript("36:*", Yaa, Siin) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 38, ChapterName = "Sad" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Saad}", innerText = Saad }
                            }
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 40, ChapterName = "Mümin" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"40-{Haa}", innerText  = Haa },
                                new InitialLetter { id = $"40-{Miim}", innerText = Miim }
                            }
                        }
                    },
                    new td
                    {
                        
                        new CountingResult { id = "40-41-42", MultipleOf = 59, SearchScript = GetLetterCountingScript("40:*,41:*,42:*", Haa, Miim) },
                        new CountingResult { id = "41-42-43", MultipleOf = 55, SearchScript = GetLetterCountingScript("41:*,42:*,43:*", Haa, Miim)},
                    }
                },
                
                new tr
                {
                    new td
                    {
                        new div
                        {
                            new Chapter { ChapterNo = 41, ChapterName        = "Fussilet" },
                            
                        }
                        
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"41-{Haa}", innerText  = Haa },
                                new InitialLetter { id = $"41-{Miim}", innerText = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "42-Ain-Sin-Kaf", MultipleOf = 11, SearchScript = GetLetterCountingScript("42:*", Ayn, Siin, Qaaf) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 42, ChapterName = "Şura" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"42-{Haa}", innerText  = Haa },
                                new InitialLetter { id = $"42-{Miim}", innerText = Miim },
                                new InitialLetter { id = $"42-{Ayn}", innerText = Ayn },
                                new InitialLetter { id = $"42-{Siin}", innerText = Siin },
                                new InitialLetter { id = $"42-{Qaaf}", innerText = Qaaf }
                            }
                        }
                    },
                    new td { new CountingResult { id = $"42-{Qaaf}-counts", MultipleOf = 3, SearchScript = GetLetterCountingScript("42:*", Qaaf) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 43, ChapterName = "Zuhruf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"43-{Haa}", innerText  = Haa },
                                new InitialLetter { id = $"43-{Miim}", innerText = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "Ha-Mim", MultipleOf = 113, SearchScript = GetLetterCountingScript("40:*,41:*,42:*,43:*,44:*,45:*,46:*", Haa, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 44, ChapterName = "Duhan" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"44-{Haa}", innerText  = Haa },
                                new InitialLetter { id = $"44-{Miim}", innerText = Miim }
                            }
                        }
                    },
                    new td{ new CountingResult { id = "43-44-45-46", MultipleOf = 54, SearchScript = GetLetterCountingScript("43:*,44:*,45:*,46:*", Haa, Miim) } }
                    
                },
                
                new tr
                {
                    new td { new Chapter { ChapterNo = 45, ChapterName = "Casiye" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"45-{Haa}", innerText  = Haa },
                                new InitialLetter { id = $"45-{Miim}", innerText = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "40-44-45-46", MultipleOf = 58, SearchScript = GetLetterCountingScript("40:*,44:*,45:*,46:*", Haa, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 46, ChapterName = "Ahkaf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"46-{Haa}", innerText  = Haa },
                                new InitialLetter { id = $"46-{Miim}", innerText = Miim }
                            }
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 50, ChapterName = "Kaf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"50-{Qaaf}", innerText = Qaaf }
                            }
                        }
                    },
                    new td { new CountingResult { id = "50-counts", MultipleOf = 3, SearchScript = GetLetterCountingScript("50:*", Qaaf) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNo = 68, ChapterName = "Kalem" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"68-{Nun}", innerText = Nun }
                            }
                        }
                    },
                    new td { new CountingResult { id = "68-counts", MultipleOf = 7, SearchScript = GetLetterCountingScript("68:*", Nun) } }
                }
            }

        };



        var colorForConnectedFromOtherChapters = "#66f295";


        return new div
        {
           style={ display = "flex", justifyContent = "center", fontSize = Context.ClientWidth < 400 ? "9px": null },
           children =
           {
               table,

               new Arrow{startElementId=$"2-{Alif}", endElementId = "2-counts"},
               new Arrow{startElementId=$"2-{Laam}",endElementId = "2-counts"},
               new Arrow{startElementId=$"2-{Miim}",endElementId = "2-counts"},


               new Arrow{startElementId=$"3-{Alif}",endElementId = "3-counts"},
               new Arrow{startElementId=$"3-{Laam}",endElementId = "3-counts"},
               new Arrow{startElementId=$"3-{Miim}",endElementId = "3-counts"},

               new Arrow{startElementId =$"7-{Alif}",endElementId = "7-counts"},
               new Arrow{startElementId =$"7-{Laam}",endElementId  = "7-counts"},
               new Arrow{startElementId =$"7-{Miim}",endElementId  = "7-counts"},
               new Arrow{startElementId =$"7-{Saad}",endElementId  = "7-counts"},

               new Arrow{startElementId =$"10-{Alif}",endElementId = "10-counts"},
               new Arrow{startElementId =$"10-{Laam}",endElementId  = "10-counts"},
               new Arrow{startElementId =$"10-{Raa}", endElementId  = "10-counts"},

               new Arrow{startElementId =$"11-{Alif}",endElementId = "11-counts"},
               new Arrow{startElementId =$"11-{Laam}",endElementId  = "11-counts"},
               new Arrow{startElementId =$"11-{Raa}", endElementId  = "11-counts"},

               new Arrow{startElementId =$"12-{Alif}",endElementId = "12-counts"},
               new Arrow{startElementId =$"12-{Laam}",endElementId  = "12-counts"},
               new Arrow{startElementId =$"12-{Raa}", endElementId  = "12-counts"},

               new Arrow{startElementId =$"13-{Alif}",endElementId = "13-counts"},
               new Arrow{startElementId =$"13-{Laam}",endElementId  = "13-counts"},
               new Arrow{startElementId =$"13-{Miim}",endElementId  = "13-counts"},
               new Arrow{startElementId =$"13-{Raa}",endElementId  =  "13-counts"},

               new Arrow{startElementId =$"14-{Alif}",endElementId = "14-counts"},
               new Arrow{startElementId =$"14-{Laam}",endElementId  = "14-counts"},
               new Arrow{startElementId =$"14-{Raa}",endElementId   = "14-counts"},

               new Arrow{startElementId =$"15-{Alif}",endElementId = "15-counts"},
               new Arrow{startElementId =$"15-{Laam}",endElementId  = "15-counts"},
               new Arrow{startElementId =$"15-{Raa}",endElementId   = "15-counts"},

               new Arrow{startElementId=$"19-{Qaaf}",endElementId = "19-counts"},
               new Arrow{startElementId=$"19-{Haa}",endElementId =  "19-counts"},
               new Arrow{startElementId=$"19-{Yaa}",endElementId =  "19-counts"},
               new Arrow{startElementId=$"19-{Ayn}",endElementId = "19-counts"},
               new Arrow{startElementId=$"19-{Saad}",endElementId = "19-counts"},

               new Arrow{startElementId =$"19-{Haa}",endElementId  = "TaSinMim"},
               new Arrow{startElementId =$"20-{Taa_}",endElementId  = "TaSinMim"},
               new Arrow{startElementId =$"20-{Haa}",endElementId  = "TaSinMim"},
               new Arrow{startElementId =$"26-{Taa_}",endElementId  = "TaSinMim"},
               new Arrow{startElementId =$"26-{Siin}",endElementId = "TaSinMim"},
               new Arrow{startElementId =$"26-{Miim}",endElementId = "TaSinMim"},
               new Arrow{startElementId =$"27-{Taa_}",endElementId  = "TaSinMim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"27-{Siin}",endElementId = "TaSinMim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"28-{Taa_}",endElementId  = "TaSinMim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"28-{Siin}",endElementId = "TaSinMim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"28-{Miim}",endElementId = "TaSinMim", StartAnchorFromTop = true},

               new Arrow{startElementId =$"29-{Alif}", endElementId  = "29-counts"},
               new Arrow{startElementId =$"29-{Laam}", endElementId   = "29-counts"},
               new Arrow{startElementId =$"29-{Miim}", endElementId   = "29-counts"},

               new Arrow{startElementId =$"30-{Alif}", endElementId = "30-counts"},
               new Arrow{startElementId =$"30-{Laam}", endElementId  = "30-counts"},
               new Arrow{startElementId =$"30-{Miim}", endElementId  = "30-counts"},

               new Arrow{startElementId =$"31-{Alif}", endElementId = "31-counts"},
               new Arrow{startElementId =$"31-{Laam}", endElementId  = "31-counts"},
               new Arrow{startElementId =$"31-{Miim}", endElementId  = "31-counts"},


               new Arrow{startElementId =$"32-{Alif}", endElementId = "32-counts"},
               new Arrow{startElementId =$"32-{Laam}", endElementId  = "32-counts"},
               new Arrow{startElementId =$"32-{Miim}", endElementId  = "32-counts"},

               new Arrow{startElementId =$"36-{Yaa}", endElementId = "36-counts"},
               new Arrow{startElementId =$"36-{Siin}", endElementId = "36-counts"},

               new Arrow{startElementId =$"7-{Saad}",  endElementId = "Three-Sad", Dashness = true,StartAnchorFromRight  = true,color=colorForConnectedFromOtherChapters},
               new Arrow{startElementId =$"19-{Saad}", endElementId = "Three-Sad",Dashness  = true, StartAnchorFromRight = true,color=colorForConnectedFromOtherChapters},
               new Arrow{startElementId =$"38-{Saad}", endElementId = "Three-Sad",Dashness  = true, StartAnchorFromRight = true,color=colorForConnectedFromOtherChapters},
               
               new Arrow{startElementId =$"42-{Ayn}", endElementId = "42-Ain-Sin-Kaf", StartAnchorFromTop = true},
               new Arrow{startElementId =$"42-{Siin}", endElementId = "42-Ain-Sin-Kaf",StartAnchorFromTop  = true},
               new Arrow{startElementId =$"42-{Qaaf}", endElementId = "42-Ain-Sin-Kaf",StartAnchorFromTop  = true},

               new Arrow{startElementId =$"42-{Qaaf}", endElementId = $"42-{Qaaf}-counts"},

               new Arrow{startElementId =$"40-{Haa}", endElementId  = "Ha-Mim"},
               new Arrow{startElementId =$"40-{Miim}", endElementId = "Ha-Mim"},
               new Arrow{startElementId =$"41-{Haa}", endElementId  = "Ha-Mim"},
               new Arrow{startElementId =$"41-{Miim}", endElementId = "Ha-Mim"},
               new Arrow{startElementId =$"42-{Haa}", endElementId  = "Ha-Mim"},
               new Arrow{startElementId =$"42-{Miim}", endElementId = "Ha-Mim"},
               new Arrow{startElementId =$"43-{Haa}", endElementId  = "Ha-Mim"},
               new Arrow{startElementId =$"43-{Miim}", endElementId = "Ha-Mim"},
               new Arrow{startElementId =$"44-{Haa}", endElementId  = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"44-{Miim}", endElementId = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"45-{Haa}", endElementId  = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"45-{Miim}", endElementId = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"46-{Haa}", endElementId  = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"46-{Miim}", endElementId = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{startElementId =$"46-{Miim}", endElementId = "Ha-Mim", StartAnchorFromTop = true},

               new Arrow{startElementId =$"50-{Qaaf}", endElementId  = "50-counts"},
               new Arrow{startElementId =$"68-{Nun}", endElementId = "68-counts"},


               new Arrow{startElementId =$"40-{Haa}", endElementId  = "40-41-42", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"40-{Miim}", endElementId = "40-41-42", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"41-{Haa}", endElementId  = "40-41-42", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"41-{Miim}", endElementId = "40-41-42", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"42-{Haa}", endElementId  = "40-41-42", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"42-{Miim}", endElementId = "40-41-42", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},

               new Arrow{startElementId =$"43-{Haa}", endElementId  = "43-44-45-46", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"43-{Miim}", endElementId = "43-44-45-46", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"44-{Haa}", endElementId  = "43-44-45-46", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"44-{Miim}", endElementId = "43-44-45-46", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"45-{Haa}", endElementId  = "43-44-45-46", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"45-{Miim}", endElementId = "43-44-45-46", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"46-{Haa}", endElementId  = "43-44-45-46", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},
               new Arrow{startElementId =$"46-{Miim}", endElementId = "43-44-45-46", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},


               new Xarrow{start =$"41-{Haa}",  end  = "41-42-43", startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"41-{Miim}", end = "41-42-43",  startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"42-{Haa}",  end  = "41-42-43", startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"42-{Miim}", end = "41-42-43",  startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"43-{Haa}",  end  = "41-42-43", startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"43-{Miim}", end = "41-42-43",  startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
           }
        };
        


        
        
    }

   
}

class Arrow: ReactComponent
{
   public  string startElementId;
   public  string endElementId;
   public  string color;

   
   public bool StartAnchorFromTop { get; set; }
   public bool StartAnchorFromRight { get; set; }
    

    public bool Dashness { get; set; }

    public double? strokeWidth { get; set; } = 1;

    public override Element render()
    {
        color ??= "#a9acaa";
        
        return new Xarrow
        {
            start       = startElementId,
            end         = endElementId,
            path        = "smooth",
            color       = color,
            strokeWidth = strokeWidth,
            startAnchor = StartAnchorFromTop ? "top" : StartAnchorFromRight ? "right" : "bottom",
            dashness    = true,
            //curveness  = 1.02,
            endAnchor = "left"

        };
    }
}