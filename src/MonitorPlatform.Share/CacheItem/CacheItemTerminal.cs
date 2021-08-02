/**********************************************************************
*******命名空间： MonitorPlatform.Share.CacheItem
*******类 名 称： CacheItemTerminal
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 12:42:02 PM
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
    /// 采集器信息
    /// </summary>
    public class CacheItemTerminal
    {
        /// <summary>
        /// 终端的485地址
        /// </summary>
        public short Addr { get;set;}
        /// <summary>
        /// 终端的id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 传感器的个数
        /// </summary>
        public int SensorCount { get; set;  }
        /// <summary>
        /// 是否当前可用
        /// </summary>
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 设备id
        /// </summary>
        public Guid DeviceId { get; set; }
    }
}
