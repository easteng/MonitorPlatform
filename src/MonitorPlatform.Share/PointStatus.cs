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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share
{
    public enum PointStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 预警
        /// </summary>
        Warning,
        /// <summary>
        /// 报警状态
        /// </summary>
        Alerting
    }
}
