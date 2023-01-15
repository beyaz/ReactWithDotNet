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
                new br(),
                "Sadece iki maddeyi teyid edemezsiniz.",
                new br(),
                new br(),

                "1 - Elinizdeki mushafta 68. sureyi açtığınızda surenin en başında sadece bir tane ", AsLetter(ArabicLetter.Nun), " harfi görürsünüz.",
                " 19 sistemini ilk keşfeden kişi burada tek nun değil çift nun harfi olduğunu söylüyor. ",
                " Hatta bu 19 sistemini ilk bu nun ile başlayan surede farkediyor. Eski mushaflarda bunun iki nun ile yazıldığını gördüm diyor.",

                new br(),
                new br(),
                "2- Hangi mushafı kullanırsanız kullanın ", AsLetter(ArabicLetter.Alif), " harfi neredeyse tüm mushaflarda farklı sayıdadır. ",
                "Mesela bazı cümleler bazı mushaflarda atıyorum 5 elif içerirken başka mushafda 6 elif harfi içerebilir. ",
                "Özetle burada şu soru haklı olarak soruluyor. Ne malum Reşad'ın Elif harfini doğru saydığı? ",
                "Neden onun sayımlarını doğru kabul edelim ki? ",
                "Belki sırf 19 s uydurmak için fazladan elif harfi saymış olabilir? ",

                new br(),
                new br(),
                
                "- Eğer art niyetli olsa neden çok uzun ayetlerde bunu yapmadı. Şöyle açıklayayım; " ,
                "mesela 2. surenin 233'üncü ayetinde Reşad ın aktarmış olduğu mushafta da Tanzil.net den alınan mushafta da aynı sayıda toplamda 53 tane elif sayımı yapılmış.",
                new br(),
                "Buna rağmen 2:81 de biri 8 elif saymış diğeri 7 elif saymış.",
                "Eğer kandırma yoluna gidecek biri kısa ayetler yerine uzun cümlelerde bu şekilde bir oynamaya giderdi. ",
                "Tam 81 farklı ayette Reşad ve Tanzil.net Elif harfini farklı saymışlar." ,
                "Tabii bu fark maksimum 1-2 olacak şekilde bir fark. Mesela Bakara suresinin 282.inci ayetinde biri 108 öteki 107 saymış.",
                new br(),
                new br(),
                "- Başka veriler Reşad'ın doğru saydığını gösteriyor.",
                new br(),
                new br(),
                new FlexRowCentered
                {
                    new a
                    {

                        href = GetPageLink(PageId.PageVerseListContainsAllInitialLetters),
                        text = "Bütün Başlangıç Harflerini İçeren Ayetler"
                    }
                },
                new br(),
                new FlexRowCentered
                {
                    new a
                    {

                        href = GetPageLink(PageId.AllInitialLettersCombined),
                        text = "Başlangıç harflerinin yan yana yazılması ile oluşan büyük sayılar"
                    }
                }
                
               
            }


        };
    }
}