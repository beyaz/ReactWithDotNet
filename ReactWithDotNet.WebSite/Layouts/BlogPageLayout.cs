namespace ReactWithDotNet.WebSite;

sealed class BlogPageLayout : PureComponent
{
    protected override Element render()
    {
        return new main(PaddingY(48), Background("#f9f9fa"), DisplayFlexRow, JustifyContentCenter)
        {
            new FlexColumn(MaxWidth(820), SizeFull, PaddingX(24))
            {
                children
            }
        };
    }
}