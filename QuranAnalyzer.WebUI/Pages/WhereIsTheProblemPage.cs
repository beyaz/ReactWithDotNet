using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages;

public class WhereIsTheProblemPage : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Açık Kapılar"),

            new p
            {
                @"Madem bu başlangıç harflerindeki olay bu kadar şahane. İnsanın nefesi kesiliyor. Peki red edenler niye red ediyorlar."
            },
            new p
            {
                @"Gayet haklı bir soru.",
                new br(),
                "Elinize sıradan bir Kuran alın bu sitede başlangıç haflerinde gösterilen her bir maddeyi bizzat kendiniz teyiz edebilirsiniz.",
                new br(),
                "Sadece iki maddeyi teyid edemezsiniz.",
                "1 - elinizdeki mushafta 68. sureyi açtığınızda surenin en başında sadece bir tane Nun harfi görürsünüz." +
                " Ama 19 sistemini ilk keşfeden kişi burada tek nun değil çift nun harfi olduğunu söylüyor. " ,
                " Hatta bu 19 sistemini ilk bu nun ile başlayan surede farkediyor. Eski mushaflarda bunun iki nun ile yazıldığını gördüm diyor.", 
                
                new br(),
                new br(),
                "2- Hangi mushafı kullanırsanız kullanın elif harfi neredeyse tüm mushaflarda farklı sayıdadır. Mesela bazı cümleler bazı mushaflarda atıyorum 5 elif içerirken başka mushafda 6 elif harfi içerebilir.",
                "Özetle burada şu soru haklı olarak soruluyor. Ne malum Reşad'ın Elif harfini doğru saydığı? " ,
                "Neden onun sayımlarını doğru kabul edelim ki?",
                "Belki sırf 19 s uydurmak için fazladan elif harfi saymış olabilir?",
                
                new br(),
                new br(),
                "- Neden çok uzun ayetlerde bunu yapmadı da kısa ayetlerde bunu yaptı ?",
                "- Kesin bilebilir miyiz hayır.",
                "- Başka veriler Reşadın doğru saydığını gösteriyor."
            },
            

        };
    }
}