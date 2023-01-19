using System.Numerics;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.Libraries.react_awesome_reveal;
using ReactWithDotNet.react_xarrows;

namespace QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined;

class TotalCounts : ReactComponent
{
    public bool EnterJoInMode { get; set; }

    public IReadOnlyList<InitialLetterCountInfo> Records { get; set; } = Extensions.AllInitialLetterTotalCounts;

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

    static string GetIdOf(int recordIndex, bool isBegin)
    {
        return $"{nameof(TotalCounts)}-{(isBegin ? "begin" : "end")}-{recordIndex}";
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
            When(needArrow, new Arrow { start = GetIdOf(recordIndex, isBegin: true), end = GetIdOf(recordIndex, isBegin: false) }),

            new Fade
            {
                triggerOnce = true,
                direction   = "down",
                delay       = delayForAnimation + 200,
                children =
                {
                    new FlexRowCentered(ComponentBorder, BorderRadius(3), Id(GetIdOf(recordIndex, isBegin: false))) { record.Count }
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

    input CreateInput(Expression<Func<string>> bindingExpression)
    {
        return new input(Width(40), TextAlignCenter, Border($"0.1px solid {BorderColor}"))
        {
            type                     = "text",
            valueBind                = bindingExpression,
            valueBindDebounceTimeout = 200,
            valueBindDebounceHandler = RecalculateTotalCounts
        };
    }

    Element CreateWithCount(int recordIndex)
    {
        return new FlexColumn(ComponentBorder, BorderRadius(5), Padding(3), Gap(4), Id(GetIdOf(recordIndex, isBegin: true)))
        {
            new FlexRow(JustifyContentCenter) { AsLetter(Records[recordIndex].Text) },
            new FlexRowCentered
            {
                CreateInput(() => Records[recordIndex].Count)
            }
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

    void RecalculateTotalCounts()
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