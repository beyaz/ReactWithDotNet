using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using QuranAnalyzer.WebUI.Pages.MainPage;
using ReactWithDotNet.UIDesigner;

namespace QuranAnalyzer.WebUI;

static class ReactWithDotNetIntegration
{
    public static void ConfigureReactWithDotNet(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", HomePage);
        endpoints.MapPost("/" + nameof(HandleReactWithDotNetRequest), HandleReactWithDotNetRequest);
        endpoints.MapGet("/" + nameof(ReactWithDotNetDesigner), ReactWithDotNetDesigner);
        endpoints.MapGet("/" + nameof(ReactWithDotNetDesignerComponentPreview), ReactWithDotNetDesignerComponentPreview);
    }

    static async Task HandleReactWithDotNetRequest(HttpContext httpContext)
    {
        var input = new ProcessReactWithDotNetRequestInput
        {
            HttpContext = httpContext
        };
        var componentResponse = await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);

        httpContext.Response.ContentType = "application/json; charset=utf-8";

        await httpContext.Response.WriteAsync(componentResponse.ToJson());
    }

    static async Task HomePage(HttpContext httpContext)
    {
        await httpContext.WriteHtmlResponse(new MainLayout
        {
            Page = new View()
        });
    }

    static async Task ReactWithDotNetDesigner(HttpContext httpContext)
    {
        await httpContext.WriteHtmlResponse(new MainLayout
        {
            Page = new ReactWithDotNetDesigner()
        });
    }

    static async Task ReactWithDotNetDesignerComponentPreview(HttpContext httpContext)
    {
        await httpContext.WriteHtmlResponse(new MainLayout
        {
            Page = new ReactWithDotNetDesignerComponentPreview()
        });
    }

    static async Task WriteHtmlResponse(this HttpContext httpContext, MainLayout mainLayout)
    {
        httpContext.Response.ContentType = "text/html; charset=UTF-8";

        httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
        httpContext.Response.Headers[HeaderNames.Expires]      = "0";
        httpContext.Response.Headers[HeaderNames.Pragma]       = "no-cache";

        var input = new ProcessReactWithDotNetRequestInput
        {
            HttpContext = httpContext,
            componentRequest = new ComponentRequest
            {
                MethodName                        = "FetchComponent",
                FullName                          = mainLayout.GetType().GetFullName(),
                LastUsedComponentUniqueIdentifier = 1,
                ComponentUniqueIdentifier         = 1
            }
        };

        var componentResponse = await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);

        var reactContext = ReactContext.Create(httpContext.Request.QueryString.ToString(), 500, 500);

        var htmlContent = mainLayout.ToString(reactContext);

        await httpContext.Response.WriteAsync(htmlContent);
    }
}