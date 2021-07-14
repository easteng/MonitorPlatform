/**********************************************************************
*******命名空间： MonitorPlatform.Wpf
*******类 名 称： MinitorPlatformModule
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 9:13:23 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using Autofac;
using Autofac.Extensions.DependencyInjection;

using ESTCore.ORM.FreeSql;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Silky.Lms.AutoMapper;
using Silky.Lms.Core;
using Silky.Lms.Core.Modularity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf
{
    [DependsOn(typeof(FreeSqlModule))]
    public class MinitorPlatformModule: StartUpModule
    {
        protected override void RegisterServices(ContainerBuilder builder)
        {
            var service = new ServiceCollection();
            var config = EngineContext.Current.Resolve<IConfiguration>();
            //service.AddMassTransit(conf =>
            //{
            //    //  conf.AddConsumer<MessageConsumer>();
            //    //  conf.AddConsumer<UpdateOrderStatusConsumer>();
            //    conf.UsingRabbitMq((context, cif) =>
            //    {
            //        cif.Host(config["Rabbitmq:Host"], c =>
            //        {
            //            c.Username(config["Rabbitmq:Username"]);
            //            c.Password(config["Rabbitmq:Password"]);
            //        });
            //        cif.ReceiveEndpoint("server3", e =>
            //        {
            //            e.Observer(new MessageConsumer());
            //            // e.Consumer<UpdateOrderStatusConsumer>(context);
            //        });
            //    });

            //});
            //service.AddMassTransitHostedService();
            // builder.Populate(service);
            base.RegisterServices(builder);
        }
    }
}
