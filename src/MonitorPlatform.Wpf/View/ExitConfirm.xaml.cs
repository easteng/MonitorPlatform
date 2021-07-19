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
    /// Interaction logic for ExitConfirm.xaml
    /// </summary>
    public partial class ExitConfirm : Window
    {
        public EventHandler FormMini;
        public EventHandler FormExit;
        public ExitConfirm()
        {
            InitializeComponent();

            foreach (RadioButton item in radiopanel.Children)
            {
                item.Checked += Item_Checked;
                item.Unchecked += Item_Unchecked;
            }
        }

        private bool Exit { get; set; }
        private bool Minimize { get; set; }

        private void Item_Unchecked(object sender, RoutedEventArgs e)
        {
            var tag = ((RadioButton)sender).Tag.ToString();
            if (tag == "exit")
            {
                Exit = false;
            }
            if (tag == "mini")
            {
                Minimize = false;
            }
        }

        private void Item_Checked(object sender, RoutedEventArgs e)
        {
            var tag = ((RadioButton)sender).Tag.ToString();
            if (tag == "exit")
            {
                Exit = true;
            }
            if (tag == "mini")
            {
                Minimize = true;
            }
        }

        private void btn_cancle_Click(object sender, RoutedEventArgs e)
        {
            // 取消
            this.Close();
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            // 确认
            if (this.Minimize)
            {
                this.FormMini?.Invoke(this,new EventArgs());
            }

            if (this.Exit)
            {
                this.FormExit?.Invoke(this, new EventArgs());
            }
        }
    }
}
