using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;



class InitialLetterGroup_HaMimSeparated : InitialLetterGroup
{
    static string Id(int chapterNumber, string letter) => $"HaMimSeparated-{chapterNumber}-{letter}";

    static string IdOfCountingResult_1 => $"HaMimSeparated-{nameof(IdOfCountingResult_1)}";
    static string IdOfCountingResult_2 => $"HaMimSeparated-{nameof(IdOfCountingResult_2)}";

    public bool ShowCounts { get; set; }
    
    
    protected override Element render()
    {
        (int? count, string url) getOnlyOneLetterCountingInfo(string verseSelectScript, string letter, int? count)
        {
            if (!ShowCounts)
            {
                return default;
            }
            return (count, Extensions.GetUrlOfLetterCountingSearchScript(GetLetterCountingScript(verseSelectScript, letter)));
        }
        
        return new div
        {

            new table(Width("100%"))
            {
               
                    new tbody
                    {
                        HeaderTr,
                        HeaderSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 40, ChapterName = "Mümin" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter 
                                        {
                                            Id = Id(40,Haa), 
                                            Letter  = Haa, 
                                            Count = getOnlyOneLetterCountingInfo("40:*", Haa,64)
                                        },
                                        new InitialLetter
                                        {
                                            Id     = Id(40,Miim), 
                                            Letter = Miim,
                                            Count  = getOnlyOneLetterCountingInfo("40:*", Miim,380)
                                        }
                                    
                                }
                            },
                            new td
                            {
                                rowSpan = 6,
                                children =
                                {
                                    new FlexRow(JustifyContentCenter,mt(-50))
                                    {
                                        new CountingResult { id = IdOfCountingResult_1, MultipleOf = 59, SearchScript = GetLetterCountingScript("40:*,41:*,42:*", Haa, Miim) },
                                    }
                                }
                            }
                        },
                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 41, ChapterName = "Fussilet" },
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter
                                        {
                                            Id     = Id(41,Haa), 
                                            Letter = Haa,
                                            Count  = getOnlyOneLetterCountingInfo("41:*", Haa,48)
                                        },
                                        new InitialLetter
                                        {
                                            Id     = Id(41,Miim), 
                                            Letter = Miim,
                                            Count  = getOnlyOneLetterCountingInfo("41:*", Miim,276)
                                        }
                                    
                                }
                            },
                            
                        },

                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 42, ChapterName = "Şura" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter
                                        {
                                            Id     = Id(42, Haa), 
                                            Letter = Haa,
                                            Count  = getOnlyOneLetterCountingInfo("42:*", Haa,53)
                                        },
                                        new InitialLetter
                                        {
                                            Id     = Id(42, Miim), 
                                            Letter = Miim,
                                            Count  = getOnlyOneLetterCountingInfo("42:*", Miim,300)

                                        },
                                    
                                }
                            }
                        },

                        RowSpace,
                        RowSpace,
                        RowSpace,
                        new tr
                        {
                            new td(),
                            new td(),
                            new td
                            {
                                
                                style = { DisplayFlex, JustifyContentCenter },
                                children =
                                {
                                    new FlexRow(AlignItemsCenter, Gap(5))
                                    {
                                        new QuranAnalyzer.WebUI.Components.Switch{IsChecked = ShowCounts, ValueChange = x=>ShowCounts=x},"Geçiş adetlerini göster"
                                    }
                                }
                                    
                               
                            }
                        },
                        RowSpace,
                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 43, ChapterName = "Zuhruf" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter
                                        {
                                            Id     = Id(43,Haa), 
                                            Letter = Haa,
                                            Count  = getOnlyOneLetterCountingInfo("43:*", Haa,44)
                                        },
                                        new InitialLetter
                                        {
                                            Id     = Id(43,Miim), 
                                            Letter = Miim,
                                            Count  = getOnlyOneLetterCountingInfo("43:*", Miim,324)
                                        }
                                    
                                }
                            }
                        },

                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 44, ChapterName = "Duhan" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                   
                                        new InitialLetter
                                        {
                                            Id     = Id(44, Haa), 
                                            Letter = Haa,
                                            Count  = getOnlyOneLetterCountingInfo("44:*", Haa,16)
                                        },
                                        new InitialLetter
                                        {
                                            Id     = Id(44, Miim),
                                            Letter = Miim,
                                            Count  = getOnlyOneLetterCountingInfo("44:*", Miim,150)
                                        }
                                    
                                }
                            },
                            new td
                            {
                                rowSpan = 6,
                                children =
                                {
                                    new FlexRow(JustifyContentCenter,mt(-50))
                                    {
                                        new CountingResult { id = IdOfCountingResult_2, MultipleOf = 54, SearchScript = GetLetterCountingScript("43:*,44:*,45:*,46:*", Haa, Miim) }
                                    }
                                }

                            }
                        },

                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 45, ChapterName = "Casiye" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                   
                                        new InitialLetter
                                        {
                                            Id     = Id(45,Haa), 
                                            Letter = Haa,
                                            Count  = getOnlyOneLetterCountingInfo("45:*", Haa,31)
                                        },
                                        new InitialLetter
                                        {
                                            Id     = Id(45,Miim),
                                            Letter = Miim,
                                            Count  = getOnlyOneLetterCountingInfo("45:*", Miim,200)
                                        }
                                    
                                }
                            }
                        },

                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 46, ChapterName = "Ahkaf" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter
                                        {
                                            Id     = Id(46, Haa),
                                            Letter = Haa,
                                            Count  = getOnlyOneLetterCountingInfo("46:*", Haa,36)
                                        },
                                        new InitialLetter
                                        {
                                            Id     = Id(46, Miim), 
                                            Letter = Miim,
                                            Count  = getOnlyOneLetterCountingInfo("46:*", Miim,225)
                                        }
                                    
                                }
                            }
                        }
                    }
                

            },


            new Note
            {
                @" Başlangıç harfleri genelde surelerin ilk ayetinde olur. Bunun tek istisnası 42. suredir. ",
                " 42. sure diğerlerinden farklı olarak 2. ayetinde de başlangıç harfi barındırır.",
                new br(),
                " Şekilden de görüldüğü üzere 42. suredeki bu olay farklı bir ahenk daha katar. " ,
                " Sanki bu 7 surede var olan ", AsLetter(Haa), " - ", AsLetter(Miim)," tablosunu 19.un katları şeklinde ikiye bölüyormuş gibi düşünebilirsiniz."
            },
            
   
       new Arrow{start =Id(40,Haa ), end = IdOfCountingResult_1, StartAnchorFromRight = true},
       new Arrow{start =Id(40,Miim), end = IdOfCountingResult_1, StartAnchorFromRight = true},
       new Arrow{start =Id(41,Haa ), end = IdOfCountingResult_1,StartAnchorFromRight  = true},
       new Arrow{start =Id(41,Miim), end = IdOfCountingResult_1, StartAnchorFromRight = true},
       new Arrow{start =Id(42,Haa ), end = IdOfCountingResult_1, StartAnchorFromRight = true},
       new Arrow{start =Id(42,Miim), end = IdOfCountingResult_1, StartAnchorFromRight = true},

       new Arrow{start =Id(43,Haa ), end = IdOfCountingResult_2, StartAnchorFromRight = true},
       new Arrow{start =Id(43,Miim), end = IdOfCountingResult_2, StartAnchorFromRight = true},

       new Arrow{start =Id(44,Haa ), end = IdOfCountingResult_2, StartAnchorFromRight = false},
       new Arrow{start =Id(44,Miim), end = IdOfCountingResult_2, StartAnchorFromRight = false},

       new Arrow{start =Id(45,Haa ), end = IdOfCountingResult_2, StartAnchorFromTop   = true},
       new Arrow{start =Id(45,Miim), end = IdOfCountingResult_2, StartAnchorFromRight = true},
       new Arrow{start =Id(46,Haa ), end = IdOfCountingResult_2, StartAnchorFromTop   = true},
       new Arrow{start =Id(46,Miim), end = IdOfCountingResult_2, StartAnchorFromRight = true},
   
        };
    }
}