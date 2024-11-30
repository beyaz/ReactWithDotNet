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
                new nav(DisplayFlex, AlignItemsCenter, WhenMediaMaxWidth(MD, DisplayNone))
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
                    Link = "https://github.com/beyaz/ReactWithDotNet",
                    Svg  = "M12 1.27a11 11 0 00-3.48 21.46c.55.09.73-.28.73-.55v-1.84c-3.03.64-3.67-1.46-3.67-1.46-.55-1.29-1.28-1.65-1.28-1.65-.92-.65.1-.65.1-.65 1.1 0 1.73 1.1 1.73 1.1.92 1.65 2.57 1.2 3.21.92a2 2 0 01.64-1.47c-2.47-.27-5.04-1.19-5.04-5.5 0-1.1.46-2.1 1.2-2.84a3.76 3.76 0 010-2.93s.91-.28 3.11 1.1c1.8-.49 3.7-.49 5.5 0 2.1-1.38 3.02-1.1 3.02-1.1a3.76 3.76 0 010 2.93c.83.74 1.2 1.74 1.2 2.94 0 4.21-2.57 5.13-5.04 5.4.45.37.82.92.82 2.02v3.03c0 .27.1.64.73.55A11 11 0 0012 1.27"
                },
                
                new
                {
                    Text = "Twitter",
                    Link = "https://x.com/ReactWithDotNet",
                    Svg  = "M19.633 7.997c.013.175.013.349.013.523 0 5.325-4.053 11.461-11.46 11.461-2.282 0-4.402-.661-6.186-1.809.324.037.636.05.973.05a8.07 8.07 0 0 0 5.001-1.721 4.036 4.036 0 0 1-3.767-2.793c.249.037.499.062.761.062.361 0 .724-.05 1.061-.137a4.027 4.027 0 0 1-3.23-3.953v-.05c.537.299 1.16.486 1.82.511a4.022 4.022 0 0 1-1.796-3.354c0-.748.199-1.434.548-2.032a11.457 11.457 0 0 0 8.306 4.215c-.062-.3-.1-.611-.1-.923a4.026 4.026 0 0 1 4.028-4.028c1.16 0 2.207.486 2.943 1.272a7.957 7.957 0 0 0 2.556-.973 4.02 4.02 0 0 1-1.771 2.22 8.073 8.073 0 0 0 2.319-.624 8.645 8.645 0 0 1-2.019 2.083z"
                }
            };

            return new FlexRow(Gap(15), AlignItemsCenter, MarginRight(50), WhenMediaMaxWidth(MD, DisplayNone))
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