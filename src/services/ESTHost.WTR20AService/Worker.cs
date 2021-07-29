using EasyCaching.Core;

using ESTCore.Message;
using ESTCore.Message.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonitorPlatform.Share;
using MonitorPlatform.Share.ServerCache;
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
            this.redisCachingProvider = redisCachingProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            // 获取当前协议的所有的站点服务
            //var collectionString = await this.redisCachingProvider.StringGetAsync("");
           // var collectionServers = JsonConvert.DeserializeObject<List<CollectionServerCacheItem>>(collectionString);
            var list = new List<CollectionServerCacheItem>();
            list.Add(new CollectionServerCacheItem()
            {
                Name = "test",
                Ip="127.0.0.1",
                Port=502
            });
            try
            {
                //CollectionServerFactory.StartAllServer(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务启动异常");
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            // 服务启动 发送消息
            currentMessage.ServiceType = ServerType.WTR20AService;
            currentMessage.Online = true;
            messageClient.SendMessage(currentMessage); 
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            currentMessage.ServiceType = ServerType.WTR20AService;
            currentMessage.Online = false;
            messageClient.SendMessage(currentMessage);
            return base.StopAsync(cancellationToken);
        }
    }
}
