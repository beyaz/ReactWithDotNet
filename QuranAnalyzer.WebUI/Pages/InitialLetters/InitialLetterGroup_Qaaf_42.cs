using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Qaaf_42 : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"Qaaf_42-{chapterNumber}-{letter}";

    static string IdOfCountingResult => $"Qaaf_42-{nameof(IdOfCountingResult)}";

    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 3, SearchScript = GetLetterCountingScript("42:*", Qaaf) };

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
                                    Items =
                                    {
                                        new InitialLetter { id = Id(42, Qaaf), text = Qaaf }
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
                                        style = { marginTop = "0px", display = "flex", justifyContent = "center" },
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

            new Arrow{start =Id(42, Qaaf), end = IdOfCountingResult, StartAnchorFromRight = true}
        };
    }
}