using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public class MainWindow : ReactPureComponent
{
    string PageName => Context.Query[QueryKey.Page];
    

    protected override Element render()
    {
        return new div(WidthMaximized, HeightMaximized)
        {
            new MainPageHeader(),
            
            new main(PaddingTopBottom(80), DisplayFlex, JustifyContentCenter)
            {
                new MainContentContainer(JustifyContentCenter, WidthMaximized)
                {
                    createContent
                }

            }
        };

        Element createContent()
        {
            var typeOfPage = Type.GetType($"ReactWithDotNet.WebSite.Pages.{PageName}") ?? typeof(PageMain);

            return (Element)Activator.CreateInstance(typeOfPage);
        }

    }
}

static class QueryKey
{
    public static string Page = "p";
}

