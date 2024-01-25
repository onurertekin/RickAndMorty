using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Host
{
    static class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                                 .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            #region Read Config

            string environment = configuration.GetValue<string>("Api:Environment", Environments.Development);

            string httpUrl = configuration.GetValue<string>("Api:Kestrel:Http:Url");
            int httpPort = configuration.GetValue<int>("Api:Kestrel:Http:Port");

            #endregion

            #region WebApplication

            var builder = WebApplication.CreateBuilder(args);
            builder.Environment.EnvironmentName = environment;
            builder.WebHost.UseConfiguration(configuration);
            builder.Logging.ClearProviders();

            builder.WebHost.UseKestrel(options =>
            {
                options.AllowSynchronousIO = true;
                options.AddServerHeader = false;
                if (!string.IsNullOrEmpty(httpUrl) && httpPort != 0)
                {
                    options.Listen(IPAddress.Parse(httpUrl), httpPort);
                }
            });

            builder.WebHost.UseShutdownTimeout(TimeSpan.FromSeconds(10));

            #endregion



            Startup startup = new Startup(configuration, builder.Environment);
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();
            startup.Configure(app);

            #region Run

            app.Lifetime.ApplicationStarted.Register(() => OnStarted(configuration));
            app.Lifetime.ApplicationStopped.Register(() => OnStopped());

            app.Run();

            #endregion
        }

        private static void OnStarted(IConfiguration configuration)
        {
            string serviceName = configuration.GetValue<string>("Api:ServiceName", "");
            string operationSystem = Environment.OSVersion.VersionString;

            string httpUrl = configuration.GetValue<string>("Api:Kestrel:Http:Url");
            int httpPort = configuration.GetValue<int>("Api:Kestrel:Http:Port");

            Console.WriteLine("_______________________________________________\r\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"  ServiceName: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{serviceName}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"  OperationSystem: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{operationSystem}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  Service Started and Listening..");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ■ http://{httpUrl}:{httpPort}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\r\n  Press Ctrl+C to shut down.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("_______________________________________________\r\n");
        }

        private static void OnStopped()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\r\n  Service stopped.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}