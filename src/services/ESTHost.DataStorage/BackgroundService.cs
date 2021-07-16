/**********************************************************************
*******命名空间： ESTHost.WTR31.Service
*******类 名 称： BackgroundService
*******类 说 明： 标准后台服务
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 3:37:09 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;

using MassTransit;

using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.DataStorage.Service
{
    /// <summary>
    ///  后台服务
    /// </summary>
    public class WindowBackgroundService : BackgroundService
    {
        readonly IBus _mainBus;
        readonly ICommandSender commandSender;
        public WindowBackgroundService(IBus mainBus = null, ICommandSender commandSender = null)
        {
            MessageCenter.InitHealtMessage("数据存储服务");
            _mainBus = mainBus;
            this.commandSender = commandSender;

            // 执行一个定时器  五分钟检查一次服务状态
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 发送一条通知命令
            return Task.CompletedTask;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            MessageCenter.HealthCheckMessage.Message = "服务已启动";
            MessageCenter.HealthCheckMessage.Online=true;
            MessageCenter.HealthCheckMessage.EventType = MessageEventType.Heartbeat;
            while (true)
            {
                await commandSender.Send(MessageCenter.HealthCheckMessage);
                await Task.Delay(1000);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            MessageCenter.HealthCheckMessage.Message = "服务已启动";
            MessageCenter.HealthCheckMessage.Online = true;
            MessageCenter.HealthCheckMessage.EventType = MessageEventType.Heartbeat;
            //return Task.CompletedTask;
        }
    }
}
