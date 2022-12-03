using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_HaMim : InitialLetterGroup
{
    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 113, SearchScript = GetLetterCountingScript("40:*,41:*,42:*,43:*,44:*,45:*,46:*", Haa, Miim) };

    static string IdOfCountingResult => $"HaMim-{nameof(IdOfCountingResult)}";

    protected override Element render()
    {
        return new div
        {
            new table(WidthMaximized)
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
                                new InitialLetter { Id = Id(40, Haa), Letter  = Haa },
                                new InitialLetter { Id = Id(40, Miim), Letter = Miim }
                            }
                        },
                        new td
                        {
                            rowSpan = 99,
                            children =
                            {
                                new FlexRow(JustifyContentCenter, mt(-50))
                                {
                                    countingResult
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
                                new InitialLetter { Id = Id(41, Haa), Letter  = Haa },
                                new InitialLetter { Id = Id(41, Miim), Letter = Miim }
                            }
                        }
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
                                new InitialLetter { Id = Id(42, Haa), Letter  = Haa },
                                new InitialLetter { Id = Id(42, Miim), Letter = Miim },
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
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { Id = Id(43, Haa), Letter  = Haa },
                                new InitialLetter { Id = Id(43, Miim), Letter = Miim }
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
                                new InitialLetter { Id = Id(44, Haa), Letter  = Haa },
                                new InitialLetter { Id = Id(44, Miim), Letter = Miim }
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
                                new InitialLetter { Id = Id(45, Haa), Letter  = Haa },
                                new InitialLetter { Id = Id(45, Miim), Letter = Miim }
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
                                new InitialLetter { Id = Id(46, Haa), Letter  = Haa },
                                new InitialLetter { Id = Id(46, Miim), Letter = Miim }
                            }
                        }
                    }
                }
            },

            new Note
            {
                AsLetter(Haa)," ve ", AsLetter(Miim) , @" ile başlayan 7 tane sure vardır.",
                " Bu iki harfin bu 7 suredeki geçiş adeti ise ", 2147.AsMultipleOf19(), "'tür.",
                new br(),
                " Bu 7 sure neredeyse Kuranın 5 de 1'ine tekabül eder.",
                " Eğer bu iki harften bir tanesi fazla veya eksik olsaydı yukarıdaki şekildeki gibi bir ahenk olmazdı.",
                new br(),
                new br(),
                "Mesela Mekke şehri bu surelerde 'Mekke' yerine 'Bekke' şeklinde yazılmıştır.",
                " Neden Bekke şeklinde yazıldığı ile ilgili başka yorumlar da elbet var. Ama Yukarıdaki şekil incelendiğinde sanırım nedeni daha net anlaşılıyor.",
                " Eğer Mekke şeklinde yazılsaydı ",AsLetter(Miim), " harfleri bir fazla olurdu ve bu şekildeki ahenk olmazdı."
            },
            
            new Arrow { start = Id(40, Haa), end  = IdOfCountingResult },
            new Arrow { start = Id(40, Miim), end = IdOfCountingResult, StartAnchorFromRight = true },
            new Arrow { start = Id(41, Haa), end  = IdOfCountingResult },
            new Arrow { start = Id(41, Miim), end = IdOfCountingResult, StartAnchorFromRight = true },
            new Arrow { start = Id(42, Haa), end  = IdOfCountingResult },
            new Arrow { start = Id(42, Miim), end = IdOfCountingResult, StartAnchorFromRight = true },
            new Arrow { start = Id(43, Haa), end  = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(43, Miim), end = IdOfCountingResult, StartAnchorFromRight = true },
            new Arrow { start = Id(44, Haa), end  = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(44, Miim), end = IdOfCountingResult, StartAnchorFromRight = true },
            new Arrow { start = Id(45, Haa), end  = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(45, Miim), end = IdOfCountingResult, StartAnchorFromRight = true },
            new Arrow { start = Id(46, Haa), end  = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(46, Miim), end = IdOfCountingResult, StartAnchorFromRight = true },
        };
    }

    static string Id(int chapterNumber, string letter) => $"HaMim-{chapterNumber}-{letter}";
}