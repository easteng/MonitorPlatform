/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： Sensor
*******类 说 明： 传感器表
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:00:38 AM
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
    public class Sensor : BaseEntity<Guid>
    {
        /// <summary>
        /// 传感器编号
        /// </summary>
        public string SensorCode { get; set; }
        /// <summary>
        /// 安装位置
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
