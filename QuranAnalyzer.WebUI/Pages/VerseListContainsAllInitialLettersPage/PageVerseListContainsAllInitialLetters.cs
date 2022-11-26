using QuranAnalyzer.WebUI.Components;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.VerseListContainsAllInitialLettersPage;

class PageVerseListContainsAllInitialLetters : ReactComponent
{
    public int? SelectedIndex { get; set; }

    protected override Element render()
    {
        static Element raisePanel(Element element) => new div
        {
            BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), Padding(15), BorderRadius(5), MarginTopBottom(10),
            element
        };
        
        return new Article
        {
            new VSpace(10),
            new LargeTitle("Bütün Başlangıç Harflerini içeren ayetler"),

            new FlexColumn(MarginTop(50))
            {
                "Toplamda 14 tane başlangıç harfi vardır. ",
                "Kur’an’ın 29 suresinin 30 ayetinde bu başlangıç harfleri farklı kombinasyonlar oluşturacak şekilde surelerin başlarında bulunmaktadır.",
                new br(),
                new br(),
                "Peki bu 14 başlangıç harfinin hepsini birden içeren ayetlerde bir ilginçlik olabilir mi?",
                new br(),
                new br(),
                "Aşağıdaki program yardımı ile bu bilgiyi inceleyelim.",
                "Not: Komut satırlarında değişiklik yaparak farklı aramalar yapabilirsiniz.",
                Space(10),

                raisePanel(new NumericValueCalculator
                {
                    Letters = string.Join(" ", Alif, Laam, Miim, Saad, Raa, Kaaf, Haa, Yaa, Ayn, Taa_, Siin, Haa_, Qaaf, Nun)
                }),
               

                Space(10),
                "Madde1",
                Space(10),

                raisePanel(new Calculator
                {
                    ShowVerseList = true,
                    SearchScript  = "*",
                    Letters       = string.Join(" ", Alif, Laam, Miim, Saad, Raa, Kaaf, Haa, Yaa, Ayn, Taa_, Siin, Haa_, Qaaf, Nun)
                }),
                
                raisePanel(new Calculator
                {
                    ShowNumbers  = true,
                    SearchScript = "*",
                    Letters      = string.Join(" ", Alif, Laam, Miim, Saad, Raa, Kaaf, Haa, Yaa, Ayn, Taa_, Siin, Haa_, Qaaf, Nun)
                }),

                "Son söz"
            },

            new VSpace(15)
        };
    }
}