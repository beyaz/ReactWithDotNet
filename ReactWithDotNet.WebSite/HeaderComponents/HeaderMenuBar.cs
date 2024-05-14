using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.WebSite.Content;

namespace ReactWithDotNet.WebSite.HeaderComponents;

class HeaderMenuBar : PureComponent
{
    static StyleModifier BorderRadius => BorderRadius(10);

    protected override Element render()
    {
        return new FlexRow(WidthFull, JustifyContentSpaceBetween, AlignItemsCenter, Height(60))
        {
            new FlexRow(AlignItemsCenter)
            {
                new Logo(),
                SpaceX(10),
                new nav(DisplayFlex, AlignItemsCenter, WhenMediaSizeLessThan(MD,DisplayNone))
                {
                    RawData.MenuList.Select(AsTooltip)
                }
            },

            new MobileMenu(),
            
            new FlexRow(Gap(15), AlignItemsCenter, MarginRight(50), WhenMediaSizeLessThan(MD,DisplayNone))
            {
                RawData.SocialMediaLinks.Select(x => new a(Href(x.Link))
                {
                    DisplayFlex, FlexDirectionRow, AlignItemsCenter, JustifyContentCenter,
                    Border(Solid(1, Theme.grey_50)),
                    BorderRadius,
                    Padding(7),
                    Transition("background-color", 200, cubic_bezier(0.4, 0, 0.2, 1), 0),
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
            }
        };
    }

    Element AsTooltip(Menu m)
    {
        return new Tooltip
        {
            classes = { { "tooltip", new Style { Background(Theme.common_background), Padding(0),BorderRadius } } },
            title = new FlexColumn(BorderRadius, Border(Solid(1, Theme.grey_200)), Width(400), BoxShadow("rgba(170, 180, 190, 0.3) 0px 4px 20px"))
            {
                m.Children.ToListOf(AsTooltipRow)
            },
            children =
            {
                new div
                {
                    Text(m.Title),
                    Padding(10),
                    Hover(Background(Theme.grey_50)),
                    BorderRadius,
                    FontSize14,
                    FontWeight700,
                    CursorDefault
                }
            }
        };
    }
    
    Element AsTooltipRow(MenuItem model)
    {
        return new a(PaddingTopBottom(20), BorderRadius, PaddingLeft(20), PaddingRight(30), TextDecorationNone, CursorDefault)
        {
            GetPageLink(model.PageName),

            LetterSpacingNormal,
            BackgroundForPaper,
            Hover(Background(Theme.grey_50)),

            new FlexRow(AlignItemsCenter, Gap(20), BorderRadius)
            {
                new img { Src(Asset(model.SvgFileName)), Size(36) },
                new FlexColumn
                {
                    FontFamily_IBM_Plex_Sans,
                    LineHeight21,
                    FontSize14,
                    Color(Theme.text_primary),

                    new div(FontWeight600) { model.Title },
                    new div(FontWeight400) { model.Description }
                }
            }
        };
    }
}