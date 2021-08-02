/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： Station
*******类 说 明： 站点表
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/2/2021 3:49:24 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
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
    ///  站点表
    /// </summary>
    public class Station : BaseEntity<Guid>
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 站点描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 站点下的配电室
        /// </summary>
        public List<PowerRoom> Rooms { get; set; }
        /// <summary>
        ///站点下的设备信息
        /// </summary>
        public List<Device> Devices { get; set; }
    }
}
