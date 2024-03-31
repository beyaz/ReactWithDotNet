using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;
using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public class PageList
{
    public static readonly (string pattern, Type type) Home = ("/", typeof(MainWindow));
    public static readonly (string pattern, Type type) Doc = ("/doc", typeof(PageDocumentation));
    public static readonly (string pattern, Type type) DocDetail = ("/doc/{0}", typeof(PageDocumentation));
    public static readonly (string pattern, Type type) LiveEditor = ("/LiveEditor", typeof(HtmlToCSharpView));
}

static class ReactWithDotNetIntegration
{
    public static void ConfigureReactWithDotNet(this IEndpointRouteBuilder endpoints)
    {
        var routing = new[]
        {
            PageList.Home,
            PageList.Doc,
            PageList.DocDetail,
            PageList.LiveEditor,
            ("/CSharpPropertyMapper", typeof(CSharpPropertyMapperView)),
            ("/importFigmaCss", typeof(FigmaCss2ReactInlineStyleConverterView))
        };

        foreach (var (pattern, cmp) in routing)
        {
            endpoints.MapGet(pattern, httpContext => WriteHtmlResponse(httpContext, typeof(MainLayout), cmp));
        }

        RegisterReactWithDotNetDevelopmentTools(endpoints);

        endpoints.MapPost("/" + nameof(HandleReactWithDotNetRequest), HandleReactWithDotNetRequest);
    }

    static Task HandleReactWithDotNetRequest(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        return ProcessReactWithDotNetComponentRequest(new()
        {
            HttpContext           = httpContext,
            OnReactContextCreated = InitializeTheme
        });
    }

    static Task InitializeTheme(HttpContext httpContext, ReactContext context)
    {
        context.Set(ThemeKey, new());

        KeyForHttpContext[context] = httpContext;

        return Task.CompletedTask;
    }

    [Conditional("DEBUG")]
    static void RegisterReactWithDotNetDevelopmentTools(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/$", httpContext => WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesigner)));
        endpoints.MapGet("/" + nameof(ReactWithDotNetDesignerComponentPreview), httpContext => WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesignerComponentPreview)));
    }

    static Task WriteHtmlResponse(HttpContext httpContext, Type layoutType, Type mainContentType)
    {
        httpContext.Response.ContentType = "text/html; charset=UTF-8";

        httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
        httpContext.Response.Headers[HeaderNames.Expires]      = "0";
        httpContext.Response.Headers[HeaderNames.Pragma]       = "no-cache";

        return ProcessReactWithDotNetPageRequest(new()
        {
            LayoutType            = layoutType,
            MainContentType       = mainContentType,
            HttpContext           = httpContext,
            QueryString           = httpContext.Request.QueryString.ToString(),
            OnReactContextCreated = InitializeTheme
        });
    }
}