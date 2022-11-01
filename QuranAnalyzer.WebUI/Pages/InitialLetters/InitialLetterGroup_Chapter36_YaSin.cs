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
                                new Chapter { ChapterNumber = 36, ChapterName = "Yasin" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                   
                                        new InitialLetter { id = Id(36, Yaa), text = Yaa },
                                        new InitialLetter { id = Id(36, Siin), text = Siin },
                                    
                                }
                            },
                            new td
                            {
                                rowSpan = 99,
                                children =
                                {
                                    new FlexRow(JustifyContentCenter, mt(50))
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
                @"Belki de ismi en yaygın olarak bilinen sure Yasin suresidir.",
                " Bu sure " , Letter("Ye","ي") , " ve ",Letter("Sin","س"), " olmak üzere iki tane başlangıç harfi ile başlar.",
                " Yüzyıllar boyu bu harfler için farklı farklı bir çok fikir ortaya atılmıştır.",
                " Size bir mektup geldiğini hayal edin ve mektubun ilk satırında sadece bir K harfi olduğunu düşünün. " +
                " İster istemez burada bir kasıt ararsınız. Bir açıklama beklersiniz.",
                " Bu surede toplamda 57(19x3) adet Kaf(ق) harfi vardır.",
                " İsterseniz incele linkine tıklayarak bu sayımları kendiniz yapabilirsiniz."
            },
            
            new Arrow { start = Id(36, Yaa), end = IdOfCountingResult},
            new Arrow { start = Id(36, Siin), end = IdOfCountingResult},
        };

        static Element Letter(string trName, string arabicLetter)
        {
            return new div{ trName, "(", (strong)arabicLetter, ")" };
        }
    }
}