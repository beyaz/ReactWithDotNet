using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

sealed record PageRouteInfo(string Url, Type page);

static class Page
{
    // H o m e
    public static readonly PageRouteInfo Home = new("/", typeof(PageMain));
    
    // H o m e   b u t t o n s
    public static readonly PageRouteInfo Milestones = new($"/{nameof(Milestones)}", typeof(PageMilestones));
    public static readonly PageRouteInfo Showcase = new($"/{nameof(Showcase)}", typeof(PageShowcase));
    
    // T e c h n i c a l
    public static readonly PageRouteInfo TechnicalDetail = new($"/{nameof(TechnicalDetail)}", typeof(PageTechnicalDetail));
    public static readonly PageRouteInfo Modifiers = new($"/{nameof(Modifiers)}", typeof(PageModifiers));
    public static readonly PageRouteInfo ReactContexts = new($"/{nameof(ReactContexts)}", typeof(PageReactContexts));
    
    // D o c
    public static readonly PageRouteInfo DocStart = new("/doc/start", typeof(PageDocumentation_Start));
    public static readonly PageRouteInfo DocSetup = new("/doc/setup", typeof(PageDocumentation_Setup));
    
    // H e l p e r   A p p s
    public static readonly PageRouteInfo LiveEditor = new("/LiveEditor", typeof(HtmlToCSharpView));
    public static readonly PageRouteInfo LivePreview = new($"/{nameof(LivePreview)}", typeof(LivePreview));
    public static readonly PageRouteInfo Designer = new($"/{nameof(Designer)}", typeof(PageDesigner));
    
    // i n t e r n a l
    public static readonly PageRouteInfo DemoPreview = new($"/{nameof(DemoPreview)}", typeof(DemoPreview));
    

    public static string DemoPreviewUrl(string fullTypeName)
    {
        return DemoPreview.Url + $"?{Pages.DemoPreview.QueryParameterNameOfFullTypeName}={fullTypeName}";
    }

    public static string LivePreviewUrl(string guid)
    {
        return LivePreview.Url + $"?{Components.LivePreview.QueryParameterNameOfGuid}={guid}";
    }
    


    
}