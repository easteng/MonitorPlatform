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
    /// Interaction logic for Monitor.xaml
    /// </summary>
    public partial class Monitor : UserControl
    {
        MonitorViewModel monitorViewModel;
        public Monitor()
        {
            InitializeComponent();
            this.DataContext = monitorViewModel = new MonitorViewModel();
            monitorViewModel.ReloadImage += MonitorViewModel_ReloadImage;
            this.SvgContainer.PointSelectedEvent += SvgContainer_PointSelectedEvent;
            this.SvgContainer.ElementUpdate += SvgContainer_ElementUpdate;

            // 如果配置数据不为空，则渲染标记点
            monitorViewModel.InitPoint += MonitorViewModel_InitPoint;

            this.tabitem_cofig.Visibility = Visibility.Collapsed;
            this.tabitem_device.Visibility = Visibility.Collapsed;
            this.tabitem_sensor.Visibility = Visibility.Collapsed;

            // 绑定自定义温度点
            foreach (var item in this.radio_container.Children)
            {
                if(item is RadioButton rad)
                {
                    rad.Checked += Rad_Checked;
                }
            }
        }

        private void Rad_Checked(object sender, RoutedEventArgs e)
        {
            var radio=(RadioButton)sender;
            if (radio.IsChecked.Value)
            {
                var color = radio.Foreground.ToString();
                this.monitorViewModel.DiagramConfigModel.ValueColor = color;
            }
        }
        
        #region 测温点添加及图纸管理


        // 初始化测温点
        private void MonitorViewModel_InitPoint(object sender, List<DiagramConfigModel> e)
        {
            this.SvgContainer.Initialization();
            if (e == null) return;

            foreach (var item in e)
            {
                var point = new Point(item.PointX, item.PointY);
                var template = new Template();
                template.UpdateElement(this.monitorViewModel.TemplateModel, item.SensorCode,item.CustomStyle,item.ValueColor);
                template.Name = item.PropName;
                this.SvgContainer.AddUIElement(template, point);
            }
        }

        private TemplateModel GetCustomTemplateModel(bool showBorder,string color)
        {
            var model = new TemplateModel();
            model.BorderThickness = showBorder ? 1 : 0;
            model.DefaultValueForeground = color;
            model.AlertValueForegrund = color;
            model.WaringValueForegrund = color;
            model.BorderBackground = "#00FFFFFF";
            return model;
        }

        // 移动温度元素后更新坐标值

        private void SvgContainer_ElementUpdate(object sender, FrameworkElement e)
        {
            this.monitorViewModel.OpenRightDrawAction("true");
            this.monitorViewModel.UpdatePoint(e.Name, (Point)e.Tag);
        }


        // 编辑状态下点选择  新增温度点

        private void SvgContainer_PointSelectedEvent(object sender, Point point)
        {
            this.monitorViewModel.OpenRightDrawAction("true");
            // 判断上一个温度是否保存
            if (this.monitorViewModel.DiagramConfigModel != null && !this.monitorViewModel.DiagramConfigModel.IsSave)
            {
                //没有保存
                HandyControl.Controls.MessageBox.Show("请先保存当前数据", "温馨提示");
                return;
            }
            // 对坐标进行转换，需要与温度模板的长宽做计算
            var template = new Template();
            template.Name = GenerateName();
            template.UpdateElement(this.monitorViewModel.TemplateModel, "",false,"");
            this.SvgContainer.AddUIElement(template, point);
            this.monitorViewModel.NewPoint(template.Name, point);
        }
        // 生产属性名称
        private string GenerateName()
        {
            var id = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"est_{id}";
        }
        // 加载电路图纸
        private void MonitorViewModel_ReloadImage(object sender, EventArgs e)
        {
            if(sender is bool)
            {
                this.SvgContainer.UnloadDocument(false);
            }
            else if (sender != null)
            {
                this.SvgContainer.LoadDocument(sender.ToString());
            }
        }
        // 编辑按钮切换
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // 是否编辑的按钮切换
            this.SvgContainer.ViewerModel = ESTControl.SvgViewer.SvgViewModel.Edit;
        }
        // 编辑按钮切换
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.SvgContainer.ViewerModel = ESTControl.SvgViewer.SvgViewModel.View;
            this.monitorViewModel.OpenRightDrawAction("false");
        }
        // 保存图纸
        private void btnSavePic_Click(object sender, RoutedEventArgs e)
        {
            // 点击保存当前的数据
            // 调用进度条
            // 提示保存成功
            this.monitorViewModel.SavePicData(this.monitorViewModel.ConfigModel.SelectedFilePath);
        }
        // 配置温度模板样式
        private void brnConfigTemp_Click(object sender, RoutedEventArgs e)
        {
            // 配置温度
            this.monitorViewModel.OpenConfigDrawAction(true);
            this.monitorViewModel.TemplateModel = this.ValueTemplate.UpdateElement(this.monitorViewModel.TemplateModel, "",false,"");
            // 初始化按钮的颜色
            btn_background.Background = GetBrush(this.monitorViewModel.TemplateModel.BorderBackground);
            btn_bordercolor.Background = GetBrush(this.monitorViewModel.TemplateModel.BorderBrush);
            btn_normal.Background = GetBrush(this.monitorViewModel.TemplateModel.ValueForeground);
            btn_waring.Background = GetBrush(this.monitorViewModel.TemplateModel.WaringValueForegrund);
            btn_alert.Background = GetBrush(this.monitorViewModel.TemplateModel.AlertValueForegrund);

            // 初始化数据值
            num_cronradus.Value = this.monitorViewModel.TemplateModel.BorderWidth;
            num_height.Value = this.monitorViewModel.TemplateModel.BorderHeight;
            num_fontsize.Value = this.monitorViewModel.TemplateModel.FontSize;
            num_thinkless.Value = this.monitorViewModel.TemplateModel.BorderThickness;
        }
        // 点击上传电路图
        private void uploadImg_Click(object sender, RoutedEventArgs e)
        {
            // 点击上传电路图
            var file = new OpenFileDialog();
            // 限制svg
            file.Filter = "(.svg)|*svg";
            if (file.ShowDialog() == true)
            {
                var filePath = file.FileName;
                this.monitorViewModel.ConfigModel.SelectedFilePath = filePath; // 记录文件路径
                this.SvgContainer.LoadDocument(filePath);
            }
        }
        // 删除测温点
        private void btn_deletepoint_Click(object sender, RoutedEventArgs e)
        {
            // 删除温度点

            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var name = ((Button)sender).Tag.ToString();
                if (name != null)
                {
                    this.monitorViewModel.DeletePoint(name);
                }
            }
        }
        // 选择传感器
        private void btn_select_sensor_Click(object sender, RoutedEventArgs e)
        {
            // 选择传感器 传感选择界面中只能展示当前站点关联设备所关联的采集器  再通过采集器进行筛选传感器信息
            var frm = new SensorSelectModal();
            frm.ShowTerminal = true;
            frm.PowerRoomId = this.monitorViewModel.SelectedTreeNode.Id;
            frm.Confirm += (e, d) =>
            {
                // 确认数据
                if (d.Any())
                    this.monitorViewModel.SetSensorCode(d[0]);
                frm.Close();
            };
            frm.OpenDialoge();
        }

        // 自定义温度点的颜色



        #endregion

        #region 监测点管理相关 废弃


        

        #endregion

        #region 温度显示模板配置


        private Brush GetBrush(string hex) => new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
        private void btn_bordercolor_Click(object sender, RoutedEventArgs e)
        {
            // 配置温度边框颜色
            OpenColorPickWindow(btn_bordercolor, color =>
            {
                this.monitorViewModel.TemplateModel.BorderBrush = color;
                btn_bordercolor.Background = GetBrush(color);
                UpdateElement();
            }, color =>
            {
                this.monitorViewModel.TemplateModel.BorderBrush = color;
                btn_bordercolor.Background = GetBrush(color);
                UpdateElement();
            });

        }

        /// <summary>
        ///  更新模板要素
        /// </summary>
        private void UpdateElement()
        {
            this.ValueTemplate.UpdateElement(this.monitorViewModel.TemplateModel, "",this.monitorViewModel.DiagramConfigModel.CustomStyle, this.monitorViewModel.DiagramConfigModel.ValueColor);
        }
        private void OpenColorPickWindow(FrameworkElement element, Action<string> change, Action<string> confirm)
        {
            var picker = SingleOpenHelper.CreateControl<HandyControl.Controls.ColorPicker>();
            var window = new HandyControl.Controls.PopupWindow
            {
                PopupElement = picker
            };
            picker.SelectedColorChanged += (sender, e) =>
            {
                change.Invoke(GetColorString(e));
            };
            picker.Confirmed += (sender, e) =>
            {
                confirm.Invoke(GetColorString(e));
                window.Close();
            };
            picker.Canceled += delegate { window.Close(); };
            window.Show(element, false);
        }

        /// <summary>
        /// 获取十六进制的温度值
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetColorString(FunctionEventArgs<Color> data)
        {
            var str = data.Info.ToString();
            if (str.Length > 7)
            {
                str = $"#{str.Substring(3)}";
            }
            return str.ToString();
        }

        private void btn_background_Click(object sender, RoutedEventArgs e)
        {
            // 配置温度背景
            OpenColorPickWindow(btn_background, color =>
            {
                this.monitorViewModel.TemplateModel.BorderBackground = color;
                btn_background.Background = GetBrush(color);
                UpdateElement();
            }, color =>
            {
                btn_background.Background = GetBrush(color);
                UpdateElement();
            });
        }

        private void btn_normal_Click(object sender, RoutedEventArgs e)
        {
            // 配置正常温度颜色
            OpenColorPickWindow(btn_normal, color =>
            {
                this.monitorViewModel.TemplateModel.ValueForeground = color;
                btn_normal.Background = GetBrush(color);
                UpdateElement();
            }, color =>
            {
                this.monitorViewModel.TemplateModel.ValueForeground = color;
                btn_normal.Background = GetBrush(color);
                UpdateElement();
            });
        }

        private void btn_waring_Click(object sender, RoutedEventArgs e)
        {
            // 配置预警温度颜色
            OpenColorPickWindow(btn_waring, color =>
            {
                this.monitorViewModel.TemplateModel.WaringValueForegrund = color;
                btn_waring.Background = GetBrush(color);
                UpdateElement();
            }, color =>
            {
                this.monitorViewModel.TemplateModel.WaringValueForegrund = color;
                btn_waring.Background = GetBrush(color);
                UpdateElement();
            });
        }

        private void btn_alert_Click(object sender, RoutedEventArgs e)
        {
            // 配置报警温度颜色
            OpenColorPickWindow(btn_alert, color =>
            {
                this.monitorViewModel.TemplateModel.AlertValueForegrund = color;
                btn_alert.Background = GetBrush(color);
                UpdateElement();
            }, color =>
            {
                this.monitorViewModel.TemplateModel.AlertValueForegrund = color;
                btn_alert.Background = GetBrush(color);
                UpdateElement();
            });
        }

        private void num_thinkless_ValueChanged(object sender, FunctionEventArgs<double> e)
        {
            // 边框的厚度
            var value = (int)e.Info;
            this.monitorViewModel.TemplateModel.BorderThickness = value;
            UpdateElement();
        }

        private void num_cronradus_ValueChanged(object sender, FunctionEventArgs<double> e)
        {
            // 边框宽度
            var value = (int)e.Info;
            this.monitorViewModel.TemplateModel.BorderWidth = value;
            UpdateElement();
        }

        private void num_fontsize_ValueChanged(object sender, FunctionEventArgs<double> e)
        {
            // 字体大小
            var value = (int)e.Info;
            this.monitorViewModel.TemplateModel.FontSize = value;
            UpdateElement();
        }

        private void num_height_ValueChanged(object sender, FunctionEventArgs<double> e)
        {
            // 边框高度
            var value = (int)e.Info;
            this.monitorViewModel.TemplateModel.BorderHeight = value;
            UpdateElement();
        }

        #endregion

        

        #region 1.左侧树结构相关内容
        // 树节点选中
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.tabitem_cofig.Visibility = Visibility.Collapsed;
            this.tabitem_device.Visibility = Visibility.Collapsed;
            this.tabitem_sensor.Visibility = Visibility.Collapsed;
            var treeNode = ((TreeView)sender).SelectedItem as TreeViewModel;
            if (treeNode == null) return;
            // 监测点选中
            switch (treeNode.NodeType)
            {
                case TreeNodeType.Station:
                    this.tabitem_device.Visibility = Visibility.Visible;
                    break;
                case TreeNodeType.Room:
                    this.tabitem_cofig.Visibility = Visibility.Visible;
                    break;
                case TreeNodeType.Termianl:
                    this.tabitem_sensor.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
            this.monitorViewModel.TreeSelected(treeNode);
        }
        // 添加顶级树节点
        private void btn_add_treenode_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.CreateTreeNode();
        }

        private void btn_addchild_Click(object sender, RoutedEventArgs e)
        {
            // 添加子级
            var tag = ((Button)sender).Tag;
            if (tag != null)
            {
                var model = GetTreeViewModel(this.monitorViewModel.TreeViewModels, Guid.Parse(tag.ToString()));
                this.monitorViewModel.CreateTreeNode(model);
            }
        }

        private void btn_editchild_Click(object sender, RoutedEventArgs e)
        {
            // 编辑数据
            var tag = ((Button)sender).Tag;
            if (tag != null)
            {
                var model = GetTreeViewModel(this.monitorViewModel.TreeViewModels, Guid.Parse(tag.ToString()));
                // this.monitorViewModel.ActiveMonitorId = Guid.Parse(tag.ToString());
                this.monitorViewModel.EditTreeNode(model);
            }
        }

        private void btn_delete_item_Click(object sender, RoutedEventArgs e)
        {
            // 删除数据
            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var tag = ((Button)sender).Tag;
                if (tag != null)
                {
                    var model = GetTreeViewModel(this.monitorViewModel.TreeViewModels, Guid.Parse(tag.ToString()));
                    this.monitorViewModel.DeleteTreeNode(model);
                }
            }
        }

        // 保存站点信息
        private void btn_save_station_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.SaveStation();
        }
        /// <summary>
        /// 获取树节点
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private TreeViewModel GetTreeViewModel(List<TreeViewModel> list, Guid id)
        {
            TreeViewModel res = null;
            foreach (var item in list)
            {
                if (item.Id == id)
                {
                    return item;
                }
                else
                {
                    if (item.Children != null && item.Children.Any())
                    {
                        res = GetTreeViewModel(item.Children, id);
                    }
                }
                if (res != null) return res;
            }
            return res;
        }

        // 保存配电室
        private void btn_save_power_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.SavePowerRoom();
        }


        #endregion

        #region 2 设备管理相关

        // 点击添加设备
        private void btn_add_device_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.CreateDevice();
        }
        // 点击编辑设备
        private void btn_edit_device_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            if (tag != null)
            {
                var id = Guid.Parse(tag.ToString());
                this.monitorViewModel.GetDevice(id);
            }
        }
        // 删除设备
        private void btn_delete_device_Click(object sender, RoutedEventArgs e)
        {
            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var tag = ((Button)sender).Tag;
                if (tag != null)
                {
                    var id = Guid.Parse(tag.ToString());
                    this.monitorViewModel.DeleteDevice(id);
                }
            }
        }
        // 设备协选择
        private void com_device_ptocol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.com_device_ptocol.SelectedItem != null)
            {
                var item = this.com_device_ptocol.SelectedItem;
                this.monitorViewModel.DeviceModel.Ptotocol = item.ToString();
            }
        }
        // 设备采集模式 服务端、客户端、4g、串口
        private void collection_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.collection_type.SelectedValue != null)
            {
                var item = this.collection_type.SelectedValue;
                this.monitorViewModel.DeviceModel.Type = item.ToString();
            }
        }
        // 保存设备信息
        private void btn_save_device_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.SaveDevice();
        }
        #endregion

        #region 3.传感器管理相关
        // 添加传感器信息
        private void btn_addsensor_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.CreateSensor();
        }
        // 删除传感器
        private void btn_delete_sensor_Click(object sender, RoutedEventArgs e)
        {
            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var id = ((Button)sender).Tag.ToString();
                if (id != null)
                {
                    this.monitorViewModel.DeleteSensor(Guid.Parse(id));
                }
            }
        }
        // 保存传感器
        private void btn_save_sensor_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.SaveSensor();
        }
        // 编辑传感器
        private void btn_edit_sensor_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag.ToString();
            this.monitorViewModel.GetSensor(Guid.Parse(id));
        }

        // 传感器写入终端
        private void btn_write_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.WriteSensor();
        }
        #endregion

        #region 4.采集终端管理相关
        private void com_select_device_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((ComboBox)sender).SelectedItem as DeviceModel;
            if (item != null)
            {
                this.monitorViewModel.TerminalModel.DeviceId = item.Id;
            }
        }
        private void btn_save_terminal_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.SaveTerminal();
        }

        #endregion

        // 同步缓存
        private void btn_update_cache_Click(object sender, RoutedEventArgs e)
        {
            this.monitorViewModel.UpdateDeviceCache();
        }
    }
}
