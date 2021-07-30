/**********************************************************************
*******命名空间： ESTHost.Protocol.WTR20A
*******类 名 称： ProtocolProvider
*******类 说 明： 协议提供者
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 2:08:15 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Message;
using ESTCore.Message.Client;
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.ProtocolBase;

using Microsoft.Extensions.Logging;

using MonitorPlatform.Share;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Protocol.WTR20A
{
    /// <summary>
    ///  协议提供者 是该协议启动的入口,必须继承基础协议接口
    /// </summary>
    public class ProtocolProvider : IBaseProtocol
    {
        private ILogger<ProtocolProvider> _logger;
        private IMessageServerProvider serverProvider;
        private NoticeMessage currentMessage;
        private IRedisCachingProvider redisCachingProvider;
        public string Name { get => "WTR20A"; set => throw new NotImplementedException(); }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }

        public Task StartAsync()
        {
            var serviceProvider = EngineContext.Current;
            this._logger = serviceProvider.Resolve<ILogger<ProtocolProvider>>();
            this.serverProvider = serviceProvider.Resolve<IMessageServerProvider>();
            this.redisCachingProvider = serviceProvider.Resolve<IRedisCachingProvider>();
            this.currentMessage = new NoticeMessage();
            _logger.LogInformation($"{this.Name} 协议已启动");
           // Console.WriteLine();
            //while (true)
            //{
            //    this.currentMessage.Content = $"{this.Name} 协议采集中....";
            //    await this.serverProvider.Publish(MessageTopic.Notice, BaseMessage.CreateMessage(currentMessage)); ;
            //    await Task.Delay(2000);
            //}

           return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine($"{this.Name} 协议已关闭");
            return Task.CompletedTask;
        }
    }
}
