namespace ReactWithDotNet.WebSite.Components;

static class UI
{
    public static Element ProgressBar(int total, int current)
    {
        return new div(WidthFull, Height(14), BorderRadius(15), Border(3, solid, "#eee"), BackgroundColor("#dddddd"))
        {
            new div
            {
                Width(current, total),
                HeightFull,
                BorderRadius(15),
                BackgroundImage(linear_gradientTo("right", "#8490ff", "#a3eeff"))
            }
        };
    }

    public static Element LeftMenu()
    {
        var data = new[]
        {
            new
            {
                Title ="Introduction",
                Links =new[]
                {
                    new {Label ="Getting started", Url  ="/getuser"},
                    new {Label ="Server Driven UI", Url ="/getuser"},
                    new {Label ="React and .Net", Url ="/getuser"}
                }
            },
            new
            {
                Title ="Core concept",
                Links=new[]
                {
                    new {Label ="Syntax Sugars", Url ="/getuser"},
                    new {Label ="Modifiers", Url ="/getuser"},
                    new {Label ="Sample Todo App", Url ="/getuser"}
                }
            },
            new
            {
                Title ="Advanced concept",
                Links =new[]
                {
                    new {Label ="Syntax Sugars", Url   ="/getuser"},
                    new {Label ="Modifiers", Url       ="/getuser"},
                    new {Label ="Sample Todo App", Url ="/getuser"}
                }
            }
        };
        
        return new FlexRow(JustifyContentCenter, Background("#f8fafc"), Width(286))
        {
            new nav(FontSize14, LineHeight24)
            {
                new ul(DisplayFlexColumn, Gap(48), Role("list"))
                {
                    data.Select(item => new li(ListStyleNone)
                    {
                        new h2(FontWeight500, FontSize14, Color(rgb(15, 23, 42)))
                        {
                            item.Title
                        },
                        new ul(Role("list"), BorderLeft(2 * px, solid, rgb(226, 232, 240)))
                        {
                            item.Links.Select(link => new li(MarginTop(16), DisplayFlexRow, AlignItemsCenter)
                            {
                                new span(Size(8),MarginLeft(-5),  BorderRadius(4),Background(rgb(30, 167, 253))),
                            
                                new a(PaddingLeft(14), TextDecorationNone, Color(rgb(71, 85, 105)))
                                {
                                    link.Label, Href(link.Url)
                                }
                            })
                        }

                    })
                }
            }
        };
    }
}