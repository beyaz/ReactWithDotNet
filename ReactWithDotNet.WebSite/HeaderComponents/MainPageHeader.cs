namespace ReactWithDotNet.WebSite.HeaderComponents;

class MainPageHeader : ReactPureComponent
{
    protected override Element render()
    {
        return new header(DisplayFlex, JustifyContentCenter, BoxShadow($"inset 0px -1px 1px {Theme.grey_100}"))
        {
            new MainContentContainer(JustifyContentFlexStart, WidthMaximized)
            {
                new HeaderMenuBar()
            }
        };
    }
}