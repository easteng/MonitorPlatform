/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： MenuModel
*******类 说 明： 菜单项实体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/6/2021 12:18:28 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Wpf.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf
{
    public class MenuModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 子菜单项
        /// </summary>
        public List<MenuItemModel> MenuItems { get; set; }=new List<MenuItemModel>();
        /// <summary>
        /// 是否有连接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Font { get; set;  }

        private bool _show;

        public bool Show
        {
            get { return MenuItems.Any(); }
            set { _show = value; }
        }

    }
}
