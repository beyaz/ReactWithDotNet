using QuranAnalyzer.WebUI.Components;
using static QuranAnalyzer.WebUI.ResourceAccess;
using static QuranAnalyzer.WebUI.Extensions;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.MushafOptionsDetail;

public class View : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {

            
        
        
            new VSpace(10),
            new LargeTitle("Bu Sitede Kullanılan Mushaf Hakkında"),
            new VSpace(15),

            @"Kuran günümüze farklı mushaflar üzerinden gelmiştir. En bilindik mushaflara örnek vercek olur isek Medine mushafı ve Osman mushafı'nı örnek verebiliriz.",
            new br(),
            new br(),
            "Peki farklı mushaf ne demek? Kuran farklı coğrafyalara dağılırken bizdeki noktalama işaretleri eklenerek yayıldı. El ile yazılarak çoğaltıldığını da hesaba katınız. O devirdeki Arapların rakamlardan haberi dahi yok. Roma rakamlarında olduğu gibi  harfleri aynı zamanda rakam olarak kullanıyorlar.",
            "En başta yapılacak küçük bir hata otomatik olarak çoğaltılan kopyalara da yansımış olacaktır. " ,
            "Mesela Türkiye'ye en yakın coğrafyalardan biri olan İran'dan bir Kuran ile Türkiyedeki diyanetin bastırmış olduğu Kuranını önünüze açın ve Elif(ﺍ) harflerini inceleyin arada bazı Elif(ﺍ) harflerinin birbirini tutmadığını görebilirsiniz. ",
            new br(),
            new br(),
            "Dileyenler konuyu daha detaylı inceleyebilirler. Aşağıda örnek bir çalışmayı paylaşıyorum. ",
            new br(),
            new img
            {
                src   = Img("MushafDifferences.jpg"),
                style = { width = "100%",  height = "auto", display = "block", marginLeftRight = "auto" }
            },

            new br(),
            
            "İşte bu farklılıklar beraberinde bazı zorlukları da getiriyor. Bir örnek vermek gerekir ise 7. surenin 69. ayetindeki bestaten kelimesi yazılırken sin ve Sad harleri üst üste yazılmış.",
            "Özetle oradaki harfin hangisi olduğu üzerine tartışma var. Kimisi sin diye yazılır Sad diye okunur demiş. Elbette başka yorumlar da var.",
            
            new br(),
            new br(),
            "Peki Kuranı  elektronik ortama aktaranlar arasında genelde çoğunlukla kullanılan ",new a{text ="tanzil.net" ,href = "https://tanzil.net/docs/tanzil_project"} ,
            " den aldığım mushafı kullandım.",
            new br(),
            new br(),
            "Peki tanzil.net 'den de önce bu bilgisayara aktarma işlemini ilk Reşad Halife 1970 li yıllarda yapmış.",
            "Düşünün ki 70 li yıllarda zaten bilgisayar herkesin erişebileceği bir alet değil. Hatta o zamanki yazılım dünyasında arap harleri dahi yok. ",
            "Reşad Halife farklı mushafları da önüne dizip tek tek inceleyip Arap alfabesine karşılık gelen herbir harf için İngilizce harf karşılığını mesela Arapçadaki ",AsLetter(Miim)," yerine İngilizcedeki M harfini yazarak tüm kuranı bilgisayara aktarıyor ve bu başlangıç harflerini araştırmaya koyuluyor.",
            new br(),
            new br(),
            "Bu konuyu araştırırken bulabildiği tek elektronik mushaf olan tanzil.net 'den aldığım mushafı kullandım.",
            "İşte harf arama olaylarını yaparken bu mushaf farklılıklarını da göz önüne alarak yapmak durumundasınız.",
            "Bundan dolayıdırki bu ayarları kullabilirsiniz. " ,
            "Mesela Elif(ﺍ) harfleri için isterseniz tanzil.net'i referans alarak sayımlar yapın isterseniz Reşad Halifenin Elif sayımlarını baz alarak yapın.",
            "Sonuçta tanzil.net deki mushafı da 'Hamid Zarrabi-Zadeh' adında iranlı bir öğretim üyesi bilgisayara aktarmış. İkisi de insan :) ",
            new VSpace(15)
        };
    }
}