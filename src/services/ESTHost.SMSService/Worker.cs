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

namespace ESTHost.SMSService
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
            currentMessage.ServiceType = Core.ServerType.SmsService;
            currentMessage.Online = true;
            messageClient.SendMessage(currentMessage);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            currentMessage.ServiceType = Core.ServerType.SmsService;
            currentMessage.Online = false;
            messageClient.SendMessage(currentMessage);
            return base.StopAsync(cancellationToken);
        }
    }
}
