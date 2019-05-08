using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HelloMVC
{
    public class Program
    {
        // entry point for ASP.NET
        public static void Main(string[] args)
        {
            // creates a web host and runs it
            CreateWebHostBuilder(args).Build().Run();
        }

        // here we have a lot of default configuration
        // we do plug in logging in this file
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
