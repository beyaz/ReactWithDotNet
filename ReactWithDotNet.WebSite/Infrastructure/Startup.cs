using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ReactWithDotNet.WebSite;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseStaticFiles(new StaticFileOptions
        {
            RequestPath         = new PathString("/wwwroot"),
            ContentTypeProvider = new Utf8CharsetContentTypeProvider()
        });

        app.UseResponseCompression();

        app.UseEndpoints(endpoints =>
        {
            endpoints.ConfigureReactWithDotNet();

            endpoints.MapControllers();
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.AddControllers().AddJsonOptions(jsonOptions => { jsonOptions.JsonSerializerOptions.ModifyForReactWithDotNet(); });
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