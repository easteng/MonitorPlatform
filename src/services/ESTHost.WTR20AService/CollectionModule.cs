/**********************************************************************
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
using ESTCore.Message.Services;

using ESTHost.Core;

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

            //  注册客户端
            builder.RegisterMessageCenter(b =>
            {
                b.OptionClient(o =>
                {
                    o.AddReceiver<CommandReceiver>(a =>
                    {
                        a.Name = MessageTopic.Command;
                        a.Topic = MessageTopic.Iot;
                    });
                    o.Build();
                });
            });

            var service = new ServiceCollection();
            service.AddHostedService<Worker>();
            builder.Populate(service);
           // base.RegisterServices(builder);
        }
    }
}
