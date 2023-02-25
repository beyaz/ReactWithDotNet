
namespace ReactWithDotNet.WebSite.Pages;

class PageModifiers : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow(Gap(150), WidthMaximized, JustifyContentSpaceAround)
        {
           "Page modifiers"
        };
    }
}