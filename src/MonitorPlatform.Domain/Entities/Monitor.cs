/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： Monitor
*******类 说 明： 监测点表
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/7/2021 11:49:44 PM
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
    public class Monitor:BaseEntity<Guid>
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
      //  public StationType Type { get; set; }
        /// <summary>
        /// 串口服务器 一个站点只能有一台服务器   多站点服务器   通过ip 地址进行定位使用
        /// </summary>
        public Device Device { get; set; }
        public Guid? DeviceId { get; set; }
        /// <summary>
        /// 父级设备
        /// </summary>
        public Monitor Parent { get; set; }
        public Guid? ParentId { get; set;  }

        public ICollection<Monitor> Children { get; set; }
    }
}
