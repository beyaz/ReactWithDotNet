using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Qaaf_42 : InitialLetterGroup
{
    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 3, SearchScript = GetLetterCountingScript("42:*", Qaaf) };

    static string IdOfCountingResult => $"Qaaf_42-{nameof(IdOfCountingResult)}";

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
                            new Chapter { ChapterNumber = 42, ChapterName = "Şura" }
                        },
                        new td
                        {
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { Id = Id(42, Qaaf), Letter = Qaaf }
                            }
                        },
                        new td
                        {
                            rowSpan = 99,
                            children =
                            {
                                new FlexRow(JustifyContentCenter)
                                {
                                    countingResult
                                }
                            }
                        }
                    },
                }
            },

            new Note
            {
                @"42. surede yine ", AsLetter(Qaaf), " başlangıç harfi vardır.",
                " Bu surede toplamda ", 57.AsMultipleOf19(), " adet ", AsLetter(Qaaf), " harfi vardır.",
                new br(), new br(),
                " Özetlemek gerekirse Kuranda ", AsLetter(Qaaf), " başlangıç harfi içeren iki tane sure vardır.",
                "  Bu iki sure de kendi içlerinde eşit sayıda yani ", 57.AsMultipleOf19(), " adet ", AsLetter(Qaaf), " harfi içerir."
            },

            new Arrow { start = Id(42, Qaaf), end = IdOfCountingResult, StartAnchorFromRight = true }
        };
    }

    static string Id(int chapterNumber, string letter) => $"Qaaf_42-{chapterNumber}-{letter}";
}