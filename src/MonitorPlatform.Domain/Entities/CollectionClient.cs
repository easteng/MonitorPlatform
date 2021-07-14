/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： ColllectionClient
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:14:25 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Domain.Entity;

using MonitorPlatform.Share;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Domain.Entities
{
    /// <summary>
    /// 采集客户端配置
    /// </summary>
    public class CollectionClient : BaseEntity<Guid>
    {
        public string Name { get; set; }
        /// <summary>
        /// 数据协议
        /// </summary>
        public PtotocolType Ptotocol { get; set; }

        /// <summary>
        /// 采集模式  server  client
        /// </summary>
        public DeviceCollectionType Type { get; set; }

        /// <summary>
        /// 温度采集频率
        /// </summary>
        public decimal Frequency { get; set; }
        /// <summary>
        /// 容错温度，前后温度超过这个值，则不做处理
        /// </summary>

        public decimal TolerantValue { get; set; }

        public decimal WarnStart { get; set; }
        public decimal WarnEnd { get; set; }
        public decimal AlertEnd { get; set; }
        public decimal AlertStart { get; set; }
    }
}
