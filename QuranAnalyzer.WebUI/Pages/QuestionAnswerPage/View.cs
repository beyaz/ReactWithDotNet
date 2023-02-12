namespace QuranAnalyzer.WebUI.Pages.QuestionAnswerPage;

public class View : ReactComponent
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
İmana dair bir meselenin üzerinde düşünlüp içselleştirilmedikten sonra bir faydasının olmayacağına inanıyorum.
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

                new QuestionLink { Question = "Reşad Halife kendini peygamber ilan etmiş doğru mu ?" },

               
                
                new QuestionLink { Question = "19 cular diye bir cemaat / tarikat / topluluk felan mı var ?" },

             

                new QuestionLink { Question = "Paralel 19 sistemleri", Url = GetPageLink(PageId.AlternativeSystems) },

                new QuestionLink { Question = "Edip Yüksel", Url = GetPageLink(PageId.AboutEdipYukselPage) },

                new QuestionLink { Question = "Allah mı? Tanrı mı ?" }
            }
        };
    }
}