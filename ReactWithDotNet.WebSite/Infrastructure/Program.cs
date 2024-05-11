using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ReactWithDotNet.WebSite;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        // C O N F I G U R E     S E R V I C E S
        services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
        services.Configure<GzipCompressionProviderOptions>(options => { options.Level   = CompressionLevel.Optimal; });
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        // C O N F I G U R E     A P P L I C A T I O N
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseStaticFiles(new StaticFileOptions
        {
            RequestPath         = new PathString("/wwwroot"),
            ContentTypeProvider = new Utf8CharsetContentTypeProvider(),
            OnPrepareResponse   = ctx => { ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={TimeSpan.FromMinutes(5).TotalSeconds}"); }
        });

        app.UseResponseCompression();

        app.ConfigureReactWithDotNet();

        app.Run();
    }
    
    class Utf8CharsetContentTypeProvider : IContentTypeProvider
    {
        readonly IContentTypeProvider _defaultProvider = new FileExtensionContentTypeProvider();

        public bool TryGetContentType(string subpath, out string contentType)
        {
            subpath = subpath.ToLower();

            if (subpath.EndsWith(".js"))
            {
                contentType = "application/javascript; charset=utf-8";
                return true;
            }

            if (subpath.EndsWith(".css"))
            {
                contentType = "text/css; charset=utf-8";
                return true;
            }

            return _defaultProvider.TryGetContentType(subpath, out contentType);
        }
    }
}