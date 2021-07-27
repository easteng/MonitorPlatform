/**********************************************************************
*******命名空间： ESTHost.Core.Colleaction
*******类 名 称： CollectionServerFactory
*******类 说 明： 采集服务工厂，用来创建采集服务
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 10:48:35 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Contracts.ServerCache;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.Core.Colleaction
{
    /// <summary>
    ///   采集服务工厂，用来创建采集服务，一个站点对应一个线程  一个线拥有各自的采集逻辑  并可复用
    ///   通过工厂快速创建采集服务
    /// </summary>
    public class CollectionServerFactory
    {
        private static Dictionary<string, CollectionServices> serviceTheadDictionary;
        static CollectionServerFactory()
        {
            serviceTheadDictionary = new Dictionary<string, CollectionServices>();
        }
        public static void CreateService(CollectionServices collection)
        {
            try
            {
                if(!serviceTheadDictionary.TryGetValue(collection.Name,out var server))
                {
                    // 不存在服务，则重新创建
                    server = new CollectionServices();
                    server.CreateServer(collection.Name);// 创建服务
                    serviceTheadDictionary.TryAdd(collection.Name, server);
                }
            }
            catch (Exception ex)
            {
                // 采集服务创建异常
            }
        }
        /// <summary>
        /// 开启所有的串口服务
        /// </summary>
        /// <param name="server"></param>
        public static void StartAllServer(List<CollectionServerCacheItem> server)
        {
            if (server.Any())
            {
                foreach (var item in server)
                {
                    var cs = new CollectionServices();
                    cs.CreateServer(item);
                    CreateService(cs);
                }
            }
        }
    }
}
