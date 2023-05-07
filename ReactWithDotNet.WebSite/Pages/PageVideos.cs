
namespace ReactWithDotNet.WebSite.Pages;

class PageVideos : ReactPureComponent
{
    protected override Element render()
    {
        return new div(Background("#f9f9f9"))
        {
            new FlexRow(DisplayFlex, JustifyContentCenter)
            {
                new MainContentContainer(JustifyContentCenter, WidthMaximized, FlexDirectionColumn)
                {
                    new section(DisplayFlexRow, Padding(100), FlexWrap, JustifyContentCenter, Margin(-20), Gap(20),
                        CursorDefault)
                    {
                        RawData.YoutubeLinks.Select(x => new YoutubeCard { Model = x })
                    }
                }
            }
        };
    }
}


