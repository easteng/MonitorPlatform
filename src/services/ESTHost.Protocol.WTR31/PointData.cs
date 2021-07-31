/**********************************************************************
*******命名空间： ESTHost.WTR20AService.Collections
*******类 名 称： PointData
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/29/2021 3:20:09 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Protocol.WTR31
{
    /// <summary>
    ///  测温点数据
    /// </summary>
    public class PointData
    {
        /// <summary>
        /// 传感器的需要，需要从传感器列表中获取对应的传感器编号
        /// </summary>
        public byte PointNo { get; set; }
        /// <summary>
        /// 温度值
        /// </summary>
        public double Temp { get; set; }

        /// <summary>
        /// 数据采集时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public ushort Byte { get; set; }

        /// <summary>
        /// 电池电压
        /// </summary>
        public byte Battery { get; set; } = 0xFF;

        /// <summary>
        /// 电池电压描述
        /// </summary>
        public string BatteryStatus
        {
            get
            {
                switch (Battery)
                {
                    case 0:
                        return "未知";
                    case 1:
                        return "低";
                    case 2:
                        return "中";
                    case 3:
                        return "高";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 是否离线
        /// </summary>
        public bool OffLine { get; set; } = false;
        /// <summary>
        /// 传感器状态-- 0正常 1故障
        /// </summary>
        public byte PointState { get; set; } = 0;
    }
}
