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
using ESTCore.Caching;
using ESTCore.Message;
using ESTCore.Message.Services;
using ESTCore.ORM.FreeSql;
using Microsoft.Extensions.DependencyInjection;
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Receiver;
using Silky.Lms.Core.Modularity;

using System.Threading.Tasks;

namespace MonitorPlatform.Wpf
{
    [DependsOn(
        typeof(FreeSqlModule),
        typeof(ESTMessageModule),
        typeof(ESTRedisCacheModule)
        )]
    public class MonitorPlatformModule: StartUpModule
    {
        public override Task Initialize(ApplicationContext applicationContext)
        {
            // 启动主界面
            GlableDelegateHandler.InitComplate?.Invoke(); // 初始化完成
            return base.Initialize(applicationContext);
        }
        protected override void RegisterServices(ContainerBuilder builder)
        {
            var service = new ServiceCollection();
            // 注册消息中心
            builder.RegisterMessageCenter(reg =>
            {
                reg.OptionClient(o =>
                {
                    o.AddReceiver<RealtimeMessageReceiver>(a => a.Name = MessageTopic.Realtime); // 添加实时数据接收机
                    o.AddReceiver<NoticeMessageReceiver>(a => a.Name = MessageTopic.Notice); // 添加通知消息接收机
                    o.Build(); // 构建服务
                });
            });
          //  base.RegisterServices(builder);
        }
    }
}
