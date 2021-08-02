/**********************************************************************
*******命名空间： MonitorPlatform.Share.Message
*******类 名 称： NoticeMessageLevel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/1/2021 11:47:25 PM
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
    /// <summary>
    /// 通知消息等级
    /// </summary>
    public enum NoticeMessageLevel
    {
        /// <summary>
        /// 成功的
        /// </summary>
        Success,
        /// <summary>
        /// 警告
        /// </summary>
        Waring,
        /// <summary>
        /// 危险
        /// </summary>
        Danger,
        /// <summary>
        /// 异常
        /// </summary>
        Error
    }
}
