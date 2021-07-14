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
    /// Interaction logic for SensorManager.xaml
    /// </summary>
    public partial class SensorManager : UserControl
    {
        readonly SensorManagerViewModel SensorManagerViewModel;
        public SensorManager()
        {
            SensorManagerViewModel=new SensorManagerViewModel();
            InitializeComponent();
            this.DataContext = SensorManagerViewModel;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            if (btn.Tag != null)
            {
                SensorManagerViewModel.EditAction(btn.Tag);
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HandyControl.Controls.MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                SensorManagerViewModel.DeleteAction(btn.Tag);
            }
        }
    }
}
