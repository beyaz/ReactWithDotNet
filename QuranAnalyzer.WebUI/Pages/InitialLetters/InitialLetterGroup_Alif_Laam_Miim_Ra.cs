using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Alif_Laam_Miim_Ra : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"Alif_Laam_Miim_Ra-{chapterNumber}-{letter}";

    static string IdOfCountingResult(int chapterNumber) => $"Alif_Laam_Miim_Ra-{chapterNumber}";

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
                                new Chapter { ChapterNumber = 13, ChapterName = "Rad" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    
                                        new InitialLetter { Id = Id(13, Alif), Letter = Alif },
                                        new InitialLetter { Id = Id(13, Laam), Letter = Laam },
                                        new InitialLetter { Id = Id(13, Miim), Letter = Miim },
                                        new InitialLetter { Id = Id(13, Raa), Letter = Raa },
                                    
                                }
                            },
                            new td
                            {
                                new FlexRow(JustifyContentCenter,mt(65))
                                {
                                    new CountingResult
                                    {
                                        id = IdOfCountingResult(13), MultipleOf = 78, SearchScript = GetLetterCountingScript("13:*", Alif, Laam, Miim, Raa)
                                    }
                                }
                            }
                        },
                       }

                


            },


            new Arrow { start = Id(13, Alif), end = IdOfCountingResult(13) },
            new Arrow { start = Id(13, Laam), end = IdOfCountingResult(13) },
            new Arrow { start = Id(13, Miim), end = IdOfCountingResult(13) },
            new Arrow { start = Id(13, Raa), end  = IdOfCountingResult(13) },
            

        };
    }
}