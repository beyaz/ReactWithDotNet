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
        await context.WriteHtmlResponse(new MainLayout
        {
            Page = new View()
        });
    }

    static async Task ReactWithDotNetDesigner(HttpContext context)
    {
        await context.WriteHtmlResponse(new MainLayout
        {
            Page        = new ReactWithDotNetDesigner()
        });
    }

    static async Task ReactWithDotNetDesignerComponentPreview(HttpContext context)
    {
        await context.WriteHtmlResponse(new MainLayout
        {
            Page        = new ReactWithDotNetDesignerComponentPreview()
        });
    }

    static async Task WriteHtmlResponse(this HttpContext context, MainLayout mainLayout)
    {
        context.Response.ContentType = "text/html; charset=UTF-8";

        context.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
        context.Response.Headers[HeaderNames.Expires]      = "0";
        context.Response.Headers[HeaderNames.Pragma]       = "no-cache";


        var reactContext = ReactContext.Create(context.Request.QueryString.ToString(), 500, 500);
        
        var htmlContent  = mainLayout.ToString(reactContext);

        await context.Response.WriteAsync(htmlContent);
    }
}