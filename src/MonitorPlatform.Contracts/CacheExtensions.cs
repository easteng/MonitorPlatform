/**********************************************************************
*******命名空间： MonitorPlatform.Contracts
*******类 名 称： CacheExtensions
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 1:24:50 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Caching;

using MonitorPlatform.Share.CacheItem;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Contracts
{
    /// <summary>
    ///  缓存扩展方法
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 根据协议名称获取该协议下的设备信息
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<CacheItemDevice> GetDevicesByProtocol(this IRedisCachingProvider redis,string name)
        {
            var key = CacheItemHandler.GetProtocolCacheKey(name);
            var deviceString= redis.StringGet(key);
            return ESTCache.GetList<CacheItemDevice>(deviceString);
        }
        /// <summary>
        /// 根据设备的id获取该设备下的终端采集的信息
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static List<CacheItemTerminal> GetTerminalsByDevice(this IRedisCachingProvider redis, Guid deviceId)
        {
            var key = CacheItemHandler.GetDeviceTerminalCacheKey(deviceId);
            var terminalString = redis.StringGet(key);
            return ESTCache.GetList<CacheItemTerminal>(terminalString);
        }

        /// <summary>
        /// 根据设备的id获取设备的详细信息
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>

        public static CacheItemDeviceInfo GetDeviceInfoCache(this IRedisCachingProvider redis, Guid deviceId)
        {
            var key = CacheItemHandler.GetDeviceCacheKey(deviceId);
            var terminalString = redis.StringGet(key);
            return ESTCache.Get<CacheItemDeviceInfo>(terminalString);
        }
        /// <summary>
        /// 获取指定采集的传感器信息
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="terminalId"></param>
        /// <returns></returns>
        public static List<CacheItemSensor> GetTerminalSensorCache(this IRedisCachingProvider redis,Guid terminalId)
        {
            var key = CacheItemHandler.GetTerminalSersorCacheKey(terminalId);
            var sensor = redis.StringGet(key);
            return ESTCache.GetList<CacheItemSensor>(sensor);
        }

        /// <summary>
        /// 获取具体的采集器
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="terminalId"></param>
        /// <returns></returns>
        public static CacheItemTerminal GetTerminalInfo(this IRedisCachingProvider redis,Guid terminalId)
        {
            var key=CacheItemHandler.GetDeviceTerminalInfoCacheKey(terminalId);
            var terminalString = redis.StringGet(key);
            return ESTCache.Get<CacheItemTerminal>(terminalString);
        }
    }
}
