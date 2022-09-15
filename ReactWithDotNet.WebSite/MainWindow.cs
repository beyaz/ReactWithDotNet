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
            style = { width_height = "100%", display = "flex", flexDirection = "column", gap = "10px", justifyContent = "center", alignItems = "center", textAlign = "center"},
            children =
            {
                new a { text = "Figma css to React inline style", href = $"?{QueryKey.Page}={nameof(FigmaCss2ReactInlineStyleConverterView)}" },
                new a { text = "Html to ReactWithDotNet", href                   = $"?{QueryKey.Page}={nameof(HtmlToCSharpView)}" }
            }
        };

    }
}

static class QueryKey
{
    public static string Page = "p";
}