using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Alif_Laam_Miim_Sad : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"Alif_Laam_Miim_Sad-{chapterNumber}-{letter}";

    static string IdOfCountingResult(int chapterNumber) => $"Alif_Laam_Miim_Sad-{chapterNumber}";

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
                                new Chapter { ChapterNumber = 7, ChapterName = "Araf" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter { id = Id(7, Alif), text = Alif },
                                        new InitialLetter { id = Id(7, Laam), text = Laam },
                                        new InitialLetter { id = Id(7, Miim), text = Miim },
                                        new InitialLetter { id = Id(7, Saad), text = Saad },
                                    
                                }
                            },
                            new td
                            {
                                new div
                                {
                                    style = { marginTop = "50px", display = "flex", justifyContent = "center" },
                                    children =
                                    {
                                        new CountingResult
                                        {
                                            id = IdOfCountingResult(7), MultipleOf = 280, SearchScript = GetLetterCountingScript("7:*", Alif, Laam, Miim, Saad)
                                        }
                                    }
                                }
                            }
                        },
                       }

                },


            },


            new Arrow { start = Id(7, Alif), end = IdOfCountingResult(7) },
            new Arrow { start = Id(7, Laam), end = IdOfCountingResult(7) },
            new Arrow { start = Id(7, Miim), end = IdOfCountingResult(7) },
            new Arrow { start = Id(7, Saad), end = IdOfCountingResult(7) },
            

        };
    }
}