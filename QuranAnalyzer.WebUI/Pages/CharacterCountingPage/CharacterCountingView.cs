using System.Text;
using System.Threading.Tasks;
using QuranAnalyzer.WebUI.Pages.Shared;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

[Serializable]
public class CharacterCountingViewModel
{
    public int ClickCount { get; set; }

    public bool IncludeBismillah { get; set; } = true;

    public bool IsBlocked { get; set; }

    public MushafOption MushafOption { get; set; } = new();

    public string SearchScript { get; set; }

    public string SearchScriptErrorMessage { get; set; }
}

class CharacterCountingView : ReactComponent<CharacterCountingViewModel>
{
    protected override Task componentDidMount()
    {
        Client.OnArabicKeyboardPressed(ArabicKeyboardPressed);

        return Task.CompletedTask;
    }

    protected override void constructor()
    {
        state = new CharacterCountingViewModel();

        var value = Context.Query[QueryKey.SearchQuery];
        if (value is not null)
        {
            var parseResponse = SearchScript.ParseScript(value);
            if (parseResponse.IsFail)
            {
                state.SearchScriptErrorMessage = parseResponse.FailMessage;
                Client.GotoMethod(3000, ClearErrorMessage);
                return;
            }

            state.SearchScript = parseResponse.Value.AsReadibleString();
        }

        if (Context.Query[QueryKey.IncludeBizmillah] == "0")
        {
            state.IncludeBismillah = false;
        }
    }

