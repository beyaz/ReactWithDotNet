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