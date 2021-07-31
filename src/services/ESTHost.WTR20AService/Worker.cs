using EasyCaching.Core;

using ESTCore.Message;
using ESTCore.Message.Client;

using ESTHost.Core.Colleaction;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonitorPlatform.Share;
using MonitorPlatform.Share.ServerCache;

using Newtonsoft.Json;

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
        private readonly IRedisCachingProvider redisCachingProvider;
        public Worker(
            ILogger<Worker> logger, IMessageClientProvider messageClient = null, IRedisCachingProvider redisCachingProvider = null)
        {
            _logger = logger;
            this.messageClient = messageClient;
            this.currentMessage = new NoticeMessage();
            this.currentMessage.ServiceName ="WTR20A协议";

            this.redisCachingProvider = redisCachingProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var devicesString = await this.redisCachingProvider.StringGetAsync("Device:WTR_20A");
            var collectionServers = JsonConvert.DeserializeObject<List<DeviceCacheItem>>(devicesString);
            if(collectionServers.Any())
              CollectionServerFactory.CreateService(collectionServers,"");
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            // �������� ������Ϣ
          
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
