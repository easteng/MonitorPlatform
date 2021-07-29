/**********************************************************************
*******命名空间： MonitorPlatform.Share.Enums
*******类 名 称： ControlType
*******类 说 明： 控制类型
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/29/2021 5:38:04 PM
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

namespace MonitorPlatform.Share
{
    /// <summary>
    ///  控制类型
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// 重启服务命令
        /// </summary>
        Restart,
        /// <summary>
        /// 写入数据命令
        /// </summary>
        Write,
        /// <summary>
        /// 更新缓存命令
        /// </summary>
        Update
    }
}
