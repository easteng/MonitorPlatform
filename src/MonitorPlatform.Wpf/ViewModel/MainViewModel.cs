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
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class MainViewModel
    {
        public List<MenuModel> MenuModels { get; set; }
        public CommandBase MenuClickCommand { get; set;  }
        private FrameworkElement _mainContainer;

        public FrameworkElement MainContainer
        {
            get { return _mainContainer; }
            set { _mainContainer = value; }
        }

        public MainViewModel()
        {
            MenuModels = new List<MenuModel>();
            MenuClickCommand=new CommandBase();
            MenuClickCommand.DoExecute = new Action<object>(LeftMenuClick);
            MenuClickCommand.DoCanExecute = new Func<object, bool>(a => true);

            this.BuilderMenus();
            this.LeftMenuClick("Dashboard");
        }

        public void LeftMenuClick(object obj)
        {
          
            var viewType = Type.GetType($"MonitorPlatform.Wpf.View.{obj}");
            if (viewType == null) return;
            var contructor = viewType.GetConstructor(Type.EmptyTypes);
            this.MainContainer = (FrameworkElement)contructor.Invoke(null);
        }

        #region 菜单项配置
        private void BuilderMenus()
        {
            this.MenuModels.Add(new MenuModel()
            {
                Name = "综合监控",
                Font = "&#xe668;",
                Link= "Dashboard"
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "运行监测",
                Font = "&#xe6ef;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="运行监测",Link="network"},
                    new MenuItemModel(){Name="实时列表",Link="network"},
                }
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "数据查询",
                Font = "&#xe663;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="历史数据",Link="network"},
                    new MenuItemModel(){Name="报警数据",Link="network"},
                    new MenuItemModel(){Name="短信查询",Link="network"},
                }
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "监测管理",
                Font = "&#xe687;",
                Link = "network",
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "采集管理",
                Font = "&#xe618;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="传感器管理",Link="network"},
                    new MenuItemModel(){Name="采集服务管理",Link="network"},
                    new MenuItemModel(){Name="设备管理",Link="network"},
                }
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "系统管理",
                Font = "&#xe6b4;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="用户管理",Link="network"},
                    new MenuItemModel(){Name="数据维护",Link="network"},
                    new MenuItemModel(){Name="短息管理",Link="network"}
                }
            });
        }
        #endregion

    }
}
