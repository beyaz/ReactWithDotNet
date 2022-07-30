using ReactWithDotNet;
using static QuranAnalyzer.WebUI.Extensions;

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
            return new LeftMenuItem { Text = text, PageId = id };
        }
    }
}

class LeftMenuItemState
{
    public bool IsMouseEntered { get; set; }
    public string Text { get; set; }
    public string PageId { get; set; }
}

class LeftMenuItem: ReactComponent<LeftMenuItemState>
{
    public string Text { get; set; }
    public string PageId { get; set; }

    public LeftMenuItem()
    {
        state = new LeftMenuItemState();
        StateInitialized += () =>
        {
            state.PageId ??= PageId;
            state.Text ??= Text;
        };
    }
    
    public override Element render()
    {
        return new a
        {
            innerText = state.Text,
            href      = GetPageLink(state.PageId),
            style =
            {
                padding        = "10px",
                textDecoration = "none",
                color          = "Black",
                overflowWrap   = "break-word",
                borderBottom   = state.IsMouseEntered ? "1px solid red" : "1px solid #e9e9f2"
            },
            onMouseEnter = _=> state.IsMouseEntered = true,
            onMouseLeave = _ => state.IsMouseEntered = false
        };
    }
}