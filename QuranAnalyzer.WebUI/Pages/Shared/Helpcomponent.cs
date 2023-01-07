namespace QuranAnalyzer.WebUI.Pages.Shared;

class Helpcomponent : ReactComponent
{
    public bool IsHelpVisible { get; set; }

    protected override Element render()
    {
        if (IsHelpVisible)
        {
            return new FlexColumn(Gap(20))
            {
                Title() + MarginTop(11),
                HelpDetail
            };
        }

        return Title();
    }

    static Element HelpDetail()
    {
        return new div(TextAlignCenter)
        {
            new table
            {
                new tbody
                {
                    new tr
                    {
                        new th { "Komut" } + FontWeight500, new th { "Açıklama" } + FontWeight500
                    },
                    new tr { Height(15) },
                    new tr
                    {
                        commandText("* | الله"),
                        description("(Tüm Kuran boyunca geçen ", (b)"الله", " kelimesini aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText("2:* | الله"),
                        description("(2. surenin tamamında geçen ", (b)"الله", " kelimesini aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText("2:*, 3:*, 7:5-40 | الله"),
                        description("(2. surenin tamamında, 3. surenin tamamında ve 7. surenin 5 ila 40. ayetler arasında geçen ", (b)"الله", " kelimesini aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText("2:*, -2:4, -2:8 | الله"),
                        description("(2. surenin tamamında(4. ve 8. ayetler hariç), geçen ", (b)"الله", " kelimesini aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText("*, -9:128, -9:129 | الله"),
                        description("(Tüm mushaf boyunca (9:128 ve 9:129 hariç), geçen ", (b)"الله", " kelimesini aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText("2:17 --> 5:4 | الله"),
                        description("(2. surenin 17. ayeti ile 5. surenin 4. ayeti arasında geçen ", (b)"الله", " kelimesini aratır)")
                    }
                }
            }
        };

        static Element commandText(string text)
        {
            return new td { (b)text } + Width(200) + ComponentBorder + BorderRadius(5);
        }

        static Element description(params Element[] children)
        {
            return new td { children } + Width(400);
        }
    }

    Element Title()
    {
        return new FlexRow(AlignItemsCenter, Gap(5), CursorPointer, OnClick(_ => IsHelpVisible = !IsHelpVisible))
        {
            new img
            {
                wh(20),
                IsHelpVisible ? SrcArrowUp : SrcArrowDown
            },
            "Örnek arama komutları"
        };
    }
}