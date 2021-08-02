/**********************************************************************
*******命名空间： MonitorPlatform.Share.CacheItem
*******类 名 称： CacheItemHandler
*******类 说 明： 缓存数据帮助类
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 12:51:26 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share.CacheItem
{
    [Serializable]
    /// <summary>
    /// 缓存内容帮助类
    /// </summary>
    public class CacheItemHandler
    {
        public static string GetProtocolCacheKey(string name) => $"Protocol:{name}";
        public static string GetDeviceTerminalCacheKey(Guid id) => $"Device:Terminal:{id.ToString()}";
        public static string GetDeviceCacheKey(Guid id) => $"Device:{id.ToString()}";
        public static string GetTerminalSersorCacheKey(Guid id) => $"Terminal:Sensor:{id.ToString()}";
        public static string GetDeviceTerminalInfoCacheKey(Guid id) => $"Terminal:{id.ToString()}";
        public static string GetSensorInfoCacheKey(string code) => $"Sensor:{code}";
        public static string GetSmsCacheKey() => $"Sms";
        public static string GetSmsConfigCacheKey(Guid monitorId) => $"Sms:{monitorId}";
    }
}
