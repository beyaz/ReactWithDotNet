namespace QuranAnalyzer.WebUI.Components;

class ArabicKeyboard : ReactComponent
{
    public static Element Content => new div(DisplayFlex, FlexWrap, JustifyContentCenter, DirectionRtl)
    {
        LetterToElement(ArabicLetter.Alif, nameof(ArabicLetter.Alif)),
        LetterToElement(ArabicLetter.Baa, nameof(ArabicLetter.Baa)),
        LetterToElement(ArabicLetter.Taa, nameof(ArabicLetter.Taa)),
        LetterToElement(ArabicLetter.Thaa, nameof(ArabicLetter.Thaa)),
        LetterToElement(ArabicLetter.Jiim, nameof(ArabicLetter.Jiim)),
        LetterToElement(ArabicLetter.Haa, nameof(ArabicLetter.Haa)),
        LetterToElement(ArabicLetter.Khaa, nameof(ArabicLetter.Khaa)),
        LetterToElement(ArabicLetter.Daal, nameof(ArabicLetter.Daal)),
        LetterToElement(ArabicLetter.Dhaal, nameof(ArabicLetter.Dhaal)),
        LetterToElement(ArabicLetter.Raa, nameof(ArabicLetter.Raa)),
        LetterToElement(ArabicLetter.Zay, nameof(ArabicLetter.Zay)),
        LetterToElement(ArabicLetter.Siin, nameof(ArabicLetter.Siin)),
        LetterToElement(ArabicLetter.Shiin, nameof(ArabicLetter.Shiin)),
        LetterToElement(ArabicLetter.Saad, nameof(ArabicLetter.Saad)),
        LetterToElement(ArabicLetter.Daad, nameof(ArabicLetter.Daad)),
        LetterToElement(ArabicLetter.Taa_, nameof(ArabicLetter.Taa_)),
        LetterToElement(ArabicLetter.Zaa, nameof(ArabicLetter.Zaa)),
        LetterToElement(ArabicLetter.Ayn, nameof(ArabicLetter.Ayn)),
        LetterToElement(ArabicLetter.Ghayn, nameof(ArabicLetter.Ghayn)),
        LetterToElement(ArabicLetter.Faa, nameof(ArabicLetter.Faa)),
        LetterToElement(ArabicLetter.Qaaf, nameof(ArabicLetter.Qaaf)),
        LetterToElement(ArabicLetter.Kaaf, nameof(ArabicLetter.Kaaf)),
        LetterToElement(ArabicLetter.Laam, nameof(ArabicLetter.Laam)),
        LetterToElement(ArabicLetter.Miim, nameof(ArabicLetter.Miim)),
        LetterToElement(ArabicLetter.Nun, nameof(ArabicLetter.Nun)),
        LetterToElement(ArabicLetter.Haa_, nameof(ArabicLetter.Haa_)),
        LetterToElement(ArabicLetter.Waaw, nameof(ArabicLetter.Waaw)),
        LetterToElement(ArabicLetter.Yaa, nameof(ArabicLetter.Yaa))
    };

    protected override Element render()
    {
        return Content;
    }

    static Element LetterToElement(string arabicLetter, string englistText)
    {
        return new ArabicKeyboardLetterView { ArabicLetter = arabicLetter, English = englistText };
    }
}

class ArabicKeyboardLetterView : ReactComponent
{
    public string ArabicLetter { get; set; }
    public string English { get; set; }

    protected override Element render()
    {
        return new div
        {
            style =
            {
                DisplayFlex,
                FlexDirectionColumn,
                AlignItemsCenter,
                Margin(5),
                Border("1px solid #dee2e6"),
                BorderRadius(5),
                Background("rgb(248 249 251)"),
                CursorPointer,
                Hover(Border("1px solid #7daee7"))
            },
            children =
            {
                new div(PaddingLeftRight(5), FontSize(35), FontFamily_Lateef,  Hover(Color("#7daee7")))
                {
                    text = ArabicLetter
                },
                new div(MarginLeftRight(2), FontSize("0.8rem"))
                {
                    text = English
                }
            },

            onClick = OnArabicKeyboardLetterClicked
        };
    }
    
    [CacheThisMethod]
    void OnArabicKeyboardLetterClicked(MouseEvent e)
    {
        Client.ArabicKeyboardPressed(ArabicLetter);
    }
}