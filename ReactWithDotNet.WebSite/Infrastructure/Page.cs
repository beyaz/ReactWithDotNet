using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

sealed record PageRouteInfo(string Url, Type page);

static class Page
{
    public static readonly PageRouteInfo CSharpPropertyMapper = new("/CSharpPropertyMapper", typeof(CSharpPropertyMapperView));
    public static readonly PageRouteInfo Doc = new("/doc", typeof(PageDocumentation));
    public static readonly PageRouteInfo DocDetail = new("/doc/", typeof(PageDocumentation));
    public static readonly PageRouteInfo Home = new("/", typeof(MainWindow));
    public static readonly PageRouteInfo ImportFigmaCss = new("/importFigmaCss", typeof(FigmaCss2ReactInlineStyleConverterView));
    public static readonly PageRouteInfo LiveEditor = new("/LiveEditor", typeof(HtmlToCSharpView));

    internal static readonly IReadOnlyDictionary<string, PageRouteInfo> Map = new[]
    {
        Home,
        Doc,
        DocDetail,
        LiveEditor,
        CSharpPropertyMapper,
        ImportFigmaCss
    }.ToDictionary(x => x.Url, x => x, StringComparer.OrdinalIgnoreCase);

    public static string DocDetailUrl(string part)
    {
        return DocDetail.Url + part;
    }
}