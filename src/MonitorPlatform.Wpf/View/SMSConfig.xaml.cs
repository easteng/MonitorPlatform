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
    /// SMSConfig.xaml 的交互逻辑
    /// </summary>
    public partial class SMSConfig : UserControl
    {
        SMSConfigViewModel sMSConfigViewModel;
        public SMSConfig()
        {
            InitializeComponent();
            this.DataContext= sMSConfigViewModel=new SMSConfigViewModel();
        }

        private void btn_enable_Click(object sender, RoutedEventArgs e)
        {
            // 是否启用
            var tag = ((Button)sender).Tag;
            this.sMSConfigViewModel.Enable(Guid.Parse(tag.ToString()));
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            // 删除
            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                sMSConfigViewModel.DeleteAction(btn.Tag);
            }
        }

        private void com_comname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 串口号选择
            var row = this.com_comname.SelectedItem.ToString();
            this.sMSConfigViewModel.SMSConfig.ComName = row;
        }

        private void com_baudrate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 波特率先择
            var row = this.com_baudrate.SelectedItem.ToString();
            this.sMSConfigViewModel.SMSConfig.BaudRate = int.Parse(row);
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var tag=((Button)sender).Tag;
            this.sMSConfigViewModel.EditAction(tag);
        }
    }
}
