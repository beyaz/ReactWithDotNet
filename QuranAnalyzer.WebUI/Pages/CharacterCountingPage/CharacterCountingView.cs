using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.PrimeReact;
using static QuranAnalyzer.Analyzer;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

[Serializable]
public class CharacterCountingViewModel
{
    #region Public Properties
    public int ClickCount { get; set; }

    public bool IsBlocked { get; set; }

    public MushafOption MushafOption { get; set; } = new();

    public string SearchScript { get; set; }

    public string SearchScriptErrorMessage { get; set; }
    #endregion
}

class CharacterCountingView : ReactComponent<CharacterCountingViewModel>
{
    #region Methods
    protected override void componentDidMount()
    {
        ClientTask.ListenEvent(ApplicationEventName.ArabicKeyboardPressed, ArabicKeyboardPressed);
        ClientTask.ListenEvent(ApplicationEventName.MushafOptionChanged, MushafOptionChanged);
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
                return;
            }

            state.SearchScript = parseResponse.Value.AsReadibleString();
        }
    }

    protected override Element render()
    {
        if (state.SearchScriptErrorMessage.HasValue())
        {
            ClientTask.GotoMethod(5000, ClearErrorMessage);
        }

        var searchPanel = new divWithBorder
        {
            style = { paddingLeftRight = "15px", paddingBottom = "15px" },

            children =
            {
                new h4 { text = "Harf Arama", style = { textAlign = "center" } },
                new VStack
                {
                    new VStack
                    {
                        new div { text                = "Arama Komutu", style          = { fontWeight = "500", fontSize = "0.9rem", marginBottom = "2px" } },
                        new InputTextarea { valueBind = () => state.SearchScript, rows = 2, autoResize = true },
                        new ErrorText { Text          = state.SearchScriptErrorMessage }
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
                    },
                }
            }
        };

        if (state.ClickCount == 0)
        {
            return searchPanel;
        }

        if (state.IsBlocked)
        {
            return CalculatingComponent.WithBlockUI(searchPanel);
        }

        var mushafVerse = new List<LetterColorizer>();

        var summaryInfoList = new List<SummaryInfo>();

        foreach (var (ChapterFilter, SearchLetters) in SearchScript.ParseScript(state.SearchScript).Value.Lines)
        {
            var chapterFilter         = ChapterFilter;
            var searchLettersAsString = string.Join("", SearchLetters);

            var searchLetters = AnalyzeText(searchLettersAsString).Unwrap().Where(IsArabicLetter).GroupBy(x => x.ArabicLetterIndex).Select(grp => grp.FirstOrDefault()).Distinct().ToList();

            summaryInfoList.AddRange(searchLetters.AsListOf(x => new SummaryInfo
            {
                Count = VerseFilter.GetVerseList(chapterFilter).Then(verses => QuranAnalyzerMixin.GetCountOfLetter(verses, x.ArabicLetterIndex, state.MushafOption)).Value,
                Name  = x.MatchedLetter
            }));

            foreach (var verse in VerseFilter.GetVerseList(chapterFilter).Value)
            {
                if (verse.AnalyzedFullText.Any(x => searchLetters.Any(l => l.ArabicLetterIndex == x.ArabicLetterIndex)))
                {
                    var letterColorizer = new LetterColorizer
                    {
                        VerseTextNodes          = verse.AnalyzedFullText,
                        ChapterNumber           = verse.ChapterNumber.ToString(),
                        VerseNumber             = verse.Index,
                        LettersForColorizeNodes = searchLetters,
                        VerseText               = verse.TextWithBismillah,
                        Verse                   = verse,
                        MushafOption            = state.MushafOption
                    };

                    mushafVerse.Add(letterColorizer);
                }
            }
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
                    Children = mushafVerse
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

    void MushafOptionChanged(MushafOption mushafOption)
    {
        state.ClickCount   = 0;
        state.MushafOption = mushafOption;
        Context.Set(ContextKey.MushafOptionKey, state.MushafOption);
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
            ClientTask.PushHistory("", $"/?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={script.AsString()}");
            ClientTask.GotoMethod(5, OnCaclculateClicked, _);
            return;
        }

        state.IsBlocked = false;
    }
    #endregion
}

// TODO: check and remove : QuranAnalyzer_WebUI_PanelHeaderTemplate