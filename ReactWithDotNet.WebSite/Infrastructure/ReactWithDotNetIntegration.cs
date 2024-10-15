using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;

namespace ReactWithDotNet.WebSite;

public static class ReactWithDotNetIntegration
{
    public static void ConfigureReactWithDotNet(this WebApplication app)
    {
        var map = typeof(Page)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(f => f.FieldType == typeof(PageRouteInfo))
            .Select(f => (PageRouteInfo)f.GetValue(null))
            .Where(x => x is not null)
            .ToDictionary(x => x.Url, x => x, StringComparer.OrdinalIgnoreCase);

        RequestHandlerPath = "/"+nameof(HandleReactWithDotNetRequest);
       
        app.Use(async (httpContext, next) =>
        {   
            var path = httpContext.Request.Path.Value ?? string.Empty;

            if (path == RequestHandlerPath)
            {
                await HandleReactWithDotNetRequest(httpContext);
                return;
            }

            if (map.TryGetValue(path, out var routeInfo))
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), routeInfo.page);
                return;
            }
            
            if (path == "/UploadFile")
            {
                await UploadFileAndWriteResponse(httpContext);
                return;
            }
#if DEBUG
            if (path == ReactWithDotNetDesigner.UrlPath)
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesigner));
                return;
            }
#endif

            await next();
        });
    }

    static Task HandleReactWithDotNetRequest(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        return ProcessReactWithDotNetComponentRequest(new()
        {
            HttpContext           = httpContext,
            OnReactContextCreated = OnReactContextCreated
        });
    }

    static async Task UploadFileAndWriteResponse(HttpContext httpContext)
    {
        var uploadResult = await UploadFile(httpContext);

        await uploadResult.ExecuteAsync(httpContext);
    }
    static async Task<IResult> UploadFile(HttpContext httpContext)
    {
        var request = httpContext.Request;
        if (!request.HasFormContentType)
        {
            return Results.BadRequest("The request doesn't contain form content type");
        }

        var form = await request.ReadFormAsync();
        var file = form.Files["file"];
        if (file == null || file.Length == 0)
        {
            return Results.BadRequest("The file is empty or not provided");
        }

        var filePath = Path.Combine(@"C:\Users\beyaz\Downloads\", file.FileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Results.Ok(new { FilePath = filePath });
    }

    static Task OnReactContextCreated(HttpContext httpContext, ReactContext context)
    {
        context.Set(ThemeKey, new());

        return Task.CompletedTask;
    }

    static Task WriteHtmlResponse(HttpContext httpContext, Type layoutType, Type mainContentType)
    {
        httpContext.Response.ContentType = "text/html; charset=UTF-8";

        httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
        httpContext.Response.Headers[HeaderNames.Expires]      = "0";
        httpContext.Response.Headers[HeaderNames.Pragma]       = "no-cache";

        return ProcessReactWithDotNetPageRequest(new()
        {
            LayoutType            = layoutType,
            MainContentType       = mainContentType,
            HttpContext           = httpContext,
            QueryString           = httpContext.Request.QueryString.ToString(),
            OnReactContextCreated = OnReactContextCreated
        });
    }
}