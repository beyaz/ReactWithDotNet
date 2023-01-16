using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet;
using ReactWithDotNet.Libraries.react_awesome_reveal;
using System.Drawing;
using System.Numerics;
using ReactWithDotNet.react_xarrows;

namespace QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined;

class TotalCounts : ReactComponent
{

    class Arrow : ReactComponent
    {
        public string start;
        public string end;
        public string color;


        public bool StartAnchorFromTop { get; set; }
        public bool StartAnchorFromRight { get; set; }


        public bool dashness { get; set; }

        public double? strokeWidth { get; set; } = 1;

        protected override Element render()
        {
            color ??= "#a9acaa";

            return new Xarrow
            {
                start       = start,
                end         = end,
                path        = "smooth",
                color       = color,
                strokeWidth = strokeWidth,
                startAnchor = "bottom",
                dashness    = true,
                endAnchor = "top"
            };
        }
    }
    
    
    public bool EnterJoInMode { get; set; }
    
    static Element InFadeAnimation(Element element, int delay)
    {
        return new Fade
        {
            triggerOnce = true,
            delay       = delay,
            children =
            {
                element
            }
        };
    }
    void Calculate()
    {
        EnterJoInMode = true;
    }

    Element CreateWithCount(int index)
    {
        
        Element countView = new FlexRowCentered
        {
            new input
            {
                type = "text", valueBind = ()=>Records[index].Count,
                valueBindDebounceTimeout = 200,
                valueBindDebounceHandler = ValueBindDebounceHandler,
                style =
                {
                    Border("none"),
                    Width(40),
                    TextAlignCenter,
                    Focus(Border($"0.1px solid {BorderColor}"))
                }
                
            }
        };
        
        return new FlexColumn(ComponentBorder, BorderRadius(5), Padding(3), Gap(4), Id("begin-"+ Records[index].Text))
        {
            new FlexRow(JustifyContentCenter) { AsLetter(Records[index].Text) },
            countView
        };
    }

    void ValueBindDebounceHandler()
    {
        Records.SkipLast(1).Sum(x => ParseInt(x.Count)).Then(total=> Records[^1].Count = total.ToString());
    }

    Element CreateWithCount2(string arabicLetter, string count)
    {
        Element countView = new FlexRowCentered(ComponentBorder,BorderRadius(3),Id("end-"+arabicLetter)) { count.ToString() };

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
        return countView;
    }

   

    public IReadOnlyList<Pair> Records { get; set; } = new []
    {
        new Pair{Text = ArabicLetter.Alif,Count = "17152"},
new Pair{Text = ArabicLetter.Laam,Count = "11797"},
new Pair{Text = ArabicLetter.Miim,Count = "8659"},
new Pair{Text = ArabicLetter.Saad,Count = "152"},
new Pair{Text = ArabicLetter.Raa,Count = "1232"},
new Pair{Text = ArabicLetter.Kaaf,Count = "137"},
new Pair{Text = ArabicLetter.Haa,Count = "426"},
new Pair{Text = ArabicLetter.Yaa,Count = "580"},
new Pair{Text = ArabicLetter.Ayn,Count = "215"},
new Pair{Text = ArabicLetter.Taa,Count = "107"},
new Pair{Text = ArabicLetter.Siin,Count = "392"},
new Pair{Text = ArabicLetter.Haa_,Count = "292"},
new Pair{Text = ArabicLetter.Qaaf,Count = "114"},
new Pair{Text = ArabicLetter.Nun,Count = "133"},
new Pair{Text = "Toplam",Count = "41388"}
    };

    protected override Element render()
    {
        var delay = 200;
        
        int nextDelay() => delay += 300;

        return new FlexColumn(Gap(10))
        {
            new FlexColumn
            {
                new FlexRow(Gap(5), FlexWrap, JustifyContentFlexEnd)
                {
                    Records.Select((x,i) => CreateWithCount(i))
                }
            },

            new FlexRow(JustifyContentFlexEnd)
            {
                new ActionButton { Label = "Hesapla", OnClick = Calculate } + When(EnterJoInMode,DisplayNone)
            },
            Space(20),
            new FlexColumn
            {
                When(EnterJoInMode, () =>
                         new FlexRowCentered
                         {
                             Records.Select(x => InFadeAnimation(new div
                             {
                                 new Arrow { start = "begin-"+x.Text, end = "end-"+x.Text },
                                 CreateWithCount2(x.Text, x.Count)
                                 
                             }, nextDelay()))
                         }),

                //When(EnterJoInMode, () =>new div
                //{
                //    Records.Take(3).Select(x => InFadeAnimation(new Arrow { start = "begin-"+x.Text, end = "end-"+x.Text },nextDelay())),

                //    Records.TakeLast(3).Select(x => InFadeAnimation(new Arrow { start = "begin-"+x.Text, end = "end-"+x.Text },nextDelay())),



                //}),
                When(EnterJoInMode, () =>



                         new FlexRowCentered
                         {
                             new Fade
                             {
                                 triggerOnce = true,
                                 delay       = nextDelay(),
                                 children =
                                 {
                                     new img(MarginTopBottom(10))
                                     {
                                         src    = "wwwroot/img/arrow-down-double.svg",
                                         width  = 40,
                                         height = 40,
                                         
                                     }
                                 }
                             }

                         }),

                When(EnterJoInMode, () => new FlexRowCentered
                {

                    InFadeAnimation(new FlexRow { calculateResult()}, nextDelay())
                })
            }
        };
    }

    Element calculateResult()
    {
        var bigNumber = BigInteger.Parse(string.Join(string.Empty, Records.Select(x => x.Count)));

        if (bigNumber%19 == 0)
        {
            return new FlexRow(AlignItemsFlexEnd, Gap(3))
            {
                (strong)"19", (small)"x", (small)(bigNumber/19).ToString()
            };
        }

        return new div { bigNumber.ToString()};
    }
}

public class Pair
{
    public string Text { get; set; }
    public string Count { get; set; }

}