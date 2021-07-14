/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： DeviceRltSensor
*******类 说 明： 设备管理传感器
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:11:24 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Domain.Entities
{
    /// <summary>
    /// 设备传感关联表
    /// </summary>
    public class DeviceRltSensor:BaseEntity<Guid>
    {
        public DeviceRltSensor() { }
        public DeviceRltSensor(Guid deviceId, Guid sensorId)
        {
            this.DeviceId = deviceId;
            this.SensorId = sensorId;
        }
        public Sensor Sensor { get; set; }  
        public Guid SensorId { get; set; }  
        public Device Device { get; set; }
        public Guid DeviceId { get; set; }  
    }
}
