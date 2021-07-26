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

namespace ESTHost.WTR31Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly NoticeMessage CurrentMessage;
        private readonly IMessageClientProvider messageClientProvider;
        public Worker(ILogger<Worker> logger, IMessageClientProvider messageClientProvider = null)
        {
            _logger = logger;
            CurrentMessage = new NoticeMessage();
            this.messageClientProvider = messageClientProvider;
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
            CurrentMessage.ServiceType = Core.ServerType.WTR31Service;
            CurrentMessage.Online = true;
            this.messageClientProvider.SendMessage(CurrentMessage);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            CurrentMessage.ServiceType = Core.ServerType.WTR31Service;
            CurrentMessage.Online = false;
            this.messageClientProvider.SendMessage(CurrentMessage);
            return base.StopAsync(cancellationToken);
        }
    }
}
