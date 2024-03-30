
namespace ReactWithDotNet.WebSite.Pages;

class PageArticles : PureComponent
{
    protected override Element render()
    {
        var articleId = KeyForHttpContext[Context].Request.Query[QueryKey.Id];
        
        if (string.IsNullOrWhiteSpace(articleId) == false)
        {
            return new FlexRow(DisplayFlex, JustifyContentCenter)
            {
                new MainContentContainer(JustifyContentCenter, WidthFull, FlexDirectionColumn, PaddingTopBottom(20))
                {
                    new Article { FilePathInContentFolder = "tr\\6.html" }
                }
            };
        }
        
        
        
        return new div(Background("#f9f9f9"))
        {
            new FlexRow(DisplayFlex, JustifyContentCenter)
            {
                new MainContentContainer(JustifyContentCenter, WidthFull, FlexDirectionColumn)
                {
                    new section(DisplayFlexRow, Padding(100), FlexWrap, JustifyContentCenter, Margin(-20), Gap(20),
                        CursorDefault)
                    {
                        RawData.Cards.Select(x=>new RawCardViewer{ Model = x})
                    }
                }
            }
        };
    }
}






