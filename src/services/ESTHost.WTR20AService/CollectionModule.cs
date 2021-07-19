﻿/**********************************************************************
*******命名空间： ESTHost.WTR20A.Service
*******类 名 称： CollectionModule
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 2:49:31 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using Autofac;
using Autofac.Extensions.DependencyInjection;

using ESTCore.Caching;
using ESTCore.Message;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Silky.Lms.Core;
using Silky.Lms.Core.Modularity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.WTR20AService
{
    [DependsOn(
         //  typeof(FreeSqlModule), 
         typeof(ESTRedisCacheModule),
         typeof(ESTMessageModule)
         )]
    public class CollectionModule : StartUpModule
    {
        public override Task Initialize(ApplicationContext applicationContext)
        {
            return base.Initialize(applicationContext);
        }

        protected override void RegisterServices(ContainerBuilder builder)
        {
            // 注册消息中心
            var config = EngineContext.Current.Resolve<IConfiguration>();
            var service = new ServiceCollection();
            service.AddMassTransit(c =>
            {
                c.UsingRabbitMq((context, conf) =>
                {
                    var host = config["Rabbitmq:Host"];
                    var user = config["Rabbitmq:Username"];
                    var pwd = config["Rabbitmq:Password"];
                    conf.Host(host, cc => {
                        cc.Username(user);
                        cc.Password(pwd);
                    });

                    // 设置订阅频道
                    conf.ReceiveEndpoint("storge", e =>
                    {
                        // 注册消费者  消费需要保存的物联网数据
                        // e.Consumer<StandardDataConsumer>();
                    });
                });
            });
            service.AddMassTransitHostedService();
            service.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            service.AddHostedService<Worker>();
            builder.Populate(service);
            base.RegisterServices(builder);
        }
    }
}