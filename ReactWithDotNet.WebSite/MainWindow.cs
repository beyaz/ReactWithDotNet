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
        if (PageId == "2")
        {
            return new HtmlToCSharpView();
        }

        return new FigmaCss2ReactInlineStyleConverterView();
    }
}

static class QueryKey
{
    public static string Page = "p";
}