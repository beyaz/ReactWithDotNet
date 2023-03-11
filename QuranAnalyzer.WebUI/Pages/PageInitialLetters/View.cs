using ReactWithDotNet.react_xarrows;
using static QuranAnalyzer.ArabicLetter;

namespace QuranAnalyzer.WebUI.Pages.PageInitialLetters;

class InitialLetter : ReactPureComponent
{
    public (int? count, string url, string color) Count { get; set; }

    public string CountColor { get; set; }

    public string Id { get; set; }

    public bool IsSelected { get; set; }
    public string Letter { get; set; }

    protected override Element render()
    {
        var color = "#a9acaa";
        if (IsSelected)
        {
            color = "red";
        }

        var pronuncation = GetPronunciationOfArabicLetter(Letter);

        return new FlexColumn(Id(Id), TextAlignCenter, Border($"{(IsSelected ? 2 : 1)}px solid {color}"), BorderRadius("0.5rem"), Padding("5px"))
        {
            new div(Text(Letter)),
            new div(pronuncation) { FontSize("70%"), FontWeight600 },

            When(Count.count.HasValue, new a(Text(Count.count.ToString()), Href(Count.url), Color(Count.color), FontSize("70%"), FontWeight600, TextDecorationUnderline))
        };
    }
}

class CountingResult : ReactPureComponent
{
    public string id { get; set; }

    public int MultipleOf { get; set; }

    public string MultipleOfColor { get; set; }

    public string SearchScript { get; set; }

    protected override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "row", flexWrap = "wrap" },
            id    = id,
            children =
            {
                new FlexRow(AlignItemsFlexEnd)
                {
                    new div("19") { FontWeight500 }, (small)" x ", new small(MultipleOf.ToString()) { When(MultipleOfColor.HasValue(), Color(MultipleOfColor)) }
                },
                new a
                {
                    innerText = "incele",
                    href      = $"?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={SearchScript}",
                    style     = { marginLeft = "5px", color = BluePrimary }
                }
            }
        };
    }
}

class InitialLetterLineGroup : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow
        {
            Margin(1),

            JustifyContentSpaceEvenly,

            Children(children)
        };
    }
}

class Chapter : ReactPureComponent
{
    public string ChapterName { get; set; }
    public int ChapterNumber { get; set; }

    protected override Element render()
    {
        return new div
        {
            style = { margin = "3px", textAlign = "center" },

            children =
            {
                new div { innerText = $"Sure - {ChapterNumber}" },
                new div { text      = $"({ChapterName})", style = { fontWeight = "500" } }
            }
        };
    }
}

class Arrow : ReactPureComponent
{
    public string color;
    public string end;
    public string start;

    public bool dashness { get; set; }
    public bool StartAnchorFromRight { get; set; }

    public bool StartAnchorFromTop { get; set; }

    public double? strokeWidth { get; set; } = 1;

    protected override Element render()
    {
        color ??= "#a9acaa";

        return new Xarrow
        {
            start       = start,
            end         = end,
            path        = "smooth",
            color       = color,
            strokeWidth = strokeWidth,
            startAnchor = StartAnchorFromTop ? "top" : StartAnchorFromRight ? "right" : "bottom",
            dashness    = true,
            //curveness  = 1.02,
            endAnchor = "left"
        };
    }
}

abstract class InitialLetterGroup : ReactPureComponent
{
    protected tr HeaderSpace => new() { Height(15), new td(), new td(), new td() };

    protected tr HeaderTr => new()
    {
        new th { innerText = "Sure" },
        new th { innerText = "Başlangıç Harfleri" },
        new th { innerText = "Sayım Sonuçları" }
    };

    protected tr RowSpace => new() { Height(10), new td(), new td(), new td() };
}

class InitialLetterGroup_Saad : InitialLetterGroup
{
    static string IdOfCountingResult => $"ThreeSaad-{nameof(IdOfCountingResult)}";

    protected override Element render()
    {
        return new div
        {
            new table
            {
                style = { width = "100%" },
                children =
                {
                    new tbody
                    {
                        HeaderTr,
                        HeaderSpace,
                        new tr
                        {
                            new td { new Chapter { ChapterNumber = 7, ChapterName = "Araf" } },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    new InitialLetter { Id = Id(7, Alif), Letter = Alif },
                                    new InitialLetter { Id = Id(7, Laam), Letter = Laam },
                                    new InitialLetter { Id = Id(7, Miim), Letter = Miim },
                                    new InitialLetter { Id = Id(7, Saad), Letter = Saad, IsSelected = true }
                                }
                            }
                        },
                        RowSpace,
                        new tr
                        {
                            new td
                            {
                                new Chapter { ChapterNumber = 19, ChapterName = "Meryem" }
                            },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    new InitialLetter { Id = Id(19, Kaaf), Letter = Kaaf },
                                    new InitialLetter { Id = Id(19, Haa), Letter  = Haa_ },
                                    new InitialLetter { Id = Id(19, Yaa), Letter  = Yaa },
                                    new InitialLetter { Id = Id(19, Ayn), Letter  = Ayn },
                                    new InitialLetter { Id = Id(19, Saad), Letter = Saad, IsSelected = true }
                                }
                            },
                            new td
                            {
                                colSpan = 3,
                                children =
                                {
                                    new div
                                    {
                                        style = { marginTop = "-50px", display = "flex", justifyContent = "center" },
                                        children =
                                        {
                                            new CountingResult
                                            {
                                                id           = IdOfCountingResult, MultipleOf = 8,
                                                SearchScript = GetLetterCountingScript("7:*,19:*,38:*", Saad),
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        RowSpace,
                        new tr
                        {
                            new td { new Chapter { ChapterNumber = 38, ChapterName = "Sad" } },
                            new td
                            {
                                new InitialLetterLineGroup
                                {
                                    new InitialLetter { Id = Id(38, Saad), Letter = Saad, IsSelected = true, style = { ml(100) } }
                                }
                            }
                        }
                    }
                }
            },

            new Note
            {
                AsLetter(Saad), @" başlangıç harfi olarak 3 surenin başında vardır.",
                " Bu üç suredeki toplam geçiş adeti ise ", 152.AsMultipleOf19(), "'dir.",
                " Buradaki girift yapıya dikkatli bakınız.",
                " Mesela 19. suredeki ", AsLetter(Saad),
                " harfi hem 19. suredeki toplam ile uyumlu ",
                " hemde bu 3 suredeki geçiş adeti ile uyumludur.",
                " Eğer 19.surede bir tane fazla ", AsLetter(Saad), " harfi olsaydı bu ahenk bozulurdu."
            },

            new Arrow { start = Id(7, Saad), end  = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
            new Arrow { start = Id(19, Saad), end = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
            new Arrow { start = Id(38, Saad), end = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
        };
    }

    static string Id(int chapterNumber, string letter) => $"ThreeSaad-{chapterNumber}-{letter}";
}