using ESTCore.Message;

using MassTransit;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        readonly IBus _bus;
        readonly ICommandSender<ServiceStatusMessage> _commandSender;  // √¸¡Ó∑¢ÀÕ
        public Worker(ILogger<Worker> logger, IBus bus = null, ICommandSender<ServiceStatusMessage> commandSender = null)
        {
            _logger = logger;
            _bus = bus;
            _commandSender = commandSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _commandSender.Send(ServiceType.Storage, ServiceStatus.Runting);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
           _commandSender.Send(ServiceType.Storage, ServiceStatus.Start);
           return base.StartAsync(cancellationToken);  
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _commandSender.Send(ServiceType.Storage, ServiceStatus.Stop);
            return base.StopAsync(cancellationToken);
        }
    }
}
