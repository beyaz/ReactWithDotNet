namespace ReactWithDotNet.WebSite.HeaderComponents;

class HeaderMenuBar : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow
        {
            new a{ Href("/"), new Logo(), PaddingTopBottom(10), TextDecorationNone},
            new nav(DisplayFlex,AlignItemsCenter)
            {
                new HeaderMenuItem
                {
                    Text = "What is ReactWithDotNet",
                    TooltipRows = new []
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
                    Text = "Tutorial",
                    TooltipRows = new []
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
                    TooltipRows = new []
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

        };
    }
}