
using Autofac;
using Autofac.Extensions.DependencyInjection;

using ESTCore.ORM.FreeSql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MonitorPlatform.DataAccess;

using Surging.Core.CPlatform.Utilities;

using System;
using System.Threading.Tasks;

namespace MonitorPlatform.Server
{
    /// <summary>
    /// 启动项配置
    /// </summary>
    public class Startup
    {
        public IConfiguration configuration { get; }
        public Startup(IConfigurationBuilder builder)
        {
            configuration = builder.Build();
        }

        public IContainer ConfigureServices(ContainerBuilder builder)
        {
            var service = new ServiceCollection();
            service.AddSingleton<IConfiguration>(a => new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build());
            service.AddFreeRepository();
            service.AddSingleton<IMonitorServiceProvider,MonitorServiceProvider>();
            builder.RegisterModule<ServerModule>();
            builder.RegisterModule<MonitorPlatformModule>();
            builder.RegisterModule<FreeSqlModule>();
            builder.Populate(service);
            ServiceLocator.Current = builder.Build();
            return ServiceLocator.Current;
        }

        public void Configure(IContainer app)
        {
            var service = app.Resolve<IMonitorServiceProvider>();
            service.Start();
        }
    }

}
