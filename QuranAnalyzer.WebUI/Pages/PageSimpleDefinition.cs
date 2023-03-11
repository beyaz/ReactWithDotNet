namespace QuranAnalyzer.WebUI.Pages;

public class PageSimpleDefinition : ReactPureComponent
{
    protected override Element render()
    {
        var ul_style = DisplayFlex + FlexDirectionColumn + Gap(8);

        return new Article
        {
            new LargeTitle("Tanım"),

            new p
            {
                " Yüzyıllar boyu Kuranda bu başlangıç harfleri için farklı farklı bir çok fikir ortaya atılmıştır.",
                " Kimi alim bu harfler sureleri birbirinden ayırt etmek içindir demiş. ",
                " Bazı alimler bu harfler Allah'ın isimlerinin bazılarının anahtarlarıdır demiş. ",
                " Bazı alimler bu harflerin sayısal değerleri ilerde olacak olan olayların tarihlerini verir demiş. ",
                "Özetle bu başlangıç harfleri konusunda bir çok yorum var.",
            },

            new p
            {
                " Size bir mektup geldiğini hayal edin ve mektubun ilk satırında sadece bir K harfi olduğunu düşünün. ",
                " İster istemez burada bir kasıt ararsınız. Bir açıklama beklersiniz.",
            },

            new ul(ul_style)
            {
                (li)" Ya mektubu gönderen kişi yahut yazıya döken kişi burada bir yazım hatası yapmıştır.",
                (li)" Mektubu gönderen kişi burada kasıtlı bir şey yapmış ve bana bir şey demek istiyor."
            },

            new p
            {
                "Durumumuz bu örneğe çok benziyor. Elimizde bir mektuplar - bilgilendirme yazıları var.",
                " Bu mektuplardan bazılarının isimleri şöyle: Tevrat, İncil, Kuran.",
            },

            new p
            {
                (strong)"Peki bu kitapların doğru olduğu ne malum?",
            },

            new p
            {
                " Evet soru gayet haklı bir soru. Eğer bu kitapların içinde akla mantığa ters bir tek cümle var ise kitabın tamamına şüphe ile yaklaşmak zorundayız.",
                " Önceki nesillerin bu soruyu çokça dillendirmemesi bizim neslimizi bağlamaz. ",
                " Kaldı ki bu bundan 100 yıl öncesinde bu soruyu Müslüman bir coğrafyada zaten dillendiremezdiniz.",
            },
            new p
            {
                "Bir kaç örnek ile konuyu detaylandıralım",
            },

            new ul(ul_style)
            {
                (li)"- Kuranda yüksekten düşen kayaların Allah korkusu ile düştüklerinden bahsedilir.",

                (li)"- Kuranda seçim özgürlüğünün başka varlıklara da teklif edildiği fakat sadece biz insanların bunu kabul ettiğinden ve bunun cahilce bir hareket oduğundan bahsedilir.",

                (li)"- Kuranda göklerin Yaratıcı'nın sağ elinde dürülmüş olduğundan bahseder.",
            },

            (p)"Eğer bu kitabın Yüceler Yücesi'nden geldiğine inanıyor isek bu gibi ifadeleri zihnimizde sağlam bir zemine oturtmalıyız.",

            new p
            {
                " Hatta daha da ileri gidip şu soruyu soralım. Neden atalarımızdan gelen her bilgiyi peşinen doğru kabul ediyoruz ? ",
                " Ya onlar bazı konularda yanıldılarsa ?"
            },

            new p
            {
                "Hatta bir an olsun batı insanının zihin dünyasına girelim. ",
                "İç savaşlar, kan ve gözyaşı çoğunlukla Müslüman coğrafyada oluyor, ",
                "bu insanların dinleri doğru olsaydı en başta kendilerine faydası olurdu. " +
                "Ne malum Müslümanların iddia ettikleri gibi Muhammed adında bir Arabın peygamber olduğu?"
            },

            (p)" Özetle ateist sayısının sürekli arttığını da göz önüne alırsak şu soru oldukça yüksek sesle sorulmaya başlandı. ",
            (strong)" Ne malum Kuranın Allah kelamı olduğu ?",

            new p
            {
                "İşte bu sorunun cevabı yine Kuranın kendisinden çıkmış durumda.",
                " Bilgeler Bilgesi olan  Kuranın üzerine biz insanların anlayabileceği kolaylıkta fakat taklit edemeyeceği zorlukta bir sayısal harmoni-örüntü-imza-desen eklemiş. ",
                " Eklemiş ki bu kitabın kendisinden gelen bir kitap olduğuna emin olunsun ve içerisindeki dehşetli uyarılara kulak asılsın, verilen müjdeler için çaba sarf edilsin.",
                " Bilim ve teknolojinin patlama yaptığı bu çağda 'Bilgeler Bilgesi' biz insanoğluna yine çağın diline uygun olarak teknoloji üzerinden bilgisayar ile bir mesaj veriyor.",
                " Tanrı'nın bu matematiksel imzası 19 sayısı üzerine kuruludur.",
                " Kurandaki bir çok matematiksel verinin 19 rakamı ile ilişkili olmasıdır.",
                " Bu matematiksel veriler öyle bir derecedeki insanoğlunun günümüz bilgisayarları ile yapabileceğinden çok daha üst seviyededir. ",
                " Bir Olan'ın Kuran üzerindeki matematiksel imzasıdır diyebiliriz."
            },

            new p
            {
                " İddia kısaca bu şekildedir. Gelin bu iddiayı beraber inceleyelim. Yalnız başımıza karar verelim.",
                (strong)" Bu sitedeki anlatılan kısım 19 sisteminin en temel verisi olup sadece başlangıç harfleri ile ilgili kısımdır.",
                " Bunun dışında 19 sisteminin alt başlıkları diyebileceğimiz  büyük sayılar ile ilgili veyahut harflerin sayısal değerleri ile ilgili başka bir çok ilginç verisi var."
            }
        };
    }
}