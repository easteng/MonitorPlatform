/**********************************************************************
*******命名空间： ESTHost.DataCenter.Repeater
*******类 名 称： CollectionReceiver
*******类 说 明： 采集接收机
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/31/2021 12:33:56 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Caching;
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.ProtocolBase;

using Microsoft.Extensions.Logging;

using MonitorPlatform.Contracts;
using MonitorPlatform.Share;
using MonitorPlatform.Share.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.DataCenter
{
    /// <summary>
    /// 采集接收机 用来接收各协议采集并处理好的设备数据
    /// </summary>
    public class CollectionReceiver : ICollectionRepeater
    {
        private readonly ILogger<CollectionReceiver> logger;
        private readonly IMessageServerProvider serverProvider;
        private readonly IRedisCachingProvider redisCachingProvider;
        public CollectionReceiver(IMessageServerProvider serverProvider = null, IRedisCachingProvider redisCachingProvider = null, ILogger<CollectionReceiver> logger = null)
        {
            this.serverProvider = serverProvider;
            this.redisCachingProvider = redisCachingProvider;
            this.logger = logger;
        }
        /// <summary>
        /// 接收到各设备采集到的数据，对数据解析并转发到相应的通道
        /// </summary>
        /// <param name="deviceMessage"></param>
        /// <returns></returns>
        public async Task Receive(DeviceMessage deviceMessage)
        {
            // 获取设备的缓存信息，信息包括预警温度值，报警温度值等
            var device = this.redisCachingProvider?.GetDeviceInfoCache(deviceMessage.DeviceId);
            var iotMessage = deviceMessage.IOTData;
            if (iotMessage == null) return;
            this.logger.LogInformation($"接收到采集数据：{deviceMessage.Protocol}：{deviceMessage}");

            foreach (var item in iotMessage)
            {
                var standard = new StandardMessage()
                {
                    SensorCode = item.SensorCode,
                    TerminalId = item.TerminalId,
                    Battary = item.Battary,
                    Value = item.Value
                };
                if (device != null)
                {
                    if (item.Value > device.WarnValue && item.Value <= device.AlertValue)
                        standard.Status = PointStatus.Warning; // 温度预警
                    else if (item.Value > device.AlertValue)
                        standard.Status = PointStatus.Alerting; // 温度预警
                    else
                        standard.Status = PointStatus.Normal; // 温度预警
                }
                // 推送实时数据
                var real = new RealtimeMessage(standard);
                await this.serverProvider.Publish(MessageTopic.Realtime, BaseMessage.CreateMessage(real));

                // 推送报警数据
                var alert = new AlertMessage(standard);
                await this.serverProvider.Publish(MessageTopic.Alert, BaseMessage.CreateMessage(alert));

                // 推送存储数据
                var storage = new StorageMessge(standard);
                await this.serverProvider.Publish(MessageTopic.Storage, BaseMessage.CreateMessage(storage));
            }
        }
    }
}
