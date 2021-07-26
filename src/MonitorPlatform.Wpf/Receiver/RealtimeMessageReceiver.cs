/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Receiver
*******类 名 称： RealtimeMessageReceiver
*******类 说 明： 实时数据接收机，接收各种实时数据
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/24/2021 1:25:55 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.Core.Message;

using MonitorPlatform.Share;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Receiver
{
    /// <summary>
    /// 实时数据接收机， 接收数据中心发送来的实时数据，并通过委托将数据绑定到对应的界面
    /// </summary>
    public class RealtimeMessageReceiver : IMessageReceiverHandler
    {
        public Task Receive(BaseMessage message)
        {
            var real = message.GetMessage<RealtimeMessage>();
            GlableDelegateHandler.UpdateRealtimeData?.Invoke(real); //
            return Task.FromResult(0);
        }
    }
}
