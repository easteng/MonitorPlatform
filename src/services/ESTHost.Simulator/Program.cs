using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ESTHost.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // 数据模拟器
            CreateHostBuilder(args).Build().Run();
            Console.WriteLine("Hello World!");
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            if (isService)
            {
                var pathExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathRoot = Path.GetDirectoryName(pathExe);
                Directory.SetCurrentDirectory(pathRoot);
            }
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", true)
                    .Build();

            return Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "EST.Simulator";
                })
                .ConfigureAppConfiguration(cif => cif.AddConfiguration(config))
                .RegisterLmsServices<SimulatorModule>();
        }
    }
}
