using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCoreWebApplication
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    //throw new Exception("Fout bij het afhandelen van request");
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware1: Binnenkomende Request\n");
//    await next();
//    await context.Response.WriteAsync("Middleware1: Uitgaande Response\n");
//});
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware2: Binnenkomende Request\n");
//    await next();
//    await context.Response.WriteAsync("Middleware2: Uitgaande Response\n");
//});
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Middleware3: Binnenkomende Request verwerkt en response teruggegeven\n");
//});
