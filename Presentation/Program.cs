using System;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Template.API
{
    public class Program
    {
        public static string ApplicationName = "Template.API";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder => builder.AddJsonConsole(options =>
                {
                    options.IncludeScopes = false;
                    options.TimestampFormat = "hh:mm:ss ";
                    options.JsonWriterOptions = new JsonWriterOptions { Indented = true };
                }))
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                    .UseKestrel()
                    .UseStartup<Startup>());
    }
}
