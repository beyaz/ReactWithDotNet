
namespace ReactWithDotNet.WebSite.Pages;

class PageMain: ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow(Gap(20),WidthMaximized, JustifyContentSpaceAround)
        {
            new MainPageContentDescription()+WidthMaximized,
            new MainPageContentSample()+WidthMaximized,
            
            // C S S
            MediaQueryOnMobileOrTablet(FlexWrap, Gap(50))
        };
    }
}