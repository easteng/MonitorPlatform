

using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Model;
using MonitorPlatform.Wpf.ViewModel;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            this.treeview_station.FocusableChanged += Treeview_station_FocusableChanged;
        }

        private void Treeview_station_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.treeview_station.ExpandAll();
        }

        /// <summary>
        /// 初始化温度点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationMonitorViewModel_InitPoint(object sender, List<DiagramConfigModel> e)
        {
            this.SvgContainer.Initialization();
            var list = new List<Template>();
            if (e == null) return;

            foreach (var item in e)
            {
                var point = new Point(item.PointX, item.PointY);
                var template = new Template();
                template.UpdateElement(this.operationMonitorViewModel.TemplateModel, item.SensorCode,item.CustomStyle,item.ValueColor);
                template.Name = item.PropName;
                list.Add(template);
                this.SvgContainer.AddUIElement(template, point);
            }

            // 展开所有节点
            this.treeview_station.ExpandAll();
        }
        /// <summary>
        /// 获取自定义的温度点
        /// </summary>
        /// <param name="showBorder"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private TemplateModel GetCustomTemplateModel(bool showBorder, string color)
        {
            var model = new TemplateModel();
            model.BorderThickness = showBorder ? 1 : 0;
            model.DefaultValueForeground = color;
            model.AlertValueForegrund = color;
            model.WaringValueForegrund = color;
            model.BorderBackground = "#00FFFFFF";
            return model;
        }
        /// <summary>
        /// 切换图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationMonitorViewModel_ReloadImage(object sender, EventArgs e)
        {
            if(sender is bool)
            {
                this.SvgContainer.UnloadDocument(false);
            }
            else
            if (sender != null)
            {
                
                Dispatcher.Invoke(new Action(() =>
                {
                    this.SvgContainer.UnloadDocument(true);
                   
                }));
                this.SvgContainer.LoadDocument(sender.ToString());
            }

            // 展开所有节点
            this.treeview_station.ExpandAll();
        }
        // 节点选择
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // 监测点树选择
            // 监测点选中
            var treeNode = ((TreeView)sender).SelectedItem as TreeViewModel;
            operationMonitorViewModel.TreeSelected(treeNode);

            // 显示和隐藏设备的总览信息和具体的配电室信息
            switch (treeNode.NodeType)
            {
                case TreeNodeType.Station:
                    break;
                case TreeNodeType.Room:
                case TreeNodeType.Termianl:
                    // 显示隐藏
                    break;
                default:
                    break;
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
