/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： DiagramConfigModel
*******类 说 明： 图片配置表实体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/14/2021 9:44:33 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonitorPlatform.Wpf.Model
{
    public class DiagramConfigModel: NotifyBase
    {
        public DiagramConfigModel() { }
        public DiagramConfigModel(string name,Point point) {
            this.PropName=name; 
            this.PointX = point.X;
            this.PointY = point.Y;  
        }

        public Guid DiagramId { get; set; }
        public Diagram Diagram { get; set; }
        /// <summary>
        /// 传感器id
        /// </summary>
        private string sensorCode { get; set; }
        public string SensorCode { get => sensorCode; set { sensorCode = value; this.DoNotify(); } }

        private Point point;

        public Point Point
        {
            get { return point; }
            set { point = value;this.DoNotify(); }
        }
        /// <summary>
        /// x 坐标
        /// </summary>
        private double pointX { get; set; }
        public double PointX { get => pointX; set { pointX = value; this.DoNotify(); } }
        /// <summary>
        /// y 坐标
        /// </summary>
        private double pointY { get; set; }
        public double PointY { get => pointY; set { pointY = value; this.DoNotify(); } }
        /// <summary>
        /// 自定义样式
        /// </summary>
        private string customStyle { get; set; }
        public string CustomStyle { get => customStyle; set { customStyle = value; this.DoNotify(); } }
        /// <summary>
        /// 温度点名称
        /// </summary>
        private string pointName { get; set; }
        public string PointName { get => pointName; set { pointName = value; this.DoNotify(); } }
        /// <summary>
        /// 属性名称  est+时间
        /// </summary>
        private string propName { get; set; }
        public string PropName { get => propName; set { propName = value; this.DoNotify(); } }
        /// <summary>
        /// 最后一次的温度值
        /// </summary>
        private decimal lastValue { get; set; }
        public decimal LastValue { get => lastValue; set { lastValue = value; this.DoNotify(); } }

        private PointStatus status { get; set; }
        public PointStatus Status { get => status; set { status = value; this.DoNotify(); } }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        private bool isSendMsg { get; set; }
        public bool IsSendMsg { get => isSendMsg; set { isSendMsg = value; this.DoNotify(); } }

        /// <summary>
        /// 是否保存
        /// </summary>
        public bool IsSave { get; set; }
    }
}
