using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;
using ReactWithDotNet.WebSite.HelperApps;

namespace ReactWithDotNet.WebSite;

static class ReactWithDotNetIntegration
{
    public static void ConfigureReactWithDotNet(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", HomePage);
        endpoints.MapPost("/" + nameof(HandleReactWithDotNetRequest), HandleReactWithDotNetRequest);

        endpoints.MapGet("/LiveEditor", async httpContext => { await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(HtmlToCSharpView)); });
        endpoints.MapGet("/CSharpPropertyMapper", async httpContext => { await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(CSharpPropertyMapperView)); });
        
#if DEBUG // this two endpoints should use only development mode

        endpoints.MapGet("/" + nameof(ReactWithDotNetDesigner), async httpContext =>
        {
            ReactWithDotNetDesigner.IsAttached = true;

            await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesigner));
        });
        endpoints.MapGet("/" + nameof(ReactWithDotNetDesignerComponentPreview), async httpContext =>
        {
            ReactWithDotNetDesigner.IsAttached = true;

            await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesignerComponentPreview));
        });
#endif
    }

    static async Task HandleReactWithDotNetRequest(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        var jsonText = await CalculateRenderInfo(new CalculateRenderInfoInput
        {
            HttpContext           = httpContext,
            OnReactContextCreated = InitializeTheme
        });

        await httpContext.Response.WriteAsync(jsonText);
    }

    static async Task HomePage(HttpContext httpContext)
    {
        await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(MainWindow));
    }

    static Task InitializeTheme(HttpContext httpContext, ReactContext context)
    {
        context.Set(ThemeKey, new LightTheme());

        KeyForHttpContext[context] = httpContext;
        
        return Task.CompletedTask;
    }

    static async Task WriteHtmlResponse(HttpContext httpContext, Type layoutType, Type mainContentType)
    {
        httpContext.Response.ContentType = "text/html; charset=UTF-8";

        httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
        httpContext.Response.Headers[HeaderNames.Expires]      = "0";
        httpContext.Response.Headers[HeaderNames.Pragma]       = "no-cache";

        var html = await CalculateFirstRender(new CalculateFirstRenderInput
        {
            LayoutType            = layoutType,
            MainContentType       = mainContentType,
            HttpContext           = httpContext,
            QueryString           = httpContext.Request.QueryString.ToString(),
            OnReactContextCreated = InitializeTheme
        });

        await httpContext.Response.WriteAsync(html);
    }
}