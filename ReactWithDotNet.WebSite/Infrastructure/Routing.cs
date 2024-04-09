using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;
using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public sealed record RouteInfo(string pattern, Type page);

public static class Routing
{
    public static readonly RouteInfo CSharpPropertyMapper = new("/CSharpPropertyMapper", typeof(CSharpPropertyMapperView));
    public static readonly RouteInfo Doc = new("/doc", typeof(PageDocumentation));
    public static readonly RouteInfo DocDetail = new("/doc/", typeof(PageDocumentation));
    public static readonly RouteInfo Home = new("/", typeof(MainWindow));
    public static readonly RouteInfo ImportFigmaCss = new("/importFigmaCss", typeof(FigmaCss2ReactInlineStyleConverterView));
    public static readonly RouteInfo LiveEditor = new("/LiveEditor", typeof(HtmlToCSharpView));

    static readonly Dictionary<string, RouteInfo> Map = new[]
    {
        Home,
        Doc,
        DocDetail,
        LiveEditor,
        CSharpPropertyMapper,
        ImportFigmaCss
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

            if (path.StartsWith(DocDetail.pattern))
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), DocDetail.page);
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
        return DocDetail.pattern + part;
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