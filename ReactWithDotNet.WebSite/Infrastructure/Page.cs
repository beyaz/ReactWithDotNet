using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

sealed record PageRouteInfo(string Url, Type page);

static class Page
{
 
    public static readonly PageRouteInfo Home = new("/", typeof(PageMain));
  
    public static readonly PageRouteInfo DemoPreview = new($"/{nameof(DemoPreview)}", typeof(DemoPreview));
    public static readonly PageRouteInfo Milestones = new($"/{nameof(Milestones)}", typeof(PageMilestones));
    public static readonly PageRouteInfo Showcase = new($"/{nameof(PageShowcase)}", typeof(PageShowcase));
   
    
    public static readonly PageRouteInfo PageTechnicalDetail = new($"/{nameof(Pages.PageTechnicalDetail)}", typeof(PageTechnicalDetail));
    public static readonly PageRouteInfo Modifiers = new($"/{nameof(PageModifiers)}", typeof(PageModifiers));
    public static readonly PageRouteInfo ReactContexts = new($"/{nameof(ReactContexts)}", typeof(PageReactContexts));
    
    // D o c
    public static readonly PageRouteInfo DocStart = new("/doc/start", typeof(PageDocumentation_Start));
    public static readonly PageRouteInfo DocSetup = new("/doc/setup", typeof(PageDocumentation_Setup));
    
    // H e l p e r   A p p s
    public static readonly PageRouteInfo LiveEditor = new("/LiveEditor", typeof(HtmlToCSharpView));
    public static readonly PageRouteInfo LivePreview = new($"/{nameof(LivePreview)}", typeof(LivePreview));
    public static readonly PageRouteInfo PageDesigner = new($"/{nameof(Pages.PageDesigner)}", typeof(PageDesigner));
    

    public static string DemoPreviewUrl(string fullTypeName)
    {
        return DemoPreview.Url + $"?{Pages.DemoPreview.QueryParameterNameOfFullTypeName}={fullTypeName}";
    }

    public static string LivePreviewUrl(string guid)
    {
        return LivePreview.Url + $"?{Components.LivePreview.QueryParameterNameOfGuid}={guid}";
    }
    


    
}