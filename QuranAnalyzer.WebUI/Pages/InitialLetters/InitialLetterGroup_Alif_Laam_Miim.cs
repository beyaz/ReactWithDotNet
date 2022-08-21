using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Alif_Laam_Miim : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"Alif_Laam_Miim-{chapterNumber}-{letter}";

    static string IdOfCountingResult(int chapterNumber) => $"Alif_Laam_Miim-{chapterNumber}";


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
                            new td
                            {
                                new Chapter { ChapterNumber = 2, ChapterName = "Bakara" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(2, Alif), text = Alif },
                                        new InitialLetter { id = Id(2, Laam), text = Laam },
                                        new InitialLetter { id = Id(2, Miim), text = Miim },
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
                                        style = { marginTop = "50px", display = "flex", justifyContent = "center" },
                                        children =
                                        {
                                            new CountingResult
                                            {
                                                id = IdOfCountingResult(2), MultipleOf = 521, SearchScript = GetLetterCountingScript("2:*", Alif, Laam, Miim)
                                            }
                                        }
                                    }
                                }
                            },

                        }
                    }

                },


            },


            new Arrow { start = Id(2, Alif), end = IdOfCountingResult(2) },
            new Arrow { start = Id(2, Laam), end = IdOfCountingResult(2) },
            new Arrow { start = Id(2, Miim), end = IdOfCountingResult(2) },
        };
    }
}