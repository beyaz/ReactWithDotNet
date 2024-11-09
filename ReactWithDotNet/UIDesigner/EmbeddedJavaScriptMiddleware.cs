using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace ReactWithDotNet;

public sealed class EmbeddedJavaScriptMiddleware
{
    readonly RequestDelegate _next;

    public EmbeddedJavaScriptMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value?.EndsWith(".js", StringComparison.OrdinalIgnoreCase) is true)
        {
            
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = context.Request.Path.Value.RemoveFromStart("/");
            
            // for my development
            if (Environment.UserName == "beyaz" && resourceName =="ReactWithDotNet.UIDesigner.Resources.ComponentIndicator.js")
            {
                await using (var fileStream =File.OpenRead(@"C:\github\ReactWithDotNet\ReactWithDotNet\UIDesigner\Resources\ComponentIndicator.js"))
                {
                    context.Response.ContentType = "application/javascript";

                    await fileStream.CopyToAsync(context.Response.Body);

                    return;
                }
            }

            await using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    context.Response.ContentType = "application/javascript";

                    await stream.CopyToAsync(context.Response.Body);

                    return;
                }
            }
        }

        await _next(context); // Proceed to the next middleware
    }
}