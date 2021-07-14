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
        private DeviceCollectionType type { get; set; }
        public DeviceCollectionType Type { get => type; set { type = value; this.DoNotify(); } }
        private string ipAddress { get; set; }
        [IsIPAddress]
        public string IpAddress { get => ipAddress; set { ipAddress = value; this.DoNotify(); } }
        private int port { get; set; }
        public int Port { get => port; set { port = value; this.DoNotify(); } }
    }
}
