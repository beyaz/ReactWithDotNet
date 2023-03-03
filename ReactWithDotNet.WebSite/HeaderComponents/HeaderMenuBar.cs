﻿
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite.HeaderComponents;

class HeaderMenuBar : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow(WidthMaximized, JustifyContentSpaceBetween)
        {
            new FlexRow
            {
                new a { Href("/"), new Logo(), PaddingTopBottom(10), TextDecorationNone },
                new nav(DisplayFlex, AlignItemsCenter)
                {
                    new HeaderMenuItem
                    {
                        Text = "What is ReactWithDotNet",
                        TooltipRows = new[]
                        {
                            new HeaderMenuItemTooltipRow
                            {
                                PageName = nameof(PageTechnicalDetail),
                                SvgFileName = "doc.svg",
                                Title       = "Technical Explanation",
                                Description = "Working mechanism of ReactWithDotNet"
                            },
                            new HeaderMenuItemTooltipRow
                            {
                                PageName    = nameof(PageModifiers),
                                SvgFileName = "doc.svg",
                                Title       = "Instalition",
                                Description = "Setup environment and other information"
                            },
                            new HeaderMenuItemTooltipRow
                            {
                                PageName    = nameof(PageModifiers),
                                SvgFileName = "doc.svg",
                                Title       = "Documents",
                                Description = "Documentation of api"
                            },
                        }
                    },
                    new HeaderMenuItem
                    {
                        Text = "Tutorial",
                        TooltipRows = new[]
                        {
                            new HeaderMenuItemTooltipRow
                            {
                                SvgFileName = "doc.svg",
                                Title       = "Technical Details",
                                Description = "Working alghorithm of ReactWithDotNet"
                            },
                            new HeaderMenuItemTooltipRow
                            {
                                SvgFileName = "doc.svg",
                                Title       = "Instalition",
                                Description = "Setup environment and other information"
                            },
                            new HeaderMenuItemTooltipRow
                            {
                                SvgFileName = "doc.svg",
                                Title       = "Documents",
                                Description = "Documentation of api"
                            },
                        }
                    },
                    new HeaderMenuItem
                    {
                        Text = "Showcase",
                        TooltipRows = new[]
                        {
                            new HeaderMenuItemTooltipRow
                            {
                                PageName    =nameof(PageShowcase),
                                SvgFileName = "doc.svg",
                                Title       = "Showcase",
                                Description = "Sample demonstration of html and 3rd party react libs"
                            },
                            new HeaderMenuItemTooltipRow
                            {
                                PageName    =nameof(PageHelperApps),
                                SvgFileName = "doc.svg",
                                Title       = "Helper Apps",
                                Description = "Utility tools for import existing html or style"
                            },
                            new HeaderMenuItemTooltipRow
                            {
                                SvgFileName = "doc.svg",
                                Title       = "Technical Details",
                                Description = "Working alghorithm of ReactWithDotNet"
                            },
                            new HeaderMenuItemTooltipRow
                            {
                                SvgFileName = "doc.svg",
                                Title       = "Integration with third party libraries",
                                Description = "Integration details mui, swiper, reactsuite"
                            },
                            new HeaderMenuItemTooltipRow
                            {
                                SvgFileName = "doc.svg",
                                Title       = "Documents",
                                Description = "Documentation of api"
                            },
                        }
                    }
                }
            },



            PartLinks() + MarginRight(50)
        };
    }

    Element PartLinks()
    {
        var styleForLinks = DisplayFlex + FlexDirectionRow + AlignItemsCenter+ JustifyContentCenter +
                            Border($"1px solid {Theme[Context].grey_200}")+
                            BorderRadius(10)+ 
                            Padding(7)+
                            Transition("background-color 200ms cubic-bezier(0.4, 0, 0.2, 1) 0ms")+
                            Hover(Border($"1px solid {Theme[Context].grey_300}"), Background(Theme[Context].grey_50));

        return new FlexRow(Gap(15), AlignItemsCenter)
        {
            // github
            new a(Href("https://github.com/beyaz/ReactDotNet"), styleForLinks)
            {
                NewSvg("M12 1.27a11 11 0 00-3.48 21.46c.55.09.73-.28.73-.55v-1.84c-3.03.64-3.67-1.46-3.67-1.46-.55-1.29-1.28-1.65-1.28-1.65-.92-.65.1-.65.1-.65 1.1 0 1.73 1.1 1.73 1.1.92 1.65 2.57 1.2 3.21.92a2 2 0 01.64-1.47c-2.47-.27-5.04-1.19-5.04-5.5 0-1.1.46-2.1 1.2-2.84a3.76 3.76 0 010-2.93s.91-.28 3.11 1.1c1.8-.49 3.7-.49 5.5 0 2.1-1.38 3.02-1.1 3.02-1.1a3.76 3.76 0 010 2.93c.83.74 1.2 1.74 1.2 2.94 0 4.21-2.57 5.13-5.04 5.4.45.37.82.92.82 2.02v3.03c0 .27.1.64.73.55A11 11 0 0012 1.27")
            },

            // linkedin
            new a(Href("https://www.linkedin.com/company/mui/"), styleForLinks)
            {
                NewSvg("M19 3a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h14m-.5 15.5v-5.3a3.26 3.26 0 0 0-3.26-3.26c-.85 0-1.84.52-2.32 1.3v-1.11h-2.79v8.37h2.79v-4.93c0-.77.62-1.4 1.39-1.4a1.4 1.4 0 0 1 1.4 1.4v4.93h2.79M6.88 8.56a1.68 1.68 0 0 0 1.68-1.68c0-.93-.75-1.69-1.68-1.69a1.69 1.69 0 0 0-1.69 1.69c0 .93.76 1.68 1.69 1.68m1.39 9.94v-8.37H5.5v8.37h2.77z")
            },

            // youtube
            new a(Href("https://www.youtube.com/@MUI_hq"), styleForLinks)
            {
                NewSvg("M10 15l5.19-3L10 9v6m11.56-7.83c.13.47.22 1.1.28 1.9.07.8.1 1.49.1 2.09L22 12c0 2.19-.16 3.8-.44 4.83-.25.9-.83 1.48-1.73 1.73-.47.13-1.33.22-2.65.28-1.3.07-2.49.1-3.59.1L12 19c-4.19 0-6.8-.16-7.83-.44-.9-.25-1.48-.83-1.73-1.73-.13-.47-.22-1.1-.28-1.9-.07-.8-.1-1.49-.1-2.09L2 12c0-2.19.16-3.8.44-4.83.25-.9.83-1.48 1.73-1.73.47-.13 1.33-.22 2.65-.28 1.3-.07 2.49-.1 3.59-.1L12 5c4.19 0 6.8.16 7.83.44.9.25 1.48.83 1.73 1.73z")
            }
        };

        static svg NewSvg(string d)
        {
            return new svg(WidthHeight(20), svg.Fill("rgba(0,0,0,0.54)"), svg.ViewBox("0 0 24 24") )
            {
                new path { d = d }
            };
        }
    }

}