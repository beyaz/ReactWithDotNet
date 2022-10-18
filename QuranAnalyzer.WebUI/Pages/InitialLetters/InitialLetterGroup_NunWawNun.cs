using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_NunWawNun : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"NunWawNun-{chapterNumber}-{letter}";

    static string IdOfCountingResult => $"NunWawNun-{nameof(IdOfCountingResult)}";

    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 7, SearchScript = GetLetterCountingScript("68:*", Nun) };

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
                                new Chapter { ChapterNumber = 68, ChapterName = "Kalem" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup_old
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(68, Nun), text = Nun }
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

            new Arrow{start =Id(68, Nun), end = IdOfCountingResult, StartAnchorFromRight = true}
        };
    }
}