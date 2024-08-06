
namespace ReactWithDotNet.WebSite.Pages;

class PageMain: PureComponent
{
    protected override Element render()
    {
        return new FlexColumn(Gap(20))
        {
            new MainPageContentDescription(),

            SpaceY(15),

            new MainPageContentSample { Height(300) }
        };
    }
}

