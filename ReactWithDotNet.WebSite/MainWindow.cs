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

class Extensions
{
    public static IEnumerable<Element> PrimeReactCssLibs => new[]
    {
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/themes/saga-blue/theme.css" },
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/primereact.min.css" },
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primeicons@5.0.0/primeicons.css" },

    };
}