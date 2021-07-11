﻿/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： DiagramConfig
*******类 说 明： 线路图配置表
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/7/2021 11:55:33 PM
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
    public class DiagramConfig:BaseEntity<Guid>
    {
        public Guid DiagramId { get; set; }
        public Diagram Diagram { get; set; }
        /// <summary>
        /// 传感器id
        /// </summary>
        public string SensorCode { get; set; }
        /// <summary>
        /// x 坐标
        /// </summary>
        public decimal PointX { get; set; }
        /// <summary>
        /// y 坐标
        /// </summary>
        public decimal PointY { get; set; }
        /// <summary>
        /// 自定义样式
        /// </summary>
        public string CustomStyle { get;set;  }
        /// <summary>
        /// 温度点名称
        /// </summary>
        public string PointName { get;set;  }
        /// <summary>
        /// 说明
        /// </summary>
        public string PointDesc { get; set; }
        /// <summary>
        /// 最后一次的温度值
        /// </summary>
        public decimal LastValue { get; set; }

        public PointStatus Status { get; set; }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        public bool IsSendMsg { get; set; }
    }
}
