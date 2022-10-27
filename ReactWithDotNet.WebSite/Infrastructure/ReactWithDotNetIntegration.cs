using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuranAnalyzer.WebUI;

static class ReactWithDotNetIntegration
{
    public const string RootFolderName = "wwwroot";


    public static async Task HomePage(HttpContext context)
    {
        var filePath = Path.Combine(RootFolderName, "index.html");

        var htmlContent = await File.ReadAllTextAsync(filePath);

        htmlContent = htmlContent.Replace("~/", RootFolderName + "/");

        context.Response.ContentType = "text/html; charset=UTF-8";
        
        await context.Response.WriteAsync(htmlContent);
    }
    
    public static async Task UIDesignerPage(HttpContext context)
    {
        var filePath = Path.Combine(RootFolderName, "index.html");

        var htmlContent = await File.ReadAllTextAsync(filePath);

        htmlContent = htmlContent.Replace("~/", RootFolderName + "/");

        htmlContent = changeComponent(htmlContent);

        htmlContent = enablePrimeReactCssList(htmlContent);

        context.Response.ContentType = "text/html; charset=UTF-8";

        await context.Response.WriteAsync(htmlContent);

        static string changeComponent(string htmlContent)
        {
            return string.Join(Environment.NewLine, htmlContent.Split(Environment.NewLine).Select(line =>
            {
                if (line.Trim().Contains("fullTypeNameOfReactComponent"))
                {
                    return "fullTypeNameOfReactComponent: 'ReactWithDotNet.UIDesigner.UIDesignerView,ReactWithDotNet.Libraries',";
                }

                return line;
            }));
        }

        static string enablePrimeReactCssList(string htmlContent)
        {
            return htmlContent.Replace("<!--ReactWithDotNet-UIDesigner", string.Empty)
                              .Replace("ReactWithDotNet-UIDesigner-->", string.Empty);
        }
    }

    public static async Task UIDesignerComponentPreview(HttpContext context)
    {
        var filePath = Path.Combine(RootFolderName, "index.html");

        var htmlContent = await File.ReadAllTextAsync(filePath);

        htmlContent = htmlContent.Replace("~/", RootFolderName + "/");

        htmlContent = changeComponent();

        context.Response.ContentType = "text/html; charset=UTF-8";
        
        await context.Response.WriteAsync(htmlContent);

        string changeComponent()
        {
            return string.Join(Environment.NewLine, htmlContent.Split(Environment.NewLine).Select(line =>
            {
                if (line.Trim().Contains("fullTypeNameOfReactComponent"))
                {
                    return "fullTypeNameOfReactComponent: 'ReactWithDotNet.UIDesigner.ComponentPreivew,ReactWithDotNet.Libraries',";
                }

                return line;
            }));
        }
    }

    public static async Task HandleReactWithDotNetRequest(HttpContext context)
    {
        var input = new ProcessReactWithDotNetRequestInput
        {
            HttpContext = context
        };
        await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);
    }
}