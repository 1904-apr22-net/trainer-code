using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MovieApp.UI
{

    // 1. created ASP.NET Web Application - MVC template
    // 2. created class libraries for business logic layer and data access layer
    // 3. created classes for my business logic, and a repository interface
    // 4. created repository implementation in the data access layer,
    //    based on an in-memory list.
    // 5. in the MVC project, i made a new movie controller through right-click -> Add.
    // 6. made view models to be the models for my views.
    // 7. i made a view for the Movie/Index action, using right-click (on action)
    //    -> Add. used List template on the MovieViewModel object.
    //    (without VS, we can use some dotnet CLI templates with
    //    tool "aspnet-codegenerator")
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
