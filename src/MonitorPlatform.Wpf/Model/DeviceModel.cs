/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： DeviceManagerModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 11:29:32 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using Masuit.Tools.Core.Validator;

using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    public class DeviceModel : NotifyBase
    {
        private string name { get; set; }
        public string Name { get => name; set { name = value; this.DoNotify(); } }


        private string factory { get; set; }
        public string Factory { get => factory; set { factory = value; this.DoNotify(); } }
        private string description { get; set; }
        public string Description { get => description; set { description = value; this.DoNotify(); } }
        private string type { get; set; }
        public string Type { get => type; set { type = value; this.DoNotify(); } }
        private string ptotocol { get; set; }
        public string Ptotocol{ get => ptotocol; set { ptotocol = value; this.DoNotify(); } }
        private string ipAddress { get; set; }
        [IsIPAddress]
        public string IpAddress { get => ipAddress; set { ipAddress = value; this.DoNotify(); } }
        private int port { get; set; }
        public int Port { get => port; set { port = value; this.DoNotify(); } }
        private double warnValue { get; set; }
        /// <summary>
        /// 预警温度
        /// </summary>
        public double WarnValue { get => warnValue; set { warnValue = value; this.DoNotify(); } }
        private double alertValue { get; set; }
        /// <summary>
        /// 报警温度
        /// </summary>
        public double AlertValue { get => alertValue; set { alertValue = value; this.DoNotify(); } }
        private double tolerantValue { get; set; }
        /// <summary>
        /// 容错温度
        /// </summary>
        public double TolerantValue { get => tolerantValue; set { tolerantValue = value; this.DoNotify(); } }

        private Guid monitorId { get; set; }
        /// <summary>
        /// 容错温度
        /// </summary>
        public Guid MonitorId { get => monitorId; set { monitorId = value; this.DoNotify(); } }

        /// <summary>
        /// 站点
        /// </summary>
        private StationModel station { get; set; }
        public StationModel Station { get => station; set { station = value; this.DoNotify(); } }
    }
}
