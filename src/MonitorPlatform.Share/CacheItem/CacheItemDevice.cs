/**********************************************************************
*******命名空间： MonitorPlatform.Share.CacheItem
*******类 名 称： CacheItemDevice
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 12:38:59 PM
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
    /// 设备缓存--用来存储服务启动时的基础信息
    /// </summary>
    public class CacheItemDevice
    {
        /// <summary>
        /// 协议类型
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public Guid DeviceId { get; set; }

        public Guid MonitorId { get; set; }

        /// <summary>
        /// 服务采集模式  客户端模式  服务端模式
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// ip 地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        public int Port { get; set; }
    }
}
