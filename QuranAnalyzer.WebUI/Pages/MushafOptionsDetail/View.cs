using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.MushafOptionsDetail;

public class View : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {

            
        
        
            new VSpace(10),
            new LargeTitle("Bu Sitedeki Kullanılan Mushaf Hakkında"),
            new VSpace(15),

            @"
Kuran üzerinde 19 ile ilgili bilgiler duyulmaya başladıkça başka insanlar da 19 üzerine çalışma yapmaya başladılar.
Bu kişiler de günümüz bilgisayar yazılımlarını kullanarak bazı sayımlar yapıyorlar ve 19'un katı olan bazı sayılar elde ediyorlar.
Duruma göre bu kişilerin çalışmalarını da inceleyeilirsiniz.
İmran Akdemir, Gökmen Altay İki Sayının Sahibi gibi  isimler de çalışma yapıyorlar.
Bu isimlerin ortaya attıkları iddaları incelerken muhtemelen ilk duyacağınız cümle şu olacaktır.
'Uyduruk 19 sistemi', 'Sahte 19 sistemi' vb kelimler söyledikten sonra 19 ile ilgili kendi buldukları verilerin gerçek 19 sistemi olduğunu ileri sürüyorlar.
Bu kişilerin ortaya attıkları iddalara karşı cevaplar da var onları da dinlemenizi öneririm.
Mesela ikiz kod(7-19) sistemi diye bir sistem daha var olduğunu idda edildi.
Youtube ' da bu konu ile ilgili yapılan eleştiri videosunu incelemenizi tavsiye ederim.

https://www.youtube.com/watch?v=ZBweugCUuyk

Hatta sinema filmlerinin bir sahnesinde geçen bir arabaların plaka nolarını toplayıp çarparak Kurandan 19 ve 238 sayıları ile ilgili veriler bulan bile var.

Bu saydığım isimlerin ortak takıldıkları şey Reşad Halife'nin elçilik meselesi ve Tevbe 128-129 ile ilgili tartışılan konulardır.

Elbetteki madalyonun diğer yüzünde 19 olayını daha tarafsız inceleyen Gürkan Engin, Berker Yörgüç gibi isimler de var.

Bu saydığım isimler sadece Türkiye'de olan resim. Elbette başka ülkelerde de buna benzer ayrışmalar var.

Not: Bu sitede sadece Reşad Halife tarafından ortaya konan başlangıç harfleri üzerindeki verileri içermektedir.

",
            new VSpace(15)
        };
    }
}