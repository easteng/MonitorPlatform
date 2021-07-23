using ESTCore.Message;
using ESTCore.Message.Client;

using ESTHost.Core.Message;

using MassTransit;

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

        public Worker(
            ILogger<Worker> logger,
            ICommandSender<ServiceStatusMessage> commandSender = null, IMessageClientProvider messageClient = null)
        {
            _logger = logger;
            this.messageClient = messageClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = new IOTMessage()
                {
                    Code = "01",
                    Time = DateTime.Now,
                    Value = new Random().NextDouble(),
                };
                await messageClient.SendMessage(message);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
