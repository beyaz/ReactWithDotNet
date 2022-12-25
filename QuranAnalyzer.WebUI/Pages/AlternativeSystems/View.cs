using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.AlternativeSystems;

public class AlternativeSystemsView : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Alternatif 19 Sistemleri"),
            new VSpace(15),

            new p{ @"
Kuran üzerinde 19 ile ilgili bilgiler duyulmaya başladıkça başka insanlar da 19 üzerine çalışma yapmaya başladılar.
Bu kişiler de günümüz bilgisayar yazılımlarını kullanarak bazı sayımlar yapıyorlar ve 19'un katı olan bazı sayılar elde ediyorlar.
Duruma göre bu kişilerin çalışmalarını da inceleyeilirsiniz.
İmran Akdemir, Gökmen Altay İki Sayının Sahibi gibi  isimler de çalışma yapıyorlar.
Bu isimlerin ortaya attıkları iddaları incelerken muhtemelen ilk duyacağınız cümle şu olacaktır.
'Uyduruk 19 sistemi', 'Sahte 19 sistemi' vb kelimler söyledikten sonra 19 ile ilgili kendi buldukları verilerin gerçek 19 sistemi olduğunu ileri sürüyorlar.
Bu kişilerin ortaya attıkları iddalara karşı cevaplar da var onları da dinlemenizi öneririm.
Mesela ikiz kod(7-19) sistemi diye bir sistem daha var olduğunu idda edildi.
Youtube ' da bu konu ile ilgili yapılan eleştiri videosunu incelemenizi tavsiye ederim." },

new a{ href = "https://www.youtube.com/watch?v=ZBweugCUuyk", text = "Gürkan Engin İkiz Kod 7 & 19"},

  new p{ @"Hatta sinema filmlerinin bir sahnesinde geçen bir arabaların plaka nolarını toplayıp çarparak Kurandan 19 ve 238 sayıları ile ilgili veriler bulan bile var.
Şaka değil konuyu araştırırken gerçekten böyle videolara denk geldim."},

  new p{@"Bu saydığım isimlerin ortak takıldıkları şey Reşad Halife'nin elçilik meselesi ve Tevbe 128-129 ile ilgili tartışılan iki konudur." },
  
  new p{ @"Elbetteki madalyonun diğer yüzünde 19 olayını daha tarafsız inceleyen Gürkan Engin, KuranMucizeler.com gibi isimler de var.

Bu saydığım isimler sadece Türkiye'de olan resim. Elbette başka ülkelerde de buna benzer ayrışmalar var.
" },
  
  new p
  {
      @"Not: Bu sitede sadece Reşad Halife tarafından keşfedilen başlangıç harfleri üzerindeki verileri incelemektedir. 
      Dilerseniz bu sitede kullanılan sayım programlarını kullanabilir ve alternatif 19 iddalarını inceleyebilirsiniz."
  }
        };
    }
}