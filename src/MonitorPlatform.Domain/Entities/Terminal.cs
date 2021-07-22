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
    /// 采集终端 采集器
    /// </summary>
    public class Terminal : BaseEntity<Guid>
    {
        /// <summary>
        /// 采集器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址位
        /// </summary>
        public string Addr { get; set; }

        /// <summary>
        /// 数据协议
        /// </summary>
        public PtotocolType Ptotocol { get; set; }
        public int Frequency { get; set; }

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
        /// 传感器列表
        /// </summary>
        public List<TerminalRltSensor> Sensors { get; set; }

    }
}
