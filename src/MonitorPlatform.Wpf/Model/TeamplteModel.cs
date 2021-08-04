/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： TeamplteModel
*******类 说 明： 模板样式定义实体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/15/2021 11:16:51 PM
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

namespace MonitorPlatform.Wpf.Model
{
    public class TemplateModel : NotifyBase
    {
        private Guid powerRoomId { get; set; }
        public Guid PowerRoomId { get => powerRoomId; set { powerRoomId = value; this.DoNotify(); } }
        private PowerRoomModel powerRoom { get; set; }
        public PowerRoomModel PowerRoom { get => powerRoom; set { powerRoom = value; this.DoNotify(); } }

        private Guid diagramConfigId { get; set; }
        public Guid DiagramConfigId { get => diagramConfigId; set { diagramConfigId = value; this.DoNotify(); } }
        public DiagramConfigModel DiagramConfig { get => diagramConfig; set { diagramConfig = value; this.DoNotify(); } }
        private DiagramConfigModel diagramConfig { get; set; }

        private string name { get; set; }
        public string Name { get => name; set { name = value; this.DoNotify(); } }

        private string code { get; set; }
        public string SensorCode { get => code; set { code = value; this.DoNotify(); } }


        private string borderBrush { get; set; }
        public string BorderBrush { get => borderBrush; set { borderBrush = value; this.DoNotify(); } }
        private int borderThickness { get; set; }
        public int BorderThickness { get => borderThickness; set { borderThickness = value; this.DoNotify(); } }

        private int borderWidth { get; set; }
        public int BorderWidth { get => borderWidth; set { borderWidth = value; this.DoNotify(); } }
        private int borderHeight { get; set; }
        public int BorderHeight { get => borderHeight; set { borderHeight = value; this.DoNotify(); } }

        private int cornerRadius { get; set; }
        public int CornerRadius { get => cornerRadius; set { cornerRadius = value; this.DoNotify(); } }
        private string borderBackground { get; set; }
        public string BorderBackground { get => borderBackground; set { borderBackground = value; this.DoNotify(); } }

        // 报警点的颜色
        private string badgeBackground { get; set; }
        public string BadgeBackground { get => badgeBackground; set { badgeBackground = value; this.DoNotify(); } }
        public string NormalBadgeColor { get; set; }
        public string AlertBadgeColor { get;set;  }
        public string WaringBadgeColor { get; set; }

        // 温度字体
        private int fontSize { get; set;}
        public int FontSize { get => fontSize; set { fontSize = value; this.DoNotify(); } }
        private string valueForeground { get; set; }
        public string ValueForeground { get => defaultValueForeground; set { defaultValueForeground = value; this.DoNotify(); } }
        private string defaultValueForeground { get; set; }
        public string DefaultValueForeground { get => defaultValueForeground; set { defaultValueForeground = value; this.DoNotify(); } }
        private string waringValueForegrund { get; set; }
        public string WaringValueForegrund { get => waringValueForegrund; set { waringValueForegrund = value; this.DoNotify(); } }
        private string alertValueForegrund { get; set; }
        public string AlertValueForegrund { get => alertValueForegrund; set { alertValueForegrund = value; this.DoNotify(); } }

        // 温度值
        private double vlaue;

        public double Value
        {
            get { return vlaue; }
            set { vlaue = value; this.DoNotify(); }
        }

        private PointStatus status;
        /// <summary>
        /// 温度状态
        /// </summary>
        public PointStatus Status
        {
            get { return status; }
            set { status = value; this.DoNotify(); }
        }
    }
}
