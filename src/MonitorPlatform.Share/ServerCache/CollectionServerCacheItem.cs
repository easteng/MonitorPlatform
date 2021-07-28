/**********************************************************************
*******命名空间： MonitorPlatform.Contracts.ServerCache
*******类 名 称： StationCacheItem
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 11:23:24 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Contracts.ServerCache
{
    /// <summary>
    ///  采集站点 缓存条目
    /// </summary>
    public class CollectionServerCacheItem
    {
        public Guid Id { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public List<SensorCacheItem> Sensors { get; set; }
    }

    public class SensorCacheItem
    {

    }
}
