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

            new table(Width(Percent(100)))
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
                                    
                                        new InitialLetter { id = Id(42, Qaaf), text = Qaaf }
                                    
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
                @"42. surede yine Kaf(" , (strong)"ق" , ") başlangıç harfi vardır.",
                " Bu surede toplamda 57(19x3) adet Kaf(ق) harfi vardır.",
                new br(),new br(),
                " Özetlemek gerekirse Kuranda Kaf(ق) başlangıç harfi içeren iki tane sure var.",
                "  Bu iki sure de kendi içlerinde eşit sayıda yani 57(19x3) adet Kaf(ق) harfi içerir."
            },

            new Arrow{start =Id(42, Qaaf), end = IdOfCountingResult, StartAnchorFromRight = true}
        };
    }
}