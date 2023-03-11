namespace QuranAnalyzer.WebUI.Pages;

public class PageIsThereAnyCommunity : ReactPureComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("19'cular diye bir cemaat / tarikat / topluluk mu var?"),

            new p
            {
                " Önce sorunun cevabını net bir şekilde verelim.",
                br,
                " Birilerinin dini lider konumunda olduğu, ardından liderlerin putlaştırıldığı gruplaşma ve benzeri yapılanmalar Reşad'ın dedikleriyle taban tabana zıt şeyler."
            },
            new p
            {
                "İşin ilginç yanı Reşad bunu kendi açtığı mescitte aynen uyguluyor. O mescitte sırayla namaz kıldırıyorlar, sırayla hutbe veriyorlar. Kuran derslerini sırayla yapıyorlar.",
                " Bizim pek de alışık olmadığımız bir yöntem.",
                " Hatta akşamları yapılan Kuran derslerine bayanlar dahi katılabiliyor."
            }
        };
    }
}