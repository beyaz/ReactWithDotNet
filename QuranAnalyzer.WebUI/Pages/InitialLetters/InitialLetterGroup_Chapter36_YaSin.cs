using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Chapter36_YaSin : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"Chapter36_YaSin-{chapterNumber}-{letter}";

    static string IdOfCountingResult => $"Chapter36_YaSin-{nameof(IdOfCountingResult)}";

    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 15, SearchScript = GetLetterCountingScript("36:*", Yaa, Siin) };

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
                                new Chapter { ChapterNumber = 36, ChapterName = "Yasin" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(36, Yaa), text = Yaa },
                                        new InitialLetter { id = Id(36, Siin), text = Siin },
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
                                            countingResult
                                        }
                                    }
                                }
                            }
                        },

                    }
                }

            },


            new Arrow { start = Id(36, Yaa), end = IdOfCountingResult},
            new Arrow { start = Id(36, Siin), end = IdOfCountingResult},
        };
    }
}