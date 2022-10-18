using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;



class InitialLetterGroup_HaMimSeparated : InitialLetterGroup
{
    static string Id(int chapterNumber, string letter) => $"HaMimSeparated-{chapterNumber}-{letter}";

    static string IdOfCountingResult_1 => $"HaMimSeparated-{nameof(IdOfCountingResult_1)}";
    static string IdOfCountingResult_2 => $"HaMimSeparated-{nameof(IdOfCountingResult_2)}";

    protected override Element render()
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
                            new td
                            {
                                new Chapter { ChapterNumber = 40, ChapterName = "Mümin" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(40,Haa), text  = Haa },
                                        new InitialLetter { id = Id(40,Miim), text = Miim }
                                    }
                                }
                            },
                            new td
                            {
                                rowSpan = 6,
                                children =
                                {
                                    new div
                                    {
                                        style = { marginTop = "-50px", display = "flex", justifyContent = "center" },
                                        children =
                                        {
                                            new CountingResult { id = IdOfCountingResult_1, MultipleOf = 59, SearchScript = GetLetterCountingScript("40:*,41:*,42:*", Haa, Miim) },

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
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(41,Haa), text  = Haa },
                                        new InitialLetter { id = Id(41,Miim), text = Miim }
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
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(42, Haa), text  = Haa },
                                        new InitialLetter { id = Id(42, Miim), text = Miim },
                                    }
                                }
                            }
                        },

                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 43, ChapterName = "Zuhruf" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(43,Haa), text  = Haa },
                                        new InitialLetter { id = Id(43,Miim), text = Miim }
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
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(44, Haa), text  = Haa },
                                        new InitialLetter { id = Id(44, Miim), text = Miim }
                                    }
                                }
                            },
                            new td
                            {
                                rowSpan = 6,
                                children =
                                {
                                    new div
                                    {
                                        style = { marginTop = "-50px", display = "flex", justifyContent = "center" },
                                        children =
                                        {
                                            new CountingResult { id = IdOfCountingResult_2, MultipleOf = 54, SearchScript = GetLetterCountingScript("43:*,44:*,45:*,46:*", Haa, Miim) }

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
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(45,Haa), text  = Haa },
                                        new InitialLetter { id = Id(45,Miim), text = Miim }
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
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(46, Haa), text  = Haa },
                                        new InitialLetter { id = Id(46, Miim), text = Miim }
                                    }
                                }
                            }
                        }
                    }
                }

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