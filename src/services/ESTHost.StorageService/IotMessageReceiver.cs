/**********************************************************************
*******命名空间： ESTHost.DataCenter
*******类 名 称： DataRepeater
*******类 说 明： 物联网数据接收机
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/23/2021 10:26:31 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Common.WebSocket;
using ESTCore.Message;
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.Core;
using ESTHost.Core.Message;

using System;
using System.Threading.Tasks;

namespace ESTHost.StorageService
{
    /// <summary>
    ///  物联网数据接收机
    /// </summary>
    public class IotMessageReceiver : IMessageReceiverHandler
    {
        public Task Receive(BaseMessage message)
        {
            Console.WriteLine($"接收到物联网数据，并进行存储:{DateTime.Now.ToLocalTime()}");
            return Task.FromResult(0);  
        }
    }
}
