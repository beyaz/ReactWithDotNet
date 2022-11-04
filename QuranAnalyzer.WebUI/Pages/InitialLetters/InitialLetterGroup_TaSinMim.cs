using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_TaSinMim : InitialLetterGroup
{
    static Element countingResult => new CountingResult
    {
        id         = IdOfCountingResult,
        MultipleOf = 93,

        SearchScript = GetLetterCountingScript("19:*", Haa_) + ";" +
                       GetLetterCountingScript("20:*", Taa_, Haa_) + ";" +
                       GetLetterCountingScript("26:*", Taa_, Siin, Miim) + ";" +
                       GetLetterCountingScript("27:*", Taa_, Siin) + ";" +
                       GetLetterCountingScript("28:*", Taa_, Siin, Miim)
    };

    static string IdOfCountingResult => $"TaSinMim-{nameof(IdOfCountingResult)}";

    protected override Element render()
    {
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
                            new Chapter { ChapterNumber = 19, ChapterName = "Meryem" }
                        },
                        new td
                        {
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { Id = Id(19, Qaaf), Letter = Qaaf },
                                new InitialLetter { Id = Id(19, Haa), Letter  = Haa_, IsSelected = true },
                                new InitialLetter { Id = Id(19, Yaa), Letter  = Yaa },
                                new InitialLetter { Id = Id(19, Ayn), Letter  = Ayn },
                                new InitialLetter { Id = Id(19, Saad), Letter = Saad }
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
                        new td { new Chapter { ChapterNumber = 20, ChapterName = "Taha" } },
                        new td
                        {
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { Id = Id(20, Taa_), Letter = Taa_, IsSelected = true },
                                new InitialLetter { Id = Id(20, Haa), Letter  = Haa, IsSelected  = true }
                            }
                        }
                    },

                    RowSpace,
                    new tr
                    {
                        new td { new Chapter { ChapterNumber = 26, ChapterName = "Şuara" } },
                        new td
                        {
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { Id = Id(26, Taa_), Letter = Taa_, IsSelected = true },
                                new InitialLetter { Id = Id(26, Siin), Letter = Siin, IsSelected = true },
                                new InitialLetter { Id = Id(26, Miim), Letter = Miim, IsSelected = true }
                            }
                        }
                    },

                    RowSpace,
                    new tr
                    {
                        new td { new Chapter { ChapterNumber = 27, ChapterName = "Neml" } },
                        new td
                        {
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { Id = Id(27, Taa_), Letter = Taa_, IsSelected = true },
                                new InitialLetter { Id = Id(27, Siin), Letter = Siin, IsSelected = true }
                            }
                        }
                    },

                    RowSpace,
                    new tr
                    {
                        new td { new Chapter { ChapterNumber = 28, ChapterName = "Kasas" } },
                        new td
                        {
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { Id = Id(28, Taa_), Letter = Taa_, IsSelected = true },
                                new InitialLetter { Id = Id(28, Siin), Letter = Siin, IsSelected = true },
                                new InitialLetter { Id = Id(28, Miim), Letter = Miim, IsSelected = true }
                            }
                        }
                    }
                }
            },

            new Arrow { start = Id(19, Haa), end  = IdOfCountingResult, },
            new Arrow { start = Id(20, Taa_), end = IdOfCountingResult },
            new Arrow { start = Id(20, Haa), end  = IdOfCountingResult, StartAnchorFromRight = true },
            new Arrow { start = Id(26, Taa_), end = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(26, Siin), end = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(26, Miim), end = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(27, Taa_), end = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(27, Siin), end = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(28, Taa_), end = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(28, Siin), end = IdOfCountingResult, StartAnchorFromTop   = true },
            new Arrow { start = Id(28, Miim), end = IdOfCountingResult, StartAnchorFromTop   = true },
        };
    }

    static string Id(int chapterNumber, string letter) => $"TaSinMim-{chapterNumber}-{letter}";
}