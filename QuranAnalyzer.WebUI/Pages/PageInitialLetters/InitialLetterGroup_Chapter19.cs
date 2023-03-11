using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.PageInitialLetters;

class Note : ReactComponent
{
    public string Text { get; set; }
    
    protected override Element render()
    {
        return new FlexRow(PaddingLeftRight("5%"), PaddingTop(50))
        {
            new strong{Text("Not:"), MarginRight(5)}, new div{ Children(children) }
        };
    }
}

class InitialLetterGroup_Chapter19 : InitialLetterGroup
{
    static string Id(int chapterNumber, string letter) => $"Chapter19-{chapterNumber}-{letter}";

    static string IdOfCountingResult => $"Chapter19-{nameof(IdOfCountingResult)}";

    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 42, SearchScript = GetLetterCountingScript("19:*", Kaaf, Haa_, Yaa, Ayn, Saad) };

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
                            new Chapter { ChapterNumber = 19, ChapterName = "Meryem" }
                        },
                        new td
                        {
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { Id = Id(19, Kaaf), Letter = Kaaf },
                                new InitialLetter { Id = Id(19, Haa_), Letter = Haa_ },
                                new InitialLetter { Id = Id(19, Yaa), Letter  = Yaa },
                                new InitialLetter { Id = Id(19, Ayn), Letter  = Ayn },
                                new InitialLetter { Id = Id(19, Saad), Letter = Saad }
                            }
                        },
                        new td
                        {
                            rowSpan = 99,
                            children =
                            {
                                new FlexRow(JustifyContentCenter,MarginTop(70))
                                {
                                    countingResult
                                }
                            }
                        }
                    },

                }

            },


            new Arrow { start = Id(19, Kaaf), end = IdOfCountingResult},
            new Arrow { start = Id(19, Haa_), end = IdOfCountingResult},
            new Arrow { start = Id(19, Yaa), end  = IdOfCountingResult},
            new Arrow { start = Id(19, Ayn), end  = IdOfCountingResult},
            new Arrow { start = Id(19, Saad), end = IdOfCountingResult},


            new Note
            {
                @"Meryem suresi Kuran'da en baştan ",  (strong)"19." ," sıradadır. ",
                @"Aynı zamanda", (strong)" en çok başlangıç harfi olan", " suredir. ",
                " 5 tane başlangıç harfi vardır.",
                " Bu beş tane başlangıç harfinin toplam geçiş adeti ise yine 19 un bir katı olan ", 798.AsMultipleOf19() ,"'dir."
            }
        };
    }
}