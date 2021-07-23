/**********************************************************************
*******命名空间： ESTHost.SMSService
*******类 名 称： AlertDataReceiver
*******类 说 明： 报警数据接收机
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/24/2021 12:31:04 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.SMSService
{
    /// <summary>
    /// 报警数据接收机，接收到数据，并发短信
    /// </summary>
    public class AlertDataReceiver : IMessageReceiverHandler
    {
        public Task Receive(BaseMessage message)
        {
            Console.WriteLine("接收到报警的物联网数据，执行短信发送");
            return Task.FromResult(0);  
        }
    }
}
