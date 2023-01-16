using System.Numerics;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.Libraries.react_awesome_reveal;
using ReactWithDotNet.react_xarrows;
    
namespace QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined;

class TotalCounts : ReactComponent
{
    public bool EnterJoInMode { get; set; }

    public IReadOnlyList<Pair> Records { get; set; } = new[]
    {
        new Pair { Text = ArabicLetter.Alif, Count = "17152" },
        new Pair { Text = ArabicLetter.Laam, Count = "11797" },
        new Pair { Text = ArabicLetter.Miim, Count = "8659" },
        new Pair { Text = ArabicLetter.Saad, Count = "152" },
        new Pair { Text = ArabicLetter.Raa, Count  = "1232" },
        new Pair { Text = ArabicLetter.Kaaf, Count = "137" },
        new Pair { Text = ArabicLetter.Haa, Count  = "426" },
        new Pair { Text = ArabicLetter.Yaa, Count  = "580" },
        new Pair { Text = ArabicLetter.Ayn, Count  = "215" },
        new Pair { Text = ArabicLetter.Taa, Count  = "107" },
        new Pair { Text = ArabicLetter.Siin, Count = "392" },
        new Pair { Text = ArabicLetter.Haa_, Count = "292" },
        new Pair { Text = ArabicLetter.Qaaf, Count = "114" },
        new Pair { Text = ArabicLetter.Nun, Count  = "133" },
        new Pair { Text = "Toplam", Count          = "41388" }
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
                    Records.Select((_, i) => CreateWithCount(i))
                }
            },

            new FlexRow(JustifyContentFlexEnd)
            {
                new ActionButton { Label = "Hesapla", OnClick = Calculate } + When(EnterJoInMode, DisplayNone)
            },
            Space(20),
            new FlexColumn
            {
                When(EnterJoInMode, () => new FlexRowCentered
                {
                    Records.Select((_, i) => AnimateRecord(i, nextDelay()))
                }),

                When(EnterJoInMode, () => EqualsTo(nextDelay())),

                When(EnterJoInMode, () => new FlexRowCentered
                {
                    InFadeAnimation(new FlexRow { CalculateResult() }, nextDelay())
                })
            }
        };
    }

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

    Element AnimateRecord(int recordIndex, int delayForAnimation)
    {
        var record = Records[recordIndex];

        var needArrow = recordIndex < 3 || recordIndex + 3 >= Records.Count;

        return InFadeAnimation(new div
        {
            When(needArrow, new Arrow { start = "begin-" + record.Text, end = "end-" + record.Text }),

            new Fade
            {
                triggerOnce  = true,
                direction = "down",
                delay = delayForAnimation + 200,
                children =
                {
                    new FlexRowCentered(ComponentBorder, BorderRadius(3), Id("end-" + record.Text)) { record.Count }
                }
            }
        }, delayForAnimation);
    }

    void Calculate()
    {
        EnterJoInMode = true;
    }

    Element CalculateResult()
    {
        var bigNumber = BigInteger.Parse(string.Join(string.Empty, Records.Select(x => x.Count)));

        if (bigNumber % 19 == 0)
        {
            return new FlexRow(AlignItemsFlexEnd, Gap(3))
            {
                (strong)"19", (small)"x", (small)(bigNumber / 19).ToString()
            };
        }

        return new div { bigNumber.ToString() };
    }

    Element CreateWithCount(int index)
    {
        Element countView = new FlexRowCentered
        {
            new input
            {
                type                     = "text", 
                valueBind = () => Records[index].Count,
                valueBindDebounceTimeout = 200,
                valueBindDebounceHandler = OnCountModified,
                style =
                {
                    Width(40),
                    TextAlignCenter,
                    Border($"0.1px solid {BorderColor}")
                }
            }
        };

        return new FlexColumn(ComponentBorder, BorderRadius(5), Padding(3), Gap(4), Id("begin-" + Records[index].Text))
        {
            new FlexRow(JustifyContentCenter) { AsLetter(Records[index].Text) },
            countView
        };
    }

    FlexRowCentered EqualsTo(int delayForAnimation)
    {
        return new FlexRowCentered
        {
            new Fade
            {
                triggerOnce = true,
                delay       = delayForAnimation,
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
        };
    }

    void OnCountModified()
    {
        Records.SkipLast(1).Sum(x => ParseInt(x.Count)).Then(total => Records[^1].Count = total.ToString());
    }

    class Arrow : ReactComponent
    {
        public string end;
        public string start;

        protected override Element render()
        {
            const string color = "#a9acaa";

            return new Xarrow
            {
                start       = start,
                end         = end,
                path        = "smooth",
                color       = color,
                strokeWidth = 1,
                startAnchor = "bottom",
                dashness    = true,
                endAnchor   = "top"
            };
        }
    }
}

public class Pair
{
    public string Count { get; set; }
    public string Text { get; set; }
}