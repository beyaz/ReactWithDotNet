using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.HeaderComponents;

sealed class HeaderMenuBar : PureComponent
{
    static StyleModifier BorderRadius => BorderRadius(10);

    protected override Element render()
    {
        return new FlexRow(WidthFull, JustifyContentSpaceBetween, AlignItemsCenter, Height(60))
        {
            new FlexRow(AlignItemsCenter)
            {
                new Logo(),
                SpaceX(24),
                new nav(DisplayFlex, AlignItemsCenter, WhenMediaSizeLessThan(MD, DisplayNone))
                {
                    MenuAccess.MenuList.Select(x => new MenuView { Model = x })
                }
            },

            new MobileMenu(),

            new SocialMediaLinks()
        };
    }

    sealed class SocialMediaLinks : PureComponent
    {
        protected override Element render()
        {
            var socialMediaLinks = new[]
            {
                new
                {
                    Text = "GitHub",
                    Link = "https://github.com/beyaz/ReactDotNet",
                    Svg  = "M12 1.27a11 11 0 00-3.48 21.46c.55.09.73-.28.73-.55v-1.84c-3.03.64-3.67-1.46-3.67-1.46-.55-1.29-1.28-1.65-1.28-1.65-.92-.65.1-.65.1-.65 1.1 0 1.73 1.1 1.73 1.1.92 1.65 2.57 1.2 3.21.92a2 2 0 01.64-1.47c-2.47-.27-5.04-1.19-5.04-5.5 0-1.1.46-2.1 1.2-2.84a3.76 3.76 0 010-2.93s.91-.28 3.11 1.1c1.8-.49 3.7-.49 5.5 0 2.1-1.38 3.02-1.1 3.02-1.1a3.76 3.76 0 010 2.93c.83.74 1.2 1.74 1.2 2.94 0 4.21-2.57 5.13-5.04 5.4.45.37.82.92.82 2.02v3.03c0 .27.1.64.73.55A11 11 0 0012 1.27"
                },
                new
                {
                    Text = "Linkedin",
                    Link = "https://www.linkedin.com/company/mui/",
                    Svg  = "M19 3a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h14m-.5 15.5v-5.3a3.26 3.26 0 0 0-3.26-3.26c-.85 0-1.84.52-2.32 1.3v-1.11h-2.79v8.37h2.79v-4.93c0-.77.62-1.4 1.39-1.4a1.4 1.4 0 0 1 1.4 1.4v4.93h2.79M6.88 8.56a1.68 1.68 0 0 0 1.68-1.68c0-.93-.75-1.69-1.68-1.69a1.69 1.69 0 0 0-1.69 1.69c0 .93.76 1.68 1.69 1.68m1.39 9.94v-8.37H5.5v8.37h2.77z"
                },
                new
                {
                    Text = "Youtube",
                    Link = "https://www.youtube.com/@MUI_hq",
                    Svg  = "M10 15l5.19-3L10 9v6m11.56-7.83c.13.47.22 1.1.28 1.9.07.8.1 1.49.1 2.09L22 12c0 2.19-.16 3.8-.44 4.83-.25.9-.83 1.48-1.73 1.73-.47.13-1.33.22-2.65.28-1.3.07-2.49.1-3.59.1L12 19c-4.19 0-6.8-.16-7.83-.44-.9-.25-1.48-.83-1.73-1.73-.13-.47-.22-1.1-.28-1.9-.07-.8-.1-1.49-.1-2.09L2 12c0-2.19.16-3.8.44-4.83.25-.9.83-1.48 1.73-1.73.47-.13 1.33-.22 2.65-.28 1.3-.07 2.49-.1 3.59-.1L12 5c4.19 0 6.8.16 7.83.44.9.25 1.48.83 1.73 1.73z"
                }
            };

            return new FlexRow(Gap(15), AlignItemsCenter, MarginRight(50), WhenMediaSizeLessThan(MD, DisplayNone))
            {
                socialMediaLinks.Select(x => new a(Href(x.Link))
                {
                    DisplayFlex, FlexDirectionRow, AlignItemsCenter, JustifyContentCenter,
                    Border(Solid(1, Theme.grey_50)),
                    BorderRadius,
                    Padding(7),
                    Transition(BackgroundColor, 200, cubic_bezier(0.4, 0, 0.2, 1), 0),
                    Hover(Border(Solid(1, Theme.grey_300)), Background(Theme.grey_50)),

                    new Tooltip
                    {
                        arrow = true,
                        title = x.Text,

                        children =
                        {
                            new svg(Size(20), svg.Fill(Theme.Blue700), svg.ViewBox(0, 0, 24, 24))
                            {
                                new path { d = x.Svg }
                            }
                        }
                    }
                })
            };
        }
    }

    sealed class MenuItemView : PureComponent
    {
        public MenuItem Model { get; init; }

        protected override Element render()
        {
            return new a(PaddingTopBottom(20), BorderRadius, PaddingLeft(20), PaddingRight(30), TextDecorationNone, CursorDefault)
            {
                WidthFull,
                Href(Model.PageLink),

                LetterSpacingNormal,
                BackgroundForPaper,
                Border(1,solid,transparent),
                Hover(Background(Gray50), BorderColor(Gray100)),

                new FlexRow(AlignItemsCenter, Gap(20), BorderRadius)
                {
                    new img { Src(Asset(Model.SvgFileName)), Size(36) },
                    new FlexColumn
                    {
                        FontFamily_IBM_Plex_Sans,
                        LineHeight21,
                        FontSize14,
                        Color(text_primary),

                        new div(FontWeight600) { Model.Title },
                        new div(FontWeight400) { Model.Description }
                    }
                }
            };
        }
    }

    sealed class MenuView : PureComponent
    {
        public Menu Model { get; init; }

        protected override Element render()
        {
            return new Tooltip
            {
                classes =
                {
                    { "tooltip", [Background(Theme.common_background), Padding(0), BorderRadius] }
                },
                title = new FlexColumn(BorderRadius, Border(Solid(1, Theme.grey_200)), Width(400), BoxShadow(rgba(170, 180, 190, 0.3), 0, 4, 20))
                {
                    Model.Children.ToListOf(x => new MenuItemView { Model = x })
                },
                children =
                {
                    new div
                    {
                        Text(Model.Title),
                        Padding(10),
                        Border(1,solid,transparent),
                        Hover(Background(Gray50), BorderColor(Gray200)),
                        BorderRadius,
                        FontSize14,
                        FontWeight700,
                        CursorDefault
                    }
                }
            };
        }
    }
}