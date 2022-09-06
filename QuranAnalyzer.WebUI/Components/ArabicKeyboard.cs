namespace QuranAnalyzer.WebUI.Components;

class ArabicKeyboard : ReactComponent
{
    protected override Element render()
    {
        return new div
        {
            style = { display = "flex", flexWrap = "wrap", justifyContent = "center", direction = "rtl" },
            children =
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
            }
        };
    }

    static Element LetterToElement(string arabicLetter, string englistText)
    {
        return new ArabicKeyboardLetterView { ArabicLetter = arabicLetter, English = englistText };
    }
}


class ArabicKeyboardLetterView : ReactComponent, ISupportMouseEnter
{
    public string ArabicLetter { get; set; }
    public string English { get; set; }

    public bool IsMouseEntered { get; set; }

    protected override Element render()
    {
        return new div
        {
            style =
            {
                display       = "flex",
                flexDirection = "column",
                alignItems    = "center",
                margin        = "5px",
                border        = IsMouseEntered ? "1px solid #7daee7" : "1px solid #dee2e6",

                borderRadius = "5px",
                background   = "rgb(248 249 251)",
                cursor       = "pointer"
            },
            children =
            {
                new div
                {
                    text  = ArabicLetter,
                    style = { fontSize = "35px", paddingLeftRight = "5px", fontFamily = "Lateef, cursive" }
                },
                new div(English){style = { marginLeftRight = "2px", fontSize = "0.8rem"}}
            },

            onClick = _ => ClientTask.DispatchEvent(ApplicationEventName.ArabicKeyboardPressed, ArabicLetter)
        };
    }
}