using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ReactWithDotNet;

namespace QuranAnalyzer.WebUI;

static class ReactWithDotNetIntegration
{
    public const string RootFolderName = "wwwroot";


    public static async Task HomePage(HttpContext context)
    {
        var filePath = Path.Combine(RootFolderName, "index.html");

        var htmlContent = await File.ReadAllTextAsync(filePath);

        htmlContent = htmlContent.Replace("~/", RootFolderName + "/");

        await context.Response.WriteAsync(htmlContent);
    }
    
    public static async Task UIDesignerPage(HttpContext context)
    {
        var filePath = Path.Combine(RootFolderName, "index.html");

        var htmlContent = await File.ReadAllTextAsync(filePath);

        htmlContent = htmlContent.Replace("~/", RootFolderName + "/");

        htmlContent = changeComponent();

        await context.Response.WriteAsync(htmlContent);

        string changeComponent()
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
    }

    public static async Task UIDesignerComponentPreview(HttpContext context)
    {
        var filePath = Path.Combine(RootFolderName, "index.html");

        var htmlContent = await File.ReadAllTextAsync(filePath);

        htmlContent = htmlContent.Replace("~/", RootFolderName + "/");

        htmlContent = changeComponent();

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
        ComponentRequest componentRequest = null;

        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
        {
            componentRequest = (ComponentRequest)Json.DeserializeJsonByNewtonsoft(await reader.ReadToEndAsync(), typeof(ComponentRequest));
        }

        var response = ComponentRequestHandler.HandleRequest(componentRequest, Type.GetType);

        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(Mixin.ToJson(response));
    }
}