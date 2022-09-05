using ReactWithDotNet;
using static QuranAnalyzer.WebUI.Extensions;

namespace QuranAnalyzer.WebUI.Pages.MainPage;

class LeftMenuContent: ReactComponent
{
    protected override Element render()
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
            return new LeftMenuItem { Text = text, PageId = id };
        }
    }
}

class LeftMenuItemState
{
}

class LeftMenuItem: ReactComponent<LeftMenuItemState>,ISupportMouseEnter
{
    public string Text { get; set; }
    public string PageId { get; set; }

    protected override Element render()
    {
        var link = new a
        {
            innerText = Text,
            href      = GetPageLink(PageId),
            style =
            {
                padding        = "10px",
                textDecoration = "none",
                color          =  "black",
                overflowWrap   = "break-word",
                borderBottom   = "1px solid #e9e9f2"
            }
        };

        if (IsMouseEntered)
        {
            link.style.color        = "#4e6579";
            link.style.borderBottom = "1px solid red";
        }

        return link;
    }

    public bool IsMouseEntered { get; set; }
}