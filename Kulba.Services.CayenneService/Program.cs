using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

using Kulba.Services.CayenneService.Models;
using Kulba.Services.CayenneService.Services;

namespace Kulba.Services.CayenneService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddEnvironmentVariables();
                    configApp.AddJsonFile(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower() == "development" ? "appsettings.Development.json" : "appsettings.json");
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<SocketServerConfigInfo>(hostContext.Configuration.GetSection("SocketServerConfigInfo"));
                    services.AddHostedService<GreetingHostedService>();

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(hostContext.Configuration)
                        .CreateLogger();
                        
                    services.AddLogging(configure => configure.AddSerilog(Log.Logger));

                })
                .Build();

            await host.RunAsync();

        }
    }
}
