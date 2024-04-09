namespace ReactWithDotNet.WebSite.Components;

static class UI
{
    public static Element LeftMenu(string url)
    {
        return new FlexRow(JustifyContentCenter, AlignItemsFlexStart, Background("#f8fafc"), Width(286))
        {
            LeftMenuContent(url)
        };
    }

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

    public static Element SampleDocumentContent()
    {
        return new FlexRow(JustifyContentCenter, Background("white"), WidthFull)
        {
            new article(PaddingTopBottom(4 * rem))
            {
                new h1(FontSize32, FontWeight400, LineHeight32, MarginBottom(1.2 * rem))
                {
                    "Ana Başlık"
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },

                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta 2"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "2 Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },

                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta 2"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "2 Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },

                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta 2"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "2 Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },

                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta 2"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "2 Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },

                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta 2"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "2 Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                }
            }
        };
    }

    static Element LeftMenuContent(string url)
    {
        var data = new[]
        {
            new
            {
                Title = "Introduction",
                Links = new[]
                {
                    new { Label = "Getting started", Url  = Page.DocDetailUrl("start") },
                    new { Label = "Server Driven UI", Url = Page.DocDetailUrl("1") },
                    new { Label = "React and .Net", Url   = Page.DocDetailUrl("2") }
                }
            },
            new
            {
                Title = "Core concept",
                Links = new[]
                {
                    new { Label = "Syntax Sugars", Url   = "/getuser" },
                    new { Label = "Modifiers", Url       = "/getuser" },
                    new { Label = "Sample Todo App", Url = "/getuser" }
                }
            },
            new
            {
                Title = "Advanced concept",
                Links = new[]
                {
                    new { Label = "Syntax Sugars", Url   = "/getuser" },
                    new { Label = "Modifiers", Url       = "/getuser" },
                    new { Label = "Sample Todo App", Url = "/getuser" }
                }
            }
        };

        bool isSelected(string uuu)
        {
            return url?.EndsWith(uuu) == true;
        }

        return new nav(FontSize14, LineHeight24, PositionSticky, Top(100))
        {
            new ul(DisplayFlexColumn, Gap(2 * rem), Role("list"))
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
                            isSelected(link.Url) ? BorderLeft(2 * px, solid, rgb(30, 167, 253)) + MarginLeft(-2) : null,

                            new a(PaddingLeft(14), TextDecorationNone, Color(rgb(71, 85, 105)))
                            {
                                link.Label, Href(link.Url)
                            }
                        })
                    }
                })
            }
        };
    }
}