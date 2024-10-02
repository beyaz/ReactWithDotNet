namespace ReactWithDotNet.WebSite.Pages;

class PageMain : Component
{
    protected override Element render()
    {
        return new CommonPageLayout
        {
            new FlexColumn(Gap(20))
            {
                new MainPageContentDescription(),

                SpaceY(15),

                new MainPageContentSample {  }
            }
        };
    }
}