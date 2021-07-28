using ESTCore.Message.Handler;
using ESTCore.Message.Message;
using HandyControl.Controls;
using HandyControl.Data;
using Masuit.Tools.Systems;
using MonitorPlatform.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Receiver
{
    /// <summary>
    /// 通知消息接收机
    /// </summary>
    public class NoticeMessageReceiver : IMessageReceiverHandler
    {
        public Task Receive(BaseMessage message)
        {
            // 接受到消息，并进行通知提示
            var notice = message.GetMessage<NoticeMessage>();
            Notice(notice);
            return Task.CompletedTask;
        }

        private void Notice(NoticeMessage message)
        {
            // 获取服务名称
            var serverName = message.ServiceType.GetDisplay();
            var online = message.Online ? "已启动" : "已断开";
            var content = $"{serverName}{online}";
            GlableDelegateHandler.SystemNotice?.Invoke(content);
        }
    }
}
