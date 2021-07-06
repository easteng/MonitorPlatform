/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： MainViewModel
*******类 说 明： 主窗体视图模型
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 9:58:32 PM
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

namespace MonitorPlatform.Wpf.ViewModel
{
    public class MainViewModel
    {
        public List<MenuModel> MenuModels;
        public MainViewModel()
        {
            MenuModels = new List<MenuModel>();
            this.BuilderMenus();
        }

        #region 菜单项配置
        private void BuilderMenus()
        {
            this.MenuModels.Add(new MenuModel()
            {
                Name = "首页",
                Font = "&#xe778;"
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "数据查询",
                Font = "&#xe778;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="历史数据",Link="network"},
                    new MenuItemModel(){Name="报警数据",Link="network"},
                    new MenuItemModel(){Name="短信查询",Link="network"},
                }
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "监控配置",
                Font = "&#xe778;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="站点配置",Link="network"},
                    new MenuItemModel(){Name="配电室配置",Link="network"},
                }
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "采集配置",
                Font = "&#xe778;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="网络配置",Link="network"},
                    new MenuItemModel(){Name="协议配置",Link="network"},
                    new MenuItemModel(){Name="采集器配置",Link="network"},
                    new MenuItemModel(){Name="传感器配置",Link="network"},
                    new MenuItemModel(){Name="短信配置",Link="network"},
                    new MenuItemModel(){Name="报警配置",Link="network"},
                }
            });
        }
        #endregion

    }
}
