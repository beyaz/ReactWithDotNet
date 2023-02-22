using ReactWithDotNet.WebSite.Components;
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
        if (PageId == nameof(HelperApps))
        {
            return new HelperApps.View();
        }

        return new div
        {
            new MainPageHeader(),
            
            new main(PaddingTopBottom(80))
            {
                new MainContentContainer(JustifyContentCenter, WidthMaximized)
                {
                   new FlexRow(Gap(150))
                   {
                       new MainPageContentDescription(),
                       new MainPageContentSample()
                   }
                }

            },

            new MainPageFooter()
        };

    }
}

static class QueryKey
{
    public static string Page = "p";
}

