/**********************************************************************
*******命名空间： MonitorPlatform.Contracts.ServerCache
*******类 名 称： SensorCacheItem
*******类 说 明： 传感器缓存实体
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 11:31:30 AM
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
    ///  传感器缓存实体
    /// </summary>
    public class SensorCacheItem
    {
        /// <summary>
        /// 指定采集器中传感器的编号
        /// </summary>
        public int SensorNo { get; set; }
        /// <summary>
        /// 传感器编号
        /// </summary>
        public string SensorCode { get; set; }
        /// <summary>
        /// 安装位置
        /// </summary>
        public string Position { get; set; }
    }
}
