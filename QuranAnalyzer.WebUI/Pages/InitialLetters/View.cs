using ReactWithDotNet.react_xarrows;
using static QuranAnalyzer.ArabicLetter;
using static QuranAnalyzer.WebUI.Extensions;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;


class InitialLetter : ReactComponent
{
    public string text { get; set; }

    public string id { get; set; }

    public bool IsSelected { get; set; }
    
    public override Element render()
    {
        var color = "#a9acaa";
        if (IsSelected)
        {
            color = "red";
        }
        
        return new div
        {
            style = { border = $"{(IsSelected?2:1)}px solid {color}", borderRadius = "0.5rem", padding = "5px" },
            id    = id,
            text  = text
        };
    }
}

class CountingResult: ReactComponent
{
    public string id { get; set; }

    public int MultipleOf { get; set; }

    public string SearchScript { get; set; }

    public override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "row", flexWrap = "wrap"},
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
                display = "flex", margin = "1px", justifyContent = "space-evenly"
            },
            Children = Items
        };
    }
}

class Chapter : ReactComponent
{
    public int ChapterNumber { get; set; }
    
    public string ChapterName { get; set; }

    public override Element render()
    {
        return new div
        {
            style = { margin = "3px", textAlign = "center"},
            
            children =
            {
                new div{innerText = $"Sure - {ChapterNumber}"},
                new div{ text     = $"({ChapterName})", style ={fontWeight = "500"}}
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
                    new td { new Chapter { ChapterNumber = 2, ChapterName = "Bakara" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"2-{Alif}", text = Alif },
                                new InitialLetter { id = $"2-{Laam}", text  = Laam },
                                new InitialLetter { id = $"2-{Miim}", text  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "2-counts", MultipleOf = 521, SearchScript = GetLetterCountingScript("2:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 3, ChapterName = "İmran Ailesi" } },

                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"3-{Alif}", text = Alif },
                                new InitialLetter { id = $"3-{Laam}", text  = Laam },
                                new InitialLetter { id = $"3-{Miim}", text  = Miim }
                            }
                        },
                    },
                    new td { new CountingResult { id = "3-counts", MultipleOf = 298, SearchScript = GetLetterCountingScript("3:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 7, ChapterName = "Araf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"7-{Alif}", text = Alif },
                                new InitialLetter { id = $"7-{Laam}", text  = Laam },
                                new InitialLetter { id = $"7-{Miim}", text  = Miim },
                                new InitialLetter { id = $"7-{Saad}", text  = Saad}
                            }
                        }
                    },

                    new td { new CountingResult { id = "7-counts", MultipleOf = 280, SearchScript = GetLetterCountingScript("7:*", Alif, Laam, Miim,Saad) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 10, ChapterName = "Yunus" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"10-{Alif}", text = Alif },
                                new InitialLetter { id = $"10-{Laam}", text  = Laam },
                                new InitialLetter { id = $"10-{Raa}", text   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "10-counts", MultipleOf = 131, SearchScript = GetLetterCountingScript("10:*", Alif, Laam, Raa) } }
                },
                new tr
                {
                    new td { new Chapter { ChapterNumber = 11, ChapterName = "Hûd" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"11-{Alif}", text = Alif },
                                new InitialLetter { id = $"11-{Laam}", text  = Laam },
                                new InitialLetter { id = $"11-{Raa}", text   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "11-counts", MultipleOf = 131, SearchScript = GetLetterCountingScript("11:*", Alif, Laam, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 12, ChapterName = "Yusuf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"12-{Alif}", text = Alif },
                                new InitialLetter { id = $"12-{Laam}", text  = Laam },
                                new InitialLetter { id = $"12-{Raa}", text   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "12-counts", MultipleOf = 125, SearchScript = GetLetterCountingScript("12:*", Alif, Laam, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 13, ChapterName = "Rad" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"13-{Alif}", text = Alif },
                                new InitialLetter { id = $"13-{Laam}", text  = Laam },
                                new InitialLetter { id = $"13-{Miim}", text  = Miim },
                                new InitialLetter { id = $"13-{Raa}", text   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "13-counts", MultipleOf = 78, SearchScript = GetLetterCountingScript("13:*", Alif, Laam,Miim, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 14, ChapterName = "İbrahim" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"14-{Alif}", text = Alif },
                                new InitialLetter { id = $"14-{Laam}", text  = Laam },
                                new InitialLetter { id = $"14-{Raa}", text   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "14-counts", MultipleOf = 63, SearchScript = GetLetterCountingScript("14:*", Alif, Laam, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 15, ChapterName = "Hicr" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"15-{Alif}", text = Alif },
                                new InitialLetter { id = $"15-{Laam}", text  = Laam },
                                new InitialLetter { id = $"15-{Raa}", text   = Raa }
                            }
                        }
                    },

                    new td { new CountingResult { id = "15-counts", MultipleOf = 48, SearchScript = GetLetterCountingScript("15:*", Alif, Laam, Raa) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 19, ChapterName = "Meryem" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"19-{Qaaf}", text = Qaaf },
                                new InitialLetter { id = $"19-{Haa}", text  = Haa_ },
                                new InitialLetter { id = $"19-{Yaa}", text = Yaa },
                                new InitialLetter { id = $"19-{Ayn}", text = Ayn },
                                new InitialLetter { id = $"19-{Saad}", text = Saad }

                            }
                        }
                    },
                    new td { new CountingResult { id = "19-counts", MultipleOf = 42, SearchScript = GetLetterCountingScript("19:*", Kaaf, Haa_, Yaa, Ayn, Saad) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 20, ChapterName = "Taha" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"20-{Taa_}", text = Taa_ },
                                new InitialLetter { id = $"20-{Haa}", text = Haa }
                            }
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 26, ChapterName = "Şuara" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"26-{Taa_}", text  = Taa_ },
                                new InitialLetter { id = $"26-{Siin}", text = Siin },
                                new InitialLetter { id = $"26-{Miim}", text = Miim }
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
                    new td { new Chapter { ChapterNumber = 27, ChapterName = "Neml" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"27-{Taa_}", text  = Taa_ },
                                new InitialLetter { id = $"27-{Siin}", text = Siin }
                            }
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 28, ChapterName = "Kasas" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"28-{Taa_}", text  = Taa_ },
                                new InitialLetter { id = $"28-{Siin}", text = Siin },
                                new InitialLetter { id = $"28-{Miim}", text = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "Three-Sad", MultipleOf = 8, SearchScript  = GetLetterCountingScript("7:*,19:*,38:*", Saad) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 29, ChapterName = "Ankebut" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"29-{Alif}", text = Alif },
                                new InitialLetter { id = $"29-{Laam}", text  = Laam },
                                new InitialLetter { id = $"29-{Miim}", text  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "29-counts", MultipleOf = 88, SearchScript = GetLetterCountingScript("29:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 30, ChapterName = "Rum" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"30-{Alif}", text = Alif },
                                new InitialLetter { id = $"30-{Laam}", text  = Laam },
                                new InitialLetter { id = $"30-{Miim}", text  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "30-counts", MultipleOf = 66, SearchScript = GetLetterCountingScript("30:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 31, ChapterName = "Lokman" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"31-{Alif}", text = Alif },
                                new InitialLetter { id = $"31-{Laam}", text  = Laam },
                                new InitialLetter { id = $"31-{Miim}", text  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "31-counts", MultipleOf = 43, SearchScript = GetLetterCountingScript("31:*", Alif, Laam, Miim) } }
                },
                
                new tr
                {
                    new td { new Chapter { ChapterNumber = 32, ChapterName = "Secde" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"32-{Alif}", text = Alif },
                                new InitialLetter { id = $"32-{Laam}", text  = Laam },
                                new InitialLetter { id = $"32-{Miim}", text  = Miim }
                            }
                        }
                    },

                    new td { new CountingResult { id = "32-counts", MultipleOf = 30, SearchScript = GetLetterCountingScript("32:*", Alif, Laam, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 36, ChapterName = "Yasin" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"36-{Yaa}", text  = Yaa },
                                new InitialLetter { id = $"36-{Siin}", text = Siin }
                            }
                        }
                    },
                    new td { new CountingResult { id = "36-counts", MultipleOf = 15, SearchScript = GetLetterCountingScript("36:*", Yaa, Siin) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 38, ChapterName = "Sad" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"38-{Saad}", text = Saad }
                            }
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 40, ChapterName = "Mümin" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"40-{Haa}", text  = Haa },
                                new InitialLetter { id = $"40-{Miim}", text = Miim }
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
                            new Chapter { ChapterNumber = 41, ChapterName        = "Fussilet" },
                            
                        }
                        
                    },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"41-{Haa}", text  = Haa },
                                new InitialLetter { id = $"41-{Miim}", text = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "42-Ain-Sin-Kaf", MultipleOf = 11, SearchScript = GetLetterCountingScript("42:*", Ayn, Siin, Qaaf) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 42, ChapterName = "Şura" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"42-{Haa}", text  = Haa },
                                new InitialLetter { id = $"42-{Miim}", text = Miim },
                                new InitialLetter { id = $"42-{Ayn}", text = Ayn },
                                new InitialLetter { id = $"42-{Siin}", text = Siin },
                                new InitialLetter { id = $"42-{Qaaf}", text = Qaaf }
                            }
                        }
                    },
                    new td { new CountingResult { id = $"42-{Qaaf}-counts", MultipleOf = 3, SearchScript = GetLetterCountingScript("42:*", Qaaf) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 43, ChapterName = "Zuhruf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"43-{Haa}", text  = Haa },
                                new InitialLetter { id = $"43-{Miim}", text = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "Ha-Mim", MultipleOf = 113, SearchScript = GetLetterCountingScript("40:*,41:*,42:*,43:*,44:*,45:*,46:*", Haa, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 44, ChapterName = "Duhan" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"44-{Haa}", text  = Haa },
                                new InitialLetter { id = $"44-{Miim}", text = Miim }
                            }
                        }
                    },
                    new td{ new CountingResult { id = "43-44-45-46", MultipleOf = 54, SearchScript = GetLetterCountingScript("43:*,44:*,45:*,46:*", Haa, Miim) } }
                    
                },
                
                new tr
                {
                    new td { new Chapter { ChapterNumber = 45, ChapterName = "Casiye" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"45-{Haa}", text  = Haa },
                                new InitialLetter { id = $"45-{Miim}", text = Miim }
                            }
                        }
                    },
                    new td { new CountingResult { id = "40-44-45-46", MultipleOf = 58, SearchScript = GetLetterCountingScript("40:*,44:*,45:*,46:*", Haa, Miim) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 46, ChapterName = "Ahkaf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"46-{Haa}", text  = Haa },
                                new InitialLetter { id = $"46-{Miim}", text = Miim }
                            }
                        }
                    }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 50, ChapterName = "Kaf" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"50-{Qaaf}", text = Qaaf }
                            }
                        }
                    },
                    new td { new CountingResult { id = "50-counts", MultipleOf = 3, SearchScript = GetLetterCountingScript("50:*", Qaaf) } }
                },

                new tr
                {
                    new td { new Chapter { ChapterNumber = 68, ChapterName = "Kalem" } },
                    new td
                    {
                        new InitialLetterLineGroup
                        {
                            Items =
                            {
                                new InitialLetter { id = $"68-{Nun}", text = Nun }
                            }
                        }
                    },
                    new td { new CountingResult { id = "68-counts", MultipleOf = 7, SearchScript = GetLetterCountingScript("68:*", Nun) } }
                }
            }

        };



        var colorForConnectedFromOtherChapters = "#66f295";


        var yy =  new div
        {
           style={ display = "flex", justifyContent = "center", fontSize = Context.ClientWidth < 400 ? "9px": null },
           children =
           {
               table,

               new Arrow{start=$"2-{Alif}", end = "2-counts"},
               new Arrow{start=$"2-{Laam}",end = "2-counts"},
               new Arrow{start=$"2-{Miim}",end = "2-counts"},


               new Arrow{start=$"3-{Alif}",end = "3-counts"},
               new Arrow{start=$"3-{Laam}",end = "3-counts"},
               new Arrow{start=$"3-{Miim}",end = "3-counts"},

               new Arrow{start =$"7-{Alif}",end = "7-counts"},
               new Arrow{start =$"7-{Laam}",end  = "7-counts"},
               new Arrow{start =$"7-{Miim}",end  = "7-counts"},
               new Arrow{start =$"7-{Saad}",end  = "7-counts"},

               new Arrow{start =$"10-{Alif}",end = "10-counts"},
               new Arrow{start =$"10-{Laam}",end  = "10-counts"},
               new Arrow{start =$"10-{Raa}", end  = "10-counts"},

               new Arrow{start =$"11-{Alif}",end = "11-counts"},
               new Arrow{start =$"11-{Laam}",end  = "11-counts"},
               new Arrow{start =$"11-{Raa}", end  = "11-counts"},

               new Arrow{start =$"12-{Alif}",end = "12-counts"},
               new Arrow{start =$"12-{Laam}",end  = "12-counts"},
               new Arrow{start =$"12-{Raa}", end  = "12-counts"},

               new Arrow{start =$"13-{Alif}",end = "13-counts"},
               new Arrow{start =$"13-{Laam}",end  = "13-counts"},
               new Arrow{start =$"13-{Miim}",end  = "13-counts"},
               new Arrow{start =$"13-{Raa}",end  =  "13-counts"},

               new Arrow{start =$"14-{Alif}",end = "14-counts"},
               new Arrow{start =$"14-{Laam}",end  = "14-counts"},
               new Arrow{start =$"14-{Raa}",end   = "14-counts"},

               new Arrow{start =$"15-{Alif}",end = "15-counts"},
               new Arrow{start =$"15-{Laam}",end  = "15-counts"},
               new Arrow{start =$"15-{Raa}",end   = "15-counts"},

               new Arrow{start=$"19-{Qaaf}",end = "19-counts"},
               new Arrow{start=$"19-{Haa}",end =  "19-counts"},
               new Arrow{start=$"19-{Yaa}",end =  "19-counts"},
               new Arrow{start=$"19-{Ayn}",end = "19-counts"},
               new Arrow{start=$"19-{Saad}",end = "19-counts"},

               new Arrow{start =$"19-{Haa}",end  = "TaSinMim"},
               new Arrow{start =$"20-{Taa_}",end  = "TaSinMim"},
               new Arrow{start =$"20-{Haa}",end  = "TaSinMim"},
               new Arrow{start =$"26-{Taa_}",end  = "TaSinMim"},
               new Arrow{start =$"26-{Siin}",end = "TaSinMim"},
               new Arrow{start =$"26-{Miim}",end = "TaSinMim"},
               new Arrow{start =$"27-{Taa_}",end  = "TaSinMim", StartAnchorFromTop = true},
               new Arrow{start =$"27-{Siin}",end = "TaSinMim", StartAnchorFromTop = true},
               new Arrow{start =$"28-{Taa_}",end  = "TaSinMim", StartAnchorFromTop = true},
               new Arrow{start =$"28-{Siin}",end = "TaSinMim", StartAnchorFromTop = true},
               new Arrow{start =$"28-{Miim}",end = "TaSinMim", StartAnchorFromTop = true},

               new Arrow{start =$"29-{Alif}", end  = "29-counts"},
               new Arrow{start =$"29-{Laam}", end   = "29-counts"},
               new Arrow{start =$"29-{Miim}", end   = "29-counts"},

               new Arrow{start =$"30-{Alif}", end = "30-counts"},
               new Arrow{start =$"30-{Laam}", end  = "30-counts"},
               new Arrow{start =$"30-{Miim}", end  = "30-counts"},

               new Arrow{start =$"31-{Alif}", end = "31-counts"},
               new Arrow{start =$"31-{Laam}", end  = "31-counts"},
               new Arrow{start =$"31-{Miim}", end  = "31-counts"},


               new Arrow{start =$"32-{Alif}", end = "32-counts"},
               new Arrow{start =$"32-{Laam}", end  = "32-counts"},
               new Arrow{start =$"32-{Miim}", end  = "32-counts"},

               new Arrow{start =$"36-{Yaa}", end = "36-counts"},
               new Arrow{start =$"36-{Siin}", end = "36-counts"},

               new Arrow{start =$"7-{Saad}",  end = "Three-Sad", dashness = true,StartAnchorFromRight  = true,color=colorForConnectedFromOtherChapters},
               new Arrow{start =$"19-{Saad}", end = "Three-Sad",dashness  = true, StartAnchorFromRight = true,color=colorForConnectedFromOtherChapters},
               new Arrow{start =$"38-{Saad}", end = "Three-Sad",dashness  = true, StartAnchorFromRight = true,color=colorForConnectedFromOtherChapters},
               
               new Arrow{start =$"42-{Ayn}", end = "42-Ain-Sin-Kaf", StartAnchorFromTop = true},
               new Arrow{start =$"42-{Siin}", end = "42-Ain-Sin-Kaf",StartAnchorFromTop  = true},
               new Arrow{start =$"42-{Qaaf}", end = "42-Ain-Sin-Kaf",StartAnchorFromTop  = true},

               new Arrow{start =$"42-{Qaaf}", end = $"42-{Qaaf}-counts"},

               new Arrow{start =$"40-{Haa}", end  = "Ha-Mim"},
               new Arrow{start =$"40-{Miim}", end = "Ha-Mim"},
               new Arrow{start =$"41-{Haa}", end  = "Ha-Mim"},
               new Arrow{start =$"41-{Miim}", end = "Ha-Mim"},
               new Arrow{start =$"42-{Haa}", end  = "Ha-Mim"},
               new Arrow{start =$"42-{Miim}", end = "Ha-Mim"},
               new Arrow{start =$"43-{Haa}", end  = "Ha-Mim"},
               new Arrow{start =$"43-{Miim}", end = "Ha-Mim"},
               new Arrow{start =$"44-{Haa}", end  = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{start =$"44-{Miim}", end = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{start =$"45-{Haa}", end  = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{start =$"45-{Miim}", end = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{start =$"46-{Haa}", end  = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{start =$"46-{Miim}", end = "Ha-Mim", StartAnchorFromTop = true},
               new Arrow{start =$"46-{Miim}", end = "Ha-Mim", StartAnchorFromTop = true},

               new Arrow{start =$"50-{Qaaf}", end  = "50-counts"},
               new Arrow{start =$"68-{Nun}", end = "68-counts"},


               new Arrow{start =$"40-{Haa}", end  = "40-41-42", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"40-{Miim}", end = "40-41-42", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"41-{Haa}", end  = "40-41-42", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"41-{Miim}", end = "40-41-42", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"42-{Haa}", end  = "40-41-42", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"42-{Miim}", end = "40-41-42", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},

               new Arrow{start =$"43-{Haa}", end  = "43-44-45-46", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"43-{Miim}", end = "43-44-45-46", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"44-{Haa}", end  = "43-44-45-46", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"44-{Miim}", end = "43-44-45-46", StartAnchorFromRight =true, color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"45-{Haa}", end  = "43-44-45-46", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"45-{Miim}", end = "43-44-45-46", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"46-{Haa}", end  = "43-44-45-46", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},
               new Arrow{start =$"46-{Miim}", end = "43-44-45-46", StartAnchorFromRight = true,color = "blue",strokeWidth = 0.8},


               new Xarrow{start =$"41-{Haa}",  end  = "41-42-43", startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"41-{Miim}", end = "41-42-43",  startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"42-{Haa}",  end  = "41-42-43", startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"42-{Miim}", end = "41-42-43",  startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"43-{Haa}",  end  = "41-42-43", startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
               new Xarrow{start =$"43-{Miim}", end = "41-42-43",  startAnchor="top",endAnchor="left", color = "red",strokeWidth = 1},
           }
        };


        return new AllInitialLetters();


    }

   
}

class Arrow: ReactComponent
{
   public  string start;
   public  string end;
   public  string color;

   
   public bool StartAnchorFromTop { get; set; }
   public bool StartAnchorFromRight { get; set; }
    

    public bool dashness { get; set; }

    public double? strokeWidth { get; set; } = 1;

    public override Element render()
    {
        color ??= "#a9acaa";
        
        return new Xarrow
        {
            start       = start,
            end         = end,
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


abstract class InitialLetterGroup : ReactComponent
{
    protected tr HeaderTr => new tr
    {
        new th { innerText = "Sure" },
        new th { innerText = "Başlangıç Harfleri" },
        new th { innerText = "Sayım Sonuçları" }
    };

    protected tr RowSpace => new tr { style = { height = "10px" } };

    protected tr HeaderSpace => new tr { style = { height = "15px" } };
}

class InitialLetterGroup_Saad: InitialLetterGroup
{
    static string Id(int chapterNumber, string letter) => $"ThreeSaad-{chapterNumber}-{letter}";

    static string IdOfCountingResult => $"ThreeSaad-{nameof(IdOfCountingResult)}";

    public override Element render()
    {
        return new div
        {

            new table
            {
                style = { width = "100%" },
                children =
                {
                    new tbody
                    {
                        HeaderTr,
                        HeaderSpace,
                        new tr
                        {
                            new td { new Chapter { ChapterNumber = 7, ChapterName = "Araf" } },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(7,Alif), text = Alif },
                                        new InitialLetter { id = Id(7,Laam), text = Laam },
                                        new InitialLetter { id = Id(7,Miim), text = Miim },
                                        new InitialLetter { id = Id(7,Saad), text = Saad, IsSelected = true }
                                    }
                                }
                            }
                        },
                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 19, ChapterName = "Meryem" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(19,Qaaf), text = Qaaf },
                                        new InitialLetter { id = Id(19,Haa), text  = Haa_ },
                                        new InitialLetter { id = Id(19,Yaa), text  = Yaa },
                                        new InitialLetter { id = Id(19,Ayn), text  = Ayn },
                                        new InitialLetter { id = Id(19,Saad), text = Saad, IsSelected = true }

                                    }
                                }
                            },
                            new td
                            {
                                colSpan = 3,
                                children =
                                {
                                    new div
                                    {
                                        style = { marginTop = "-50px", display = "flex", justifyContent = "center" },
                                        children =
                                        {
                                            new CountingResult
                                            {
                                                id           = IdOfCountingResult, MultipleOf = 8,
                                                SearchScript = GetLetterCountingScript("7:*,19:*,38:*", Saad),

                                            }
                                        }
                                    }
                                }
                            }
                        },
                        RowSpace,
                        new tr
                        {
                            new td { new Chapter { ChapterNumber = 38, ChapterName = "Sad" } },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(38,Saad), text = Saad, IsSelected = true }
                                    }
                                }
                            }
                        }
                    }
                }

            },
            
            new Arrow { start = Id(7,Saad),  end = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
            new Arrow { start = Id(19,Saad), end = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
            new Arrow { start = Id(38,Saad), end = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
        };
    }
}