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
                new HeaderMenuItem{Text = "What is ReactWithDotNet"},
                new HeaderMenuItem{Text = "Tutorial"},
                new HeaderMenuItem{Text = "Showcase"}
            }

        };
    }
}