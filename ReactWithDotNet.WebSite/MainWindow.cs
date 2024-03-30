using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public class MainWindow : PureComponent
{
    protected override Element render()
    {
        return new div(WidthFull, HeightMaximized)
        {
            new MainPageHeader(),

            new main
            {
                createContent
            },
            
            new Footer()
            
        };

        Element createContent()
        {
            var pageName = KeyForHttpContext[Context].Request.Query[QueryKey.Page];
            
            var typeOfPage = Type.GetType($"ReactWithDotNet.WebSite.Pages.{pageName}") ?? typeof(PageMain);

            return (Element)Activator.CreateInstance(typeOfPage);
        }
    }
}

