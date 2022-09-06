using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

class CharacterCountingOptionView: ReactComponent
{
    public bool? ShowKeyborad { get; set; }
    
    public bool? ShowMushafOptions { get; set; }

    void KeyboardClicked(string _)
    {
        if (ShowKeyborad == null)
        {
            ShowKeyborad = true;
        }
    }
    protected override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "column" },
            children =
            {
                // Header
                new div
                {
                    style =
                    {
                        display       = "flex",
                        flexDirection = "row",
                        color         = "rgba(0, 0, 0, 0.6)",
                        textAlign     = "center",
                        cursor        = "pointer"
                    },
                    children =
                    {
                        new div("Arapça Klavye"){style = { padding = "10px", color = "#1976d2"}, onClick =KeyboardClicked},
                        new HSpace(10),
                        new div("Mushaf Ayarları"){style = { padding = "10px"}},
                    }
                },

                // content
                new div
                {
                    children= 
                    {
                        ShowKeyborad == true ? ArabicKeyboard.Content: ShowMushafOptions == true ? new div("MushafOptioncontent") : null
                    }
                }
            }
        };
    }
}