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
            this.DataContext= monitorViewModel=new MonitorViewModel();
            monitorViewModel.ReloadImage += MonitorViewModel_ReloadImage;
            this.SvgContainer.PointSelectedEvent += SvgContainer_PointSelectedEvent;
            this.SvgContainer.ElementUpdate += SvgContainer_ElementUpdate;

            // 如果配置数据不为空，则渲染标记点
            monitorViewModel.InitPoint += MonitorViewModel_InitPoint;
        }

        private void MonitorViewModel_InitPoint(object sender, List<DiagramConfigModel> e)
        {
            this.SvgContainer.Initialization();
            if (e == null) return;
           
            foreach (var item in e)
            {
                var point = new Point(item.PointX, item.PointY);
                var template = new Template();
                template.UpdateElement(this.monitorViewModel.TemplateModel);
                template.Name = item.PropName;
                this.SvgContainer.AddUIElement(template, point);
            }
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
            if (this.monitorViewModel.DiagramConfigModel!=null&&!this.monitorViewModel.DiagramConfigModel.IsSave)
            {
                //没有保存
                HandyControl.Controls.MessageBox.Show("请先保存当前数据", "温馨提示");
                return;
            }
            // 对坐标进行转换，需要与温度模板的长宽做计算
            var template=new Template();
            template.Name = GenerateName();
            template.UpdateElement(this.monitorViewModel.TemplateModel);
            this.SvgContainer.AddUIElement(template, point);
            this.monitorViewModel.NewPoint(template.Name, point);
        }
        private string GenerateName()
        {
            var id = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"est_{id}";
        }
        private void MonitorViewModel_ReloadImage(object sender, EventArgs e)
        {
            if (sender != null)
            {
                this.SvgContainer.LoadDocument(sender.ToString());
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // 监测点选中
            var treeView = ((TreeView)sender).SelectedItem as MonitorModel;
            if(treeView != null)
            {
                monitorViewModel.ActiveMonitorId = treeView.Id;
                monitorViewModel.TreeSelected(treeView);
            }
        }

        private void btn_addchild_Click(object sender, RoutedEventArgs e)
        {
            // 添加子级
            var tag = ((Button)sender).Tag;
            if (tag != null)
            {
                this.monitorViewModel.ActiveMonitorId = Guid.Parse(tag.ToString());
                this.monitorViewModel.OpenLeftDrawAction("addchild");
            }
        }

        private void btn_editchild_Click(object sender, RoutedEventArgs e)
        {
            // 编辑数据
            var tag = ((Button)sender).Tag;
            if (tag != null)
            {
                this.monitorViewModel.ActiveMonitorId=Guid.Parse(tag.ToString());
                this.monitorViewModel.OpenLeftDrawAction("edit");
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
                    this.monitorViewModel.DeleteMonitorAction(Guid.Parse(tag.ToString()));
                }
            }
        }

        private void uploadImg_Click(object sender, RoutedEventArgs e)
        {
            // 点击上传电路图
            var file = new OpenFileDialog();
            // 限制svg
            file.Filter = "(.svg)|*svg";
            if(file.ShowDialog()==true)
            {
                var filePath=file.FileName;
                this.monitorViewModel.ConfigModel.SelectedFilePath = filePath; // 记录文件路径
                this.SvgContainer.LoadDocument(filePath);
            }
        }

        private void devivetype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

        }

        private void monitorType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 站点类型切换
            var item = ((ComboBox)sender).SelectedItem as ComboxItem;
            if (item != null)
            {
                this.monitorViewModel.MonitorModel.Type = (StationType)item.Value;
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // 是否编辑的按钮切换
            this.SvgContainer.ViewerModel = ESTControl.SvgViewer.SvgViewModel.Edit;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.SvgContainer.ViewerModel = ESTControl.SvgViewer.SvgViewModel.View;
            this.monitorViewModel.OpenRightDrawAction("false");
        }

        private void btnSavePic_Click(object sender, RoutedEventArgs e)
        {
            // 点击保存当前的数据
            // 调用进度条
            // 提示保存成功
            this.monitorViewModel.SavePicData(this.monitorViewModel.ConfigModel.SelectedFilePath);
        }

        private void brnConfigTemp_Click(object sender, RoutedEventArgs e)
        {
            // 配置温度
            this.monitorViewModel.OpenConfigDrawAction(true);
            this.monitorViewModel.TemplateModel=this.ValueTemplate.UpdateElement(this.monitorViewModel.TemplateModel);
            // 初始化按钮的颜色
            btn_background.Background = GetBrush(this.monitorViewModel.TemplateModel.BorderBackground);
            btn_bordercolor.Background = GetBrush(this.monitorViewModel.TemplateModel.BorderBrush);
            btn_normal.Background = GetBrush(this.monitorViewModel.TemplateModel.ValueForeground);
            btn_waring.Background = GetBrush(this.monitorViewModel.TemplateModel.WaringValueForegrund);
            btn_alert.Background = GetBrush(this.monitorViewModel.TemplateModel.AlertValueForegrund);

            // 初始化数据值
            num_cronradus.Value = this.monitorViewModel.TemplateModel.BorderWidth;
            num_height.Value=this.monitorViewModel.TemplateModel.BorderHeight;
            num_fontsize.Value=this.monitorViewModel.TemplateModel.FontSize;
            num_thinkless.Value=this.monitorViewModel.TemplateModel.BorderThickness;
        }
        private Brush GetBrush(string hex)=> new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
        private void btn_bordercolor_Click(object sender, RoutedEventArgs e)
        {
            // 配置温度边框颜色
            OpenColorPickWindow(btn_bordercolor, color => {
                this.monitorViewModel.TemplateModel.BorderBrush = color;
                btn_bordercolor.Background = GetBrush(color);
                UpdateElement();
            }, color => {
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
            this.ValueTemplate.UpdateElement(this.monitorViewModel.TemplateModel);
        }
        private void OpenColorPickWindow(FrameworkElement element,Action<string> change,Action<string> confirm)
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
            var str= data.Info.ToString();
            if (str.Length > 7)
            {
                str = $"#{str.Substring(3)}";
            }
            return str.ToString();
        }

        private void btn_background_Click(object sender, RoutedEventArgs e)
        {
            // 配置温度背景
            OpenColorPickWindow(btn_background, color => {
                this.monitorViewModel.TemplateModel.BorderBackground = color;
                btn_background.Background = GetBrush(color);
                UpdateElement();
            }, color => {
                btn_background.Background = GetBrush(color);
                UpdateElement();
            });
        }

        private void btn_normal_Click(object sender, RoutedEventArgs e)
        {
            // 配置正常温度颜色
            OpenColorPickWindow(btn_normal, color => {
                this.monitorViewModel.TemplateModel.ValueForeground = color;
                btn_normal.Background = GetBrush(color);
                UpdateElement();
            }, color => {
                this.monitorViewModel.TemplateModel.ValueForeground = color;
                btn_normal.Background = GetBrush(color);
                UpdateElement();
            });
        }

        private void btn_waring_Click(object sender, RoutedEventArgs e)
        {
            // 配置预警温度颜色
            OpenColorPickWindow(btn_waring, color => {
                this.monitorViewModel.TemplateModel.WaringValueForegrund = color;
                btn_waring.Background = GetBrush(color);
                UpdateElement();
            }, color => {
                this.monitorViewModel.TemplateModel.WaringValueForegrund = color;
                btn_waring.Background = GetBrush(color);
                UpdateElement();
            });
        }

        private void btn_alert_Click(object sender, RoutedEventArgs e)
        {
            // 配置报警温度颜色
            OpenColorPickWindow(btn_alert, color => {
                this.monitorViewModel.TemplateModel.AlertValueForegrund = color;
                btn_alert.Background = GetBrush(color);
                UpdateElement();
            }, color => {
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

        private void btn_sensorconfig_Click(object sender, RoutedEventArgs e)
        {
        }

        private void com_sensor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 传感器选择
            var sensorcode = ((ComboBox)sender).SelectedValue;
            if (sensorcode != null)
            {
                this.monitorViewModel.DiagramConfigModel.SensorCode = sensorcode.ToString();
            }
           
        }
    }
}
