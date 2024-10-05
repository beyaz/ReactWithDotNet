using ReactWithDotNet.WebSite.HeaderComponents;

namespace ReactWithDotNet.WebSite;

sealed class PageLayout : PureComponent
{
    protected override Element render()
    {
        return new div(SizeFull)
        {
            new MainPageHeader(),

            children,

            new MainPageFooter()
        };
    }
}

sealed class BlogPageLayout : PureComponent
{
    protected override Element render()
    {
        return new PageLayout
        {
            new main(PaddingY(48), Background("#f9f9fa"), DisplayFlexRow, JustifyContentCenter)
            {
                new FlexColumn(MaxWidth(820), SizeFull, PaddingX(24))
                {
                    children
                }
            }
        };
    }
}