using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.BL;
using MovieApp.DA;

namespace MovieApp.UI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // it is in Startup.ConfigureServices that we
            // register dependencies to be injected as constructor parameters
            // whenever the framework makes a class that requires those parameters.

            // this means: "anytime someone requests an IMovieRepository,
            // create a MovieRepository and give it to him."

            var connString = Configuration.GetConnectionString("MovieCodeFirstDb");

            // choose which implementation for the service at runtime!
            if (connString == null || Configuration.GetValue<bool>("UseInMemoryRepo"))
            {
                services.AddScoped<IMovieRepository, MovieRepository>();
            }
            else
            {
                services.AddScoped<IMovieRepository, MovieDbRepository>();

                services.AddDbContext<MovieDbContext>(options =>
                {
                    options.UseSqlServer(connString);
                });
                // that registers a DbContext with scoped lifetime.
                // (one instance per HTTP request.)
            }


            // the "scoped" part means... reuse the same MovieRepository instance
            // during a given HTTP request... but make a new one
            // for the next HTTP request
            // (i.e. we can configure service lifetime)

            // "anytime someone asks for a List<Movie>, give him this instance."

            // (singleton lifetime means, it's always the same object forever)
            var actionGenre = new Genre { Id = 1, Name = "Action" };
            services.AddSingleton(new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "Die Hard",
                    ReleaseDate = new DateTime(1988, 1, 1),
                    Genre = actionGenre
                }
            });

            // Transient lifetime means, every time the service is 
            // requested, a new instance is always constructed.


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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
