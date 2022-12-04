namespace QuranAnalyzer.WebUI.Pages.MainPage;

class LeftMenu : ReactComponent
{
    static List<(string text, string pageId)> MenuItems = new List<(string text, string pageId)>
    {
        ("Anasayfa", PageId.MainPage),
        ("Teknolojide Veri İletimi", PageId.SecuringDataWithCurrentTechnology),
        ("Ön Bilgiler", PageId.PreInformation),
        ("Tanım", PageId.Definition),
        ("Başlangıç Harfleri", PageId.InitialLetters),
        ("Soru - Cevap", PageId.QuestionAnswerPage),
        ("İletişim", PageId.ContactPage)
    };

    public string SelectedPageId { get; set; }

    int? SelectedIndex => MenuItems.FindIndex(x => x.pageId == SelectedPageId);

    protected override Element render()
    {
        return new FlexColumn(Gap(20))
        {
            Children(MenuItems.Select((_, i) => createText(i))),
        };

        Element createText(int index)
        {
            var text = MenuItems[index].text;

            var isSelected = index == SelectedIndex;

            var textColor = isSelected ? "rgb(30 167 253)" : "rgb(173 164 164)";

            return new a(Href(GetPageLink(MenuItems[index].pageId)))
            {
                DisplayFlex, FlexDirectionRow, AlignItemsCenter, Gap(10),
                PositionRelative,
                TextDecorationNone,

                // C i r c l e
                new div
                {
                    wh(8),
                    Background(isSelected ? "rgb(30 167 253)" : "rgb(221 221 221)"),
                    BorderRadius("1em"),
                    Zindex(1)
                },

                // T e x t
                new FlexRowCentered
                {
                    Text(text),
                    Color(textColor),
                    When(!isSelected, Hover(Color("rgb(51 51 51)")))
                },

                // V e r t i c l e   L i n e
                new div
                {
                    PositionAbsolute,
                    MarginTop(-42),
                    Height(30),
                    Left(3.5),
                    When(index > 0, BorderLeft("1px solid rgb(238, 238, 238)"))
                }
            };
        }
    }
}



