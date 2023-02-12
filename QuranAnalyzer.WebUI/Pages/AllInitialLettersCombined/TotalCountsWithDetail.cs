using System.Numerics;
using System.Text;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.Libraries.react_awesome_reveal;
using ReactWithDotNet.react_xarrows;

namespace QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined;

class TotalCountsWithDetail : ReactComponent
{
    public bool EnterJoInMode { get; set; }

    public bool IncludeChapterNumbers { get; set; }

    public IReadOnlyList<InitialLetterCountInfo> Records { get; set; } = Extensions.AllInitialLetterTotalCounts;

    static StyleModifier InputBorder => Border($"0.1px solid {BorderColor}");

    protected override Element render()
    {
        var nextDelay = CreateDelayAccessMethod();

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
            When(EnterJoInMode, () => new FlexColumn
            {
                new FlexRowCentered(FlexWrap)
                {
                    AnimateRecords(nextDelay)
                },

                EqualsTo(nextDelay()),

                new FlexRowCentered
                {
                    InFadeAnimation(new FlexRow { CalculateResult() }, nextDelay())
                }
            })
        };
    }

    static Func<int> CreateDelayAccessMethod()
    {
        var delay = 200;

        int nextDelay() => delay += 300;

        return nextDelay;
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

    

    List<Element> AnimateRecords(Func<int> nextDelay)
    {
        bool NeedArrow(int recordIndex, int? detailIndex)
        {
            if (recordIndex == 0 && detailIndex < 3)
            {
                return true;
            }

            if (recordIndex == Records.Count - 1)
            {
                return true;
            }

            if (recordIndex == Records.Count - 2)
            {
                return true;
            }

            return false;
        }

        var returnList = new List<Element>();

        var lastDelay = 0;

        for (var recordIndex = 0; recordIndex < Records.Count; recordIndex++)
        {
            var record = Records[recordIndex];

            bool needArrow(int? detailIndex) => NeedArrow(recordIndex, detailIndex);

            if (record.Details is not null)
            {
                for (var i = 0; i < record.Details.Count; i++)
                {
                    var x = record.Details[i];

                    var drawArrow = needArrow(i);

                    if (drawArrow)
                    {
                        lastDelay = nextDelay();
                    }

                    returnList.Add(InFadeAnimation(new div
                    {
                        When(drawArrow, new Arrow { start = GetIdOf(isBegin: true, recordIndex, i), end = GetIdOf(isBegin: false, recordIndex, i) }),

                        new Fade
                        {
                            triggerOnce = true,
                            direction   = "down",
                            delay       = lastDelay + 400,
                            children =
                            {
                                new FlexRowCentered(ComponentBorder, BorderRadius(3), MarginRight(3), Id(GetIdOf(isBegin: false, recordIndex, i)))
                                {
                                    IncludeChapterNumbers ? x.ChapterNumber + ":" + x.Count : x.Count
                                }
                            }
                        }
                    }, lastDelay));
                }

                {
                    var drawArrow = needArrow(null);

                    if (drawArrow)
                    {
                        lastDelay = nextDelay();
                    }

                    // add total count of letter
                    returnList.Add(InFadeAnimation(new div
                    {
                        When(drawArrow, new Arrow
                        {
                            start       = GetIdOf(isBegin: true, recordIndex, null),
                            end         = GetIdOf(isBegin: false, recordIndex, null),
                            startAnchor = "bottom"
                        }),

                        new Fade
                        {
                            triggerOnce = true,
                            direction   = "down",
                            delay       = lastDelay + 200,
                            children =
                            {
                                new FlexRowCentered(ComponentBorder, BorderRadius(3), Id(GetIdOf(isBegin: false, recordIndex, null))) { record.Count }
                            }
                        }
                    }, lastDelay));
                }

                continue;
            }

            lastDelay = nextDelay();

            // add total count
            returnList.Add(InFadeAnimation(new div
            {
                When(needArrow(null), new Arrow { start = GetIdOf(isBegin: true, recordIndex, null), end = GetIdOf(isBegin: false, recordIndex, null) }),

                new Fade
                {
                    triggerOnce = true,
                    direction   = "down",
                    delay       = lastDelay + 200,
                    children =
                    {
                        new FlexRowCentered(ComponentBorder, BorderRadius(3), Id(GetIdOf(isBegin: false, recordIndex, null))) { record.Count }
                    }
                }
            }, lastDelay));
        }

        return returnList;
    }

    void Calculate()
    {
        EnterJoInMode = true;
    }

    Element CalculateResult()
    {
        var sb = new StringBuilder();
        foreach (var letterCountInfo in Records)
        {
            if (letterCountInfo.Details is not null)
            {
                foreach (var countInfo in letterCountInfo.Details)
                {
                    if (IncludeChapterNumbers)
                    {
                        sb.Append(countInfo.ChapterNumber);
                    }

                    sb.Append(countInfo.Count);
                }
            }

            // total count
            sb.Append(letterCountInfo.Count);
        }

        var bigNumber = BigInteger.Parse(sb.ToString());

        if (bigNumber % 19 == 0)
        {
            return new FlexRow(AlignItemsFlexStart, Gap(3))
            {
                (strong)"19", (small)"x", (small)(bigNumber / 19).ToString() + OverflowWrapAnywhere
            };
        }

        return new small { bigNumber.ToString(), OverflowWrapAnywhere };
    }

    input CreateInput(Expression<Func<string>> bindingExpression)
    {
        return new input(Width(40), TextAlignCenter, InputBorder)
        {
            type                     = "text",
            valueBind                = bindingExpression,
            valueBindDebounceTimeout = 200,
            valueBindDebounceHandler = RecalculateTotalCounts
        };
    }

    Element CreateWithCount(int recordIndex)
    {
        return new FlexColumn(ComponentBorder, BorderRadius(5), Padding(3), Gap(4), Id("begin-" + Records[recordIndex].Text))
        {
            new FlexRow(JustifyContentCenter) { AsLetter(Records[recordIndex].Text) },
            new FlexRow(Gap(5), FontWeight600, FontSize("0.8rem"), TextAlignCenter) { (small)"Sure No" + Width(50), (small)"Adet" + Width(40) },
            new FlexColumn(AlignItemsCenter)
            {
                Records[recordIndex].Details?.Select((_, i) => new FlexRow(AlignItemsStretch)
                {
                    new small { Records[recordIndex].Details[i].ChapterNumber.ToString() } + Width(50) + TextAlignCenter + FontSize("0.7rem") + InputBorder +
                    DisplayFlex + JustifyContentCenter + AlignItemsCenter,
                    CreateInput(() => Records[recordIndex].Details[i].Count) + Id(GetIdOf(isBegin: true, recordIndex, i))
                })
            },
            new FlexRow(AlignItemsStretch)
            {
                new small { "Toplam" } + Width(50) + TextAlignCenter + FontSize("0.7rem") + InputBorder + DisplayFlex + JustifyContentCenter + AlignItemsCenter,
                CreateInput(() => Records[recordIndex].Count) + Id(GetIdOf(isBegin: true, recordIndex, null))
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
                        src    = FileAtImgFolder("arrow-down-double.svg"),
                        width  = 40,
                        height = 40,
                    }
                }
            }
        };
    }

    string GetIdOf(bool isBegin, int recordIndex, int? detailIndex)
    {
        var seperator = IncludeChapterNumbers ? "-" : ".";
        return string.Join(seperator,
                           nameof(TotalCountsWithDetail),
                           isBegin ? "begin" : "end",
                           nameof(recordIndex),
                           recordIndex,
                           nameof(detailIndex),
                           detailIndex);
    }

    void RecalculateTotalCounts()
    {
        foreach (var item in Records.SkipLast(1))
        {
            item.Details.Sum(x => ParseInt(x.Count)).Then(total => item.Count = total.ToString());
        }

        Records.SkipLast(1).Sum(x => ParseInt(x.Count)).Then(total => Records[^1].Count = total.ToString());
    }

    class Arrow : ReactComponent
    {
        public string end;
        public string start;

        public string startAnchor = "left";

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
                startAnchor = startAnchor,
                dashness    = true,
                endAnchor   = "top"
            };
        }
    }
}