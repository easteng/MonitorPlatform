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
using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class MainViewModel : NotifyBase
    {
        public RuntimeDataModel RuntimeDataModel { get; set; }=new RuntimeDataModel();
        public List<MenuModel> MenuModels { get; set; }
        public CommandBase MenuClickCommand { get; set;  }
        private FrameworkElement _mainContainer;

        public FrameworkElement MainContainer
        {
            get { return _mainContainer; }
            set { _mainContainer = value; }
        }

        private string realTime;

        public string RealTime
        {
            get { return realTime; }
            set { realTime = value; this.DoNotify(); }
        }


        public MainViewModel()
        {
            MenuModels = new List<MenuModel>();

            GlableDelegateHandler.UpdateRuntime = (s) =>
            {
                RuntimeDataModel.Name = s;
            };
            this.BuilderMenus();
            //this.LeftMenuClick("Dashboard");

            var rep = EngineContext.Current.Resolve<IFreeSql>();
            var aa = rep.GetRepository<User>();
            var list = aa.Where(a => true).ToList(true);



            // 系统时间定时器
            var time = new Timer();
            time.Elapsed += (a, b) =>
            {
                RealTime = DateTime.Now.ToString("HH:mm:ss");
            };
            time.Start();
        }

        private void LeftMenuClick(object obj)
        {
          
            var viewType = Type.GetType($"MonitorPlatform.Wpf.View.{obj.ToString()}");
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
                    new MenuItemModel(){Name="运行监测",Link="OperationMonitor"},
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
                Link = "Monitor",
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "采集管理",
                Font = "&#xe618;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="传感器管理",Link="SensorManager"},
                    new MenuItemModel(){Name="采集器管理",Link="ServerManager"},
                    new MenuItemModel(){Name="设备管理",Link="DeviceManager"},
                    new MenuItemModel(){Name="远程控制",Link="RemoteControl"},
                }
            });
            this.MenuModels.Add(new MenuModel()
            {
                Name = "系统管理",
                Font = "&#xe6b4;",
                MenuItems = new List<MenuItemModel>()
                {
                    new MenuItemModel(){Name="用户管理",Link="UserManager"},
                    new MenuItemModel(){Name="数据维护",Link="network"},
                    new MenuItemModel(){Name="短息管理",Link="network"}
                }
            });
        }
        #endregion

    }
}
