/**********************************************************************
*******命名空间： ESTHost.DataCenter.Repeater
*******类 名 称： RemoteControlRepeater
*******类 说 明： 远程控制转发器
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 6:14:53 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.Core;
using ESTHost.ProtocolBase;

using MonitorPlatform.Share;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.DataCenter.Repeater
{
    /// <summary>
    ///  远程控制转发器,用来接收远程控制命令，主要用于操作采集服务使用
    /// </summary>
    public class RemoteControlRepeater : IMessageRepeaterHandler
    {
        IMessageServerProvider serverProvider;
        public RemoteControlRepeater(IMessageServerProvider serverProvider = null)
        {
            this.serverProvider = serverProvider;
        }
        public async Task Repeater(BaseMessage message)
        {
            // 发送命令到订阅该主题的服务端
           // await this.serverProvider.Publish(MessageTopic.RemoteControlCommand, message);

            // 接收到控制命令，通过服务进行相关处理
            var control=message.GetMessage<RemoteControlMessage>();
            if (control.ControlType == ControlType.Write)
            {
                // 写入的操作
                var provider = EngineContext.Current.ResolveNamed<IBaseProtocol>(control.Ptotocol);
                if(provider != null)
                {
                   await provider.WriteSensor(control.DeviceId, control.TerminalId);
                }
            }
        }
    }
}