    static Element Backdrop()
    {
        return new div
        {
            PositionAbsolute, LeftRight(0), TopBottom(0), BackgroundColor("rgba(0, 0, 0, 0.3)"), Zindex(3), BorderRadiusForPanels
        };
    }
    protected override Element render()
    {
        IEnumerable<Element> searchPanel() => new[]
        {
            When(state.IsBlocked, Backdrop),
            When(state.IsBlocked, () => new FlexRowCentered
            {
                PositionAbsolute, FontWeight700, LeftRight(0), TopBottom(0), Zindex(4),
                Children(new LoadingIcon { wh(17), mr(5) }, new span(Color("white")){"Lütfen bekleyiniz..."})
            }),
            new h4 { text = "Harf Arama", style = { TextAlignCenter } },
            new FlexColumn
            {
                new FlexColumn
                {
                    new div { text = "Arama Komutu", style = { fontWeight = "500", fontSize = "0.9rem", marginBottom = "2px" } },

                    new TextArea { TextArea.Bind(() => state.SearchScript) },

                    new ErrorText { Text = state.SearchScriptErrorMessage }
                },

                Space(3),

                new FlexRow(AlignItemsCenter)
                {
                    new CharacterCountingOptionView { MushafOption = state.MushafOption, MushafOptionChanged = MushafOptionChanged },
                    Space(30),
                    new SwitchWithLabel
                    {
                        Label       = "Besmele'yi dahil et",
                        Value       = state.IncludeBismillah,
                        ValueChange = OnIncludeBismillahChanged
                    }
                },

                Space(20),

                new FlexRow(JustifyContentSpaceBetween)
                {
                    new Helpcomponent { ShowHelpMessageForLetterSearch = true },

                    new ActionButton { Label = "Ara", OnClick = OnCaclculateClicked, IsProcessing = state.IsBlocked } + Height(22)
                }
            }
        };

        if (state.ClickCount == 0)
        {
            return Container(Panel(searchPanel()));
        }

        var searchScript = SearchScript.ParseScript(state.SearchScript).Unwrap();

        if (state.IsBlocked)
        {
            return Container(Panel(searchPanel()));
        }

        Response<(List<LetterColorizer> resultVerseList, List<SummaryInfo> summaryInfoList)> calculate()
        {
            var resultVerses = new List<LetterColorizer>();

            var summaries = new List<SummaryInfo>();

            foreach (var (chapterFilter, searchLetters) in searchScript.Lines)
            {
                var filteredVersesResponse = VerseFilter.GetVerseList(chapterFilter);
                if (filteredVersesResponse.IsFail)
                {
                    return filteredVersesResponse.ErrorsAsArray;
                }

                var filteredVerses = filteredVersesResponse.Value;

                SummaryInfo getSummaryInfo(LetterInfo letterInfo)
                {
                    return new SummaryInfo
                    {
                        Count = QuranAnalyzerMixin.GetCountOfLetter(filteredVerses, letterInfo.ArabicLetterIndex, state.MushafOption, state.IncludeBismillah),
                        Name  = letterInfo.MatchedLetter
                    };
                }

                foreach (var summaryInfo in searchLetters.AsListOf(getSummaryInfo))
                {
                    if (summaries.Any(x => x.Name == summaryInfo.Name))
                    {
                        summaries.First(x => x.Name == summaryInfo.Name).Count += summaryInfo.Count;
                        continue;
                    }

                    summaries.Add(summaryInfo);
                }

                foreach (var verse in filteredVerses)
                {
                    var analyzedTextOfVerse = state.IncludeBismillah ? verse.TextWithBismillahAnalyzed : verse.TextAnalyzed;

                    if (analyzedTextOfVerse.Any(x => searchLetters.Any(l => l.ArabicLetterIndex == x.ArabicLetterIndex)))
                    {
                        var letterColorizer = new LetterColorizer
                        {
                            VerseTextNodes          = analyzedTextOfVerse,
                            ChapterNumber           = verse.ChapterNumber.ToString(),
                            VerseNumber             = verse.Index,
                            LettersForColorizeNodes = searchLetters,
                            VerseText               = state.IncludeBismillah ? verse.TextWithBismillah : verse.Text,
                            Verse                   = verse,
                            MushafOption            = state.MushafOption
                        };

                        resultVerses.Add(letterColorizer);
                    }
                }
            }

            return (resultVerses, summaries);
        }

        return calculate().Then((resultVerseList, summaryInfoList) =>
                                {
                                    #pragma warning disable CS8321
                                    a downloadAsExcel()

                                    {
                                        var header = "Sure No; Ayet No; Ayet";

                                        var rows = string.Join('\n', resultVerseList.Select(x => $"{x.ChapterNumber};{x.VerseNumber};{x.VerseText}"));

                                        var data = string.Join('\n', header, rows);

                                        data = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));

                                        return new a
                                        {
                                            href     = "data:text/csv;base64,77u/" + data,
                                            text     = "Exel olarak indir",
                                            target   = "_blank",
                                            download = "Arama Sonuçları.csv"
                                        };
                                    }
                                    #pragma warning restore CS8321

                                    Element[] results =
                                    {
                                        new h4("Sonuçlar") + TextAlignCenter,
                                        new CountsSummaryView { Counts = summaryInfoList },
                                        new VSpace(30),
                                        new div
                                        {
                                            dangerouslySetInnerHTML =new div
                                            {
                                                resultVerseList
                                            }.ToString()
                                        }
                                        
                                    };

                                    return Container(Panel(searchPanel()), Panel(results));
                                },
                                failMessage =>
                                {
                                    state.SearchScriptErrorMessage = failMessage;

                                    return Container(Panel(searchPanel()));
                                });
    }

    static Element Container(params Element[] panels)
    {
        return new FlexColumn(Gap(10), AlignItemsStretch, WidthMaximized, MaxWidth(800))
        {
            Children(panels)
        };
    }

    static Element Panel(IEnumerable<Element> rows)
    {
        return new FlexColumn(BorderRadiusForPanels, ComponentBorder, PaddingLeftRight(15), PaddingBottom(15), PositionRelative)
        {
            Children(rows)
        };
    }

    void ArabicKeyboardPressed(string letter)
    {
        state.ClickCount = 0;
        
        state.SearchScriptErrorMessage = null;
        
        state.SearchScript = state.SearchScript?.Trim() + " " + letter;
    }

    void ClearErrorMessage()
    {
        state.SearchScriptErrorMessage = null;
    }

    void MushafOptionChanged(MushafOption mushafOption)
    {
        state.ClickCount   = 0;
        state.MushafOption = mushafOption;
    }

    void OnCaclculateClicked()
    {
        state.SearchScriptErrorMessage = null;

        if (state.SearchScript.HasNoValue())
        {
            state.SearchScriptErrorMessage = "Arama Komutu doldurulmalıdır";
            Client.GotoMethod(1000, ClearErrorMessage);
            return;
        }

        var scriptParseResponse = SearchScript.ParseScript(state.SearchScript);
        if (scriptParseResponse.IsFail)
        {
            state.SearchScriptErrorMessage = scriptParseResponse.FailMessage;
            Client.GotoMethod(3000, ClearErrorMessage);
            return;
        }

        var script = scriptParseResponse.Value;

        state.ClickCount++;

        if (state.IsBlocked == false)
        {
            state.IsBlocked = true;
            Client.PushHistory("", $"/?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={script.AsString()}&{QueryKey.IncludeBizmillah}={state.IncludeBismillah.AsNumber()}");
            Client.GotoMethod(OnCaclculateClicked);
            return;
        }

        state.IsBlocked = false;
    }

    void OnIncludeBismillahChanged(ChangeEvent changeEvent)
    {
        state.ClickCount = 0;

        state.IncludeBismillah = Convert.ToBoolean(changeEvent.target.value);
    }
}