using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static QuranAnalyzer.WebUI.ReactWithDotNetIntegration;

namespace QuranAnalyzer.WebUI;

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

        app.UseStaticFiles("/wwwroot");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", HomePage);
            endpoints.MapPost("/HandleReactWithDotNetRequest", HandleReactWithDotNetRequest);
            endpoints.MapGet("/ReactWithDotNetDesigner", UIDesignerPage);
            endpoints.MapGet("/ReactWithDotNetDesigner.ComponentPreview", UIDesignerComponentPreview);

            endpoints.MapControllers();
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddControllers().AddJsonOptions(j => { j.JsonSerializerOptions.ModifyForReactWithDotNet(); });
    }
}