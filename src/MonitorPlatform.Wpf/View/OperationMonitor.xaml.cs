

using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Model;
using MonitorPlatform.Wpf.ViewModel;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


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

            // 订阅实时数据委托,更新数据
            GlableDelegateHandler.UpdateRealtimeData = (data) =>
            {
                var iotdata = data.StandardMessage;
                var names = operationMonitorViewModel.GetPointPropNameBySensorCode(iotdata.SensorCode);

                PointStatus state = PointStatus.Normal;
                state = iotdata.Status;
                this.SvgContainer.UpdateValueAsync(names, iotdata.SensorCode, iotdata.Value, (int)state);
                this.operationMonitorViewModel.SetStatus(names, state);
            };
        }

        /// <summary>
        /// 初始化温度点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationMonitorViewModel_InitPoint(object sender, List<DiagramConfigModel> e)
        {
            this.SvgContainer.Initialization();
            var list = new List<TemplateModel>();
            if (e == null) return;

            foreach (var item in e)
            {
                var point = new Point(item.PointX, item.PointY);
                var template = new Template();
                var aa=template.UpdateElement(this.operationMonitorViewModel.TemplateModel,item.SensorCode);
                template.Name = item.PropName;
                list.Add(aa);
                this.SvgContainer.AddUIElement(template, point);
            }

            // 展开所有节点
            this.treeview_station.ExpandAll();
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

            // 展开所有节点
            this.treeview_station.ExpandAll();
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

            // 展开所有节点
            this.treeview_station.ExpandAll();
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
                this.bread.Height = 30;
            }
            else
            {
                // 图纸放大
                this.border_station_info.Width = 0;
                this.bread.Height = 0;
                IsMax = true;
            }
            // 展开所有节点
            this.treeview_station.ExpandAll();
        }
    }
}
