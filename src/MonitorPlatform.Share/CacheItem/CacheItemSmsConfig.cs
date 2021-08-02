/**********************************************************************
*******命名空间： MonitorPlatform.Share.CacheItem
*******类 名 称： CacheItemSmsConfig
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 2:03:08 PM
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
    /// 短信配置缓存
    /// </summary>
    public class CacheItemSmsConfig
    {
        /// <summary>
        /// 需要发短信的传感器编号
        /// </summary>
        public List<string> SensorCodes { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public List<string> Phones { get; set; }

    }
}
