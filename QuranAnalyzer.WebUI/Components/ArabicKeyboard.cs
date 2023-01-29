namespace QuranAnalyzer.WebUI.Components;

class ArabicKeyboard : ReactComponent
{
    public static Element Content => new div(DisplayFlex, FlexWrap, JustifyContentCenter, DirectionRtl)
    {
        LetterToElement(ArabicLetter.Alif, ArabicLetterTurkishPronunciation.Alif),
        LetterToElement(ArabicLetter.Baa, ArabicLetterTurkishPronunciation.Baa),
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

static class ArabicLetterTurkishPronunciation
{
    // @formatter:off
    public const string Alif = "elif";
    public const string Baa = "be";
    public const string Taa = "te";
    public const string Thaa = "se";
    public const string Jiim = "cim";
    public const string Haa = "hâ";
    public const string Khaa = "hı";
    public const string Daal = "dal";
    public const string Dhaal = "zel";
    public const string Raa = "ra";
    public const string Zay = "ze";
    public const string Siin = "sin";
    public const string Shiin = "şın";
    public const string Saad = "sad";
    public const string Daad = "dad";
    public const string Taa_ = "tı";
    public const string Zaa = "zı";
    public const string Ayn = "ayn";
    public const string Ghayn = "ghayn";
    public const string Faa = "fe";
    public const string Qaaf = "kaf";
    public const string Kaaf = "kef";
    public const string Laam = "lam";
    public const string Miim = "mim";
    public const string Nun = "nun";
    public const string Haa_ = "he";
    public const string Waaw = "vav";
    public const string Yaa = "yâ";
    // @formatter:on
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
                Border($"1px solid {BorderColor}"),
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