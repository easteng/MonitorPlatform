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
using System.Windows.Shapes;

namespace MonitorPlatform.Wpf.View
{
    /// <summary>
    /// SensorSelectModal.xaml 的交互逻辑
    /// </summary>
    public partial class SensorSelectModal : Window
    {
        public EventHandler<List<Guid>> Confirm;
        public EventHandler Cancle;
        SensorManagerViewModel sensorManagerViewModel { get; set; }
        List<Guid> SelectedSensors { get; set;  }=new List<Guid>();

        public bool ShowTerminal=false;
        // 配电室id
        public Guid PowerRoomId { get;set; }
        public SensorSelectModal()
        {
            InitializeComponent();
            this.DataContext = sensorManagerViewModel = new SensorManagerViewModel();
        }

        public void OpenDialoge()
        {
            //if (ShowTerminal)
            //{
            //    //border_terminal_info.Width = 200;
            //    //sensorManagerViewModel.ColumnWidth = 0;
            //}
            //else
            //{
            //    //border_terminal_info.Width = 0;
            //   // sensorManagerViewModel.ColumnWidth = 80;
            //}
            sensorManagerViewModel.ColumnWidth = 0;
            sensorManagerViewModel.QueryTerminalByPowerId(this.PowerRoomId);
            this.ShowDialog();
        }
        private void SensorSelectModal_Activated(object sender, EventArgs e)
        {
           
        }

        private void rowselect_Checked(object sender, RoutedEventArgs e)
        {
            // 传感器选中事件
            var checkbox = (CheckBox)sender;
            var id = Guid.Parse(checkbox.Tag.ToString());
            if (checkbox != null && checkbox.IsChecked.Value)
            {
                // 选中
                this.SelectedSensors.Add(id);
            }
            else
            {
                // 取消选中
                this.SelectedSensors.Remove(id);
            }
        }

        private void btn_cancle_Click(object sender, RoutedEventArgs e)
        {
            // 取消绑定
            Cancle?.Invoke(this,new EventArgs());
            this.Close();
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            // 确认绑定
            Confirm?.Invoke(this, this.SelectedSensors);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 拖动窗体
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 传感器单选事件
            SelectedSensors.Clear();
            var selected = ((ListView)sender).SelectedItem as SensorModel;
            if (selected!=null){
                SelectedSensors.Add(selected.Id);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 点击采集器，获取传感器列表
            var selectItem=((ListBox)sender).SelectedItem as TerminalModel;
            if (selectItem != null)
            {
                this.sensorManagerViewModel.GetSensorByTerminal(selectItem.Id);
            }
        }
    }
}
