/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： History
*******类 说 明： 温度历史记录表
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:23:08 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Domain.Entities
{
    public class HistoryRecord:BaseEntity<Guid>
    {
        /// <summary>
        /// 监测点名称
        /// </summary>
        public string MonitorName { get; set;  }

        public Guid MonitorId { get; set;  }

        /// <summary>
        /// 传感器id
        /// </summary>
        public string SensorCode { get; set; }
        /// <summary>
        /// 温度值
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }

    }
}
