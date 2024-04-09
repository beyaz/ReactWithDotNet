using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;
using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public sealed record PageRouteInfo(string pattern, Type page);

static class Page
{
    public static readonly PageRouteInfo CSharpPropertyMapper = new("/CSharpPropertyMapper", typeof(CSharpPropertyMapperView));
    public static readonly PageRouteInfo Doc = new("/doc", typeof(PageDocumentation));
    public static readonly PageRouteInfo DocDetail = new("/doc/", typeof(PageDocumentation));
    public static readonly PageRouteInfo Home = new("/", typeof(MainWindow));
    public static readonly PageRouteInfo ImportFigmaCss = new("/importFigmaCss", typeof(FigmaCss2ReactInlineStyleConverterView));
    public static readonly PageRouteInfo LiveEditor = new("/LiveEditor", typeof(HtmlToCSharpView));
}

public static class Router
{
    

    static readonly Dictionary<string, PageRouteInfo> Map = new[]
    {
        Page.Home,
        Page.Doc,
        Page.DocDetail,
        Page.LiveEditor,
        Page.CSharpPropertyMapper,
        Page.ImportFigmaCss
    }.ToDictionary(x => x.pattern, x => x, StringComparer.OrdinalIgnoreCase);

    public static void ConfigureReactWithDotNet(this WebApplication app)
    {
        app.Use(async (httpContext, next) =>
        {
            var path = httpContext.Request.Path.Value ?? string.Empty;

            if (path == $"/{nameof(HandleReactWithDotNetRequest)}")
            {
                await HandleReactWithDotNetRequest(httpContext);
                return;
            }

            if (Map.TryGetValue(path, out var routeInfo))
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), routeInfo.page);
                return;
            }

            if (path.StartsWith(Page.DocDetail.pattern))
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), Page.DocDetail.page);
                return;
            }

#if DEBUG
            if (path == $"/{nameof(ReactWithDotNetDesigner)}")
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesigner));
                return;
            }

            if (path == $"/{nameof(ReactWithDotNetDesignerComponentPreview)}")
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesignerComponentPreview));
                return;
            }
#endif

            await next();
        });
    }

    public static string DocDetailUrl(string part)
    {
        return Page.DocDetail.pattern + part;
    }

    static Task HandleReactWithDotNetRequest(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        return ProcessReactWithDotNetComponentRequest(new()
        {
            HttpContext           = httpContext,
            OnReactContextCreated = OnReactContextCreated
        });
    }

    static Task OnReactContextCreated(HttpContext httpContext, ReactContext context)
    {
        context.Set(ThemeKey, new());

        KeyForHttpContext[context] = httpContext;

        return Task.CompletedTask;
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
            OnReactContextCreated = OnReactContextCreated
        });
    }
}