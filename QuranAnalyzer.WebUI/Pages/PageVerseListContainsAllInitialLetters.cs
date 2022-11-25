using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.Libraries.react_free_scrollbar;

namespace QuranAnalyzer.WebUI.Pages;

class PageVerseListContainsAllInitialLettersModel
{
    public string VerseFilterScript { get; set; }

    public string Letters { get; set; }
}
class PageVerseListContainsAllInitialLetters : ReactComponent<PageVerseListContainsAllInitialLettersModel>
{
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
                    new StickyLeftMenu{Labels = new []{"1","2","3"}, SelectedIndex = 0},
                    new FlexColumn
                    {
                        new FlexColumn(ComponentBorder, Gap(5))
                        {
                            new FlexRow(AlignItemsCenter)
                            {
                                (strong)"Ayetleri Seç:", 
                                
                                new TextInput
                                {
                                    TextInput.Bind(()=>state.VerseFilterScript),
                                    Width(100), mr(5), ml(5)
                                },
                                (small)" (* demek tüm Kuran boyunca arama yapılacağı anlamına gelir)"
                            },

                            new FlexRow
                            {
                                "Aranacak Harfler:",
                                new TextInput
                                {
                                    TextInput.Bind(()=>state.Letters),
                                    Width(250), mr(5), ml(5)
                                },
                            },

                            new FlexRow(JustifyContentFlexEnd, mr(20), mb(20))
                            {
                                new ActionButton{Label = "Ara"}
                            }
                        },
                        Space(10),
                        new FlexColumn(ComponentBorder)
                        {
                            new h4("Sonuçlar"),

                            new h6("Bulunan ayet sayısı: 114"),
                            new FreeScrollBar
                            {
                                Height(250) , ComponentBorder , Margin(10) ,

                                Children(Enumerable.Range(1,114).Select(i=>new div
                                {
                                    i.ToString(),
                                    new br()
                                }))
                            },


                        },
                    }
                }
                
                    
            },

            new VSpace(15)
        };
    }
}