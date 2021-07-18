using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ESTHost.StorageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunAsync();
            Console.ReadLine();
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

            return  Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "EST.DataStorgageServer";
                })
                .ConfigureAppConfiguration(cif => cif.AddConfiguration(config))
                .RegisterLmsServices<DataStorageModule>();
        }
    }
}
