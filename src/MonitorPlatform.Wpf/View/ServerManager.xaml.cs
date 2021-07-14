using MonitorPlatform.Share;
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
    /// Interaction logic for ServerManager.xaml
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

        private void ptotoco_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void ptotoco_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ptotoco.SelectedValue != null)
            {
                var item = (PtotocolType)this.ptotoco.SelectedValue;
                this.serverManagerViewModel.CollectionClientModel.Ptotocol = item;
            }
        }

        private void typecombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.typecombox.SelectedValue != null)
            {
                var item = (DeviceCollectionType)this.typecombox.SelectedValue;
                this.serverManagerViewModel.CollectionClientModel.Type = item;
            }
           
        }

        private void debug_Click(object sender, RoutedEventArgs e)
        {
            // 打开监控界面
            this.serverManagerViewModel.BottomShow = true;
        }
    }
}
