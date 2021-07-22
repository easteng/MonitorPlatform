using HandyControl.Data;
using HandyControl.Tools;

using Microsoft.Win32;

using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Model;
using MonitorPlatform.Wpf.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonitorPlatform.Wpf.View
{
    /// <summary>
    /// Interaction logic for OperationMonitor.xaml
    /// </summary>
    public partial class OperationMonitor : UserControl
    {
        private OperationMonitorViewModel operationMonitorViewModel;
        public OperationMonitor()
        {
            this.DataContext = operationMonitorViewModel = new OperationMonitorViewModel();
            InitializeComponent();
            operationMonitorViewModel.ReloadImage += OperationMonitorViewModel_ReloadImage;
            operationMonitorViewModel.InitPoint += OperationMonitorViewModel_InitPoint;
        }

        /// <summary>
        /// 初始化温度点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationMonitorViewModel_InitPoint(object sender, List<DiagramConfigModel> e)
        {
            this.SvgContainer.Initialization();
            if (e == null) return;

            foreach (var item in e)
            {
                var point = new Point(item.PointX, item.PointY);
                var template = new Template();
                template.UpdateElement(this.operationMonitorViewModel.TemplateModel);
                template.Name = item.PropName;
                this.SvgContainer.AddUIElement(template, point);
            }
        }

        /// <summary>
        /// 切换图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationMonitorViewModel_ReloadImage(object sender, EventArgs e)
        {
            if (sender != null)
            {
                this.SvgContainer.LoadDocument(sender.ToString());
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // 监测点树选择
            // 监测点选中
            var treeView = ((TreeView)sender).SelectedItem as MonitorModel;
            if (treeView != null)
            {
                operationMonitorViewModel.ActiveMonitorId = treeView.Id;
                operationMonitorViewModel.TreeSelected(treeView);
            }
        }
        private bool IsMax = false;
        private void btn_max_show_Click(object sender, RoutedEventArgs e)
        {
            // 点击最大化图纸显示区域
            if (IsMax)
            {
                // 图纸缩小，恢复原位
                this.border_station_info.Width = 200;
                IsMax = false;
            }
            else
            {
                // 图纸放大
                this.border_station_info.Width = 0;


                IsMax = true;
            }
        }
    }
}
