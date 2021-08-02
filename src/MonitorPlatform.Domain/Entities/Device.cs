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
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 监测点id
        /// </summary>
        public Guid StationId { get; set;  }

        /// <summary>
        /// 监测点
        /// </summary>
        public Station Station { get; set;  }

        /// <summary>
        /// 服务采集模式  客户端模式  服务端模式  串口模式 4g模式
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 协议类型，  一台串口服务器只能有一种采集协议
        /// </summary>
        public string Ptotocol { get; set; }

        /// <summary>
        /// ip 地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// com 端口
        /// </summary>
        public string Com { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public int Check { get; set; }


        /// <summary>
        /// 预警温度
        /// </summary>
        public double WarnValue { get; set; }

        /// <summary>
        /// 报警温度
        /// </summary>
        public double AlertValue { get; set; }

        /// <summary>
        /// 容错温度
        /// </summary>
        public double TolerantValue { get; set; }

    }
}
