namespace QuranAnalyzer.WebUI.Pages;

public class WhereIsTheProblemPage : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Eleştirilen Noktalar"),

            new p
            {
                @"Madem bu başlangıç harflerindeki olay bu kadar şahane. İnsanın nefesi kesiliyor. Peki reddedenler niye reddediyorlar."
            },
            new p
            {
                @"Gayet haklı bir soru.",
                new br(),
                "Elinize sıradan bir Kuran alın bu sitede başlangıç harflerinde gösterilen her bir maddeyi bizzat kendiniz teyit edebilirsiniz.",

                new br(),
                new br(),
                "Sadece iki maddeyi teyit edemezsiniz.",
                new br(),
                new br(),

                "1 - Elinizdeki mushafta 68. sureyi açtığınızda surenin en başında sadece bir tane ", AsLetter(ArabicLetter.Nun), " harfi görürsünüz.",
                " 19 sistemini ilk keşfeden kişi burada tek nun değil çift nun harfi olduğunu söylüyor. ",
                " Hatta bu 19 sistemini ilk bu nun ile başlayan surede fark ediyor. Orijinal mushafta bunun iki nun ile yazıldığını gördüm diyor.",

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
                " Eğer kandırma yoluna gidecek biri kısa ayetler yerine uzun cümlelerde bu şekilde bir oynamaya giderdi.",
                " Tam 81 farklı ayette Reşad ve Tanzil.net Elif harfini farklı saymışlar." ,
                " Tabii bu fark maksimum 1-2 olacak şekilde bir fark. Mesela Bakara suresinin 282.inci ayetinde biri 108 öteki 107 elif harfi saymış.",
                new br(),
                new br(),
                "- İşte bu Elif harfinin sayımları konusunda başka veriler Reşad'ın doğru saydığını gösteriyor diye düşünüyorum." ,
                " Aşağıda iki ayrı incelemeyi paylaşıyorum. Aşağıdaki iki ayrı veri de Reşad'dan çok sonra ortaya çıkmıştır. ",
                new br(),
                new br(),
                new a
                {
                    href = GetPageLink(PageId.PageVerseListContainsAllInitialLetters),
                    text = "Bütün Başlangıç Harflerini İçeren Ayetler"
                },
                new br(),
                new br(),
                new a
                {
                    href = GetPageLink(PageId.AllInitialLettersCombined),
                    text = "Başlangıç harflerinin yan yana yazılması ile oluşan büyük sayılar"
                },
                
                new br(),
                new br(),
                "Burada kendi yorumumu paylaşmak istiyorum.",
                " Zaman içerisinde 19 ile ilgili veriler daha netlik kazanacak. " ,
                " Mesela Sad harfi 3 ayrı sureye dağılmış diye garip gelebilir. " ,
                " Reşad öldürüldükten 30 yıl sonra araştırmalar sayesinde sadece bir tek Sad harfinde bile çok ilginç veriler bulundu. " +
                " Dilerseniz daha detaylı araştırabilirsiniz.",
                new br(),
                "Özetle 68. suredeki NuN olayı ve Elif sayımları üzerinde tartışılan noktalardır. " ,
                " Belki de burası ilerde daha netlik kazanacak şimdilik bunun imtihan kısmıdır. " ,
                " Yalnız Kuran fikrini kabullenemeyenler bu açık kapılardan çıkıp gidiyorlar. " ,
                " 19 üzerinde muhteşem veriler varken Elif harfine takılıyor tüm sistemi çöpe atıyor. ",
                " Umarım üzerinde tartışma yaşanan kısmını anlatabilmişimdir. Tercih sizin. "
            }


        };
    }
}