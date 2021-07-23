/**********************************************************************
*******命名空间： ESTHost.DataStorage.Service
*******类 名 称： DataStorageModule
*******类 说 明： 数据存储模块
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 2:48:21 PM
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
using ESTCore.ORM.FreeSql;

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

namespace ESTHost.StorageService
{
    /// <summary>
    ///  数据存储模块
    /// </summary>
    [DependsOn(
      //  typeof(FreeSqlModule), 
      //  typeof(ESTRedisCacheModule),
        typeof(ESTMessageModule)
        )]
    public class DataStorageModule : StartUpModule
    {
        IConfiguration config;
        public DataStorageModule() { }
        public override Task Initialize(ApplicationContext applicationContext)
        {
            return base.Initialize(applicationContext); 
        }
        protected override void RegisterServices(ContainerBuilder builder)
        {
           
            var service = new ServiceCollection();
            // 注册消息中心
            builder.RegisterMessageCenter(b =>
            {
                b.OptionClient(o =>
                {
                    // 添加报警数据接收机，用来处理报警数据，并发送短信
                    o.AddReceiver<IotMessageReceiver>(a =>
                    {
                        a.Name = MessageTopic.Storage;  // 接收报警数据
                    });
                    o.Build();
                });
            });
            service.AddHostedService<Worker>();
            builder.Populate(service);
           // base.RegisterServices(builder);
        }

    }
}
