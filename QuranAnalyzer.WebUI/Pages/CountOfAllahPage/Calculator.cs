using System.Collections.Immutable;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.Libraries.react_awesome_reveal;

namespace QuranAnalyzer.WebUI.Pages.CountOfAllahPage;

class CalculatorModel
{
    public string ErrorText { get; set; }
    public bool IsProcessing { get; set; }
    public string SearchScript { get; set; }
    public bool ShowResults { get; set; }
    public string Word { get; set; }
}

class Calculator : ReactComponent<CalculatorModel>
{
    public string Word, SearchScript;

    public bool ShowDetails { get; set; }

    public bool ShowNumbers { get; set; }

    public bool ShowVerseList { get; set; }

    protected override void constructor()
    {
        state = new CalculatorModel
        {
            SearchScript = SearchScript,
            Word         = Word
        };
    }

    protected override Element render()
    {
        return new FlexColumn(Gap(10))
        {
            new FlexColumn
            {
                Gap(3),

                new Label { Text = "Ayet Seç" },
                new TextInput
                {
                    TextInput.Bind(() => state.SearchScript),
                    FlexGrow(1)
                }
            },

            new FlexColumn
            {
                Gap(3),

                new Label { Text = "Kelime" },
                new TextInput
                {
                    TextInput.Bind(() => state.Word),
                    FlexGrow(1)
                }
            },

            new FlexRow(AlignItemsCenter, state.ErrorText.HasValue() ? JustifyContentSpaceBetween : JustifyContentFlexEnd)
            {
                new ErrorText { Text     = state.ErrorText },
                new ActionButton { Label = "Hesapla", OnClick = OnClick, IsProcessing = state.IsProcessing }
            },

            When(state.ShowResults, InFadeAnimation(GetCalculationText))
        };
    }

    static Func<Element> InFadeAnimation(Func<Element> func)
    {
        return () => new Fade
        {
            triggerOnce = true,
            children =
            {
                func()
            }
        };
    }

    void Calculate()
    {
        state.ShowResults  = true;
        state.IsProcessing = false;
    }

    Element GetCalculationText()
    {
        var filteredVerses = VerseFilter.GetVerseList(state.SearchScript).Unwrap();

        var searchWords = state.Word.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Analyzer.AnalyzeText(x)).ToImmutableArray();

        var details = new List<(int chapterNumber, int verseNumber, int count)>();

        foreach (var verse in filteredVerses)
        {
            var count = searchWords.Sum(searchWord => verse.GetStartAndEndPointsOfSameWords(searchWord).Count).Unwrap();
            if (count > 0)
            {
                details.Add((verse.ChapterNumber, verse.IndexAsNumber, count));
            }
        }

        var sumOfChapterNumbers = details.Select(x => x.chapterNumber).Sum();
        var sumOfVerseNumbers   = details.Select(x => x.verseNumber).Sum();
        var sumOfCounts         = details.Select(x => x.count).Sum();

        static IEnumerable<Element> numberToElement(int value)
        {
            if (value > 0 && value % 19 == 0)
            {
                return new[] { (b)"19" + FontWeight600 + Color("#3b29b8"), (div)"x" + MarginLeftRight(3) + FontSizeSmall, (div)$"{value / 19}" + FontSizeSmall };
            }

            return new[] { (div)value.ToString() };
        }

        var results = new FlexRow(ComponentBorder)
        {
            new FlexColumn(FlexGrow(1), AlignItemsCenter, ComponentBorder)
            {
                new b { "Sure No Toplam" },
                new FlexRow(AlignItemsFlexEnd)
                {
                    numberToElement(sumOfChapterNumbers)
                }
            },

            new FlexColumn(FlexGrow(1), AlignItemsCenter, ComponentBorder)
            {
                new b { "Ayet No Toplam" },
                new FlexRow(AlignItemsFlexEnd)
                {
                    numberToElement(sumOfVerseNumbers)
                }
            },

            new FlexColumn(FlexGrow(1), AlignItemsCenter, ComponentBorder)
            {
                new b { "Adet Toplam" },
                new FlexRow(AlignItemsFlexEnd)
                {
                    numberToElement(sumOfCounts)
                }
            }
        };

        if (ShowDetails)
        {
            return new table(WidthMaximized)
            {
                new thead
                {
                    new tr
                    {
                        new th
                        {
                            new FlexColumn(FlexGrow(1), AlignItemsCenter, ComponentBorder)
                            {
                                new b { "Sure No" }
                            }
                        },
                        new th
                        {
                            new FlexColumn(FlexGrow(1), AlignItemsCenter, ComponentBorder)
                            {
                                new b { "Ayet No" }
                            }
                        },
                        new th
                        {
                            new FlexColumn(FlexGrow(1), AlignItemsCenter, ComponentBorder)
                            {
                                new b { "Adet" }
                            }
                        }
                    }
                },

                new tbody
                {
                    details.Select(x => new tr(ComponentBorder)
                    {
                        new th(TextAlignCenter, FontWeight500)
                        {
                            x.chapterNumber.ToString()
                        },

                        new th(TextAlignCenter, FontWeight500)
                        {
                            x.verseNumber.ToString()
                        },

                        new th(TextAlignCenter, FontWeight500)
                        {
                            x.count.ToString()
                        }
                    })
                },

                new tfoot
                {
                    new tr
                    {
                        new th
                        {
                            new FlexColumn(AlignItemsCenter, ComponentBorder)
                            {
                                new b { "Toplam" },
                                new FlexRow(AlignItemsFlexEnd)
                                {
                                    numberToElement(sumOfChapterNumbers)
                                }
                            }
                        },
                        new th
                        {
                            new FlexColumn(AlignItemsCenter, ComponentBorder)
                            {
                                new b { "Toplam" },
                                new FlexRow(AlignItemsFlexEnd)
                                {
                                    numberToElement(sumOfVerseNumbers)
                                }
                            }
                        },
                        new th
                        {
                            new FlexColumn(AlignItemsCenter, ComponentBorder)
                            {
                                new b { "Toplam" },
                                new FlexRow(AlignItemsFlexEnd)
                                {
                                    numberToElement(sumOfCounts)
                                }
                            }
                        }
                    }
                }
            };
        }

        return results;
    }

    void OnClick()
    {
        state.ShowResults = false;
        state.ErrorText   = null;

        if (state.SearchScript.HasNoValue())
        {
            state.ErrorText = "Ayet seçmelisiniz.";
            return;
        }

        var verseList = VerseFilter.GetVerseList(state.SearchScript);
        if (verseList.IsFail)
        {
            state.ErrorText = verseList.FailMessage;
            return;
        }

        if (verseList.Value.Count == 0)
        {
            state.ErrorText = "En az bir tane ayet seçilmelidir.";
            return;
        }

        if (state.Word.HasNoValue())
        {
            state.ErrorText = "En az bir tane Arapça kelime girilmelidir.";
            return;
        }

        var letters = Analyzer.AnalyzeText(state.Word.Replace(" ", ""));

        if (letters.Count == 0)
        {
            state.ErrorText = "En az bir tane Arapça kelime girilmelidir.";
            return;
        }

        state.IsProcessing = true;

        Client.GotoMethod(Calculate);
    }
}