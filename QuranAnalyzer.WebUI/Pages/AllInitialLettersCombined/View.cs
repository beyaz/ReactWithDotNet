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
            new FlexRow(Gap(EnterJoInMode?5:5), FlexWrap)
            {
                CreateWithCount(ArabicLetter.Alif,17152),
                CreateWithCount(ArabicLetter.Laam,11797),
                CreateWithCount(ArabicLetter.Miim,8659),
                CreateWithCount(ArabicLetter.Saad,152),
                CreateWithCount(ArabicLetter.Raa,1232),
                CreateWithCount(ArabicLetter.Kaaf,137),
                CreateWithCount(ArabicLetter.Haa,426),
                CreateWithCount(ArabicLetter.Yaa,580),
                CreateWithCount(ArabicLetter.Ayn,215),
                CreateWithCount(ArabicLetter.Taa,107),
                CreateWithCount(ArabicLetter.Siin,397),
                CreateWithCount(ArabicLetter.Haa_,292),
                CreateWithCount(ArabicLetter.Qaaf,114),
                CreateWithCount(ArabicLetter.Nun,133),
                CreateWithCount("Toplam",41388)
            },
            new br(),
            
            new ActionButton{Label = "Yan yana yaz",OnClick = Join},

            When(EnterJoInMode,()=>
            raisePanel(new FlexRow
            {
                InFadeAnimation(CreateWithCount2(ArabicLetter.Alif,17152),300),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Laam,11797),300),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Miim,8659),300),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Saad,152),400),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Raa,1232),500),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Kaaf,137),600),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Haa,426),800),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Yaa,580),900),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Ayn,215),1000),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Taa,107),1400),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Siin,392),1500),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Haa_,292),1600),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Qaaf,114),1700),
                InFadeAnimation(CreateWithCount2(ArabicLetter.Nun,133),1800),
                InFadeAnimation(CreateWithCount2("Toplam",41388),2000)
            })),

           new FlexRowCentered
           {
               new img
               {
                   src    = "wwwroot/img/arrow-down-double.svg",
                   width  = 40,
                   height = 40
               }
           }
           ,
            
                When(EnterJoInMode,()=>
                         raisePanel(new FlexRow
                         {
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Alif,17152),300),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Laam,11797),300),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Miim,8659),300),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Saad,152),400),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Raa,1232),500),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Kaaf,137),600),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Haa,426),800),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Yaa,580),900),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Ayn,215),1000),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Taa,107),1400),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Siin,392),1500),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Haa_,292),1600),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Qaaf,114),1700),
                             InFadeAnimation(CreateWithCount2(ArabicLetter.Nun,133),1800),
                             InFadeAnimation(CreateWithCount2("Toplam",41388),3000)
                         })),


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