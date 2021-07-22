using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    public class SMSConfigModel : Common.NotifyBase
    {
        /// <summary>
        /// 串口名称
        /// </summary>
        private string comName;
        public string ComName { get => comName; set { comName = value; this.DoNotify(); } }
        /// <summary>
        /// 波特率
        /// </summary>
        private int BaudRate { get => baudRate; set { baudRate = value; this.DoNotify(); } }
        /// <summary>
        public int baudRate;
        /// <summary>
        /// 数据位
        /// </summary>
        private int DataBits { get => dataBits; set { dataBits = value; this.DoNotify(); } }
        /// <summary>
        public int dataBits;

        /// <summary>
        /// 是否启用
        /// </summary>
        private bool Enable { get => enable; set { enable = value; this.DoNotify(); } }
        /// <summary>
        public bool enable;
    }
}
