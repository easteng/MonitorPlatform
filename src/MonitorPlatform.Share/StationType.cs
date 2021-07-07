/**********************************************************************
*******命名空间： MonitorPlatform.Share
*******类 名 称： StationType
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/7/2021 11:45:53 PM
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
    public enum StationType
    {
        /// <summary>
        /// 厂区或者站点
        /// </summary>
        Station,
        /// <summary>
        /// 配电室
        /// </summary>
        Region,
        /// <summary>
        /// 具体的设备
        /// </summary>
        Device
    }
}
