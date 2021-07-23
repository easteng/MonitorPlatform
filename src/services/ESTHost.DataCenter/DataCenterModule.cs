/**********************************************************************
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

using ESTHost.Core;
using ESTHost.Core.Message;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Silky.Lms.Core;
using Silky.Lms.Core.Modularity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.DataCenter
{
    [DependsOn(
       //  typeof(FreeSqlModule), 
       typeof(ESTRedisCacheModule),
       typeof(ESTMessageModule)
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
            // 注册消息中心
            //service.UseMessageCenterServer(b =>
            //{
            //    b.AddRepeater<IotMessageRepeater>(); //物联网数据转换器
            //    b.AddRepeater<CommandRepeater>();// 添加命令转换器
            //    b.Build();
            //});
            builder.RegisterMessageCenter(reg =>
            {
                reg.OptionServer(o =>
                {
                    o.AddRepeater<IotMessageRepeater>(MessageTopic.Iot); // 添加物联网转换器
                    o.AddRepeater<CommandRepeater>(MessageTopic.Command); // 添加命令转换器
                    o.Build(); // 构建服务
                });
            });
            service.AddHostedService<Worker>();
            builder.Populate(service);
        }
    }
}
