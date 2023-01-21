using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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

    static async Task HandleReactWithDotNetRequest(HttpContext context)
    {
        var input = new ProcessReactWithDotNetRequestInput
        {
            HttpContext = context
        };
        await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);
    }

    static async Task HomePage(HttpContext context)
    {
        await context.WriteHtmlResponse(new HtmlContentGenerator
        {
            TargetReactComponent = typeof(View),
            Page = new View(),
            HttpContext = context
        });
    }

    static async Task ReactWithDotNetDesigner(HttpContext context)
    {
        await context.WriteHtmlResponse(new HtmlContentGenerator
        {
            TargetReactComponent = typeof(ReactWithDotNetDesigner)
        });
    }

    static async Task ReactWithDotNetDesignerComponentPreview(HttpContext context)
    {
        await context.WriteHtmlResponse(new HtmlContentGenerator
        {
            TargetReactComponent = typeof(ReactWithDotNetDesignerComponentPreview)
        });
    }

    static async Task WriteHtmlResponse(this HttpContext context, HtmlContentGenerator htmlContentGenerator)
    {
        context.Response.ContentType = "text/html; charset=UTF-8";

        var htmlContent = htmlContentGenerator.GetHtmlContent();

        await context.Response.WriteAsync(htmlContent);
    }
}