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
using ESTHost.Core.Message;

using System;
using System.Threading.Tasks;

namespace ESTHost.DataCenter
{
    /// <summary>
    ///  物联网数据转换器
    /// </summary>
    public class IotMessageRepeater : IMessageRepeaterHandler
    {
        public Task Repeater(BaseMessage message, ServerContext context)
        {
            var iot = message.GetMessage<IOTMessage>();
            context.Publish<IOTMessage>("iot", iot);
            Console.WriteLine("接收的物联网数据并转发");
            return Task.CompletedTask;
        }
    }
}
