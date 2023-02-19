using ReactWithDotNet.WebSite.Components;

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
        if (PageId == nameof(FigmaCss2ReactInlineStyleConverterView))
        {
            return new FigmaCss2ReactInlineStyleConverterView();
        }

        if (PageId == nameof(HtmlToCSharpView))
        {
            return new HtmlToCSharpView();
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

        return new divVerticalCentered
        {
            new a { text = "Figma css to React inline style", href = $"?{QueryKey.Page}={nameof(FigmaCss2ReactInlineStyleConverterView)}" },

            new a { text = "Html to ReactWithDotNet", href = $"?{QueryKey.Page}={nameof(HtmlToCSharpView)}" }
        };

    }
}

static class QueryKey
{
    public static string Page = "p";
}

