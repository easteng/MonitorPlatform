using ESTHost.Core;
using ESTHost.DataStorage.Service;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace ESTHost.DataStorage
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
               .RegisterLmsServices<DataStorageModule>();

            host.Build().Run();
            Console.WriteLine("Hello World!");
        }
    }
}
