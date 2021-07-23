using MonitorPlatform.Share;
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
    /// Interaction logic for Template.xaml
    /// </summary>
    public partial class Template : UserControl
    {
        TemplateViewModel templateViewModel;
        public Template()
        {
            this.DataContext= templateViewModel=new TemplateViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// 更新要素的值
        /// </summary>
        /// <param name="model"></param>
        public TemplateModel UpdateElement(TemplateModel model,string code)
        {
           return  this.templateViewModel.InitTemplate(model, code);
        }

        /// <summary>
        /// 设置温度值
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <param name="status"></param>
        public void SetValue(string code, double value, int status)
        {
            // 为了方便通过反射传值，这个地方做了转换
            var enumState = (PointStatus)status;
            this.templateViewModel.Update(code,value, enumState);
        }
    }
}
