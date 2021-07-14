using HandyControl.Controls;

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
    /// Interaction logic for UserManager.xaml
    /// </summary>
    public partial class UserManager : UserControl
    {
        UserManagerViewModel userManagerViewModel;
        public UserManager()
        {
            InitializeComponent();
            userManagerViewModel = new UserManagerViewModel();
            this.DataContext = userManagerViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button btn=(Button)sender;
                userManagerViewModel.DeleteAction(btn.Tag);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 编辑
            Button btn = (Button)sender;
            if (btn.Tag != null)
            {
                userManagerViewModel.EditAction(btn.Tag);
            }
        }
    }
}
