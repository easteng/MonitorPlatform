using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Model;
using MonitorPlatform.Wpf.ViewModel;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for DeviceManager.xaml
    /// </summary>
    public partial class DeviceManager : UserControl
    {
        DeviceManagerViewModel deviceManagerViewModel;
        public DeviceManager()
        {
            InitializeComponent();
            this.DataContext = deviceManagerViewModel = new DeviceManagerViewModel();
            
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            //todo 测试网络
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // 编辑
            var btn = (Button)sender;
            if (btn.Tag != null)
            {
                deviceManagerViewModel.EditAction(btn.Tag);
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                deviceManagerViewModel.DeleteAction(btn.Tag);
            }
        }

        private void devivetype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 设备模式选择
            if (this.devivetype.SelectedValue != null)
            {
                var item = (DeviceCollectionType)this.devivetype.SelectedValue;
                this.deviceManagerViewModel.DeviceModel.Type = item;
            }
        }

       /// <summary>
       /// 数据行的选中事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DeviceListView
            var row = this.DeviceListView.SelectedItem as DeviceModel;   //得到当前选中的行
            
            if(row != null)
            {
                this.deviceManagerViewModel.QueryBindTerminalAction(row.Id);
            }
        }

        private void rowselect_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void deleteBindClient_Click(object sender, RoutedEventArgs e)
        {
            // 删除绑定的客户端
            if (HandyControl.Controls.MessageBox.Show("确定删除绑定的数据吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                var id=Guid.Parse(btn.Tag.ToString());  
                deviceManagerViewModel.DelectBindData(id);
            }
        }

        private void deleteSensor_Click(object sender, RoutedEventArgs e)
        {
            // 删除绑定的传感器
            if (HandyControl.Controls.MessageBox.Show("确定删除绑定的数据吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                var id = Guid.Parse(btn.Tag.ToString());
                deviceManagerViewModel.DelectBindData(id);
            }
        }

        private void rowselect_Checked_1(object sender, RoutedEventArgs e)
        {
            // 复选框选中事件
            var checkbox = (CheckBox)sender;
            var id = Guid.Parse(checkbox.Tag.ToString());
            if (checkbox != null && checkbox.IsChecked.Value)
                // 选中
            this.deviceManagerViewModel.SetChecked(id);
        }

        private void rowselect_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var id = Guid.Parse(checkbox.Tag.ToString());
            // 取消选中
            this.deviceManagerViewModel.SetUnChecked(id);
        }
        // 协议选择
        private void ptotoco_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ptotoco.SelectedValue != null)
            {
                var item = (PtotocolType)this.ptotoco.SelectedValue;
                this.deviceManagerViewModel.DeviceModel.PtotocolType = item;
            }
        }
    }
}
