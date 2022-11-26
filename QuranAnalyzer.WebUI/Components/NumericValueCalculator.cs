namespace QuranAnalyzer.WebUI.Components;

class NumericValueCalculatorModel
{
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
            new Label{Text = "Harfler"},
            new FlexRow
            {
                new TextInput
                {
                    TextInput.Bind( ()=>state.Letters),
                    FlexGrow(9)
                },
                new ActionButton{ Label = "Hesapla", OnClick = OnClick}
            }
        };
        
        
    }

    static Element GetCalculationText(string[] arabicLetters)
    {
        var container = new FlexRow(FlexWrap, Padding(10))
        {

        };

        for (var i = 0; i < arabicLetters.Length; i++)
        {
            if (i>0)
            {
                container.Add((span)" + "+MarginLeftRight(5));
            }
            
            var arabicLetter = arabicLetters[i];
            var numericValue = ArabicLetterNumericValue.GetNumericalValue(ArabicLetter.AllArabicLetters.GetIndex(arabicLetter).Value);

            container.Add(new FlexRow { new div(Text(arabicLetter), FontFamily_Lateef), (small)$"({numericValue})" });
        }

        return container;
    }
    
    void OnClick()
    {
        
    }
}