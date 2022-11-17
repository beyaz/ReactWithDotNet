namespace QuranAnalyzer.WebUI.Pages.QuestionAnswerPage;





[Serializable]
public sealed class QuestionAnswerPair
{
    public string Question { get; set; }
    public string Answer { get; set; }

}

class QuestionLink : ReactComponent
{
    public string Question { get; set; }

    public string Url { get; set; }

    protected override Element render()
    {
        return new FlexRow(AlignItemsCenter)
            {
                new div(wh(24))
                {
                    new svg
                    {
                        new path
                        {
                            d    = "M19 5v14H5V5h14m0-2H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm-5 14H7v-2h7v2zm3-4H7v-2h10v2zm0-4H7V7h10v2z",
                            fill = "#0b57d0"

                        },
                        new path
                        {
                            d    = "M0 0h24v24H0z",
                            fill = "none"
                        }
                    }
                }
               ,
                new a
                {
                    href      = Url,
                    innerText = Question,
                    style =
                    {
                        PaddingLeft(10),
                        PaddingTopBottom(10),
                        Color("#575757"), Hover(Color("rgb(165 107 107)")),
                        CursorPointer,
                        TextDecorationUnderline
                    }
                }

            }
            ;
        // https://support.google.com/youtube/answer/9872296?hl=en-GB&ref_topic=9257501
    }
}
public class View : ReactComponent
{
    

    protected override Element render()
        {

            




            return new div
            {
                new VSpace(15),
                new div
                {
                    style = { paddingLeftRight = "15px" },
                    children =
                    {
                        new div
                        {
                            innerText = "Soru - Cevap",
                            style =
                            {
                                marginBottom = "10px",
                                fontWeight   = "500",
                                textAlign    = "center"
                            }
                        },

                        @"Bu bölümde 19 meselesi etrafında dönen tartışmalı konuları ele aldım. 
Elimden geldiğince tartışılan konuları en kısa ve tarafsız bir şekilde özetlemeye çalıştım.",
                        new br(),
                        new br(),
                        @"Tekrar hatırlatmakta fayda görüyorum.",
                        new br(),
                        new br(),
                        @"Aşağıdaki soruların cevaplarının doğru olup olmadığı siz okuyucuya bırakılmıştır. 
İmana dair bir meselenin üzerinde düşünlüp içselleştirilmedikten sonra bir faydasının olmayacağına inanıyorum.
Bu sebeple ben burada tartışmayı aktarayım, tarafların özetle ne söylediğini aktarayım sonrasında üzerine düşünmek-araştırmak ve bir karara varmak size kalsın." ,

                        new br(),
                        new br(),
                        new br(),
                        new div
                        { 
                            new QuestionLink{Question  = "Madem bu 19 sayısı bu kadar ilginç veriler içeriyor, neden hiç bir alimden/hocadan duymuyoruz?"},
                            new QuestionLink{Question  = "19 sistemi nin olması için Kurandan iki ayet atılması gerekiyor mu ? Yoksa sistem çöküyormuş doğru mu ?"},
                            new QuestionLink{ Question = "Elif sayımlarının doğru olduğu ne malum ?"},
                            new QuestionLink{Question  = "19 cular diye bir cemaat / tarikat / topluluk felan mı var ?"},
                            new QuestionLink{Question  = "Reşad Halife kimdir ?"},
                            new QuestionLink{Question  = "Reşad Halife kendini peygamber ilan etmiş doğru mu ?"},
                            
                            new QuestionLink{Question  = "Paralel 19 sistemleri", Url = GetPageLink(PageId.AlternativeSystems),},
                            
                            new QuestionLink{Question  = "Allah mı? Tanrı mı ?"},


                        }
                    }
                }
            };



        }
    }

