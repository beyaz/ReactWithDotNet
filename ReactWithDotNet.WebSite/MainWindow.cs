using ReactWithDotNet.WebSite.Components;
using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.HelperApps;

namespace ReactWithDotNet.WebSite;

public class MainWindow : ReactComponent
{
    public string PageId { get; set; }

    protected override void constructor()
    {
        PageId = Context.Query[QueryKey.Page];
    }

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
            if (PageId == nameof(HelperApps))
            {
                return new HelperApps.View();
            }

            return new FlexRow(Gap(150),WidthMaximized, JustifyContentSpaceAround)
            {
                new MainPageContentDescription(),
                new MainPageContentSample()
            };
        }

    }
}

static class QueryKey
{
    public static string Page = "p";
}

