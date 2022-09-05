using System;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;
using static QuranAnalyzer.WebUI.ResourceAccess;

namespace QuranAnalyzer.WebUI.Pages.SecuringDataWithCurrentTechnology;


public class View : ReactComponent
{
    protected override Element render()
    {


        return new Article
        {
            new LargeTitle("Günümüzde Veri Nasıl Korunur Doğruluğu nasıl teyit edilir"),
            new VSpace(15),
            new div(@"
TC kimlik numaranızda bulunan rakamlar sizce rastgele rakamlar mı? Yoksa belli bir düzeni / mantığı mı var ?
"),
            
            new p{text         = "Mesela aşağıdaki TC Kimlik noları inceleyelim."},
            new div{ innerHTML = "1056227229<b>6<b>"},
            
            new div{ innerHTML = "2569375209<b>8<b>"},
            
            new div{ innerHTML = "7547453160<b>2<b>"},
            
            new p{text = @"Dikkat ettiyseniz en sondaki rakamlar hep şu mantıkda oluşmuş. 
11 hane olan TC kimlik numaranızın ilk 10 hanesinin rakamlarının toplamının 10'a bölümünden kalan sayı 11. hanedeki sayı ile hep aynıdır.
Özetle şunu söyleyebiliriz"},

            new Important(@"TC kimlik numaranızdaki ilk 10 rakamın toplamını 10'a bölerseniz elinizde kalan sayı 11.hanedeki rakamı verir."),

            new p{text = @"Bu kural bütün TC kimlik numaraları için geçerlidir. Hatta TC kimlik numarası aslında 9 hanedir. 
İlk 9 rakam belli bir hesaplama / yöntem / algoritma sonucu 10. hanedeki rakamı verir. 
İlk 10 hanedeki rakam da yukarıda detaylarını incelediğimiz yöntem ile 11. hanedeki rakamı verir.
Sondan iki rakam aslında doğrulama rakamlarıdır.
Böylelikle bir 11 haneden oluşan bir sayının TC kimlik numarası olup olmadığı tespit edilebilir.

"},



            new LargeTitle("IBAN"),
            
            new p{text  = "Peki ya bankanızın size vermiş oluğu IBAN bilgisinde buna benzer bir şey olabilir mi ?"},
            new img{src = Img("IBAN.jpg"), width = 500, height = 330, style = { textAlign = "center", display = "block"}},
            new p{text = @"Yukarıdaki resimde gördüğünüz 'Kontrol Basamakları' diye işaret edilen 56 rakamı 
rastgele olşturulmuş bir sayı değildir.
Hatta mobil şubenizden bir ibana para gönderirken ibandaki herhangi bir rakamı bilerek yanlış girin.
Muhtemelen hatalı iban diye size uyarı verecektir. Peki bu ibanın hatalı olup olmadığını nasıl bilinebiliyor ?


"},
            new div{text = "İbanın doğrulaması şu şekilde yapılıyor."},
            new li{text  = "İlk 4 hane sona taşınır."},
            new li{text  = "Her bir harf yerine o harf için belirlenen rakamsal karşılıklar."},
            new li{text  = "Bütün bu rakamı 97 ye böldüğünüzde kalan 1 ise iban doğrudur - değil ise yanlıştır."},
new br(),            
            new div{text = "İşte bu yukarıdaki resimdeki kontrol basamakları(check digits) bu Mod97 hesaplamasından 1 sonucu gelecek şekilde ayarlanıyor."},

             new SubTitle("Parity Bit"),

             new p{text = "Şuan bu yazıları okuduğunuz cihaz internetten aldığı verileri sizce nasıl kontrol ediyor?"},
             new div{text = @"Bilgisayarlar birbirlerine verileri gönderirken aslında sadece 0-1 rakamlarını gönderiyorlar. 
Gönderilecek verinin 0-1 rakamlarından oluşan karşılıkları paketler halinde gönderilir.
her bir paketin içinde de binlerce byte ismini verdikleri 8 haneli rakamlar vardır. 
İşte bu paketlerin doğru olup olmaması da yine benzer bir yöntem ile yapılmaktadır."
                 
             },

             new img{src = Img("ParityBit.PNG"), width = 300, height = 150},

             new p{text = @"Dikkat edilir ise sağındaki rakamların toplamı çift ise en baştaki değer 0 olur tek ise 1 olur.
Bu sayede A cihazı B cihazından gelen verileri kontrol eder ve duruma göre bozuk gelen paketler tekrar istenir.
"},

             new p{text = @" Özetlersek günümüz bilgisayar dünyasında bir verinin doğruluğu, 
doğru iletilip iletilmediği bu saydığımız yöntemler ile olmaktadır. Elbette bu yazıda basit olanlardan sadece bir kaçı ele alındı.
Para transferleri, bitcoin, QR ile yapılan bazı işlemlerden tutun okuduğunuz şu yazının sizin cihazınıza kadar iletilmesinde bile bu yöntemler kullanılıyor.
"},

        };
        
        
    }
}