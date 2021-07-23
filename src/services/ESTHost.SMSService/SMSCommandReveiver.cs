/**********************************************************************
*******命名空间： ESTHost.SMSService
*******类 名 称： SMSCommandReveiver
*******类 说 明： 短信服务命令接收机
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/24/2021 12:36:52 AM
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
    /// 短信服务命令接收机  接收命令，用于重启短信服务或者重置数据使用
    /// </summary>
    public class SMSCommandReveiver : IMessageReceiverHandler
    {
        public Task Receive(BaseMessage message)
        {
            Console.WriteLine("接收到控制命令消息");
            return Task.FromResult(0);
        }
    }
}
