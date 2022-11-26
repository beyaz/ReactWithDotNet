using QuranAnalyzer.WebUI.Components;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.VerseListContainsAllInitialLettersPage;

class PageVerseListContainsAllInitialLettersModel
{
    public string VerseFilterScript { get; set; }

    public string Letters { get; set; }

    public int? SelectedIndex { get; set; }
}
class PageVerseListContainsAllInitialLetters : ReactComponent<PageVerseListContainsAllInitialLettersModel>
{
    protected override void constructor()
    {
        state = new PageVerseListContainsAllInitialLettersModel
        {
            SelectedIndex = 0,
            VerseFilterScript = ""
        };
    }

    Element BuildNumericValueCalculator()
    {
        return new NumericValueCalculator
        {
            Letters = string.Join(" ", Alif, Laam, Miim, Saad, Raa, Kaaf, Haa, Yaa, Ayn, Taa_, Siin, Haa_, Qaaf, Nun)
        };
    }

    protected override Element render()
    {
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
                
                new FlexRow(Gap(10))
                {
                    new StickyLeftMenu{Labels = new []{"1","2","3"}, SelectedIndex = state.SelectedIndex, Click = i=>state.SelectedIndex =i},
                    When(state.SelectedIndex == 0, () => new FlexColumn(Padding(10),FlexGrow(1))
                    {
                        new VerseListThatContainsLettersCalculator
                        {
                            SearchScript = "*",
                            Letters = string.Join(" ", Alif, Laam, Miim, Saad, Raa, Kaaf, Haa, Yaa, Ayn, Taa_, Siin, Haa_, Qaaf, Nun)
                        }
                    }),
                    When(state.SelectedIndex == 1, () => new FlexColumn(Padding(10),FlexGrow(1))
                    {
                        BuildNumericValueCalculator()
                    })

                }
                
                    
            },

            new VSpace(15)
        };
    }
}