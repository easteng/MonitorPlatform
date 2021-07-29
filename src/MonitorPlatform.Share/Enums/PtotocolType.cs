/**********************************************************************
*******命名空间： MonitorPlatform.Share
*******类 名 称： PtotocolType
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:16:51 AM
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
    public enum PtotocolType
    {
        [Display(Name = "银澳WTR-31协议")]
        [Description("银澳WTR-31协议")]
        WTR_31,
        [Display(Name = "银澳WTR-20A协议")]
        [Description("银澳WTR-20A协议")]
        WTR_20A
    }
}
