using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;  //Serilog 
using Serilog.Events;   //for LogEventLevel

namespace MoviesListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: "/Users/parminderkaur/Documents/Projects/MoviesListinglogs/log-.txt",
                    //outputTemplate:)
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                    ).CreateLogger();

            try
            {
                Log.Information("Application Is Starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application Failed To Start"); //in case of failure in app
            }
            finally {
                Log.CloseAndFlush();    //reset logger
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()   //use Serilog instead of deafult logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
