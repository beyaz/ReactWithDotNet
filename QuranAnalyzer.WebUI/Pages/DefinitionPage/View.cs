using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.DefinitionPage;

public class DefinitionView : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new VSpace(10),
            new LargeTitle("Tanım"),
            new VSpace(15),

            " Yüzyıllar boyu Kuranda bu başlangıç harfleri için farklı farklı bir çok fikir ortaya atılmıştır.",
            " Kimi alim bu harfler sureleri birbirinden ayırt etmek içindir demiş. ",
            " Bazı alimler bu harfler Allahın isimlerinin bazılarının anahtarlarıdır demiş. ",
            " Bazı alimler bu harflerin sayısal değerleri ilerde olacak olan olayların tarihlerini verir demiş.",
            "Özetle bu başlangıç harleri konusunda bir çok yorum var.",
            new br(),
            new br(),
            " Size bir mektup geldiğini hayal edin ve mektubun ilk satırında sadece bir K harfi olduğunu düşünün. ",
            " İster istemez burada bir kasıt ararsınız. Bir açıklama beklersiniz.",
            new br(),
            (li)" Ya mektubu gönderen kişi yahut yazıya döken kişi burada bir yazım hatası yapmıştır.",
            (li)" Mektubu gönderen kişi burada kasıtlı bir şey yapmış ve bana bir şey demek istiyor.",

            new br(),
            "Durumumuz bu örneğe çok benziyor. Elimizde bir mektuplar - bilgilendirme yazıları var.",
            " Bu mektuplardan bazılarının isimleri şöyle: Tevrat, İncil, Kuran.",
            
            new br(),
            (strong)"Peki bu kitapların doğru olduğu ne malum ?",
            new br(),
            " Evet soru gayet haklı bir soru. Eğer bu kitapların içinde akla mantığa ters bir tek cümle var ise kitabın tamamına şüphe ile yaklaşmak zorundayız.",
            " Önceki nesillerin bu soruyu çokca dillendirmemesi bizim neslimizi bağlamaz. " ,
            " Zaten bu bundan 100 yıl öncesinde bu soruyu müslüman bir coğrafyada zaten dillendiremezdiniz.",
            new br(),
            new br(),
            "Bir kaç örnek ile konuyu detaylandıralım",
            new br(),
            "- Kuranda yüksekten düşen kayaların Allah korkusu ile düştüklerinden bahsedilir.",
            new br(),
            "- Kuranda seçim özgürlüğünün başka varlıklara da teklif edildiği fakat sadece biz insanların bunu kabul ettiğinden ve bunun cahilce bir hareken oduğundan bahsedilir.",
            new br(),
            "- Kuranda göklerin Yaratıcı'nın sağ elinde dürülmüş olduğundan bahseder.",

            new br(),
            new br(),
            "Eğer bu kitabın Yüceler Yücesi'nden geldiğine inanıyor isek bu gibi ifadeleri zihnimizde sağlam bir zemine oturtmalıyız.",
            new br(),
            " Hatta daha da ileri gidip şu soruyu soralım. Neden atalarımızdan gelen her bilgiyi peşinen doğru kabul ediyoruz ? " ,
            " Ya onlar bazı konularda yanıldılarsa ?",
            new br(),
            new br(),
            "Hatta bir an olsun batı insanının zihin dünyasına girelim. Ne malum müslümanların iddia ettikleri gibi Muhammed adında bir arabın peygamber olduğu ?",

            new br(),
            new br(),
            " Özetle ateist sayısının sürekli arttığını da göz önüne alırsak şu soru oldukça yüksek sesle sorulmaya başlandı. ",
            (strong)" Ne malum Kuranın Allah kelamı olduğu ?",
            new br(),
            new br(),
            "İşte bu sorunun cevabı yine Kuranın kendisinden çıkmış durumda.",
            " Bilgeler Bilgesi olan  Kuranın üzerine biz insanların anlayabileceği kolaylıkta fakat taklit edemeyeceği zorlukta bir sayısal harmoni-örüntü-imza-desen eklemiş. ",
            " Eklemiş ki bu kitabın kendisinden gelen bir kitap olduğuna emin olunsun ve içerisindeki dehşetli uyarılara kulak asılsın, verilen müjdeler için çaba sarfedilsin.",
            " Tanrı'nın bu matematiksel imzası 19 sayısı üzerine kuruludur.",
            " Kurandaki bir çok matematiksel verinin 19 rakamı ile ilişkili olmasıdır.",
            " Bu matematiksel veriler öyle bir derecedeki insanoğlunun günümüz bilgisayarları ile yapabileceğinden çok daha üst seviyededir. ",
            " Bir Olan'ın Kuran üzerindeki matematiksel imzasıdır diyebiliriz.",
            
            new br(),
            new br(),
            
            " İddia kısaca bu şekildedir. Gelin bu iddiayı beraber inceleyelim. Yalnız başımıza karar verelim.",
            (strong)" Bu sitedeki anlatılan kısım sadece başlangıç harleri ile ilgili kısımdır.",
            " Bunun dışında 19 sisteminin alt başlıkları diyebileceğimiz  büyük sayılar ile ilgili veyahut harflerin sayısal değerleri ile ilgili başka bir çok ilginç verisi var.",

            new VSpace(15)
        };
    }
}