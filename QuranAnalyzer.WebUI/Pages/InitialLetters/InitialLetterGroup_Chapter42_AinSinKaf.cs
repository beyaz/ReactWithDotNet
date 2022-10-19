using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Chapter42_AinSinKaf : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"42_AinSinKaf-{chapterNumber}-{letter}";

    static string IdOfCountingResult => $"42_AinSinKaf-{nameof(IdOfCountingResult)}";

    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 11, SearchScript = GetLetterCountingScript("42:*", Ayn, Siin, Qaaf) };

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
                                new Chapter { ChapterNumber = 42, ChapterName = "Şura" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {

                                    new InitialLetter { id = Id(42, Ayn) , text = Ayn  },
                                        new InitialLetter { id = Id(42, Siin), text = Siin },
                                        new InitialLetter { id = Id(42, Qaaf), text = Qaaf }
                                    
                                    
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
                                            countingResult
                                        }
                                    }
                                }
                            }
                        },

                    }
                }

            },


            new Arrow { start = Id(42, Ayn), end  = IdOfCountingResult},
            new Arrow { start = Id(42, Siin), end = IdOfCountingResult},
            new Arrow { start = Id(42, Qaaf), end  = IdOfCountingResult},
        };
    }
}