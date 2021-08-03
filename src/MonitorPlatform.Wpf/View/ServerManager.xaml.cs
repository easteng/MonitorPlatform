using ESTCore.Message.Client;

using HandyControl.Controls;
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.ViewModel;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
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
    /// 采集器  视图
    /// </summary>
    public partial class ServerManager : UserControl
    {
        ServerManagerViewModel serverManagerViewModel;
       
        public ServerManager()
        {
            serverManagerViewModel = new ServerManagerViewModel();
            InitializeComponent();
            this.DataContext = serverManagerViewModel;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            if (btn.Tag != null)
            {
                serverManagerViewModel.EditAction(btn.Tag);
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                serverManagerViewModel.DeleteAction(btn.Tag);
            }
        }
        // 协议选择
        private void ptotoco_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ptotoco.SelectedValue != null)
            {
                var item = (PtotocolType)this.ptotoco.SelectedValue;
               // this.serverManagerViewModel.TerminalModel.Ptotocol = item;
            }
        }

        private void btn_sensor_Click(object sender, RoutedEventArgs e)
        {
            // 打开底部菜单
            Button btn = (Button)sender;
            this.serverManagerViewModel.BottomShow = true;
            this.serverManagerViewModel.QueryBindSensorAction(btn.Tag);
            // 关联传感器
        }

        private void btn_deleterlt_sensor_Click(object sender, RoutedEventArgs e)
        {
            // 删除指定的关联的传感器
            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                serverManagerViewModel.DelteRltSensor(Guid.Parse(btn.Tag.ToString()));
            }
        }

        private void btn_rlt_sensor_Click(object sender, RoutedEventArgs e)
        {
            // 点击关联传感器
            var sensor = new SensorSelectModal();
            sensor.ShowInTaskbar = true;
            sensor.Confirm += (e, data) =>
            {
                if (this.serverManagerViewModel.SaveRelSensor(data))
                {
                    Growl.Info("关联成功");
                    sensor.Close();
                }
            };
            sensor.ShowDialog();
        }

        // 远程写入传感器
        private void btn_write_Click(object sender, RoutedEventArgs e)
        {
             Button btn = (Button)sender;
             var id=Guid.Parse(btn.Tag.ToString()); 
            this.serverManagerViewModel.WriteSensor(id);
        }
    }
}
