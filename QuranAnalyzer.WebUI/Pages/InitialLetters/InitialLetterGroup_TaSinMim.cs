using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_TaSinMim : InitialLetterGroup
{
    static string Id(int chapterNumber, string letter) => $"TaSinMim-{chapterNumber}-{letter}";

    static string IdOfCountingResult => $"TaSinMim-{nameof(IdOfCountingResult)}";

    static Element countingResult => new CountingResult
    {
        id = IdOfCountingResult,
        MultipleOf = 93,

        SearchScript = GetLetterCountingScript("19:*", Haa_) + ";" +
                       GetLetterCountingScript("20:*", Taa_, Haa_) + ";" +
                       GetLetterCountingScript("26:*", Taa_, Siin, Miim) + ";" +
                       GetLetterCountingScript("27:*", Taa_, Siin) + ";" +
                       GetLetterCountingScript("28:*", Taa_, Siin, Miim)
    };

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
                                new Chapter { ChapterNumber = 19, ChapterName = "Meryem" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(19,Qaaf), text = Qaaf },
                                        new InitialLetter { id = Id(19,Haa), text  = Haa_,IsSelected = true  },
                                        new InitialLetter { id = Id(19,Yaa), text  = Yaa },
                                        new InitialLetter { id = Id(19,Ayn), text  = Ayn },
                                        new InitialLetter { id = Id(19,Saad), text = Saad}

                                    }
                                }
                            },
                            new td
                            {
                                rowSpan = 99,
                                children =
                                {
                                    new div
                                    {
                                        style = { marginTop = "-50px", display = "flex", justifyContent = "center" },
                                        children =
                                        {
                                            countingResult
                                        }
                                    }
                                }
                            }
                        },
                        RowSpace,
                        new tr
                        {
                            new td {  new Chapter { ChapterNumber = 20, ChapterName = "Taha" }  },
                            new td
                            {
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(20,Taa_), text = Taa_ ,IsSelected = true},
                                        new InitialLetter { id = Id(20,Haa), text  = Haa ,IsSelected  = true}
                                    }
                                }
                            }
                        },

                        RowSpace,
                        new tr
                        {
                            new td {  new Chapter { ChapterNumber = 26, ChapterName = "Şuara" }   },
                            new td
                            {
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(26,Taa_), text = Taa_,IsSelected  = true },
                                        new InitialLetter { id = Id(26,Siin), text = Siin ,IsSelected = true},
                                        new InitialLetter { id = Id(26,Miim), text = Miim ,IsSelected = true}
                                    }
                                }
                            }
                        },

                        RowSpace,
                        new tr
                        {
                            new td {  new Chapter { ChapterNumber = 27, ChapterName = "Neml" }   },
                            new td
                            {
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(27,Taa_), text = Taa_ ,IsSelected = true},
                                        new InitialLetter { id = Id(27,Siin), text = Siin ,IsSelected = true}
                                    }
                                }
                            }
                        },

                        RowSpace,
                        new tr
                        {
                            new td {  new Chapter { ChapterNumber = 28, ChapterName = "Kasas" }  },
                            new td
                            {
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(28,Taa_), text = Taa_ ,IsSelected = true},
                                        new InitialLetter { id = Id(28,Siin), text = Siin ,IsSelected = true},
                                        new InitialLetter { id = Id(28,Miim), text = Miim ,IsSelected = true}
                                    }
                                }
                            }
                        }
                    }
                }

            },

            new Arrow{start =Id(19,Haa ),end = IdOfCountingResult,},
            new Arrow{start =Id(20,Taa_),end = IdOfCountingResult},
            new Arrow{start =Id(20,Haa ),end = IdOfCountingResult,StartAnchorFromRight = true},
            new Arrow{start =Id(26,Taa_),end = IdOfCountingResult,StartAnchorFromTop   = true},
            new Arrow{start =Id(26,Siin),end = IdOfCountingResult,StartAnchorFromTop   = true},
            new Arrow{start =Id(26,Miim),end = IdOfCountingResult,StartAnchorFromTop   = true},
            new Arrow{start =Id(27,Taa_),end = IdOfCountingResult, StartAnchorFromTop  = true},
            new Arrow{start =Id(27,Siin),end = IdOfCountingResult, StartAnchorFromTop  = true},
            new Arrow{start =Id(28,Taa_),end = IdOfCountingResult, StartAnchorFromTop  = true},
            new Arrow{start =Id(28,Siin),end = IdOfCountingResult, StartAnchorFromTop  = true},
            new Arrow{start =Id(28,Miim),end = IdOfCountingResult, StartAnchorFromTop  = true},
        };
    }
}