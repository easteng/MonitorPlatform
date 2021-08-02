/**********************************************************************
*******命名空间： MonitorPlatform.Share.CacheItem
*******类 名 称： CacheItemSms
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 2:00:19 PM
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
    /// 短信通知服务的缓存
    /// </summary>
    public class CacheItemSms
    {
        /// <summary>
        /// 串口名称
        /// </summary>
        public string ComName { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
    }
}
