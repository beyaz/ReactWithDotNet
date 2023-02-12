using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Chapter42_AinSinKaf : InitialLetterGroup
{
    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 11, SearchScript = GetLetterCountingScript("42:*", Ayn, Siin, Qaaf) };

    static string IdOfCountingResult => $"42_AinSinKaf-{nameof(IdOfCountingResult)}";

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
                                new InitialLetter { Id = Id(42, Ayn), Letter  = Ayn },
                                new InitialLetter { Id = Id(42, Siin), Letter = Siin },
                                new InitialLetter { Id = Id(42, Qaaf), Letter = Qaaf }
                            }
                        },
                        new td
                        {
                            rowSpan = 99,
                            children =
                            {
                                new FlexRow(JustifyContentCenter, MarginTop(70))
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
                @"Kurandaki başlangıç harfleri surenin sadece ilk ayetinde olur.",
                " Bunun tek istisnası 42. suredir.",
                " Bu surenin hem 1. ayeti hem 2.ayeti başlangıç harflerinden oluşur. ",
                AsLetter(Ayn), " , ", AsLetter(Siin), " , ", AsLetter(Qaaf), " harflerinin bu sure boyunca geçiş adeti ise yine 19 un katı olan ",
                209.AsMultipleOf19(), " sayısıdır."
            },

            new Arrow { start = Id(42, Ayn), end  = IdOfCountingResult },
            new Arrow { start = Id(42, Siin), end = IdOfCountingResult },
            new Arrow { start = Id(42, Qaaf), end = IdOfCountingResult },
        };
    }

    static string Id(int chapterNumber, string letter) => $"42_AinSinKaf-{chapterNumber}-{letter}";
}