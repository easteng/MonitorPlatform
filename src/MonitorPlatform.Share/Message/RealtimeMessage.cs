/**********************************************************************
*******命名空间： ESTHost.Core.Message
*******类 名 称： RealtimeMessage
*******类 说 明： 实时数据消息体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/24/2021 1:29:44 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;

using MonitorPlatform.Share.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share
{
    /// <summary>
    /// 实时数据消息体
    /// </summary>
    public class RealtimeMessage: AbstractMessage
    {
        public override string Topic { get => MessageTopic.Realtime; set => base.Topic = value; }
        public RealtimeMessage(StandardMessage message)
        {
            this.StandardMessage = message;
        }
        public StandardMessage StandardMessage { get; set; }
    }
}
