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
        /// 配电室id
        /// </summary>
        public Guid PowerRoomId { get; set; }
        /// <summary>
        /// 配电室id
        /// </summary>
        public PowerRoom PowerRoom { get; set; }

        /// <summary>
        /// 设备终端
        /// </summary>
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }
        /// <summary>
        /// 采集器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址位
        /// </summary>
        public string Addr { get; set; }

        /// <summary>
        /// 采集频率
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// 传感器列表
        /// </summary>
        public List<Sensor> Sensors { get; set; }

    }
}
