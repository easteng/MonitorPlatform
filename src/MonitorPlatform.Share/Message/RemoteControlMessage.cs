/**********************************************************************
*******命名空间： ESTHost.Core.Message
*******类 名 称： ControlCommandMessage
*******类 说 明： 控制命令消息体
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 6:09:00 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share
{
    /// <summary>
    ///  远程控制消息体
    /// </summary>
    public class RemoteControlMessage:AbstractMessage
    {
        /// <summary>
        /// 主题
        /// </summary>
        public override string Topic { get => MessageTopic.RemoteControlCommand; set => base.Topic = value; }
        /// <summary>
        /// 控制类型
        /// </summary>
        public ControlType ControlType { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public Guid DeviceId { get; set; }
        /// <summary>
        /// 终端id
        /// </summary>
        public Guid TerminalId { get; set; }
        /// <summary>
        /// 当前要控制的协议类型
        /// </summary>
        public string Ptotocol{ get; set; }
    }
}
