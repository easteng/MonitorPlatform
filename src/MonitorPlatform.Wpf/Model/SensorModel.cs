/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： SensorModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 9:57:44 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    public class SensorModel: NotifyBase
    {
        private string sensorCode;

        public string SensorCode
        {
            get { return sensorCode; }
            set { sensorCode = value; this.DoNotify(); }
        }

        private string position;

        public string Position
        {
            get { return position; }
            set { position = value; this.DoNotify(); }
        }
        private string  remark;

        public string  Remark
        {
            get { return remark; }
            set { remark = value; this.DoNotify(); }
        }

        /// <summary>
        /// 采集器
        /// </summary>
        private Guid terminalId { get; set; }
        /// <summary>
        /// 采集器
        /// </summary>
        public Guid TerminalId { get => terminalId; set { terminalId = value; this.DoNotify(); } }
        /// <summary>
        /// 采集器
        /// </summary>
        private TerminalModel terminal { get; set; }
        /// <summary>
        /// 采集器
        /// </summary>
        public TerminalModel Terminal { get => terminal; set { terminal = value; this.DoNotify(); } }
    }
}
