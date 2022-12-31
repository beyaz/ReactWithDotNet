using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.ContactPage;

public class View : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("İletişim"),

            new p
            {
                "İsmim ", (b)"Abdullah Beyaztaş"
            },
            new p
            {
                (b)"Yazılım", ", ", (b)"Felsefe", " ve ", (b)"Kuran",
                " olmak üzere bu üç konu üzerine ",
                "öğrenmeyi, düşünmeyi seviyorum. ",
                "Vaktim olduğu sürece bu üç konu üzerine istediğiniz kadar fikir alışverişine açığım. ",
                "Aşağıdaki mail adresinden bana ulaşabilirsiniz",
                new br(),
                new br(),
                "beyaz1404@gmail.com"
            },
            new FlexRowCentered
            {
                "* * *"
            },
            new p
            {
                "Bu sitede kullanılan tüm kodları aşağıda belirttiğim linkten inceleyebilirsiniz. ",
                "Eğer programlama biliyorsanız bu kodları kullanarak kendi analizlerinizi yapabilirsiniz.",
                new br(),
                new FlexRowCentered
                {
                    new a { href = "#", text = "TODO: Buraya güncelleyeceğim şimdilik dummy" } // TODO:
                }
            },
            new FlexRowCentered
            {
                "* * *"
            },
            new p
            {
                "Herhangi bir fikri empoze etmek niyeti ile bu siteyi kurmadım. ",
                "Ara ara vakit buldukça bu konulara bakma fırsatım oldu. ",
                "Elimden geldiğince tarafsız bir şekilde meseleyi ele incelemeye çalıştım. ",
                "Gördüğüm resmi mümkün olduğunca kendi düşüncelerim minimum olacak şekilde aktarmaya çalıştım. ",
                "Bu konular üzerine düşünen araştıran insanlara bir faydam oldu ise ne mutlu bana."
            }
        };
    }
}