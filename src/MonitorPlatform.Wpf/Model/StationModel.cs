/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： StationModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/2/2021 4:36:30 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
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
    /// <summary>
    ///  站带视图对象
    /// </summary>
    public class StationModel: NotifyBase
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        private string name { get; set; }
        public string Name { get => name; set { name = value; this.DoNotify(); } }

        /// <summary>
        /// 站点描述
        /// </summary>
        private string desc { get; set; }
        public string Desc { get => desc; set { desc = value; this.DoNotify(); } }
        /// <summary>
        /// 站点下的配电室
        /// </summary>
        private List<PowerRoomModel> rooms { get; set; }
        public List<PowerRoomModel> Rooms { get => rooms; set { rooms = value; this.DoNotify(); } }
        /// <summary>
        ///站点下的设备信息
        /// </summary>
        private List<DeviceModel> devices { get; set; }
        public List<DeviceModel> Devices { get => devices; set { devices = value; this.DoNotify(); } }
    }
}
