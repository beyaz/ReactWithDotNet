using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace ReactWithDotNet.Demo;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddControllers().AddJsonOptions(j => { j.JsonSerializerOptions.ModifyForReactWithDotNet(); });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseFileServer(new FileServerOptions
        {
            FileProvider       = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "")),
            EnableDefaultFiles = true,
            StaticFileOptions =
            {
                HttpsCompression  = HttpsCompressionMode.Compress,
                OnPrepareResponse = _ => { }
            }
        });

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}