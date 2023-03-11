namespace QuranAnalyzer.WebUI.Pages;

public class PageQuestionAnswer : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Soru - Cevap"),

            new p
            {
                @"Bu bölümde 19 meselesi etrafında dönen tartışmalı konuları ele aldım. 
Elimden geldiğince tartışılan konuları en kısa ve tarafsız bir şekilde özetlemeye çalıştım.",
                new br(),
                new br(),
                @"Tekrar hatırlatmakta fayda görüyorum.",
                new br(),
                new br(),
                @"Aşağıdaki soruların cevaplarının doğru olup olmadığı siz okuyucuya bırakılmıştır. 
İmana dair bir meselenin üzerinde düşünülüp içselleştirilmedikten sonra bir faydasının olmayacağına inanıyorum.
Bu sebeple ben burada tartışmayı aktarayım, tarafların özetle ne söylediğini aktarayım sonrasında üzerine düşünmek-araştırmak ve bir karara varmak size kalsın.",

            },
            new br(),
            new FlexColumn
            {
                new QuestionLink
                {
                    Question = "19 sistemini ilk keşfeden kişi (Reşad Halife) kimdir? Ne söylüyor?",
                    Url = GetPageLink(PageId.WhoIsReshadKhalifePage)
                },
                new QuestionLink
                {
                    Question = "Peki bu 19 sistemi hiç mi eleştiri almıyor? Kabul etmeyenler nereleri eleştiriyor?",
                    Url      = GetPageLink(PageId.WhereIsTheProblemPage)
                },
                new QuestionLink
                {
                    Question = @"Madem bu 19 sayısı bu kadar ilginç veriler içeriyor, neden medyadaki hiç bir alimden/hocadan duymuyoruz?",
                    Url      = GetPageLink(PageId.WhyFamousPeopleAreSilentPage)
                },
                new QuestionLink
                {
                    Question = "19 sistemi nin olması için Kurandan iki ayet atılması gerekiyor mu ? Yoksa sistem çöküyormuş doğru mu ?",

                    Url = GetPageLink(PageId.AdditionalVersesPage)
                },

                new QuestionLink { Question = "Reşad Halife kendini peygamber ilan etmiş doğru mu ?",Url = GetPageLink(PageId.IsHeMessangerPage) },


                


                //new QuestionLink { Question = "19 cular diye bir cemaat / tarikat / topluluk falan mı var ?" },

             

                new QuestionLink { Question = "Paralel 19 sistemleri", Url = GetPageLink(PageId.AlternativeSystems) },

                new QuestionLink { Question = "Edip Yüksel", Url = GetPageLink(PageId.AboutEdipYukselPage) },

                //new QuestionLink { Question = "Allah mı? Tanrı mı ?" }
            }
        };
    }


    class QuestionLink : ReactPureComponent
    {
        public string Question { get; set; }

        public string Url { get; set; }

        protected override Element render()
        {
            // https://support.google.com/youtube/answer/9872296?hl=en-GB&ref_topic=9257501
            const string d = "M19 5v14H5V5h14m0-2H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm-5 14H7v-2h7v2zm3-4H7v-2h10v2zm0-4H7V7h10v2z";

            return new FlexRow(AlignItemsCenter)
            {
                new svg(wh(24))
                {
                    new path
                    {
                        d    = d,
                        fill = "#6c93d0"
                    },
                    new path
                    {
                        d    = "M0 0h24v24H0z",
                        fill = "none"
                    }
                },
                new a
                {
                    href      = Url,
                    innerText = Question,
                    style =
                    {
                        PaddingLeft(10),
                        PaddingTopBottom(10),
                        Color("#575757"),
                        Hover(Color("rgb(165 107 107)"), TextDecorationUnderline),
                        CursorPointer,
                        TextDecorationNone,

                    }
                }
            };
        }
    }
}