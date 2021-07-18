using ESTHost.Core;
using ESTHost.DataStorage.Service;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using IHost = Microsoft.Extensions.Hosting.IHost;


using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = ".NET Joke Service";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<WindowBackgroundService>();
    })
    .Build();

await host.RunAsync();

//namespace ESTHost.DataStorage
//{
//    class Program
//    {
//        static async Task Main(string[] args)
//        {
//            var isService = !(Debugger.IsAttached || args.Contains("--console"));

//            if (isService)
//            {
//                var pathExe = Process.GetCurrentProcess().MainModule.FileName;
//                var pathRoot = Path.GetDirectoryName(pathExe);
//                Directory.SetCurrentDirectory(pathRoot);
//            }
//            var config = new ConfigurationBuilder()
//                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
//                    .AddJsonFile("appsettings.json", true)
//                    .Build();
//            //#if DEBUG
//            //            IHostBuilder host = CustomHostBuilder.CreateDefaultServerHostBuilder(args);
//            //#else
//            //            IHostBuilder host = CustomHostBuilder.CreateWindowsServerHostBuilder(args);
//            //#endif
//            IHost host = Host.CreateDefaultBuilder(args)
//                 .UseWindowsService(options =>
//                 {
//                     options.ServiceName = "ESTDataStorage";
//                 })
//                 //.UseConsoleLifetime()
//                 .ConfigureServices(c=> {
//                      c.AddSingleton<IConfiguration>(config);
//                 })
//               //.ConfigureAppConfiguration((host, config) =>
//               //{
//               //    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//               //})
//              // .RegisterLmsServices<DataStorageModule>()
//              .ConfigureServices(a=>a.AddHostedService<WindowBackgroundService>())
//               .Build();

//            await host.RunAsync();
//            Console.WriteLine("Hello World!");
//        }
//    }
//}
