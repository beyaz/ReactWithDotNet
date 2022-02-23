using ReactDotNet;
using ReactDotNet.PrimeReact;

namespace QuranAnalyzer.WebUI;

static class MixinForUI
{
    public static Element BlockUI(Element content, bool isBlocked, string operationMessage)
    {
        return new BlockUI
        {
            blocked = isBlocked,
            template = new div
            {
                new i { className = "pi pi-spin pi-spinner" },
                new div { Margin  = { Left = 5 }, style = { color = "White" }, text = operationMessage }
            }.MakeCenter(),

            children =
            {
                content
            }
        };
    }
}