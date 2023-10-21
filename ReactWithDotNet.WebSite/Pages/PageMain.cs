
namespace ReactWithDotNet.WebSite.Pages;

class PageMain: PureComponent
{
    protected override Element render()
    {
        return new FlexRowCentered(WidthMaximized)
        {
            new FlexRow(Gap(20), ContainerStyle, JustifyContentSpaceAround)
            {
                new MainPageContentDescription()+FlexGrow(1),
                
                new MainPageContentSample()+FlexGrow(1),
            
                // C S S
                MediaQueryOnMobileOrTablet(FlexWrap, Gap(50))
            }
        };
    }
}