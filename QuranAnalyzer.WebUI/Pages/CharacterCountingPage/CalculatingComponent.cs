using ReactWithDotNet.PrimeReact;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

class CalculatingComponent
{
    public static Element WithBlockUI(Element child)
    {
        return new BlockUI
        {
            blocked = true,
            template = new div
            {
                children =
                {
                    new i { className   = "pi pi-spin pi-spinner" },
                    new div { innerText = "Hesaplanıyor...", style = { color = "White", marginLeft = "5px" } }
                },
                style =
                {
                    display        = "flex",
                    justifyContent = "center",
                    alignItems     = "center"
                }
            },

            children = { child }
        };
    }
}