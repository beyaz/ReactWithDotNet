namespace ReactWithDotNet.WebSite;

sealed class CommonPageLayout: PureComponent
{
    protected override Element render()
    {
        return new main(PaddingY(48))
        {
            new FlexRowCentered(WidthFull)
            {
                new FlexColumn(ContainerStyle, PaddingY(48))
                {
                    children
                }
            }
        };
    }
}