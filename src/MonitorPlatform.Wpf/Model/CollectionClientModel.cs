/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： CollectionClientModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:15:28 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    public class CollectionClientModel : NotifyBase
    {
        private string name { get; set; }
        public string Name { get=>name; set{ name = value;this.DoNotify(); } }
        private PtotocolType ptotocol { get; set; }
        public PtotocolType Ptotocol { get => ptotocol; set { ptotocol = value; this.DoNotify(); } }
        private DeviceCollectionType type { get; set; }
        public DeviceCollectionType Type { get => type; set { type = value; this.DoNotify(); } }
        private decimal frequency { get; set; }
        public decimal Frequency { get => frequency; set { frequency = value; this.DoNotify(); } }
        private double tolerantValue { get; set; }
        public double TolerantValue { get => tolerantValue; set { tolerantValue = value; this.DoNotify(); } }
        private double warnStart { get; set; }
        public double WarnStart { get => warnStart; set { warnStart = value; this.DoNotify(); } }
        private double warnEnd { get; set; }
        public double WarnEnd { get => warnEnd; set { warnEnd = value; this.DoNotify(); } }
        private double alertEnd { get; set; }
        public double AlertEnd { get => alertEnd; set { alertEnd = value; this.DoNotify(); } }
        private double alertStart { get; set; }
        public double AlertStart { get => alertStart; set { alertStart = value; this.DoNotify(); } }
    }
}
