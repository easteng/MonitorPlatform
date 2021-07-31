using ESTCore.Message;
using ESTCore.Message.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MonitorPlatform.Share;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.StorageService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageClientProvider messageClient;
        private readonly NoticeMessage currentMessage;
        public Worker(ILogger<Worker> logger, IMessageClientProvider messageClient = null)
        {
            _logger = logger;
            this.messageClient = messageClient;
            this.currentMessage = new NoticeMessage();
            this.currentMessage.ServiceName="数据存储服务";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            currentMessage.Online = true;
            messageClient.SendMessage(currentMessage);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            currentMessage.Online = false;
            messageClient.SendMessage(currentMessage);
            return base.StopAsync(cancellationToken);
        }
    }
}
