using System.Numerics;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.Libraries.react_awesome_reveal;
using ReactWithDotNet.react_xarrows;
    
namespace QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined;

class TotalCounts : ReactComponent
{
    public bool EnterJoInMode { get; set; }

    public IReadOnlyList<InitialLetterCountInfo> Records { get; set; } = new[]
    {
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Alif, Count = "17152",
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 2, Count  = "4502"},
                new (){ChapterNumber = 3, Count  = "2521"},
                new (){ChapterNumber = 7, Count  = "2529"},
                new (){ChapterNumber = 10, Count = "1319"},
                new (){ChapterNumber = 11, Count = "1370"},
                new (){ChapterNumber = 12, Count = "1306"},
                new (){ChapterNumber = 13, Count = "605"},
                new (){ChapterNumber = 14, Count = "585"},
                new (){ChapterNumber = 15, Count = "493"},
                new (){ChapterNumber = 29, Count = "774"},
                new (){ChapterNumber = 30, Count = "544"},
                new (){ChapterNumber = 31, Count = "347"},
                new (){ChapterNumber = 32, Count = "257"}
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Laam, Count = "11797",
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 2, Count  = "3202"},
                new (){ChapterNumber = 3, Count  = "1892"},
                new (){ChapterNumber = 7, Count  = "1530"},
                new (){ChapterNumber = 10, Count = "913"},
                new (){ChapterNumber = 11, Count = "794"},
                new (){ChapterNumber = 12, Count = "812"},
                new (){ChapterNumber = 13, Count = "480"},
                new (){ChapterNumber = 14, Count = "452"},
                new (){ChapterNumber = 15, Count = "323"},
                new (){ChapterNumber = 29, Count = "554"},
                new (){ChapterNumber = 30, Count = "393"},
                new (){ChapterNumber = 31, Count = "297"},
                new (){ChapterNumber = 32, Count = "155"}
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Miim, Count = "8659",
            
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 2          ,Count = "2195" }, 
                new (){ChapterNumber = 3      ,Count     = "1249" },
                new (){ChapterNumber = 7      ,Count     = "1164" },
                new (){ChapterNumber = 13     ,Count     = "260" },
                new (){ChapterNumber = 26     ,Count     = "484" },
                new (){ChapterNumber = 28     ,Count     = "460" },
                new (){ChapterNumber = 29     ,Count     = "344" },
                new (){ChapterNumber = 30     ,Count     = "317" },
                new (){ChapterNumber = 31     ,Count     = "173" },
                new (){ChapterNumber = 32     ,Count     = "158" },
                new (){ChapterNumber = 40     ,Count     = "380" },
                new (){ChapterNumber = 41     ,Count     = "276" },
                new (){ChapterNumber = 42     ,Count     = "300" },
                new (){ChapterNumber = 43     ,Count     = "324" },
                new (){ChapterNumber = 44     ,Count     = "150" },
                new (){ChapterNumber = 45     ,Count     = "200" },
                new (){ChapterNumber = 46     ,Count     = "225" },
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Saad, Count = "152" ,
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 7          ,Count = "97" },
                new (){ChapterNumber = 19      ,Count     = "26" },
                new (){ChapterNumber = 38      ,Count     = "29" },
               
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Raa, Count = "1232",
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 10          ,Count = "257" },
                new (){ChapterNumber = 11      ,Count     = "325" },
                new (){ChapterNumber = 12      ,Count     = "257" },
                new (){ChapterNumber = 13     ,Count     = "137" },
                new (){ChapterNumber = 14     ,Count     = "160" },
                new (){ChapterNumber = 15     ,Count     = "96" }
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Kaaf, Count = "137" ,
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 19         ,Count = "137" }

            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Haa_, Count = "426",
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 19      ,Count    = "175" },
                new (){ChapterNumber = 20      ,Count    = "251" },

            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Yaa, Count = "580" ,
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 19      ,Count = "343" },
                new (){ChapterNumber = 36      ,Count = "237" }

            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Ayn, Count = "215" ,
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 19      ,Count = "117" },
                new (){ChapterNumber = 42      ,Count = "98" }
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Taa_, Count = "107",
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 20      ,Count = "28" },
                new (){ChapterNumber = 26      ,Count = "33" },
                new (){ChapterNumber = 27      ,Count = "27" },
                new (){ChapterNumber = 28      ,Count = "19" }
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Siin, Count = "392" ,
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 26      ,Count = "94" },
                new (){ChapterNumber = 27      ,Count = "94" },
                new (){ChapterNumber = 28      ,Count = "102" },
                new (){ChapterNumber = 36      ,Count = "48" },
                new (){ChapterNumber = 42      ,Count = "54" }
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Haa, Count = "292" ,
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 40      ,Count = "64" },
                new (){ChapterNumber = 41      ,Count = "48" },
                new (){ChapterNumber = 42      ,Count = "53" },
                new (){ChapterNumber = 43      ,Count = "44" },
                new (){ChapterNumber = 44      ,Count = "16" },
                new (){ChapterNumber = 45      ,Count = "31" },
                new (){ChapterNumber = 46      ,Count = "36" }
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Qaaf, Count = "114" ,
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 42, Count = "57"},
                new (){ChapterNumber = 50, Count = "57"}
            }
        },
        new InitialLetterCountInfo
        {
            Text = ArabicLetter.Nun, Count = "133",
            Details = new List<CountInfo>
            {
                new (){ChapterNumber = 68, Count = "133"}
            }
        },
        new InitialLetterCountInfo { Text = "Toplam", Count          = "41388" }
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

public class InitialLetterCountInfo
{
    public string Count { get; set; }
    public string Text { get; set; }
    
    public List<CountInfo> Details { get; set; }
}

public class CountInfo
{
    public int ChapterNumber { get; set; }
    public string Count { get; set; }
}