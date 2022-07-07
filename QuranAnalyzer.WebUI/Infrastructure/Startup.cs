using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;

namespace ReactDotNet.Html5.Demo
{

    

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

            
            services.AddControllers().AddJsonOptions(j =>
            {
                j.JsonSerializerOptions.ModifyForReactDotNet();
            });

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
                    HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,
                    OnPrepareResponse = (e) =>
                    {
                        e.ToString();
                    }
                }
            });

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
