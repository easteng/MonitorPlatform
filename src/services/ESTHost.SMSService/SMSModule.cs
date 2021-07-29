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
using Microsoft.Extensions.DependencyInjection;
using MonitorPlatform.Share;
using Silky.Lms.Core.Modularity;
using System.Threading.Tasks;

namespace ESTHost.SMSService
{
    [DependsOn(
          //  typeof(FreeSqlModule), 
        typeof(ESTRedisCacheModule),
        typeof(ESTMessageModule)
        )]
    public class SMSModule : StartUpModule
    {
        public override Task Initialize(ApplicationContext applicationContext)
        {
            return base.Initialize(applicationContext);
        }
        protected override void RegisterServices(ContainerBuilder builder)
        {
            //  注册消息中心
            //  注册客户端
            builder.RegisterMessageCenter(b =>
            {
                b.OptionClient(o =>
                {
                    // 添加报警数据接收机，用来处理报警数据，并发送短信
                    o.AddReceiver<AlertDataReceiver>(a =>
                    {
                        a.Name = MessageTopic.Alert;  // 接收报警数据
                    });

                    // 添加服务命令接收机，用来操作当前的服务
                    o.AddReceiver<SMSCommandReveiver>(a => a.Name = MessageTopic.SmsCommand);

                    o.Build();
                });
            });
            var service = new ServiceCollection();
            service.AddHostedService<Worker>();
            builder.Populate(service);
            //base.RegisterServices(builder);
        }
    }
}
