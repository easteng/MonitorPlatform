/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： Device
*******类 说 明： 设备采集服务表
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:06:31 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
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
    /// 设备表
    /// </summary>
    public class Device:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Factory { get; set; }
        public string Description { get; set; }
        public DeviceCollectionType Type { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }

        /// <summary>
        ///绑定的传感器
        /// </summary>
        public IEnumerable<DeviceRltSensor> Sensors { get; set; }
        public IEnumerable<DeviceRltClient> Clients { get; set; }

    }
}
