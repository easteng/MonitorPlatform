/**********************************************************************
*******命名空间： MonitorPlatform.Contracts
*******类 名 称： CacheFactory
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 1:10:27 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Caching;

using MonitorPlatform.Share.CacheItem;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Contracts
{
    /// <summary>
    /// 缓存工厂，用来处理系统中的缓存问题
    /// 
    /// </summary>
    public class CacheFactory
    {
        private static IRedisCachingProvider redisCachingProvider;
        private static IEasyCachingProvider cachingProvider;
        static CacheFactory()
        {
            redisCachingProvider = EngineContext.Current.Resolve<IRedisCachingProvider>();
            cachingProvider = EngineContext.Current.Resolve<IEasyCachingProvider>();
        }
        /// <summary>
        /// 添加或更新某中协议的设备（站点信息）
        /// </summary>
        /// <param name="protocol">协议名称</param>
        /// <param name="devices">该协议下的设备或站点信息</param>
        public static void AddOrUpdateDeviceCache(string protocol, List<CacheItemDevice> devices)
        {
            var key = CacheItemHandler.GetProtocolCacheKey(protocol);
            //cachingProvider.Set(key, ESTCache.GetCacheString(devices), TimeSpan.FromDays(365));
            redisCachingProvider?.StringSet(key, ESTCache.GetCacheString(devices));
        }
        /// <summary>
        /// 添加或更新终端信息
        /// </summary>
        /// <param name="deviceId">设备的id</param>
        /// <param name="terminals">终端列表</param>

        public static void AddOrUpdateTerminalCache(Guid deviceId, List<CacheItemTerminal> terminals)
        {
            var key = CacheItemHandler.GetDeviceTerminalCacheKey(deviceId);
            redisCachingProvider?.StringSet(key, ESTCache.GetCacheString(terminals));
            //cachingProvider.Set(key, ESTCache.GetCacheString(terminals), TimeSpan.FromDays(365));
        }
        /// <summary>
        /// 添加采集的信息缓存
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="terminal"></param>
        public static void AddOrUpdateTerminalInfoCache(Guid deviceId, CacheItemTerminal terminal)
        {
            var key = CacheItemHandler.GetDeviceTerminalInfoCacheKey(deviceId);
            redisCachingProvider?.StringSet(key, ESTCache.GetCacheString(terminal));
            //cachingProvider.Set(key, ESTCache.GetCacheString(terminal), TimeSpan.FromDays(365));
        }

        /// <summary>
        /// 添加或更新某一设备的缓存数据
        /// </summary>
        /// <param name="deviceId">设备的id</param>
        /// <param name="device">设备信息</param>
        public static void AddOrUpdateDeviceInfoCache(Guid deviceId, CacheItemDeviceInfo device)
        {
            var key = CacheItemHandler.GetDeviceCacheKey(deviceId);
            redisCachingProvider?.StringSet(key, ESTCache.GetCacheString(device));
            //cachingProvider.Set(key, ESTCache.GetCacheString(device), TimeSpan.FromDays(365));
        }
        /// <summary>
        /// 添加或更新某一采集中绑定的传感器信息
        /// </summary>
        /// <param name="terminalId"></param>
        /// <param name="sensors"></param>
        public static void AddOrUpdateTerminalSensorCache(Guid terminalId, List<CacheItemSensor> sensors)
        {
            var key = CacheItemHandler.GetTerminalSersorCacheKey(terminalId);
            redisCachingProvider?.StringSet(key, ESTCache.GetCacheString(sensors));
            //cachingProvider.Set(key, ESTCache.GetCacheString(sensors), TimeSpan.FromDays(365));
        }
        /// <summary>
        /// 添加或更新传感器的详细信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="sensorInfo"></param>
        public static void AddOrUpdateSensorInfoCache(string code, CacheItemSensorInfo sensorInfo)
        {
            var key = CacheItemHandler.GetSensorInfoCacheKey(code);
            //cachingProvider.Set(key, ESTCache.GetCacheString(sensorInfo), TimeSpan.FromDays(365));
            redisCachingProvider?.StringSet(key, ESTCache.GetCacheString(sensorInfo));
        }

        /// <summary>
        /// 更新短信配置
        /// </summary>
        /// <param name="sms"></param>
        public static void AddOrUpdateSmsCache(CacheItemSms sms)
        {
            var key = CacheItemHandler.GetSmsCacheKey();
            //cachingProvider.Set(key, ESTCache.GetCacheString(sms), TimeSpan.FromDays(365));
            redisCachingProvider?.StringSet(key, ESTCache.GetCacheString(sms));
        }
        /// <summary>
        /// 添加或更新短信配置缓存
        /// </summary>
        /// <param name="monitorId"></param>
        /// <param name="config"></param>
        public static void AddOrUpdateSmsConfigCache(Guid monitorId, CacheItemSmsConfig config)
        {
            var key = CacheItemHandler.GetSmsConfigCacheKey(monitorId);
            //cachingProvider.Set(key, ESTCache.GetCacheString(config), TimeSpan.FromDays(365));
            redisCachingProvider?.StringSet(key, ESTCache.GetCacheString(config));
        }

        public static void DeleteCache()
        {
            cachingProvider?.RemoveByPrefix("Device");
            cachingProvider?.RemoveByPrefix("Protocol");
            cachingProvider?.RemoveByPrefix("Sensor");
            cachingProvider?.RemoveByPrefix("Terminal");
        }
        public static void DeleteCache(string key)
        {
            cachingProvider?.RemoveByPrefix(key);
        }
    }
}
