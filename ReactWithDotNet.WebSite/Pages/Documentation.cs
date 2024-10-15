namespace ReactWithDotNet.WebSite.Pages;

class PageDocumentation : Component<PageDocumentation.State>
{
    internal record State
    {
        public bool? LeftMenuIsCollapsed { get; init; }
    }
    Element LeftMenu(string url)
    {
        return new FlexRow(JustifyContentCenter, AlignItemsFlexStart, Background("#f8fafc"), PositionRelative)
        {
            Transition(Width,300,"ease-in"),
                
            state.LeftMenuIsCollapsed is true ? Width(16) : Width(286),
                
            new IconLeft
            {
                style = { PositionAbsolute, Right(-10), Top(25), When(state.LeftMenuIsCollapsed is true, Rotate("180deg")) } 
                    
            } + OnClick(ToggleCollapse) + MD(DisplayNone),
                
            state.LeftMenuIsCollapsed is true ? null :
                LeftMenuContent(url)
        };

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

            return new nav(FontSize14, LineHeight24, PositionSticky, Top(100), PaddingBottom(32))
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

    Task ToggleCollapse(MouseEvent e)
    {
        if (state.LeftMenuIsCollapsed is null)
        {
            state = state with { LeftMenuIsCollapsed = false };
        }
        state = state with { LeftMenuIsCollapsed = !state.LeftMenuIsCollapsed };
                
        return Task.CompletedTask;
    }
    
    static Element Start()
    {
        return new FlexRow(JustifyContentCenter, WidthFull)
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

            }
        };
    }
    
     static Element ServerDrivenUI()
    {
        return new FlexRow(JustifyContentCenter, WidthFull)
        {
            new article(PaddingTopBottom(4 * rem))
            {
                new h1(FontSize32, FontWeight400, LineHeight32, MarginBottom(1.2 * rem))
                {
                    "What is Server Driven UI"
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

            }
        };
    }

    protected override Element render()
    {
        return new PageLayout
        {
            new main(DisplayFlexRow)
            {
                LeftMenu(Context.Request.Path),

                
                new FlexRow(PaddingX(5 * percent), Background(White), Flex(1.5))
                {
                    Start()
                }+ When(state.LeftMenuIsCollapsed is false , Height100vh, OverflowHidden)
            }
        };
    }

 
   
    
    sealed class IconLeft : PureComponent
    {
        // ReSharper disable once MemberCanBePrivate.Local
        public string Color { get; init; } = "#c5d7e8";

        protected override Element render()
        {
            return new svg(ViewBox(0, 0, 50, 50), svg.Size(20))
            {
                new path { fill = Color, d = "M25 1C11.767 1 1 11.767 1 25s10.767 24 24 24 24-10.767 24-24S38.233 1 25 1zm0 46C12.869 47 3 37.131 3 25S12.869 3 25 3s22 9.869 22 22-9.869 22-22 22z" },
                new path { fill = Color, d = "M29.293 10.293 14.586 25l14.707 14.707 1.414-1.414L17.414 25l13.293-13.293z" }
            };
        }
    }
}