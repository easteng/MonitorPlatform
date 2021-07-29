using ESTCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Domain.Entities
{
    /// <summary>
    /// 采集器关联传感器
    /// </summary>
    public class TerminalRltSensor:BaseEntity<Guid>
    {
        /// <summary>
        /// 传感器序号
        /// </summary>
        public int SensorNo { get; set;  }
        public Terminal Terminal {  get; set; }
        public Guid TerminalId { get; set; }    

        public Sensor Sensor {  get; set; }
        public Guid SensorId {  get; set; }
    }
}
