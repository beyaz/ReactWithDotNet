using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Pages.MainPage;

class LeftMenuContent: ReactComponent
{
    public override Element render()
    {
        return new div
        {
            children =
            {
                new VSpace(20),
                toSidebarMenuItem("1 - Anasayfa",PageId.MainPage),
                new VSpace(20),
                toSidebarMenuItem("2 - Günümüz Teknolojisinde Veri Nasıl Korunur",PageId.SecuringDataWithCurrentTechnology),
                new VSpace(20),
                toSidebarMenuItem("3 - Ön Bilgiler",PageId.MainPage),
                new VSpace(20),
                toSidebarMenuItem("4 - Başlangıç Harfleri",PageId.InitialLetters),
                new VSpace(20),
                toSidebarMenuItem("5 - Soru - Cevap",PageId.MainPage),
                new VSpace(20),
                toSidebarMenuItem("6 - İletişim",PageId.ContactPage),
            },
            style =
            {
                width_height  = "100%",
                display       = "flex",
                flexDirection = "column",
                alignItems    = "center",
                textAlign     = "center"
            }
        };

        static Element toSidebarMenuItem(string text, string id)
        {
            return new a
            {
                className = "q-sidebarlink",
                innerText = text,
                href      = Extensions.GetPageLink(id),
                style     = { padding = "10px", textDecoration = "none", color = "Black", overflowWrap = "break-word" }
            };
        }
    }
}