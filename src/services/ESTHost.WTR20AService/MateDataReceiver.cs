/**********************************************************************
*******命名空间： ESTHost.WTR20AService
*******类 名 称： MateDataReceiver
*******类 说 明： 元数据接收机，通过协议进行解析
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 1:46:24 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Message.Client;
using ESTCore.Message.Message;

using ESTHost.Core.Colleaction;
using ESTHost.WTR20AService.Collections;

using MonitorPlatform.Share;
using MonitorPlatform.Share.Message;
using MonitorPlatform.Share.ServerCache;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.WTR20AService
{
    /// <summary>
    ///  原始数据接收类
    /// </summary>
    public class MateDataReceiver : AbstractEventBus
    {
        private readonly IRedisCachingProvider redisCachingProvider;
        private readonly IMessageClientProvider messageClient;
        private NoticeMessage noticeMessage;
        private Dictionary<Guid, List<PointData>> lastPointData;
        /**
         * 主要业务：
         * 1、通过请求的结果进行判断，再根据功能码判断如何将消息组装并发送
         * 2、解析完数据，第一次存储温度值，结合容错温度进行处理温度是否发送
         * 3、将最终完整的数据以消息的方式发送到数据中心，再由数据中心进行精加工
         */
        public MateDataReceiver(IRedisCachingProvider redisCachingProvider = null, IMessageClientProvider messageClient = null)
        {
            this.redisCachingProvider = redisCachingProvider;
            this.messageClient = messageClient;
            this.noticeMessage = new NoticeMessage();
            this.noticeMessage.ServiceType = ServerType.WTR20AService;
            this.noticeMessage.Online = true;
            this.lastPointData = new Dictionary<Guid, List<PointData>>();
        }
        /// <summary>
        /// 读取到温度数据，根据wtr20A 的数据协议进行解析成标准的数据并发送给数据中心
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task<bool> ReceiverMateData(ReadCallbackMessage result)
        {
            var device = result.DeviceId;
            var terminal = result.Terminal;
            if (result.Data != null)
            {
                var pointData = this.ResolveBuffer(result.Data);
                Console.WriteLine(JsonConvert.SerializeObject(pointData));
                // 获取采集器的传感器缓存 缓存的key 值为采集器的id
                var sensorCacheString = redisCachingProvider.StringGet($"Terminal:Sensor:{terminal.Id}");
                var sensors = JsonConvert.DeserializeObject<List<SensorCacheItem>>(sensorCacheString);
                // 解析数据成标准格式
                var iotMessage = this.GetIotMessage(terminal, pointData, sensors);
                var deviceMessage = new DeviceMessage();
                deviceMessage.TerminalId = terminal.Id;
                deviceMessage.IOTData = iotMessage;
                await this.messageClient.SendMessage(deviceMessage); // 给数据中心发送数据
                return true;
            }
            else
            {
                var content = $"{device}设备的{terminal.Name} 采集器获取数据异常，请检查";
                this.noticeMessage.Content= content;
                await messageClient.SendMessage(this.noticeMessage);
                return true;
            }
        }
     
        /// <summary>
        /// 获取标准的物联网数据，同时处理数据跳变 将异常数据过滤掉
        /// </summary>
        /// <param name="terminal">采集终端信息，记录报警 预警等</param>
        /// <param name="list"></param>
        /// <param name="sensors"></param>
        private List<IOTMessage> GetIotMessage(TerminalCacheItem terminal,List<PointData> list,List<SensorCacheItem> sensors)
        {
            try
            {
                var iotDatas = new List<IOTMessage>();
                if (list.Any() && sensors.Any())
                {
                    var lastData = this.lastPointData.GetValueOrDefault(terminal.Id);
                    foreach (var item in list)
                    {
                        var iot = new IOTMessage();
                        iot.SensorCode = sensors.FirstOrDefault(a => a.SensorNo == item.PointNo)?.SensorCode;
                        iot.OffLine = item.OffLine;
                        iot.TerminalId = terminal.Id;
                        iot.Value = item.Temp;
                        iot.Battary = item.Battery;
                        iot.PointState = item.PointState;

                        // 获取缓存，看是否温度是否跳变
                        // 判断前后时间是否超过了五分钟
                        if (lastData != null)
                        {
                            var p = lastData.FirstOrDefault(a => a.PointNo == item.PointNo);
                            if (p != null&&(item.Time-p.Time).TotalMinutes<3)
                            {
                                // 三分钟之内的数据
                                if (Math.Abs(item.Temp - p.Temp) > terminal.TolerantValue)
                                {
                                    continue;
                                }
                            }
                        }

                        iotDatas.Add(iot);
                    }
                    this.lastPointData.TryAdd(terminal.Id, list);
                }
                return iotDatas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取物联网数据异常");
                return null;
            }
        }

        /// <summary>
        /// 解析二进制数据
        /// </summary>
        private List<PointData> ResolveBuffer(byte[] buffer)
        {
            var list = new List<PointData>();
            var time = DateTime.Now;
            var length = buffer[2];
            for (int i = 0; i < length; i+=2)
            {
                var pdata = new PointData();
                var index = i / 2;
                pdata.PointNo = (byte)index;
                pdata.Time = time;
                pdata.Temp = (sbyte)buffer[3 + i];
                pdata.Byte = (ushort)(buffer[3 + i] * 0x100 + buffer[3 + i + 1]);
                if (index != 0)
                {
                    byte state = buffer[3 + i + 1];
                    pdata.Battery = (byte)((state >> 2) & 0x03);
                    pdata.PointState= (byte)((state >> 1) & 0x01);
                    if (buffer[3 + i] == 0xc4)
                        pdata.OffLine = true;  // 传感器离线
                    list.Add(pdata);
                }
                // 根据缓存数据判断当前的温度是否跳变
            }
            return list;
        }

    }
}
