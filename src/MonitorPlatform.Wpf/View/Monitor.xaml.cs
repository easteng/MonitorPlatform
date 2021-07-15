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
            this.monitorViewModel.OpenRightDrawAction("true");
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
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

        }
    }
}
