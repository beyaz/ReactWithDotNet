namespace QuranAnalyzer.WebUI.Pages.MainPage;

class LeftMenu : ReactComponent
{
    public static IReadOnlyList<(string text, string pageId)> MenuItems = new List<(string text, string pageId)>
    {
        ("Anasayfa",PageId.MainPage),
        ("Günümüz Teknolojisinde Veri Nasıl Korunur",PageId.SecuringDataWithCurrentTechnology),
        ("Ön Bilgiler",PageId.PreInformation),
        ("Tanım",PageId.Definition),
        ("Başlangıç Harfleri",PageId.InitialLetters),
        ("Soru - Cevap",PageId.QuestionAnswerPage),
        ("İletişim",PageId.ContactPage)
    };

    public int? SelectedIndex { get; set; }
    
    protected override Element render()
    {
        return new div
        {
            new FlexColumn
            {
                Children(MenuItems.Select((_, i) => createText(i))),
               
            }
        };

        Element createText(int index)
        {
            string text       = MenuItems[index].text;
            bool   isSelected = index == SelectedIndex;
                              
            var textColor = isSelected ? "#1EA7FD" : "#666666";


            return new a(DisplayFlex,FlexDirectionRow,Gap(10), AlignItemsCenter, MarginTopBottom(5), Id(index),PositionRelative, Href(MenuItems[index].pageId))
            {
                TextDecorationNone,
                Color("inherit"),
                Color("rgb(68, 68, 68)"),
                Hover(Color("rgb(173 164 164)")),
               new div
               {
                   wh(8),
                   Background("rgb(221 221 221)"),
                   BorderRadius("1em"),
                   Zindex(1)
               },
                new FlexRowCentered
                {
                    Text(text),
                    //Background("#EEF2FF"),
                    //BorderRadius("50%"),
                    // wh(30),
                    //Color(textColor)
                },

                new div
                {
                    PositionAbsolute,
                    MarginTop(-30),
                    Height(18),
                    Left(3.5),
                    BorderLeft("1px solid rgb(238, 238, 238)"),
                }
            };
        }
    }
}

class LeftMenuContent_old : ReactComponent
{
 

    protected override Element render()
    {
        return new FlexColumn(AlignItemsCenter, Width("100%"), Height("100%"), TextAlignCenter, Gap(20))
        {
            toSidebarMenuItem("1 - Anasayfa", PageId.MainPage),

            toSidebarMenuItem("2 - Günümüz Teknolojisinde Veri Nasıl Korunur", PageId.SecuringDataWithCurrentTechnology),

            toSidebarMenuItem("3 - Ön Bilgiler", PageId.PreInformation),

            toSidebarMenuItem("4 - Tanım", PageId.Definition),

            toSidebarMenuItem("5 - Başlangıç Harfleri", PageId.InitialLetters),

            toSidebarMenuItem("6 - Soru - Cevap", PageId.QuestionAnswerPage),

            toSidebarMenuItem("7 - İletişim", PageId.ContactPage)
        };

        static Element toSidebarMenuItem(string text, string id)
        {
            return new LeftMenuItem { Text = text, PageId = id };
        }
    }

    class LeftMenuItem : ReactComponent
    {
        public string PageId { get; set; }
        public string Text { get; set; }

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
                    color          = "black",
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
}


