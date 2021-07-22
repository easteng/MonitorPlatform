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
        public int BaudRate { get => baudRate; set { baudRate = value; this.DoNotify(); } }
        /// <summary>
        private int baudRate;
        /// <summary>
        /// 数据位
        /// </summary>
        private int dataBits;
        /// <summary>
        public int DataBits { get => dataBits; set { dataBits = value; this.DoNotify(); } }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get => enable; set { enable = value; this.DoNotify(); } }
        /// <summary>
        private bool enable;
    }
}
