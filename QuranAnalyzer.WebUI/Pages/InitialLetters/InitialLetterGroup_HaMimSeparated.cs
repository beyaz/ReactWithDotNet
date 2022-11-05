using QuranAnalyzer.WebUI.Components;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_HaMimSeparated : InitialLetterGroup
{
    public bool ShowCounts { get; set; }

    static string IdOfCountingResult_1 => $"HaMimSeparated-{nameof(IdOfCountingResult_1)}";
    static string IdOfCountingResult_2 => $"HaMimSeparated-{nameof(IdOfCountingResult_2)}";

    protected override Element render()
    {
        const string firstColor  = "#d62454";
        const string secondColor = "#c75fe0";

        (int? count, string url, string color) getOnlyOneLetterCountingInfo(string verseSelectScript, string letter, int? count, string color)
        {
            if (!ShowCounts)
            {
                return default;
            }

            return (count, Extensions.GetUrlOfLetterCountingSearchScript(GetLetterCountingScript(verseSelectScript, letter)), color);
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
                                    Id     = Id(40, Haa),
                                    Letter = Haa,
                                    Count  = getOnlyOneLetterCountingInfo("40:*", Haa, 64, firstColor)
                                },
                                new InitialLetter
                                {
                                    Id     = Id(40, Miim),
                                    Letter = Miim,
                                    Count  = getOnlyOneLetterCountingInfo("40:*", Miim, 380, firstColor)
                                }
                            }
                        },
                        new td
                        {
                            rowSpan = 6,
                            children =
                            {
                                new FlexRow(JustifyContentCenter, mt(-50))
                                {
                                    new CountingResult
                                    {
                                        id = IdOfCountingResult_1, 
                                        MultipleOf = 59, 
                                        MultipleOfColor = ShowCounts ? firstColor : null,
                                        SearchScript = GetLetterCountingScript("40:*,41:*,42:*", Haa, Miim)
                                    }
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
                                    Id     = Id(41, Haa),
                                    Letter = Haa,
                                    Count  = getOnlyOneLetterCountingInfo("41:*", Haa, 48, firstColor)
                                },
                                new InitialLetter
                                {
                                    Id     = Id(41, Miim),
                                    Letter = Miim,
                                    Count  = getOnlyOneLetterCountingInfo("41:*", Miim, 276, firstColor)
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
                                    Count  = getOnlyOneLetterCountingInfo("42:*", Haa, 53, firstColor)
                                },
                                new InitialLetter
                                {
                                    Id     = Id(42, Miim),
                                    Letter = Miim,
                                    Count  = getOnlyOneLetterCountingInfo("42:*", Miim, 300, firstColor)
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
                                    new Switch { IsChecked = ShowCounts, ValueChange = x => ShowCounts = x }, "Geçiş adetlerini göster"
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
                                    Id     = Id(43, Haa),
                                    Letter = Haa,
                                    Count  = getOnlyOneLetterCountingInfo("43:*", Haa, 44, secondColor)
                                },
                                new InitialLetter
                                {
                                    Id     = Id(43, Miim),
                                    Letter = Miim,
                                    Count  = getOnlyOneLetterCountingInfo("43:*", Miim, 324, secondColor)
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
                                    Count  = getOnlyOneLetterCountingInfo("44:*", Haa, 16, secondColor)
                                },
                                new InitialLetter
                                {
                                    Id     = Id(44, Miim),
                                    Letter = Miim,
                                    Count  = getOnlyOneLetterCountingInfo("44:*", Miim, 150, secondColor)
                                }
                            }
                        },
                        new td
                        {
                            rowSpan = 6,
                            children =
                            {
                                new FlexRow(JustifyContentCenter, mt(-50))
                                {
                                    new CountingResult
                                    {
                                        id = IdOfCountingResult_2,
                                        MultipleOf = 54, 
                                        MultipleOfColor =  ShowCounts ? secondColor : null,
                                        SearchScript = GetLetterCountingScript("43:*,44:*,45:*,46:*", Haa, Miim)
                                    }
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
                                    Id     = Id(45, Haa),
                                    Letter = Haa,
                                    Count  = getOnlyOneLetterCountingInfo("45:*", Haa, 31, secondColor)
                                },
                                new InitialLetter
                                {
                                    Id     = Id(45, Miim),
                                    Letter = Miim,
                                    Count  = getOnlyOneLetterCountingInfo("45:*", Miim, 200, secondColor)
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
                                    Count  = getOnlyOneLetterCountingInfo("46:*", Haa, 36, secondColor)
                                },
                                new InitialLetter
                                {
                                    Id     = Id(46, Miim),
                                    Letter = Miim,
                                    Count  = getOnlyOneLetterCountingInfo("46:*", Miim, 225, secondColor)
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
                " Şekilden de görüldüğü üzere 42. suredeki bu olay farklı bir ahenk daha katar. ",
                " Sanki bu 7 surede var olan ", AsLetter(Haa), " - ", AsLetter(Miim), " tablosunu 19.un katları şeklinde ikiye bölüyormuş gibi düşünebilirsiniz."
            },

            new Arrow { start = Id(40, Haa), end  = IdOfCountingResult_1, StartAnchorFromRight = true },
            new Arrow { start = Id(40, Miim), end = IdOfCountingResult_1, StartAnchorFromRight = true },
            new Arrow { start = Id(41, Haa), end  = IdOfCountingResult_1, StartAnchorFromRight = true },
            new Arrow { start = Id(41, Miim), end = IdOfCountingResult_1, StartAnchorFromRight = true },
            new Arrow { start = Id(42, Haa), end  = IdOfCountingResult_1, StartAnchorFromRight = true },
            new Arrow { start = Id(42, Miim), end = IdOfCountingResult_1, StartAnchorFromRight = true },

            new Arrow { start = Id(43, Haa), end  = IdOfCountingResult_2, StartAnchorFromRight = true },
            new Arrow { start = Id(43, Miim), end = IdOfCountingResult_2, StartAnchorFromRight = true },

            new Arrow { start = Id(44, Haa), end  = IdOfCountingResult_2, StartAnchorFromRight = false },
            new Arrow { start = Id(44, Miim), end = IdOfCountingResult_2, StartAnchorFromRight = false },

            new Arrow { start = Id(45, Haa), end  = IdOfCountingResult_2, StartAnchorFromTop   = true },
            new Arrow { start = Id(45, Miim), end = IdOfCountingResult_2, StartAnchorFromRight = true },
            new Arrow { start = Id(46, Haa), end  = IdOfCountingResult_2, StartAnchorFromTop   = true },
            new Arrow { start = Id(46, Miim), end = IdOfCountingResult_2, StartAnchorFromRight = true },
        };
    }

    static string Id(int chapterNumber, string letter) => $"HaMimSeparated-{chapterNumber}-{letter}";
}