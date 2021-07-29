/**********************************************************************
*******命名空间： ESTHost.DataCenter
*******类 名 称： DataRepeater
*******类 说 明： 数据转换器
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/23/2021 10:26:31 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.Core;

using MonitorPlatform.Share;

using System;
using System.Threading.Tasks;

namespace ESTHost.DataCenter
{
    /// <summary>
    ///  物联网数据转换器
    /// </summary>
    public class IotMessageRepeater : IMessageRepeaterHandler
    {
        IMessageServerProvider serverProvider;
        public IotMessageRepeater(IMessageServerProvider serverProvider = null)
        {
            this.serverProvider = serverProvider;
        }
        public Task Repeater(BaseMessage message)
        {
            var iot = message.GetMessage<IOTMessage>();

            var newMsg= BaseMessage.CreateMessage(iot);

            if (iot.Value > 50)
            {
                var alertMessage = new AlertMessage()
                {
                    Value = iot.Value,
                    Code = iot.Code
                };
                // 模拟报警
                serverProvider.Publish(MessageTopic.Alert, BaseMessage.CreateMessage(alertMessage));
            }

            var store = new StorageMessge()
            {
                Value = iot.Value,
                Code = iot.Code
            };
            // 将数据发送数据存储服务进行数据存储
            serverProvider.Publish(MessageTopic.Storage, BaseMessage.CreateMessage(store));


            var real = new RealtimeMessage()
            {
                Value = iot.Value,
                Code = iot.Code
            };
            serverProvider.Publish(MessageTopic.Realtime, BaseMessage.CreateMessage(real));
            Console.WriteLine($"接收的物联网数据并转发:{DateTime.Now.ToLocalTime()}");
            return Task.CompletedTask;
        }
    }
}
