using ESTCore.Message.Handler;
using ESTCore.Message.Message;
using System;
using System.Threading.Tasks;

namespace ESTHost.WTR31Service
{
    /// <summary>
    /// 命令接收机
    /// </summary>
    public class CommandReceiver : IMessageReceiverHandler
    {
        readonly CommandReceiver commandReceiver;
        public CommandReceiver(CommandReceiver commandReceiver = null)
        {
            this.commandReceiver = commandReceiver;
        }
        public Task Receive(BaseMessage message)
        {
            // 接收到命令
            Console.WriteLine("接收到命令消息");
            return Task.CompletedTask;
        }
    }
}
