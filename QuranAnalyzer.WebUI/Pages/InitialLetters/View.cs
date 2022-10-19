using ReactWithDotNet.react_xarrows;
using static QuranAnalyzer.ArabicLetter;
using static QuranAnalyzer.WebUI.Extensions;

namespace QuranAnalyzer.WebUI.Pages.InitialLetters;


class InitialLetter : ReactComponent
{
    public string text { get; set; }

    public string id { get; set; }

    public bool IsSelected { get; set; }

    protected override Element render()
    {
        var color = "#a9acaa";
        if (IsSelected)
        {
            color = "red";
        }
        
        return new div
        {
            style = { border = $"{(IsSelected?2:1)}px solid {color}", borderRadius = "0.5rem", padding = "5px" },
            id    = id,
            text  = text
        };
    }
}

class CountingResult: ReactComponent
{
    public string id { get; set; }

    public int MultipleOf { get; set; }

    public string SearchScript { get; set; }

    protected override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "row", flexWrap = "wrap"},
            id    = id,
            children =
            {
                new div($"19 x {MultipleOf}"),
                new a
                {
                    innerText = "incele",
                    href      = $"?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={SearchScript}",
                    style     = { marginLeft = "5px" },
                    target = "_blank"
                }
            }
        };
    }
}

class InitialLetterLineGroup : ReactComponent
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

class Chapter : ReactComponent
{
    public int ChapterNumber { get; set; }
    
    public string ChapterName { get; set; }

    protected override Element render()
    {
        return new div
        {
            style = { margin = "3px", textAlign = "center"},
            
            children =
            {
                new div{innerText = $"Sure - {ChapterNumber}"},
                new div{ text     = $"({ChapterName})", style ={fontWeight = "500"}}
            }
        };
    }
}



class Arrow: ReactComponent
{
   public  string start;
   public  string end;
   public  string color;

   
   public bool StartAnchorFromTop { get; set; }
   public bool StartAnchorFromRight { get; set; }
    

    public bool dashness { get; set; }

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


abstract class InitialLetterGroup : ReactComponent
{
    protected tr HeaderTr => new tr
    {
        new th { innerText = "Sure" },
        new th { innerText = "Başlangıç Harfleri" },
        new th { innerText = "Sayım Sonuçları" }
    };

    protected tr RowSpace => new tr { style = { height = "10px" } };

    protected tr HeaderSpace => new tr { style = { height = "15px" } };
}

class InitialLetterGroup_Saad: InitialLetterGroup
{
    static string Id(int chapterNumber, string letter) => $"ThreeSaad-{chapterNumber}-{letter}";

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
                                   
                                        new InitialLetter { id = Id(7,Alif), text = Alif },
                                        new InitialLetter { id = Id(7,Laam), text = Laam },
                                        new InitialLetter { id = Id(7,Miim), text = Miim },
                                        new InitialLetter { id = Id(7,Saad), text = Saad, IsSelected = true }
                                    
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
                                    
                                        new InitialLetter { id = Id(19,Qaaf), text = Qaaf },
                                        new InitialLetter { id = Id(19,Haa), text  = Haa_ },
                                        new InitialLetter { id = Id(19,Yaa), text  = Yaa },
                                        new InitialLetter { id = Id(19,Ayn), text  = Ayn },
                                        new InitialLetter { id = Id(19,Saad), text = Saad, IsSelected = true }

                                    
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
                                   
                                        new InitialLetter { id = Id(38,Saad), text = Saad, IsSelected = true }
                                    
                                }
                            }
                        }
                    }
                }

            },
            
            new Arrow { start = Id(7,Saad),  end = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
            new Arrow { start = Id(19,Saad), end = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
            new Arrow { start = Id(38,Saad), end = IdOfCountingResult, dashness = true, StartAnchorFromRight = true },
        };
    }
}