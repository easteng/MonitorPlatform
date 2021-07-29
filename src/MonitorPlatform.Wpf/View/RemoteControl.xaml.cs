using MonitorPlatform.Wpf.ViewModel;
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
    /// RemoteControl.xaml 的交互逻辑
    /// </summary>
    public partial class RemoteControl : UserControl
    {
        RemoteControlViewModel remoteControlViewModel;
        public RemoteControl()
        {
            InitializeComponent();
            this.DataContext= remoteControlViewModel=new RemoteControlViewModel();
        }

        private void btn_update_data_Click(object sender, RoutedEventArgs e)
        {
            // 更新远程数据
            this.remoteControlViewModel.UpdateMonitorData();
        }

        private void btn_remote_write_Click(object sender, RoutedEventArgs e)
        {
            // 远程写入数据，包括写入传感器编码 温度报警等
        }

        private void btn_restart_server_Click(object sender, RoutedEventArgs e)
        {
            // 重启远程数据采集服务
        }

        private void btn_restart_sms_Click(object sender, RoutedEventArgs e)
        {
            // 重启短信发送服务
        }
    }
}
