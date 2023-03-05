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

            @"Kuran günümüze farklı mushaflar üzerinden gelmiştir. 
En bilindik mushaflara örnek verecek olur isek Medine mushafı ve Osman mushafı'nı örnek verebiliriz.",
            new br(),
            new br(),
            "Peki farklı mushaf ne demek? ",
            new br(),
            "Kuran farklı coğrafyalara dağılırken noktalama işaretleri eklenerek yayıldı. ",
            "El ile yazılarak çoğaltıldığını da hesaba katınız. Hatta o devirdeki Araplar aynen Roma rakamlarında olduğu gibi harfleri aynı zamanda rakam olarak kullanıyorlar. ",
            "En başta yapılacak küçük bir hata otomatik olarak çoğaltılan kopyalara da yansımış olacaktır. ",
            "Mesela Türkiye'ye en yakın coğrafyalardan biri olan İran'dan bir Kuran ile Türkiyedeki diyanetin bastırmış olduğu Kuranını önünüze açın ve Elif(ﺍ) harflerini inceleyin arada bazı Elif(ﺍ) harflerinin birbirini tutmadığını kendiniz de gözlemleyebilirsiniz. ",
            new br(),
            new br(),
            "Dileyenler yazım farkları konusunu daha detaylı inceleyebilirler. Aşağıda örnek bir çalışmayı paylaşıyorum. ",
            new br(),
            new img
            {
                src   = FileAtImgFolder("MushafDifferences.jpg"),
                style = { width = "100%", height = "auto", display = "block", marginLeftRight = "auto" }
            },

            new br(),
            seperation,
            new p
            {
                "Eski mushafları dijital ortama aktarmayı amaçlayan bir çalışma var. ",
                "Hangi kelimenin hangi mushafta nasıl yazıldığına ait detaylı olarak bu siteden de inceleyebilirsiniz. ",
                "Eğer mushaftaki yapraklar oldukça hasarlı ise maalesef o kısımlar elektronik ortama aktarılamamış oluyor.",
                new br(),
                new FlexRowCentered
                {
                    new a { href = "http://elktb.net/Mushaflar/MushafGoruntule", text = "El Kitab" }
                }
            },
            seperation,

            new p
            {
                "İşte bu farklılıklar beraberinde bazı zorlukları da getiriyor. ",
                "Bir örnek vermek gerekir ise 7. surenin 69. ayetindeki bestaten kelimesi yazılırken ", AsLetter(Siin), " ve ", AsLetter(Saad), " harfleri üst üste yazılmış. ",
                " Böyle yazılmış çünkü bazı mushaflarda sin harfi var bazılarında sin harfinin olduğu yerde sad harfi var. ",
                new img
                {
                    src   = FileAtImgFolder("7_69_sin_sad.png"),
                    style = { width = "91px", height = "auto", display = "block", marginLeftRight = "auto" }
                },
                "Oradaki harfin hangisi olduğu üzerine tartışma var. Kimisi sin diye yazılır Sad diye okunur demiş. Elbette başka yorumlar da var.",
            },

            seperation,

            "2000'li yılların başında bilgisayar teknolojisindaki hızlı gelişme ile beraber Kuran elektronik ortama aktarılmış. Bu konuda çoğunlukla kullanılan elektronik mushaf ", new a { text = "tanzil.net", href = "https://tanzil.net/docs/tanzil_project" },
            " in hazırlamış olduğu çalışmadır. Bu sitede tanzil.net den indirdiğim mushafı kullandım.",
            new br(),
            new br(),
            "Tanzil.net 'den de önce bu bilgisayara aktarma işlemini ilk olarak Reşad Halife 1970 li yıllarda yapmış. ",
            "Düşünün ki 70 li yıllarda zaten bilgisayar herkesin erişebileceği bir alet değil. Hatta o zamanki yazılım dünyasında Arap harleri dahi yok. ",
            "Reşad Halife farklı mushafları da önüne dizip tek tek inceleyip Arap alfabesine karşılık gelen herbir harf için İngilizce harf karşılığını mesela Arapçadaki ", AsLetter(Miim), " yerine İngilizcedeki M harfini yazarak tüm Kuranı bilgisayara aktarıyor ve bu başlangıç harflerini araştırmaya koyuluyor.",
            new br(),
            new br(),
            "Bu konuyu araştırırken bulabildiği tek elektronik mushaf olan tanzil.net 'den aldığım mushafı kullandım.",
            "İşte harf arama olaylarını yaparken bu mushaf farklılıklarını da göz önüne alarak yapmak mecburiyetindesiniz. ",
            "Bundan dolayıdırki bu ayarları kullabilirsiniz. ",
            "Mesela Elif(ﺍ) harfleri için isterseniz tanzil.net'i referans alarak sayımlar yapın isterseniz Reşad Halifenin Elif sayımlarını baz alarak yapın. ",
            "Tanzil.net deki mushafı da 'Hamid Zarrabi-Zadeh' adında İranlı bir öğretim üyesi bilgisayara aktarmış. Sonuçta ikisi de insan :) ",
            new br(),
            new br(),
            new SubTitle("Ayarlar"),
            new br(),

            new FlexColumn(Gap(40))
            {
                new FlexColumn(Gap(10))
                {
                    new SwitchWithLabel
                    {
                        Label         = "Elif sayımları için Tanzil.net'i referans al",
                        LabelMaxWidth = 250,
                        IsDisabled    = true
                    },

                    "Bu seçeneği seçerseniz Tanzil.netdeki Elif sayımlarını referans alır. ",
                    "Seçmez iseniz Reşad Halife'nin Elif sayımlarını referans alır."
                },

                new FlexColumn(Gap(10))
                {
                    new SwitchWithLabel
                    {
                        Label         = "7:69 ve 2:245 daki bestaten ve yebsutu kelimelerindeki sad-sin yazım farklılığında Sad harfini tercih et",
                        LabelMaxWidth = 250,
                        IsDisabled    = true
                    },

                    "Bu bahsi geçen iki kelime farklı mushaflarda farklı yazılmış. Kimisinde sin harfi kullanılmış kimisinde sin harfi olan yerde sad harfi kullanılmış.",
                    "Eğer bu seçeneği seçerseniz bu iki yerdeki iki harf farklılığında Sad harfini tercih etmiş olursunuz."
                },

                new FlexColumn(Gap(10))
                {
                    new SwitchWithLabel
                    {
                        Label         = "68:1 tek nun olarak say",
                        LabelMaxWidth = 250,
                        IsDisabled    = true
                    },

                    "68:1 in ilk harfi nun-vav-nun olarak saymak istemiyorsanız bu seçeneği işaretleyebilirsiniz. ",
                    "Buradaki NN mi N mi olduğu ile ilgili detaylı açıklama soru cevap kısmında verilmiştir."
                },

                new FlexColumn(Gap(10))
                {
                    new SwitchWithLabel
                    {
                        Label         = "11:70 ve 30:21 surelerdeki Lam harf farklılığında Tanzil.neti tercih et",
                        LabelMaxWidth = 250,
                        IsDisabled    = true
                    },

                    "11:70 ve 30:21 surelerdeki Lam harf farklılığı şu demek bu ayetlerde tanzil.net Lam harfini bir fazla saymış.",
                },

                new FlexColumn(Gap(10))
                {
                    new SwitchWithLabel
                    {
                        Label         = "6:5 ve 26:6 surelerdeki [enba'u] kelimesindeki Vav harf farklılığında Tanzil.neti tercih et",
                        LabelMaxWidth = 250,
                        IsDisabled    = true
                    },

                    "6:5 ve 26:6 numaralı ayetlerdeki [enba'u] kelimesi bazı mushaflarda vav'lı yazılmış bazılarında ise vav hardi olmadan yazılmış. " ,
                    "Bu vav harfinin başlangıç harfleri ile ilgisi yoktur. ",
                    "Kuran üzerindeki verileri incelerken böyle bir ayara ihtiyacım olduğu için ekledim."
                },

                new FlexColumn(Gap(10))
                {
                    new SwitchWithLabel
                    {
                        Label         = "75:13 nolu ayetteki [yunebbeu](يُنَبَّؤُ) kelimesindeki 'vav' harf farklılığında vav harfi olan versiyonu seç.",
                        LabelMaxWidth = 250,
                        IsDisabled    = true
                    },

                    "75:13 nolu ayetide bulunan  [yunebbeu](يُنَبَّؤُ) kelimesi bazı mushaflarda vav'lı bazılarında vav'sız yazılıyor." ,
                    "Bu vav harfinin başlangıç harfleri ile ilgisi yoktur. ",
                    "Kuran üzerindeki verileri incelerken böyle bir ayara ihtiyacım olduğu için ekledim."
                }
            },
            new p
            {
                "Tanzil.net'den aldığım mushafta 12:39 ve 12:41 nolu ayetlerde bulunan [ya sahibeyi](يَا صَاحِبَيِ) ifadesi orjinal Osman mushafından aktarım yapılırken eksik aktarılmış." ,
                " Bu sitede Osman mushafındaki orjinal hali baz alınmıştır. Sanırım burada tanzil.net'in bir aktarım hatası var. "
            },
            new p
            {
                "Özetlersek genel durum şöyle; Elif sayımlarını hariç tutarsak, Tanzil.net'in sayımları ile Reşad Halife'nin sayımları arasında tüm mushaf boyunca toplamda 5 tane harf farklılığı vardır. ",
                "Bir tanesi Nun harfinde iki tanesi Lam harfinde ve 2 tanesi de Sin harfi olmak üzere toplamda 5 tane farklılık var denilebilir. ",
                "Elif harfini tamamen farklı ele almak lazım. Çünkü  nerdeyse bir çok mushafta Elif harfleri birbirlerinden tamamen farklıdır. ",
                "Unutmayalım ki mushafdaki Elif harfini bilgisayara aktaran kişilerin Arapça bilgisi de işin içine giriyor. "
            }
        };

        static Element seperation() => new FlexRowCentered(MarginTopBottom(10)) { "* * *" };
    }
}