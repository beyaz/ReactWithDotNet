using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;

namespace ReactWithDotNet;

public sealed class ReactWithDotNetJavaScriptFiles
{
    readonly RequestDelegate _next;

    readonly ConcurrentDictionary<string, ContentInfo> Cache = new();

    public ReactWithDotNetJavaScriptFiles(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///     Default value is 5 minute.
    /// </summary>
    public static TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);

    public async Task InvokeAsync(HttpContext context)
    {
        var content = TryGetContentOfJsFile(context);
        if (content != null)
        {
            context.Response.ContentType = content.Type;

            context.Response.Headers.Append("Cache-Control", $"public, max-age={Timeout.TotalSeconds}");

            await context.Response.Body.WriteAsync(content.Data);

            return;
        }

        await _next(context);
    }

    ContentInfo TryGetContentOfJsFile(HttpContext context)
    {
        const string ContentTypeApplication_Javascript = "application/javascript";

        const string ContentTypeText_Css = "text/css";

        var httpRequest = context.Request;

        var requestPath = httpRequest.Path.Value ?? string.Empty;

        var isJsFile = requestPath.EndsWith(".js");

        var isCssFile = requestPath.EndsWith(".css");

        if (!(isJsFile || isCssFile))
        {
            return null;
        }

        if (Cache.TryGetValue(requestPath, out var fileContent))
        {
            return fileContent;
        }

        string resourceName = null;

        var isLocalMaintainer = Environment.UserName == "beyaz";

        // for my development
        if (requestPath.EndsWith(ReactWithDotNetDesignerComponentPreview.HelperJsFileResourceName))
        {
            if (isLocalMaintainer)
            {
                return new()
                {
                    Type = ContentTypeApplication_Javascript,
                    Data = File.ReadAllBytes(@"C:\github\ReactWithDotNet\ReactWithDotNet\UIDesigner\Resources\ComponentIndicator.js")
                };
            }

            resourceName = requestPath;
        }

        if (isLocalMaintainer && requestPath.StartsWith("/ReactWithDotNet/", StringComparison.OrdinalIgnoreCase))
        {
            const string distFolderPath = @"C:\github\ReactWithDotNet\ReactWithDotNet\JsClientEngine\dist\";
            return new()
            {
                Type = isJsFile ? ContentTypeApplication_Javascript : ContentTypeText_Css,
                Data = File.ReadAllBytes($"{distFolderPath}{requestPath.Split('/').Last()}")
            };
        }
        
        if (requestPath.EndsWith("/clr.js", StringComparison.OrdinalIgnoreCase) || 
            requestPath.EndsWith("/long.js", StringComparison.OrdinalIgnoreCase))
        {
            if (isLocalMaintainer)
            {
                return new()
                {
                    Type = ContentTypeApplication_Javascript,
                    Data = File.ReadAllBytes($@"C:\github\ReactWithDotNet\ReactWithDotNet\JsClientEngine\{requestPath.Split('/').Last()}")
                };
            }

            resourceName = requestPath;
        }

        if (resourceName is null)
        {
            if (requestPath.StartsWith("/ReactWithDotNet/", StringComparison.OrdinalIgnoreCase))
            {
                resourceName = "ReactWithDotNet.JsClientEngine.dist." + requestPath.RemoveFromStart("/ReactWithDotNet/");
            }

            if (resourceName is null)
            {
                var headers = httpRequest.Headers;
                if (headers.TryGetValue(HeaderNames.Referer, out var referer) &&
                    headers.TryGetValue(HeaderNames.Host, out var host) &&
                    referer[0] is not null &&
                    host[0] is not null)
                {
                    var path = referer[0][referer[0].IndexOf(host[0], StringComparison.OrdinalIgnoreCase)..];

                    if (path.Contains("/ReactWithDotNet/", StringComparison.OrdinalIgnoreCase))
                    {
                        resourceName = "ReactWithDotNet.JsClientEngine.dist." + requestPath.RemoveFromStart("/ReactWithDotNet/");
                    }
                }
            }
        }

        if (resourceName is null)
        {
            return null;
        }

        var assembly = Assembly.GetExecutingAssembly();

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream != null)
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    fileContent = new()
                    {
                        Data = binaryReader.ReadBytes((int)stream.Length),
                        Type = isJsFile ? ContentTypeApplication_Javascript : ContentTypeText_Css
                    };
                }
            }
        }

        if (fileContent is null)
        {
            return null;
        }

        Cache.TryAdd(requestPath, fileContent);

        return fileContent;
    }

    class ContentInfo
    {
        public required byte[] Data;
        public required string Type;
    }
}

partial class Mixin
{
    const string Version = "1.0.3";
    public static readonly string IndexJsFilePath = $"/ReactWithDotNet/index.js?v={Version}";
    public static readonly string IndexCssFilePath = $"/ReactWithDotNet/index.css?v={Version}";
}