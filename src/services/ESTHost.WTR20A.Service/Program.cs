using ESTHost.Core;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace ESTHost.WTR20A.Service
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            IHostBuilder host = CustomHostBuilder.CreateDefaultServerHostBuilder(args);
#else
            IHostBuilder host = CustomHostBuilder.CreateWindowsServerHostBuilder(args);
#endif
            host.UseConsoleLifetime()
               .ConfigureAppConfiguration((host, config) =>
               {
                   config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
               })
               .ConfigureServices(a =>
               {
                   a.AddHostedService<WindowBackgroundService>();
               })
               .RegisterLmsServices<CollectionModule>();
            Console.WriteLine("Hello World!");
        }
    }
}
