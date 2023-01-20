using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined;

class View : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Başlangıç harflerinin yan yana yazılması ile oluşan büyük sayılar"),

            new p
            {
                "Başlangıç harflerinin içinde bulunduğu sure ile ilişkili olduğunu 'Başlangıç Harfleri' sayfasında detaylı olarak incelenmişti.",
                new br(),
                "Peki bu geçiş adetlerini yanyana yazsak acaba önümüze nasıl bir rakam çıkar?"
            },

            seperation,

            "Mesela yasin suresideki ya harfi o surede 580 defa geçer. Kaf harfi iki surede olmak üzere toplamda 114 defa geçer.",
            new br(),
            raisePanel(new TotalCounts()),
            new br(),
            new br(),

            raisePanel(new TotalCountsWithDetail()),

            new VSpace(10),

            new p
            {
                "İlginç... "
            }
        };

        static Element seperation() => new FlexRowCentered(MarginTopBottom(10)) { "* * *" };

        static Element raisePanel(Element element) => new div
        {
            BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), Padding(15), BorderRadius(5), MarginTopBottom(10),
            element
        };
    }
}