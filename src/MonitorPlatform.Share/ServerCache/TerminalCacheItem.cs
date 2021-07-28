/**********************************************************************
*******命名空间： MonitorPlatform.Contracts.ServerCache
*******类 名 称： TerminalCacheItem
*******类 说 明： 采集器缓存
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 11:26:55 AM
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
    ///  终端采集器缓存内容
    /// </summary>
    public class TerminalCacheItem
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 采集器地址位
        /// </summary>
        public int Addr { get; set; }
        /// <summary>
        /// 预警温度
        /// </summary>
        public double WarinValue { get; set; }
        /// <summary>
        /// 报警温度
        /// </summary>
        public double AlertValue { get; set; }
        /// <summary>
        /// 容错温度
        /// </summary>
        public double TolerantValue { get; set; }

        /// <summary>
        /// 是否可用，由于不同终端设置有所不同，对与数据写入来说，
        /// 当该终端执行写入时，不应该再读取数据 默认时true
        /// </summary>
        public bool Enabled { get; set; } = true;
    }
}
