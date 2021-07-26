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
    /// 串口服务器  一个站点对应一台设备  一台串口服务器对应多个采集器
    /// </summary>
    public class Device:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Factory { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 服务采集模式  客户端模式  服务端模式
        /// </summary>
        public DeviceCollectionType Type { get; set; }
        /// <summary>
        /// 协议类型，  一台串口服务器只能有一种采集协议
        /// </summary>
        public PtotocolType PtotocolType { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }

        /// <summary>
        ///绑定的传感器
        /// </summary>
        public IEnumerable<DeviceRltTerminal> Clients { get; set; }

    }
}
