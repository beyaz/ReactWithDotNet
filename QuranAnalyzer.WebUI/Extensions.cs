using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;



namespace QuranAnalyzer.WebUI;

class CalculatingComponent : ReactComponent
{
    public override Element render()
    {
        return new BlockUI
        {
            blocked = true,
            template = new div
            {
                children =
                {
                    new i { className   = "pi pi-spin pi-spinner" },
                    new div { innerText = "Hesaplanıyor...", style = { color = "White", marginLeft = "5px"} }
                },
                style =
                {
                    display        = "flex",
                    justifyContent = "center",
                    alignItems     = "center"
                }
            },

            Children = children
        };
    }
}

static class Extensions
{
    public static string GetPageLink(string pageId) => "/wwwroot/index.html?page=" + pageId;
    
    public static bool HasNoValue(this string value) => string.IsNullOrWhiteSpace(value);

    public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);
}