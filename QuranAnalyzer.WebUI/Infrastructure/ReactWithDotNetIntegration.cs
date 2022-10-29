using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace QuranAnalyzer.WebUI;

static class ReactWithDotNetIntegration
{
    public static void ConfigureReactWithDotNet(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", HomePage);
        endpoints.MapPost($"/{nameof(HandleReactWithDotNetRequest)}", HandleReactWithDotNetRequest);
        endpoints.MapGet($"/{nameof(ReactWithDotNetDesigner)}", ReactWithDotNetDesigner);
        endpoints.MapGet($"/{nameof(ReactWithDotNetDesignerComponentPreview)}", ReactWithDotNetDesignerComponentPreview);
    }

    static async Task<string> GetIndexHtmlFileContent()
    {
        const string RootFolderName = "wwwroot";

        var filePath = Path.Combine(RootFolderName, "index.html");

        var htmlContent = await File.ReadAllTextAsync(filePath);

        htmlContent = htmlContent.Replace("~/", RootFolderName + "/");

        return htmlContent;
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
        var file = new IndexFileContent
        {
            RootFolderName = "wwwroot",
            Component      = typeof(Pages.MainPage.View)
        };

        var htmlContent = file.GetFileContent();

        context.Response.ContentType = "text/html; charset=UTF-8";

        await context.Response.WriteAsync(htmlContent);
    }

    static async Task ReactWithDotNetDesigner(HttpContext context)
    {
        context.Response.ContentType = "text/html; charset=UTF-8";

        var file = new IndexFileContent
        {
            RootFolderName = "wwwroot",
            Component      = typeof(ReactWithDotNet.UIDesigner.ReactWithDotNetDesigner),
            
            Head=new []
            {
                "<link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/themes/saga-blue/theme.css'>",
                "<link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/primereact.min.css'>",
                "<link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/primeicons@5.0.0/primeicons.css'>"
            }
        };

        var htmlContent = file.GetFileContent();

        await context.Response.WriteAsync(htmlContent);
    }

    static async Task ReactWithDotNetDesignerComponentPreview(HttpContext context)
    {
        context.Response.ContentType = "text/html; charset=UTF-8";
        
        var file = new IndexFileContent
        {
            RootFolderName = "wwwroot",
            Component      = typeof(ReactWithDotNet.UIDesigner.ReactWithDotNetDesignerComponentPreview)
        };

        var htmlContent = file.GetFileContent();

        await context.Response.WriteAsync(htmlContent);
    }
}