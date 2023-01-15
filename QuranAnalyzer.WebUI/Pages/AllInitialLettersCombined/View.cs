using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.Libraries.react_awesome_reveal;

namespace QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined;

class View : ReactComponent
{
    protected override Element render()
    {
         Element CreateWithCount(string arabicLetter, int count)
         {
             Element countView = new FlexRowCentered { count.ToString() };
             
             if (EnterJoInMode)
             {
                 countView = new Fade
                 {
                     reverse=true,
                     direction = "down",
                     children =
                     {
                         countView
                     }
                 };
             }
             return new FlexColumn(ComponentBorder, Gap(4))
             {
                 new FlexRow { AsLetter(arabicLetter) } + When(EnterJoInMode,FontSize10),
                 countView
             };
         }

        Element CreateWithCount2(string arabicLetter, int count)
        {
            Element countView = new FlexRowCentered { count.ToString() };

            if (EnterJoInMode)
            {
                countView = new Fade
                {
                    direction = "down",
                    children =
                    {
                        countView
                    }
                };
            }
            return new FlexColumn
            {
                //new FlexRow { AsLetter(arabicLetter) },
                countView
            };
        }

        return new Article
        {
            new LargeTitle("Başlangıç harflerinin yan yana yazılması ile oluşan büyük sayılar"),

            new p
            {
                "Başlangıç harflerinin içinde bulunduğu sure ile ilişkili olduğunu 'Başlangıç Harfleri' sayfasında detaylı olarak öğrendik.",
                new br(),
                "Peki bu geçiş adetlerini yanyana yazsak acaba önümüze nasıl bir rakam çıkar?"
            },

            seperation,

            "Mesela yasin suresideki ya harfi o surede 580 defa geçer. Kaf harfi iki surede olmak üzere toplamda 114 defa geçer.",
            new br(),
            raisePanel(new TotalCounts()),
            new br(),
            
            
            
                


            new VSpace(10),
            "Böylece programın neyi hesapladığını kısa bir veri üzerinde görmüş olduk. ",

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

        static Element InFadeAnimation(Element element,int delay)
        {
            return new Fade
            {
                triggerOnce = true,
                delay = delay,
                children =
                {
                    element
                }
            };
        }

    }

    public bool EnterJoInMode { get; set; }
    
    void Join()
    {
        EnterJoInMode = true;

    }
}