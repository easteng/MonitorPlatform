using MonitorPlatform.Wpf.Model;
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
    /// Interaction logic for Monitor.xaml
    /// </summary>
    public partial class Monitor : UserControl
    {
        MonitorViewModel monitorViewModel;
        public Monitor()
        {
            InitializeComponent();
            this.DataContext= monitorViewModel=new MonitorViewModel();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // 监测点选中
            var treeView = ((TreeView)sender).SelectedItem as MonitorModel;
            if(treeView != null)
            {
                monitorViewModel.ActiveMonitorId = treeView.Id;
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
    }
}
