using System.Text;
using System.Text.RegularExpressions;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.PrimeReact;
using static QuranAnalyzer.Analyzer;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

[Serializable]
public class CharacterCountingViewModel
{
    public MushafOption MushafOption { get; set; } = new();

    public int ClickCount { get; set; }

    public bool IsBlocked { get; set; }

    public string SearchScript { get; set; }
    
    public string SearchScriptErrorMessage { get; set; }
}

class SearchScript
{
    public IReadOnlyList<(string ChapterFilter, IReadOnlyList<string> SearchLetters)> Lines { get; private set; }

    public static SearchScript ParseScript(string value)
    {
        var lines = parseToLines(value).AsListOf(parseLine);

        return new SearchScript
        {
            Lines = lines
        };


        static IEnumerable<string> parseToLines(string value)
        {
            value = value.Replace(Environment.NewLine, ";");
            
            return value.Split(';', StringSplitOptions.RemoveEmptyEntries);
        }

        static (string ChapterFilter, IReadOnlyList<string> SearchLetters) parseLine(string line)
        {
            var arr = line.Split('|', StringSplitOptions.RemoveEmptyEntries);

            return (arr[0].Trim(), AnalyzeText(clearText(arr[1])).Where(IsArabicLetter).AsListOf(x => x.MatchedLetter));
        }

        static string clearText(string str) => Regex.Replace(str, @"\s+", String.Empty);

    }

    public string AsReadibleString()
    {
        var sb = new StringBuilder();

        foreach (var line in Lines)
        {
            sb.AppendLine(line.ChapterFilter + " | " + string.Join(" ", line.SearchLetters));
        }

        return sb.ToString();
    }

    public string AsString()
    {
        return string.Join(";", Lines.Select(line => line.ChapterFilter + "|" + string.Join("", line.SearchLetters)));
    }
}

class CharacterCountingView : ReactComponent<CharacterCountingViewModel>
{
    #region Constructors
    

    protected override void constructor()
    {
        state = new CharacterCountingViewModel();
            var value = Context.Query[QueryKey.SearchQuery];
            if (value is not null)
            {
                state.SearchScript = SearchScript.ParseScript(value).AsReadibleString();
            }

        ClientTask.ListenEvent(ApplicationEventName.ArabicKeyboardPressed, ArabicKeyboardPressed);
        ClientTask.ListenEvent(ApplicationEventName.MushafOptionChanged, MushafOptionChanged);
    }
    
    #endregion

    
    public void MushafOptionChanged(MushafOption mushafOption)
    {
        state.MushafOption = mushafOption;
        Context
    }
    public void ArabicKeyboardPressed(string letter)
    {
        state.SearchScriptErrorMessage = null;
        state.ClickCount               = 0;
        state.SearchScript             = state.SearchScript.Trim() + " " + letter;
    }

    #region Public Methods
    protected override Element render()
    {
        var searchPanel = new divWithBorder
        {
            style = { paddingLeftRight = "15px", paddingBottom = "15px" },

            children =
            {
                new h4 { text = "Harf Arama" , style = { textAlign = "center"}},
                new VStack
                {
                    new VStack
                    {
                        new div { text = "Arama Komutu", style = { fontWeight = "500", fontSize = "0.9rem", marginBottom = "2px" } },
                        new InputTextarea { valueBind = () => state.SearchScript, rows = 2, autoResize = true},
                        new ErrorText{Text = state.SearchScriptErrorMessage}
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
            return new CalculatingComponent { searchPanel };
        }

        var mushafVerse           = new List<LetterColorizer>();

        var summaryInfoList = new List<SummaryInfo>();

        foreach (var (ChapterFilter, SearchLetters) in SearchScript.ParseScript(state.SearchScript).Lines)
        {
            var chapterFilter         = ChapterFilter;
            var searchLettersAsString = string.Join("", SearchLetters);

            var searchLetters = AnalyzeText(searchLettersAsString).Where(IsArabicLetter).GroupBy(x => x.ArabicLetterIndex).Select(grp => grp.FirstOrDefault()).Distinct().ToList();

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
    #endregion

    #region Methods
    void OnCaclculateClicked(string _)
    {
        state.SearchScriptErrorMessage = null;
        if (state.SearchScript.HasNoValue())
        {
            state.SearchScriptErrorMessage = "Arama Komutu doldurulmalıdır";
            return;
        }
        state.ClickCount++;

        if (state.IsBlocked == false)
        {
            state.IsBlocked = true;
            ClientTask.PushHistory("", $"/?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={SearchScript.ParseScript(state.SearchScript).AsString()}");
            ClientTask.GotoMethod(5, OnCaclculateClicked, _);
            return;
        }

        state.IsBlocked = false;
    }
    #endregion
}

// TODO: check and remove : QuranAnalyzer_WebUI_PanelHeaderTemplate