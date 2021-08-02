///**********************************************************************
//*******命名空间： ESTHost.DataCenter
//*******类 名 称： DataRepeater
//*******类 说 明： 数据转换器
//*******作    者： Easten
//*******机器名称： EASTEN
//*******CLR 版本： 4.0.30319.42000
//*******创建时间： 7/23/2021 10:26:31 AM
//*******联系方式： 1301485237@qq.com
//***********************************************************************
//******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
//***********************************************************************
// */
//using EasyCaching.Core;

//using ESTCore.Message;
//using ESTCore.Message.Handler;
//using ESTCore.Message.Message;

//using ESTHost.Core;

//using MonitorPlatform.Share;
//using MonitorPlatform.Share.Message;

//using Newtonsoft.Json;

//using System;
//using System.Threading.Tasks;

//namespace ESTHost.DataCenter
//{
//    /// <summary>
//    ///  物联网数据转换器
//    /// </summary>
//    public class IotMessageRepeater : IMessageRepeaterHandler
//    {
//        IMessageServerProvider serverProvider;
//        private readonly IRedisCachingProvider redisCachingProvider;
//        public IotMessageRepeater(IMessageServerProvider serverProvider = null, IRedisCachingProvider redisCachingProvider = null)
//        {
//            this.serverProvider = serverProvider;
//            this.redisCachingProvider = redisCachingProvider;
//        }


//        public Task Repeater(BaseMessage message)
//        {
//            var deviceMessage = message.GetMessage<DeviceMessage>();
//            // 获取采集器
//            var termnalString = redisCachingProvider.StringGet($"Terminal:{deviceMessage.TerminalId}");
//            var terminal=JsonConvert.DeserializeObject<TerminalCacheItem>(termnalString);

//            // 处理数据，不同状态的数据执行不同的操作
//            var iotMessage = deviceMessage.IOTData;
//            // 对接收到的数据进行处理
//            foreach (var item in iotMessage)
//            {
//                var standard = new StandardMessage()
//                {
//                    SensorCode = item.SensorCode,
//                    TerminalId = item.TerminalId,
//                    Battary = item.Battary,
//                    Value = item.Value
//                };
//                if (terminal != null)
//                {
//                    if (item.Value > terminal.WarinValue && item.Value <= terminal.AlertValue)
//                        standard.Status = PointStatus.Warning; // 温度预警
//                    else if (item.Value > terminal.AlertValue)
//                        standard.Status = PointStatus.Alerting; // 温度预警
//                    else
//                        standard.Status = PointStatus.Normal; // 温度预警
//                }
//                // 推送实时数据
//                var real = new RealtimeMessage(standard);
//                serverProvider.Publish(MessageTopic.Realtime, BaseMessage.CreateMessage(real));

//                // 推送报警数据
//                var alert = new AlertMessage(standard);
//                serverProvider.Publish(MessageTopic.Alert, BaseMessage.CreateMessage(alert));

//                // 推送存储数据
//                var storage = new StorageMessge(standard);
//                serverProvider.Publish(MessageTopic.Storage, BaseMessage.CreateMessage(storage));
//            }
//            //Console.WriteLine(JsonConvert.SerializeObject(deviceMessage));
//            Console.WriteLine($"接收的物联网数据并转发:{DateTime.Now.ToLocalTime()}");
//            return Task.CompletedTask;
//        }
//    }
//}
