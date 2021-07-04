using Autofac;
using Autofac.Extensions.DependencyInjection;

using ESTCore.ORM.FreeSql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MonitorPlatform.DataAccess;
using MonitorPlatform.Domain.Entities;

using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.ServiceHosting;
using Surging.Core.ServiceHosting.Internal;
using Surging.Core.ServiceHosting.Internal.Implementation;

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MonitorPlatform.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHostBuilder()
              .RegisterServices(builder =>
              {
                  builder.Register(a => new CPlatformContainer(ServiceLocator.Current));
              })
              .UseStartup<Startup>()
              .Build();
            host.Run();
            Console.Read();
        }
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    new HostBuilder()
        //      .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //      .ConfigureServices(serviceCollection =>
        //      {
        //          serviceCollection.AddSingleton<IConfiguration>(a =>
        //          new ConfigurationBuilder()
        //              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build());
        //          var builder = new ContainerBuilder();
        //          builder.Register(a => new CPlatformContainer(ServiceLocator.Current));
        //          builder.Populate(serviceCollection);
        //          builder.RegisterModule<MonitorPlatformModule>();
        //          builder.RegisterModule<FreeSqlModule>();
        //          ServiceLocator.Current = builder.Build();
        //          // serviceCollection.AddHostedService<MonitorServiceProvider>();
        //      });

        //public static IServiceHostBuilder CreateServiceHostBuilder()
        //{
        //    //try
        //    //{

        //    //    return host;
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    throw;
        //    //}

        //}
    }
}
