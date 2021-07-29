/**********************************************************************
*******命名空间： ESTHost.WTR20AService
*******类 名 称： CommandReceiver
*******类 说 明： 控制命令接收机
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/23/2021 10:53:50 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message.Client;
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.Core.Command;

using MonitorPlatform.Share;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.WTR20AService
{
    /// <summary>
    ///  控制命令接收机
    /// </summary>
    public class CommandReceiver : IMessageReceiverHandler
    {
        /// <summary>
        /// 当前协议类型
        /// </summary>
        private readonly PtotocolType CurrentPtoto = PtotocolType.WTR_20A;
        public CommandReceiver()
        {
            
        }

        public Task Receive(BaseMessage message)
        {
            // 接收到命令
            Console.WriteLine("接收到命令消息");

            var controlMessage = message.GetMessage<RemoteControlMessage>();
            if (controlMessage.PtotocolType != this.CurrentPtoto) return Task.CompletedTask; // 不是当前协议的内容不出处理

            switch (controlMessage.ControlType)
            {
                case ControlType.Restart:
                    break;
                case ControlType.Write:
                    break;
                case ControlType.Update:
                    break;
                default:
                    break;
            }
            // 重启命令  
            // 写入传感器
            // 写入可控制的传感器数量
            // 消息内容包括：命令类型（read\write\restart）  设备id（名称，用来定位到具体的服务中去） 终端id 用来定位到具体的终端  
            // 写入命令有终端类自动解析  每次操作都需要更新缓存数据

            return Task.CompletedTask;
        }
    }
}
