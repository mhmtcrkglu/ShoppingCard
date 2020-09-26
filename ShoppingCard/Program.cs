using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ShoppingCard.Core;

namespace ShoppingCard
{
    class Program
    {
        public static void Main(string[] args)
        {
            var settings = GetConfigurationRoot(args).GetSection("CoreSettings").Get<AppSettings>();

            try
            {
                Log.Information($"{settings.Name} is starting...");
                var host = CreateHostBuilder(args).Build();
                var svc = ActivatorUtilities.CreateInstance<BasketManager>(host.Services);
                svc.Run();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, $"{settings.Name} terminated unexpectedly...");
            }
            finally
            {
                Log.Warning($"{settings.Name} is stopping...");

                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => { config.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", false, true).AddEnvironmentVariables(); })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(o => o.AllowSynchronousIO = true)
                        .UseStartup<Startup>();
                }).UseSerilog();

        private static IConfigurationRoot GetConfigurationRoot(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true).AddCommandLine(args)
                .Build();
        }
    }
}