using QuranAnalyzer.WebUI.Components;
using static QuranAnalyzer.WebUI.Extensions;


namespace QuranAnalyzer.WebUI.Pages.PreInformation;

public class PreInformationView : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new VSpace(10),
            new LargeTitle("Kuran Hakkında Bazı Bilgiler"),
            new VSpace(15),
            @"
Bu bölümde Kuran hakkında dikkat çeken bazı bilgiler verilecektir.
",
            new VSpace(15),
            new li { text = "Kuran toplamda 114 tane sure adı verilen bölümden oluşur. Mesela 1. sure Fatiha suresidir." },
            new VSpace(15),
            new li { text = "Her bölümün(surenin) başında besmele vardır. Sadece 9. surenin başında besmele yoktur." },
            new VSpace(15),
            new li { text = "Sureler vahyedilirken karışık sırada geliyor. Mesela  bazen 2. surenin bir kısmı geliyor sonra başka bir surenin başka bir kısmı geliyor. " },
            new VSpace(15),
            new li { text = @"Genel kabule göre Kuranın kitaplaşması halife Ebubekir, çoğaltılması ise halife Osman zamanında yapılıyor. 
Rivayetlere göre halife Osman zamanında, o zamanki insanların kendi kuran notları, sakladıkları kuran parçaları başka karmaşaya sebep olmasın diye Ebubekir zamanındaki orjinal mushaf dahil yakılmıştır." },
            new VSpace(15),
            new li { text = @"Kuran farklı mushaflar üzerinden bugüne gelmiştir. 
Yani klasik herkesin öyle tahmin ettiği gibi yeryüzündeki bütün kuranlar harfi harfine aynı değildir. 
İran'dan ve Türkiye'den ve Afrika'dan kuranları  önünüze açtığınızda elif harflerinde farklılıklar göreceksiniz. 
İsterseniz farklı mushafları aşağıdaki linkten inceleyebilirsiniz." },
            new a{href = "https://www.quranflash.com/home?en", text = "Kuran mushafları"},
            new VSpace(15),
            new li { text = @"Kurandaki bazı kelimeler ilginç bir şekilde farklı yazılmıştır. 
Çoğumuzun bildiği Mekke şehri Kuranda bir cümlede Bekke diye ifade edilir." },
            new VSpace(15),
            new li { text = @"Kuranda bazı bölümlerin başında harfler vardır. En çok bilinen Yasin suresinin başında Ya(ي) ve Sin(س) harfleri vardır.
Bazı bölümlerin başında bir tane harf olurken mesela 50. surenin başındaki Kaf(ق) harfi gibi. Bazı bölümlerde sure başında iki tane harf vardır. 
Mesela 40. ve 46. arası 7 tane surenin başlarında sadece Ha(ح) ve Mim(م) olmak üzere iki harf vardır. 
En çok ise 19. surede beş tane başlangıç harfi vardır. Kaf(ق) - Ha(ه) - Ya(ي) - Ayn(ع) - Sad(ص). 
Toplamda 29 surenin başında böyle harfler vardır. Başlangıç harfleri-Kesik harfler-hurufu mukatta gibi isimlerle anılmaktadır.
Tarih boyu bu başlangıç harleri ile ilgili bir çok farklı yorum yapılmıştır.
Yine bu harflerin geçtiği surelerin bir kısmında ilk çümleler şöyledir. Bunlar kitabın ayetleridir-kanıtlarıdır-işaretleridir." },

            new VSpace(15),
            new li { text = @"Kurandaki bazı kelimelerin geçişleri anlamları/olayları ile ilgili olarak çok ilginç sayıda geçmektedir. 
Mesela gün gelimesinin 365 defa geçmesi buna bir örnek olarak verilebilir.
Adem ve İsanın durumu aynıdır denmesi ve Adem / İsa kelimeleri 25'er defa geçmesi bunlara örnek olarak verilebilir. 
Dilerseniz aşağıdaki linklerden bu sayımları kendiniz yapabilirsiniz." },
            new a{href =  PageUrlOfDays365, text = "Gün Sayısının 365 kez geçmesi"},
            new br(),
            new a{href = PageUrlOfDays30, text = "Günler kelimesinin 30 defa geçmesi"},
            new br(),
            new a{href = PageUrlOfDays30, text = "Adem ve İsa kelimelerinin geçiş adeti"},

            new VSpace(15)
        };
    }
}