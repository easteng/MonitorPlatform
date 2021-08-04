using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;

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
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        public Startup()
        {
            InitializeComponent();
            //this.DataContext = new StartupViewModel();
            GlableDelegateHandler.InitComplate = () =>
            {
                // 模块加载成功，启动main
                Dispatcher.Invoke(() =>
                {
                    var main = new MainWindow();
                    main.Show();
                    Application.Current.MainWindow = main;
                    this.Hide();
                });
            };
            // 启动
            Task.Run(async () =>
            {
                var host = CreateHost().Build();
                 await host.StartAsync();
            });
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private static IHostBuilder CreateHost()
        {
            var host = Host
                .CreateDefaultBuilder()
                .UseConsoleLifetime()
                .ConfigureAppConfiguration((host, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .RegisterLmsServices<MonitorPlatformModule>();  // 注册服务
            return host;
        }
    }

    public class StartupViewModel: NotifyBase
    {
        private Geometry geometry;

        public Geometry PathGeometry
        {
            get { return geometry; }
            set { geometry = value; this.DoNotify(); }
        }

        public StartupViewModel()
        {
            //this.PathGeometry = CreateTextPath("启动中", new Point(0, 0), new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal), 20);
        }

        //public Geometry CreateTextPath(string word, Point point, Typeface typeface, int fontSize)
        //{
        //    FormattedText text = new FormattedText(word,
        //    new System.Globalization.CultureInfo("zh-cn"),
        //    FlowDirection.LeftToRight, typeface, fontSize,
        //    Brushes.Black);
        //    Geometry geo = text.BuildGeometry(point);
        //    PathGeometry path = geo.GetFlattenedPathGeometry();
        //    return path;
        //}
    }
}
