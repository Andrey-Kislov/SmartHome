using System.Net;
using Andead.SmartHome.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore;

namespace Andead.SmartHome.Presentation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables(prefix: Settings.PREFIX);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((hostingContext, serverOptions) =>
                    {
                        var listenPort = hostingContext.Configuration.GetValue(Settings.LISTEN_PORT, 5000);
                        serverOptions.Listen(IPAddress.Any, listenPort);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
