/**********************************************************************
*******命名空间： MonitorPlatform.Share.CacheItem
*******类 名 称： CacheItemDeviceInfo
*******类 说 明： 设备基础信息
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 12:56:40 PM
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
    /// 设备基础信息吗，用来存储设备的基础信息，报警温度等。
    /// </summary>
    public class CacheItemDeviceInfo
    {
        public string Name { get; set; }
        /// <summary>
        /// 预警温度
        /// </summary>
        public double WarnValue { get; set; }

        /// <summary>
        /// 报警温度
        /// </summary>
        public double AlertValue { get; set; }

        /// <summary>
        /// 容错温度
        /// </summary>
        public double TolerantValue { get; set; }
    }
}
