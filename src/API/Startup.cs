using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // global policy - assign here or on each controller
            app.UseCors("CorsPolicy");

            // app.Use(async (context, next) =>
            // {
            //     if (context.Request.Path.ToString().Equals("/sse"))
            //     {
            //         var response = context.Response;
            //         response.Headers.Add("Content-Type", "text/event-stream");

            //         for (var i = 0; true; ++i)
            //         {
            //             // WriteAsync requires `using Microsoft.AspNetCore.Http`
            //             await response.WriteAsync($"data: Middleware {i} at {DateTime.Now}\r\r");

            //             response.Body.Flush();
            //             await Task.Delay(5 * 1000);
            //         }
            //     }

            //     await next.Invoke();
            // });


            //Add CORS middleware before MVC
            //app.UseCors("AllowAll");


            app.UseMvc();
        }
    }
}
