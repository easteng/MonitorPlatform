using EasyCaching.Core;

using ESTCore.Message;
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.ProtocolBase;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MonitorPlatform.Contracts;
using MonitorPlatform.Share;
using MonitorPlatform.Share.CacheItem;
using MonitorPlatform.Share.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.Simulator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageServerProvider messageProvider;
        private readonly IRedisCachingProvider redisCachingProvider;
        public Worker(ILogger<Worker> logger, IMessageServerProvider messageProvider = null, IRedisCachingProvider redisCachingProvider = null)
        {
            _logger = logger;
            this.messageProvider = messageProvider;
            this.redisCachingProvider = redisCachingProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var devices = new List<CacheItemDevice>();
            // 获取设备
            var protocol = this.redisCachingProvider.GetDevicesByProtocol("WTR20A");

            if (protocol != null)
            {
                // 获取采集器
                foreach (var item in protocol)
                {
                    var device = this.redisCachingProvider.GetTerminalsByDevice(item.DeviceId);
                }
            }

            // 获取传感器信息
            var sensors = this.redisCachingProvider.GetTerminalSensorCache(Guid.Parse("6107f1df-efc2-bf7c-00e9-d761574ab9a6"));
            var battary = new Random();
            var Value = new Random();
            while (!stoppingToken.IsCancellationRequested)
            {
                sensors?.ForEach(a =>
                {
                    var standard = new StandardMessage()
                    {
                        SensorCode = a.SensorCode,
                        // TerminalId = item.TerminalId,
                        // TerminalId = item.TerminalId,
                        Battary = battary.Next(1, 3),
                        Value = Value.Next(20, 80)
                    };

                    if (standard.Value > 40 && standard.Value < 60)
                        standard.Status = PointStatus.Warning;
                    else if (standard.Value > 60)
                        standard.Status = PointStatus.Alerting;
                    else
                        standard.Status = PointStatus.Normal;
                    // 推送实时数据
                    var real = new RealtimeMessage(standard);
                    this.messageProvider.Publish(MessageTopic.Realtime, BaseMessage.CreateMessage(real));

                    _logger.LogInformation("推送数据： {data}", standard.ToString()) ;
                });
               
                await Task.Delay(5000, stoppingToken);
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            // 获取并启动数据协议
            //await ProtocolFactory.StartupProtocolProvider();
            await base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
