/**********************************************************************
*******命名空间： MonitorPlatform.Share
*******类 名 称： ExceptionType
*******类 说 明： 温度异常类型
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:26:13 AM
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
    public enum ExceptionType
    {
        [Display(Name = "预警状态")]
        [Description("预警状态")]
        /// <summary>
        /// 预警
        /// </summary>
        Warning,
        [Display(Name = "报警状态")]
        [Description("报警状态")]
        /// <summary>
        /// 报警
        /// </summary>
        Alert
    }
}
