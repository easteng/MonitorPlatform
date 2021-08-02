﻿/**********************************************************************
*******命名空间： ESTHost.DataCenter
*******类 名 称： DataCenterModule
*******类 说 明： 数据中心模块
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/23/2021 10:18:30 AM
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

using ESTHost.DataCenter.Repeater;
using ESTHost.ProtocolBase;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MonitorPlatform.Share;

using Silky.Lms.Core;
using Silky.Lms.Core.Modularity;

using System.Threading.Tasks;

namespace ESTHost.DataCenter
{
    [DependsOn(
       typeof(FreeSqlModule), 
       typeof(ESTRedisCacheModule),
       typeof(ESTMessageModule),
       typeof(ProtocolModule)
       )]
    public class DataCenterModule : StartUpModule
    {
        public override Task Initialize(ApplicationContext applicationContext)
        {
            return base.Initialize(applicationContext);
        }

        protected override void RegisterServices(ContainerBuilder builder)
        {
            var config = EngineContext.Current.Resolve<IConfiguration>();
            var service = new ServiceCollection();
            builder.RegisterMessageCenter(reg =>
            {
                reg.OptionServer(o =>
                {
                   // o.AddRepeater<IotMessageRepeater>(MessageTopic.DeviceData); // 添加设备采集数据
                    o.AddRepeater<RemoteControlRepeater>(MessageTopic.RemoteControlCommand); // 添加远程控制命转发器
                    o.AddRepeater<NoticeMessageRepeater>(MessageTopic.Notice); // 添加通知消息转换器
                    o.Build(); // 构建服务
                });
            });
            // 添加数据采集实例
            service.AddSingleton<ICollectionRepeater, CollectionReceiver>();
            service.AddHostedService<Worker>();
            builder.Populate(service);
        }
    }
}
