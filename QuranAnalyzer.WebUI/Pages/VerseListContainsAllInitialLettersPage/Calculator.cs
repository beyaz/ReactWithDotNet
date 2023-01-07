using System.Collections.Immutable;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet.Libraries.react_free_scrollbar;

namespace QuranAnalyzer.WebUI.Pages.VerseListContainsAllInitialLettersPage;

class CalculatorModel
{
    public string ErrorText { get; set; }
    public bool IsProcessing { get; set; }
    public string Letters { get; set; }
    public string SearchScript { get; set; }
    public bool ShowResults { get; set; }
}

class Calculator : ReactComponent<CalculatorModel>
{
    public string Letters, SearchScript;

    public bool ShowNumbers { get; set; }

    public bool ShowVerseList { get; set; }

    protected override void constructor()
    {
        state = new CalculatorModel
        {
            SearchScript = SearchScript,
            Letters      = Letters
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
                new ActionButton { Label = "Hesapla", OnClick = OnClick, IsProcessing = state.IsProcessing }
            },

            When(state.ShowResults, GetCalculationText)
        };
    }

    void Calculate()
    {
        state.ShowResults  = true;
        state.IsProcessing = false;
    }

    Element GetCalculationText()
    {
        var option = new MushafOption();

        var letterInfoList  = Analyzer.AnalyzeText(state.Letters.Replace(" ", ""));
        var letterIndexList = letterInfoList.Select(x => x.ArabicLetterIndex).ToImmutableList();
        var verseList       = VerseFilter.GetVerseList(state.SearchScript).Unwrap().Where(isContainsGivenLetters).ToList();

        if (ShowNumbers)
        {
            return showNumbers();
        }

        if (ShowVerseList)
        {
            return showVerseList();
        }

        throw new InvalidOperationException();

        Element showVerseList()
        {
            return new FlexColumn(Padding(10), AlignItemsCenter)
            {
                (strong)$"{verseList.Count} adet ayet bulundu.",

                new FreeScrollBar
                {
                    Height(300), WidthMaximized, ComponentBorder, BorderRadius(5),
                    Children(verseList.Select(verse => new LetterColorizer
                    {
                        VerseTextNodes          = verse.TextWithBismillahAnalyzed,
                        ChapterNumber           = verse.ChapterNumber.ToString(),
                        VerseNumber             = verse.Index,
                        LettersForColorizeNodes = letterInfoList,
                        VerseText               = verse.TextWithBismillah,
                        Verse                   = verse,
                        MushafOption            = option
                    }))
                }
            };
        }

        bool isContainsGivenLetters(Verse verse)
        {
            foreach (var arabicLetterIndex in letterIndexList)
            {
                if (QuranAnalyzerMixin.GetCountOfLetterInVerse(verse, arabicLetterIndex, option,true) <= 0)
                {
                    return false;
                }
            }

            return true;
        }

        Element showNumbers()
        {
            var total = 0;

            var items = new List<Element>();

            var currentChapter = -1;

            foreach (var verse in verseList)
            {
                if (items.Count > 0)
                {
                    items.Add(new div("+") { MarginLeftRight(3) });
                }

                if (currentChapter == verse.ChapterNumber)
                {
                    items.Add(verse.Index);
                    total += verse.IndexAsNumber;
                    continue;
                }

                currentChapter = verse.ChapterNumber;

                items.Add(new span(currentChapter.ToString()) { Color("red") });
                items.Add(new div("+") { MarginLeftRight(3) });
                items.Add(verse.Index);

                total += verse.ChapterNumber;
                total += verse.IndexAsNumber;
            }

            items.Insert(0, total.ToString());
            items.Insert(1, new div("=") { MarginLeftRight(3) });

            return new FlexRow(FlexWrap)
            {
                Children(items)
            };
        }
    }

    void OnClick()
    {
        state.ShowResults = false;
        state.ErrorText   = null;

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
        

        if (letters.Count == 0)
        {
            state.ErrorText = "En az bir tane Arapça karakter girilmelidir.";
            return;
        }

        state.IsProcessing = true;
        Client.GotoMethod(Calculate);
    }
}