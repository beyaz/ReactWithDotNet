using System.Collections.Immutable;

namespace QuranAnalyzer.WebUI.Components;

class VerseListThatContainsLettersCalculatorModel
{
    public string SearchScript { get; set; }
    
    public string ErrorText { get; set; }
    public IReadOnlyList<LetterInfo> LetterInfoList { get; set; }
    public string Letters { get; set; }

    
}

class VerseListThatContainsLettersCalculator : ReactComponent<VerseListThatContainsLettersCalculatorModel>
{
    public bool ShowResults { get; set; }
    
    public string Letters,SearchScript;

    

    protected override void constructor()
    {
        state = new VerseListThatContainsLettersCalculatorModel
        {
            SearchScript = SearchScript,
            Letters = Letters
        };
    }

    protected override Element render()
    {
        return new FlexColumn
        {
            new FlexRow
            {
                Gap(3),
                
                new Label { Text = "Ayet Seç" },
                new TextInput
                {
                    TextInput.Bind(() => state.SearchScript),
                    FlexGrow(1)
                }
            },

             new FlexRow
             {
                 Gap(3),

                 new Label { Text = "Harfler" },
                 new TextInput
                 {
                     TextInput.Bind(() => state.Letters),
                     FlexGrow(1)
                 }
             },
             
            new FlexRow(JustifyContentSpaceEvenly)
            {
                new ErrorText { Text     = state.ErrorText },
                new ActionButton { Label = "Hesapla", OnClick = OnClick }
            },


            When(ShowResults, GetCalculationText)
        };
    }

    Element GetCalculationText()
    {
        var option    = new MushafOption();

        var letterInfoList  = Analyzer.AnalyzeText(state.Letters.Replace(" ", "")).Unwrap();
        var letterIndexList = letterInfoList.Select(x=>x.ArabicLetterIndex).ToImmutableList();
        var verseList       = VerseFilter.GetVerseList(state.SearchScript).Unwrap().Where(isContainsGivenLetters).ToList();


        var container = new FlexRow(Padding(10), AlignItemsCenter)
        {
           (strong)$"{verseList.Count} adet ayet bulundu."
        };

        container.children.AddRange(verseList.Select(verse => new LetterColorizer
        {
            VerseTextNodes          = verse.AnalyzedFullText,
            ChapterNumber           = verse.ChapterNumber.ToString(),
            VerseNumber             = verse.Index,
            LettersForColorizeNodes = letterInfoList,
            VerseText               = verse.TextWithBismillah,
            Verse                   = verse,
            MushafOption            = option
        }));
        

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
        
        ShowResults = true;
    }
    
}