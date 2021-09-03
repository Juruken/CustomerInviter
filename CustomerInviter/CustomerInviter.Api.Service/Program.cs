using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Enrichers;

namespace CustomerInviter.Api.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogging();

            var host = new WebHostBuilder()
                .UseKestrel(o => o.AllowSynchronousIO = true)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        private static void ConfigureLogging()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build();

            var assembly = Assembly.GetEntryAssembly().GetName();
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", assembly.Name)
                .Enrich.WithProperty("Version", assembly.Version)
                .Enrich.With<EnvironmentUserNameEnricher>()
                .WriteTo.Seq(config["seqApplicationUrl"], apiKey: config["seqApplicationKey"])
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
