using ESTCore.Message;
using ESTCore.Message.Client;

using ESTHost.Core.Message;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.WTR20AService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageClientProvider messageClient;
        private readonly NoticeMessage currentMessage;

        public Worker(
            ILogger<Worker> logger, IMessageClientProvider messageClient = null)
        {
            _logger = logger;
            this.messageClient = messageClient;
            this.currentMessage=new NoticeMessage();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = new IOTMessage()
                {
                    Code = "202001",
                    Time = DateTime.Now,
                    Value = new Random().Next(0,100),
                };
                var message1 = new IOTMessage()
                {
                    Code = "202002",
                    Time = DateTime.Now,
                    Value = new Random().Next(10, 100),
                };
                await messageClient.SendMessage(message);

                await messageClient.SendMessage(message1);
                Console.WriteLine($"发送数据：{message.Value}:{DateTime.Now.ToLocalTime()}");
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            // 服务启动 发送消息
            currentMessage.ServiceType = Core.ServerType.WTR20AService;
            currentMessage.Online = true;
            messageClient.SendMessage(currentMessage); 
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            currentMessage.ServiceType = Core.ServerType.WTR20AService;
            currentMessage.Online = false;
            messageClient.SendMessage(currentMessage);
            return base.StopAsync(cancellationToken);
        }
    }
}
