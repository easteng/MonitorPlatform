/**********************************************************************
*******命名空间： MonitorPlatform.Share.Message
*******类 名 称： StandardMessage
*******类 说 明： 标准数据消息体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 12:02:00 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share.Message
{
    /// <summary>
    /// 标准数据消息体
    /// </summary>
    public class StandardMessage
    {
        /// <summary>
        /// 传感器id
        /// </summary>
        public string SensorCode { get; set;  }
        /// <summary>
        /// 温度状态
        /// </summary>
        public PointStatus Status { get; set; }
        /// <summary>
        /// 温度值
        /// </summary>
        public double Value { get;set;  }
        /// <summary>
        /// 电池电压
        /// </summary>
        public decimal Battary { get; set;  }
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime Time { get; set;  }
        /// <summary>
        /// 采集器id
        /// </summary>
        public Guid TerminalId { get; set; }
    }
}
