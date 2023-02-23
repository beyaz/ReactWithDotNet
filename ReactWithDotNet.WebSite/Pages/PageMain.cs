
namespace ReactWithDotNet.WebSite.Pages;

class PageMain: ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow(Gap(150), WidthMaximized, JustifyContentSpaceAround)
        {
            new MainPageContentDescription(),
            new MainPageContentSample()
        };
    }
}