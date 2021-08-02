/**********************************************************************
*******命名空间： ESTHost.Protocol.WTR20A
*******类 名 称： BufferReceiver
*******类 说 明： 缓存数据接收类
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 11:41:58 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Message.Client;
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.Core.Colleaction;
using ESTHost.ProtocolBase;

using Microsoft.Extensions.Logging;

using MonitorPlatform.Contracts;
using MonitorPlatform.Share;
using MonitorPlatform.Share.CacheItem;
using MonitorPlatform.Share.Message;

using Newtonsoft.Json;

using Silky.Lms.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Protocol.WTR31
{
    /// <summary>
    /// 缓存数据接收类  用来接收服务端返回的数据并根据当前协议进行解析
    /// 对解析后的协议转发到数据中心  由数据中心决定标准数据的去处
    /// 接收机的名称必须是BufferReceiver 结束 否在不成功
    /// </summary>
    public class WTR31Receiver : AbstractEventBus
    {
        private ILogger<WTR31Receiver> _logger;
        private readonly IRedisCachingProvider redisCachingProvider;
        private readonly IMessageServerProvider serverProvider;
        private readonly ICollectionRepeater collectionRepeater;
        private NoticeMessage noticeMessage;
        private Dictionary<Guid, List<PointData>> lastPointData;
        public WTR31Receiver(IRedisCachingProvider redisCachingProvider = null, ILogger<WTR31Receiver> logger = null, IMessageServerProvider serverProvider = null, ICollectionRepeater collectionRepeater = null)
        {
            this.redisCachingProvider = redisCachingProvider;
            this.noticeMessage = new NoticeMessage();
            this.noticeMessage.ServiceName = "WTR20A 协议服务";
            this.noticeMessage.Online = true;
            this.lastPointData = new Dictionary<Guid, List<PointData>>();
            _logger = logger;
            this.serverProvider = serverProvider;
            this.collectionRepeater = collectionRepeater;
        }

        /// <summary>
        /// 读取到串口服务器的缓冲数据  31协议的报文比较特殊，电池状态单独获取的
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public override async Task<bool> ReceiverMateData(ReadCallbackMessage result)
        {
            var device = result.DeviceId;
            var terminal = result.Terminal;
            if (result.Data != null)
            {
                var pointData = this.ResolveBuffer(result.Data,result.StateData);
                //_logger.LogInformation(JsonConvert.SerializeObject(pointData));
                // 获取采集器的传感器缓存 缓存的key 值为采集器的id
                var sensors = redisCachingProvider.GetTerminalSensorCache(terminal.Id);
                // 解析数据成标准格式
                var iotMessage = this.GetIotMessage(terminal, pointData, sensors,20);
                var ptotocol = nameof(WTR31Receiver).RemovePostFix(StringComparison.OrdinalIgnoreCase, "Receiver");
                var deviceMessage = new DeviceMessage(terminal.DeviceId,terminal.Id, iotMessage, ptotocol);
                await this.collectionRepeater.Receive(deviceMessage);// 向数据中心发送数据
            }
            else
            {
                var content = $"{device}设备的{terminal.Addr} 采集器获取数据异常，请检查";
                this.noticeMessage.Content = content;
                await this.serverProvider.Publish(MessageTopic.Notice, BaseMessage.CreateMessage(this.noticeMessage));
            }
            return true;
        }

        /// <summary>
        /// 获取标准的物联网数据，同时处理数据跳变 将异常数据过滤掉
        /// </summary>
        /// <param name="terminal">采集终端信息，记录报警 预警等</param>
        /// <param name="list"></param>
        /// <param name="sensors"></param>
        private List<IOTMessage> GetIotMessage(CacheItemTerminal terminal, List<PointData> list, List<CacheItemSensor> sensors,int touvalue)
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
                            if (p != null && (item.Time - p.Time).TotalMinutes < 3)
                            {
                                // 三分钟之内的数据
                                if (Math.Abs(item.Temp - p.Temp) > touvalue)
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
        private List<PointData> ResolveBuffer(byte[] buffer,byte[] stateBuffer)
        {
            var list = new List<PointData>();
            var time = DateTime.Now;
            var length = buffer[2];
            for (int i = 0; i < length; i += 2)
            {
                var pdata = new PointData();
                byte dev_no = (byte)(i / 2 + 1);
                pdata.PointNo = dev_no;
                pdata.Time = time;

                short s_wendu = buffer[3 + i];
                s_wendu <<= 8;
                s_wendu += buffer[3 + i + 1];
                pdata.Temp =(double)( s_wendu / 10m);
                //p_data.d_temp = (decimal)(frame_buf[3 + i] * 0x100 + frame_buf[3 + i + 1]) / 10m;

                //-55/-60表示探头离线
                //if (frame_buf[3 + i] == 0xC9 || frame_buf[3 + i] == 0xC4)
                if (pdata.Temp < -50)
                    pdata.OffLine = true;
                else if (pdata.Temp > 300)
                    pdata.OffLine = true;//测温点离线

                list.Add(pdata);
            }

            // 解析电池电压
            if (stateBuffer!=null&&stateBuffer.Length > 4)
            {
                int nLen = stateBuffer[2];
                for (int i = 0; i < nLen; i += 2)
                {
                    byte dev_no = (byte)(i / 2 + 1);
                    var pdata = list.FirstOrDefault(a=>a.PointNo==dev_no);
                    if (pdata == null)
                        continue;

                    //帧序号
                    pdata.FrameNo = (byte)(stateBuffer[3 + i + 1] >> 4);

                    //电池电压
                    pdata.Battery = stateBuffer[3 + i + 1] & 0x0F;
                    pdata.Battery += 21;
                    pdata.Battery /= 10;
                }
            }
            return list;
        }
    }
}
