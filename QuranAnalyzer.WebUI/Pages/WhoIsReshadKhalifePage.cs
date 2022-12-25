using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages;

public class WhoIsReshadKhalifePage : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Reşad Halife Kimdir? Ne söylüyor?"),

            new p
            {
                @"
Bu 19 meselesini incelerken farkettim ki meselenin odağındaki isim bu sistemi ilk bulan kişi Reşad Halife ismi.
 Daha doğrusu onun fikirleri. Zaten fikirleri yüzünden de öldürüldü.
 Kısaca bu kişi kimdir? Derdi neymiş?
 Özetle ne söylemiş? İşte bu bilgileri kısa kısa olacak şekilde özetlemeye çalıştım. 
 Tekrarlamakda fayda görüyorum. Bu fikirler doğrudur yanlıştır gibi bir iddiam yok. 
 Daha doğrusu benim doğru veya yanlış demem herhangi bir şey ifade etmez. Ben sadece bu adamın ne demeye çalıştığını özetlemeye çalıştım. 
 Umarım faydalı olur."
            },

            (h5)"İlk Yıllar ve Kariyer",
            new p
            {
                "1935 yılında Mısırda doğmuş. Babası tarikat şeyhi olan biridir. " +
                "Bunu özellikle belirttim. Ehli sünnet bir tarikat ortamında, dindar bir ailede doğup büyümüş birinden bahsediyoruz. " +
                "Mısırda üniversite kimya bölümünde okuyor. Onur derecesi alıyor. 25 li yaşlarda Amerikaya doktora için gidiyor ve biyokimya alanında yüksek lisans " +
                "ve doktorasını tamamlıyor. Kendi alanında konferanslar felan veriyor. Birleşmiş milletler kalkınma örgütünde kimyager olarak çalışıyor." +
                "Kısaca kariyer olarak oldukça iyi giden bir hayatı olmuş diyebiliriz."
            },
            new p
            {
                "30 lu yaşlarda Amerikaya gelince bir yandan doktorasını tamamlarken bir yandan da ingilizce yazılmış kuran meallerinin çoğunu okuyor." +
                "Eski tefsirleri zaten Mısırdayken inceleyen biri. Anadili zaten arapça." +
                "Yazılan ingilizce Kuran çevirilerini inceledikten sonra farkediyor ki bu çevirilerdeki eksik gördüğü veya tevhide uymayan fikirler olduğunu görüyor." +
                "En azından kendi evlatlarımın okuyabileceği Tevhidi merkez alan bir çeviri yapma niyeti ile çeviriye başladığını kendisi söylüyor. " +
                "Bir ayetin manasını tam anlamadan ötekine  geçmeyeceğine dair kendine de şart koşuyor." +
                "1. sureyi çevirdikten sonra 2. sureye geliyor ve sure başındaki elif-lam-mim harflerine gelince takılıyor. ",
                "Çalıştığı şirket çalışanlarını yazılım kursuna gönderiyor kendisi de bu sayede yazılım öğrenmiş.",
                "Acaba Kuranda bu başlangıç harfleri ile ilgili verileri bilgisayar yazılımı ile incelemek istiyor.",
                "Ardından farklı kuran mushaflarını inceleyerek arapçadaki her bir harfe karşılık ingilizce bir harf ile Kuranı bilgisayara aktarıyor.",
                " Açıkcası oldukça zahmetli bir iş olsa gerek :) "
            },

            new p
            {
                "İlk farkettiği şeylerden biri Kaf harfi ile başlayan surelerde Kaf harfinin biraz daha yoğun kullanıldığını görüyor.",
                "Bu veriyi 1973 tarihinde makale olarak yayınlıyor.",
                "Daha sonra 1974 de ise farkediyorki bu başlangıç harflerinin olduğu surelerin başındaki harfler bulundukları surelerde hep 19 un katları şeklinde geçiyor.",
                "Sonrası çorap söküğü gibi geliyor ve tüm surelerdeki bu başlangıç harfleri ile ilgili sayımları kitap haline getirip yayınlıyor.",
                "Hatta kitanın ismini de 'Muhammedin Ebedi Mucizesi' şeklinde yayınlıyor. Sonrasında bu ismi Kuranın Ebedi Mucizesi olarak güncelliyor."
            },

            new p
            {
                "Bu buluştan sonra bir anda İslam dünyasında popüler oluyor. Öyleki Libya-İran gibi ülkelerden devlet başkanları seviyesinde özel davetler alıyor.",
                "Hatta Türkiyede bile yankı buluyor. Öte yandan Reşad başka kitaplar yazılar makaleler de yayınlıyor. ",
                "İşte Reşad'ın fikirleride bu yayınlar sayesinde duyulmaya başlayınca eskiden el üstünde tutulan bu insan artık ismi duyulunca geri adım atılan biri haline dönüşüyor.",
                "Peki nedir bu adamın savunduğu fikirler? Bu fikirlerden bazılarını çok kısa şekilde belirtmeye çalıştım."
            },

            new ul
            {
                new li
                {
                    (b)"Ey İslam dünyası! ",
                    "Sizler Muhammed peygamberi putlaştırıyorsunuz. Allah'ın yanında sürekli Muhammed peygamberimizi ekliyorsunuz. ",
                    "Tek Olan'ı tek olarak anamıyorsunuz sürekli yanına Muhammed kelimesini yerleştiriyorsunuz. ",
                    "Hatta Kuranda mescitlerde Allah'ın adı tek olarak anılsın diye açık emir varken siz Allah yazılarının yanına sürekli Muhammed yazıları ekliyorsunuz. ",
                    "Allah'dan diler gibi Muhammed peygamberden şefaat dileniyorsunuz. ",
                    "Tüm bunları yaparak Kurandaki şu ilkeleri çiğnemiş oluyorsunuz. ",

                    new FlexColumn(MarginLeft(30), MarginTopBottom(10), Gap(10))
                    {
                        new FlexRow
                        {
                            "- Mescitlerde Allah'ın adı tek olarak anılmalıdır."
                        },
                        new FlexRow
                        {
                            "- Peygamberler arasında ayrım yapılmamalıdır."
                        }
                    },

                    "Özetle şeytan aynen Hiristiyanlara yaptığı gibi sizi de tamda sevdiğiniz damarınızdan yani Muhammed peygambere olan sevginiz-saygınız üzerinden yakalamış durumda ve maalef farkında değilsiniz."
                }
            },
            new ul
            {
                new li
                {
                    (b)"Ey İslam dünyası! ",
                    "Kuranda zekat zamanı olarak hasat zamanı ifadesi geçer. Artık çoğumuz şehirlerde maaşla çalışan insanlarız. ",
                    "Eğer aylık maaş ile çalışıyorsanız sizin hasadınız maaşınızı aldığınız gündür ve zekatınızı da maaşınızı aldığınız zaman vermelisiniz.",
                    new br(),
                    "Not: Reşad bu zekat konusunda özellikle duruyor. Hem ihtiyaç sahiplerinin 1 yıl beklememesi ve zekatın her ay sonunda sürekli gündemimizde olması sayesinde 'En Yüce Olan' a daha çok yaklaşmamız için fırsat olduğu vesaire detaylı olarak açıklıyor."
                }
            },
            new ul
            {
                new li
                {
                    (b)"Tüm dünya insanları! ",
                    " Tevrat-İncil-Kuran bu kitapların Allah'dan geldiğine dair şüpheleriniz var. Musa denizi yardığında hiç birimiz orada değildik.",
                    " İsa ölüyü dirilttiğinde hiç birimiz yanında değildik.",
                    " Bu kitapların Allahdan mı olduğuna dair şüphelerinizi giderecek ", (b)" fiziksel ", "bir kanıt gelmiş durumdadır.",
                    " Bu kitaplardan sonuncusu olan Kuran'ın Allah'dan geldiğine dair fiziksel delil (19 mucizesi) bilgisayar yardımı ile ortaya çıkmıştır.",
                    " Kurandaki en önemli mesele olan Tevhid-Şirk meselesine kulak verin İsayı, Meryemi,Muhammedi putlaştırmaktan vazgeçin.",
                    " Tanrı'nın varlığı üzerine son kutsal kitapda (Kuran) yazılanların doğru olup olmadığı hakkında artık bir bahaneniz kalmamış durumdadır."
                }
            },

            new ul
            {
                new li
                {
                    (b)"Niçin buradayız?",
                    " (Bu kısmı kendi anladığım kadarı ile anlatmaya çalışacağım.)",
                    new br(),
                    new br(),
                    " Hayatın anlamı ne? Niye burdayız? gibi sorular insanlık tarihi kadar eski sorulardır. Felsefe de bile bu soruya net bir cevap verilememiştir.",
                    new br(),
                    new br(),
                    " - Ben mi istedim bu dünyaya gelmeyi?",
                    new br(),
                    new br(),
                    "- Neden Allah sürekli olarak bizlere namaz kılmamızı tembihliyor? Ödül olarak da cenneti vaad ediyor? Madem bu kadar zengin direk verse ya.",
                    new br(),
                    new br(),
                    "- Bizler Tanrının oyuncakları mıyız? Ne yani önünde eğilenleri cennete koyan eğilmeyenleri cehenneme atan bir Tanrı figürü pek de hoş değil gibi",
                    " Habire biz insanoğlunu yarıştırıyor.",
                    new br(),
                    new br(),
                    "- Madem Tanrı bu kadar güçlü neden bunca kötülüğe izin veriyor? ",
                    "Biz insanoğluna iyi-kötü arasında seçim yaptıracağına iyi-daha iyi arasında bir seçim yapsaydık da hepimiz cennete gisek daha iyi olmaz mı?",
                    " Daha çok puan alan cennette daha üst konuma gitseydi",
                    new br(),
                    new br(),
                    "- Kutsal yazılarda bahsedildiğine göre Koskoca Tanrı ne diye şeytan gibi  yarattığı birini kendine rakip miş gibi  görüp de biz insanoğluna ",
                    " ya şeytanı seçersiniz ya beni seçersiniz gibi bir seçim ile karşı karşıya bırakıyor? Tanrı neden şeytanı kendine muhatap kabul etsin ki?",
                    new br(),
                    new br(),
                    " - Ortalama 70 yıl yaşıyorum. 20 li yaşlara kadar zaten çocukluk -  eğitim vesaire ile geçiyor. ",
                    " 60-70 den sonrasını zaten sayma istesen de günaha giremiyorsun. Zihnen de geriliyorsun. Özetle bu kadar kısacık bir yaşam için sonsuz bir cehennem ne kadar adaletli?",
                    " Hani Tanrı en merhametliydi?",
                    new br(),
                    new br(),
                    "-Uzay neden bu kadar büyük? Ne malum başka yerlerde yaşamın olmadığı?",
                    new br(),
                    new br(),
                    "-Yaşamın anlamı nedir? Yapmam gereken en önemli şey nedir? Amacım ne olmalı?",
                    new br(),
                    new br(),
                    "Benim gibi Bağcılarda doğup büyüyen ortalama birinin aklına bunlar geliyor ise kim bilir daha başka zihinlerde ne sorular dolaşıyordur. :)",
                    new br(),
                    "Bu saydığım soruları daha derinleştirebiliriz. Klasik dini literatürde bu sorulara verilen cevap şudur.",
                    new br(),
                    "Bizler bu dünyaya imtahan için gönderildik.",
                    new br(),
                    "İyi de niye?",
                    "İşte tam da burada Reşad Halife bu sorunun cevabını yine Kurandan veriyor. ",
                    "Kurandan verdiği referanslar ile niçin burada olduğumuz ile ilgili, yaratılış ile ilgili çok güzel bir açıklama yapıyor.",
                    new br(),
                    "Bu açıklamayı kasti olarak burada vermiyorum. Çünkü verilen ayetleri tek tek inceleyip o kurguyu kendi zihninizde yapmanız gerekiyor.",
                    " Zaten  yaratılış kurgusunu zihninizde oturtmadığınız durumda Kurandaki bazı noktaları da daha iyi anlamış olacaksınız. " +
                    " Yukarıda verilen ve benim  yıllarımı yiyen bu sorular artık çıtır çerez gibi gelebilir.",
                    " Aşağıda Reşad Halife tarafından yapılan çevirinin linkini ekliyorum dileyen kitabın başında detaylı olarak anlatılan bu konuyu inceleyebilir.",
                    new br(),
                    new a { href = "http://teslimolan.org/pdf/kuran-son-ahit---birinci-baski.pdf", text = "Yetkilendirilmiş Çeviri" }

                }
            },

            new ul
            {
                new li
                {
                    (b)"Yalnız Kuran!",
                    new br(),
                    "Ey İslam dünyası!. Sizler Allah'ın yüce kelamı dururken Muhammed peygambere ait olup olmadığı belli olmayan rivayatlere sarılıyorsunuz.",
                    " Hatta bir sözün Muhammed peygamberimize ait olduğu kesin kez emin olsanız bile yine de o sözü dini bir hüküm kaynağı olarak göremezsiniz.",
                    "Dinin tek bir hüküm kaynağı vardır o da Kurandır.",
                    "Eğer bir şey Kuranda belirtilmiş ise o bizim için bağlayıcıdır eğer detayı verilmemiş ise demekki 'En Bilge Olan' o kadarını uygun görmüştür.",
                    " Çünkü Kuranda üzerine basa basa şu ifadeler vardır. ",
                    new br(),
                    "Kuran tamdır. Detaylıdır. Bu kitapdan hesaba çekileceksiniz.",
                    new br(),
                    "Eğer siz Kurandan başka bir öğreti takip ederseniz Kuranın tam ve eksiksiz olduğu gerçeğini gözardı etmiş olursunuz.",
                    new br(),
                    new br(),
                    "Not: Genel gözlemim burada karıştırılan bir durum vardır. Bu Yalnız Kuran fikrini savunan insanlara 'Hadis İnkarcısı' etiketi yapıştırılıyor.",
                    "Hadisleri o dönemin şartlarını gözlemleyebilmek adına tarihi birer veri olarak değerlendirebilirsiniz ama dinde hüküm koyucu yapamazsınız.",
                    "Bir örnek ile açıklayayım. Kuranda abdest 4 adımda anlatılmıştır.",
                    "İşte Reşad şunu söylüyor; Kuranda abdest 4 adımda diyor ise 4 adımdır. ",
                    "Fazladan ensemi yıkayayım, burnuma su çekeyim dersen, sen Kuranın öğretisini değil başka bir öğretiyi takip etmiş olursun.",
                    "Özetle Kuranda bir şeyin hükmü var ise onu takip et, eğer bir şeyin detayları verilmemiş ise demekki Yaratıcı bu kadarını uygun görmüştür.",
                    " Çünkü detayın peşine düşersen bu Kuran eksiksizdir, detaylıdır, tamdır cümlelerini dikkate almamış olursun demektir."
                }
            },

            new ul
            {
                new li
                {
                    (b)"Zamanınızın çoğunda sizin zihninizi kim veya ne meşgul ediyor ise sizin tanrınız odur.",
                    new br(),
                    "Kuranda bir çok ayette Allahı sık sık anın, Onu gece gündüz yüceltin. Ayaktayken anın, yan yatarken anın gibi ifadeler geçer.",
                    "Açıkcası oldum olası bu ısrarın sebebini anlamış değildim.",
                    new br(),
                    "Klasik cevap ise şöyleydi. Allahın ihtiyacı yok senin ihtiyacın var.",
                    new br(),
                    "İşte Reşad buradaki mekanizmayı şöyle açıklıyor; Bizler bu dünyaya ruhlarımızı kurtarmak-büyütmek için gönderildik. " +
                    " Baskın zihin durumumuz Allah ile ilişkili olur ise ve doğru(şirk içermeyen) namaz -oruç gibi ibadetler ile ruhlarımız beslenir ve büyür.",
                    " Böylelikle ahirette Allah bizlere tekrar secde etmemizi söyleyecek eğer ruhlarımızı Allah'ın yakınlığına -enerjisine alıştıramaz iseniz o gün geldiğinde" +
                    " o secdeyi yapamazsınız ve Allahın varlığına dayanamyıp geriye(cehenneme) doğru kaçmaya çalışacaksınız.",
                    " Baskın zihin durumunu bir örnek ile açıklayalım.",
                    new br(),
                    "Bir elmayı ısırdığınızda zihninizi o elmaya verin. ",
                    "'En Merhametli Olan', milyon kilometre öteye adına güneş dediğimiz bir gezegen koymuş.",
                    "Bu elma için milyon km öteden ısı göndermiş ışık göndermiş." +
                    " Toprağın altında milyarlarca bakteri toprakdaki besinleri ayrıştırmış" +
                    " Ağaç ise bu vitaminleri gözle görülmeyecek kadar kçük borular ile  milim milim, damla damla bu meyveye doldurmuş.",
                    " Elmanın suyu ağzınızda akarken verdiği o lezzeti farkedebilemiz için dilimizde bir sürü farklı hücre yerleştirmiş.",
                    " İşte bütün bu şartları sağlayan 'En Lutufkar Olan'a şükürler olsun.",

                    new br(),
                    "Özetle; bizler Allah'a zarar veremeyiz. Yarar da veremeyiz. ",
                    " Yaratıcı ile bağlantılı zihin durumu  ve ibadetler ile ancak kendimize fayda sağlamış oluruz. ",
                    "Elbette bu zihin durumuna geçmek zaman alacak bir süreç. Ama madalyanonun diğer yüzünde şu var.",
                    "Bizler Allaha inanınca cennete gideceğimizi sanıyoruz. Fakat kuran bunu garanti etmiyor.",
                    " Çünkü Kuranda kesin olarak belirtiliyor ki her ne iyilik yaparsan yap, her ne kadar ibadet edersen et eğer şirk içinde bir ömür yaşadı isen " +
                    " 'Azabı En Şiddetli Olan' şirki asla affetmeyeceğini belirtiyor.",
                    " Ayrıca iman edenlerin çoğunun da bunu puta tapma suçunu işlemeden yapmayacağını belirtiyor."

                }
            },


            new p
            {

            }
        };
    }
}