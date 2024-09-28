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
    public static readonly PageRouteInfo LivePreview = new($"/{nameof(LivePreview)}", typeof(LivePreview));

    public static string LivePreviewUrl(string guid)
    {
        return Page.LivePreview.Url + $"?{ReactWithDotNet.WebSite.Components.LivePreview.QueryParameterNameOfGuid}={guid}";
    }
    
    public static string DocDetailUrl(string part)
    {
        return DocDetail.Url + part;
    }
}