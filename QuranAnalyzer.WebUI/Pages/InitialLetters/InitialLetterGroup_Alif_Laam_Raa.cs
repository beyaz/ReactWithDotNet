using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Alif_Laam_Raa : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"Alif_Laam_Raa-{chapterNumber}-{letter}";

    static string IdOfCountingResult(int chapterNumber) => $"Alif_Laam_Raa-{chapterNumber}";

    protected override Element render()
    {
        var resultContainerCss = JustifyContentCenter | mt(65);
        
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
                                new Chapter { ChapterNumber = 10, ChapterName = "Yunus" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter { Id = Id(10, Alif), Letter = Alif },
                                        new InitialLetter { Id = Id(10, Laam), Letter = Laam },
                                        new InitialLetter { Id = Id(10, Raa), Letter = Raa },
                                    
                                }
                            },
                            new td
                            {
                                new FlexRow(resultContainerCss)
                                {
                                    new CountingResult
                                    {
                                        id = IdOfCountingResult(10), MultipleOf = 131, SearchScript = GetLetterCountingScript("10:*", Alif, Laam, Raa)
                                    }
                                }
                            }
                        },
                        
                        RowSpace,

                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 11, ChapterName = "Hûd" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter { Id = Id(11, Alif), Letter = Alif },
                                        new InitialLetter { Id = Id(11, Laam), Letter = Laam },
                                        new InitialLetter { Id = Id(11, Raa), Letter  = Raa },
                                    
                                }
                            },
                            new td
                            {
                                new FlexRow(resultContainerCss)
                                {
                                    new CountingResult
                                    {
                                        id = IdOfCountingResult(11), MultipleOf = 131, SearchScript = GetLetterCountingScript("11:*", Alif, Laam, Raa)
                                    }
                                }
                            }
                        },

                        RowSpace,

                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 12, ChapterName = "Yusuf" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter { Id = Id(12, Alif), Letter = Alif },
                                        new InitialLetter { Id = Id(12, Laam), Letter = Laam },
                                        new InitialLetter { Id = Id(12, Raa), Letter  = Raa },
                                    
                                }
                            },
                            new td
                            {
                                new FlexRow(resultContainerCss)
                                {
                                    new CountingResult
                                    {
                                        id = IdOfCountingResult(12), MultipleOf = 125, SearchScript = GetLetterCountingScript("12:*", Alif, Laam, Raa)
                                    }
                                }
                            }
                        },

                        RowSpace,

                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 14, ChapterName = "İbrahim" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter { Id = Id(14, Alif), Letter = Alif },
                                        new InitialLetter { Id = Id(14, Laam), Letter = Laam },
                                        new InitialLetter { Id = Id(14, Raa), Letter  = Raa },
                                    
                                }
                            },
                            new td
                            {
                                new FlexRow(resultContainerCss)
                                {
                                    new CountingResult
                                    {
                                        id = IdOfCountingResult(14), MultipleOf = 63, SearchScript = GetLetterCountingScript("14:*", Alif, Laam, Raa)
                                    }
                                }
                            }
                        },

                        RowSpace,

                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 15, ChapterName = "Hicr" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter { Id = Id(15, Alif), Letter = Alif },
                                        new InitialLetter { Id = Id(15, Laam), Letter = Laam },
                                        new InitialLetter { Id = Id(15, Raa), Letter  = Raa },
                                    
                                }
                            },
                            new td
                            {
                                new FlexRow(resultContainerCss)
                                {
                                    new CountingResult
                                    {
                                        id = IdOfCountingResult(15), MultipleOf = 48, SearchScript = GetLetterCountingScript("15:*", Alif, Laam, Raa)
                                    }
                                }
                            }
                        }
                    }

                


            },


            new Arrow { start = Id(10, Alif), end = IdOfCountingResult(10) },
            new Arrow { start = Id(10, Laam), end = IdOfCountingResult(10) },
            new Arrow { start = Id(10, Raa ), end = IdOfCountingResult(10) },

            new Arrow { start = Id(11, Alif), end = IdOfCountingResult(11) },
            new Arrow { start = Id(11, Laam), end = IdOfCountingResult(11) },
            new Arrow { start = Id(11, Raa ), end = IdOfCountingResult(11) },

            new Arrow { start = Id(12, Alif), end = IdOfCountingResult(12) },
            new Arrow { start = Id(12, Laam), end = IdOfCountingResult(12) },
            new Arrow { start = Id(12, Raa ), end = IdOfCountingResult(12) },

            new Arrow { start = Id(14, Alif), end = IdOfCountingResult(14) },
            new Arrow { start = Id(14, Laam), end = IdOfCountingResult(14) },
            new Arrow { start = Id(14, Raa ), end = IdOfCountingResult(14) },

            new Arrow { start = Id(15, Alif), end = IdOfCountingResult(15) },
            new Arrow { start = Id(15, Laam), end = IdOfCountingResult(15) },
            new Arrow { start = Id(15, Raa ), end = IdOfCountingResult(15) },

        };
    }
}