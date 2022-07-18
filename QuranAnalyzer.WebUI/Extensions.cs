using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;



namespace QuranAnalyzer.WebUI;

static class Extensions
{
    public static bool HasNoValue(this string value) => string.IsNullOrWhiteSpace(value);

    public static Element BlockUI(Element content, bool isBlocked, string operationMessage)
    {
        return new BlockUI
        {
            blocked = isBlocked,
            template = new div
            {
                children =
                {
                    new i { className   = "pi pi-spin pi-spinner" },
                    new div { innerText = operationMessage, style = { color = "White", marginLeft = "5px"} }
                },
                style =
                {
                    display        = "flex",
                    justifyContent = "center",
                    alignItems     = "center"
                }
            },

            children =
            {
                content
            }
        };
    }
}