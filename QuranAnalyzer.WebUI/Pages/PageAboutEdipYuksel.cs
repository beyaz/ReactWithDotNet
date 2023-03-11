namespace QuranAnalyzer.WebUI.Pages;

public class PageAboutEdipYuksel : ReactPureComponent
{
    protected override Element render()
    {
        
        return new Article
        {
            new LargeTitle("Edip Yüksel hakkında"),

            new p
            {
                "Bu konuyu araştırırken bu isim çok çıktığı için özel bir yazı yazma gereğini duydum. ",

                "Bazı konuların Edip tarafından daha farklı ele alındığını gözlemledim."
            },
            new p
            {

                "Hayatını kısaca okuduğunuzu varsayıyorum. Hapis yatması, kardeşinin öldürülmesi, Amerika'ya gitmek zorunda kalması, Reşad Halife ile tanışması falan oldukça ilginç bir hayatı olmuş. ",
                "Gerçekten de cesaretine, zekasına hayran bırakıyor. " ,
                "Sosyal medyada 7 yaşından tut 70 yaşındakilerle bile oturup konuşması vesaire gerçekten iyi.",
                new br(),
                new br(),
                "Edip'in eleştiriye maruz kaldığı noktalar ise şu şekilde;",
                seperation,
                
                "Temsil konumunda olan birinin biraz daha dikkatli davranması gerekiyor.",
                new br(),
                "Bunu biraz açayım. Maalesef insanlar fikirler ile insanları özdeştiriyorlar ve insanın hatalarını fıtratlarını otomatikman fikirlere nispet ediyorlar.",
                " Edip de zaten kendini Sokrates misali insanları rahatsız eden bir ", (b)"at sineği", " olarak tanımlıyor.",
                " Haliyle kimine göre bu tavır itici olarak gelebiliyor ve Edip'in söylediği bazı sözlerin-fikirlerin de otomatikman kulak ardı edilmesine sebep oluyor.",
                
                " Yahut Edip'in yalnızca Kuran söyleminin yanlış anlaşılmasına sebep olabiliyor.",
                seperation,

                "Sanki Edip, Reşad'ın bir numaralı talebesi imiş gibi, her yönü ile Reşad'ın fikirlerini aktaran biriymiş gibi anlaşılabiliyor.",
                " Halbuki iki ayrı insan iki ayrı fıtrat.",
                " Edip; Reşad'ın yanında 1-1.5 yıl kadar kalıyor.",
                seperation,

                "Reşad Halife nin yaptığı ingilizce çeviriyi bir kenara koyup kendi çeviri yapması biraz tezat oluşturuyor. " ,
                seperation,

                "Edip'in Reşad Halifenin ölümü ardından Reşad'ın tavsiyelerini uygulayanlara sizler Reşad'ı putlaştırıyorsunuz muamelesi var.",
                seperation,
                "Namazın 3 vakit olduğu fikrini savunması, Atatürk ile 19 meselesi arasında bağ kurması gibi konular var.",
                seperation,
                
                "Demiyorum ki Edip full hatalı veya full doğru. " ,
                "Bu yazıda sadece Edip üzerinde ve onun 19 ile olan yönü üzerine yaptığım gözlemlerimin bir kısmını paylaştım.",

                " Sonuç olarak Reşad'ı veya 19 meselesini Edip yüksel üzerinden tanımaya kalkarsanız hatalı bir yaklaşım olabilir."
            }

        };

        static Element seperation() => new FlexRowCentered(MarginTopBottom(10)) { "* * *" };
    }
}