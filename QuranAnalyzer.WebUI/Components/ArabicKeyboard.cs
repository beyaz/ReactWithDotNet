using System.Net.Mime;
using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Components;

class ArabicKeyboard : ReactComponent
{
    public override Element render()
    {
        return new div
        {
            style = { display = "flex", flexWrap = "wrap", justifyContent = "center", direction = "rtl"},
            children = { 
                LetterToElement(ArabicLetter.Alif),
                LetterToElement(ArabicLetter.Baa),
                LetterToElement(ArabicLetter.Taa),
                LetterToElement(ArabicLetter.Thaa),
                LetterToElement(ArabicLetter.Jiim),
                LetterToElement(ArabicLetter.Haa),
                LetterToElement(ArabicLetter.Khaa),
                LetterToElement(ArabicLetter.Daal),
                LetterToElement(ArabicLetter.Dhaal),
                LetterToElement(ArabicLetter.Raa),
                LetterToElement(ArabicLetter.Zay),
                LetterToElement(ArabicLetter.Siin),
                LetterToElement(ArabicLetter.Shiin),
                LetterToElement(ArabicLetter.Saad),
                LetterToElement(ArabicLetter.Daad),
                LetterToElement(ArabicLetter.Taa_),
                LetterToElement(ArabicLetter.Zaa),
                LetterToElement(ArabicLetter.Ayn),
                LetterToElement(ArabicLetter.Ghayn),
                LetterToElement(ArabicLetter.Faa),
                LetterToElement(ArabicLetter.Qaaf),
                LetterToElement(ArabicLetter.Kaaf),
                LetterToElement(ArabicLetter.Laam),
                LetterToElement(ArabicLetter.Miim),
                LetterToElement(ArabicLetter.Nun),
                LetterToElement(ArabicLetter.Haa_),
                LetterToElement(ArabicLetter.Waaw),
                LetterToElement(ArabicLetter.Yaa)
                
            }
        };

        


    }

    static Element LetterToElement(string arabicLetter, string englistText = "Jiim")
    {
        return new ArabicKeyboardLetterView { ArabicLetter = arabicLetter, English = englistText };
       
    }
}

class ArabicKeyboardLetterViewModel
{
    public string ArabicLetter { get; set; }
    public string English { get; set; }
    public bool IsMouseEntered { get; set; }


}

class ArabicKeyboardLetterView: ReactComponent<ArabicKeyboardLetterViewModel>
{
    public string ArabicLetter { get; set; }
    public string English { get; set; }

    public ArabicKeyboardLetterView()
    {
        state = new ArabicKeyboardLetterViewModel();
        StateInitialized += () =>
        {
            state.ArabicLetter ??= ArabicLetter;
            state.English      ??= English;
        };
    }

    public override Element render()
    {
        return new div
        {

            style =
            {
                display       = "flex",
                flexDirection = "column",
                alignItems    = "center",
                margin        = "5px", 
                border  = state.IsMouseEntered ? "1px solid red" : "1px solid #dee2e6",


                borderRadius = "5px", 
                background = "rgb(248 249 251)"
            },
            children =
            {
                new div
                {
                    text  = state.ArabicLetter,
                    style = { fontSize = "45px", padding = "5px", }
                },
                new div(state.English),
                new i { className = "pi pi-copy", style = { fontSize = "20px", marginBottom = "3px"}}
            },

            onMouseEnter = _ => state.IsMouseEntered = true,
            onMouseLeave = _ => state.IsMouseEntered = false
        };
    }
}