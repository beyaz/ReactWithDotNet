using System.IO;
using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public class MainWindow : PureComponent
{
    protected override Element render()
    {
        return new div(WidthMaximized, HeightMaximized)
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

class Footer : PureComponent
{
    protected override Element render()
    {
        return new footer(PositionFixed,Bottom(0), BorderTop(Solid(1, Theme.grey_100)), Height(50),WidthMaximized)
        {
            DisplayFlexRowCentered,
            new HighlightedText
            {
                Text = "React [\u2665] .Net"
            }
        };
    }
}