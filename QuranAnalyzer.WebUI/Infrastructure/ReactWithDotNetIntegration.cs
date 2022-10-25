using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuranAnalyzer.WebUI;

static class ReactWithDotNetIntegration
{
    public static async Task HandleReactWithDotNetRequest(HttpContext context)
    {
        ComponentRequest componentRequest = null;

        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
        {
            componentRequest = (ComponentRequest)Json.DeserializeJsonByNewtonsoft(await reader.ReadToEndAsync(), typeof(ComponentRequest));
        }

        var response = ComponentRequestHandler.HandleRequest(componentRequest, Type.GetType);

        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(response.ToJson());
    }

    public static async Task HomePage(HttpContext context)
    {
        var htmlContent = await GetIndexHtmlFileContent();

        context.Response.ContentType = "text/html; charset=UTF-8";

        await context.Response.WriteAsync(htmlContent);
    }

    public static async Task UIDesignerComponentPreview(HttpContext context)
    {
        var htmlContent = await GetIndexHtmlFileContent();

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

    public static async Task UIDesignerPage(HttpContext context)
    {
        var htmlContent = await GetIndexHtmlFileContent();

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

    static async Task<string> GetIndexHtmlFileContent()
    {
        const string RootFolderName = "wwwroot";

        var filePath = Path.Combine(RootFolderName, "index.html");

        var htmlContent = await File.ReadAllTextAsync(filePath);

        htmlContent = htmlContent.Replace("~/", RootFolderName + "/");

        return htmlContent;
    }
}