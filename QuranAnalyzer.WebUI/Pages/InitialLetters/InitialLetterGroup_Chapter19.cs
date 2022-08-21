using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Chapter19 : InitialLetterGroup
{

    static string Id(int chapterNumber, string letter) => $"Chapter19-{chapterNumber}-{letter}";

    static string IdOfCountingResult => $"Chapter19-{nameof(IdOfCountingResult)}";

    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 42, SearchScript = GetLetterCountingScript("19:*", Kaaf, Haa_, Yaa, Ayn, Saad) };


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
                                new Chapter { ChapterNumber = 19, ChapterName = "Meryem" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    Items =
                                    {
                                        new InitialLetter { id = Id(19, Qaaf), text = Qaaf },
                                        new InitialLetter { id = Id(19, Haa), text  = Haa_ },
                                        new InitialLetter { id = Id(19, Yaa), text  = Yaa },
                                        new InitialLetter { id = Id(19, Ayn), text  = Ayn },
                                        new InitialLetter { id = Id(19, Saad), text = Saad }
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


            new Arrow { start = Id(19, Qaaf), end = IdOfCountingResult},
            new Arrow { start = Id(19, Haa), end  = IdOfCountingResult},
            new Arrow { start = Id(19, Yaa), end  = IdOfCountingResult},
            new Arrow { start = Id(19, Ayn), end  = IdOfCountingResult},
            new Arrow { start = Id(19, Saad), end = IdOfCountingResult}
        };
    }
}