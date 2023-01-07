using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

[Serializable]
public class CharacterCountingViewModel
{
    public int ClickCount { get; set; }

    public bool IsBlocked { get; set; }

    public MushafOption MushafOption { get; set; } = new();

    public string SearchScript { get; set; }

    public string SearchScriptErrorMessage { get; set; }
}

class CharacterCountingView : ReactComponent<CharacterCountingViewModel>
{
    protected override void componentDidMount()
    {
        Client.OnArabicKeyboardPressed(ArabicKeyboardPressed);
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
    }

    protected override Element render()
    {
        IEnumerable<Element> searchPanel ()=> new []
        {
            When(state.IsBlocked, () => new div { PositionAbsolute, LeftRight(0), TopBottom(0), BackgroundColor("rgba(0, 0, 0, 0.3)"), Zindex(3) }),
            When(state.IsBlocked, () => new FlexRowCentered
            {
                PositionAbsolute, FontWeight700, LeftRight(0), TopBottom(0), Zindex(4),
                Children(new LoadingIcon { wh(17), mr(5) }, "Lütfen bekleyiniz...")
            }),
            new h4 { text = "Harf Arama", style = { TextAlignCenter } },
            new FlexColumn
            {
                new FlexColumn
                {
                    new div { text = "Arama Komutu", style = { fontWeight = "500", fontSize = "0.9rem", marginBottom = "2px" } },

                    new TextArea { ValueBind = () => state.SearchScript },

                    new ErrorText { Text = state.SearchScriptErrorMessage }
                },

                Space(3),

                new CharacterCountingOptionView { MushafOption = state.MushafOption, MushafOptionChanged = MushafOptionChanged },

                Space(20),

                new FlexRow(JustifyContentFlexEnd)
                {
                    new ActionButton { Label = "Ara", OnClick = OnCaclculateClicked }
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

        var resultVerseList = new List<LetterColorizer>();

        var summaryInfoList = new List<SummaryInfo>();

        foreach (var (chapterFilter, searchLetters) in searchScript.Lines)
        {
            foreach (var summaryInfo in searchLetters.AsListOf(x => new SummaryInfo
                     {
                         Count = VerseFilter.GetVerseList(chapterFilter).Then(verses => QuranAnalyzerMixin.GetCountOfLetter(verses, x.ArabicLetterIndex, state.MushafOption)).Unwrap(),
                         Name  = x.MatchedLetter
                     }))
            {
                if (summaryInfoList.Any(x => x.Name == summaryInfo.Name))
                {
                    summaryInfoList.First(x => x.Name == summaryInfo.Name).Count += summaryInfo.Count;
                    continue;
                }

                summaryInfoList.Add(summaryInfo);
            }

            foreach (var verse in VerseFilter.GetVerseList(chapterFilter).Unwrap())
            {
                if (verse.TextWithBismillahAnalyzed.Any(x => searchLetters.Any(l => l.ArabicLetterIndex == x.ArabicLetterIndex)))
                {
                    var letterColorizer = new LetterColorizer
                    {
                        VerseTextNodes          = verse.TextWithBismillahAnalyzed,
                        ChapterNumber           = verse.ChapterNumber.ToString(),
                        VerseNumber             = verse.Index,
                        LettersForColorizeNodes = searchLetters,
                        VerseText               = verse.TextWithBismillah,
                        Verse                   = verse,
                        MushafOption            = state.MushafOption
                    };

                    resultVerseList.Add(letterColorizer);
                }
            }
        }

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

    void MushafOptionChanged(MushafOption mushafOption)
    {
        state.ClickCount   = 0;
        state.MushafOption = mushafOption;
        Context.Set(ContextKey.MushafOptionKey, state.MushafOption);
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
            Client.PushHistory("", $"/?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={script.AsString()}");
            Client.GotoMethod(OnCaclculateClicked);
            return;
        }

        state.IsBlocked = false;
    }
}