using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class InitialLetterGroup_Qaaf_50 : InitialLetterGroup
{
    static Element countingResult => new CountingResult { id = IdOfCountingResult, MultipleOf = 3, SearchScript = GetLetterCountingScript("50:*", Qaaf) };

    static string IdOfCountingResult => $"Qaaf_50-{nameof(IdOfCountingResult)}";

    protected override Element render()
    {
        return new div
        {
            new table(Width("100%"))
            {
                new tbody
                {
                    HeaderTr,
                    HeaderSpace,
                    new tr
                    {
                        new td
                        {
                            new Chapter { ChapterNumber = 50, ChapterName = "Kaf" }
                        },
                        new td
                        {
                            new InitialLetterLineGroup
                            {
                                new InitialLetter { id = Id(50, Qaaf), text = Qaaf }
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
                @"Kuranda 50. surenin adı  'Kaf Suresi' dir. İlginç bir şekilde surenin başında sadece bir tane Kaf(" , (strong)"ق" , ") harfi vardır.",
                " Yüzyıllar boyu bu harfler için farklı farklı bir çok fikir ortaya atılmıştır.",
                " Size bir mektup geldiğini hayal edin ve mektubun ilk satırında sadece bir K harfi olduğunu düşünün. " +
                " İster istemez burada bir kasıt ararsınız. Bir açıklama beklersiniz.",
                " Bu surede toplamda 57(19x3) adet Kaf(ق) harfi vardır.",
                " İsterseniz incele linkine tıklayarak bu sayımları kendiniz yapabilirsiniz."
            },

            new Arrow { start = Id(50, Qaaf), end = IdOfCountingResult, StartAnchorFromRight = true }
        };
    }

    static string Id(int chapterNumber, string letter) => $"Qaaf_50-{chapterNumber}-{letter}";
}