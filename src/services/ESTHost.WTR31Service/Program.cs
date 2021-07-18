using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ESTHost.WTR31Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunAsync();
            Console.Read();
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
                    options.ServiceName = "EST.WTR20AService";
                })
                .ConfigureAppConfiguration(cif => cif.AddConfiguration(config))
                .RegisterLmsServices<CollectionModule>();
        }
    }
}
