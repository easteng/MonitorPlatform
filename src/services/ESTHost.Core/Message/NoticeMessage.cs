using ESTCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Core.Message
{
    /// <summary>
    /// 通知消息体，用于各种服务上线后通知最终客户端，发现服务是否上线
    /// </summary>
    public class NoticeMessage:AbstractMessage
    {
        /// <summary>
        /// 主题
        /// </summary>
        public override string Topic { get => MessageTopic.Notice; set => base.Topic = value; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public ServerType ServiceType { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool Online { get; set; }
    }
}
