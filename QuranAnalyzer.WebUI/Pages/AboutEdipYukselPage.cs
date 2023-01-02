using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages;

public class AboutEdipYukselPage : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Edip Yüksel hakkında"),

            new p
            {
                "Bu konuyu araştırırken bu isim çok çıktığı için özel bir yazı yazma gereğini duydum. ",

                "Türkçemizde güzel bir deyim vardır; ",(b)"Yiğidi öldür ama hakkını ver"," bu yazıda bu metodu uyguladım."
            },
            new p
            {
                @"Öncelikle gördüğüm iyi şeyleri söyleyeyim",
                
                new br(),
                
                
                "Hayatını okuduğunuzda, hapis yatması, kardeşinin öldürülmesi, Amerika'ya gitmek zorunda kalması, Reşad Halife ile tanışması felan oldukça ilginç bir hayatı olmuş. ",
                "Gerçekten de cesaretine, zekasına diyecek herhangi bir lafımız zaten olamaz." ,
                "Sosyal medyada 7 yaşından tut 70 yaşındakilerle bile oturup konuşması vesaire gerçekten iyi.",
                new br(),
                new br(),
                "Eleştirdiğim nokta ise şu;",
                new br(),
                "Temsil konumunda olan birinin biraz daha dikkatli davranması gerekiyor.",
                new br(),
                "Bunu biraz açayım. Maalesef insanlar fikirler ile insanları özdeştiriyorlar ve insanın hatalarını fıtratlarını otomatikmen fikirlere nispet ediyorlar.",
                "Edip de zaten kendini Sokrates misali insanları rahatsız eden bir ", (b)"at sineği", " olarak tanımlıyor. ",
                "Haliyle kimine göre bu tavır itici olarak gelebiliyor ve Edip'in söylediği bazı sözlerin de otomatikmen kulakardı edilmesine sebep oluyor. ",
                
                "Yahut Edip'in yalnızca Kuran söyleminin yanlış anlaşılmasına sebep olabiliyor. ",
                "Yahut sanki Edip Reşadın bir numaralı talebesi imiş gibi, her yönü ile Reşadın fikirlerini aktaran biriymiş gibi anlaşılabiliyor.",
                "Halbuki iki ayrı insan iki ayrı fıtrat.",
                "Reşadın seskayıtlarında bir kere bile öfkelendiğini görmedim.",
                "Reşad Halife nin yaptığı ingilizce çeviriyi bir kenara koyup kendi çeviri yapması felan pek de anlaşılır şeyler değil.",
                "Özetle Edip'in Reşad Halifenin ölümü ardından Reşadın tavsiyelerini uygulayanlara sizler Reşadı putlaştırıyorsunuz muamelesi oldukça ilginç.",
               
                "Demiyorum ki Edip full hatalı. Ama Atatürk ile 19 meselesi arasında bağ kurması bana göre hatalı bir yaklaşım.",
                "Namazın 3 vakit olduğu fikrini savunması hatalı.",

                "Sonuç olarak Reşad'ı veya 19 meselesini Edip yüksel üzerinden tanımaya kalkarsanız hatalı bir yaklaşım olur. "
            }

        };
    }
}