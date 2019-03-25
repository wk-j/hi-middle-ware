using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Middleware {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.Use(async (context, next) => {
                var ok = context.Request.Headers.TryGetValue("Custom-Token", out var customToken);
                var parameter = context.Request.Query.Where(x => x.Key == "customToken").FirstOrDefault();
                if (ok) {
                    Console.WriteLine("Next ...");
                    context.Request.Headers.Add("Authorization", "Bearer " + customToken);
                    await next.Invoke();
                } else if (parameter.Key != null) {
                    Console.WriteLine("customToken - {0}", parameter.Value);
                    await next.Invoke();
                } else {
                    context.Response.Headers.Add("Hi", "Hi");
                    context.Response.StatusCode = StatusCodes.Status511NetworkAuthenticationRequired;
                    await context.Response.WriteAsync("Invalid token!");
                }
            });

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
