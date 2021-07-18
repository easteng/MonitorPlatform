/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： TemplateStyle
*******类 说 明： 温度模板样式
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/15/2021 11:33:34 PM
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
    /// 模板样式实体
    /// </summary>
    public class TemplateStyle:BaseEntity<Guid>
    {
        // 外边框的样式
        public Guid MonitorId { get; set; }
        public Monitor Monitor { get; set; }    

        public string BorderBrush { get; set; }
        public int BorderThickness { get; set; }

        public int BorderWidth { get; set; }    
        public int BorderHeight { get; set; }   

        public int CornerRadius { get; set; }
        public string BorderBackground { get; set; }

        // 报警点的颜色
        public string BadgeBackground { get; set; }
        public string BadgeBorderBrush { get; set; }

        // 温度字体
        public int FontSize { get; set; }
        public string ValueForeground { get; set; } 
        public string DefaultValueForeground { get; set; } 
        public string WaringValueForegrund { get; set; }
        public string AlertValueForegrund { get; set; }

    }
}
