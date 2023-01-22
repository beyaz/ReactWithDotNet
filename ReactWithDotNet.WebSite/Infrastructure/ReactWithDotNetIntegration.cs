using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ReactWithDotNet.UIDesigner;
using ReactWithDotNet.WebSite;

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

    static async Task HomePage(HttpContext context)
    {
        await context.WriteHtmlResponse(new HtmlContentGenerator
        {
            TargetReactComponent = typeof(MainWindow)
        });
    }

    static async Task ReactWithDotNetDesigner(HttpContext httpContext)
    {
        await httpContext.WriteHtmlResponse(new HtmlContentGenerator
        {
            TargetReactComponent = typeof(ReactWithDotNetDesigner)
        });
    }

    static async Task ReactWithDotNetDesignerComponentPreview(HttpContext httpContext)
    {
        await httpContext.WriteHtmlResponse(new HtmlContentGenerator
        {
            TargetReactComponent = typeof(ReactWithDotNetDesignerComponentPreview)
        });
    }

    static async Task WriteHtmlResponse(this HttpContext httpContext, HtmlContentGenerator htmlContentGenerator)
    {
        httpContext.Response.ContentType = "text/html; charset=UTF-8";

        var htmlContent = htmlContentGenerator.GetHtmlContent();

        await httpContext.Response.WriteAsync(htmlContent);
    }
}