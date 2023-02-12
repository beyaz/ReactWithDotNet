namespace QuranAnalyzer.WebUI.Pages.MainPage;

class MainPageContent : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Bu sitede ne var?"),

            new p
            {
                @"Bir kaç yıl önce Kuran hakkında 19 Sistemi - 19 Mucizesi benzeri isimlerle duyduğum bir konu üzerine vakit buldukça araştırma yapma fırsatım oldu.",
                new br(),

                @"Elimden geldiğince aklımın yettiği ölçüde nedir ne değildir inceledim.
Bu konu etrafında doğru yanlış bir çok şey duydum ve okudum.
Konuyu kendim bizzat incelemek ve konu etrafında dönen doğru yanlış şeylere kendimce verdiğim cevapları paylaşmak istedim.
Böylelikle konuyu araştıran kişiler için tarafsız bir gözlem sunmak niyetindeyim.
"
            },

            (p)"Bu siteyi hazırlarken aşağıdaki konulara dikkat ettim;",

            new ul
            {
                new li
                {
                    new p
                    {
                        "Özellikle Kuran'dan bir ayetin Türkçe mealini paylaşmadım. ",
                        "Çünkü insanlar genelde önce kendi fikirlerini peşinen doğru kabul ediyorlar ardından Kuran ayetlerine de o bakış açısı ile mana veriyorlar. ",
                        "Farkında olmadan Kuranı kendi fikirlerine uyduruyorlar."
                    },
                },

                new li
                {
                    new p
                    {
                        "Her ne kadar düşünen insan sayısı az olsa da düşünebilen her insanın bir beyni olduğuna inanıyorum. ",
                        "Bu sebeple doğru şudur veya şu yanlıştır gibi ifadelerden uzak durmaya çalıştım. ",
                        "Konu etrafında dönen zıt fikirleri elimden geldiğince tarafsız bir biçimde aktarıp karar verme yorum yapma işini ise elimden geldiğince okuyucuya bırakmaya çalıştım.",
                    }
                },

                new li
                {
                    new p
                    {
                        "Bu sitede bazı matematiksel veriler bulunmaktadır. ",
                        "Bu veriler gerçek zamanlı olarak çalışmaktadır. Giriş değerlerini değiştirdiğinizde sonuçlar da otomatik olarak değişecektir."
                    }
                }
            },

            (p)"Lütfen konunun anlaşılması için soldaki menüyü sırası ile takip ediniz."
        };
    }
}