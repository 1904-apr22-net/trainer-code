﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogRestService.API.Repositories;
using DogRestService.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DogRestService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string AllowAngularAllMethods = "_AllowLocalAngularAllMethods";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DogRepository>();
            services.AddDbContext<DogDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DogDb")));

            //services.AddAuthentication(sharedOptions =>
            //{
            //    sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            //    .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            services.AddCors(options =>
            {
                options.AddPolicy(AllowAngularAllMethods,
                builder =>
                {
                    // i would also need to allow the domain of
                    // my deployed angular app.
                    builder.WithOrigins("http://localhost:4200", "http://localhost:9200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            })
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(AllowAngularAllMethods);

            app.UseHttpsRedirection();

            //app.UseAuthentication();

            app.UseMvc();
        }
    }
}
