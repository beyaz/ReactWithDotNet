using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;
using ReactWithDotNet.PrimeReact;

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
                return;
            }

            state.SearchScript = parseResponse.Value.AsReadibleString();
        }
    }
    
    protected override void componentDidMount()
    {
        ClientTask.ListenEvent(ApplicationEventName.ArabicKeyboardPressed, ArabicKeyboardPressed);
    }

    protected override Element render()
    {
        var searchPanel = new divWithBorder
        {
            style = { paddingLeftRight = "15px", paddingBottom = "15px" },
            children =
            {
                new h4 { text = "Kelime Arama" , style = { textAlign = "center"}},

                new VStack
                {
                    new div { text = "Arama Komutu", style = { fontWeight = "500", fontSize = "0.9rem", marginBottom = "2px" } },
                    
                    new InputTextarea { valueBind = () => state.SearchScript, rows = 2, autoResize = true, style = { fontFamily = "Lateef, cursive" }},

                    new ErrorText { Text = state.SearchScriptErrorMessage }
                },

                new VSpace(3),

                new CharacterCountingOptionView(),

                new VSpace(20),
                
                new Button
                {
                    label     = "Ara",
                    onClick   = OnCaclculateClicked,
                    className = "p-button-outlined",
                    style     = { alignSelf = "flex-end", flexDirection = "column", paddingLeft = "50px", paddingRight = "50px" }
                }
            }
        };


        if (state.ClickCount == 0)
        {
            return searchPanel;
        }

        var parseResponse = SearchScript.ParseScript(state.SearchScript);
        if (parseResponse.IsFail)
        {
            state.SearchScriptErrorMessage = parseResponse.FailMessage;
            return searchPanel;
        }

        var searchScript = parseResponse.Value;
        
        if (state.IsBlocked)
        {
            return CalculatingComponent.WithBlockUI(searchPanel);
        }




        var matchMap = new Dictionary<string, List<(IReadOnlyList<LetterInfo> searchWord, IReadOnlyList<(LetterInfo start, LetterInfo end)> startPoints)>>();


       
     

        var summaryInfoList = new List<SummaryInfo>();

       
        
        foreach (var (chapterFilter, searchWord) in searchScript.Lines)
        {
            var verseListResponse = VerseFilter.GetVerseList(chapterFilter);
            if (verseListResponse.IsFail)
            {
                state.SearchScriptErrorMessage = verseListResponse.FailMessage;
                return searchPanel;
            }

            var verseList = verseListResponse.Value;
            
            foreach (var verse in verseList)
            {
                var startAndEndPointsOfSameWords = verse.GetStartAndEndPointsOfSameWords(searchWord);
                if (startAndEndPointsOfSameWords.Count > 0)
                {
                    if (!matchMap.ContainsKey(verse.Id))
                    {
                        matchMap.Add(verse.Id,new List<(IReadOnlyList<LetterInfo> searchWord, IReadOnlyList<(LetterInfo start, LetterInfo end)> startPoints)>());
                    }

                    matchMap[verse.Id].Add((searchWord, startAndEndPointsOfSameWords));

                   

                    // update summary
                    {
                        if (summaryInfoList.All(x => x.Name != searchWord.AsText()))
                        {
                            summaryInfoList.Add(new SummaryInfo { Name = searchWord.AsText() });
                        }

                        summaryInfoList.First(x => x.Name == searchWord.AsText()).Count += startAndEndPointsOfSameWords.Count;
                    }
                }
            }
        }

        var resultVerseList = new List<WordColorizedVerse>();
        
        foreach (var (verseId, matchList) in matchMap.ToList().OrderBy(x=>x.Key, new VerseNumberComparer()))
        {
            resultVerseList.Add(new WordColorizedVerse
            {
                Verse     = VerseFilter.GetVerseById(verseId),
                MatchList = matchList
            });
        }

        var results = new divWithBorder
        {
            style = { paddingLeftRight = "15px", paddingBottom = "15px", marginTop = "5px" },
            children =
            {
                new h4("Sonuçlar"),

                new CountsSummaryView { Counts = summaryInfoList },
                new VSpace(30),
                new div
                {
                   Children = resultVerseList
                }
            }
        };


        return new div
        {
            children = { searchPanel, results },
            style =
            {
                display = "flex", flexDirection = "column", alignItems = "stretch"
            }
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
    
    void OnCaclculateClicked(string _)
    {
        state.SearchScriptErrorMessage = null;
        if (state.SearchScript.HasNoValue())
        {
            state.SearchScriptErrorMessage = "Arama Komutu doldurulmalıdır";
            return;
        }

        var scriptParseResponse = SearchScript.ParseScript(state.SearchScript);
        if (scriptParseResponse.IsFail)
        {
            state.SearchScriptErrorMessage = scriptParseResponse.FailMessage;
            return;
        }

        var script = scriptParseResponse.Value;
        
        state.ClickCount++;

        if (state.IsBlocked == false)
        {
            state.IsBlocked = true;
            ClientTask.PushHistory("", $"/?{QueryKey.Page}={PageId.WordSearchingPage}&{QueryKey.SearchQuery}={script.AsString()}");
            ClientTask.GotoMethod(5, OnCaclculateClicked, _);
            return;
        }

        state.IsBlocked = false;
    }
}