using System.Collections.Immutable;
using ReactWithDotNet.Libraries.react_free_scrollbar;

namespace QuranAnalyzer.WebUI.Components;

class VerseListThatContainsLettersCalculatorModel
{
    public string ErrorText { get; set; }
    public IReadOnlyList<LetterInfo> LetterInfoList { get; set; }
    public string Letters { get; set; }
    public string SearchScript { get; set; }
}

class VerseListThatContainsLettersCalculator : ReactComponent<VerseListThatContainsLettersCalculatorModel>
{
    public string Letters, SearchScript;

    public bool IsProcessing { get; set; }
    public bool ShowResults { get; set; }

    protected override void constructor()
    {
        state = new VerseListThatContainsLettersCalculatorModel
        {
            SearchScript = SearchScript,
            Letters      = Letters
        };
    }

    protected override Element render()
    {
        return new FlexColumn(Gap(9))
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

                new Label { Text = "Harfler" },
                new TextInput
                {
                    TextInput.Bind(() => state.Letters),
                    FlexGrow(1)
                }
            },

            new FlexRow(AlignItemsCenter, state.ErrorText.HasValue() ? JustifyContentSpaceBetween : JustifyContentFlexEnd)
            {
                new ErrorText { Text     = state.ErrorText },
                new ActionButton { Label = "Hesapla", OnClick = OnClick, IsProcessing = IsProcessing }
            },

            When(ShowResults, GetCalculationText)
        };
    }

    void Calculate()
    {
        ShowResults  = true;
        IsProcessing = false;
    }

    Element GetCalculationText()
    {
        var option = new MushafOption();

        var letterInfoList  = Analyzer.AnalyzeText(state.Letters.Replace(" ", "")).Unwrap();
        var letterIndexList = letterInfoList.Select(x => x.ArabicLetterIndex).ToImmutableList();
        var verseList       = VerseFilter.GetVerseList(state.SearchScript).Unwrap().Where(isContainsGivenLetters).ToList();

        var container = new FlexColumn(Padding(10), AlignItemsCenter)
        {
            (strong)$"{verseList.Count} adet ayet bulundu.",

            new FreeScrollBar
            {
                Height(300), Width("100%"), ComponentBorder, BorderRadius(5),
                Children(verseList.Select(verse => new LetterColorizer
                {
                    VerseTextNodes          = verse.AnalyzedFullText,
                    ChapterNumber           = verse.ChapterNumber.ToString(),
                    VerseNumber             = verse.Index,
                    LettersForColorizeNodes = letterInfoList,
                    VerseText               = verse.TextWithBismillah,
                    Verse                   = verse,
                    MushafOption            = option
                }))
            }
        };

        bool isContainsGivenLetters(Verse verse)
        {
            foreach (var arabicLetterIndex in letterIndexList)
            {
                if (QuranAnalyzerMixin.GetCountOfLetterInVerse(verse, arabicLetterIndex, option) <= 0)
                {
                    return false;
                }
            }

            return true;
        }

        return container;
    }

    void OnClick()
    {
        ShowResults     = false;
        state.ErrorText = null;

        if (state.SearchScript.HasNoValue())
        {
            state.ErrorText = "Ayet seçmelisiniz. Tüm Kuran boyunca arama yapmak için * yazabilirsiniz.";
            return;
        }

        var verseList = VerseFilter.GetVerseList(state.SearchScript);
        if (verseList.IsFail)
        {
            state.ErrorText = verseList.FailMessage;
            return;
        }

        if (state.Letters.HasNoValue())
        {
            state.ErrorText = "En az bir tane Arapça karakter girilmelidir.";
            return;
        }

        var letters = Analyzer.AnalyzeText(state.Letters.Replace(" ", ""));
        if (letters.IsFail)
        {
            state.ErrorText = letters.FailMessage;
            return;
        }

        if (letters.Value.Count == 0)
        {
            state.ErrorText = "En az bir tane Arapça karakter girilmelidir.";
            return;
        }

        IsProcessing = true;
        Client.GotoMethod(Calculate);
    }
}