using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

sealed record PageRouteInfo(string Url, Type page);

static class Page
{
    public static readonly PageRouteInfo CSharpPropertyMapper = new("/CSharpPropertyMapper", typeof(CSharpPropertyMapperView));
    public static readonly PageRouteInfo Doc = new("/doc", typeof(PageDocumentation));
    public static readonly PageRouteInfo DocDetail = new("/doc/", typeof(PageDocumentation));
    public static readonly PageRouteInfo Home = new("/", typeof(PageMain));
    public static readonly PageRouteInfo ImportFigmaCss = new("/importFigmaCss", typeof(FigmaCss2ReactInlineStyleConverterView));
    public static readonly PageRouteInfo LiveEditor = new("/LiveEditor", typeof(HtmlToCSharpView));
    public static readonly PageRouteInfo LivePreview = new($"/{nameof(LivePreview)}", typeof(LivePreview));
    public static readonly PageRouteInfo DemoPreview = new($"/{nameof(DemoPreview)}", typeof(DemoPreview));
    public static readonly PageRouteInfo PageMilestones = new($"/{nameof(PageMilestones)}", typeof(PageMilestones));
    public static readonly PageRouteInfo PageShowcase = new($"/{nameof(Pages.PageShowcase)}", typeof(PageShowcase));
    public static readonly PageRouteInfo PageModifiers = new($"/{nameof(Pages.PageModifiers)}", typeof(PageModifiers));
    public static readonly PageRouteInfo PageTechnicalDetail = new($"/{nameof(Pages.PageTechnicalDetail)}", typeof(PageTechnicalDetail));
    
    

    public static string LivePreviewUrl(string guid)
    {
        return Page.LivePreview.Url + $"?{ReactWithDotNet.WebSite.Components.LivePreview.QueryParameterNameOfGuid}={guid}";
    }
    
    public static string DemoPreviewUrl(string fullTypeName)
    {
        return Page.DemoPreview.Url + $"?{ReactWithDotNet.WebSite.Pages.DemoPreview.QueryParameterNameOfFullTypeName}={fullTypeName}";
    }
    
    public static string DocDetailUrl(string part)
    {
        return DocDetail.Url + part;
    }

    
}