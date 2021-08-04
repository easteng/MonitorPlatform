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
            var list = new List<CacheItemTerminal>();
            var devices = new List<CacheItemDevice>();
            // ��ȡ�豸
            var protocol1 = this.redisCachingProvider.GetDevicesByProtocol("WTR20A");
            var protpcol2 = this.redisCachingProvider.GetDevicesByProtocol("WTR31");
            if (protocol1 != null)
            {
                // ��ȡ�ɼ���
                foreach (var item in protocol1)
                {
                    var device = this.redisCachingProvider.GetTerminalsByDevice(item.DeviceId);
                    if (device != null)
                    {
                        list.AddRange(device);
                    }
                }
            }
            if (protpcol2 != null)
            {
                foreach (var item in protpcol2)
                {
                    var device = this.redisCachingProvider.GetTerminalsByDevice(item.DeviceId);
                    if (device != null)
                    {
                        list.AddRange(device);
                    }
                }

            }


            var sensorList = new List<CacheItemSensor>();
            list?.ForEach(a =>
            {
                var sensors = this.redisCachingProvider.GetTerminalSensorCache(a.Id);
                if (sensors != null && sensors.Any())
                {
                    sensorList.AddRange(sensors);
                }
            });

            // ��ȡЭ���µ�
            // ��ȡ��������Ϣ
          
            var battary = new Random();
            var Value = new Random();
            while (!stoppingToken.IsCancellationRequested)
            {
                sensorList?.ForEach(a =>
                {
                    var standard = new StandardMessage()
                    {
                        SensorCode = a.SensorCode,
                        // TerminalId = item.TerminalId,
                        // TerminalId = item.TerminalId,
                        Battary = battary.Next(1, 3),
                        Value = Value.Next(20, 80),
                        Time = DateTime.Now
                    };

                    if (standard.Value > 40 && standard.Value < 60)
                        standard.Status = PointStatus.Warning;
                    else if (standard.Value > 60)
                        standard.Status = PointStatus.Alerting;
                    else
                        standard.Status = PointStatus.Normal;
                    // ����ʵʱ����
                    var real = new RealtimeMessage(standard);
                    this.messageProvider.Publish(MessageTopic.Realtime, BaseMessage.CreateMessage(real));

                    _logger.LogInformation("�������ݣ� {data}", standard.ToString()) ;
                });
               
                await Task.Delay(5000, stoppingToken);
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            // ��ȡ����������Э��
            //await ProtocolFactory.StartupProtocolProvider();
            await base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
