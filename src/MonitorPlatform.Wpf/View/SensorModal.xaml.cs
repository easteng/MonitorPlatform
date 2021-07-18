using HandyControl.Controls;

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
    /// Interaction logic for SensorModal.xaml
    /// </summary>
    public partial class SensorModal : UserControl, ISingleOpen
    {
        public SensorModal()
        {
            InitializeComponent();
        }

        public bool CanDispose => true;

        public void Dispose()
        {
           // throw new NotImplementedException();
        }
    }
}
