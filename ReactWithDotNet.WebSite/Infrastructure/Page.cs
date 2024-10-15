using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

sealed record PageRouteInfo(string Url, Type page);

static class Page
{
 
    public static readonly PageRouteInfo Home = new("/", typeof(PageMain));
    public static readonly PageRouteInfo LiveEditor = new("/LiveEditor", typeof(HtmlToCSharpView));
    public static readonly PageRouteInfo LivePreview = new($"/{nameof(LivePreview)}", typeof(LivePreview));
    public static readonly PageRouteInfo DemoPreview = new($"/{nameof(DemoPreview)}", typeof(DemoPreview));
    public static readonly PageRouteInfo PageMilestones = new($"/{nameof(PageMilestones)}", typeof(PageMilestones));
    public static readonly PageRouteInfo PageShowcase = new($"/{nameof(Pages.PageShowcase)}", typeof(PageShowcase));
    public static readonly PageRouteInfo PageModifiers = new($"/{nameof(Pages.PageModifiers)}", typeof(PageModifiers));
    public static readonly PageRouteInfo PageTechnicalDetail = new($"/{nameof(Pages.PageTechnicalDetail)}", typeof(PageTechnicalDetail));
    
    
    public static readonly PageRouteInfo DocStart = new("/doc/start", typeof(PageDocumentation_Start));
    public static readonly PageRouteInfo DocServerDrivenUI = new("/doc/server_driven_ui", typeof(PageDocumentation_ServerDrivenUI));
    

    public static string LivePreviewUrl(string guid)
    {
        return Page.LivePreview.Url + $"?{ReactWithDotNet.WebSite.Components.LivePreview.QueryParameterNameOfGuid}={guid}";
    }
    
    public static string DemoPreviewUrl(string fullTypeName)
    {
        return Page.DemoPreview.Url + $"?{ReactWithDotNet.WebSite.Pages.DemoPreview.QueryParameterNameOfFullTypeName}={fullTypeName}";
    }
    


    
}