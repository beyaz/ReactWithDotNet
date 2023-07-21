
namespace ReactWithDotNet.WebSite.Pages;

class PageArticles : ReactPureComponent
{
    string ArticleId => Context.Query[QueryKey.Id];
    
    protected override Element render()
    {

        if (string.IsNullOrWhiteSpace(ArticleId) == false)
        {
            return new FlexRow(DisplayFlex, JustifyContentCenter)
            {
                new MainContentContainer(JustifyContentCenter, WidthMaximized, FlexDirectionColumn, PaddingTopBottom(20))
                {
                    new Article { FilePathInContentFolder = "tr\\6.html" }
                }
            };
        }
        
        
        
        return new div(Background("#f9f9f9"))
        {
            new FlexRow(DisplayFlex, JustifyContentCenter)
            {
                new MainContentContainer(JustifyContentCenter, WidthMaximized, FlexDirectionColumn)
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






