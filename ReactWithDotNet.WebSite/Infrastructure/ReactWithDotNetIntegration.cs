using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;
using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

static class ReactWithDotNetIntegration
{
    public static void ConfigureReactWithDotNet(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", HomePage);
        endpoints.MapPost("/" + nameof(HandleReactWithDotNetRequest), HandleReactWithDotNetRequest);

        endpoints.MapGet("/LiveEditor", httpContext => WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(HtmlToCSharpView)));
        endpoints.MapGet("/CSharpPropertyMapper", httpContext => WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(CSharpPropertyMapperView)));
        endpoints.MapGet("/importFigmaCss", httpContext => WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(FigmaCss2ReactInlineStyleConverterView)));
        
        endpoints.MapGet("/doc/{0}", httpContext => WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(PageDocumentation)));
        
        RegisterReactWithDotNetDevelopmentTools(endpoints);
    }

    static Task HandleReactWithDotNetRequest(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        return ProcessReactWithDotNetComponentRequest(new ()
        {
            HttpContext           = httpContext,
            OnReactContextCreated = InitializeTheme
        });
    }

    static Task HomePage(HttpContext httpContext)
    {
        return WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(MainWindow));
    }

    static Task InitializeTheme(HttpContext httpContext, ReactContext context)
    {
        context.Set(ThemeKey, new LightTheme());

        KeyForHttpContext[context] = httpContext;

        return Task.CompletedTask;
    }

    [Conditional("DEBUG")]
    static void RegisterReactWithDotNetDevelopmentTools(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/$", httpContext =>
        {
            ReactWithDotNetDesigner.IsAttached = true;

            return WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesigner));
        });
        endpoints.MapGet("/" + nameof(ReactWithDotNetDesignerComponentPreview), httpContext =>
        {
            ReactWithDotNetDesigner.IsAttached = true;

            return WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesignerComponentPreview));
        });
    }

    static Task WriteHtmlResponse(HttpContext httpContext, Type layoutType, Type mainContentType)
    {
        httpContext.Response.ContentType = "text/html; charset=UTF-8";

        httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
        httpContext.Response.Headers[HeaderNames.Expires]      = "0";
        httpContext.Response.Headers[HeaderNames.Pragma]       = "no-cache";

        return ProcessReactWithDotNetPageRequest(new ()
        {
            LayoutType            = layoutType,
            MainContentType       = mainContentType,
            HttpContext           = httpContext,
            QueryString           = httpContext.Request.QueryString.ToString(),
            OnReactContextCreated = InitializeTheme
        });
    }
}