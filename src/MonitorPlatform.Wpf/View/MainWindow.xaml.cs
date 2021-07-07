using MonitorPlatform.Wpf.Common;
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

namespace MonitorPlatform.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight-10;
            this.DataContext = new MainViewModel();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // todo 
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void menu_item_Click(object sender, RoutedEventArgs e)
        {
            var clickIndex = 0;
            var radioButtons = VisualTreeHelp.FindChilds<RadioButton>(MenuContainer, "menu_item");
            for (var i = 0; i < radioButtons.Count; i++)
            {
                if (radioButtons[i] == sender)
                {
                    clickIndex = i;
                    var radioButton= radioButtons[i];
                    var viewName = radioButton.Tag;
                    ((MainViewModel)this.DataContext).LeftMenuClick(viewName);
                    break;
                }
            }
        }
    }
}
