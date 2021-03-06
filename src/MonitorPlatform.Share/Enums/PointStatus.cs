/**********************************************************************
*******命名空间： MonitorPlatform.Share
*******类 名 称： PointStatus
*******类 说 明： 温度的的状态
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/8/2021 12:03:38 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share
{
    public enum PointStatus
    {
        [Display(Name = "正常")]
        [Description("正常")]
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        [Display(Name = "预警")]
        [Description("预警")]
        /// <summary>
        /// 预警
        /// </summary>
        Warning,
        [Display(Name = "报警")]
        [Description("报警")]
        /// <summary>
        /// 报警状态
        /// </summary>
        Alerting
    }
}
