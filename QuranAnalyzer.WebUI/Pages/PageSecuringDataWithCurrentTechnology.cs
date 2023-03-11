namespace QuranAnalyzer.WebUI.Pages;

public class PageSecuringDataWithCurrentTechnology : ReactPureComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Günümüz teknolojisinde veri iletimi nasıl sağlanır ?"),

            (p)@"TC kimlik numaranızda bulunan rakamlar sizce rastgele rakamlar mı? Yoksa belli bir düzeni / mantığı mı var?",

            new p { "Mesela aşağıdaki TC Kimlik numaralarını inceleyelim." },
            new div
            {
                TextAlignCenter,
                new div {  "1056227229",(b)"6" },

                new div {  "2569375209",(b)"8" },

                new div {  "7547453160" ,(b)"2" }
            },

            (p)@"Dikkat ettiyseniz en sondaki rakamlar hep şu mantıkta oluşmuş. 
11 hane olan TC kimlik numaranızın ilk 10 hanesinin rakamlarının toplamının 10'a bölümünden kalan sayı 11. hanedeki sayı ile hep aynıdır.
Özetle şunu söyleyebiliriz",

            @"TC kimlik numaranızdaki ilk 10 rakamın toplamını 10'a bölerseniz elinizde kalan sayı 11.hanedeki rakamı verir.",

            new p
            {
                @"Bu kural bütün TC kimlik numaraları için geçerlidir. Hatta TC kimlik numarası aslında 9 hanedir. 
İlk 9 rakam belli bir hesaplama (algoritma) sonucu 10. hanedeki rakamı verir. 
İlk 10 hanedeki rakam da yukarıda detaylarını incelediğimiz yöntem ile 11. hanedeki rakamı verir.
Özetle en sağdaki son iki rakam aslında doğrulama rakamlarıdır.
Böylelikle herhangi 11 haneden oluşan bir sayının TC kimlik numarası olup olmadığı tespit edilebilir.

"
            },

            new LargeTitle("IBAN"),

            new p { "Bankanızın size vermiş oluğu IBAN bilgisinde buna benzer bir şey olabilir mi?" },
            new img { src = FileAtImgFolder("IBAN.jpg"), alt ="iban" ,style = { width = "100%", maxWidth = "600px", height = "auto", display = "block", marginLeftRight = "auto" } },

            new p
            {
                @"Yukarıdaki resimde gördüğünüz 'Kontrol Basamakları' diye işaret edilen 56 rakamı 
rastgele oluşturulmuş bir sayı değildir.
"
            },

            (p)@"Hatta mobil şubenizden bir ibana para gönderirken ibandaki herhangi bir rakamı bilerek yanlış girin.
Muhtemelen hatalı iban diye size uyarı verecektir.",

            (p)"Peki bu ibanın hatalı olup olmadığını nasıl bilinebiliyor ?",

            new div { text = "İbanın doğrulaması şu şekilde yapılıyor." },

            new ul
            {
                (li)"İlk 4 hane sona taşınır.",
                (li)"Her bir harf yerine o harf için belirlenen rakamsal karşılıklar yazılır.",
                (li)"Bütün bu rakamı 97 ye böldüğünüzde kalan 1 ise iban doğrudur - değil ise yanlıştır."
            },

            new div { text = "İşte bu yukarıdaki resimde gördüğünüz iki hane olan kontrol rakamları(check digits) bu Mod97 hesaplamasından 1 sonucu gelecek şekilde ayarlanıyor." },

            new LargeTitle("Parity Bit") + mt(15),

            new p { text = "Şuan bu yazıları okuduğunuz cihaz internetten aldığı verileri sizce nasıl kontrol ediyor?" },
            new div
            {
                @"Bilgisayarlar birbirlerine verileri gönderirken aslında sadece 0-1 rakamlarını gönderiyorlar. 
Gönderilecek verinin 0-1 rakamlarından oluşan karşılıkları paketler halinde gönderilir.
Her bir paketin içinde de binlerce byte ismini verdikleri 8 haneli rakamlar vardır. 
İşte bu paketlerin doğru olup olmaması da yine benzer bir yöntem ile yapılmaktadır."
            },

            new img { src = FileAtImgFolder("ParityBit.PNG"),alt = "Parity bit", style = { width = "100%", height = "auto", maxWidth = "400px", display = "block", marginLeftRight = "auto" } },

            new p
            {
                @"Dikkat edilir ise sağındaki rakamların toplamı çift ise en baştaki değer 0 olur tek ise 1 olur.
Bu sayede A cihazı B cihazından gelen verileri kontrol eder ve duruma göre bozuk gelen paketler tekrar istenir.
"
            },

            new p
            {
                @" Özetlersek günümüz bilgisayar dünyasında bir verinin doğruluğu, 
doğru iletilip iletilmediği bu saydığımız yöntemler ile olmaktadır. 
Çok daha karmaşık doğrulama-şifreleme yöntemleri olmakla beraber elbette bu yazıda basit olanlardan sadece bir kaçı ele alındı.
Para transferleri, bitcoin, QR ile yapılan işlemlerden tutun okuduğunuz şu yazının sizin cihazınıza kadar iletilmesinde bile bu yöntemler kullanılıyor.
",
                (strong)"Doğrulama bilgisi veri içinde beraber gönderiliyor."
            },

            new p(FontWeight600)
            {
                " İyi de bu anlatılanların Kuran ile 19 ile ne alakası var?"
            },

            new p
            {
                @"Aynen burada bahsedilen örneklerde olduğu gibi bir doğrulama kodu Kuran içinde olabilir mi ? 
Bu sayede Kuran'ın Yaratıcıdan gelen bir kitap olduğunu daha iyi anlayabilelim diye Kuran'ın içine bir doğrulama kodu-sayısı-anahtarı olabilir mi ?"
            }
        };
    }
}