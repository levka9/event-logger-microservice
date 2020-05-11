using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventLogger.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nancy.Owin;
using Microsoft.EntityFrameworkCore;

namespace EventLogger
{
    public class Startup
    {
        IConfiguration configuration;

        public Startup(IConfiguration Configuration)
        {
            configuration = Configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options => 
            {
                options.AllowSynchronousIO = true;
            });

            services.AddDbContext<EventLoggerContext>(options => 
                            options.UseSqlServer(configuration.GetSection("ConnectionString:DevWork").Value));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseOwin(buildFunc => buildFunc.UseNancy());
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
