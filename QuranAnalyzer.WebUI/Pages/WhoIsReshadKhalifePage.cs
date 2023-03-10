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
Bu 19 meselesini incelerken fark ettim ki meselenin odağındaki isim bu sistemi ilk bulan kişi Reşad Halife ismi.
 Daha doğrusu onun fikirleri. Zaten fikirleri yüzünden de öldürüldü.
 Kısaca bu kişi kimdir? Derdi neymiş?
 Özetle ne söylemiş? İşte bu bilgileri kısa kısa olacak şekilde özetlemeye çalıştım. 
 Tekrarlamakta fayda görüyorum. Bu fikirler doğrudur yanlıştır gibi bir iddiam yok. 
 Daha doğrusu benim doğru veya yanlış demem herhangi bir şey ifade etmez. Ben sadece bu adamın ne demeye çalıştığını özetlemeye çalıştım. 
 Umarım faydalı olur."
            },

            (h5)"İlk Yıllar ve Kariyer",
            new p
            {
                new img {Src(FileAtImgFolder("rh.png")), Alt("Reşad Halife"), Width(200), HeightAuto, FloatLeft, MarginRight(20) },

                "1935 yılında Mısırda doğmuş. Babası tarikat şeyhi olan biridir. " +
                "Bunu özellikle belirttim. Ehli sünnet bir tarikat ortamında, dindar bir ailede doğup büyümüş birinden bahsediyoruz. " +
                "Mısırda üniversite kimya bölümünde okuyor. Onur derecesi alıyor. 25'li yaşlarda Amerika'ya doktora için gidiyor ve biyokimya alanında yüksek lisans " +
                "ve doktorasını tamamlıyor. Kendi alanında konferanslar falan veriyor. Birleşmiş milletler kalkınma örgütünde kimyager olarak çalışıyor. " ,
                "Kısaca kariyer olarak oldukça iyi giden bir hayatı olmuş diyebiliriz."
            },
            new p
            {
                "30'lu yaşlarda Amerika'ya gelince bir yandan doktorasını tamamlarken bir yandan da İngilizce yazılmış kuran meallerinin çoğunu okuyor. " ,
                "Eski tefsirleri zaten Mısırdayken inceleyen biri. Anadili zaten Arapça. " ,
                "Yazılan İngilizce Kuran çevirilerini inceledikten sonra fark ediyor ki bu çevirilerdeki eksik gördüğü veya tevhide uymayan fikirler olduğunu görüyor. " ,
                "En azından kendi evlatlarımın okuyabileceği Tevhidi merkez alan bir çeviri yapma niyeti ile çeviriye başladığını kendisi söylüyor. " ,
                "Bir ayetin manasını tam anlamadan ötekine  geçmeyeceğine dair kendine de şart koşuyor. " ,
                "1. sureyi çevirdikten sonra 2. sureye geliyor ve sure başındaki elif-lam-mim harflerine gelince takılıyor. ",
                "Çalıştığı şirket çalışanlarını yazılım kursuna gönderiyor kendisi de bu sayede yazılım öğrenmiş. ",
                "Acaba Kuranda bu başlangıç harfleri ile ilgili verileri bilgisayar yazılımı ile incelemek istiyor. ",
                "Ardından farklı kuran mushaflarını inceleyerek Arapçadaki her bir harfe karşılık İngilizce bir harf ile Kuranı bilgisayara aktarıyor.",
                " Açıkçası oldukça zahmetli bir iş olsa gerek :) "
            },

            new p
            {
                "İlk fark ettiği şeylerden biri Kaf harfi ile başlayan surelerde Kaf harfinin biraz daha yoğun kullanıldığını görüyor. ",
                "Bu veriyi 1973 tarihinde makale olarak yayınlıyor. ",
                "Daha sonra 1974 de ise fark ediyor ki bu başlangıç harflerinin olduğu surelerin başındaki harfler bulundukları surelerde hep 19 un katları şeklinde geçiyor. ",
                "Sonrası çorap söküğü gibi geliyor ve tüm surelerdeki bu başlangıç harfleri ile ilgili sayımları kitap haline getirip yayınlıyor. ",
                "Hatta kitabının ismini de 'Muhammedin Ebedi Mucizesi' şeklinde yayınlıyor. Sonrasında bu ismi Kuranın Ebedi Mucizesi olarak güncelliyor. "
            },

            new p
            {
                "Bu buluştan sonra bir anda İslam dünyasında popüler oluyor. Öyle ki Libya-İran gibi ülkelerden devlet başkanları seviyesinde özel davetler alıyor. ",
                "Hatta Türkiye'de bile yankı buluyor. Öte yandan Reşad başka kitaplar yazılar makaleler de yayınlıyor. ",
                "İşte Reşad'ın fikirleri de bu yayınlar sayesinde duyulmaya başlayınca eskiden el üstünde tutulan bu insan artık ismi duyulunca geri adım atılan biri haline dönüşüyor. ",
                "Peki nedir bu adamın savunduğu fikirler? Bu fikirlerden bazılarını çok kısa şekilde belirtmeye çalıştım. Yazıda ben kendi anladıklarımı ifade etmeye çalıştım bu sebeple üsluba pek takılmayın. "
            },

            new ul
            {
                new li
                {
                    (b)"Ey İslam dünyası! ",
                    "Sizler Muhammed peygamberi putlaştırıyorsunuz. Allah'ın yanında sürekli Muhammed peygamberimizi ekliyorsunuz. ",
                    "Tek Olan'ı tek olarak anamıyorsunuz sürekli yanına Muhammed kelimesini yerleştiriyorsunuz. ",
                    "Hatta Kuranda mescitlerde Allah'ın adı tek olarak anılsın diye açık emir varken siz Allah yazılarının yanına sürekli Muhammed yazıları ekliyorsunuz. ",
                    "Allah'tan diler gibi Muhammed peygamberden şefaat dileniyorsunuz. ",
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

                    "Özetle şeytan aynen Hristiyanlara yaptığı gibi sizi de tamda sevdiğiniz damarınızdan yani Muhammed peygambere olan sevginiz-saygınız üzerinden yakalamış durumda ve maalesef farkında değilsiniz. ",
                    "Hatta bu putlaştırma olayı sadece peygamber ile de sınırlı değil. Tarikat şeyhlerini cemaat liderlerini putlaştırmaya kadar gidiyor."
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
                    " Tevrat-İncil-Kuran bu kitapların Allah'tan geldiğine dair şüpheleriniz var. Musa denizi yardığında hiç birimiz orada değildik.",
                    " İsa ölüyü dirilttiğinde hiç birimiz yanında değildik.",
                    " Bu kitapların Allah'tan mı olduğuna dair şüphelerinizi giderecek ", (b)" fiziksel ", "bir kanıt gelmiş durumdadır.",
                    " Bu kitaplardan sonuncusu olan Kuran'ın Allah'tan geldiğine dair fiziksel delil (19 mucizesi) bilgisayar yardımı ile ortaya çıkmıştır.",
                    " Kurandaki en önemli mesele olan Tevhid-Şirk meselesine kulak verin İsa'yı, Meryem'i, Muhammed'i putlaştırmaktan vazgeçin.",
                    " Tanrı'nın varlığı üzerine son kutsal kitapta (Kuran) yazılanların doğru olup olmadığı hakkında artık bir bahaneniz kalmamış durumdadır."
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
                    " Hayatın anlamı ne? Niye buradayız? gibi sorular insanlık tarihi kadar eski sorulardır. Felsefe de bile bu soruya net bir cevap verilememiştir.",
                    new br(),
                    new br(),
                    " - Ben mi istedim bu dünyaya gelmeyi?",
                    new br(),
                    new br(),
                    "- Neden Allah sürekli olarak bizlere namaz kılmamızı tembihliyor? Ödül olarak da cenneti vaat ediyor? Madem bu kadar zengin direk verse ya.",
                    new br(),
                    new br(),
                    "- Bizler Tanrının oyuncakları mıyız? Ne yani önünde eğilenleri cennete koyan eğilmeyenleri cehenneme atan bir Tanrı figürü pek de hoş değil gibi",
                    " Sürekli biz insanoğlunu iyilik için yarıştırıyor.",
                    new br(),
                    new br(),
                    "- Türkiye'de doğan Mehmet Müslüman olduğu için eninde sonunda cennete gidecek fakat Almanya'da doğup büyüyen Hans, Müslüman olmadığı için cehenneme gidecek? " ,
                    "Hans Ortadoğu’da bir yerde doğsaydı o da muhtemelen Müslüman olacaktı. ",
                    "Burada bir adaletsizlik yok mu?",
                    new br(),
                    new br(),
                    "- Madem Tanrı bu kadar güçlü neden bunca kötülüğe izin veriyor? ",
                    "Biz insanoğluna iyi-kötü arasında seçim yaptıracağına iyi-daha iyi arasında bir seçim yapsaydık da hepimiz cennete gitsek daha iyi olmaz mı?",
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
                    "Bizler bu dünyaya imtihan için gönderildik.",
                    new br(),
                    "İyi de niye? Niye durup dururken imtihan oluyoruz?", new br(),
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
                    "Ey İslam dünyası!. Sizler Allah'ın yüce kelamı dururken Muhammed peygambere ait olup olmadığı belli olmayan rivayetlere sarılıyorsunuz. ",
                    "Dinin tek bir hüküm kaynağı vardır o da Kurandır. ",
                    "Eğer bir şey Kuranda belirtilmiş ise o bizim için bağlayıcıdır eğer detayı verilmemiş ise demek ki 'En Bilge Olan' o kadarını uygun görmüştür. ",
                    " Çünkü Kuranda üzerine basa basa şu ifadeler vardır. ",
                    new br(),
                    "Kuran tamdır. Detaylıdır. Bu kitaptan hesaba çekileceksiniz. ",
                    new br(),
                    "Eğer siz Kurandan başka bir öğreti takip ederseniz Kuranın tam ve eksiksiz olduğu gerçeğini göz ardı etmiş olursunuz. ",
                    new br(),
                    new br(),
                    "Not: Genel gözlemim burada karıştırılan bir durum vardır. Bu Yalnız Kuran fikrini savunan insanlara 'Hadis İnkarcısı' etiketi yapıştırılıyor. ",
                    "Hadisleri o dönemin şartlarını gözlemleyebilmek adına tarihi birer veri olarak değerlendirebilirsiniz ama dinde hüküm koyucu yapamazsınız. ",
                    "Bir örnek ile açıklayayım. Kuranda abdest 4 adımda anlatılmıştır. ",
                    "İşte Reşad şunu söylüyor; Kuranda abdest 4 adımda diyor ise 4 adımdır. ",
                    "Fazladan ensemi yıkayayım, burnuma su çekeyim dersen, sen Kuranın öğretisini değil başka bir öğretiyi takip etmiş olursun. ",
                    "Özetle Kuranda bir şeyin hükmü var ise onu takip et, eğer bir şeyin detayları verilmemiş ise demek ki Yaratıcı bu kadarını uygun görmüştür. ",
                    " Çünkü detayın peşine düşersen bu Kuran eksiksizdir, detaylıdır, tamdır cümlelerini boşa çıkarmış, dikkate almamış olursun demektir. "
                }
            },

            new ul
            {
                new li
                {
                    (b)"Zamanınızın çoğunda sizin zihninizi kim veya ne meşgul ediyor ise sizin tanrınız odur.",
                    new br(),
                    "Kuranda bir çok ayette Allah'ı sık sık anın, Onu gece gündüz yüceltin. Ayaktayken anın, yan yatarken anın gibi ifadeler geçer. ",
                    "Açıkçası oldum olası bu ısrarın sebebini anlamış değildim.",
                    new br(),
                    "Klasik cevap ise şöyleydi. Allah'ın ihtiyacı yok senin ihtiyacın var.",
                    new br(),
                    "İşte Reşad buradaki mekanizmayı şöyle açıklıyor; Bizler bu dünyaya ruhlarımızı kurtarmak-büyütmek için gönderildik. " ,
                    " Baskın zihin durumumuz Allah ile ilişkili olur ise ve doğru(şirk içermeyen) namaz -oruç gibi ibadetler ile ruhlarımız beslenir ve büyür.",
                    " Böylelikle ahirette Allah bizlere tekrar secde etmemizi söyleyecek eğer ruhlarımızı Allah'ın yakınlığına -enerjisine alıştıramaz iseniz o gün geldiğinde" ,
                    " o secdeyi yapamazsınız ve Allah'ın varlığına dayanamayıp geriye(cehenneme) doğru kaçmaya çalışacaksınız.",
                    " Baskın zihin durumunu bir örnek ile açıklayalım.",
                    new br(),
                    "Bir elmayı ısırdığınızda zihninizi o elmaya verin.",
                    " 'En Merhametli Olan', milyon kilometre öteye adına güneş dediğimiz bir gezegen koymuş.",
                    " Bu elma için milyon km öteden ısı göndermiş ışık göndermiş." ,
                    " Toprağın altında milyarlarca bakteri topraktaki besinleri ayrıştırmış" ,
                    " Ağaç ise bu vitaminleri gözle görülmeyecek kadar küçük borular ile  milim milim, damla damla bu meyveye doldurmuş.",
                    " Elmanın suyu ağzınızda akarken verdiği o lezzeti fark edebilmemiz için dilimizde bir sürü farklı hücre yerleştirmiş.",
                    " İşte bütün bu şartları sağlayan 'En Lutufkar Olan'a şükürler olsun.",

                    new br(),
                    "Özetle; bizler Allah'a zarar veremeyiz. Yarar da veremeyiz.",
                    " Yaratıcı ile bağlantılı zihin durumu  ve ibadetler ile ancak kendimize fayda sağlamış oluruz.",
                    " Elbette bu zihin durumuna geçmek zaman alacak bir süreç. Ama madalyonun diğer yüzünde şu var.",
                    " Bizler Allaha inanınca cennete gideceğimizi sanıyoruz. Fakat kuran bunu garanti etmiyor.",
                    " Çünkü Kuranda kesin olarak belirtiliyor ki her ne iyilik yaparsan yap, her ne kadar ibadet edersen et eğer şirk içinde bir ömür yaşadı isen " ,
                    " 'Azabı En Şiddetli Olan' şirki asla affetmeyeceğini belirtiyor.",
                    " Ayrıca iman edenlerin çoğunun da bunu puta tapma suçunu işlemeden yapmayacağını belirtiyor."

                }
            },


            new p
            {
                (b)"Sonuç olarak;",
                new br(),
                "Bu yukarıdaki belirtilen fikirler size doğru veya yanlış gelebilir." ,
                " Reşad Halife daha başka şeyler de söylüyor ama burada hepsine yer vermem mümkün değil.",
                " Reşad Halife ısrarla şunu da söylüyor 'Eğer benden Kuranda referansı olmayan bir söz duyarsanız onu çöpe atabilirsiniz'.",
                " Reşad 80'li yıllarda sanki bu zamandaki youtube-sosyal medya olaylarını tahmin etmiş gibi:)" ,
                " bu fikirlerin bir kısmını cuma hutbelerinde bir kısmını kamera karşısına geçerek tek tek anlatmış.",
                " Doğru namazın nasıl kılınacağı için video çekmiş.",
                " Bilim adamı kimliği ile evrim hakkında video çekmiş. (Biyokimya doktorası olduğunu hatırlayın.)",
                " Toplamda 10-15 tane videosu var. Ayrıca kendi yapmış olduğu Kuran çevirisi var.",
                " Bu yazıdan ancak bir kısmını bahsettiğim fikirleri onun ağzından dinleyebilirsiniz.",
                " Açıkçası bu fikirler kadar olmasa da beni etkileyen şu kısma da değinerek yazıyı tamamlamış olayım.",
                " Reşad Halife de gördüğüm durumlar şunlar.",
                
                " Bu adamın çok baskın bir tevhid anlayışı var. Allah'ı çok farklı ve güzel ve Kurandan referanslar ile anlatıyor." ,
                " Bende sıradan bir yurdum insanıyım. Allah hakkında bir sürü hikaye dinledim. Kuran okudukça fark ediyorum ki bu hikayelerin çoğu kurandan değilmiş." ,
                " Yani zihninizdeki Allah ile gerçekteki Allah farklı olabilir. Dolayısı ile Allah'ı Kuran üzerinden tanımak mecburiyetindeyiz. " ,
                " Bu konuda Reşad oldukça iyi bir eğitmen. Tevhid mesajını en yüksek tonda seslendirmeye çalışan biri." ,
                
                " Dinden kazanç sağlamıyor olması benim için önemli. Kazanç sadece para ile olmaz. Bazen şöhret ile de olur. " ,
                " 19 sistemini ilk bulduğunda popülaritesi varken istese bu fikirlerini pek duyurmayıp o popülaritenin kaymağını yiyebilirdir.",
                " Fakat o tam tersini yapıyor. Emeklilik parası ile gidip mescit kuruyor. " ,
                " Hadisleri dinin kaynağı olarak görmediğini ve dinin tek kaynağının kuran olduğunu üzerine basa basa tekrarlıyor.",
                " Bu uğurda şöhreti elden gidiyor. Ölüm tehditleri aldığı halde geri adım atmıyor. Hatta bir videoda kamera yanlışlıkla kayıyor arkasında toplamda 10 kişilik bir saf bile yok.",
                " Zaten bu fikirleri yüzünden de 55 yaşında sabah namazında öldürülüyor.",
                
                new br(),
                new br(),
                "Elimden geldiğince bu adamın ne söylediğini anlatmaya çalıştım. Belki benim bakışımda hata vardır, Belki yanlış anlamışımdır. " +
                " Fırsatınız var ise bu bilgileri lütfen kendiniz teyit ediniz."
                
            }
        };
    }
}