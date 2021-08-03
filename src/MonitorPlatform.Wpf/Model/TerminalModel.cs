using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    /// <summary>
    /// 采集终端采集器  实体
    /// </summary>
    public class TerminalModel : NotifyBase
    {
        /// <summary>
        /// 采集器名称
        /// </summary>
        private string name { get; set; }
        public string Name { get => name; set { name = value; this.DoNotify(); } }
        /// <summary>
        /// 地址位
        /// </summary>
        private string addr { get; set; }
        public string Addr { get => addr; set { addr = value; this.DoNotify(); } }

        /// <summary>
        /// 采集频率
        /// </summary>
        private int frequency { get; set; }
        public int Frequency { get => frequency; set { frequency = value; this.DoNotify(); } }

        ///// <summary>
        ///// 数据协议
        ///// </summary>
        //private PtotocolType ptotocol { get; set; }
        //public PtotocolType Ptotocol { get => ptotocol; set { ptotocol = value; this.DoNotify(); } }

        /// <summary>
        /// 预警温度
        /// </summary>
        private double warnValue{ get; set; }
        public double WarinValue { get => warnValue; set { warnValue = value; this.DoNotify(); } }
        /// <summary>
        /// 报警温度
        /// </summary>
        private double alertValue { get; set; }
        private double tolerantValue { get; set; }
        public double AlertValue { get => alertValue; set { alertValue = value; this.DoNotify(); } }
        public double TolerantValue { get => tolerantValue; set { tolerantValue = value; this.DoNotify(); } }

        /// <summary>
        /// 配电室
        /// </summary>
        private Guid powerRoomId { get; set; }
        /// <summary>
        /// 配电室
        /// </summary>
        public Guid PowerRoomId { get => powerRoomId; set { powerRoomId = value; this.DoNotify(); } }
        /// <summary>
        /// 配电室
        /// </summary>
        private PowerRoomModel powerRoom { get; set; }
        /// <summary>
        /// 配电室
        /// </summary>
        public PowerRoomModel PowerRoom { get => powerRoom; set { powerRoom = value; this.DoNotify(); } }

        /// <summary>
        /// 设备终端
        /// </summary>
        private Guid deviceId { get; set; }
        public Guid DeviceId { get => deviceId; set { deviceId = value; this.DoNotify(); } }
        private DeviceModel device { get; set; }
        public DeviceModel Device { get => device; set { device = value; this.DoNotify(); } }
    }
}
