using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Pages.MainPage;

class MainPageContent : ReactComponent
{
    public override Element render()
    {
        return new div
        {
            new LargeTitle("Bu sitede ne var?"){ style ={marginTopBottom = "10px"}},
            new div(@"
Bir kaç yıl önce Kuran hakkında 19 Sistemi - 19 Mucizesi benzeri isimlerle duyduğum bir konu üzerine vakit buldukça araştırma yapma fırsatım oldu.
Elimden geldiğince aklımın yettiği ölçüde nedir ne değildir inceledim.
Bu konu etrafında doğru yanlış bir çok şey duydum.
Konuyu kendi bizzat incelemek ve konu etrafında dönen doğru yanlış şeylere kendimce verdiğim cevapları paylaşmak istedim.
Böylelikle konuyu araştıran kişiler için tarafsız bir gözlem sunmak niyetindeyim.
Site şu 3 ana konuyu ele alır.
Lütfen konunun anlaşılması için soldaki menüyü sırası ile takip ediniz.
")
        };
    }
}