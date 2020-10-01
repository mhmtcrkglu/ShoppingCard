using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCard.Core;

namespace ShoppingCard
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var settings = GetConfigurationRoot(args).GetSection("Settings").GetSection("CoreSettings").Get<AppSettings>();

            try
            {
                Console.WriteLine($"{settings.Name} is starting...");
                var host = CreateHostBuilder(args).Build();
                var svc = ActivatorUtilities.CreateInstance<BasketManager>(host.Services);
                svc.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{settings.Name} terminated unexpectedly... Exception: {exception}");
            }
            finally
            {
                Console.WriteLine($"{settings.Name} is stopping...");
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile(
                        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                        false, true).AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(o => o.AllowSynchronousIO = true)
                        .UseStartup<Startup>();
                });

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