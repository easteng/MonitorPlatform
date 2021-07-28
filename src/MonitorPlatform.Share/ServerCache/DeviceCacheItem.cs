/**********************************************************************
*******命名空间： MonitorPlatform.Share.ServerCache
*******类 名 称： DeviceCacheItem
*******类 说 明： 设备缓存
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/28/2021 7:49:04 PM
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

namespace MonitorPlatform.Share.ServerCache
{
    /// <summary>
    ///  设备缓存
    /// </summary>
    public class DeviceCacheItem
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 站点id
        /// </summary>
        public Guid MonitorId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 协议类型，  一台串口服务器只能有一种采集协议
        /// </summary>
        public PtotocolType PtotocolType { get; set; }
        /// <summary>
        /// 设备通讯ip 地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 设备通讯端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        ///绑定的采集器
        /// </summary>
        public List<TerminalCacheItem> Terminal { get; set; }
    }
}
