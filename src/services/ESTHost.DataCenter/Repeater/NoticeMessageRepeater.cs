﻿using ESTCore.Message.Handler;
using ESTCore.Message.Message;
using ESTHost.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.DataCenter
{
    /// <summary>
    /// 通知消息转发器
    /// </summary>
    public class NoticeMessageRepeater : IMessageRepeaterHandler
    {
        IMessageServerProvider serverProvider;
        public NoticeMessageRepeater(IMessageServerProvider serverProvider = null)
        {
            this.serverProvider = serverProvider;
        }
        public Task Repeater(BaseMessage message)
        {
            Console.WriteLine("接收到通知消息，并转发");
            serverProvider.Publish(MessageTopic.Notice, message);
            return Task.CompletedTask;
        }
    }
}
