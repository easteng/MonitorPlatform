using ESTCore.Message;

namespace MonitorPlatform.Share
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
        public string ServiceName { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool Online { get; set; }
        /// <summary>
        /// 消息内容，用来通知一些各服务组件报错的问题
        /// </summary>

        public string Content { get; set; }

        /// <summary>
        /// 消息等级
        /// </summary>
        public NoticeMessageLevel Level { get; set; }

        public static NoticeMessage CreateSuccessMessage(string content)
        {
            var notic= CreateNoticeInstance(content);
            notic.Level= NoticeMessageLevel.Success;
            return notic;
        }

        public static NoticeMessage CreateWaringMessage(string content)
        {
            var notic = CreateNoticeInstance(content);
            notic.Level = NoticeMessageLevel.Waring;
            return notic;
        }

        public static NoticeMessage CreateDangerMessage(string content)
        {
            var notic = CreateNoticeInstance(content);
            notic.Level = NoticeMessageLevel.Danger;
            return notic;
        }
        public static NoticeMessage CreateErrorMessage(string content)
        {
            var notic = CreateNoticeInstance(content);
            notic.Level = NoticeMessageLevel.Error;
            return notic;
        }

        private static NoticeMessage CreateNoticeInstance(string content)
        {
            var notic = new NoticeMessage();
            notic.Content = content;
            return notic;
        }
    }
}
