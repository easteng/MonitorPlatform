/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： MenuItemModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/6/2021 12:19:14 AM
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

namespace MonitorPlatform.Wpf.Model
{
    public class MenuItemModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 链接名称
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
