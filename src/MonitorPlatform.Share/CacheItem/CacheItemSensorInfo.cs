/**********************************************************************
*******命名空间： MonitorPlatform.Share.CacheItem
*******类 名 称： CacheItemSensorInfo
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 12:43:14 PM
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
    /// 传感器信息缓存，用来短信使用
    /// </summary>
    public class CacheItemSensorInfo
    {
        public string SensorCode { get; set; }
        public string StationName { get; set; }  // 所属站点
        public string Position { get;set; }  // 安装位置
        public string RegionName { get; set; }  // 配电室名称
        public string PointName { get;set;} // 点位名称
    }
}
