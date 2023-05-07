using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public class MainWindow : ReactPureComponent
{
    private string PageName => Context.Query[QueryKey.Page];

    protected override Element render()
    {
        return new div(WidthMaximized, HeightMaximized)
        {
            new MainPageHeader(),

            new main
            {
                createContent
            },


            new footer(BorderTop(Solid(1, Theme.grey_100)), Height(50), DisplayFlexRowCentered)
            {
                new HighlightedText { Text = RawData.FooterText }
            }
        };

        Element createContent()
        {
            var typeOfPage = Type.GetType($"ReactWithDotNet.WebSite.Pages.{PageName}") ?? typeof(PageMain);

            return (Element)Activator.CreateInstance(typeOfPage);
        }
    }
}