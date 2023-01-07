using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;
using QuranAnalyzer.WebUI.Pages.Shared;

namespace QuranAnalyzer.WebUI.Pages.WordSearchingPage;

class WordSearchingViewModel
{
    public int ClickCount { get; set; }

    public bool IsBlocked { get; set; }

    public string SearchScript { get; set; }

    public string SearchScriptErrorMessage { get; set; }
}

class WordSearchingView : ReactComponent<WordSearchingViewModel>
{
    protected override void componentDidMount()
    {
        Client.OnArabicKeyboardPressed(ArabicKeyboardPressed);
    }

    protected override void constructor()
    {
        state = new WordSearchingViewModel();

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
    }

    protected override Element render()
    {
        IEnumerable<Element> searchPanel ()=> new[]
        {
            When(state.IsBlocked, () => new div { PositionAbsolute, LeftRight(0), TopBottom(0), BackgroundColor("rgba(0, 0, 0, 0.3)"), Zindex(3) }),
            When(state.IsBlocked, () => new FlexRowCentered
            {
                PositionAbsolute, FontWeight700, LeftRight(0), TopBottom(0), Zindex(4),
                Children(new LoadingIcon { wh(17), mr(5) }, "Lütfen bekleyiniz...")
            }),

            new h4 { text = "Kelime Arama", style = { textAlign = "center" } },

            new FlexColumn
            {
                new FlexColumn
                {
                    new div { text = "Arama Komutu", style = { fontWeight = "500", fontSize = "0.9rem", marginBottom = "2px" } },

                    new TextArea { ValueBind = () => state.SearchScript, style = { FontFamily_Lateef } }, // rows = 2, autoResize = true,

                    new ErrorText { Text = state.SearchScriptErrorMessage }
                },

                Space(15),

                new FlexRow(JustifyContentSpaceBetween)
                {
                    new Helpcomponent(),
                    new ActionButton { Label = "Ara", OnClick = OnCaclculateClicked } + Height(22)
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

        Response<(List<WordColorizedVerse> resultVerseList, List<SummaryInfo> summaryInfoList)> calculate()
        {


            var matchMap = new Dictionary<string, List<(IReadOnlyList<LetterInfo> searchWord, IReadOnlyList<(LetterInfo start, LetterInfo end)> startPoints)>>();

            var summaries = new List<SummaryInfo>();

            foreach (var (chapterFilter, searchWord) in searchScript.Lines)
            {
                var filteredVersesResponse = VerseFilter.GetVerseList(chapterFilter);
                if (filteredVersesResponse.IsFail)
                {
                    return filteredVersesResponse.ErrorsAsArray;
                }

                var filteredVerses = filteredVersesResponse.Value;

                foreach (var verse in filteredVerses)
                {
                    var startAndEndPointsOfSameWords = verse.GetStartAndEndPointsOfSameWords(searchWord);
                    if (startAndEndPointsOfSameWords.Count > 0)
                    {
                        if (!matchMap.ContainsKey(verse.Id))
                        {
                            matchMap.Add(verse.Id, new List<(IReadOnlyList<LetterInfo> searchWord, IReadOnlyList<(LetterInfo start, LetterInfo end)> startPoints)>());
                        }

                        matchMap[verse.Id].Add((searchWord, startAndEndPointsOfSameWords));

                        // update summary
                        {
                            if (summaries.All(x => x.Name != searchWord.AsText()))
                            {
                                summaries.Add(new SummaryInfo { Name = searchWord.AsText() });
                            }

                            summaries.First(x => x.Name == searchWord.AsText()).Count += startAndEndPointsOfSameWords.Count;
                        }
                    }
                }
            }

            var sumOfChapterNumbers = 0;
            var sumOfVerseNumbers   = 0;
            var sumOfCounts         = 0;

            var resultVerses = new List<WordColorizedVerse>();

            foreach (var (verseId, matchList) in matchMap.ToList().OrderBy(x => x.Key, new VerseNumberComparer()))
            {
                resultVerses.Add(new WordColorizedVerse
                {
                    Verse     = VerseFilter.GetVerseById(verseId),
                    MatchList = matchList
                });

                sumOfChapterNumbers += int.Parse(verseId.Split(':')[0]);
                sumOfVerseNumbers   += int.Parse(verseId.Split(':')[1]);

                sumOfCounts += matchList.Sum(x => x.startPoints.Count).Unwrap();
            }

            sumOfChapterNumbers.ToString();


            return (resultVerses, summaries);
        }

        return calculate().Then((resultVerseList, summaryInfoList) =>
                                {
                                    var results = new Element[]
                                    {
                                        new h4("Sonuçlar"),

                                        new CountsSummaryView { Counts = summaryInfoList },
                                        new VSpace(30),
                                        new div
                                        {
                                            Children(resultVerseList)
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
        return new FlexColumn(BorderRadius(5), ComponentBorder, PaddingLeftRight(15), PaddingBottom(15), PositionRelative)
        {
            Children(rows)
        };
    }

    void ArabicKeyboardPressed(string letter)
    {
        state.SearchScriptErrorMessage = null;
        state.ClickCount               = 0;
        state.SearchScript             = state.SearchScript?.Trim() + " " + letter;
    }

    void ClearErrorMessage()
    {
        state.SearchScriptErrorMessage = null;
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
            Client.PushHistory("", $"/?{QueryKey.Page}={PageId.WordSearchingPage}&{QueryKey.SearchQuery}={script.AsString()}");
            Client.GotoMethod(OnCaclculateClicked);
            return;
        }

        state.IsBlocked = false;
    }
}