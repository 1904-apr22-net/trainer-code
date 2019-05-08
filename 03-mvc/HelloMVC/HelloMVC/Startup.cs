using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloMVC
{
    // this class is where most of our configuration goes
    // what dependencies we need, what extra things
    // to plug in to MVC do we want
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // ASP.NET reads in key-value-pair configuration from
        // a variety of sources: appsettings.json, user secrets, environment variables.
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // here we enable MVC middleware, with particular conventional routes.
            app.UseMvc(routes =>
            {
                // each MapRoute call adds one route.
                // when we recieve a request, we go through these one by one
                // until we find the first one that matches.
                routes.MapRoute(
                    name: "homepage",
                    template: "HomePage",
                    defaults: new { Controller = "Home", Action = "Index" });

                // if the controller or action is not provided by values in the
                // template, we need a "defaults" object to indicate them.

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
                // each route needs to somehow determine
                // 1. the controller name
                // 2. the action name
                // 3. optionally, some route parameters.
            });
        }
    }
}
