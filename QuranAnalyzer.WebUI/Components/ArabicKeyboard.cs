namespace QuranAnalyzer.WebUI.Components;

class ArabicKeyboard : ReactPureComponent
{
    public static Element Content => new div(DisplayFlex, FlexWrap, JustifyContentCenter, DirectionRtl)
    {
        LetterToElement(ArabicLetter.Alif,  ArabicLetterTurkishPronunciation.Alif),
        LetterToElement(ArabicLetter.Baa,   ArabicLetterTurkishPronunciation.Baa),
        LetterToElement(ArabicLetter.Taa,   ArabicLetterTurkishPronunciation.Taa),
        LetterToElement(ArabicLetter.Thaa,  ArabicLetterTurkishPronunciation.Thaa),
        LetterToElement(ArabicLetter.Jiim,  ArabicLetterTurkishPronunciation.Jiim),
        LetterToElement(ArabicLetter.Haa,   ArabicLetterTurkishPronunciation.Haa),
        LetterToElement(ArabicLetter.Khaa,  ArabicLetterTurkishPronunciation.Khaa),
        LetterToElement(ArabicLetter.Daal,  ArabicLetterTurkishPronunciation.Daal),
        LetterToElement(ArabicLetter.Dhaal, ArabicLetterTurkishPronunciation.Dhaal),
        LetterToElement(ArabicLetter.Raa,   ArabicLetterTurkishPronunciation.Raa),
        LetterToElement(ArabicLetter.Zay,   ArabicLetterTurkishPronunciation.Zay),
        LetterToElement(ArabicLetter.Siin,  ArabicLetterTurkishPronunciation.Siin),
        LetterToElement(ArabicLetter.Shiin, ArabicLetterTurkishPronunciation.Shiin),
        LetterToElement(ArabicLetter.Saad,  ArabicLetterTurkishPronunciation.Saad),
        LetterToElement(ArabicLetter.Daad,  ArabicLetterTurkishPronunciation.Daad),
        LetterToElement(ArabicLetter.Taa_,  ArabicLetterTurkishPronunciation.Taa_),
        LetterToElement(ArabicLetter.Zaa,   ArabicLetterTurkishPronunciation.Zaa),
        LetterToElement(ArabicLetter.Ayn,   ArabicLetterTurkishPronunciation.Ayn),
        LetterToElement(ArabicLetter.Ghayn, ArabicLetterTurkishPronunciation.Ghayn),
        LetterToElement(ArabicLetter.Faa,   ArabicLetterTurkishPronunciation.Faa),
        LetterToElement(ArabicLetter.Qaaf,  ArabicLetterTurkishPronunciation.Qaaf),
        LetterToElement(ArabicLetter.Kaaf,  ArabicLetterTurkishPronunciation.Kaaf),
        LetterToElement(ArabicLetter.Laam,  ArabicLetterTurkishPronunciation.Laam),
        LetterToElement(ArabicLetter.Miim,  ArabicLetterTurkishPronunciation.Miim),
        LetterToElement(ArabicLetter.Nun,   ArabicLetterTurkishPronunciation.Nun),
        LetterToElement(ArabicLetter.Haa_,  ArabicLetterTurkishPronunciation.Haa_),
        LetterToElement(ArabicLetter.Waaw,  ArabicLetterTurkishPronunciation.Waaw),
        LetterToElement(ArabicLetter.Yaa,   ArabicLetterTurkishPronunciation.Yaa)
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

static class ArabicKeyboardEvents
{
    public static void ArabicKeyboardPressed(this Client client, string arabicLetter)
    {
        client.DispatchEvent(nameof(ArabicKeyboardPressed), arabicLetter);
    }

    public static void OnArabicKeyboardPressed(this Client client, Action<string> handlerAction)
    {
        client.ListenEvent(ArabicKeyboardPressed, handlerAction);
    }
}