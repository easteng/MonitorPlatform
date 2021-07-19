using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.View;
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
using System.Windows.Media.Animation;
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
            this.SizeChanged += MainWindow_SizeChanged;
        }

        /// <summary>
        /// 窗体大小编号，同时改变子窗体的大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InitFormSize();
        }
        private void InitFormSize()
        {
            if (this.menuExpand)
            {
                ChangeChildContentWidth(200);
            }
            else
            {
                ChangeChildContentWidth(0);
            }
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
                    var viewType = Type.GetType($"MonitorPlatform.Wpf.View.{viewName.ToString()}");
                    if (viewType == null) return;
                    var contructor = viewType.GetConstructor(Type.EmptyTypes);
                    var ui = (FrameworkElement)contructor.Invoke(null);
                    this.mainContainer.Content = ui;
                    InitFormSize();
                   // ((MainViewModel)this.DataContext).LeftMenuClick(viewName);
                    break;
                }
            }
        }

        private void btn_user_Click(object sender, RoutedEventArgs e)
        {
            user_popue.IsOpen = true;
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            // 点击退出系统
            var exit = new ExitConfirm();
            exit.FormExit += (a, b) =>
             {
                 Application.Current.Shutdown();
             };
            exit.FormMini += (a, b) =>
            {
                this.WindowState = System.Windows.WindowState.Minimized;
                exit.Close();
            };
            exit.ShowDialog();
        }

        private void alertinfo_popute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void alert_info_Click(object sender, RoutedEventArgs e)
        {
            // 状态栏的报警
            this.alertinfo_popute.IsOpen = true;
        }


        #region 左侧菜单收缩展开代码逻辑
        private bool menuExpand = true;
        private DoubleAnimation menuAnimation = new DoubleAnimation()
        {
            BeginTime = TimeSpan.FromMilliseconds(0),
            FillBehavior = FillBehavior.HoldEnd,
            Duration = new Duration(TimeSpan.FromMilliseconds(200))
        };
        private void btn_menut_switch_Click(object sender, RoutedEventArgs e)
        {
            // 左侧菜单切换时间
            if (menuExpand)
            {
                menuExpand = false;
                this.LeftGrid.Width = 0;
                // 收缩菜单
                 menuAnimation.To =0;
                 menuAnimation.From=200;
                ChangeChildContentWidth(0);
                LeftGrid.BeginAnimation(Grid.WidthProperty, menuAnimation);
            }
            else
            {
                menuExpand = true;
                this.LeftGrid.Width = 200;
                //展开菜单
                menuAnimation.To = 200;
                menuAnimation.From = 0;
                LeftGrid.BeginAnimation(Grid.WidthProperty, menuAnimation);
                ChangeChildContentWidth(200);
            }
        }

        private void ChangeChildContentWidth(int width)
        {
            var aa = this.mainContainer;
            FrameworkElement ui = aa.Content as FrameworkElement;
            if (ui != null)
            {
                ui.Width =this.Width-width;
            }
        }
        #endregion


    }
}
