namespace QuranAnalyzer.WebUI.Components;

class NumericValueCalculatorModel
{
    public string ErrorText { get; set; }
    public IReadOnlyList<LetterInfo> LetterInfoList { get; set; }
    public string Letters { get; set; }
}

class NumericValueCalculator : ReactComponent<NumericValueCalculatorModel>
{
    public string Letters;

    protected override void constructor()
    {
        state = new NumericValueCalculatorModel
        {
            Letters = Letters
        };
    }

    protected override Element render()
    {
        return new FlexColumn
        {
            new Label { Text = "Harfler" },
            new FlexRow(Gap(3))
            {
                MediaQuery("(max-width:350px)",FlexDirectionColumn,AlignItemsStretch),
                new TextInput
                {
                    TextInput.Bind(() => state.Letters),
                    FlexGrow(1)
                },
                new ActionButton { Label = "Hesapla", OnClick = OnClick }
            },

            new ErrorText { Text = state.ErrorText },

            When(state.LetterInfoList?.Count > 0, () => GetCalculationText(state.LetterInfoList.Select(x => x.ToString()).ToArray()))
        };
    }

    Element GetCalculationText(string[] arabicLetters)
    {
        var totalView = new FlexRowCentered();

        var container = new FlexRow(FlexWrap, Padding(10), AlignItemsCenter)
        {
            totalView, new span { Text("="), MarginLeftRight(5) }
        };

        var total = 0;

        for (var i = 0; i < arabicLetters.Length; i++)
        {
            if (i > 0)
            {
                container.Add((span)" + " + MarginLeftRight(5));
            }

            var arabicLetter  = arabicLetters[i];
            var pronunciation = GetPronunciationOfArabicLetter(arabicLetter);
            var numericValue  = ArabicLetterNumericValue.GetNumericalValue(ArabicLetter.AllArabicLetters.GetIndex(arabicLetter).Value);

            var item = new ArabicLetterWithNumericValue
            {
                ArabicLetter               = arabicLetter,
                Pronunciation              = pronunciation,
                NumericValueOfArabicLetter = numericValue
            };
            container.Add(item.BuildUi());

            total += numericValue;
        }

        totalView.text = total.ToString();

        return container;
    }

    void OnClick()
    {
        state.ErrorText = null;

        if (state.Letters.HasNoValue())
        {
            state.ErrorText = "En az bir tane Arapça karakter girilmelidir.";
            return;
        }

        var letters = Analyzer.AnalyzeText(state.Letters.Replace(" ", ""));
       

        state.LetterInfoList = letters;
    }

    class ArabicLetterWithNumericValue
    {
        public string ArabicLetter { get; set; }
        public int NumericValueOfArabicLetter { get; set; }
        public string Pronunciation { get; set; }

        public Element BuildUi()
        {
            return new FlexColumn
            {
                AlignItemsCenter,
                Margin(5),
                Border($"1px solid {BorderColor}"),
                BorderRadiusForPanels,

                new div(PaddingLeftRight(5), FontSize(15), FontFamily_Lateef)
                {
                    ArabicLetter
                },
                new div(MarginLeftRight(2), FontSize("0.6rem"))
                {
                    Pronunciation, " - ", (small)NumericValueOfArabicLetter.ToString()
                }
            };
        }
    }
}



