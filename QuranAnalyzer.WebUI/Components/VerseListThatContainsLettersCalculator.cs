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
    
    public string Letters;

    

    protected override void constructor()
    {
        state = new VerseListThatContainsLettersCalculatorModel
        {
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
        var totalView = new FlexRowCentered();

        var container = new FlexRow(FlexWrap, Padding(10), AlignItemsCenter)
        {
            totalView, new span { Text("="), MarginLeftRight(5) }
        };

        Analyzer.AnalyzeText(state.Letters.Replace(" ", ""));
        var verseList = VerseFilter.GetVerseList(state.SearchScript).Unwrap();

        //var allInitialLetters = new[] { Alif, Laam, Miim, Saad, Raa, Kaaf, Haa, Yaa, Ayn, Taa_, Siin, Haa_, Qaaf, Nun };
        
        //bool isContainsAllInitialLetters(Verse verse)
        //{


        //    foreach (var initialLetterIndex in allInitialLetters)
        //    {
        //        if ( QuranAnalyzerMixin.GetCountOfLetterInVerse(verse, initialLetterIndex, option) <= 0)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //var total = 0;

        //for (var i = 0; i < arabicLetters.Length; i++)
        //{
        //    if (i > 0)
        //    {
        //        container.Add((span)" + " + MarginLeftRight(5));
        //    }

        //    var arabicLetter  = arabicLetters[i];
        //    var pronunciation = GetPronunciationOfArabicLetter(arabicLetter);
        //    var numericValue  = ArabicLetterNumericValue.GetNumericalValue(ArabicLetter.AllArabicLetters.GetIndex(arabicLetter).Value);

        //    var item = new ArabicLetterWithNumericValue
        //    {
        //        ArabicLetter               = arabicLetter,
        //        Pronunciation              = pronunciation,
        //        NumericValueOfArabicLetter = numericValue
        //    };
        //    container.Add(item.BuildUi());

        //    total += numericValue;
        //}

        //totalView.text = total.ToString();

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

        var (letters, exception) = Analyzer.AnalyzeText(state.Letters.Replace(" ", ""));
        if (exception is not null)
        {
            state.ErrorText = exception;
            return;
        }

        if (letters.Count == 0)
        {
            state.ErrorText = "En az bir tane Arapça karakter girilmelidir.";
            return;
        }
        
        ShowResults = true;
    }
    
}