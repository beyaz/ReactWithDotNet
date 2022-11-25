namespace QuranAnalyzer.WebUI.Pages.MainPage;

class LeftMenuContent: ReactComponent
{
    protected override Element render()
    {
        return new FlexColumn(AlignItemsCenter,Width("100%"), Height("100%"), TextAlignCenter, Gap(20))
        {
            toSidebarMenuItem("1 - Anasayfa",PageId.MainPage),

            toSidebarMenuItem("2 - Günümüz Teknolojisinde Veri Nasıl Korunur",PageId.SecuringDataWithCurrentTechnology),

            toSidebarMenuItem("3 - Ön Bilgiler",PageId.PreInformation),

            toSidebarMenuItem("4 - Tanım",PageId.Definition),

            toSidebarMenuItem("5 - Başlangıç Harfleri",PageId.InitialLetters),

            toSidebarMenuItem("6 - Soru - Cevap",PageId.QuestionAnswerPage),

            toSidebarMenuItem("7 - İletişim",PageId.ContactPage)
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

class LeftMenuItem: ReactComponent<LeftMenuItemState>
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
                borderBottom   = "1px solid #e9e9f2",
                hover =
                {
                    color        = "#4e6579",
                    borderBottom = "1px solid red"
                }
            }
        };

        return link;
    }
}