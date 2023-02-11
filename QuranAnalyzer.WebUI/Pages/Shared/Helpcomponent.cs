namespace QuranAnalyzer.WebUI.Pages.Shared;

class Helpcomponent : ReactComponent
{
    public bool IsHelpVisible { get; set; }

    public bool ShowHelpMessageForLetterSearch { get; set; }

    string DescriptionPart
    {
        get
        {
            if (ShowHelpMessageForLetterSearch)
            {
                return "harflerini";
            }

            return "kelimesini";
        }
    }

    string SearchItem
    {
        get
        {
            if (ShowHelpMessageForLetterSearch)
            {
                return ArabicLetter.Qaaf;
            }

            return "الله";
        }
    }

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

    Element HelpDetail()
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
                        commandText($"* | {SearchItem}"),
                        description("(Tüm mushaf boyunca geçen ", (b)SearchItem, $" {DescriptionPart} aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText($"2:* | {SearchItem}"),
                        description("(2. surenin tamamında geçen ", (b)SearchItem, $" {DescriptionPart} aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText($"2:*, 3:*, 7:5-40 | {SearchItem}"),
                        description("(2. surenin tamamında, 3. surenin tamamında ve 7. surenin 5 ila 40. ayetler arasında geçen ", (b)SearchItem, $" {DescriptionPart} aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText($"2:*, -2:4, -2:8 | {SearchItem}"),
                        description("(2. surenin tamamında(4. ve 8. ayetler hariç), geçen ", (b)SearchItem, $" {DescriptionPart} aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText($"*, -9:128, -9:129 | {SearchItem}"),
                        description("(Tüm mushaf boyunca (9:128 ve 9:129 hariç), geçen ", (b)SearchItem, $" {DescriptionPart} aratır)")
                    },
                    new tr { Height(10) },
                    new tr
                    {
                        commandText($"2:17 --> 5:4 | {SearchItem}"),
                        description("(2. surenin 17. ayeti ile 5. surenin 4. ayeti arasında geçen ", (b)SearchItem, $" {DescriptionPart} aratır)")
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