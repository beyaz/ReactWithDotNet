using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.Libraries.mui.material;
using ReactWithDotNet.UIDesigner;


namespace ReactWithDotNet.WebSite;

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
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        var jsonText = await CalculateRenderInfo(new CalculateRenderInfoInput
        {
            HttpContext = httpContext,
            OnReactContextCreated = InitializeTheme
        });

        await httpContext.Response.WriteAsync(jsonText);
    }

    static async Task HomePage(HttpContext httpContext)
    {
        await WriteHtmlResponse(httpContext, new MainLayout
        {
            Page        = new MainWindow(),
            QueryString = httpContext.Request.QueryString.ToString()
        });
    }

    static async Task ReactWithDotNetDesigner(HttpContext httpContext)
    {
        await WriteHtmlResponse(httpContext, new MainLayout
        {
            Page = new ReactWithDotNetDesigner()
        });
    }

    static async Task ReactWithDotNetDesignerComponentPreview(HttpContext httpContext)
    {
        await WriteHtmlResponse(httpContext, new MainLayout
        {
            Page = new ReactWithDotNetDesignerComponentPreview()
        });
    }

    static async Task WriteHtmlResponse(HttpContext httpContext, MainLayout mainLayout)
    {
        httpContext.Response.ContentType = "text/html; charset=UTF-8";

        httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
        httpContext.Response.Headers[HeaderNames.Expires] = "0";
        httpContext.Response.Headers[HeaderNames.Pragma] = "no-cache";

        mainLayout.RenderInfo = await CalculateRenderInfo(mainLayout.Page, mainLayout.QueryString, InitializeTheme);

       
        
        var html = await CalculateHtmlText(new CalculateHtmlTextInput
        {
            ReactComponent = mainLayout,
            QueryString    = httpContext.Request.QueryString.ToString(),
            OnReactContextCreated = InitializeTheme
        });

        await httpContext.Response.WriteAsync(html);
    }

    static Task InitializeTheme(ReactContext context)
    {
        context.Set(Theme, new ColorPaletteLight());
        return Task.CompletedTask;
    }
}